using CommanLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options)
        {

        }
        //table name
        public DbSet<UserAccountDetails> FondooNotes { get; set; }
        
    }
}
