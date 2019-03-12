using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimesheetMobileBackEndApi.DataAccess;

namespace TimesheetMobileBackEndApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public string[] GetAll()

        {

            string[] employeeNames = null;
            TimesheetMobileEntities entities = new TimesheetMobileEntities();
            try
            {
                employeeNames = (from e in entities.Employees
                                 where (e.Active == true)
                                 select e.FirstName + " " +
                                 e.LastName).ToArray();
            }
            finally
            {
                entities.Dispose();
            }
            return employeeNames;
        }
    }
}
