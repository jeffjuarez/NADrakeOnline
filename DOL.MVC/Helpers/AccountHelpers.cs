using System;
using System.Security.Cryptography;
using DOL.MVC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DOL.MVC.Helpers
{
    public static class AccountHelpers
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash
        private const int Pbkdf2Iterations = 1000;

        public static AccountData HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
            return new AccountData() {Salt = Convert.ToBase64String(salt), HashPassword = Convert.ToBase64String(hash)};
        }

        public static bool ValidatePassword(string password, string storedSalt, string storedPassword)
        {
            var salt = Convert.FromBase64String(storedSalt);
            var hash = Convert.FromBase64String(storedPassword);

            var testHash = Pbkdf2(password, salt, Pbkdf2Iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }


        public static bool ValidatePasswordDOL(string userid, string passwordSupplied, string storedPassword)
        {
            bool boolResult = false;
            //string connstr = @"Data Source=.\SQLEXPRESS;Initial Catalog=olo_nz;Integrated Security=SSPI";
         //   string connstr = @"Data Source=10.3.3.11;Initial Catalog=olo_nz;user id=jjuarez;Password=Manil@01 providerName=System.Data.SqlClient";
            string connstr = ConfigurationManager.ConnectionStrings["DOLDataContext"].ConnectionString;




            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = new SqlCommand("dbo.olo_get_userpassword", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                cmd.Parameters.Add("@passwordtocompare", SqlDbType.VarChar).Value = passwordSupplied;

                cmd.Parameters.Add("@UpdateResult", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;

                cn.Open();
                cmd.ExecuteNonQuery();

                string _retPassword = cmd.Parameters["@UpdateResult"].Value.ToString();

                if (_retPassword == "1") { boolResult = true;}


                return boolResult;
            }


         //   var salt = Convert.FromBase64String(storedSalt);
            //var hash = Convert.FromBase64String(storedPassword);



            return true;
         //   var testHash = Pbkdf2(password, salt, Pbkdf2Iterations, hash.Length);
         //   return SlowEquals(hash, testHash);
        }


        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) {IterationCount = iterations};
            return pbkdf2.GetBytes(outputBytes);
        }


      

    }
}