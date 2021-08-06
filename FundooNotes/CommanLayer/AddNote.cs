using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CommanLayer
{
    public class AddNote
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }

        [DefaultValue(false)]
        public bool IsArchive { get; set; }
        public DateTime? CreatedDate { get; set; }
        [DefaultValue(false)]
        public bool IsTrash { get; set; }
        [DefaultValue(false)]
        public bool IsPin { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
    }
}
