using GymManagementSystemDAL.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public class Trainer : GymUser
    {
        public Specialties Specialties { get; set; }

        // hiring date is the same of CreatedAt


        public ICollection<Sessoin> Sessoins { get; set; } = null!;
    }
}
