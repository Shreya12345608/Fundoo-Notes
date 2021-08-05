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
        public UserAccountDetails AddUser(UserAccountDetails addUser)
        {
            try
            {
                fundooContext.FondooNotes.Add(addUser);
                int row = fundooContext.SaveChanges();
                return row == 1 ? addUser : null;
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
        public UserAccountDetails UserLogin(string userEmail, string password)
        {
            try
            {

               // string pass = EncryptPassword(password);
                UserAccountDetails userValidation = fundooContext.FondooNotes.FirstOrDefault(user => user.UserEmail == userEmail && user.Password == password);
                //userAccountDetails.Password = pass;
                return userValidation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
        /// <summary>
        /// EncryptPassword
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        private static string EncryptPassword(string Password)
        {
            try
            {

                //SHA1 hash value for the input data using the
                //implementation provided by the cryptographic service provider (CSP)
                var provider = new SHA1CryptoServiceProvider();
                //Represents a UTF-16 encoding of Unicode characters.
                var encoding = new UnicodeEncoding();
                //encrypt the given password string into Encrypted data  
                byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
                String encrypted = Convert.ToBase64String(encrypt);
                return encrypted;
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
