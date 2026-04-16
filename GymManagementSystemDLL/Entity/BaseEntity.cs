using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public abstract class BaseEntity 
    {
        public int Id { get; set; }
        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
    }
}
