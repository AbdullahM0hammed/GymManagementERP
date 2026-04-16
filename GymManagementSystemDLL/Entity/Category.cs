using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Sessoin> Sessoins { get; set; } = null!;

    }
}
