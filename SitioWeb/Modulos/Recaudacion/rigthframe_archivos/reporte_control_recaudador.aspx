<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporte_control_recaudador.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_reporte_control_recaudador" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Control Recaudador</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
        <base target="_self"></base>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">
    </asp:ScriptManager>
    <table cellspacing="0" >
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label8" runat="server" CssClass="Titulos" 
                    Text="Control de Recaudación"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" valign="top">
               
                <table>
                    <tr>
                        <td valign="top">
                            <table cellspacing="0" width="370">
                                <tr>
                                    <td class="Cabecera">
                            <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="Tipo de Busqueda"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height: 32px">
                <asp:RadioButtonList ID="rb_criterio" runat="server" CssClass="Label" 
                    RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="A">Por Año</asp:ListItem>
                    <asp:ListItem Value="M">Por Mes</asp:ListItem>
                    <asp:ListItem Value="S">Por Semana</asp:ListItem>
                </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                <table  runat="server" id="mes_año" cellspacing="0" width="370">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" 
                                Text="Rango de Fechas Mensual y Anual"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Mes"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dr_mes" runat="server" CssClass="clsMandatorio" 
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_año" runat="server" Width="90px" 
                                            CssClass="clsMandatorio" MaxLength="4"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txt_año_FilteredTextBoxExtender" 
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_año">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                <table runat="server" id="SEMANA" cellspacing="0" width="370">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" 
                                Text="Rango Fechas Semanal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fec_des" runat="server" CssClass="clsMandatorio" 
                                            Width="90px" AutoPostBack="True" CausesValidation="True"></asp:TextBox>
                                                                                                    <cc1:MaskedEditExtender ID="txt_fec_des_MaskedEditExtender" runat="server" 
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec_des">
                                        </cc1:MaskedEditExtender>
                                                  
                                        <cc1:CalendarExtender ID="txt_fec_des_CalendarExtender" runat="server" 
                                            CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                            TargetControlID="txt_fec_des" FirstDayOfWeek="Monday">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fec_has" runat="server" Width="90px" 
                                            CssClass="clsDisabled" CausesValidation="True"></asp:TextBox>
                                                                                                    <cc1:MaskedEditExtender ID="txt_fec_has_MaskedEditExtender" runat="server" 
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec_has">
                                        </cc1:MaskedEditExtender>
                                            
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    
                </table>
                            
                            </ContentTemplate>
                            </asp:UpdatePanel>
                
                        </td>
                        <td valign="bottom">
                            <asp:ImageButton ID="btn_buscar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                  onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                  onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" 
                                ValidationGroup="Busca" ToolTip="Generar Informe" />
                        </td>
                    </tr>
                    
                 
                </table>
            </td>
        </tr>
        <tr>
            <td>
               <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="814px" Width="1003px">
    </rsweb:ReportViewer>
 </td>
        </tr>
        <tr>
            <td>
    
                <uc1:Mensaje ID="Mensaje1" runat="server" />
    
            </td>
        </tr>
    </table>
 
    </form>
</body>
</html>
