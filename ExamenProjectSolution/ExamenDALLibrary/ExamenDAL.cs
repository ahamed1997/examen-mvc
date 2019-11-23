using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ExamenUserLibrary;


namespace ExamenDALLibrary
{
    public class ExamenDAL
    {
        SqlConnection conn;
        SqlCommand cmdRegister, cmdValidation, cmdPayment, cmdGetQuestions, cmdInsertScores,cmdGetScores,cmdGetAllTestModels,cmdCheckPayment,cmdCheckUserInPayment,cmdInsertoTempScore,cmdGetCount,cmdTruncate,cmdDeleteUser,cmdGetCandidateId,cmdUpdateScore,cmdScores,cmdCheckUserInScore,cmdDeletetTempCalScr;
        public ExamenDAL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connExamen"].ConnectionString);
        }
        public bool InsertCandidate(ExamenStack examen)
        {
            conn.Open();
            
            bool insertresult = false;
            cmdRegister = new SqlCommand("proc_RegisterCandidates", conn);
            
            cmdRegister.Parameters.AddWithValue("@p_phone", examen.Phone);
            cmdRegister.Parameters.AddWithValue("@p_email", examen.Email);
            cmdRegister.Parameters.AddWithValue("@p_username", examen.Username);
            cmdRegister.Parameters.AddWithValue("@p_password", examen.Password);
            
            cmdRegister.Parameters.AddWithValue("@p_name", examen.Name);
            cmdRegister.CommandType = CommandType.StoredProcedure;
            if (cmdRegister.ExecuteNonQuery()>0)
            {
                insertresult = true;
            }

            conn.Close();
            return insertresult;
        }
        public bool Validation(string username, string password)
        {
            conn.Open();
            bool validresult = false;
            cmdValidation = new SqlCommand("proc_UserValidation", conn);
            cmdValidation.Parameters.AddWithValue("@p_Username", username);
            cmdValidation.Parameters.AddWithValue("@p_password", password);
            cmdValidation.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmdValidation.ExecuteReader();
            if (dataReader.HasRows == true)
            {
                validresult = true;
            }
            conn.Close();

            return validresult;
        }
        public bool UpdatePayment(string username, string testmodel,  string candidate_Id,string payment)
        {
            bool paymentresult = false;
            conn.Open();
            cmdPayment = new SqlCommand("proc_Payment", conn);
            cmdPayment.Parameters.AddWithValue("@p_CandidateId",candidate_Id);
            cmdPayment.Parameters.AddWithValue("@p_Username", username);
            cmdPayment.Parameters.AddWithValue("@p_TestModel", testmodel);
            cmdPayment.Parameters.AddWithValue("@p_Payment", payment);
            cmdPayment.CommandType = CommandType.StoredProcedure;
            if (cmdPayment.ExecuteNonQuery() > 0)
            {
                paymentresult = true;
            }

            conn.Close();
            return paymentresult;
        }
        public List<ExamenStack> GetAllQuestions(string testmodel)
        {
            conn.Open();
            List<ExamenStack> examens = new List<ExamenStack>();
            cmdGetQuestions = new SqlCommand("proc_GetAllQuestions",conn);
            cmdGetQuestions.Parameters.Add("@TestModel", SqlDbType.VarChar, 50);
            cmdGetQuestions.CommandType = CommandType.StoredProcedure;            
            cmdGetQuestions.Parameters[0].Value = testmodel;
            
            
            ExamenStack examen;
            SqlDataReader dataReader = cmdGetQuestions.ExecuteReader();
            while (dataReader.Read())
            {
                examen = new ExamenStack();
                examen.Sno = Convert.ToInt32(dataReader[0]);
                examen.Testmodel = dataReader[1].ToString();
                examen.Questions = dataReader[2].ToString();
                examen.Answers = dataReader[3].ToString();
                examen.Option1 = (dataReader[4].ToString());
                examen.Option3 = (dataReader[5].ToString());
                examen.Option2 = (dataReader[6].ToString());
                examen.Option4 = (dataReader[7].ToString());
                examen.Mark = Convert.ToInt32(dataReader[8]);
                examens.Add(examen);
            }
            conn.Close();
            return examens;
        }
        public bool InsertIntoScores(string candidateid, string username, string testmodel, int mark)
        {
            bool result = false;
            conn.Open();
            cmdInsertScores = new SqlCommand("proc_InsertIntoScores",conn);
            cmdInsertScores.Parameters.AddWithValue("@CandidateId", candidateid);
            cmdInsertScores.Parameters.AddWithValue("@Username", username);
            cmdInsertScores.Parameters.AddWithValue("@TestModel", testmodel);
            cmdInsertScores.Parameters.AddWithValue("@Mark", mark);
            cmdInsertScores.CommandType = CommandType.StoredProcedure;
            if (cmdInsertScores.ExecuteNonQuery() > 0)
            {
                result = true;
            }
            conn.Close();
            return result;
        }
        public List<ExamenStack> GetScores(string Username)
        {
            conn.Open();
            List<ExamenStack> examens = new List<ExamenStack>();
            cmdGetScores = new SqlCommand("proc_GetAllScores", conn);
            cmdGetScores.Parameters.Add("@Username", SqlDbType.VarChar, 50);
            cmdGetScores.Parameters.Add("@CandidateId", SqlDbType.VarChar, 20);
            cmdGetScores.Parameters.Add("@TestModel", SqlDbType.VarChar, 50);
            cmdGetScores.Parameters.Add("@Mark", SqlDbType.Int);
            cmdGetScores.CommandType = CommandType.StoredProcedure;
            
            cmdGetScores.Parameters[0].Value = Username;
            cmdGetScores.Parameters[1].Direction = ParameterDirection.Output;
            cmdGetScores.Parameters[2].Direction = ParameterDirection.Output;
            cmdGetScores.Parameters[3].Direction = ParameterDirection.Output;
            ExamenStack examen = new ExamenStack();
            cmdGetScores.ExecuteNonQuery();
            examen.Candidate_ID = cmdGetScores.Parameters[1].Value.ToString();
            examen.Testmodel = cmdGetScores.Parameters[2].Value.ToString();
            examen.Mark = Convert.ToInt32(cmdGetScores.Parameters[3].Value);
            examens.Add(examen);
            conn.Close();
            return examens;
        }
        public List<string> GetAllTestModels()
        {
            conn.Open();
            List<string> examens = new List<string>();
            cmdGetAllTestModels = new SqlCommand("proc_GetAllTestModels", conn);            
            cmdGetAllTestModels.CommandType = CommandType.StoredProcedure;
            cmdGetAllTestModels.ExecuteNonQuery();
            ExamenStack examen;
            SqlDataReader dataReader = cmdGetAllTestModels.ExecuteReader();
            while (dataReader.Read())
            {
                examen = new ExamenStack();
                string Testmodel = dataReader[0].ToString();
                examens.Add(Testmodel);
            }
            conn.Close();
            return examens;
        }
        public bool CheckPayment(string username, string candidate_Id)
        {
            bool result = false;
            conn.Open();
            cmdCheckPayment = new SqlCommand("proc_CheckPayment", conn);
            cmdCheckPayment.Parameters.AddWithValue("@username", username);
            cmdCheckPayment.Parameters.AddWithValue("@candidate_id", candidate_Id);
            cmdCheckPayment.CommandType = CommandType.StoredProcedure;
            SqlDataReader datareader = cmdCheckPayment.ExecuteReader();
            if(datareader.HasRows==true)
            {
                result = true;
            }

            conn.Close();
            return result;
        }
        public bool CheckUserInPayment(string username, string testmodel)
        {
            bool result = false;
             
            conn.Open();
            cmdCheckUserInPayment = new SqlCommand("proc_CheckUserInPaymentTable", conn);
            cmdCheckUserInPayment.Parameters.AddWithValue("@username", username);
            cmdCheckUserInPayment.Parameters.AddWithValue("@testmodel", testmodel);
            cmdCheckUserInPayment.CommandType = CommandType.StoredProcedure;
            SqlDataReader datareader = cmdCheckUserInPayment.ExecuteReader();
            if (datareader.HasRows == true)
            {
                result = true;
            }
            conn.Close();

            return result;
        }
        public bool InserttoTempScore(int sno, string options)
        {
            bool result = false;
            conn.Open();            
            cmdInsertoTempScore = new SqlCommand("proc_InsertTempScore", conn);            
            cmdInsertoTempScore.Parameters.AddWithValue("@sno", sno);
            cmdInsertoTempScore.Parameters.AddWithValue("@options", options);           
            cmdInsertoTempScore.CommandType = CommandType.StoredProcedure;
            if (cmdInsertoTempScore.ExecuteNonQuery() > 0)
            {
                result = true;
            }
            conn.Close();
            return result;

        }
        public bool UpdateTempScore(string candidate, int mark)
        {
            

            bool result = false;
            conn.Open();
            cmdUpdateScore = new SqlCommand("proc_UpdateTempScore", conn);
            cmdUpdateScore.Parameters.AddWithValue("@candidateid", candidate);
            cmdUpdateScore.Parameters.AddWithValue("@mark", mark);
            cmdUpdateScore.CommandType = CommandType.StoredProcedure;
            if (cmdUpdateScore.ExecuteNonQuery() > 0)
            {
                result = true;
            }
            conn.Close();
            return result;
        }
        public int GetCount()
        {
            int count = 0;
            conn.Open();
            cmdGetCount = new SqlCommand("procSelectCount", conn);
            cmdGetCount.CommandType = CommandType.StoredProcedure;
            cmdGetCount.ExecuteNonQuery();
            SqlDataReader reader = cmdGetCount.ExecuteReader();
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            conn.Close();
            return count;
        }
        public bool DeleteTempScores()
        {
            bool result = false;
            conn.Open();
            cmdTruncate = new SqlCommand("proc_TruncateScores", conn);
            cmdTruncate.CommandType = CommandType.StoredProcedure;
            if(cmdTruncate.ExecuteNonQuery()>0)
            {
                result = true;
            }

            conn.Close();
            return result;
        }
        public bool DeleteUserInPaymentList( string username,string testmodel)
        {
            
            bool result=false;
            conn.Open();
            cmdDeleteUser = new SqlCommand("proc_DeleteUser", conn);
            cmdDeleteUser.Parameters.AddWithValue("@Username", username);
            cmdDeleteUser.Parameters.AddWithValue("@testmodel", testmodel);
            cmdDeleteUser.CommandType = CommandType.StoredProcedure;
            if (cmdDeleteUser.ExecuteNonQuery() > 0)
            {
                result = true;
            }
            conn.Close();
            return result;
        }
        public string GetCandidateId(string username,string testmodel)
        {
            
            string cand = null;
            conn.Open();
            cmdGetCandidateId = new SqlCommand("proc_GetCandID", conn);
            cmdGetCandidateId.Parameters.Add("@username", SqlDbType.VarChar, 50);
            cmdGetCandidateId.Parameters.Add("@testmodel", SqlDbType.VarChar, 50);            
            cmdGetCandidateId.CommandType = CommandType.StoredProcedure;            
            cmdGetCandidateId.Parameters[0].Value = username;
            cmdGetCandidateId.Parameters[1].Value = testmodel;
            cmdGetCandidateId.ExecuteNonQuery();
            SqlDataReader reader = cmdGetCandidateId.ExecuteReader();
            while (reader.Read())
            {
                cand = reader[0].ToString();
            }
            conn.Close();
            return cand;
        }
        
        public int Scores()
        {
        
            int score = 0;
            conn.Open();
            cmdScores = new SqlCommand("proc_Calculate", conn);
            cmdScores.CommandType = CommandType.StoredProcedure;
            cmdScores.ExecuteNonQuery();
            SqlDataReader reader = cmdScores.ExecuteReader();
            while (reader.Read())
            {
                score = Convert.ToInt32(reader[0]);
            }
            
            conn.Close();
            return score;
        }
        public bool CheckUserInScore(string username)
        {
            
            bool result=false;
            conn.Open();
            cmdCheckUserInScore = new SqlCommand("proc_CheckUserinScores", conn);
            cmdCheckUserInScore.Parameters.AddWithValue("@username", username);
            cmdCheckUserInScore.CommandType = CommandType.StoredProcedure;
            SqlDataReader datareader = cmdCheckUserInScore.ExecuteReader();
            if (datareader.HasRows == true)
            {
                result = true;
            }
            conn.Close();
            return result;

        }
        public bool DeleteTemPCalculateScore()
        {
            bool result = false;
            conn.Open();
            cmdDeletetTempCalScr = new SqlCommand("proc_TruncateScoreCalculate", conn);
            cmdDeletetTempCalScr.CommandType = CommandType.StoredProcedure;
            if (cmdDeletetTempCalScr.ExecuteNonQuery() > 0)
            {
                result = true;
            }

            conn.Close();
            return result;

        }
    }
}
