using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Model.Models;
using Booking.Web.Data;
using Booking.Web.helpers;
using Microsoft.AspNetCore.Authorization;

namespace Booking.Web.Controllers
{
    [Authorize]
    public class DemoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Demo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Demo;//.Include(d => d.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Demo/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                Demo model = new Demo();
                //model.AppUser = null;
                //ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "ImageUrl", model.AppUserId);
                return View(model);
            }
            else
            {
                var demo = await _context.Demo.FindAsync(id);
                if (demo == null)
                {
                    return NotFound();
                }
                ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "ImageUrl", demo.AppUserId);
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
                        _context.Add(model);//add
                    }
                    else
                    {
                        msg = "Updated Successfully";
                        _context.Update(model);//update
                    }
                    await _context.SaveChangesAsync();
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
                return Json(new { isValid = true, message = msg, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Demo.ToList()) });
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "ImageUrl", model.AppUserId);
            return Json(new { isValid = false, message = "Something went wrong!", html = Helper.RenderRazorViewToString(this, "Edit", model) });
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Demo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Demo'  is null.");
            }
            var demo = await _context.Demo.FindAsync(id);
            if (demo != null)
            {
                _context.Demo.Remove(demo);
            }
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, message = "Deleted Successfully", html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Demo.ToList()) });
        }

        private bool DemoExists(int id)
        {
            return _context.Demo.Any(e => e.Id == id);
        }
    }
}
