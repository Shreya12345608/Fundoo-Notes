using BussinessLayer.IFundooBussiness;
using CommanLayer;
using RepositoryLayer.IFundooRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.FundooBussiness
{
    class FundooNoteBL : IFundooNoteBL

    {
        IFundooNoteRL fundooNoteRL;
        public FundooNoteBL(IFundooNoteRL fundooNoteRL)
        {
            this.fundooNoteRL = fundooNoteRL;
        }
        /// <summary>
        /// Add Note
        /// </summary>
        /// <param name="notes"></param>
        public NotesModel AddNotes(NotesModel notes)
        {
            try
            {
                fundooNoteRL.AddNotes(notes);
                return notes;
            }
            catch
            {
                throw;
            }

        }
    }

}
