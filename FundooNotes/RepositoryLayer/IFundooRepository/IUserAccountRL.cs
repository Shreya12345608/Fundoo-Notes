using System;
using System.Collections.Generic;
using System.Text;
using CommanLayer;

namespace RepositoryLayer.IRepository
{
    public interface IUserAccountRL
    {
        /// <summary>
        /// list all the user
        /// </summary>
        /// <returns></returns>
        public List<UserAccountDetails> GetFundoo();

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="adduser"></param>
        ///<returns></returns>
        UserAccountDetails AddUser(UserAccountDetails addUser);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        UserAccountDetails UserLogin(string userEmail, string password);
    }
}
