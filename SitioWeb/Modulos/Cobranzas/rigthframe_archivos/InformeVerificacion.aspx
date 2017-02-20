<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeVerificacion.aspx.vb" Inherits="Modulos_Cobranzas_Reportes_InformeVerificacion" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    
   
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" BorderStyle="None" 
        DocumentMapWidth="100%" ExportContentDisposition="AlwaysAttachment" 
        Font-Names="Verdana" Font-Size="8pt" Height="1024px" 
        InternalBorderColor="White" ShowDocumentMapButton="False" 
        ShowFindControls="False" ShowRefreshButton="False" SizeToReportContent="True" 
        Width="1280px">
        <LocalReport ReportPath="Modulos\Cobranzas\Reportes\ReporteVerificacion.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
    
   
    </form>
</body>
</html>
