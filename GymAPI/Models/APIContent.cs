using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.Models
{
    public class APIContent<GeneralType>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Response<GeneralType> response
        {
            get { return kresponse; }
            set { kresponse = value; }
        }
        private Response<GeneralType> kresponse = new Response<GeneralType>();
    }
    public class Response<GeneralType>
    {
        public GeneralType ktoken { get; set; }
    }
}