using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace DigitalMVC
{
    class SHAHash
    {
        public static byte[] generatedSalt;

        public static Tuple<string, string> HashPassword(string userName, string password)
        {
            string plainText = password;
            string hashedPassword = string.Empty;
            byte[] salt = GenerateSalt();
            byte[] hashWithSaltBytes;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSalt = new byte[plainTextBytes.Length + salt.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSalt[i] = plainTextBytes[i];

            for (int i = 0; i < salt.Length; i++)
                plainTextWithSalt[plainTextBytes.Length + i] = salt[i];


            SHA256Managed hashedString = new SHA256Managed();
            byte[] hash = hashedString.ComputeHash(plainTextWithSalt);
            hashWithSaltBytes = new byte[hash.Length + salt.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hash.Length; i++)
                hashWithSaltBytes[i] = hash[i];

            // Append salt bytes to the result.
            for (int i = 0; i < salt.Length; i++)
                hashWithSaltBytes[hash.Length + i] = salt[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);
            string base64Salt = Convert.ToBase64String(generatedSalt);
            Tuple<string, string> loginInfo = Tuple.Create(hashValue, base64Salt);

            if (StoreInDbSuccess(hashValue, base64Salt))
            {
                Console.WriteLine("Stored in DB");
            }
            else
            {
                Console.WriteLine("Didn't store in DB Successfully");
            }

            Console.WriteLine("Hashed string : " + hashValue + "Salt : " + generatedSalt);
            Console.WriteLine("Base 64 salt : " + base64Salt);
            CheckPassword(plainText, hashValue, generatedSalt);

            return loginInfo;
        }

        public static byte[] GenerateSalt()
        {
            byte[] saltBytes;
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);
            Console.WriteLine("Salt size : " + saltSize);

            // Allocate a byte array, which will hold the salt.
            saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);
            // rng.GetNonZeroBytes(saltBytes);

            //string result = System.Text.Encoding.UTF8.GetString(byteArray);
            generatedSalt = saltBytes;
            return saltBytes;
        }

        public static void CheckPassword(string plainText, string computedHash, byte[] previousSalt)
        {

            string hashedPassword = string.Empty;
            byte[] salt = previousSalt;
            byte[] hashWithSaltBytes;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSalt = new byte[plainTextBytes.Length + salt.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSalt[i] = plainTextBytes[i];

            for (int i = 0; i < salt.Length; i++)
                plainTextWithSalt[plainTextBytes.Length + i] = salt[i];


            SHA256Managed hashedString = new SHA256Managed();
            byte[] hash = hashedString.ComputeHash(plainTextWithSalt);
            hashWithSaltBytes = new byte[hash.Length + salt.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hash.Length; i++)
                hashWithSaltBytes[i] = hash[i];

            // Append salt bytes to the result.
            for (int i = 0; i < salt.Length; i++)
                hashWithSaltBytes[hash.Length + i] = salt[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);
            Console.WriteLine("Checking hash - " + hashValue + " Salt : " + generatedSalt);

            if (hashValue == computedHash)
            {
                Console.WriteLine("IT WORKS");
            }
            else
            {
                Console.WriteLine("Doesn't work");
            }
        }

        public static bool StoreInDbSuccess(string baseHashedPassword, string baseSalt)
        {
            DBConnectionHelper dbcon = new DBConnectionHelper("digitaloceanmvc");
            dbcon.IsConnect();
            //dbcon.InsertRecord();
            foreach (string thing in dbcon.GetAllValues())
            {
                Console.WriteLine(thing);
            }
            dbcon.Close();

            return true;
        }
       
    }
}
