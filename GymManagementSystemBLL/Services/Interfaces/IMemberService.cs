using GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember (CreateMemberViewModel CreatedMember);   
        MemberViewModel? GetMemberDesk (int MemberID);
        HealtRecordViewModel? GetMemberHealt (int MemberID);
        MemberToUpdateViewModel? GetMemberToUpdateViewModel (int MemberID);
        bool UpdateMember (int ID , MemberToUpdateViewModel UpdatedMember);
        bool RemoveMember (int MemberID);
    }
}
