
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
    }

}
