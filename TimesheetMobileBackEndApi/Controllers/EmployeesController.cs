using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetMobileBackEndApi.DataAccess;
using TimesheetMobileBackEndApi.ViewModels;

namespace TimesheetMobileBackEndApi.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        private TimesheetMobileEntities db = new TimesheetMobileEntities();

        // GET: Employees
        public ActionResult Index()
        {
            List<EmployeesViewModel> model = new List<EmployeesViewModel>();

            TimesheetMobileEntities entities = new TimesheetMobileEntities();

            try
            {
                List<Employees> employees = entities.Employees.OrderBy(Employees => Employees.LastName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Employees employee in employees)
                {
                    EmployeesViewModel view = new EmployeesViewModel();
                    view.Id_Employee = employee.Id_Employee;
                    view.FirstName = employee.FirstName;
                    view.LastName = employee.LastName;
                    view.PhoneNumber = employee.PhoneNumber;
                    view.EmailAddress = employee.EmailAddress;
                    view.DeletedAt = employee.DeletedAt;
                    view.Active = employee.Active;




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