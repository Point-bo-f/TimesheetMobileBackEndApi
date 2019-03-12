using TimesheetMobileBackEndApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimesheetMobileApp.Models;

namespace TimesheetMobileBackEndApi.Controllers
{
    public class WorkAssignmentController : ApiController
    {
        public string[] GetAll()

        {

            string[] assignmentNames = null;
            TimesheetMobileEntities entities = new TimesheetMobileEntities();
            try
            {
                assignmentNames = (from wa in entities.WorkAssignments
                                   where (wa.Active == true)
                                   select wa.Title).ToArray();
            }
            finally
            {
                entities.Dispose();

            }
            return assignmentNames;
        }

        [HttpPost]
        public bool PostStatus(WorkAssignmentOperationModel input)
        {
            TimesheetMobileEntities entities = new TimesheetMobileEntities();


            try
            {
                WorkAssignments assignment = (from wa in entities.WorkAssignments
                                              where (wa.Active == true) &&
                                              (wa.Title == input.AssignmentTitle)
                                              select wa).FirstOrDefault();

                if (assignment == null)
                {
                    return false;
                }
                if (input.Operation == "Start")
                {

                    int assignmentId = assignment.Id_WorkAssignment;

                    Timesheet newEntry = new Timesheet()
                    {
                        Id_WorkAssignment = assignmentId,
                        WorkComplete = false,
                        StartTime = DateTime.Now,
                        Active = true,
                        CreatedAt = DateTime.Now
                    };
                    entities.Timesheet.Add(newEntry);
                   
                }
                else if (input.Operation == "Stop")
                {
                    int assignmentId = assignment.Id_WorkAssignment;
                    Timesheet existing = (from ts in entities.Timesheet
                                          where (ts.Id_WorkAssignment == assignmentId) &&
                                          (ts.Active == true)
                                          select ts).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.StopTime = DateTime.Now;
                        existing.WorkComplete = true;
                        existing.LastModifiedAt = DateTime.Now;
                    }
                    else
                    {
                        return false;
                    }
                }
                entities.SaveChanges();
            }
            catch
            {
                return false;

            }
            finally
            {
                entities.Dispose();

            }
            return true;
        }
    }
}







