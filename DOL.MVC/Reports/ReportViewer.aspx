<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="DOL.MVC.Reports.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000" Height="800" AsyncRendering ="true" ShowExportControls="true">
      <%--  <LocalReport ReportPath="Reports\rptUsers.rdlc">
        </LocalReport>--%>
    </rsweb:ReportViewer>
    <asp:ScriptManager runat="server" ID="SillyPrerequisite"></asp:ScriptManager>


    </div>
    </form>
</body>
</html>
