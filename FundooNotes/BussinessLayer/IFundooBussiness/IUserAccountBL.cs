using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.IFundooBussiness
{
    public interface IUserAccountBL
    {
        /// <summary>
        /// list all the user
        /// </summary>
        /// <returns></returns>
        public List<UserAccountDetails> GetFundoo();

        /// <summary>
        ///add new user
        /// </summary>
        /// <param name="adduser"></param>
        /// <returns></returns>
        public UserAccountDetails AddUser(UserAccountDetails addUser);

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccountDetails LoginAccount(string userEmail, string password);

    }
}
