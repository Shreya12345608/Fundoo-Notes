using BussinessLayer.IFundooBussiness;
using CommanLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// instance variable of IFundooNoteBL
        /// </summary>
        IFundooNoteBL fundooNoteBL;
        /// <summary>
        /// Constructor of NoteController
        /// </summary>
        /// <param name="fundooNoteBL"></param>
        public NotesController(IFundooNoteBL fundooNoteBL)
        {
            this.fundooNoteBL = fundooNoteBL;

        }
        /// <summary>
        ///  Controller for Add Note
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNotes(AddNote notes)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteAdded = fundooNoteBL.AddNotes(notes, userId);
                if (noteAdded == true)
                {
                    return Ok(new { Success = true, message = "New Note added Successfully!", data = notes });
                }
                return BadRequest(new { Success = false, message = "Failed to Add New Note to Database" });
            }
            catch (Exception ex)
            {

                return NotFound(new { sucess = false, message = ex.Message });

            }


        }
        /// <summary>
        /// Get userid from Token
        /// </summary>
        /// <returns></returns>
        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userid").Value);
        }

        /// <summary>
        /// Get Email from Token
        /// </summary>
        /// <returns></returns>
        private string GetIdFromTokenEmail()
        {
            return User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            //getting user details from token
            //ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            //string userEmail = principal.Claims.FirstOrDefault(user => user.Type == ClaimTypes.Email).Value;
            //return userEmail;
        }

     

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                int userId = GetIdFromToken();
                string email = GetIdFromTokenEmail();
                var fundoos = fundooNoteBL.GetAll(userId,email);
                if (fundoos != null)
                {
                    return this.Ok(new { Success = true, Message = "Get Note SuccessFull", Data = fundoos });
                }
                return this.BadRequest(new { Success = false, Message = "NO Notes To  be Display" });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="noteTrash"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{NotesId}/Trash")]
        public ActionResult Trash(int NotesId)
        {
            try
            {
                this.fundooNoteBL.Trash(NotesId);

                return Ok(new { success = true, message = $"Trash is retrieved Successfully" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"Unable to  get any Trash note." });
            }
        }
        /// <summary>
        /// Get all trash Note
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("trash")]
        public ActionResult GetAllTrash()
        {
            try
            {
                int userId = GetIdFromToken();
                var trash = fundooNoteBL.GetAllTrash(userId);
                if (trash != null)
                {

                    return this.Ok(new { Success = true, Message = "Trash Notes retrieved Successfully", Data = trash });
                }
                return this.BadRequest(new { Success = false, Message = "Unable to retrieve Trash notes." });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        /// <summary>
        /// set amd restore the archive
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{NotesId}/Archive")]
        public ActionResult Archive(int NotesId)
        {
            try
            {

                this.fundooNoteBL.Archive(NotesId);
                return Ok(new { success = true, message = $"Archive is retrieved Successfully" });
            }
            catch (Exception)
            {

                return BadRequest(new { success = false, message = $"Unable to get Archive note." });
            };
        }
        /// <summary>
        /// Get all Archive Note
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("archive")]
        public ActionResult GetAllArchive()
        {
            try
            {
                int userId = GetIdFromToken();
                var archive = fundooNoteBL.GetAllArchive(userId);
                if (archive != null)
                {
                    return this.Ok(new { Success = true, Message = "Archive Notes retrieved Successfully", Data = archive });
                }
                return this.BadRequest(new { Success = false, Message = "Unable to retrieve Archive notes." });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpDelete]
        public ActionResult DeleteNote(int notesId)
        {
            try
            {
                var delete = fundooNoteBL.DeleteNote(notesId);
                if (delete == true)
                {
                    return this.Ok(new { Success = true, Message = "Notes detete Successfully", Data = delete });
                }
                return this.BadRequest(new { Success = false, Message = "Unable to retrieve Delete notes." });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });

            }

        }

      
        [HttpPut]
        [Route("pin")]
        public IActionResult PinOrUnpinNote(int NoteId)
        {
            try
            {
                var pin = this.fundooNoteBL.PinOrUnpinNote(NoteId);
                if (pin != null)
                {
                    return this.Ok(new { Success = true, Message = "Note has been Pinned", Data = pin });
                }

                return this.BadRequest(new{ Status = false, Message = " No Note  Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new{ Status = false, Message = ex.Message });
            }
        }

    }
}
