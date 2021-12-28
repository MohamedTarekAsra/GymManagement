using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GymAPI.Models
{
    public class Tenant
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
        public Nullable<bool> ChangePassowrdOnLogin { get; set; }
        public Nullable<DateTime> PasswordExpiryDate { get; set; }

        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        public Nullable<int> CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }
        public Nullable<int> LastChangedBy { get; set; }
        public Nullable<DateTime> LastChangedDate { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<int> Gender { get; set; }
        public int GymId { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string VerificationCode { get; set; }
        [StringLength(int.MaxValue)]
        public string Token { get; set; }
        public Nullable<DateTime> VerificationStartDate { get; set; }
        public Nullable<bool> IsMobileVerified { get; set; }
        public string UserNameFilter { get; set; }
        public Nullable<DateTime> SubscribeStartDate { get; set; }
        public Nullable<int> SubscribeEndDate { get; set; }
    }
}