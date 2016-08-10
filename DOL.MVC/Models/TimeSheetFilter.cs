using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DotNet.Highcharts;

namespace DOL.MVC.Models
{
  
    
        public class TimesheetFilterModel
        {
            public string TimeSheetNo { get; set; }
            public DateTime? OrderDateFrom { get; set; }
            public DateTime? OrderDateTo{ get; set; }
            public List<ClientOrganizationModels> List_Clients { get; set; }
            public Int32 ClientSelected { get; set; }

            public List<ClientOrganizationModels> List_Others { get; set; }
            public Int32 OtherSelected { get; set; }
        }




}