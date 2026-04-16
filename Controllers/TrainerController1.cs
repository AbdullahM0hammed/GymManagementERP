using GymManagementSystemBLL.Services.Classes;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs;
using GymManagementSystemBLL.Services.ViewModeIs.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public ActionResult Index()
        {
            var Trainers = _trainerService.GetAllTrainers();
            return View(Trainers);
        }
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error massage"] = "ID Of Trainer Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerDetails(id);
            if (Trainer is null)
            {
                TempData["Error massage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreatetrainerViewmodel CreatedTrainer) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreatedTrainer);
            }
            bool IsCreated = _trainerService.CreateTrainer(CreatedTrainer);
            if (IsCreated)
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            else
                TempData["ErrorMessage"] = "Failed To Create Trainer";

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit(int Id)
        {
            if (Id <= 0)
            {
                TempData["Error massage"] = "ID Of Trainer Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerService.GetTrinerToUpdateViewModel(Id);
            if (Trainer is not null)
            {
                return View(Trainer);
            }
            TempData["Error massage"] = "Trainer Not Found .";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int Id, UpdataedTrinerViewModel updataedTriner )
        {
            if (!ModelState.IsValid)
                return View(updataedTriner);
            var result = _trainerService.UpdateTrainer(Id, updataedTriner);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully"; //  SuccessMessage
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Updated Failed"; //  ErrorMessage
            }

            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed([FromForm]int id)
        //{
        //    if (id<= 0)
        //    {
        //        TempData["Error massage"] = "ID Of Trainer Can Not Be 0 OR Negative Number .";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    var result = _trainerService.DeleteTrainer(id);
        //    if (result)
        //        TempData["SuccessMessage"] = "Trainer Deleted Successfully"; //  SuccessMessage
        //    else
        //        TempData["ErrorMessage"] = "Trainer Deleted Failed"; //  ErrorMessage
        //    return RedirectToAction(nameof(Index));
        //}
        // GET: /Trainer/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer ID.";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = id;
            return View(); // هيفتح Delete.cshtml
        }

        // POST: /Trainer/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Of Trainer Can Not Be 0 OR Negative Number.";
                return RedirectToAction(nameof(Index));
            }

            var result = _trainerService.DeleteTrainer(id);

            if (result)
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Trainer Deleted Failed";

            return RedirectToAction(nameof(Index));
        }
    }
}
