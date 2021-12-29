using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.Models
{
    public class APIContent
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Response response
        {
            get { return kresponse; }
            set { kresponse = value; }
        }
        private Response kresponse = new Response();
    }
    public class Response
    {
        public string access_token { get; set; }
        public Nullable<int> error { get; set; }
        public string error_description { get; set; }
        public string error_uri { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string session_state { get; set; }
        public string token_type { get; set; }
    }
}