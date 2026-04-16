using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public class Sessoin : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;


        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;


            public ICollection<MemberSession> MemberSessions { get; set; } = null!;

    }
}
