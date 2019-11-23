using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenProject.Models;
using ExamenUserLibrary;
using ExamenBLLibrary;
using System.Data;
using System.Data.SqlClient;

namespace ExamenProject.Controllers
{
    public class HomePageExamenController : Controller
    {
        // GET: TestPageExamen
         ExamenDataModel dataModel = new ExamenDataModel();
        ExamenBL bl = new ExamenBL();
        int i=0;
        List<int> index = new List<int>();
        public ActionResult ExamRegister(ExamenDataModel dataModel)
        {
            //if (TempData["un"] == null)
            //{
            //    return RedirectToAction("LogIn", "WelcomePage");
            //}
            //else if (TempData["un"] != null)
            //{
            //    ViewBag.WelcomeMsg = TempData["un"].ToString();
            //}
            ExamenBL bl = new ExamenBL();
            dataModel = new ExamenDataModel();
            List<string> examens = bl.GetAllTestModels();
            dataModel.Testmodels = new SelectList(examens.ToList());
            return View(dataModel);
        }
        [HttpPost]
        public ActionResult ExamRegister(string submitbutton,ExamenDataModel examen)
        {           
            
            if (submitbutton == "Register")
            {


                string username = Session["un"].ToString();
                string testmodel = examen.Testmodel;
                if (bl.CheckUserInPayment(username,testmodel))
                {
                    TempData["fail"]= "Resgistration Failed!!!";
                    return RedirectToAction("ExamRegisterResult", "HomePageExamen");
                }
                else if (bl.Payment(username,testmodel)!="")
                {
                    
                    return RedirectToAction("ExamRegisterResult", "HomePageExamen");
                }
                else
                {
                   ViewBag.Message = "Resgistration Failed!!!";
                }
                
            }
            else if (submitbutton == "Cancel")
            {
                return RedirectToAction("Home", "WelcomePage");
            }
                return View();             
        }

        
       
        [HttpGet]
        public ActionResult TakeTest(ExamenDataModel examen)
        {
            try
            {
                
                if (Session["no"] == null)
                {
                    i = 0;
                }
                else
                {
                    i = Convert.ToInt32(Session["no"]);
                }
                ExamenBL bl = new ExamenBL();
                string testmodel = Session["modl"].ToString();
                //string testmodel = "Aptitude";
                List<ExamenDataModel> pQuestions = GetAllQuestionsFromBL(testmodel);
                ExamenDataModel pageQuestions = new ExamenDataModel();                             
                
                pageQuestions.Sno = pQuestions[i].Sno;
                pageQuestions.Questions = pQuestions[i].Questions;
                pageQuestions.Option1 = pQuestions[i].Option1;
                pageQuestions.Option2 = pQuestions[i].Option2;
                pageQuestions.Option3 = pQuestions[i].Option3;
                pageQuestions.Option4 = pQuestions[i].Option4;
                pageQuestions.Answers = pQuestions[i].Answers;
                return View(pageQuestions);


            }
            catch (Exception ex)
            {
                ViewBag.Message = "Please Select an answer";
            }            
            return View();
            
        }
        private List<ExamenDataModel> GetAllQuestionsFromBL(string testmodel)
        {            
            ExamenBL bl = new ExamenBL();
            
            List<ExamenDataModel> dataModels = new List<ExamenDataModel>();
            ExamenDataModel dataModel = null;
            List<ExamenStack> examen = bl.GetAllQuestionsandAnswers(testmodel);
            foreach (ExamenStack item in examen)
            {
                dataModel = new ExamenDataModel();
                dataModel.Sno = item.Sno;
                
                dataModel.Questions = item.Questions;
                dataModel.Option1 = item.Option1;
                dataModel.Option2 = item.Option2;
                dataModel.Option3 = item.Option3;
                dataModel.Option4 = item.Option4;
                dataModel.Answers = item.Answers;
                dataModels.Add(dataModel);
                
            }
            return dataModels;
        }
        [HttpPost]
        public ActionResult TakeTest(string submitbutton,ExamenDataModel dataModel)
        {
            string username = "afr";
            string answer = dataModel.Options;
            try
            {
                int count = 0;                 
                 if (bl.InserttoTempScore(dataModel.Sno, answer))
                 {
                    count = bl.GetCount();
                    if (count > 9)
                    {
                        string testmodel = Session["modl"].ToString();
                        
                        //string candID = bl.GetCandId(username, testmodel);
                        //int mark = bl.Scores();
                        //bl.InsertScores(candID, username, testmodel, mak);
                        //Session["no"] = null;
                        
                        return RedirectToAction("TestFinishView", "HomePageExamen");
                    }
                    else
                    {
                        string testmodel = Session["modl"].ToString();
                        
                        List<ExamenDataModel> examen = GetAllQuestionsFromBL(testmodel);
                        if (answer == examen[count - 1].Answers)
                        {
                            int mark = 1;
                            string candID = bl.GetCandId(username, testmodel);
                            bool reslt = bl.UpdateTempScore(candID, mark);
                            int mak = bl.Scores();
                            
                            Session.Add("no", count);
                            return RedirectToAction("TakeTest", "HomePageExamen");
                        }
                        else
                        {
                            Session.Add("no", count);
                            return RedirectToAction("TakeTest", "HomePageExamen");
                        }                        
                    }                    
                 }                 
            }
            catch (SqlException ex)
            {

                ViewBag.Message = ex.Message;
            }
            return View();            
        }
        
        
        
        [HttpGet]
        public ActionResult ViewProfile(ExamenDataModel dataModel)
        {
            string username = Session["un"].ToString();
            if (bl.CheckUserInScores(username))
            {
                List<ExamenStack> examen = bl.GetScores(username);
                List<ExamenDataModel> data = new List<ExamenDataModel>();
                foreach (ExamenStack item in examen)
                {
                    dataModel = new ExamenDataModel();
                    dataModel.Username = item.Username;
                    dataModel.Candidate_ID = item.Candidate_ID;
                    dataModel.Mark = item.Mark;
                    dataModel.Testmodel = item.Testmodel;
                    data.Add(dataModel);
                }
                var datamodel = new ExamenDataModel();
                datamodel.Examens = new List<ExamenDataModel>();
                datamodel.Examens = data;
                return View(datamodel);
                
            }else 
            {
                ViewBag.Message = "Data not Found!!";
            }
            
           
            return View();
        }
        [HttpGet]
        public ActionResult ExamRegisterResult(ExamenDataModel examen,string submitbutton)
        {
            ViewBag.Profile= Session["un"].ToString();
            if (TempData["fail"]==null)
            {
                ViewBag.Message = "REGISTERED SUCCESSFULLY...";
            }
            else if (TempData["fail"] != null)
            {
                ViewBag.Message = "Registration Failed...";
            }
            
            if (submitbutton=="Back")
            {
                
                return RedirectToAction("Home", "WelcomePage");
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult TestFinishView()
        {
            string username = Session["un"].ToString();
            
            string testmodel = Session["modl"].ToString();
            string candID = bl.GetCandId(username, testmodel);            
            int mark = bl.Scores();
            bool rdt = bl.InsertScores(candID, username, testmodel, mark);
            ViewBag.Scores = bl.Scores();
            Session["no"] = null;
            bool rst = bl.DeleteUserInPaymentList(username, testmodel);
            bool rsult = bl.DeleteTemPCalculateScore();
            bool result = bl.DeleteTempData();
            return View();
        }
        [HttpPost]
        public ActionResult TestFinishView(string submitbutton)
        {
            if (submitbutton=="Finish")
            {
                return RedirectToAction("Home", "WelcomePage");
            }
            return View();
        }
        [HttpGet]
        public ActionResult InstructionPage()
        {         
            return View();
        }
        public ActionResult InstructionPage(string submitbutton)
        {

            string username = Session["un"].ToString();
            if(submitbutton == "Cancel")
            {
                return RedirectToAction("Home", "WelcomePage");
            }
            else if (submitbutton=="APTITUDE MODULE")
            {                
                string module = "Aptitude";
                if (bl.CheckUserInPayment(username,module))
                {
                   // bool rst = bl.DeleteUserInPaymentList(username, module);
                    
                        Session.Add("modl",module);
                        return RedirectToAction("TakeTest", "HomePageExamen");
                    
                    
                }
                else
                {
                    ViewBag.Message = "Sorry You haven't registered for this Module";
                }
                
            }
            else if (submitbutton == "TECHNICAL MODULE")
            {
                string module = "Technical";
                if (bl.CheckUserInPayment(username, module))
                {
                    //bool rst = bl.DeleteUserInPaymentList(username, module);
                    
                        Session.Add("modl", module);
                        return RedirectToAction("TakeTest", "HomePageExamen");
                    
                }
                else
                {
                    ViewBag.Message = "Sorry You haven't registered for this Module";
                }

            }
            
            return View();
        }
        public ActionResult MainView()
        {




            return View("PartialsView");
        }
        [HttpGet]
        public ActionResult PartialsView()
        {




            return PartialView();
        }
        [HttpPost]
        public ActionResult PartialsView(ExamenDataModel ex,string submitbutton)
        {
            if (submitbutton=="Next")
            {
                return RedirectToAction("PartialsView", "HomePageExamen");
            }
            return PartialView();
        }

    }
}