using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace course.Models
{

    public class RequestInfo
    {
        public string TutorName { get; set; }
        public string TutorId { get; set; }
    }

    public class TutorToActivate
    {
        public string TutorId { get; set; }
    }

}