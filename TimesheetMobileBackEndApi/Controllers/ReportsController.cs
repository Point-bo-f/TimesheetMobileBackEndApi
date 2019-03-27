using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TimesheetMobileBackEndApi.DataAccess;
using TimesheetMobileBackEndApi.ViewModels;



namespace TimesheetMobileBackEndApi.Controllers
{
    


    public class ReportsController : Controller
    {
        
        // GET: Reports
        public ActionResult HoursPerWorkAssignment()
        {
            TimesheetMobileEntities entities = new TimesheetMobileEntities();

            try
            {
                DateTime today = DateTime.Today.AddDays(0);
                DateTime tomorrow = today.AddDays(1);
               
                //haetaan kaikki kuluvan päivän tuntikirjaukset

                List<Timesheet> allTimesheetsToday = (from ts in entities.Timesheet
                                                      where (ts.StartTime > today) &&
                                                      (ts.StartTime < tomorrow) &&
                                                      (ts.WorkComplete == true)
                                                       select ts).ToList();

                //ryhmitellään kirjaukset tehtävittäin ja lasketaan kestot
                List<HoursPerWorkAssignmentModel> model = new List<HoursPerWorkAssignmentModel>();

                foreach (Timesheet timesheet in allTimesheetsToday)
                {
                    int assignmentId = timesheet.Id_WorkAssignment.Value;
                    HoursPerWorkAssignmentModel existing = model.Where(
                        m => m.Id_WorkAssignment == assignmentId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.TotalHours += (timesheet.StopTime.Value - timesheet.StartTime.Value).TotalHours;
                    }
                    else
                    {
                        //Timesheet timesheet = timesheet;
                        existing = new HoursPerWorkAssignmentModel()
                        {
                            Id_WorkAssignment = assignmentId,
                            WorkAssignmentName = timesheet.WorkAssignments.Title,
                            TotalHours = (timesheet.StopTime.Value - timesheet.StartTime.Value).TotalHours,
                            WorkComplete = timesheet.WorkComplete,
                            StartTime = timesheet.StartTime.Value,
                            StopTime = timesheet.StopTime.Value,
                            Comments = timesheet.Comments
                        };
                        model.Add(existing);
                    }
                }

                return View(model);
            }
            finally
            {
                entities.Dispose();
            }
        }
        public ActionResult HoursPerWorkAssignmentAsExcel()
        {
            // TODO: hae tiedot tietokannasta!
            //Muododostetaan data stingbuilderillä:
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto 
            csv.AppendLine("Matti;123,5");
            csv.AppendLine("Jesse;86,25");
            csv.AppendLine("Kaisa;99,00");

            // palautetaan CSV-tiedot selaimelle
            //Muutetaan string-builder tavutaulukoksi, jossa käytetään UTF8-merkistöä:
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString()); //merkkijono
                                                                    //File-rutiini, jossa käytetään tavutaulukkoa ja palautetaan MVC-controllerista tiedosto:
            return File(buffer, "text/csv", "Työtunnit.csv"); //MIME csv -tietotyyppi, tiedoston nimi
        }

        
        public ActionResult HoursPerWorkAssignmentAsExcel2()


        {
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto
            TimesheetMobileEntities entities = new TimesheetMobileEntities();
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(30);
                
                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Timesheet> allTimesheetsToday = (from ts in entities.Timesheet
                                                      where (ts.StartTime > today) &&
                                                      (ts.StartTime < tomorrow) &&
                                                      (ts.WorkComplete == true)
                                                      select ts).ToList();



                foreach (Timesheet timesheet in allTimesheetsToday)
                {
                    csv.AppendLine(timesheet.Id_Employee + ";" +
                        timesheet.StartTime + ";" + timesheet.StopTime + ";" + timesheet.Comments + ";");
                }
            }
            finally
            {
                entities.Dispose();
            }
            //Palautetaan CSV-tiedot selaimelle
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "Työtunnit2.csv");
        }
        
        [HttpPost]
        public ActionResult HoursPerWorkAssignmentAsExcel3(DateTime dateFrom, DateTime dateTo)
        {
            HoursPerWorkAssignmentModel dates = new HoursPerWorkAssignmentModel();
            dates.dateFrom = dateFrom;
            dates.dateTo = dateTo;
            
        
                 

     
        StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto
            TimesheetMobileEntities entities = new TimesheetMobileEntities();
            try
            {


                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Timesheet> allTimesheetsTime = (from ts in entities.Timesheet
                                                     where (ts.StartTime > dateFrom) &&
                                                     (ts.StartTime < dateTo) &&
                                                     (ts.WorkComplete == true)
                                                     select ts).ToList();


                {
                    
                    {
                        csv.AppendLine("Id_WorkAssignment" + ";" + "Id_Employee" + ";" + "StartTime" + ";" + "StopTime" + ";" + "TotalHours" + ";" + "Comments" + ";");


                        foreach (Timesheet timesheet in allTimesheetsTime)
                        {

                            csv.AppendLine(timesheet.Id_WorkAssignment + ";" + timesheet.Id_Employee + ";" +
                                timesheet.StartTime + ";" + timesheet.StopTime + ";" + (timesheet.StopTime.Value - timesheet.StartTime.Value).TotalHours + ";" + timesheet.Comments + ";");
                        }
                    }
                }
            }
            finally
            {
                entities.Dispose();
            }
            //Palautetaan CSV-tiedot selaimelle
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "Työtunnit3.csv");
        }

        public ActionResult GetTimesheetCounts(string onlyComplete)
        {
            ReportChartDataViewModel model = new ReportChartDataViewModel();

            TimesheetMobileEntities entities = new TimesheetMobileEntities();
            try
            {
                model.Labels = (from wa in entities.WorkAssignments
                                orderby wa.Id_WorkAssignment
                                select wa.Title).ToArray();

                if (onlyComplete == "1")
                {
                    model.Counts = (from ts in entities.Timesheet
                                    where (ts.WorkComplete == true)
                                    orderby ts.Id_WorkAssignment
                                    group ts by ts.Id_WorkAssignment into grp
                                    select grp.Count()).ToArray();

                }
                else
                {
                    model.Counts = (from ts in entities.Timesheet                                    
                                    orderby ts.Id_WorkAssignment
                                    group ts by ts.Id_WorkAssignment into grp
                                    select grp.Count()).ToArray();
                }
            }
            finally
            {
                entities.Dispose();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
    }

