using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public class MemberSession : BaseEntity
    {
        public int MemberId { get; set; }   
        public Member Member { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public Sessoin Session { get; set; } = null!;
        public int SessionId { get; set; }

    }
}
