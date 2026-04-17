

namespace GymManagementSystemDAL.Entity
{
    public class Member : GymUser
    {
        // CreatedAt is the same of joining date 
        public string Photo { get; set; } = null!;

        public HealthRecord HealthRecord { get; set; } = null!;

        public ICollection<Membership> Memberships { get; set; } = null!;

        public ICollection<MemberSession> MemberSessions { get; set; } = null!;

    }
}
