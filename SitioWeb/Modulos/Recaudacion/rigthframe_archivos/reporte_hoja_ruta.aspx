<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporte_hoja_ruta.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_reporte_hoja_ruta"  Title="Reporte Hoja de Ruta"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
  <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
       <table cellpadding="0" cellspacing="0" width="750" >
       <%--<tr>
       <td class="Cabecera">
       
           <asp:Label ID="Label124" runat="server" CssClass="SubTitulos" 
               Text="Hoja de Ruta"></asp:Label>
       
       </td>
       </tr>--%>
           <tr>
               <%--<td class="Contenido">--%>
               <td>

         

    
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="600px" Width="750px" BorderStyle="None" 
            DocumentMapWidth="0px" ExportContentDisposition="AlwaysAttachment" 
            SizeToReportContent="True" InternalBorderColor="White" 
           ShowDocumentMapButton="False" ShowFindControls="False" 
           ShowRefreshButton="False" DocumentMapCollapsed="True">
            <LocalReport ReportPath="Modulos\Recaudacion\Reportes\Hoja_Ruta.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
               </td>
           </tr>
           <tr>
               <td align="right">
                   <asp:HiddenField ID="suc2" runat="server" />
                   <asp:HiddenField ID="suc1" runat="server" />
                   <asp:HiddenField ID="eje" runat="server" />
                   <asp:HiddenField ID="am_pm" runat="server" />
                   <asp:HiddenField ID="fecha" runat="server" />
               </td>
           </tr>
       </table>
       
    </form>
</body>
</html>
