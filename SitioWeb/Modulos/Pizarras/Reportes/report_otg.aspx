<%@ Page Language="VB" AutoEventWireup="false" CodeFile="report_otg.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_report_otg" %>
   <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <base target="_self"/>
    <title>Reporte de Otorgamiento de Operación</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

       <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" 
           Text="Mostrar detalle Documentos"></asp:Label>
       <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
           CssClass="Label" RepeatDirection="Horizontal" Width="16px">
           <asp:ListItem Selected="True" Value="0">Si</asp:ListItem>
           <asp:ListItem Value="1">No</asp:ListItem>
       </asp:RadioButtonList>
 
    
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="1024px" Width="1280px" BorderStyle="None" 
            DocumentMapWidth="100%" ExportContentDisposition="AlwaysAttachment" 
            SizeToReportContent="True" InternalBorderColor="White" 
           ShowDocumentMapButton="False" ShowFindControls="False" 
           ShowRefreshButton="False">
            <LocalReport ReportPath="Modulos\Operaciones\Reportes\Reportesimu.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    
    </form>
</body>
</html>
