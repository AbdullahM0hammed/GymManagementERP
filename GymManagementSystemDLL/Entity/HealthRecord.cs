using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public class HealthRecord :BaseEntity
    {
        public decimal hight { get; set; }
        public decimal Wight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
        // lastUpdate is the same of UpdatedAT 
    }
}
