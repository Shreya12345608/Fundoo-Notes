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
        public bool AddUser(AddUserModel addUser);

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginResponse LoginAccount(string userEmail, string password);
        /// <summary>
        /// Create token
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string CreateToken(string userEmail, int userid);
        /// <summary>
        /// Forger password
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <returns></returns>
        public bool ForgotPassword(string UserEmail);

        /// <summary>
        /// Reset Password Method
        /// </summary>
        /// <param name="resetPassword">Reset Password</param>
        /// <returns>boolean result</returns>
        public bool ResetPassword(ResetPassword reset, int userId);
    }
}
