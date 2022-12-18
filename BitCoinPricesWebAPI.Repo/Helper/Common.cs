using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
namespace BitCoinPricesWebAPI.Repo.Helper
{
    public static class Common
    {
        public static string AllPersonsFound;
        private static readonly IConfiguration configuration;
        public static string EncryptString(string plainText, IConfiguration configuration)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(configuration.GetSection("Encryption").GetSection("Key").Value);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
        public static string DecryptString(string cipherText, IConfiguration configuration)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(configuration.GetSection("Encryption").GetSection("Key").Value);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string DateToSQLString(DateTime Date)
        {
            // Convert a date to string in format yyyy-mm-dd
            string Day = Convert.ToString(Date.Day);
            string Month = Convert.ToString(Date.Month);
            string Year = Convert.ToString(Date.Year);
            if (Day.Length == 1)
                Day = '0' + Day;
            if (Month.Length == 1)
                Month = '0' + Month;
            return String.Format("{0}-{1}-{2}", Year, Month, Day);
        }
        public enum ConnectionStrings 
        {
            BTCDB
        }
    }
}
