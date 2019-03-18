using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetMobileBackEndApi.DataAccess;
using TimesheetMobileBackEndApi.ViewModels;

namespace TimesheetMobileBackEndApi.Controllers
{
    public class TimesheetsController : Controller
    {
        // GET: Timesheets
        public ActionResult Index()
        {
            List<TimesheetsViewModel> model = new List<TimesheetsViewModel>();

            TimesheetMobileEntities entities = new TimesheetMobileEntities();

            try
            {
                List<Timesheet> timesheets = entities.Timesheet.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Timesheet timesheet in timesheets)
                {
                    TimesheetsViewModel view = new TimesheetsViewModel();
                    view.Id_Timesheet = timesheet.Id_Timesheet;
                    //view.Id_Customer = timesheet.Customers.Id_Customer;
                    //view.Id_Contractor = timesheet.Contractors.Id_Contractor;
                    //view.Id_Employee = timesheet.Id_Employee;
                    view.Id_WorkAssignment = timesheet.WorkAssignments.Id_WorkAssignment;
                    view.StartTime = timesheet.StartTime.GetValueOrDefault();
                    view.StopTime = timesheet.StopTime.GetValueOrDefault();
                    view.Comments = timesheet.Comments;
                    view.WorkComplete = timesheet.WorkComplete;
                    view.CreatedAt = timesheet.CreatedAt.GetValueOrDefault();
                    view.LastModifiedAt = timesheet.LastModifiedAt.GetValueOrDefault();
                    view.DeletedAt_ = timesheet.DeletedAt;
                    view.Active = timesheet.Active;

                    
                    view.FirstName = timesheet.Employees?.FirstName;
                    view.LastName = timesheet.Employees?.LastName;




                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }



            return View(model);
        }
    }
}