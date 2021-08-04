using BussinessLayer.IFundooBussiness;
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
        [HttpPost]
        [Route("addNote")]
        public ActionResult AddNotes(NotesModel notes)
        {
            try
            {
                fundooNoteBL.AddNotes(notes);
                return Created(notes.NotesId.ToString(), notes);
            }
            catch (Exception )
            {

                return NotFound(new { sucess = false, message = "Error While Craeting Note" });
                
            }

        }
    }
}
