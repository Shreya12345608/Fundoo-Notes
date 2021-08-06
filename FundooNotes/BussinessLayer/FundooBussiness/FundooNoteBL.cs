using BussinessLayer.IFundooBussiness;
using CommanLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.IFundooRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.FundooBussiness
{
    public class FundooNoteBL : IFundooNoteBL
    {
        /// <summary>
        /// instance variable for FundooNoteRL
        /// </summary>
        IFundooNoteRL fundooNoteRL;
        /// <summary>
        /// constructor for FundooNoteBL
        /// </summary>
        /// <param name="fundooNoteRL"></param>
        /// <param name="configuration"></param>
        public FundooNoteBL(IFundooNoteRL fundooNoteRL, IConfiguration configuration)
        {
            this.fundooNoteRL = fundooNoteRL;
        }
        /// <summary>
        /// Method for Add Note
        /// </summary>
        /// <param name="notes"></param>

        public bool AddNotes(AddNote notes, int userId)
        {
            try
            {
                return fundooNoteRL.AddNotes(notes, userId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method List all the Notes From the DB
        /// </summary>
        /// <returns></returns>
        public List<NotesModel> GetAll(int userId)
        {
            return this.fundooNoteRL.GetAll(userId);
        }
       
        /// <summary>
        ///  Method to Trash 
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public void Trash(int NotesId)
        {
            this.fundooNoteRL.Trash(NotesId);
        }

        /// <summary>
        /// Method for get all trash 
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAllTrash()
        {
            return this.fundooNoteRL.GetAllTrash();
        }



        /// <summary>
        /// Method for Archive
        /// </summary>
        /// <param name="NoteId"></param>
        public void Archive(int NoteId)
        {
            this.fundooNoteRL.Archive(NoteId);
        }
    }

}
