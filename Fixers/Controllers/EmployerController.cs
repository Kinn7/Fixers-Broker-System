using Fixers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fixers.Controllers
{
    public class EmployerController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();
        // GET: Employer

        public ActionResult SignUp()
        {
            if (Session["FirstName"] != null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Employer emp)
        {
            if (ModelState.IsValid)
            {
                context.Employers.Add(emp);
                context.SaveChanges();
                return RedirectToAction("Login", "Employer");
            }
            return View();
        }
        [AllowAnonymous]
        public JsonResult CheckEmail(string Email)
        {

            var result = context.Employers.Where(a => a.Email.ToLower() == Email.ToLower()).FirstOrDefault();
            bool status;
            if (result != null)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            if(Session["FirstName"] != null)
            {
                return RedirectToAction("Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employer employer)
        {
            if (!ModelState.IsValid)
            {
                var obj = context.Employers.Where(e => e.Email.Equals(employer.Email) && e.Password.Equals(employer.Password)).FirstOrDefault();
                if(obj != null)
                {
                    Session["FirstName"] = obj.FirstName.ToString();
                    Session["EmployerId"] = obj.id;
                    return RedirectToAction("Home");

                }
            }
            return View();
        }

        public ActionResult Home()
        {
            if (Session["FirstName"] != null)
            {
                var AvailableProfessionals = context.Professionals.Where(p =>(int) p.Status == 1 && p.Balance >= 100).ToList();
                return View(AvailableProfessionals);
            }
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            Session["FirstName"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Hire( int id)
        {
            int EmployerId = (int)Session["EmployerId"];
            var EmployNumber = context.HireStatus.Where(e => e.EmployerID == EmployerId).Count();

            if (EmployNumber < 2) { //Checking if the employer hired more than two professionals
                var changestatus = context.Professionals.Where(t => t.ID == id).FirstOrDefault();
                var balance = context.Professionals.Where(b => b.ID == id).FirstOrDefault();
                    
                balance.Balance -= 100; //subtract 100 from balance of professional
                changestatus.Status = 0;//Change Status of professional from Avilalble to hired

                HireStatus hireStatus = new HireStatus();
                hireStatus.ProfessionalID = id;
                hireStatus.EmployerID = EmployerId;
                context.HireStatus.Add(hireStatus);//insert selected professional id and employer id to the hire status table 

                context.SaveChanges();
                return RedirectToAction("Home");
            }
            ViewBag.Error = "Can not hire More than Three Professionals";
            return RedirectToAction("Home");
           
        }

        public ActionResult Hired()
        {
            if (Session["FirstName"] != null)
            {
                int EmployerId = (int)Session["EmployerId"];
                var hiredProfessionals = context.HireStatus.Where(a => a.EmployerID == EmployerId).ToList().Select(p => new HireStatus
                {
                    Professional = p.Professional
                }).ToList();
                return View(hiredProfessionals);
            }
            return RedirectToAction("Login ");
        }

        public ActionResult Account()
        {
            
            if(Session["FirstName"] != null)
            {
                int EmployerId = (int)Session["EmployerId"];
                var existingEmployers = context.Employers.Where(t => t.id == EmployerId).FirstOrDefault();
                return View(existingEmployers); 
            }

            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Account(Employer emp)
        {
            if (ModelState.IsValid)
            {
                int EmployerId = (int)Session["EmployerId"];
                var existingEmployer = context.Employers.Where(t => t.id == EmployerId).FirstOrDefault();
                existingEmployer.FirstName = emp.FirstName;
                existingEmployer.LastName = emp.LastName;
                existingEmployer.Email = emp.Email;
                existingEmployer.Password = emp.Password;
                existingEmployer.Address.kebele_id = emp.Address.kebele_id;
                existingEmployer.Address.sub_city = emp.Address.sub_city;
                existingEmployer.Address.woreda = emp.Address.woreda;
                existingEmployer.Address.house_no = emp.Address.house_no;
                existingEmployer.Address.phone_no = emp.Address.phone_no;

                Session["FirstName"] = existingEmployer.FirstName;

                context.SaveChanges();
                return RedirectToAction("Home");
            }
            return View();
        }
    }
}
