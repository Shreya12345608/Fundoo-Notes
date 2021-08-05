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
        IFundooNoteRL fundooNoteRL;
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
        public List<AddNote> GetAll()
        {
            return this.fundooNoteRL.GetAll();
        }
        /// <summary>
        ///  Method to Trash 
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public void Trash(int NotesId, bool IsTrash)
        {
             this.fundooNoteRL.Trash(NotesId, IsTrash);
        }
    }

}
