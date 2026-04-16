using GymManagementSystemDAL.Entity;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IsessionRepository :IGenaricRepository<Sessoin>
    {
        IEnumerable<Sessoin> GetAllSessionsWithTrinerAndCategory();
        int GetCountOfBookedSlots(int sessionId);
        Sessoin? GetSessionWithTrainerAndCategory(int sessionId);
    }
}
