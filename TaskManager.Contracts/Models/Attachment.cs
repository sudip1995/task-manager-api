using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Contracts.Models
{
    public class Attachment
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}
