﻿using BussinessLayer.IFundooBussiness;
using CommanLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("addNote")]
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

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {

                var fundoos = fundooNoteBL.GetAll();
                return this.Ok(new { Success = true, Message = "Get Note SuccessFull", Data = fundoos });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false,Message = ex.Message, StackTrace = ex.StackTrace});
            }
        }
    }
}