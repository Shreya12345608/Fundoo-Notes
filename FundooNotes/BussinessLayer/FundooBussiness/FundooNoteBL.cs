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
        public List<AddNote> GetAll(int userId, string email)
        {
            try
            {
                return this.fundooNoteRL.GetAll(userId, email);
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
        public List<AddNote> GetAllTrash(string email)
        {
            try
            {
                return this.fundooNoteRL.GetAllTrash(email);
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
        public bool DeleteNote(int notesId, string email)
        {
            try
            {
                return this.fundooNoteRL.DeleteNote(notesId, email);
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
        public List<AddNote> GetAllArchive(string email)
        {
            try
            {
                return this.fundooNoteRL.GetAllArchive(email);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// update note
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNotes(UpdateNote note, int NotesId, string email)
        {
            try
            {
                this.fundooNoteRL.UpdateNotes(note, NotesId, email);

            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Method to Call PinOrUnpinNote() method to Pin Or Unpin a Note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string PinOrUnpinNote(int NotesId, string email)
        {
            try
            {
                var note = this.fundooNoteRL.PinOrUnpinNote(NotesId, email);
                return note;
            }
            catch
            {

                throw;
            }
        }
        public string AddColour(int NotesId, string color, string email)
        {
            try
            {
                var note = this.fundooNoteRL.AddColour(NotesId, color, email);
                return note;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Create Label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="LabelName"></param>
        /// <returns></returns>
        public LabelResponse CreateLabel(int userID, LabelModel LabelName)
        {
            try
            {
                LabelResponse responseData = fundooNoteRL.CreateLabel(userID, LabelName);
                return responseData;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        ///Method for Delete Label 
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns></returns>
        public bool DeleteLabel(int userID, int LabelId)
        {
            try
            {
                return this.fundooNoteRL.DeleteLabel(userID,LabelId);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// get all label
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<LabelModel> GetAllLabel(int userID)
        {
            try
            {
                return this.fundooNoteRL.GetAllLabel(userID);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Delete Trash Note
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteTrashNotes(int userId)
        {
            try
            {
                 return this.fundooNoteRL.DeleteTrashNotes(userId);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Create Collaboration
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="collab"></param>
        /// <returns></returns>
        public CollaborationModel CreateCollaboration(int userID, CollaborationModel collab)
        {
            try
            {
                return this.fundooNoteRL.CreateCollaboration(userID, collab);
            }
            catch
            {

                throw;
            }
        }
    }
}
