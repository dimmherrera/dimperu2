<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_doctos_no_cedidos.aspx.vb"
    Inherits="Modulos_Recaudacion_rigthframe_archivos_popup_doctos_no_cedidos" Title="Documentos no Cedidos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />

    <script language="javascript">
        function NCedidos() {
            window.dialogArguments.__doPostBack('ctl00$ContentPlaceHolder1$LB_NCE', '');
            window.close();

        }
    </script>

    <style type="text/css">
        .style2
        {
            width: 160px;
        }
        .style3
        {
            height: 73px;
        }
        .style4
        {
            width: 41px;
        }
    </style>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>

    <script language="javascript" src="../Funciones_modulo_js/Recaudacion.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
        EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" style="width: 500px">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label13" runat="server" CssClass="SubTitulos" Text="DOCUMENTOS NO CEDIDOS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table>
                            <tr>
                                <td class="style3">
                                    <table>
                                        <tr>
                                            <td class="Cabecera" colspan="4" valign="top">
                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" Width="90px"
                                                    ReadOnly="True"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    MaxLength="1" onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_lb_deu);" Width="20px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="k,K">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td valign="top">
                                                <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagador" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                    Style="margin-top: 0px" Width="20px" Enabled="False" />
                                            </td>
                                            <td width="100%">
                                                <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="220px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="Cabecera" colspan="4">
                                                <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                    Width="20px" AutoPostBack="True"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="k,K">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="IB_Ayudacli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                    Width="20px" Style="margin-top: 0px" Enabled="False" />
                                            </td>
                                            <td width="100%">
                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="220px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="4" class="Cabecera">
                                                <asp:Label ID="Label9" runat="server" CssClass="SubTitulos" Text="Datos Generales"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Nro. Contrato" Width="90px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Contrato" runat="server" Width="150px" CssClass="clsDisabled"
                                                    ReadOnly="false"></asp:TextBox>
                                                <asp:ImageButton ID="IB_AyudaDoc" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                    Style="margin-top: 0px" Width="20px" Enabled="False" />
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Nro.Docto Asoc"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_nro_doc" runat="server" Width="150px" CssClass="clsDisabled" MaxLength="20" ReadOnly="false"></asp:TextBox>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4" class="Cabecera">
                                                <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Datos Especificos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Nro.Docto"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txt_nro_doc2" runat="server" Width="120px" CssClass="clsMandatorio"></asp:TextBox>
                                               <%--<cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_nro_doc2">
                                               </cc1:MaskedEditExtender>  --%>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Tipo Docto"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dr_tipdoc" runat="server" Width="200px" CssClass="clsMandatorio">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dr_moneda" runat="server" Width="130px" CssClass="clsMandatorio">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Mto Docto." Width="90px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_mto_doc" runat="server" Width="100px" CssClass="clsMandatorio"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_doc">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Fecha Vcto"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_fec_vcto" runat="server" Width="90px" CssClass="clsMandatorio"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="txt_fec_vcto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec_vcto">
                                                </cc1:MaskedEditExtender>
                                                <cc1:CalendarExtender ID="txt_fec_vcto_CalendarExtender" runat="server" CssClass="radcalendar"
                                                    Enabled="True" Format="dd-MM-yyyy" TargetControlID="txt_fec_vcto" FirstDayOfWeek="Monday"
                                                    PopupPosition="TopRight">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="margin-left: 40px" class="Cabecera" colspan="4">
                                                <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" Text="Descuento Titulo Valor"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="margin-left: 40px">
                                                <asp:CheckBox ID="ch_fct" runat="server" Text="Ninguno" CssClass="clsTxt" AutoPostBack="True"
                                                    Checked="True" />
                                            </td>
                                            <td class="style2">
                                                <asp:DropDownList ID="dr_factoring" runat="server" Width="200px" CssClass="clsDisabled"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="Cabecera" colspan="4">
                                                <asp:Label ID="Label12" runat="server" CssClass="SubTitulos" Text="Observacion"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" class="Contenido">
                                                <asp:TextBox ID="txt_obs" runat="server" Width="98%" CssClass="clsMandatorio"></asp:TextBox>
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
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                            ToolTip="Buscar Documentos" />
                        <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_guardar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_guardar_in.gif';"
                            ToolTip="Guardar" Enabled="False" ValidationGroup="ingreso" />
                        <asp:ImageButton ID="BTN_VOLVER" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            ToolTip="Volver" />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="lb_cli" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lb_deu" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="val_cli_deu" runat="server" />
            <asp:HiddenField ID="id_hre" runat="server" />
            <asp:HiddenField ID="val_mto_rec" runat="server" />
            <asp:HiddenField ID="ID_NCE" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            <asp:LinkButton ID="Lb_cuo" runat="server"></asp:LinkButton>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BTN_VOLVER" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lb_rel_deu" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    </form>
</body>
</html>
