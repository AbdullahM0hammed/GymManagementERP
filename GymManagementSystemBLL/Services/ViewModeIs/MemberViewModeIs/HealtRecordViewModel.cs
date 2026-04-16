using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs
{
    public class HealtRecordViewModel
    {
        [Range(0.1, 300, ErrorMessage = "Weight must be between 1 and 300 kg")]
        [Required(ErrorMessage = "Weight is required")]
        public decimal hight { get; set; }
        [Range(0.1, 300, ErrorMessage = "Weight must be between 1 and 300 kg")]
        [Required(ErrorMessage = "Weight is required")]
        public decimal Weight { get; set; }
        [Required (ErrorMessage ="Blood Type Requierd")]
        public string BloodType { get; set; } = null!;
        public String? Note { get; set; }   
    }
}
