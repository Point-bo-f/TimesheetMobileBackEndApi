using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimesheetMobileBackEndApi.ViewModels
{
    public class HoursPerWorkAssignmentModel
    {
        public int Id_WorkAssignment { get; set; }
        public string WorkAssignmentName { get; set; }
        public double TotalHours { get; set; }

        public bool WorkComplete { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }

        public string Comments { get; set; }
     
    }
}