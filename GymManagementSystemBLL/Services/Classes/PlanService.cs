
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.PlanViewModels;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Interfaces;
using System.Numerics;

namespace GymManagementSystemBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public PlanViewmodel? GetPlanByID(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetByID(PlanId);
            if (plan is null)
                return null;
            return new PlanViewmodel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };
        }
        public IEnumerable<PlanViewmodel> GetPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().Getall();
            if (plans is null || !plans.Any())
                return [];
            return plans.Select(p => new PlanViewmodel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive
            }).ToList();
        }
        public UpdatePlanViewmodel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetByID(PlanId);
            if (plan is null||plan.IsActive==false || HasActiveMembershibs(PlanId)) return null;
            return new UpdatePlanViewmodel()
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };

        }
        public bool TogglePlanStatus(int PlanId)
        {
            var repo = _unitOfWork.GetRepository<Plan>();
            var plan = repo.GetByID(PlanId);
            if (plan is null || HasActiveMembershibs(PlanId))return false;
            plan.IsActive = !plan.IsActive;
            plan.UpdatedAT= DateTime.UtcNow;   // ← prefer UtcNow in most APIs
            try
            {
                repo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdatePlan(int PlanId, UpdatePlanViewmodel updatePlan)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetByID(PlanId);
            if (plan is null || HasActiveMembershibs(PlanId)) return false;
            try
            {
                plan.Description = updatePlan.Description;
                plan.Price = updatePlan.Price;
                plan.DurationDays = updatePlan.DurationDays;
                plan.UpdatedAT = DateTime.Now;

                _unitOfWork.GetRepository<Plan>().Update(plan);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        #region Helper
        private bool HasActiveMembershibs(int PlanID)
        {
            var ActiveMemberships = _unitOfWork.GetRepository<Membership>()
                .Getall(ms => ms.Id == PlanID && ms.status== "Active");
              return ActiveMemberships.Any();   
        }
        #endregion
    }
}
