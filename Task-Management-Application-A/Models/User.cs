using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task_Management_Application_A.Models
{
    public class User
    {
        [Display(Name = "User Id")]
        public int UserUID { get; set; }
        public string Name { get; set; }
        [Display(Name = "Email Id")]
        public string EmailID { get; set; }
        public string Address { get; set; }
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}