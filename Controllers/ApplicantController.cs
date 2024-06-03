
using JobAppManagement.Context;
using JobAppManagement.Models.Applicant;
using JobAppManagement.Utility;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobAppManagement.Data;

namespace JobAppManagement.Controllers;

[Authorize]
public class ApplicantController(UserManager<IdentityUser> userManager,
SignInManager<IdentityUser> signInManager,
INotyfService notyf,
JobAppManagementContext jobAppManagementContext,
IHttpContextAccessor httpContextAccessor) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly INotyfService _notyfService = notyf;
    private readonly JobAppManagementContext _jobAppManagementContext = jobAppManagementContext;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ApplicantRegistration()
    {
        var states = _jobAppManagementContext.States.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();

        var jobs = _jobAppManagementContext.Job.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();

        var viewModel = new ApplicantViewModel
        {
            States = states,
            Job = jobs
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ApplicantRegistration(ApplicantViewModel model)
    {
        var applicantExist = await _jobAppManagementContext.Applicant.AnyAsync(x => x.FirstName == model.FirstName || x.Email == model.Email);
        var userDetail = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);

        if (applicantExist)
        {
            _notyfService.Warning("Applicant already exist");
            return View(model);
        }

        var applicant = new Applicant
        {
            UserId = userDetail.userId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            StateId = model.StateId,
            JobId = model.JobId
        };

        await _jobAppManagementContext.AddAsync(applicant);
        var result = await _jobAppManagementContext.SaveChangesAsync();

        if (result > 0)
        {
            _notyfService.Success("Applicant registered successfully");
            return RedirectToAction("Index", "Applicant");
        }

        _notyfService.Error("An error occured during registration");
        return View();
    }

    [HttpGet("Applicant/ApplicantDetail")]
    public async Task<IActionResult> ApplicantDetail()
    {
        var user = await _userManager.GetUserAsync(User);

        var applicant = await _jobAppManagementContext.Applicant
            // .Include(m => m.FirstName)
            // .Include(m => m.LastName)
            // .Include(m => m.Email)
            // .Include(m => m.PhoneNumber)
            // .Include(m => m.Gender)        
            .FirstOrDefaultAsync(m => m.UserId == user!.Id);


        if (applicant != null)
        {
            var applicantDetailViewModel = new ApplicantDetailViewModel
            {
                FirstName = applicant.FirstName,
                LastName = applicant.LastName,
                Email = applicant.Email,
                PhoneNumber = applicant.PhoneNumber,
                Gender = applicant.Gender,
            };

            return View(applicantDetailViewModel);
        }
        else
        {
            _notyfService.Error("Applicant details not found");
            return RedirectToAction("ApplicantRegistration", "Applicant");
        }
    }


    public IActionResult EditApplicant()
    {
        var states = _jobAppManagementContext.States.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();

        var viewModel = new ApplicantViewModel
        {
            States = states
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditApplicant(ApplicantDetailViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);

            var applicant = await _jobAppManagementContext.Applicant
                .FirstOrDefaultAsync(a => a.UserId == user!.Id);

            if (applicant != null)
            {
                applicant.FirstName = model.FirstName;
                applicant.LastName = model.LastName;
                applicant.Email = model.Email;
                applicant.PhoneNumber = model.PhoneNumber;
                applicant.Gender = model.Gender;

                _jobAppManagementContext.Update(applicant);
                await _jobAppManagementContext.SaveChangesAsync();

                _notyfService.Success("Applicant details updated successfully");
                return RedirectToAction("Index", "Applicant");
            }
            else
            {
                _notyfService.Error("Applicant details not found");
                return RedirectToAction("ApplicantDetail", "Applicant");
            }
        }

        _notyfService.Error("An error occurred while updating detail");
        return View(model);
    }


    public IActionResult DeleteApplicant()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteApplicant(ApplicantDetailViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);

        var applicant = await _jobAppManagementContext.Applicant
            .FirstOrDefaultAsync(a => a.UserId == user!.Id);

        if (applicant != null)
        {
            _jobAppManagementContext.Remove(applicant);
            await _jobAppManagementContext.SaveChangesAsync();

            _notyfService.Success("Applicant details deleted successfully");
            return RedirectToAction("ApplicantRegistration", "Applicant");
        }
        else
        {
            _notyfService.Error("Applicant details not found");
            return RedirectToAction("ApplicantRegistration", "Applicant");
        }
    }
}
