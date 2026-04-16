using GymManagementSystemDAL.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public abstract class GymUser: BaseEntity
    {
        public string Name { get; set; } =null!;
        public string Email { get; set; } = null!;
        public string phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;

    }
    [Owned]
  public  class Address
    {
        public int BuildingNumber { get; set; } 
        public string City { get; set; } = null!;
        public string streeet { get; set; } =null!;
    }
}
