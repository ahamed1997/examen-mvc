using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenProject.Models;
using ExamenBLLibrary;
using ExamenUserLibrary;
using System.Data.SqlClient;
using System.Data;

namespace ExamenProject.Controllers
{
    public class WelcomePageController : Controller
    {
        // GET: WelcomePage
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(string submitbutton)
        {
            try
            {
                switch (submitbutton)
                {
                    case "Log In":
                        return RedirectToAction("LogIn", "WelcomePage");

                    case "Sign Up":
                        return RedirectToAction("Register", "WelcomePage");
                    default:
                        return View();
                }
            }
            catch (SqlException se)
            {
                ViewBag.ErrorMessage = "Invalid Entry";
                
            }
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(ExamenDataModel examen, string submitbutton)
        {
            try
            {
                ExamenBL bl = new ExamenBL();
                string Username = examen.Username;
                string Password = examen.Password;                          
                if (submitbutton == "LogIn")
                {
                    if (string.IsNullOrEmpty(examen.Username))
                    {
                        ModelState.AddModelError("Username", "Username is required");
                    }else 
                    { 
                        if ((bl.UserValidation(Username, Password)))
                        {
                            Session.Add("un", Username);
                            return RedirectToAction("Home", "WelcomePage");
                        }
                        else
                        {
                            ViewBag.Message = "Invalid Username or Password";
                        }
                    }
                }
                else if(submitbutton == "Cancel")
                {
                        return RedirectToAction("Index", "WelcomePage");
                }
                
            }
            catch (SqlException se)
            {

                ViewBag.ErrorMessage = "Invalid Username or Password..";
            }
            

            return View();

        }
        public ActionResult Register(ExamenDataModel dataModel)
        {

           return View();


        }
        [HttpPost]
        public ActionResult Register(ExamenDataModel dataModel, string submitButton)
        {
            try
            {
                ExamenBL bl = new ExamenBL();
                ExamenStack examenStack = new ExamenStack();
                
                examenStack.Phone = dataModel.Phone;
                examenStack.Email = dataModel.Email;
                examenStack.Username = dataModel.Username;
                examenStack.Password = dataModel.Password;
                
                examenStack.Name = dataModel.Name;
                if (submitButton == "Submit")
                {

                    if (bl.InsertIntoCandidates(examenStack))
                    {
                        ViewBag.Message = "Registered Successfully";
                        return RedirectToAction("RegisterSuccessView", "WelcomePage");
                    }
                    else
                    {
                        return (ViewBag.Message = "Registration Failed..");
                    }

                }
                else
                if (submitButton == "Cancel")
                {
                    return RedirectToAction("Index", "WelcomePage");
                }
            }
            catch (SqlException se)
            {

                ViewBag.ErrorMessage = "Invalid Username or Password..";
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult Home()
        {

            if (Session["un"] == null)
            {
                return RedirectToAction("LogIn", "WelcomePage");
            }
            else if (Session["un"] != null)
            {
                ViewBag.WelcomeMsg = Session["un"].ToString();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Home(string submitbutton)
        {
            ExamenBL bl = new ExamenBL();
            if (submitbutton == "Register for Exam")
            {
              return RedirectToAction("ExamRegister", "HomePageExamen");
            }
            else 
            if (submitbutton == "Take Test")
            {              
                
                return RedirectToAction("InstructionPage", "HomePageExamen"); 
            }
            else if (submitbutton == "View Profile")
            {
                TempData["un"] = ViewBag.WelcomeMsg;
                return RedirectToAction("ViewProfile", "HomePageExamen");
            }
            else if (submitbutton == "Log Out")
            {
                Session["un"] = null;
                return RedirectToAction("Index", "WelcomePage");
            }


            return View();
        }
        public ActionResult RegisterSuccessView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterSuccessView(string submitbutton)
        {
            if (submitbutton == "Back")
            {
                return RedirectToAction("Index", "WelcomePage");
            }
            else
                return View();

        }
    
    
    }
}