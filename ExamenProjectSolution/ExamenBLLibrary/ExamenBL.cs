using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenDALLibrary;
using ExamenUserLibrary;

namespace ExamenBLLibrary
{
    public class ExamenBL
    {
        ExamenDAL dal;
        string candidate_Id;
        public ExamenBL()
        {
            dal = new ExamenDAL();
        }
        public bool InsertIntoCandidates(ExamenStack examen)
        {
            return dal.InsertCandidate(examen);
        }
        public List<ExamenStack> GetAllQuestionsandAnswers(string testmodel)
        {
            return dal.GetAllQuestions(testmodel);
        }
        public bool UserValidation(string username, string password)
        {
            return dal.Validation(username, password);
        }
        public string Payment(string username, string testmodel)
        {
            
            string num = "123456789";
            int len = num.Length;
            string otp = string.Empty;
            int otpdigit = 5;
            string finaldigit;
            int getindex;
            for (int i = 0; i < otpdigit; i++)
            {
                do
                {
                    getindex = new Random().Next(0, len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                } while (otp.IndexOf(finaldigit)!=-1);
                otp += finaldigit;
            }
            candidate_Id = otp;
            string payment = "1";
            if(dal.UpdatePayment(username,testmodel,candidate_Id,payment))
            {
                return candidate_Id;
            }
            else
            {
                return candidate_Id = "";
            }
            
        }
        public bool InsertScores(string CandidateId, string username, string testmodel, int mark)
        {
            return dal.InsertIntoScores(CandidateId,username,testmodel,mark);
        }
        public List<ExamenStack> GetScores(string username)
        {
            return dal.GetScores(username);
        }
        public List<string> GetAllTestModels()
        {
            return dal.GetAllTestModels();
        }
        public bool CheckPayment(string username, string candidate_id)
        {
            return dal.CheckPayment(username, candidate_id);
        }
        public bool CheckUserInPayment(string username, string testmodel)
        {
            return dal.CheckUserInPayment(username, testmodel);
        }
        public bool InserttoTempScore(int sno, string options)
        {
            return dal.InserttoTempScore(sno, options);
        }
        public int GetCount()
        {
            return dal.GetCount();
        }
        public bool DeleteTempData()
        {
            return dal.DeleteTempScores();
        }
        public bool DeleteUserInPaymentList(string username,string testmodel)
        {
            return dal.DeleteUserInPaymentList(username,testmodel);
        }
        public string GetCandId(string username,string testmodel)
        {
            return dal.GetCandidateId(username,testmodel);
        }
        public bool UpdateTempScore(string candidate, int mark)
        {
            return dal.UpdateTempScore(candidate, mark);
        }
        public int Scores()
        {
            return dal.Scores();
        }
        public bool CheckUserInScores(string username)
        {
            return dal.CheckUserInScore(username);
        }
        public bool DeleteTemPCalculateScore()
        {
            return dal.DeleteTemPCalculateScore();
        }
    }
}

