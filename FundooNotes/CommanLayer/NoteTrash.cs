using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CommanLayer
{
    public class NoteTrash
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trash; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool IsTrash { get; set; }
    }
}
