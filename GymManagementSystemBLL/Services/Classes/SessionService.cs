using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.SessionViewModeIs;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Classes;
using GymManagementSystemDAL.Repositories.Interfaces;

namespace GymManagementSystemBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool CreateSession(CreateSessionViewModel CreatedSession)
        {
            try
            {
                if (!IsTrinerExsist(CreatedSession.TrainerId) || !IsCategoryExsist(CreatedSession.CategoryId) || !IsDateTimeValid(CreatedSession.StartDate, CreatedSession.EndDate) || CreatedSession.Capacity > 25 || CreatedSession.Capacity < 0)
                    return false;
                var sessionEntity = _mapper.Map<Sessoin>(CreatedSession);
                _unitOfWork.GetRepository<Sessoin>().Add(sessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
            Console.WriteLine($"Create Session Field : {ex}");
                return false;
            }
        }
        public IEnumerable<SessionViewModel> GetallSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrinerAndCategory();

            if (!sessions.Any())return [];

            //return sessions.Select(s => new SessionViewModeI
            //{
            //  Id = s.Id,
            //  Description = s.Description,
            //  StartDate = s.StartDate,
            //  EndDate = s.EndDate,
            //  Capacity = s.capacity,
            //  TrainerName = s.Trainer.Name,
            //  CategoryName = s.Category.Name,
            //  AvailableSlots = s.capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(s.Id)
            //}).ToList();
            var mappedSessions = _mapper.Map<IEnumerable<Sessoin>,IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            }
            return mappedSessions;
        }
        public SessionViewModel? GetSessionById(int sessionID)
        {
            var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionID);
            if (session is null) return null;
            //return new SessionViewModeI
            //{
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.capacity,
            //    TrainerName = session.Trainer.Name,
            //    CategoryName = session.Category.Name,
            //    AvailableSlots = session.capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id)
            //};
            var mappedSession = _mapper.Map<Sessoin, SessionViewModel>(session);
            mappedSession.AvailableSlots = mappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(mappedSession.Id);
            return mappedSession;
        }
        public UpdateSessionViewModel? GetSessionToUpdate(int sessionID)
        {
            var session = _unitOfWork.SessionRepository.GetByID(sessionID);
            if(!IsSessionAvailableForUpdating(session!)) return null;
            return _mapper.Map<Sessoin, UpdateSessionViewModel>(session!);

        }
        public bool UpdateSession(int sessionID, UpdateSessionViewModel UpdatedSession)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetByID(sessionID);
                if (!IsSessionAvailableForUpdating(session!)) return false;
                if (!IsTrinerExsist(UpdatedSession.TrainerId) || !IsDateTimeValid(UpdatedSession.StartDate, UpdatedSession.EndDate))
                    return false;
                _mapper.Map(UpdatedSession , session);
                session!.UpdatedAT= DateTime.Now;
                _unitOfWork.SessionRepository.Update(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Field : {ex}");
                return false;
            }
        }
        public bool DeleteSession(int sessionID)
        {
            try
            {
                var session=_unitOfWork.SessionRepository.GetByID(sessionID);
                if (!IsSessionAvailableForRemoving(session!)) return false;
                _unitOfWork.SessionRepository.Delete(session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine( $"Remove Field :  {ex}");
                return false;
            }
        }


        public IEnumerable<CategorySelectViewModel> GetAllCategoriesForSelection()
        {
            var categories = _unitOfWork.GetRepository<Category>().Getall();
            return categories.Select(c => new CategorySelectViewModel
            {
                CategoryID = c.Id,
                CategoryName = c.Name
            }).ToList();
        }

        public IEnumerable<TrainerSelectViewModel> GetAllTrainersForSelection()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().Getall();
            return trainers.Select(t => new TrainerSelectViewModel
            {
                TrainerID = t.Id,
                TrainerName = t.Name
            }).ToList();
        }

        #region Helper Methods
        private bool IsSessionAvailableForUpdating(Sessoin session)
        {
            if (session is null)return false;
            if (session.EndDate < DateTime.Now)return false;
            if (session.StartDate <= DateTime.Now)return false;
            var hasActiveBooking =_unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBooking)return false;
            return true;
        }
        private bool IsSessionAvailableForRemoving(Sessoin session)
        {
            if (session is null)return false;
            if (session.StartDate <= DateTime.Now && session.EndDate>DateTime.Now)return false;
            if (session.StartDate > DateTime.Now)return false;

            var hasActiveBooking =_unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBooking)return false;
            return true;
        }
        private bool IsTrinerExsist(int trinerID)
        {
        return _unitOfWork.GetRepository<Trainer>().GetByID(trinerID) is not null;
        }
        private bool IsCategoryExsist(int CategoryID)
        {
            return _unitOfWork.GetRepository<Category>().GetByID(CategoryID) is not null;
        }
        private bool IsDateTimeValid(DateTime StartDate , DateTime EndDate)
        {
            return StartDate < EndDate;
        }

        #endregion
    }
}
