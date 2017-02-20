<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="RadicarFact.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_RadicarFact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Tabla_general" border="0" cellpadding="0" cellspacing="1" width="100%"
                class="Contenido">
                <tr>
                    <td class="Cabecera" height="31">
                        <asp:Label ID="Label1" runat="server" CssClass="Titulos" Text="Cobranza - Radicación Facturas"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="height: 590px; padding: 5px" valign="top" align="center">
                        <table id="Contenedora" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center">
                                    <table id="Datos Fecha" border="0" cellpadding="0" cellspacing="0" width="400px">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Fecha de Generacion"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="center">
                                                <table id="Controles" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Fecha"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_feg" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txt_Feg_CalendarExtender" runat="server" Enabled="true"
                                                                FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_feg" CssClass="radcalendar">
                                                            </cc1:CalendarExtender>
                                                            <cc1:MaskedEditExtender ID="txt_Feg_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDecimalPlaceholder=""
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="true" Mask="99/99/9999"
                                                                TargetControlID="Txt_feg" MaskType="Date" AcceptAMPM="true" AutoComplete="false"
                                                                ErrorTooltipEnabled="true">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                   <td align="right">
                        <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprimir" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            Style="position: static" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            Style="position: static" />
                   </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
          <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers> 
    </asp:UpdatePanel>
</asp:Content>
