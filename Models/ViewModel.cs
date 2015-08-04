using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManApp.Models
{
    public class TaskVM
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Please input the Task Name.")]
        [Display(Name = "Task Name"), StringLength(100)]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Please select the Start Date.")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please select the Finish Date.")]
        [Display(Name = "Finish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FinishDate { get; set; }

        public int TaskListID { get; set; }

        [ForeignKey("TaskListID")]
        public virtual TaskList TaskList { get; set; }
    }
}