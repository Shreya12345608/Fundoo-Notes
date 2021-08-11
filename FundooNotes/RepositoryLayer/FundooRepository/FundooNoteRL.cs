using CommanLayer;
using Microsoft.EntityFrameworkCore;
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
                    //Reminder = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")),
                    CreatedDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")),
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
                note.Email = user.UserEmail;
                if (notes.Title != null || notes.Description != null)
                {
                    //adding content in table
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


        /// <summary>
        /// list all the notes From the table
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAll(int userId, string email)
        {
            try
            {
                List<NotesModel> notes = fundooContext.NotesDB.Include(user => user.UserAccount).ToList().FindAll(note => note.IsArchive == false && note.Email == email && note.IsTrash == false);
                List<AddNote> list = new List<AddNote>();
                foreach (var addNotes in notes)
                {
                    AddNote addNote = new AddNote();

                    addNote.NotesId = addNotes.NotesId;
                    addNote.Title = addNotes.Title;
                    addNote.Description = addNotes.Description;
                    //  addNote.Reminder = addNotes.Reminder;
                    addNote.IsArchive = addNotes.IsArchive;
                    addNote.IsTrash = addNotes.IsTrash;
                    addNote.IsPin = addNotes.IsPin;
                    addNote.Color = addNotes.Color;
                    addNote.Image = addNotes.Image;
                    addNote.userId = addNotes.UserAccount.Userid;
                    list.Add(addNote);
                }
                //getting all pinned lists.
                var pinnedList = list.FindAll(notes => notes.IsPin == true);
                List<AddNote> finalList = new List<AddNote>();
                finalList.AddRange(pinnedList);
                finalList = finalList.Concat(list.Where(notes => notes.IsPin == false)).ToList();
                return finalList;
                if (finalList.Count != 0)
                {
                    return finalList;
                }
                return null;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Get all trash
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAllTrash(string email)
        {
            try
            {
                List<NotesModel> trash = fundooContext.NotesDB.Include(user => user.UserAccount).ToList().FindAll(x => x.IsTrash == true && x.Email == email);
                List<AddNote> list = new List<AddNote>();
                foreach (var addNotes in trash)
                {
                    AddNote addNote = new AddNote();

                    addNote.NotesId = addNotes.NotesId;
                    addNote.Title = addNotes.Title;
                    addNote.Description = addNotes.Description;
                    //addNote.Reminder = addNotes.Reminder;
                    addNote.IsArchive = addNotes.IsArchive;
                    addNote.IsTrash = addNotes.IsTrash;
                    addNote.IsPin = addNotes.IsPin;
                    addNote.Color = addNotes.Color;
                    addNote.Image = addNotes.Image;
                    addNote.userId = addNotes.UserAccount.Userid;
                    list.Add(addNote);
                }
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
            catch
            {

                throw;
            }
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
        public void Archive(int NotesId)
        {
            try
            {
                var resultArcive = fundooContext.NotesDB.FirstOrDefault(archive => archive.NotesId == NotesId);
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
        /// <summary>
        /// Get all trash
        /// </summary>
        /// <returns></returns>
        public List<AddNote> GetAllArchive(string email)
        {
            try
            {
                var Archive = fundooContext.NotesDB.Include(user => user.UserAccount).ToList().FindAll(x => x.IsArchive == true && x.Email == email && x.IsTrash == false);
                List<AddNote> list = new List<AddNote>();
                foreach (var addNotes in Archive)
                {
                    AddNote addNote = new AddNote();

                    addNote.NotesId = addNotes.NotesId;
                    addNote.Title = addNotes.Title;
                    addNote.Description = addNotes.Description;
                    //addNote.Reminder = addNotes.Reminder;
                    addNote.IsArchive = addNotes.IsArchive;
                    addNote.IsTrash = addNotes.IsTrash;
                    addNote.IsPin = addNotes.IsPin;
                    addNote.Color = addNotes.Color;
                    addNote.Image = addNotes.Image;
                    addNote.userId = addNotes.UserAccount.Userid;
                    list.Add(addNote);
                }
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Delete Note
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public bool DeleteNote(int notesId, string email)
        {
            try
            {
                var result = fundooContext.NotesDB.FirstOrDefault(delete => delete.NotesId == notesId && delete.Email == email && delete.IsTrash == true);
                if (result == null)
                {
                    throw new Exception("No Note Exist");
                }
                else
                {
                    fundooContext.NotesDB.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Update Note
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNotes(UpdateNote note, int NotesId, string email)
        {
            try
            {
                var result = fundooContext.NotesDB.FirstOrDefault(unote => unote.NotesId == NotesId && unote.Email == email && unote.IsTrash == false);
                if (result == null)
                {
                    throw new Exception("No such note exits");
                }
                else
                {
                    result.Title = note.Title;
                    result.ModifiedDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                    result.Description = note.Description;
                    fundooContext.SaveChanges();
                }
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Method to Pin Or Unpin the Note 
        /// </summary>
        /// <param name="NotesId">note id</param>
        /// <returns>string message</returns>
        public string PinOrUnpinNote(int NotesId, string email)
        {
            try
            {
                string message;
                var note = fundooContext.NotesDB.FirstOrDefault(pin => pin.NotesId == NotesId && pin.Email == email && pin.IsTrash == false && pin.IsArchive == false);
                if (note != null)
                {
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                        note.ModifiedDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                        this.fundooContext.SaveChanges();
                        message = "Note Pinned";
                        return message;
                    }
                    if (note.IsPin == true)
                    {
                        note.IsPin = false;
                        note.ModifiedDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                        this.fundooContext.SaveChanges();
                        message = "Note Unpinned";
                        return message;
                    }
                }

                return message = "Unable to pin or unpin note.";
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Adds the colour.
        /// </summary>
        /// <param name="noteId">note id.</param>
        /// <param name="color">The color.</param>
        /// <returns>return true or false</returns>
        /// <exception cref="Exception"></exception>
        public string AddColour(int NotesId, string color, string email)
        {
            try
            {
                string message;
                var notes = this.fundooContext.NotesDB.Where(color => color.NotesId == NotesId && color.Email == email && color.IsTrash == false).SingleOrDefault();
                if (notes != null)
                {
                    notes.Color = color;
                    fundooContext.Entry(notes).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    message = "Color Updated SuccessFully";
                    return message;
                }
                return message = "Unable to Change /Update Color.";
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create or add label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="LabelName"></param>
        /// <returns></returns>
        public LabelResponse CreateLabel(int userID, string LabelName)
        {
            try
            {
              
                LabelModel labelmodel = new LabelModel()
                {
                    Label = LabelName

                    
                };
                //craeting new object for userAcconutdetails
                UserAccountDetails user = new UserAccountDetails();
                //getting userid by using linq
                user = fundooContext.FondooNotes.FirstOrDefault(usr => usr.Userid == userID);
                labelmodel.User = user;
                fundooContext.Label.Add(labelmodel);
                fundooContext.SaveChanges();

                LabelResponse responseData = new LabelResponse()
                {
                    Label = labelmodel.Label
                    
                };
                return responseData;
            }
            catch
            {

                throw;
            }
        }
       

    }
}
