using System;
using System.Collections.Generic;
using System.Threading;

namespace MDLibrary.Utils
{
	/// <summary>
	/// Universal Custom thread pool running tasks passed as a parameter
	/// </summary>
	public class ParallelTasksPool
	{
		private readonly int m_maxThreads;
		private readonly TaskItem[] m_taskItems;

		public enum EScheduleResult
		{
			Success,
			TaskAlareadyRunning,
			NoSlots,
			InvalidSlot,
			InvalidThreadState,
			FailedToStartThread
		}

		public ParallelTasksPool(int maxThreads)
		{
			maxThreads = NumUtils.BoundValue(maxThreads, 0, 200); //200 set as hard upper limit - safety value

			m_maxThreads = maxThreads;
			m_taskItems = new TaskItem[m_maxThreads];
			for (int i = 0; i < maxThreads; i++)
			{
				m_taskItems[i] = new TaskItem();
			}
		}

		public EScheduleResult AddTask(long taskID, object taskData, Action<long, object> action)
		{
			if (CheckTaskIsRunning(taskID))
				return EScheduleResult.TaskAlareadyRunning;

			int slot = FindAvailableSlot();
			if (slot >= 0)
			{
				return ExecuteAction(taskID, taskData, slot, action);
			}

			return EScheduleResult.NoSlots;
		}

		public IEnumerable<long> ExpireTasks(int timeoutSecs)
		{
			List<long> abortedTasks = new List<long>();

			for (int i = 0; i < m_maxThreads; i++)
			{
				if (m_taskItems[i].GetRunDurationSecs() > timeoutSecs)
				{
					try
					{
						abortedTasks.Add(m_taskItems[i].TaskId);
						m_taskItems[i].Abort();
					}
					catch (ThreadAbortException abortException)
					{
						System.Diagnostics.Debug.WriteLine(abortException.Message);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
					}
				}
			}
			return abortedTasks;
		}

		/// <summary>
		/// Diagnotics helper, returns state of the engine as text
		/// </summary>
		public IEnumerable<string> GetSlotsState()
		{
			List<string> ret = new List<string>();
			lock (m_taskItems)
			{
				for (int i = 0; i < m_maxThreads; i++)
				{
					ret.Add(string.Format("[{0}] task ID ={1}, state={2}", i, m_taskItems[i].TaskId, m_taskItems[i].State));
				}
			}
			return ret;
		}


		#region Implementation

		private bool CheckTaskIsRunning(long taskId)
		{
			bool isRunning = false;
			lock (m_taskItems)
			{
				for (int i = 0; i < m_maxThreads; i++)
				{
					if (m_taskItems[i].State == ETaskState.Running && m_taskItems[i].TaskId == taskId)
					{
						isRunning = true;
						break;
					}
				}
			}
			return isRunning;
		}

		private int FindAvailableSlot()
		{
			int slotnum = -1;

			lock (m_taskItems)
			{
				for (int i = 0; i < m_maxThreads; i++)
				{
					if (m_taskItems[i].State == ETaskState.Idle)
					{
						slotnum = i;
						break;
					}
				}
			}
			return slotnum;
		}

		private EScheduleResult ExecuteAction(long taskID, object taskData, int slot, Action<long,object> action)
		{
			if (slot < 0 || slot >= m_maxThreads)
				return EScheduleResult.InvalidSlot;

			if (m_taskItems[slot].State != ETaskState.Idle)
				return EScheduleResult.InvalidThreadState;

			lock (m_taskItems)
			{
				try
				{
					m_taskItems[slot].Start(taskID,taskData,action);
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					return EScheduleResult.FailedToStartThread;
				}
			}
			return EScheduleResult.Success;
		}

		private class TaskItem
		{
			public ETaskState State;
			public long TaskId { get; set; }

			private Thread m_threadHandle;
			private DateTime m_startTimestamp;

			public TaskItem()
			{
				State = ETaskState.Idle;
				TaskId = 0;
				m_threadHandle = null;
				m_startTimestamp = DateTime.UtcNow;
			}


			public void Start(long taskID, object taskData, Action<long, object> action)
			{
				State = ETaskState.Running;
				TaskId = taskID;
				m_startTimestamp = DateTime.UtcNow;

				m_threadHandle = new Thread(() =>
												{
													try
													{
														System.Diagnostics.Debug.WriteLine("Processing of task {0} started", taskID);
														action(TaskId, taskData);
														System.Diagnostics.Debug.WriteLine("Processing of task {0} ended", taskID);
													}
													catch (Exception ex)
													{
														System.Diagnostics.Debug.WriteLine(ex.Message);
													}
													finally
													{
														m_threadHandle = null;
														State = ETaskState.Idle;
													}
												})
									 {
										 IsBackground = true
									 };
				m_threadHandle.Start();
			}

			public double GetRunDurationSecs()
			{
				if (State == ETaskState.Idle)
					return 0;

				TimeSpan duration = DateTime.UtcNow - m_startTimestamp;
				return duration.TotalSeconds;
			}

			public void Abort()
			{
				if (m_threadHandle != null)
				{
					//exception intentionally not caught here
					m_threadHandle.Abort();
					m_threadHandle = null;
				}
			}


		}

		private enum ETaskState
		{
			Idle,
			Running
		}

		#endregion

	}
}
