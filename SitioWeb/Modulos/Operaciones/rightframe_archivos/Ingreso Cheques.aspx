<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ingreso Cheques.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Ingreso_Cheques" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Ingreso de Cheques</title>
    <base target="_self">

    <script language="javascript" src="../../../../FuncionesJS/Funciones.js"></script>

    <script language="javascript" src="../../../../FuncionesJS/Grilla.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>

    <script language="javascript" src="../../../../FuncionesJS/Ajax.js"></script>

    <script src="../../Ayudas/FuncionesPrivadasJS/AyudaPlaza.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
        EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <table cellpadding="0" cellspacing="1" border="0" width="100%">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label26" runat="server" Text="Operaciones - Ingreso de Cheques" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" align="center" >
                        <table cellspacing="0" width="600">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label27" runat="server" Text="Datos Cliente / Pagador" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="RB_Cli_deu" runat="server" RepeatDirection="Horizontal"
                                                                CssClass="Label" AutoPostBack="True">
                                                                <asp:ListItem Value="C" Selected="True">Cliente</asp:ListItem>
                                                                <asp:ListItem Value="D">Pagador</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label25" runat="server" CssClass="Label">Razón Social/Nombre</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" TabIndex="1"
                                                                Width="90px" ReadOnly="True"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                TabIndex="1" Width="30px" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_raz_soc" runat="server" Width="350px" CssClass="clsDisabled"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:TextBox ID="Txt_Rut_Deu0" runat="server" CssClass="clsDisabled" TabIndex="1"
                                                                Width="90px" ReadOnly="True"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Deu0_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu0">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Dig_Deu0" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                TabIndex="1" Width="30px" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="dr_deu" runat="server" Width="350px" AutoPostBack="True" CssClass="clsMandatorio">
                                                            </asp:DropDownList>
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
                        <table cellspacing="0" width="600">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label28" runat="server" Text="Datos del Cheque" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Width="119px">Nro. Cheque</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" __designer:dtid="1125908496777294" CssClass="Label"
                                                    Width="114px">Monto Cheque</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Width="127px">Fecha Vencimiento</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_NroDocto" runat="server" TabIndex="7" CssClass="clsMandatorio"
                                                    MaxLength="9" AutoPostBack="True"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_NroDocto_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" TargetControlID="Txt_NroDocto" FilterType="Numbers" ValidChars=".,">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="Txt_NroDocto"
                                                    Display="None" ErrorMessage="Ingrese numero del cheque." ValidationGroup="ingreso"
                                                    Font-Size="8pt" />
                                                <cc1:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_MontoFinanciar0" runat="server" TabIndex="9" Width="133px" CssClass="clsMandatorio"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_mto_fin_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    AutoComplete="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_MontoFinanciar0">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="Txt_MontoFinanciar0"
                                                    Display="None" ErrorMessage="Ingrese monto del cheque" ValidationGroup="ingreso"
                                                    Font-Size="8pt" />
                                                <cc1:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_FecVcto" runat="server" AutoPostBack="True" TabIndex="12" Width="90px"
                                                    CssClass="clsMandatorio"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_FecVcto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_FecVcto">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="Txt_FecVcto"
                                                    Display="None" ErrorMessage="Ingrese Fecha de Vencimiento del cheque." ValidationGroup="ingreso"
                                                    Font-Size="8pt" />
                                                <cc1:CalendarExtender ID="Txt_FecVcto_CalendarExtender" runat="server" CssClass="radcalendar"
                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FecVcto">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellspacing="0" width="600">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label29" runat="server" Text="Datos Bancarios" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Width="98px" Visible="false">Plaza</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            
                                                <asp:ImageButton ID="IB_Plaza" runat="server" ImageUrl="~/Imagenes/btn_workspace/Plazas_out.gif"
                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/Plazas_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/Plazas_out.gif';" />
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_PlazaDes" runat="server" CssClass="clsDisabled" MaxLength="50"
                                                    TabIndex="22" Width="305px" ReadOnly="True" AutoPostBack="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label23" runat="server" CssClass="Label">Banco</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="Dr_bco" runat="server" AutoPostBack="false" Width="281px" CssClass="clsMandatorio"
                                                    TabIndex="23">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="Dr_bco_ListSearchExtender" runat="server" PromptText=""
                                                    TargetControlID="Dr_bco">
                                                </cc1:ListSearchExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="Dr_bco"
                                                    Display="None" ErrorMessage="Seleccione un banco." ValidationGroup="ingreso"
                                                    Font-Size="8pt" InitialValue="0" />
                                                <cc1:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator4">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" CssClass="Label">Cuenta Corriente</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="cta_cte" runat="server" CssClass="clsMandatorio" TabIndex="24"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="cta_cte"
                                                    Display="None" ErrorMessage="Ingrese Cuenta Corriente." ValidationGroup="ingreso"
                                                    Font-Size="8pt" />
                                                <cc1:ValidatorCalloutExtender runat="server" ID="vce" TargetControlID="RequiredFieldValidator6">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" CssClass="Label">Envio a Custodia</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dr_custodia" runat="server" CssClass="clsMandatorio" TabIndex="26"
                                                    Width="300px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dr_custodia"
                                                    Display="None" ErrorMessage="Debe seleccionar custodia" Font-Size="8pt" ValidationGroup="ingreso"
                                                    InitialValue="0" />
                                                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator7_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="RequiredFieldValidator7">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RB_CUST" runat="server" AutoPostBack="True" 
                                                    CssClass="Label" RepeatDirection="Horizontal" TabIndex="25" 
                                                    Visible="False" Width="97px">
                                                    <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td valign="top">
                                    <asp:ImageButton ID="btn_guardar" runat="server" OnClick="btn_guardar_Click" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                        TabIndex="27" ValidationGroup="ingreso" ToolTip="Guardar" onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" />
                                </td>
                                <td valign="top">
                                    <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_IN.gif';"
                                        OnClick="btn_limpiar_Click" TabIndex="28" ToolTip="Limpiar" />
                                </td>
                                <td valign="top">
                                    <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                        TabIndex="29" ToolTip="Volver" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:LinkButton ID="Lb_cuo" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="txt_itemope" runat="server" />
            <asp:HiddenField ID="HF_IdPlaza" runat="server" />
            <asp:HiddenField ID="fev_rea" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_volver" />
            <asp:PostBackTrigger ControlID="IB_Plaza" />
        </Triggers>
    </asp:UpdatePanel>
    
    
    </form>
</body>
</html>
