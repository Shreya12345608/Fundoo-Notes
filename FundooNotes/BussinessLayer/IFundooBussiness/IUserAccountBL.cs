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
    }
}
