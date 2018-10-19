using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MDLibrary.Utils
{
	public static class CryptUtils
	{
		private static byte[] m_cryptKey = Encoding.ASCII.GetBytes("keifaocsgeldmfsmosntoufgbhsighsh"); // 32 bytes
		private static readonly byte[] m_initVector = Encoding.ASCII.GetBytes("furntsstheotnfns"); //16 bytes

		public static void SetKey(byte[] cryptKey)
		{
			m_cryptKey = cryptKey;
		}

		public static byte[] GetKey()
		{
			return m_cryptKey;
		}

		public static string EncryptString(string clearText)
		{
		    if (clearText == null) return "";

			byte[] clearTextBytes = Encoding.UTF8.GetBytes(clearText);

			SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

			MemoryStream ms = new MemoryStream();

			CryptoStream cs = new CryptoStream(ms, rijn.CreateEncryptor(m_cryptKey, m_initVector), CryptoStreamMode.Write);

			cs.Write(clearTextBytes, 0, clearTextBytes.Length);
			cs.Close();

			return Convert.ToBase64String(ms.ToArray());
		}

		public static string DecryptString(string encryptedText)
		{
			try
			{
				byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

				MemoryStream ms = new MemoryStream();

				SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

				CryptoStream cs = new CryptoStream(ms, rijn.CreateDecryptor(m_cryptKey, m_initVector),
												   CryptoStreamMode.Write);

				cs.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
				cs.Close();

				return Encoding.UTF8.GetString(ms.ToArray());
			}
			catch (Exception ex)
			{
				// the text most probably hasn't been encrypted
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return encryptedText;
			}
		}
	}
}
