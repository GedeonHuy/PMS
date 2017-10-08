﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class UploadedFileResource
    {
        public int UploadedFileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public int EnrollmentId { get; set; }
        public EnrollmentResource Enrollment { get; set; }
    }
}