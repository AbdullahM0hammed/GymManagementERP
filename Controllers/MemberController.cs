using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["Error massage"] = "ID Of Member Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var members = _memberService.GetMemberDesk(id);
            if (members is null) {
                TempData["Error massage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["Error massage"] = "ID Of Member Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var HealthRecord = _memberService.GetMemberHealt(id);
            if (HealthRecord is null)
            {
                TempData["Error massage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            return View(HealthRecord);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreatedMember);
            }

            bool Result = _memberService.CreateMember(CreatedMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Create , Check Phone And Email";
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult MemberEdit(int Id)
        {
            if (Id <= 0)
            {
                TempData["Error massage"] = "ID Of Member Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberToUpdateViewModel(Id);
            if (Member is null)
            {
                TempData["Error massage"] = "Member Not Found .";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int Id, MemberToUpdateViewModel memberToEdit)
        {
            if (!ModelState.IsValid)
                return View(memberToEdit);
            var result = _memberService.UpdateMember(Id, memberToEdit);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully"; //  SuccessMessage
            }
            else
            {
                TempData["ErrorMessage"] = "Member Updated Failed"; //  ErrorMessage
            }

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                TempData["Error massage"] = "ID Of Member Can Not Be 0 OR Negative Number .";
                return RedirectToAction(nameof(Index));
            }
            var result = _memberService.GetMemberHealt(Id);
            if (result is null)
            {
                TempData["Error massage"] = "ID Not Found .";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = Id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int Id)
        {
            var result = _memberService.RemoveMember(Id);
            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully"; //  SuccessMessage
            else
                TempData["ErrorMessage"] = "Member Deleted Failed"; //  ErrorMessage
            return RedirectToAction(nameof(Index));
        }
    }
}