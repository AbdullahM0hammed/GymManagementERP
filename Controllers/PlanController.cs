using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.PlanViewModels;
using GymManagementSystemDAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public ActionResult Index()
        {
            var Plans= _planService.GetPlans();
            return View(Plans);
        }
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Of Plan Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var Plans = _planService.GetPlanByID(id);
            if (Plans is null)
            {
                TempData["ErrorMessage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            return View(Plans);
        }
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Of Plan Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var Plans = _planService.GetPlanToUpdate(id);
            if (Plans is null)
            {
                TempData["ErrorMessage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            return View(Plans);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdatePlanViewmodel updatePlan)
        { 
         if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid Data .";
                return RedirectToAction(nameof(Index));
            }
         var result = _planService.UpdatePlan(id, updatePlan);
            if (!result)
            {
                TempData["ErrorMessage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Plan Updated Successfully .";
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Activate(int id)
        {
            var result = _planService.TogglePlanStatus(id);
            if (!result)
            {
                TempData["ErrorMessage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Plan status toggled successfully .";
            return RedirectToAction(nameof(Index));
        }
    }
}
