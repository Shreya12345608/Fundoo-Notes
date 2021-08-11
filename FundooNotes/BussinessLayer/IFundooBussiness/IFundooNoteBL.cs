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
        public List<AddNote> GetAllTrash(string email);

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
        public List<AddNote> GetAllArchive(string email);
        /// <summary>
        ///Model Delete Note 
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public bool DeleteNote(int notesId, string email);
        /// <summary>
        /// Update Note 
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNotes(UpdateNote note, int NotesId, string email);

        /// <summary>
        ///  Method declaration to pin and unpin to note
        /// </summary>
        /// <param name="NotesId">Note id</param>
        /// <param name="email">user email</param>
        /// <returns></returns>
        public string PinOrUnpinNote(int NotesId, string email);

        /// <summary>
        /// Adds the colour.
        /// </summary>
        /// <param name="NotesId">note id.</param>
        /// <param name="color">The color.</param>
        /// <returns>return true or false</returns>
        public string AddColour(int NotesId, string color, string email);

        /// <summary>
        /// create a label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="LabelName "></param>
        /// <returns></returns>
        LabelResponse CreateLabel(int userID, string LabelName);
        /// <summary>
        /// Delete label
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns></returns>
        public bool DeleteLabel(int LabelId);

    }
}

