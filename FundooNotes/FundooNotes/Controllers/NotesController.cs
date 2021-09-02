using BussinessLayer.IFundooBussiness;
using CommanLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly FundooContext context;
        private readonly IDistributedCache distributedCache;
        private readonly string cacheKey = "fundoosList";

        /// <summary>
        /// Constructor of NoteController
        /// </summary>
        /// <param name="fundooNoteBL"></param>
        public NotesController(IFundooNoteBL fundooNoteBL, FundooContext context, IDistributedCache distributedCache)
        {
            this.fundooNoteBL = fundooNoteBL;
            this.context = context;
            this.distributedCache = distributedCache;

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
                var fundoos = fundooNoteBL.GetAll(userId, email);
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

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNoteUsingRedisCache()
        {
            try
            {
                //var cacheKey = "fundoosList";
                string serializedFundooList;
                var fundoosList = new List<AddNote>();
                var redisFundooList = await distributedCache.GetAsync(cacheKey);
                if (redisFundooList != null)
                {
                    serializedFundooList = Encoding.UTF8.GetString(redisFundooList);
                    fundoosList = JsonConvert.DeserializeObject<List<AddNote>>(serializedFundooList);
                }
                else
                {
                    int userId = GetIdFromToken();
                    string email = GetIdFromTokenEmail();
                    fundoosList = fundooNoteBL.GetAll(userId, email);
                    // customerList = await context.NotesDB.ToListAsync();
                    serializedFundooList = JsonConvert.SerializeObject(fundoosList);
                    redisFundooList = Encoding.UTF8.GetBytes(serializedFundooList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisFundooList, options);
                }
                return Ok(fundoosList);
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

                string email = GetIdFromTokenEmail();
                var trash = fundooNoteBL.GetAllTrash(email);

                if (trash != null)
                {
                  //  distributedCache.Remove(cacheKey);

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
                string email = GetIdFromTokenEmail();
                var archive = fundooNoteBL.GetAllArchive(email);
                if (archive != null)
                {
                   // distributedCache.Remove(cacheKey);
                    return this.Ok(new { Success = true, Message = "Archive Notes retrieved Successfully", Data = archive });
                }
                return this.BadRequest(new { Success = false, Message = "Unable to retrieve Archive notes." });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        /// <summary>
        /// delete Note
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteNote(int notesId)
        {
            try
            {
                string email = GetIdFromTokenEmail();
                var delete = fundooNoteBL.DeleteNote(notesId, email);
                if (delete == true)
                {
                    distributedCache.Remove(cacheKey);
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
        /// <summary>
        /// update note
        /// </summary>
        /// <param name="note"></param>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateNote")]
        public ActionResult UpdateNotes(UpdateNote note, int NotesId)
        {
            try
            {
                string email = GetIdFromTokenEmail();
                this.fundooNoteBL.UpdateNotes(note, NotesId, email);
               // distributedCache.Remove(cacheKey);
                return Ok(new { Success = true, Message = "Note Updated Successfully", data = note });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new
                {
                    Success = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }
        [HttpPut]
        [Route("pin")]
        public IActionResult PinOrUnpinNote(int NoteId)
        {
            try
            {
                string email = GetIdFromTokenEmail();
                var pin = this.fundooNoteBL.PinOrUnpinNote(NoteId, email);
                if (pin != null)
                {
                    return this.Ok(new { Success = true, Data = pin });
                }

                return this.BadRequest(new { Status = false, Message = " No Note  Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Adds the color.
        /// </summary>
        /// <param name="NoteId">note id</param>
        /// <param name="color">The color</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("addColor")]
        public IActionResult AddColor(int NoteId, string color)
        {
            try
            {
                string email = GetIdFromTokenEmail();
                var result = this.fundooNoteBL.AddColour(NoteId, color, email);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Add Colour Sucessfully", Data = color });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Controller method to Create lable
        /// </summary>
        /// <param name="lable">lable name</param>
        /// <returns>API response</returns>
        [HttpPost]
        [Route("CreateLabel")]
        public IActionResult CreateLabel(LabelModel label)
        {
            try
            {
                int userId = GetIdFromToken();
                LabelResponse result = fundooNoteBL.CreateLabel(userId, label);
                bool success = false;
                string message;
                if (result == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Label Created Successfully";
                    return Ok(new { success, message, result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        /// <summary>
        /// Method to delete lable
        /// </summary>
        /// <param name="LabelId">lable id</param>
        /// <returns>API response</returns>
        [HttpDelete]
        [Route("RemoveLable")]
        public IActionResult RemoveLable( int LabelId)
        {
            try
            {
                int userId = GetIdFromToken();
                var result = this.fundooNoteBL.DeleteLabel(userId, LabelId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Lable Deleted Successfully !", Data = LabelId });
                }

                return this.BadRequest(new { Status = false, Message = "Unable to delete this Lable." });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GelLabel")]
        public ActionResult GetAllLabel()
        {
            try
            {
                int userId = GetIdFromToken();
                var getalllabel = fundooNoteBL.GetAllLabel(userId);
                if (getalllabel != null)
                {
                 //   distributedCache.Remove(cacheKey);
                    return this.Ok(new { Success = true, Message = "Label retrieved Successfully", Data = getalllabel });
                }
                return this.BadRequest(new { Success = false, Message = "Unable to retrieve Label." });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        [HttpDelete]
        [Route("trash")]
        public ActionResult DeleteTrashNotes()
        {
            try
            {
                int userId = GetIdFromToken();
                var delete = fundooNoteBL.DeleteTrashNotes(userId);
                if (delete == true)
                {
                   // distributedCache.Remove(cacheKey);
                    return this.Ok(new { Success = true, Message = "All Notes detete Successfully", Data = delete });
                }
                return this.BadRequest(new { Success = false, Message = "Unable to Delete  notes." });
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
        [HttpPost]
        [Route("CreateCollaboration")]
        public IActionResult CreateCollaboration(int userID, CollaborationModel collab)
        {
            try
            {
                int userId = GetIdFromToken();
                CollaborationModel result = fundooNoteBL.CreateCollaboration(userId, collab);
                bool success = false;
                string message;
                if (result == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Collaboration Created Successfully";
                    return Ok(new { success, message, result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}