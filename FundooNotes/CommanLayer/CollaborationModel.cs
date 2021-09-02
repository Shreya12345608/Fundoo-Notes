using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommanLayer
{
   public class CollaborationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaborationId { get; set; }
        public string ReceiverMail { get; set; }
        public int UserId { get; set; }
        public int NotesId { get; set; }
    }
}
