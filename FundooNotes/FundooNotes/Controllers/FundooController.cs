﻿using BussinessLayer.IFundooBussiness;
using CommanLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundooController : ControllerBase
    {
        private IUserAccountBL Fundoo;
        public FundooController(IUserAccountBL fundoo)
        {
            this.Fundoo = fundoo;

        }
        /// <summary>
        /// Controller method for Adds the specified model.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFundoo()
        {
            try
            {
                var fundoos = Fundoo.GetFundoo();
                return this.Ok(new { Success = true, Message = "Get User SuccessFull", Data = fundoos });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="adduser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registration")]
        public ActionResult AddUser(UserAccountDetails adduser)
        {
            try
            {
                Fundoo.AddUser(adduser);
                return Created(adduser.Userid.ToString(), adduser);
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
