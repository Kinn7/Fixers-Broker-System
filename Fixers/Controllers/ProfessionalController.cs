using Fixers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fixers.Controllers
{
    public class ProfessionalController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();
        // GET: Professional
        public ActionResult Login()
        {
            if (Session["FirstName"] != null)
            {
                return RedirectToAction("EmploymentStatus");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Professional prof)
        {
            if (!ModelState.IsValid)
            {
                var obj = context.Professionals.Where(e => e.FirstName.Equals(prof.FirstName) && e.Password.Equals(prof.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["ProfessionalIds"] = obj.ID;
                    Session["ProfUser"] = obj.FirstName.ToString();
                    return RedirectToAction("EmploymentStatus");

                }
            }
            return View();
        }

        public ActionResult Expt()
        {
            return View();
        }

        public ActionResult EmploymentStatus()
        {
            if (Session["ProfessionalIds"] != null)
            {
                var ProfessionalId = (int)Session["ProfessionalIds"];
                var Employed = context.HireStatus.Select(p => p.ProfessionalID == ProfessionalId);
                if (Employed.Any())
                {
                    var hiredByEmployer = context.HireStatus.Where(a => a.ProfessionalID == ProfessionalId).ToList().Select(p => new HireStatus
                    {
                        Employer = p.Employer
                        //Professional = p.Professional
                        
                    }).ToList();
                    //return View(hiredByEmployer);
                    return View(hiredByEmployer);
                }
                else
                {
                    return RedirectToAction("Expt");
                }
               

            }
            return RedirectToAction("Login");
           
        }

        public ActionResult Reject(int id)
        {
            int ProfessionalId = (int)Session["ProfessionalIds"];
            var changestatus = context.Professionals.Where(t => t.ID == ProfessionalId).FirstOrDefault();
            var balance = context.Professionals.Where(b => b.ID == ProfessionalId).FirstOrDefault();

            balance.Balance += 100;
            changestatus.Status = (Data.ProfessionalStatus)1;

            //HireStatus hireStatus = new HireStatus();
            ////hireStatus.ProfessionalID = ProfessionalId;
            //hireStatus.EmployerID = id;
            //hireStatus.ProfessionalID = ProfessionalId;
            var delEmp = context.HireStatus.Where(e => e.ProfessionalID == ProfessionalId).FirstOrDefault();
            context.HireStatus.Remove(delEmp);//insert selected professional id and employer id to the hire status table 

            context.SaveChanges();
            
            return RedirectToAction("EmploymentStatus");
        }


        public ActionResult Logout()
        {
            Session["ProfessionalIds"] = null;
            Session["ProfUser"] = null;
            return RedirectToAction("Login");
        }

    }
}