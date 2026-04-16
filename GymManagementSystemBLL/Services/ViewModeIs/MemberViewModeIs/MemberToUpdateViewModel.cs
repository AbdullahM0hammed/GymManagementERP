using GymManagementSystemDAL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null! ;
        public string ? Photo { get; set; }
        [Required(ErrorMessage = "Email is Reqierd")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be less than 100 characters")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone is Reqierd")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be an Egyptian number")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Address is Reqierd")]
        [Range(1, 9000, ErrorMessage = "building number must be from 1 to 9000")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2,
            ErrorMessage = "Street Must Be Between 2 and 30 Chars")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Chars")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;
    }
}
