using CommanLayer;
using RepositoryLayer.IRepository;
using RepositoryLayer.MSMQService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public UserAccountDetails UserLogin(string userEmail, string password)
        {
            try
            {

                //string pass = EncryptPassword(password);
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
        public bool ForgotPassword(string UserEmail)
        {
            try
            {
                bool result;
                string user;
                string mailSubject = "Link to reset your FundooNotes App Credentials";
                var userCheck = this.fundooContext.FondooNotes.SingleOrDefault(x => x.UserEmail == UserEmail);
                if (userCheck != null)
                {
                    msmqUtility msmq = new msmqUtility();
                    msmq.SendMessage();
                    var messageBody = msmq.receiverMessage();
                    user = messageBody;
                    using (MailMessage mailMessage = new MailMessage("malviyashreya26@gmail.com", UserEmail))
                    {
                        mailMessage.Subject = mailSubject;
                        mailMessage.Body = user;
                        mailMessage.IsBodyHtml = true;
                        SmtpClient Smtp = new SmtpClient();
                        Smtp.Host = "smtp.gmail.com";
                        Smtp.EnableSsl = true;
                        Smtp.UseDefaultCredentials = false;
                        Smtp.Credentials = new NetworkCredential("malviyashreya26@gmail.com", "Shreya@123");
                        Smtp.Port = 587;
                        Smtp.Send(mailMessage);
                    }

                    result = true;
                    return result;
                }

                result = false;
                return result;
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
        /// reset password
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(ResetPassword resetPassword)
        {
            var user = this.fundooContext.FondooNotes.FirstOrDefault(user => user.UserEmail == resetPassword.UserEmail);
            if (user != null)
            {
                user.Password = resetPassword.NewPassword;
                int row = fundooContext.SaveChanges();
                return row == 1 ? true : false;
            }
            return false;
        }
    }
}
