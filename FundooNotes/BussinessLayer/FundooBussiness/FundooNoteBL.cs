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
        public List<AddNote> GetAll(int userId)
        {
            try
            {
                return this.fundooNoteRL.GetAll(userId);
            }
            catch 
            {

                throw;
            }
        }
       
        /// <summary>
        ///  Method to Trash 
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public void Trash(int NotesId)
        {
            try
            {
                this.fundooNoteRL.Trash(NotesId);
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Method for get all trash 
        /// </summary>
        /// <returns></returns>
        public List<NotesModel> GetAllTrash(int userId)
        {
            try
            {
                return this.fundooNoteRL.GetAllTrash(userId);
            }
            catch
            {

                throw;
            }
        }



        /// <summary>
        /// Method for Archive
        /// </summary>
        /// <param name="NotesId"></param>
        public void Archive(int NotesId)
        {
            try
            {
                this.fundooNoteRL.Archive(NotesId);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Method Delete a Note 
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public bool DeleteNote(int NotesId)
        {
            try
            {
                return this.fundooNoteRL.DeleteNote(NotesId);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        ///  get all archive
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<NotesModel> GetAllArchive(int userId)
        {
            try
            {
                return this.fundooNoteRL.GetAllArchive(userId);
            }
            catch
            {

                throw;
            }
        }
    }

}
