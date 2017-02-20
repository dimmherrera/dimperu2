<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Gestion.aspx.vb" Inherits="Modulos_Gestion_rigthframe_archivos_Gestion" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="1" class="Contenido">
                <tr>
                    <td style="height: 31px" valign="middle" align="center" class="Cabecera">
                        <asp:Label ID="Label25353" runat="server" CssClass="Titulos" 
                            Text="Gestión - Informes de Gestión"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="590" class="Contenido" valign="top" style="padding: 5px" align="center">
                        <table cellpadding="0" cellspacing="0" style="text-align:-moz-center">
                            <tr>
                                <td align="center">
                                
                                    <table id="Info" runat="server" style="text-align:-moz-center" cellpadding="0" 
                                        cellspacing="0">
                                         <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label25392" runat="server" CssClass="SubTitulos" 
                                                    Text="Criterios Informe"></asp:Label>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top" align="center">
                                                <table >
                                                    <tr>
                                                        <td valign="top" style="width: 311px">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera" style="width: 300px">
                                                                        <asp:Label ID="Label25390" runat="server" CssClass="SubTitulos" 
                                                                            Text="Tipo de Informe"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" style="width: 300px">
                                                                        <asp:DropDownList ID="Dr_tip_inf" runat="server" CssClass="clsMandatorio" 
                                                                            Width="300px" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                            <asp:ListItem Value="1">Flujo de Operaciones vencidas.</asp:ListItem>
                                                                            <asp:ListItem Value="2">Flujo de Operaciones por vencer.</asp:ListItem>
                                                                            <asp:ListItem Value="3">Evolución Negocios de Clientes.</asp:ListItem>
                                                                            <asp:ListItem Value="4">Cartera vigente al dia de hoy</asp:ListItem>
                                                                            <asp:ListItem Value="5">Cartera Vencida al dia de hoy</asp:ListItem>
                                                                            <asp:ListItem Value="6">Situación de vencimiento de lineas por periodo</asp:ListItem>
                                                                            <asp:ListItem Value="7">Resumen ejecutivo ficha cliente</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 4px">
                                                        </td>
                                                        <td>
                                                            <table style="width: 99%; height: 53px;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera" style="height: 20px; width: 411px">
                                                                        <asp:Label ID="Label25393" runat="server" CssClass="SubTitulos" 
                                                                            Text="Rango de Fechas"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" style="width: 411px; height: 26px;">
                                                                        <table style="width: 97%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25394" runat="server" CssClass="Label" 
                                                                                        Text="Fecha desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_fec_dde" runat="server" Width="90px" 
                                                                                        CssClass="clsDisabled" ReadOnly="True" Enabled="false"></asp:TextBox>
                                                                                        <cc1:MaskedEditExtender ID="txt_venc_des_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_fec_dde">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_venc_des_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" PopupPosition="BottomRight" 
                                                                                        TargetControlID="Txt_fec_dde" FirstDayOfWeek="Monday">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label25395" runat="server" CssClass="Label" 
                                                                                        Text="Fecha hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_fec_hta" runat="server" CssClass="clsDisabled" 
                                                                                        Width="90px" ReadOnly="True" Enabled="false"></asp:TextBox>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_fec_hta">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="BottomRight"
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        TargetControlID="Txt_fec_hta" FirstDayOfWeek="Monday">
                                                                                    </cc1:CalendarExtender>
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
                                   
                                    </table>
                                    <br />
                                    <table style="height: 65px;text-align:-moz-center" cellpadding="0" cellspacing="0" width="100%" >
                                       <tr>
                                         <td class="Cabecera" style="height: 20px; width: 411px" align="left">
                                               <asp:Label ID="Label25407" runat="server" CssClass="SubTitulos" 
                                               Text="Datos del Cliente"></asp:Label>
                                         </td>
                                       </tr>
                                        <tr>
                                            <td class="Cabecera">
                                                <table>
                                                                            <tr>
                                                                                <td style="width: 42px">
                                                                                    <asp:Label ID="Label25408" runat="server" CssClass="Label" Text="Año"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 81px">
                                                                                    <asp:TextBox ID="Txt_Per" runat="server" CssClass="clsDisabled" TabIndex="1" 
                                                                                        Width="70px" Enabled="false"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="Txt_Per_MaskedEditExtender" runat="server" 
                                                                                        AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        ErrorTooltipEnabled="True" InputDirection="RightToLeft" MaskType="Number" 
                                                                                        TargetControlID="Txt_Per" Mask="9999">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                                <td style="width: 33px">
                                                                                    <asp:Label ID="Label25409" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" 
                                                                                        TabIndex="1" Width="90px" Enabled="false"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" 
                                                                                        AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                                        MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <asp:TextBox ID="Txt_Dig_Cli" runat="server" AutoPostBack="True" 
                                                                                        CssClass="clsDisabled" MaxLength="1" TabIndex="2" Width="15px" Enabled="false"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                                        TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                    &nbsp;
                                                                                    <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" 
                                                                                        ImageUrl="../../../Imagenes/Iconos/155.ICO" Style="margin-top: 0px" 
                                                                                        Width="20px" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                                                        ReadOnly="True" TabIndex="6" Width="388px" Height="18px"></asp:TextBox>
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
          
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        TabIndex="23" ToolTip="Buscar Documentos" />
                                    
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar Selección"
                                        ImageUrl="~/Imagenes/Botones/Boton_limpiar_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_limpiar_in.gif';" TabIndex="25"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
  
                    </td>
                </tr>
            </table>
 </ContentTemplate>
 <Triggers>
 <asp:PostBackTrigger ControlID="IB_Buscar" />

 
 </Triggers>
    </asp:UpdatePanel>
</asp:Content>

