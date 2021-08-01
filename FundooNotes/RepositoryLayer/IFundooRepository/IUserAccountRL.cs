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
    }
}
