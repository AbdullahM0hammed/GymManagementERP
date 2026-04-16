using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public ActionResult Index()
        {
            var sessions = _sessionService.GetallSessions();
            return View(sessions);
        }
        public ActionResult Details(int id)
        { 
        if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }
        var session = _sessionService.GetSessionById(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }
        public ActionResult Create()
        {
            LoadSelectCategoryDropDowns();
            LoadSelectTrainerDropDowns();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form.";
                return RedirectToAction(nameof(Create));
            }
            var result = _sessionService.CreateSession(createSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create session. Please try again.";
                LoadSelectCategoryDropDowns();
                LoadSelectTrainerDropDowns();
                return View(createSession);
            }
        }
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionToUpdate(id); 
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            LoadSelectTrainerDropDowns();
            return View(session); 
        }
        [HttpPost]
        public ActionResult Edit ([FromRoute] int id ,UpdateSessionViewModel editSession)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form.";
                LoadSelectTrainerDropDowns();
                return View(editSession);
            }
            var result = _sessionService.UpdateSession(id, editSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update session. Please try again.";
                LoadSelectTrainerDropDowns();
                return View(editSession);
            }
        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionId = id; 
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }
            var result = _sessionService.DeleteSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete session. Please try again.";
            }
            return RedirectToAction(nameof(Index));
        }
        #region Helper
        private void LoadSelectCategoryDropDowns()
        {
            var categories = _sessionService.GetAllCategoriesForSelection();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName");
        }
        private void LoadSelectTrainerDropDowns()
        {
            var trainers = _sessionService.GetAllTrainersForSelection();
            ViewBag.Trainers = new SelectList(trainers, "TrainerID", "TrainerName");
        }
        #endregion
    }
}
