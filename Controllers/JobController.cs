using JobAppManagement.Context;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationManagementSystem.Controllers
{
    public class JobController(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    INotyfService notyf,
    JobAppManagementContext _jobAppManagementContext,
    IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly INotyfService _notyfService = notyf;
        private readonly JobAppManagementContext _jobAppManagementContext = _jobAppManagementContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<IActionResult> Index()
        {
            // List<Job> jobs =  _jobAppManagementContext.Job.ToList();
            return View(await _jobAppManagementContext.Job.ToListAsync());
        }

        public async Task<IActionResult> JobDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobAppManagementContext.Job
                .FirstOrDefaultAsync(j => j.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        public IActionResult CreateJob()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateJob([Bind("Id,Title,Description,Requirements,Location,PostedDate,ExpiryDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                _jobAppManagementContext.Add(job);
                await _jobAppManagementContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        public async Task<IActionResult> EditJob(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobAppManagementContext.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJob(int id, [Bind("Id,Name,Description")] Job job)
        {
            if (id != job!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _jobAppManagementContext.Update(job);
                    await _jobAppManagementContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        public async Task<IActionResult> DeleteJob(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobAppManagementContext.Job
                .FirstOrDefaultAsync(j => j.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _jobAppManagementContext.Job.FindAsync(id);
            _jobAppManagementContext.Job.Remove(job!);
            await _jobAppManagementContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _jobAppManagementContext.Job.Any(j => j.Id == id);
        }
    }
}