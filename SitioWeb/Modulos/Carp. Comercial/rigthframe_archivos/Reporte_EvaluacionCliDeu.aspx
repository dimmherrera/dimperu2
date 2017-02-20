<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reporte_EvaluacionCliDeu.aspx.vb" Inherits="Reporte_EvaluacionCliDeu" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Evaluación Cliente Deudor</title>
    <base target=_self></base>
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:ScriptManager--%>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="850px" 
        Width="1150px">
    </rsweb:ReportViewer>
    </form>
</body>
</html>
