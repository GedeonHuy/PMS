using System;
using System.Collections.Generic;
using System.Linq;

namespace PMS.Models
{
    public class UploadedFile
    {
        public int UploadedFileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public bool IsDeleted { get; set; }
        public Group Group { get; set; }
        public TaskingModels.Task Task { get; set; }
        public UploadedFile()
        {
            IsDeleted = false;
        }
    }
}
