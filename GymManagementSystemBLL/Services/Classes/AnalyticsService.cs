using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AnalyticsViewModels;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Interfaces;

namespace GymManagementSystemBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnalyticsService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsViewModel()
        {
            var sessions = _unitOfWork.GetRepository<Sessoin>().Getall();
            return new AnalyticsViewModel
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().Getall(m => m.status=="Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().Getall().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().Getall().Count(),
                UpcomingSessions = sessions.Where(s => s.StartDate > DateTime.Now).Count(),
                OngoingSessions = sessions.Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now).Count(),
                CompletedSessions = sessions.Where(s => s.EndDate < DateTime.Now).Count()
            };
        }
    }
}
