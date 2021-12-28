using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.DTOs
{
    public class UserDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public Nullable<int> UserType { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int Status { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> GymId { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
        public Nullable<int> TenantId { get; set; }
    }
}