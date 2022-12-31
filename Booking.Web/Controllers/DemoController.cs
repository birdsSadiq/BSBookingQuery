using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Model.Models;
using Booking.Web.Data;
using Booking.Web.helpers;
using Microsoft.AspNetCore.Authorization;
using Booking.Web.Data.Repository;

namespace Booking.Web.Controllers
{
    [Authorize]
    public class DemoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork uow;
        public DemoController(IUnitOfWork uow, ApplicationDbContext context)
        {
            this.uow = uow;
            this.context = context;
        }

        // GET: Demo
        public async Task<IActionResult> Index()
        {
            return View(await uow.DemoRepository.GetAllAsync());
        }

        // GET: Demo/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                Demo model = new Demo();
                return View(model);
            }
            else
            {
                //var demo = await  _context.Demo.FindAsync(id);
                var demo = uow.DemoRepository.GetById(id);
                if (demo == null)
                {
                    return NotFound();
                }
                return View(demo);
            }
        }

        // POST: Demo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LogoUrl,AppUserId")] Demo model)
        {
            string msg = "Something went wrong!";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        msg = "Saved Successfully";
                        uow.DemoRepository.Add(model);//add
                    }
                    else
                    {
                        msg = "Updated Successfully";
                        uow.DemoRepository.Update(model);//update
                    }
                    bool status = await uow.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemoExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var models = uow.DemoRepository.GetAllAsync();
                return Json(new { isValid = true, message = msg, html = Helper.RenderRazorViewToString(this, "_ViewAll", models.Result.ToList()) });
            }
            return Json(new { isValid = false, message = "Something went wrong!", html = Helper.RenderRazorViewToString(this, "Edit", model) });
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!uow.DemoRepository.IsModelExist(id))
            {
                return Problem("Not Found.");
            }
            var model = uow.DemoRepository.GetById(id);
            if (model != null)
            {
                uow.DemoRepository.Delete(id);
                var result = uow.SaveAsync();
            }
            //return Json(new { isValid = true, message = "Deleted Successfully", html = Helper.RenderRazorViewToString(this, "_ViewAll", uow.DemoRepository.GetAllAsync()) });
            return Json(new { isValid = true, message = "Deleted Successfully", html = Helper.RenderRazorViewToString(this, "_ViewAll", context.Demo.ToList()) });
        }

        // GET: Demo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var demo = uow.DemoRepository.GetById(id.Value);
            if (demo == null)
            {
                return NotFound();
            }

            return View(demo);
        }

        private bool DemoExists(int id)
        {
            return uow.DemoRepository.IsModelExist(id);
        }
    }
}
