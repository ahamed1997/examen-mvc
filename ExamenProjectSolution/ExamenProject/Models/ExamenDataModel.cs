using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamenUserLibrary;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExamenProject.Models
{
    [MetadataType(typeof(ExamenDataModelMetaData))]
    public class ExamenDataModel: ExamenStack
    {
        string mytestmodel;
        SelectList testmodels;

        [Required(ErrorMessage = "Select Options")]
        public string Options { get; set; }
        public List<ExamenDataModel> Examens { get; set; }
        public SelectList Testmodels { get => testmodels; set => testmodels = value; }
        public string Mytestmodel { get => mytestmodel; set => mytestmodel = value; }
        
    }
    
    public class ExamenDataModelMetaData
    {

        [Required(ErrorMessage = "*")]
        public string Testmodel { get; set; }
        [Required(ErrorMessage = "*")]
        public SelectList Testmodels { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        
        public string Yop { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        
        public string Confirmpassword { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10)]
        [MaxLength(10)]
        public string Phone { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public string Questions { get; set; }
        
        public string Answers { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        
        public string Option4 { get; set; }

        
    }
    
}