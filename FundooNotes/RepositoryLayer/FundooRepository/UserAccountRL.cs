using CommanLayer;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace RepositoryLayer.FundooRepository
{
    public class UserAccountRL : IUserAccountRL
    {
        private FundooContext fundooContext;
        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="fundooContext"></param>
        public UserAccountRL(FundooContext fundooContext)
        {

            this.fundooContext = fundooContext;

        }

        /// <summary>
        /// Add user in db
        /// </summary>
        /// <param name="addUser"></param>
        /// <returns></returns>
        public bool AddUser(AddUserModel addUser)
        {
            try
            {
                UserAccountDetails user = new UserAccountDetails()
                {
                    FirstName = addUser.FirstName,
                    LastName = addUser.LastName,
                    UserEmail = addUser.UserEmail,
                    Password = EncodePassword(addUser.Password)
                };
                if (user != null)
                {
                    addUser.Password = EncodePassword(addUser.Password);
                    fundooContext.FondooNotes.Add(user);
                    fundooContext.SaveChanges();
                    return true;

                }
                return false;

                //fundooContext.FondooNotes.Add(addUser);
                //int row = fundooContext.SaveChanges();
                //return row == 1 ? addUser : null;

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// List user from database
        /// </summary>
        /// <returns></returns>
        public List<UserAccountDetails> GetFundoo()
        {
            try
            {
                return fundooContext.FondooNotes.ToList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get user by email
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <returns></returns>
        public UserAccountDetails GetUser(string UserEmail)
        {
            try
            {
                var user = this.fundooContext.FondooNotes.SingleOrDefault(x => x.UserEmail == UserEmail);
                return user;
            }
            catch
            {
                throw;
            }
        }
        public LoginResponse UserLogin(string userEmail, string password)
        {
            try
            {
                var userValidation = fundooContext.FondooNotes.FirstOrDefault(user => user.UserEmail == userEmail);
                if (userValidation != null)
                {
                    string pass = DecodePassword(userValidation.Password);
                    if (pass == password)
                    {
                        LoginResponse loginResponse = new LoginResponse();
                        
                        loginResponse.Userid = userValidation.Userid;
                        loginResponse.FirstName = userValidation.FirstName;
                        loginResponse.LastName = userValidation.LastName;
                        loginResponse.UserEmail = userValidation.UserEmail;
                        return loginResponse;
                    }
              
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// forget password
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <returns></returns>
        public UserAccountDetails ForgotPassword(string UserEmail)
        {
            try
            {
                var user = this.fundooContext.FondooNotes.SingleOrDefault(x => x.UserEmail == UserEmail);
                return user;
            }
            catch
            {
                throw;
            }
        }
        ///// <summary>
        ///// EncryptPassword
        ///// </summary>
        ///// <param name="Password"></param>
        ///// <returns></returns>
        //private static string EncryptPassword(string Password)
        //{
        //    try
        //    {

        //        //SHA1 hash value for the input data using the
        //        //implementation provided by the cryptographic service provider (CSP)
        //        var provider = new SHA1CryptoServiceProvider();
        //        //Represents a UTF-16 encoding of Unicode characters.
        //        var encoding = new UnicodeEncoding();
        //        //encrypt the given password string into Encrypted data  
        //        byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
        //        String encrypted = Convert.ToBase64String(encrypt);
        //        return encrypted;
        //    }
        //    catch
        //    {

        //        throw;
        //    }
        //}
        /// <summary>
        /// ability to encrypt password using UTF8 standards
        /// </summary>
        /// <param name="password">user password</param>
        /// <returns>encrypted password</returns>
        private string EncodePassword(string password)
        {
            byte[] encData = new byte[password.Length];
            encData = Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData);
            return encodedData;
        }
        /// <summary>
        /// ability to decrypt password into human readable format
        /// </summary>
        /// <param name="encPassword"></param>
        /// <returns>decrypted password</returns>
        private string DecodePassword(string encPassword)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder decoder = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(encPassword);
                int charCount = decoder.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodeChar = new char[charCount];
                decoder.GetChars(todecodeByte, 0, todecodeByte.Length, decodeChar, 0);
                string password = new String(decodeChar);
                return password;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(ResetPassword reset, int userId)
        {
            if (reset.NewPassword == reset.ConfirmPassword)
            {
                var user = this.fundooContext.FondooNotes.FirstOrDefault(user => user.Userid == userId);
                if (user != null)
                {
                    user.Password = reset.NewPassword;
                    int row = fundooContext.SaveChanges();
                    return row == 1 ? true : false;
                }
            }
            return false;
        }
    }
}
