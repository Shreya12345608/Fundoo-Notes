﻿using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.IFundooBussiness
{
    public interface IFundooNoteBL
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
        public List<NotesModel> GetAll();
    

    }
}
