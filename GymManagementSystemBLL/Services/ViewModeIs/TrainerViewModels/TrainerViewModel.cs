using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemBLL.Services.ViewModeIs.TrainerViewModels
{
    public class TrainerViewModel
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Photo { get; set; }
        public string Gender { get; set; } = null!;
        public string? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Specialization { get; set; } = null!;


    }
}
