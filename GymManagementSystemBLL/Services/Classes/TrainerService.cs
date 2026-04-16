using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.TrainerViewModels;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Classes;
using GymManagementSystemDAL.Repositories.Interfaces;


namespace GymManagementSystemBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreatetrainerViewmodel trainer)
        {
            //Name , Email , Phone ,date of birth ,gender , address , specialization
            try
            {
                if (ISEmailExsist(trainer.Email) || ISPhoneExsist(trainer.Phone)) return false;
                var newTrainer = new Trainer
                {
                    Name = trainer.Name,
                    Email = trainer.Email,
                    phone = trainer.Phone,
                    DateOfBirth = trainer.DateOFBirth,
                    Gender = trainer.Gender,
                    Address = new Address
                    {
                        BuildingNumber = trainer.BuildingNumber,
                        streeet = trainer.Street,
                        City = trainer.City
                    },
                    Specialties = trainer.Specialization
                };
                _unitOfWork.GetRepository<Trainer>().Add(newTrainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex) 
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred while creating the trainer: {ex.Message}");
                return false;
            }

        }
        public bool DeleteTrainer(int trainerId)
        {
            var repo = _unitOfWork.GetRepository<Trainer>();

            var trainerToRemove = repo.GetByID(trainerId);

            if (trainerToRemove is null || HasActiveSessions(trainerId))
                return false;

            repo.Delete(trainerToRemove);

            return _unitOfWork.SaveChanges() > 0;
        }
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            //Name , Email , Phone , Specialization
            var trainers = _unitOfWork.GetRepository<Trainer>().Getall();
            if (trainers == null || !trainers.Any())
                return [];
            return trainers.Select(t => new TrainerViewModel
            {
                id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.phone,
                Specialization = t.Specialties.ToString()
            });
        }
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            try
            {
                // Photo, Name , Email , Phone ,date of birth , Specialization
                var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(trainerId);
                if (trainer == null)
                    return null;
                return new TrainerViewModel
                {
                    Name = trainer.Name,
                    Email = trainer.Email,
                    Phone = trainer.phone,
                    DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                    Specialization = trainer.Specialties.ToString(),
                    Photo = trainer.phone
                };
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred while retrieving trainer details: {ex.Message}");
                return null;
            }
        }
        public UpdataedTrinerViewModel? GetTrinerToUpdateViewModel(int MemberID)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(MemberID);
            if ( trainer is null)
                return null;
            var updatedtriner = new UpdataedTrinerViewModel
            {
               Name = trainer.Name,
               Email = trainer.Email,
               Phone = trainer.phone,
               BuildingNumber = trainer.Address.BuildingNumber,
               Street = trainer.Address.streeet,
               City = trainer.Address.City,
               Specialization = trainer.Specialties
            };
            return updatedtriner;
        }
        public bool UpdateTrainer(int trainerId, UpdataedTrinerViewModel updatedTrainer)
        {
            try
            {
                var triner = _unitOfWork.GetRepository<Trainer>().GetByID(trainerId);
                if (triner is null) return false;

                bool emailTaken = _unitOfWork.GetRepository<Trainer>()
                    .Getall()
                    .Any(t => t.Email == updatedTrainer.Email && t.Id != trainerId);

                bool phoneTaken = _unitOfWork.GetRepository<Trainer>()
                    .Getall()
                    .Any(t => t.phone == updatedTrainer.Phone && t.Id != trainerId);

                if (emailTaken || phoneTaken) return false;

                triner.Email = updatedTrainer.Email;
                triner.phone = updatedTrainer.Phone;
                triner.Address = new Address
                {
                    BuildingNumber = updatedTrainer.BuildingNumber,
                    streeet = updatedTrainer.Street,
                    City = updatedTrainer.City
                };
                triner.Specialties = updatedTrainer.Specialization;
                triner.UpdatedAT = DateTime.Now;

                _unitOfWork.GetRepository<Trainer>().Update(triner);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }        
        #region HelperMethods
        private bool ISEmailExsist(string Email)
        {
            return _unitOfWork.GetRepository<Trainer>().Getall(X => X.Email == Email).Any();
        }
        private bool ISPhoneExsist(string Phone)
        {
            return _unitOfWork.GetRepository<Trainer>().Getall(X => X.phone == Phone).Any();
        }
        private bool HasActiveSessions(int trainerId)
        {
            return _unitOfWork.GetRepository<Sessoin>().Getall(s => s.TrainerId == trainerId && s.StartDate>DateTime.Now).Any();
        }
        #endregion
    }
}
