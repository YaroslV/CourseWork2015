using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace course.Data
{
    public class Lecture
    {
        public string LectureId { get; set; }
        public string TutorId { get; set; }
        public string LectureText { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string FilePath { get; set; }
    }
}