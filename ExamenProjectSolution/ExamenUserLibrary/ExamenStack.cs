using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace ExamenUserLibrary
{
    
    public class ExamenStack 
    {
        string name,degree,yop,phone,email,username,password,confirmpassword;
        string candidate_ID, testmodel, payment;
        string testmode, questions, answers;
        string options,option1, option2, option3, option4;
        int mark,sno;

        [Required(ErrorMessage = "*")]
        public string Phone { get => phone; set => phone = value; }
        [Required(ErrorMessage = "*")]
        public string Email { get => email; set => email = value; }
        [Required(ErrorMessage = "*")]

        public string Username { get => username; set => username = value; }
        [Required(ErrorMessage = "*")]

        public string Password { get => password; set => password = value; }
        public string Candidate_ID { get => candidate_ID; set => candidate_ID = value; }
        [Required(ErrorMessage ="*")]
        public string Testmodel { get => testmodel; set => testmodel = value; }
        public string Payment { get => payment; set => payment = value; }
        public string Testmode { get => testmode; set => testmode = value; }
        public string Questions { get => questions; set => questions = value; }
        public string Answers { get => answers; set => answers = value; }
        public string Option1 { get => option1; set => option1 = value; }
        public string Option2 { get => option2; set => option2 = value; }
        public string Option3 { get => option3; set => option3 = value; }
        public string Option4 { get => option4; set => option4 = value; }
        public int Mark { get => mark; set => mark = value; }
        [Required]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match")]
        public string Confirmpassword { get => confirmpassword; set => confirmpassword = value; }
        [Required]
        public string Yop { get => yop; set => yop = value; }
        [Required]
        public string Degree { get => degree; set => degree = value; }
        [Required]
        public string Name { get => name; set => name = value; }
        [Required]
        public int Sno { get => sno; set => sno = value; }
        public string Options { get => options; set => options = value; }
    }
    
}
