
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.IRepository;
using CommanLayer;
using BussinessLayer.IFundooBussiness;

namespace BussinessLayer.Service
{
    public class UserAccountBL : IUserAccountBL
    {
        //instance variable
        private IUserAccountRL fundoo;
        public UserAccountBL(IUserAccountRL fundoo)
        {
            this.fundoo = fundoo;
        }

        //-------------------------ADD USER------------------------------------------//
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="adduser"></param>
        /// <returns></returns>
        public UserAccountDetails AddUser(UserAccountDetails addUser)
        {
            try
            {
                fundoo.AddUser(addUser);
                return addUser;
            }
            catch
            {
                throw;
            }
        }

        //---------------------------------GET DETAILS-------------------------------------------------//

        /// <summary>
        /// Method for   Get USer
        /// </summary>
        /// <returns></returns>
        public List<UserAccountDetails> GetFundoo()
        {
            try
            {
                return this.fundoo.GetFundoo();
            }
            catch
            {
                throw;
            }
        }

        public UserAccountDetails LoginAccount(string userEmail, string password)
        {
            try
            {

                return this.fundoo.UserLogin(userEmail, password);

            }
            catch
            {
                throw;
            }
        }
    }

}
