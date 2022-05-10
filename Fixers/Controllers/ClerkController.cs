using Fixers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fixers.Controllers
{

    public class ClerkController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();
        // GET: Clerk

        public ActionResult Signin()
        {
            if(Session["UserName"] != null || Session["Clerkid"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Signin(Clerk clerk)
        {
         

                if (ModelState.IsValid)
                {
//                clerk.UserName = Request.Form["username"];
                    var obj = context.Clerks.Where(a => a.UserName.Equals(clerk.UserName) && a.password.Equals(clerk.password)).FirstOrDefault();
                
                    if (obj != null)
                    {
                        Session["Clerkid"] = obj.id.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Dashboard");
                    }

                }
                return View();
            
        }
        public ActionResult expt()
        {
            return View();
        }
    
        public ActionResult Dashboard()
        {


            if (Session["Clerkid"] != null)
            {
                var professionals = context.Professionals.Count();
                var Employers = context.Employers.Count();
                var EmployersHired = context.HireStatus.Select(s => s.EmployerID).Distinct().Count();
                var ProfessionalsHired = context.HireStatus.Select(p => p.ProfessionalID).Count();
                
                ViewBag.Employers = Employers;
                ViewBag.Professionals = professionals;
                ViewBag.EmployersHire = EmployersHired;
                ViewBag.ProfessionalsHired = ProfessionalsHired;

                return View();
            }
            else
            {
                return RedirectToAction("Signin");
            }
        }

        
        public ActionResult Professionals()
        {


            if (Session["Clerkid"] != null)
            {
                var profession = context.Professions.ToList();
                ViewBag.Profession = profession;

                return View();
            }
            else
            {
                return RedirectToAction("Signin");
            }
        }
        [HttpPost]
        public ActionResult Professionals(Professional professional)
        {
            if (ModelState.IsValid)
            {
                context.Professionals.Add(professional);
                context.SaveChanges();
                return RedirectToAction("Dashboard", "Clerk");
            }
            else
            {
                var profession = context.Professions.ToList();
                ViewBag.Profession = profession;

                return View();
            }
        }
        public ActionResult Employer()
        {
            if (Session["Clerkid"] == null)
            {
                return RedirectToAction("Signin");
            }
            var Employers = context.Employers.ToList();

            return View(Employers);
            

        }
        public ActionResult DeleteEmployers(long id)
        {
            var Employers = context.Employers.Where(t => t.id == id).FirstOrDefault();
            context.Employers.Remove(Employers);
            context.SaveChanges();
            return RedirectToAction("Employer");
        }
        public ActionResult DeleteProfessionals(long id)
        {
            var Professionals = context.Professionals.Where(p => p.ID == id).FirstOrDefault();
            context.Professionals.Remove(Professionals);
            context.SaveChanges();
            return RedirectToAction("ManageProfessional");
            
        }
        public ActionResult EditProfessional(long id)
        {
            if (Session["Clerkid"] != null)
            {
                var profession = context.Professions.ToList();
                ViewBag.Profession = profession;
                var existingProfessionals = context.Professionals.Where(t => t.ID == id).FirstOrDefault();
                return View(existingProfessionals);
            }
            else
            {
                return RedirectToAction("Signin");
            }
            

        }
        [HttpPost]
        public ActionResult EditProfessional(Professional prof)
        {
            if (ModelState.IsValid)
            {
                 var existingProfessionals = context.Professionals.Where(t => t.ID == prof.ID).FirstOrDefault();
                existingProfessionals.FirstName = prof.FirstName;
               
                existingProfessionals.LastName = prof.LastName;
                existingProfessionals.Balance = prof.Balance;
                existingProfessionals.Status = prof.Status;
                existingProfessionals.Fee = prof.Fee;
                existingProfessionals.Password = prof.Password;
                existingProfessionals.ConfirmPassword = prof.ConfirmPassword;
                existingProfessionals.Address.kebele_id = prof.Address.kebele_id;
                existingProfessionals.Address.sub_city = prof.Address.sub_city;
                existingProfessionals.Address.woreda = prof.Address.woreda;
                existingProfessionals.Address.house_no = prof.Address.house_no;
                existingProfessionals.Address.phone_no = prof.Address.phone_no;
                existingProfessionals.ProfessionID = prof.ProfessionID;

                //!existingProfessionals.Status.Equals(0)



                if (existingProfessionals.Status != 0 && context.HireStatus.Where(p => p.ProfessionalID == prof.ID).Any())
                {
                    var cs = context.HireStatus.Single(o => o.ProfessionalID == prof.ID);

                    context.HireStatus.Remove(cs);
                }

                //context.Professionals.Add(existingProfessionals);
                context.SaveChanges();
                return RedirectToAction("manageprofessional","Clerk");



            }
            else
            {
                var profession = context.Professions.ToList();
                ViewBag.Profession = profession;

                return View();
            }

        }
        public ActionResult ManageProfessional()
        {
            var professional = context.Professionals.ToList();
            return View(professional);
        }

        public ActionResult AddProfession()
        {
            if (Session["Clerkid"] != null)
            {
                return View();
            }
            return RedirectToAction("Signin");
        }
        [HttpPost]
        public ActionResult AddProfession(Profession prof)
        {
            if (ModelState.IsValid)
            {
                context.Professions.Add(prof);
                context.SaveChanges();
                return RedirectToAction("AddProfession", "Clerk");

            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Clerkid"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Signin");
        }

    }
}