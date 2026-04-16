using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Classes;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace GymManagementSystemBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberService(IUnitOfWork unitOfWork , IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool CreateMember(CreateMemberViewModel CreatedMember)
        {
            try
            { 
                if (ISEmailExsist(CreatedMember.Email) || ISPhoneExsist(CreatedMember.Phone))
                {
                    return false;
                }
                var member = _mapper.Map<CreateMemberViewModel, Member>(CreatedMember);
                 _unitOfWork.GetRepository<Member>().Add(member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating a member: {ex.Message}");
                return false;
            }
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().Getall();
            if (Members is null || !Members.Any())
                return [];
                var MemberViewModeIs = Members.Select(X => new MemberViewModel
            {
                id = X.Id,
                Name = X.Name,
                Email = X.Email,
                Phone = X.phone,
                Photo = X.Photo,
                Gender = X.Gender.ToString()
            });
            return MemberViewModeIs;
        }
        public MemberViewModel? GetMemberDesk(int MemberID)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetByID(MemberID);
            if (Member is null)
            {
                return null;
            }
            var MemberViewModeIs = new MemberViewModel
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.phone,
                Gender = Member.Gender.ToString(),
                DateOfBirth = Member.DateOfBirth.ToShortDateString(),
                Address = $"{Member.Address.BuildingNumber} {Member.Address.streeet} {Member.Address.City}",
                Photo = Member.Photo,
            };
            var ActiveMembership = _unitOfWork.GetRepository<Membership>().Getall(X => X.MemberId == MemberID && X.status == "Active").FirstOrDefault();
            if (ActiveMembership is not null)
            {
                MemberViewModeIs.PlanName = ActiveMembership.Plan.Name;
                MemberViewModeIs.MembershipStartDate = ActiveMembership.CreatedAT.ToShortDateString();
                MemberViewModeIs.MembershipEndDate = ActiveMembership.EndDate.ToShortDateString();
            }
            return MemberViewModeIs;
        }
        public HealtRecordViewModel? GetMemberHealt(int MemberID)
        {
            var HealtRecord = _unitOfWork.GetRepository<HealthRecord>().GetByID(MemberID);
            if (HealtRecord is null)
                return null;
            var HealtRecordViewModel = new HealtRecordViewModel
            {
                hight = HealtRecord.hight,
                Weight = HealtRecord.Wight,
                BloodType = HealtRecord.BloodType,
                Note = HealtRecord.Note
            };
            return HealtRecordViewModel;
        }
        public MemberToUpdateViewModel? GetMemberToUpdateViewModel(int MemberID)
        {

            var Member = _unitOfWork.GetRepository<Member>().GetByID(MemberID);
            if (Member is null)
                return null;

            var MemberToUpdateViewModel = new MemberToUpdateViewModel
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.phone,
                BuildingNumber = Member.Address.BuildingNumber,
                Street = Member.Address.streeet,
                City = Member.Address.City,
                Photo = Member.Photo
            };
            return MemberToUpdateViewModel;
        }
        //public bool RemoveMember(int MemberID)
        //{
        //    var MemeberRepo = _unitOfWork.GetRepository<Member>();
        //    var MemberSessioonRepo = _unitOfWork.GetRepository<MemberSession>();
        //    var Member = MemeberRepo.GetByID(MemberID);
        //    if (Member is null)
        //        return false;

        //    var SessionIds = _unitOfWork.GetRepository<MemberSession>().Getall(
        //         b => b.MemberId == MemberID).Select(selector: x => x.SessionId); // 1 2 9

        //    var HasFutureSessions = _unitOfWork.GetRepository<Sessoin>().Getall(
        //         X => SessionIds.Contains(value: X.Id) && X.StartDate > DateTime.Now).Any();

        //    if (HasFutureSessions) return false;
        //var Membership = MemberSessioonRepo.Getall(X => X.MemberId == MemberID);
        //    try
        //        {
        //        foreach (var item in Membership)
        //        {
        //            MemberSessioonRepo.Delete(item);
        //        }
        //         MemeberRepo.Delete(Member) ;
        //        return _unitOfWork.SaveChanges() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred while removing the member: {ex.Message}");
        //        return false;
        //    }
        //}
        public bool RemoveMember(int MemberId)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var Member = memberRepo.GetByID(MemberId);
            if (Member is null)
                return false;

            var SessionIds = _unitOfWork.GetRepository<MemberSession>().Getall(
                b => b.MemberId == MemberId).Select(x => x.SessionId);

            var HasFutureSessions = _unitOfWork.GetRepository<Sessoin>().Getall(
                X => SessionIds.Contains(X.Id) && X.StartDate > DateTime.Now).Any();

            if (HasFutureSessions) return false;

            var memberShipRepo = _unitOfWork.GetRepository<Membership>();
            var MemberShips = memberShipRepo.Getall(X => X.MemberId == MemberId);

            try
            {
                foreach (var item in MemberShips)
                    memberShipRepo.Delete(item);

                memberRepo.Delete(Member);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public bool UpdateMember(int ID, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                var emailExists =_unitOfWork.GetRepository<Member>()
                    .Getall(X => X.Email == UpdatedMember.Email && X.Id != ID);
                var phoneExists = _unitOfWork.GetRepository<Member>()
                    .Getall(X => X.phone == UpdatedMember.Phone && X.Id != ID);

                //if (ISEmailExsist(UpdatedMember.Email) || ISPhoneExsist(UpdatedMember.Phone))
                    //return false;
                var Member = _unitOfWork.GetRepository<Member>().GetByID(ID);
                if (Member is null) return false;
                Member.Email = UpdatedMember.Email;
                Member.phone = UpdatedMember.Phone;
                Member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                Member.Address.streeet = UpdatedMember.Street;
                Member.Address.City = UpdatedMember.City;
                Member.UpdatedAT = DateTime.Now;
                 _unitOfWork.GetRepository<Member>().Update(Member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the member: {ex.Message}");
                return false;
            }
        }

        #region HelperMethods
        private bool ISEmailExsist (string Email)
        {
        return _unitOfWork.GetRepository<Member>().Getall(X => X.Email == Email).Any();
        }
        private bool ISPhoneExsist (string Phone)
        {
            return _unitOfWork.GetRepository<Member>().Getall(X => X.phone == Phone).Any();
        }
        #endregion
    }
}
