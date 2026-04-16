using GymManagementSystemDAL.Data.Contexts;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class SessionRepository : GenaricRepository<Sessoin>, IsessionRepository
    {
        private readonly GymDbContext _dbContext;
        public SessionRepository( GymDbContext dbContext ) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Sessoin> GetAllSessionsWithTrinerAndCategory()
        {
            return _dbContext.sessoins.Include(s => s.Trainer)
                                        .Include(s => s.Category)
                                        .ToList();
        }
        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(b => b.SessionId == sessionId);
        }

        public Sessoin? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.sessoins.Include(s => s.Trainer)
                                        .Include(s => s.Category)
                                        .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
