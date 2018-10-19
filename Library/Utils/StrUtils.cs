using System;
using System.Linq;

namespace Library.Utils
{
    public static class StrUtils
    {
		/// <summary>
		/// True: "true", "TRUE", "1", "yes", "Yes" etc.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="dftValue"></param>
		/// <returns></returns>
		public static bool CvtStrToBool(string text, bool dftValue)
		{
			if (string.IsNullOrEmpty(text))
				return dftValue;

			if (text.Equals("1"))
				return true;

			if (text.Equals("0"))
				return false;

			if (text.ToUpper().Equals("YES"))
				return true;

			if (text.ToUpper().Equals("NO"))
				return false;

			bool ret;
			return Boolean.TryParse(text, out ret) ? ret : dftValue;
		}

		public static int CvtStrToInt(string text, int dftValue)
		{
			if (string.IsNullOrEmpty(text))
				return dftValue;

			int ret;
			return int.TryParse(text, out ret) ? ret : dftValue;
		}

		public static long CvtStrToLong(string text, long dftValue)
		{
			if (string.IsNullOrEmpty(text))
				return dftValue;

			long ret;
			return long.TryParse(text, out ret) ? ret : dftValue;
		}

		public static decimal CvtStrToDecimal(string text, decimal dftValue)
		{
			if (string.IsNullOrEmpty(text))
				return dftValue;

			decimal ret;
			return decimal.TryParse(text, out ret) ? ret : dftValue;
		}

		public static float CvtStrToFloat(string text, float dftValue)
		{
			if (string.IsNullOrEmpty(text))
				return dftValue;

			float ret;
			return float.TryParse(text, out ret) ? ret : dftValue;
		}

		public static string Truncate(string value, int maxLength)
		{
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}

		/// <summary>
		/// Returns the left portion of the text of the given length.
		/// </summary>
		/// <param name="text">source text</param>
		/// <param name="length">number of characters to return</param>
		public static string Left(string text, int length)
		{
			string ret = "";
			if (text != null)
			{
				if (length < 0)
				{
					length = -length;
				}
				if (length >= text.Length)
				{
					ret = text;
				}
				else
				{
					ret = text.Substring(0, length);
				}
			}
			return ret;
		}

		/// <summary>
		/// Returns the right portion of the text of the given length.
		/// </summary>
		/// <param name="text">source text</param>
		/// <param name="length">number of characters to return</param>
		public static string Right(string text, int length)
		{
			string ret = "";
			if (text != null)
			{
				if (length < 0)
				{
					length = -length;
				}
				if (length >= text.Length)
				{
					ret = text;
				}
				else
				{
					ret = text.Substring(text.Length - length);
				}
			}
			return ret;
		}

		/// <summary>
		/// Returns the right portion of the text starting from the given index.
		/// </summary>
		/// <param name="text">source text</param>
		/// <param name="offset">start index</param>
		public static string From(string text, int offset)
		{
			string ret = "";
			if (text != null)
			{
				if (offset < 0)
				{
					offset = -offset;
				}
				if (offset >= text.Length)
				{
					ret = "";
				}
				else
				{
					ret = text.Substring(offset);
				}
			}
			return ret;
		}

        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + p.GetValue(obj, null);
            //select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

    }
}
