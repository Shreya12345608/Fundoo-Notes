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
    }
}
