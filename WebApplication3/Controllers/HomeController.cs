using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext = ApplicationDbContext.Create();
        [Authorize]
        public ActionResult Index()
        {

            string id = User.Identity.GetUserId();
            bool isAdm = User.IsInRole("Administrator");
            List<VacationViewModel> model = isAdm ? dbContext.Vacations.ToList() : dbContext.Vacations.Where(x => x.UserId == id).ToList();

            foreach (VacationViewModel vacationViewModel in model)
            {
                vacationViewModel.ApplicationUser = dbContext.Users.Find(vacationViewModel.UserId);
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            VacationViewModel model = dbContext.Vacations.Find(id);
            bool isAdm = User.IsInRole("Administrator");
            if (model != null)
            {
                if (isAdm || model.UserId == User.Identity.GetUserId())
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(VacationViewModel model)
        {

            var v = dbContext.Vacations.Find(model.Id);
            if (v != null)
            {
                v.Description = model.Description;
                v.StartsAt = model.StartsAt;
                v.EndsAt = model.EndsAt;
                dbContext.Entry(v).State = EntityState.Modified;
                dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            VacationViewModel model = new VacationViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(VacationViewModel model)
        {

            VacationViewModel v = new VacationViewModel();
            v.Description = model.Description;
            v.StartsAt = model.StartsAt;
            v.EndsAt = model.EndsAt;
            v.UserId = User.Identity.GetUserId();
            dbContext.Vacations.Add(v);
            dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            var v = dbContext.Vacations.Find(id);
            bool isAdm = User.IsInRole("Administrator");
            if (v != null)
            {
                if (isAdm || v.UserId == User.Identity.GetUserId())
                {
                    {
                        dbContext.Entry(v).State = EntityState.Deleted;
                        dbContext.SaveChangesAsync();
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}