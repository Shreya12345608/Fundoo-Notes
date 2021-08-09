using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.IFundooBussiness
{
    public interface IFundooNoteBL
    {
        /// <summary>
        /// method for Add Notes
        /// </summary>
        /// <param name="notes"></param>
        public bool AddNotes(AddNote notes, int userId);
        /// <summary>
        /// List all the Notes from the table
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAll(int userId, string email);

        /// <summary>
        /// List all the Trash Notes from the table
        /// </summary>
        /// <returns></returns>
        public List<NotesModel> GetAllTrash(int userId);

        /// <summary>
        /// Method Declaration to Trash or Restore a note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public void Trash(int NotesId);

        /// <summary>
        /// Method Declaration to Archive or Restore a note
        /// </summary>
        /// <param name="NoteId"></param>
        public void Archive(int NotesId);
       
        /// <summary>
        /// List all the Trash Notes from the table
        /// </summary>
        /// <returns></returns>
        public List<NotesModel> GetAllArchive(int userId);
        /// <summary>
        ///Model Delete Note 
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public bool DeleteNote(int notesId);
        /// <summary>
        /// Update Note 
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNotes(UpdateNote note, int NotesId, string email);

        /// <summary>
        /// Method declaration to pin and unpin to note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string PinOrUnpinNote(int NotesId);
    }
}

