using GymManagementSystemBLL.Services.ViewModeIs.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable <PlanViewmodel> GetPlans ();
        PlanViewmodel? GetPlanByID  (int PlanId);
        UpdatePlanViewmodel? GetPlanToUpdate (int PlanId);
        bool UpdatePlan (int PlanId, UpdatePlanViewmodel updatePlan);
        bool TogglePlanStatus (int PlanId);
    }
}
