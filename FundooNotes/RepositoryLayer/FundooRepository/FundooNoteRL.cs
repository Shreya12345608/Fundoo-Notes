using CommanLayer;
using RepositoryLayer.IFundooRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.FundooRepository
{
    class FundooNoteRL : IFundooNoteRL
    {
        /// <summary>
        /// instance 
        /// </summary>
        private FundooContext fundooContext;
        public FundooNoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NotesModel AddNotes(NotesModel notes)
        {

            try
            {
                var noteUser = fundooContext.NotesDB.Add(notes);
                int row = fundooContext.SaveChanges();
                return row == 1 ? notes : null;
            }
            catch
            {

                throw;
            }
        }
    }
}
