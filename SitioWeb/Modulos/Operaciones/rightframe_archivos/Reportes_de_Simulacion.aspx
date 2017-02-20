<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reportes_de_Simulacion.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Reportes_de_Simulacion" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title></title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div style="width:100%;text-align:center">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Informe" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DP_Informes" runat="server" CssClass="clsMandatorio" AutoPostBack="true">
                    <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Solicitud de Descuento de Facturas" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Anexo de Endoso" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Carta de Notificación" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Aceptación de Endoso y Compromiso de Pago" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" BorderStyle="None" 
            DocumentMapWidth="100%" ExportContentDisposition="AlwaysAttachment" 
            SizeToReportContent="True" InternalBorderColor="White" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowRefreshButton="False" Height="700px" Width="800px">
           
        </rsweb:ReportViewer>
        
    </div>
    </form>
</body>
</html>
