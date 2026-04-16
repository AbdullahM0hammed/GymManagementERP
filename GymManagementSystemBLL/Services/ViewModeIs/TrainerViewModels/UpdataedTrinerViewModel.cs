using GymManagementSystemDAL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementSystemBLL.Services.ViewModeIs.TrainerViewModels
{
    public class UpdataedTrinerViewModel
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be less than 100 characters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$",
            ErrorMessage = "Phone number must be Egyptian number (11 digits)")]
        public string Phone { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateOnly DateOFBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [Range(1, 9000, ErrorMessage = "Building number must be from 1 to 9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2,
            ErrorMessage = "Street Must Be Between 2 and 30 Chars")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Chars")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Specialization Is Required")]
        public Specialties Specialization { get; set; }
    }
}