using CommanLayer;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                //UserAccountDetails userAccountDetails = new UserAccountDetails();
                //userAccountDetails.Password = pass;
                return userValidation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
