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
        /// Add Note
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
    }

}
