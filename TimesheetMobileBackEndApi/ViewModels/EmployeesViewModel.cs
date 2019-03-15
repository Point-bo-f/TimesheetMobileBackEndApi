using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimesheetMobileBackEndApi.DataAccess;

namespace TimesheetMobileBackEndApi.ViewModels
{
    public class EmployeesViewModel
    {
        public int Id_Employee { get; set; }
        public int? Id_Contractor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeReferences { get; set; }
        public string CreatedAt { get; set; }
        public string LastModified { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }
        

    }
}