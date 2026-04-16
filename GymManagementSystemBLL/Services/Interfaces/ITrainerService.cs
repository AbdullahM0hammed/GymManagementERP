using GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs;
using GymManagementSystemBLL.Services.ViewModeIs.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        // GetallTrainers
        IEnumerable<TrainerViewModel> GetAllTrainers();
        // CreateTrainer
        bool CreateTrainer(CreatetrainerViewmodel trainer);
        // GetTrainerDetails
        TrainerViewModel? GetTrainerDetails(int trainerId);
        // Update Trainer Data
        bool UpdateTrainer(int trainerId, UpdataedTrinerViewModel updatedTrainer);
        UpdataedTrinerViewModel? GetTrinerToUpdateViewModel(int MemberID);
        // Remove Trainer
        bool DeleteTrainer(int trainerId);
    }
}
