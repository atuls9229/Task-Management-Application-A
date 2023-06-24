using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_Management_Application_A.Models
{
    public class Task
    {
        [Display(Name = "Task Id")]
        public int TaskUID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Assigne To")]
        public int AssigneeTo { get; set; }
        public int Priority { get; set; }
        public string AssignetoVal { get; set; }
        public string PriorityVal { get; set; }
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public List<SelectListItem> UserList { get; set; }
    }
}