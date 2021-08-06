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
                    //getting detials of notes to Addnotes
                    Title = notes.Title,
                    Description = notes.Description,
                    Reminder = notes.Reminder,
                    IsArchive = notes.IsArchive,
                    IsTrash = notes.IsTrash,
                    IsPin = notes.IsPin,
                    Color = notes.Color,
                    Image = notes.Image
                };
                //craeting new object for userAcconutdetails
                UserAccountDetails user = new UserAccountDetails();
                //getting userid by using linq
                user = fundooContext.FondooNotes.FirstOrDefault(usr => usr.Userid == userId);
                note.UserAccount = user;
                if (notes.Title != null || notes.Description != null)
                {
                    //adding content in table
                    fundooContext.NotesDB.Add(note);
                    //saving data
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


        /// <summary>
        /// list all the notes From the table
        /// </summary>
        /// <returns></returns>
        public List<NotesModel> GetAll(int userId)
        {
            List<NotesModel> notes = fundooContext.NotesDB.ToList().FindAll(note => note.IsArchive == false && note.IsTrash == false);
            // AddNote addNote = new AddNote();
            //List<AddNote> addNotees = new List<AddNote>();
            //foreach (var addNotes in notes)
            //{
            //    addNote.Title = addNotes.Title;
            //    addNote.Description = addNotes.Description;
            //    addNote.Reminder = addNotes.Reminder;
            //    addNote.IsArchive = addNotes.IsArchive;
            //    addNote.IsTrash = addNotes.IsTrash;
            //    addNote.IsPin = addNotes.IsPin;
            //    addNote.Color = addNotes.Color;
            //    addNote.Image = addNotes.Image;
            //    addNotees.Add(addNote);
            //}
            //return addNotees;
            if (notes.Count != 0)
            {
                return notes;
            }
            return null;
        }
        /// <summary>
        /// Get all trash
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAllTrash()
        {
            List<NotesModel> notes = fundooContext.NotesDB.ToList().FindAll(note => note.IsTrash == true);
            AddNote addNote = new AddNote();
            List<AddNote> addNotees = new List<AddNote>();
            foreach (var addNotes in notes)
            {
                addNote.Title = addNotes.Title;
                addNote.Description = addNotes.Description;
                addNote.Reminder = addNotes.Reminder;
                addNote.IsArchive = addNotes.IsArchive;
                addNote.IsTrash = addNotes.IsTrash;
                addNote.IsPin = addNotes.IsPin;
                addNote.Color = addNotes.Color;
                addNote.Image = addNotes.Image;
                addNotees.Add(addNote);
            }
            return addNotees;
        }

        /// <summary>
        ///  Method to Trash 
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public void Trash(int NotesId)
        {
            try
            {
                var result = fundooContext.NotesDB.FirstOrDefault(trash => trash.NotesId == NotesId);
                if (result != null)
                {
                    if (result.IsTrash == false)
                    {
                        result.IsTrash = true;
                        this.fundooContext.SaveChanges();
                    }
                    else
                    {
                        result.IsTrash = false;
                        this.fundooContext.SaveChanges();
                    }
                }
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Method for Archive
        /// </summary>
        /// <param name="NoteId"></param>
        public void Archive(int NoteId)
        {
            try
            {
                var resultArcive = fundooContext.NotesDB.FirstOrDefault(archive => archive.NotesId == NoteId);
                if (resultArcive != null)
                {
                    if (resultArcive.IsArchive == false)
                    {
                        resultArcive.IsArchive = true;
                        this.fundooContext.SaveChanges();
                    }
                    else
                    {
                        resultArcive.IsArchive = false;
                        this.fundooContext.SaveChanges();
                    }
                }
            }
            catch
            {

                throw;
            }
        }

    }
}
