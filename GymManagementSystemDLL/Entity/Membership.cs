using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Entity
{
    public class Membership : BaseEntity
    {
        public string status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
            
        }
        public DateTime EndDate { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public Plan Plan { get; set; } =null!;
        public int PlanId { get; set; } 

    }
}
