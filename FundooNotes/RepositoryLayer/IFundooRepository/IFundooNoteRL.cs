using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IFundooRepository
{
    public interface IFundooNoteRL
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
        public List<NotesModel> GetAll(int userId);

        /// <summary>
        /// Method Declaration to Trash or Restore a note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public void Trash(int NotesId);
        /// <summary>
        /// List all the Trash Notes from the table
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAllTrash();

        /// <summary>
        /// Method Declaration to Archive or Restore a note
        /// </summary>
        /// <param name="NoteId"></param>
        public void Archive(int NoteId);

    }
}
