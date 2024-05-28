using System.Security.Cryptography;
using System.Text;

namespace Business.Lib
{

	public class TCryptor
	{
		private const string fPasswordEncryptKey = "fyh**O9.!&%ky*-6*9-!";

		public string mGetEncryptedValue(string val)
		{
			Encryption64 e64 = new Encryption64(fPasswordEncryptKey);
			return e64.Encrypt(val);
		}
		public string mGetDecryptedValue(string val)
		{
			Encryption64 e64 = new Encryption64(fPasswordEncryptKey);
			return e64.Decrypt(val);
		}
	}
	public class Encryption64
	{
		public string sEncryptionKey;
		public Encryption64(string encryptionKey)
		{
			sEncryptionKey = encryptionKey;
		}
		public string Decrypt(string stringToDecrypt)
		{
			if (string.IsNullOrEmpty(stringToDecrypt))
				return null;

			byte[] key = { };
			byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
			byte[] inputByteArray = new byte[stringToDecrypt.Length];
			try
			{
				key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
				DESCryptoServiceProvider des = new DESCryptoServiceProvider();
				inputByteArray = Convert.FromBase64String(stringToDecrypt);
				MemoryStream ms = new MemoryStream();
				CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
				cs.Write(inputByteArray, 0, inputByteArray.Length);
				cs.FlushFinalBlock();
				Encoding encoding = Encoding.UTF8;
				return encoding.GetString(ms.ToArray());
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public string Encrypt(string stringToEncrypt)
		{
			byte[] key = { };
			byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
			byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length) 
			try
			{
				key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
				DESCryptoServiceProvider des = new DESCryptoServiceProvider();
				inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
				MemoryStream ms = new MemoryStream();
				CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
				cs.Write(inputByteArray, 0, inputByteArray.Length);
				cs.FlushFinalBlock();
				return Convert.ToBase64String(ms.ToArray());
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
