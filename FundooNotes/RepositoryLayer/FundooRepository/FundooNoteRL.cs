using CommanLayer;
using RepositoryLayer.IFundooRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.FundooRepository
{
    public class FundooNoteRL : IFundooNoteRL
    {
        /// <summary>
        /// instance variable
        /// </summary>
        private FundooContext fundooContext;
        public FundooNoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        /// <summary>
        /// Add Note in the database with using of user id of Fundoo Note table
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddNotes(AddNote notes, int userId)
        {

            try
            {
                NotesModel note = new NotesModel()
                {
                    Title = notes.Title,
                    Description = notes.Description,
                    Reminder = notes.Reminder,
                    IsArchive = notes.IsArchive,
                    IsTrash = notes.IsTrash,
                    IsPin = notes.IsPin,
                    Color = notes.Color,
                    Image = notes.Image
                };
                UserAccountDetails user = new UserAccountDetails();
                user = fundooContext.FondooNotes.FirstOrDefault(usr=>usr.Userid == userId);
                note.UserAccount = user;
                if(notes.Title != null || notes.Description != null)
                {
                    fundooContext.NotesDB.Add(note);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {

                throw;
            }
        }

        
    }
}
