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
       public NotesModel AddNotes(NotesModel notes);

    }
}
