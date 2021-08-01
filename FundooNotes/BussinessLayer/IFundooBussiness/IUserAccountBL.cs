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
    }
}
