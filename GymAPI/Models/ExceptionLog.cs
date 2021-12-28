using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.Models
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatedBy { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}