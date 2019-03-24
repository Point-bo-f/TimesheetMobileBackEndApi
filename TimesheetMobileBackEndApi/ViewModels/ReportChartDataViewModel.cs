using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimesheetMobileBackEndApi.ViewModels
{
    public class ReportChartDataViewModel
    {
        public string[] Labels { get; set; }
        public int[] Counts { get; set; }
    }
}