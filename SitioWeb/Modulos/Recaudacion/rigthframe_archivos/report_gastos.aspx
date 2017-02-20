<%@ Page Language="VB" AutoEventWireup="false" CodeFile="report_gastos.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_report_gastos" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Gastos de Recaudación</title>
</head>
<body>
    <form id="form1" runat="server">
  

    
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="800px" Width="740px" BorderStyle="None" 
            DocumentMapWidth="100%" ExportContentDisposition="AlwaysAttachment" 
            SizeToReportContent="True" InternalBorderColor="White" 
           ShowDocumentMapButton="False" ShowFindControls="False" 
           ShowRefreshButton="False">
            <LocalReport ReportPath="Modulos\Recaudacion\Reportes\Reporte_Hoja_Recaudación.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
               <asp:HiddenField ID="hf_nro_hoja" runat="server" />
  
    </form>
</body>
</html>
