using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS.Resources.TaskResources;

namespace PMS.Resources
{
    public class UploadedFileResource
    {
        public int UploadedFileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public bool IsDeleted { get; set; }
        public int? GroupId { get; set; }
        public int? TaskId { get; set; }
        public GroupResource Group { get; set; }
        public TaskResource Task { get; set; }
        public UploadedFileResource()
        {
            IsDeleted = false;
        }
    }
}
