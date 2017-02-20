<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reporte_Pago_rec.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_Reporte_Pago_rec"  Title= "Reporte de Pago Recaudación"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
      <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe de Pagos Recaudación</title>
    <base target="_self" ></base>
    
</head>
<body>
    <form id="form1" runat="server">
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"  ScriptMode="Release" LoadScriptsBeforeUI="false"
     EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>

    
       <table cellpadding="0" cellspacing="0" width="1100" >
       <tr>
       <td class="Cabecera">
       
           <asp:Label ID="Label124" runat="server" CssClass="SubTitulos" 
               Text="Reporte Pagos Recaudación"></asp:Label>
       
       </td>
       </tr>
           <tr>
               <td>
                   <table>
                       <tr>
                           <td>
                               <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Fecha Recaudación"></asp:Label>
                           </td>
                           <td>
                               &nbsp;
                           </td>
                           <td>
                               <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Recaudador Origen"></asp:Label>
                           </td>
                       </tr>
                       <tr>
                           <td align="center">
                               <asp:TextBox ID="Txt_Fec_Rec" runat="server" CssClass="clsMandatorio" 
                                   Width="90px"></asp:TextBox>
                                        
                                   <cc1:CalendarExtender ID="Txt_Fec_Rec_CalendarExtender" runat="server" 
                                   Enabled="True" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Rec" 
                                   CssClass="radcalendar">
                               </cc1:CalendarExtender>
                                        
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="Txt_Fec_Rec"
                                                    Display="None" ErrorMessage="<b>Recaudación</b><br/>Ingrese Fecha a Consultar."
                                                    ValidationGroup="ingreso" Font-Size="8pt"/>
                                                <cc1:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender5" TargetControlID="RequiredFieldValidator4"
                                                    HighlightCssClass="validatorCalloutHighlight" />
                                                    
                               
                           </td>
                           <td>
                               <asp:RadioButtonList ID="rb_hora" runat="server" CssClass="Label" 
                                   RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True" Value="A">AM</asp:ListItem>
                                   <asp:ListItem Value="P">PM</asp:ListItem>
                               </asp:RadioButtonList>
                           </td>
                           <td>
                               <asp:DropDownList ID="Dr_Rec" runat="server" CssClass="clsMandatorio" 
                                   Width="250px">
                               </asp:DropDownList>
                               
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="Dr_Rec"
                                                    Display="None" ErrorMessage="<b>Recaudación</b><br/>Seleccione un Recaudador."
                                                    ValidationGroup="ingreso" Font-Size="8pt"/>
                                                <cc1:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender1" TargetControlID="RequiredFieldValidator1"
                                                    HighlightCssClass="validatorCalloutHighlight" />
                               
                           </td>
                       </tr>
                   </table>
         

    
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="600px" Width="1100px" BorderStyle="None" 
            DocumentMapWidth="100%" ExportContentDisposition="AlwaysAttachment" 
            SizeToReportContent="True" InternalBorderColor="White" 
           ShowDocumentMapButton="False" ShowFindControls="False" 
           ShowRefreshButton="False">
            <LocalReport ReportPath="Modulos\Recaudacion\Reportes\Reporte_Hoja_Recaudación.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
               </td>
           </tr>
           <tr>
               <td align="right">
                   <asp:ImageButton ID="ib_buscar" runat="server" 
                       ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" 
                       ValidationGroup="ingreso" ToolTip="Generar Informe" />
               </td>
           </tr>
       </table>
       
   
    </form>
</body>
</html>
