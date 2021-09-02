using BussinessLayer.IFundooBussiness;
using CommanLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
namespace FundooNotes.Controllers
{
    [EnableCors()]
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
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="adduser"></param>
        /// <returns></returns>
        // [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult AddUser(AddUserModel adduser)
        {
            try
            {
                Fundoo.AddUser(adduser);
                return Created(adduser.Userid.ToString(), adduser);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().GetType() == typeof(SqlException))
                {
                    Int32 ErrorCode = ((SqlException)ex.InnerException).Number;
                    switch (ErrorCode)
                    {
                        case 2627:  // Unique constraint error
                            return this.BadRequest(new { Success = false, Message = " Unique constraint error", StackTrace = ex.StackTrace });

                        case 547:   // Constraint check violation
                            return this.BadRequest(new { Success = false, Message = " Constraint check violation", StackTrace = ex.StackTrace });

                        case 2601:  // Duplicated key row error
                            return this.BadRequest(new { Success = false, Message = " Duplicated Email ID. Please enter Unique Email IDow ", StackTrace = ex.StackTrace });
                        default:
                            break;
                    }
                }
                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        /// <summary>
        ///Controller method for Logins the instance.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UserLogin(LoginModel loginModel)
        {
            try
            {

                var users = Fundoo.LoginAccount(loginModel.UserEmail, loginModel.Password);
                if (users != null)
                {
                    string Token = Fundoo.CreateToken(users.UserEmail, users.Userid);
                    return Ok(new { sucess = true, message = "User Logged in Successfull", Data = users.FirstName,users.LastName,users.UserEmail ,Token });

                }
                return NotFound(new { sucess = false, message = "Invalid details- Login Failed" });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        /// <summary>
        /// Controller method for Forget Password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPost]
        [Route("forget-Password")]
        public ActionResult ForgotPassword(ForgetPasswordModel user)
        {
            try
            {
                bool forgetpass = Fundoo.ForgotPassword(user.UserEmail);

                Task.Delay(5000);
                if (forgetpass)
                {
                    return Ok(new { Success = true, message = "Link has sent to the given email address to reset the password" });

                }
                return NotFound(new { Sucess = false, message = "Unable to sent link to given email address.This Email doesn't exist in database." });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }

        }
        /// <summary>
        /// Controller method for Reset Password
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("reset-password")]
        public ActionResult ResetPassword([FromRoute] string Token, ResetPassword resetPassword)
        {

            try
            {

                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                int userId = Convert.ToInt32(principal.Claims.SingleOrDefault(c => c.Type == "userid").Value);
                //int userId = 0;
                bool resetPaswrd = Fundoo.ResetPassword(resetPassword, userId);
                if (resetPaswrd)
                {
                    return Ok(new { Success = true, message = "Password Reset Successfull !" });
                }
                return NotFound(new { Sucess = false, message = "Failed to Reset Password. Given Email doesn't exist in database." });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message, StackTrace = ex.StackTrace });
            }

        }
    }
}