using GymManagementSystemBLL.Services.ViewModeIs.SessionViewModeIs;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetallSessions();
        SessionViewModel? GetSessionById(int sessionID);
        bool CreateSession(CreateSessionViewModel CreatedSession);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionID);
        bool UpdateSession(int sessionID, UpdateSessionViewModel UpdatedSession);
        bool DeleteSession(int sessionID);
        IEnumerable<CategorySelectViewModel> GetAllCategoriesForSelection();
        IEnumerable<TrainerSelectViewModel> GetAllTrainersForSelection();
    }
}
