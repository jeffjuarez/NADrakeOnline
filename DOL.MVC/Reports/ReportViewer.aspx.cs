using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace DOL.MVC.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["RDLC_Dataset_Name"].ToString() != string.Empty)
                { 
                var reportDataSource = new ReportDataSource
                {
                    // Must match the DataSource in the RDLC

                    Name = Session["RDLC_Dataset_Name"].ToString(),
                    Value = Session["ReportData"]
                };

                ReportViewer1.LocalReport.ReportPath = Session["ReportPath"].ToString();

                ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
                ReportViewer1.DataBind();
            }

            }

        }
    }
}