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
        /// <summary>
        /// for unique constaint email
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAccountDetails>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();
            builder.Entity<NotesModel>()
               .HasIndex(u => u.Email)
               .IsUnique();
        }
        //table name
        public DbSet<UserAccountDetails> FondooNotes { get; set; }
        public DbSet<NotesModel> NotesDB { get; set; }

    }
}
