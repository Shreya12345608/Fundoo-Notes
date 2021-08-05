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
        public List<AddNote> GetAll();

        

    }
}
