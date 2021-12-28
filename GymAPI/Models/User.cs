using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymAPI.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
        public Nullable<bool> ChangePassowrdOnLogin { get; set; }
        public Nullable<DateTime> PasswordExpiryDate { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        [Required]
        public Nullable<DateTime> CreatedDate { get; set; }
        [Required]
        public int Status { get; set; }
        public Nullable<int> LastChangedBy { get; set; }
        public Nullable<DateTime> LastChangedDate { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> GymId { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string VerificationCode { get; set; }
        [StringLength(int.MaxValue)]
        public string Token { get; set; }
        public Nullable<DateTime> VerificationStartDate { get; set; }
        public Nullable<bool> IsMobileVerified { get; set; }
        public string UserNameFilter { get; set; }
        public Nullable<int> TenantId { get; set; }
        public int LoginFailedAttempts { get; set; }
        public Nullable<DateTime> LastLoginFailedDate { get; set; }
    }
}