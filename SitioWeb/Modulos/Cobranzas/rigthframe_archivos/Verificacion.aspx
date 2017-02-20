<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Verificacion.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Verificacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
    <script src="../FuncionesProvadasJS/Verificacion.js" type="text/javascript"></script>

    <script src="../../Adm.%20Cuentas/FuncionesPrivadasJS/CXC.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Ajax.js" type="text/javascript"></script>
 
    <base target="_self"></base>

 
    
</head>

<body style="border:10px">
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"  />
    
    <asp:UpdatePanel runat="server" ID="UP_Verificacion" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table id="tb_gral" cellpadding="0" cellspacing="1" class="Contenido" width="100%">
                <tr>
                    <td height="31px" class="Cabecera" align="center">
                        <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="VERIFICACIÓN - Documentos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px;text-align:-moz-center;height:590px">
                        <%--Cliente--%>
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                        top: -14px" Text="Cliente"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right" width="120">
                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" 
                                                    Width="90px" ReadOnly="True"></asp:TextBox>
                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                    recivedecimal="False" Style="position: static" Width="15px" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="position: static" ID="Txt_Raz_Soc_Cli" runat="server" CssClass="clsDisabled"
                                                    Width="400px"  ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <%--Deudor--%>
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label13" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right" width="120">
                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsMandatorio" TabIndex="30"
                                                    Width="90px"  ></asp:TextBox>
                                                <cc2:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                </cc2:MaskedEditExtender>
                                                <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsMandatorio" 
                                                    TabIndex="31" Width="15px" MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                    TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                </cc2:FilteredTextBoxExtender>
                                               
                                                <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagador" ImageUrl="../../../Imagenes/Iconos/155.ICO" width="20"  />                                
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Tipo Pagador"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Tip_Deu" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                    Width="300px" Enabled="False" TabIndex="3">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Lbl_Nom_Deu" runat="server" CssClass="Label" Text="Nombre"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" 
                                                    Width="400px" MaxLength="50" TabIndex="4" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Lbl_Ape_Pat_Deu" runat="server" CssClass="Label" Text="1er. Apellido"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Ape_Pat" runat="server" CssClass="clsDisabled"
                                                    Width="210px" ReadOnly="True" TabIndex="5"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Lbl_Ape_Mat_Deu" runat="server" CssClass="Label" Text="2do. Apellido"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Ape_Mat" runat="server" CssClass="clsDisabled"
                                                    Width="210px" ReadOnly="True" TabIndex="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Abr. Raz. Social"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Abr_Raz_Soc" runat="server" CssClass="clsDisabled"
                                                    Width="210px" Enabled="False" TabIndex="7">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Zona de Ries."></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Zon_Rie" runat="server" CssClass="clsDisabled"
                                                    Width="211px" Enabled="False" TabIndex="8">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <%--Datos del Deudor--%>
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label21" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                        top: 14px" Text="Datos Pagador"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top" >
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Nombre Contacto /Cargo  Contacto"
                                                                Width="500px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:10px">
                                                            <asp:DropDownList ID="Dp_Raz_Cnt" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                Width="500px" Enabled="False" TabIndex="9">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Telefono"></asp:Label>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:TextBox ID="Txt_Fono_Cnt" runat="server" CssClass="clsDisabled"
                                                                            Width="90px" ReadOnly="True"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fono_Cnt_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Fono_Cnt">
                                                                        </cc2:MaskedEditExtender>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label25" runat="server" CssClass="Label" Text="Fax"></asp:Label>
                                                                    </td>
                                                                    <td Width="90px">
                                                                        <asp:TextBox ID="Txt_Fax_Cnt" runat="server" CssClass="clsDisabled"
                                                                            Width="90px" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label26" runat="server" CssClass="Label" Text="E-Mail"></asp:Label>
                                                                    </td>
                                                                    <td width="200px">
                                                                        <asp:TextBox Style="position: static" ID="Txt_Mail_Cnt" runat="server" CssClass="clsDisabled"
                                                                            Width="197px"  ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td >
                                                <table cellpadding="0" cellspacing="0" width="100">
                                                    <tr>
                                                        <td align="center" >
                                                            <asp:ImageButton ID="IB_Contactos" runat="server" ImageUrl="~/Imagenes/btn_workspace/Contactos_Out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Contactos_Out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Contactos_In.gif';"
                                                                Enabled="False" ToolTip="Contactos" TabIndex="10" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="IB_Obs_Deudor" runat="server" Enabled="False" 
                                                                ImageUrl="~/Imagenes/btn_workspace/ObsDeu_Out.gif" 
                                                                OnClick="IB_Obs_Deudor_Click" 
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/ObsDeu_Out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/ObsDeu_In.gif';" 
                                                                TabIndex="12" ToolTip="Observación Deudor" />
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center">
                                                            <asp:ImageButton ID="IB_Dias_Pago" runat="server" ImageUrl="~/Imagenes/btn_workspace/DiaPago_Out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/DiaPago_Out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/DiaPago_In.gif';"
                                                                Enabled="False" ToolTip="Días de Pagos" TabIndex="11" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="IB_Cal_Pago" runat="server" Enabled="False" 
                                                                ImageUrl="~/Imagenes/btn_workspace/Cal_pago_out.gif" 
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Cal_pago_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/Cal_pago_in.gif';" 
                                                                TabIndex="12" ToolTip="Calendario de Pagos Pagador" />
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center">
                                                            &nbsp;</td>
                                                        <td align="center">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" align="center">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label43" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                top: -14px" Text="¿Con Cobranza?"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Image ID="ImgVerde" runat="server" ImageUrl="~/Imagenes/Iconos/verde02.gif"
                                                                Visible="true" />
                                                            <asp:Image ID="ImgRojo" runat="server" ImageUrl="~/Imagenes/Iconos/rojo02.gif" Width="16px"
                                                                Visible="true" />
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
                        <%--Datos del Documento--%>
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label27" runat="server" CssClass="SubTitulos" Text="Datos Documentos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="right" >
                                                                                    <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Nro. Docto."></asp:Label>
                                                                                </td>
                                                                                <td >
                                                                                    <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsDisabled" TabIndex="13"
                                                                                        Width="52px" ReadOnly="True" MaxLength="6"></asp:TextBox>
                                                                                    <cc2:FilteredTextBoxExtender ID="Txt_Nro_Doc_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Numbers" TargetControlID="Txt_Nro_Doc" ValidChars=".">
                                                                                    </cc2:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label29" runat="server" CssClass="Label" Text="Tipo Docto."></asp:Label>
                                                                                </td>
                                                                                <td >
                                                                                    <asp:DropDownList ID="Dp_Tip_Doc" runat="server" CssClass="clsDisabled"
                                                                                        Width="300px" Enabled="False" TabIndex="14">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label30" runat="server" CssClass="Label" Text="Est. Verif."></asp:Label>
                                                                                </td>
                                                                                <td colspan="3">
                                                                                    <asp:DropDownList ID="Dp_Est_Veri" runat="server" CssClass="clsDisabled"
                                                                                        Width="400px" Enabled="False" TabIndex="15">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="100">
                                                                                    <asp:Label ID="Label31" runat="server" CssClass="Label" Text="Obs."></asp:Label>
                                                                                </td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="Txt_Obs_Docto" runat="server" CssClass="clsDisabled" Width="400px"
                                                                                        MaxLength="250" ReadOnly="True" TabIndex="16"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="right" width="100">
                                                                                    <asp:Label ID="Label32" runat="server" CssClass="Label" Text="Fec. Vcto."></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_Vcto" runat="server" CssClass="clsDisabled" Style="margin-bottom: 0px"
                                                                                        Width="90px" TabIndex="17" AutoPostBack="True"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Vcto_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Vcto">
                                                                                    </cc2:MaskedEditExtender>
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_Vcto_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" TargetControlID="Txt_Fec_Vcto" Format="dd-MM-yyyy" FirstDayOfWeek="Monday">
                                                                                    </cc2:CalendarExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label44" runat="server" CssClass="Label" Text="Fec. Vcto. Real"></asp:Label>
                                                                                </td>
                                                                                <td >
                                                                                    <asp:TextBox ID="Txt_Fec_Vto_Real" runat="server" CssClass="clsDisabled" 
                                                                                        Style="margin-bottom: 0px" TabIndex="17" Width="90px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Vto_Real_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Vto_Real">
                                                                                    </cc2:MaskedEditExtender>
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_Vto_Real_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                                        Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Vto_Real">
                                                                                    </cc2:CalendarExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="100">
                                                                                    <asp:Label ID="Label34" runat="server" CssClass="Label" Text="Tipo Moneda"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dp_Tip_Mon" runat="server" AutoPostBack="True" 
                                                                                        CssClass="clsDisabled" Enabled="False" TabIndex="19" Width="130px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label33" runat="server" CssClass="Label" Text="Monto."></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Mto_Doc" runat="server" CssClass="clsDisabled" 
                                                                                        ReadOnly="True" TabIndex="18" Width="105px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Mto_Doc_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                                                                        CultureName="es-ES" CultureThousandsPlaceholder="." CultureTimePlaceholder="" 
                                                                                        Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                                        MaskType="Number" TargetControlID="Txt_Mto_Doc">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label35" runat="server" CssClass="Label" Text="Llegada"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label37" runat="server" CssClass="Label" Text="Fec. Pago"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fec_LLeg" runat="server" CssClass="clsDisabled" 
                                                                            Width="90px" ReadOnly="True" TabIndex="20"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fec_LLeg_MaskedEditExtender" runat="server" 
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_LLeg">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="Txt_Fec_LLeg_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" TargetControlID="Txt_Fec_LLeg" FirstDayOfWeek="Monday" Format="dd-MM-yyyy">
                                                                        </cc2:CalendarExtender>
                                                                        <asp:TextBox ID="Txt_Hor_LLeg" runat="server" CssClass="clsDisabled" onkeypress="fnTrapKD(LB_Valida_Fechas)"
                                                                            Width="39px" ReadOnly="True" TabIndex="21"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Hor_LLeg_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="Txt_Hor_LLeg">
                                                                        </cc2:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fec_Pag" runat="server" CssClass="clsDisabled" 
                                                                            Style="margin-bottom: 0px" Width="90px" ReadOnly="True" TabIndex="22" 
                                                                            AutoPostBack="True"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Pag_MaskedEditExtender" runat="server" 
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Pag">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="Txt_Fec_Pag_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" TargetControlID="Txt_Fec_Pag" Format="dd-MM-yyyy" PopupPosition="BottomRight"
                                                                            FirstDayOfWeek="Monday">
                                                                        </cc2:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Verificación"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label38" runat="server" CssClass="Label" Text="1° Gestión"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fec_Veri" runat="server" CssClass="clsDisabled" 
                                                                            Width="90px" ReadOnly="True" TabIndex="23"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Veri_MaskedEditExtender" runat="server" 
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Veri">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="Txt_Fec_Veri_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="False" TargetControlID="Txt_Fec_Veri" Format="dd-MM-yyyy" 
                                                                            FirstDayOfWeek="Monday">
                                                                        </cc2:CalendarExtender>
                                                                        <asp:TextBox ID="Txt_Hor_Veri" runat="server" CssClass="clsDisabled" onkeypress="fnTrapKD(LB_Valida_Fechas)"
                                                                            Width="39px" ReadOnly="True" TabIndex="24"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Hor_Veri_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99:99" MaskType="Time" 
                                                                            TargetControlID="Txt_Hor_Veri">
                                                                        </cc2:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fec_Ges" runat="server" CssClass="clsDisabled" 
                                                                            Width="90px" ReadOnly="True" TabIndex="25" AutoPostBack="True"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Ges_MaskedEditExtender" runat="server" 
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Ges">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="Txt_Fec_Ges_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" TargetControlID="Txt_Fec_Ges" Format="dd-MM-yyyy" PopupPosition="BottomRight"
                                                                            FirstDayOfWeek="Monday">
                                                                        </cc2:CalendarExtender>
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
                        <%--Documentos Verificados--%>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="Cabecera">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="220px">
                                                <asp:Label ID="Label39" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                    top: -14px" Text="Documentos Verificados"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChB_Con_Dat" runat="server" CssClass="Label" 
                                                    Text="Conservar Datos" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                
                                    
                                <%--Grilla--%>
                                <td class="Contenido">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButton Style="position: static" ID="RB_Todos" runat="server" CssClass="Label"
                                                    Text="Todos" Checked="True" AutoPostBack="True"></asp:RadioButton>
                                            </td>
                                            <td style="margin-left: 40px">
                                                <asp:DropDownList ID="Dp_Tip_Doc_Bus" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" CssClass="Label" Text="Fecha Inicio"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Fec_Ini" runat="server" CssClass="clsMandatorio" Height="20px"
                                                    Style="margin-bottom: 0px" Width="90px"></asp:TextBox>
                                                <cc2:MaskedEditExtender ID="Txt_Fec_Ini_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Ini">
                                                </cc2:MaskedEditExtender>
                                                <cc2:CalendarExtender ID="Txt_Fec_Ini_CalendarExtender" runat="server" CssClass="radcalendar"
                                                    Enabled="True" TargetControlID="Txt_Fec_Ini" FirstDayOfWeek="Monday" Format="dd-MM-yyyy">
                                                </cc2:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label42" runat="server" CssClass="Label" Text="Fecha Término"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Fec_Ter" runat="server" CssClass="clsMandatorio" Height="21px"
                                                    Style="margin-bottom: 0px" Width="90px" AutoPostBack="True"></asp:TextBox>
                                                <cc2:MaskedEditExtender ID="Txt_Fec_Ter_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Ter">
                                                </cc2:MaskedEditExtender>
                                                <cc2:CalendarExtender ID="Txt_Fec_Ter_CalendarExtender" runat="server" CssClass="radcalendar"
                                                    Enabled="True" TargetControlID="Txt_Fec_Ter" Format="dd-MM-yyyy" FirstDayOfWeek="Monday">
                                                </cc2:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <asp:Panel ID="Panel_GV_DoctosDvf" runat="server" ScrollBars="Horizontal" width="860px" height="230px">
                                    
                                        <asp:GridView ID="GV_DoctosDvf" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="0" CssClass="formatUltcell" ShowHeader="True" Width="2000px">
                                            <FooterStyle BorderStyle="Dashed" />
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="90px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("dvf_num") %>' onclick="Img_Ver_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>                                            
                                                <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deu_rso" HeaderText="Razón Social" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                                    <FooterStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_num" HeaderText="N° Docto." ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_mto" HeaderText="Monto" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pnu_des" HeaderText="Tipo Docto.">
                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_fev" HeaderText="Fec. Vcto." ReadOnly="True" 
                                                    DataFormatString="{0:dd/MM/yyyy}"> 
                                                    <ItemStyle HorizontalAlign="Center" Width="120px"  />
                                                    </asp:BoundField>
                                                <asp:BoundField DataField="EstadoVer" HeaderText="Est. Ver." 
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_obs" HeaderText="Observación" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_fec_pag" HeaderText="Fec. Pago" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_fec_pri_gsn" HeaderText="Fec. 1ª Gestión" 
                                                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_fec_vfc" HeaderText="Fec. Ver." 
                                                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px"  />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_fec_ing" HeaderText="Fec. Ing." 
                                                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cnc_nom" HeaderText="Contacto">
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cnc_car" HeaderText="Cargo" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cnc_tel" HeaderText="Telefono" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dvf_zon_rgo_rec" HeaderText="Zona Rsgo.">
                                                    <ItemStyle HorizontalAlign="Center" Width="220px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                   </asp:Panel>
                                    <%--</div>--%>
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" AlternateText="Anterior" />
                                                <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" AlternateText="Siguiente" />
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
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        ToolTip="Buscar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/boton_Nuevo_out.gif"
                                        OnClick=" IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';" ToolTip="Nuevo"
                                        Enabled="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                                        OnClick=" IB_Eliminar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';" ToolTip="Eliminar"
                                        Enabled="False" />
                                   
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                        OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" Enabled="False"
                                        ToolTip="Guardar" />
                                   
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Inf_Confir" runat="server" ImageUrl="~/Imagenes/Botones/boton_confirmacion_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_confirmacion_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_confirmacion_in.gif';" 
                                        ToolTip="Imprimir Registro Confirmación de Facturas" Enabled="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Imprimir" runat="server" ImageUrl="~/Imagenes/Botones/boton_Imprimir_out.gif"
                                        OnClick="IB_Imprimir_Click" onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" ToolTip="Imprimir Listado de Verificaciones" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';"
                                        ToolTip="Volver" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        
            <asp:HiddenField ID="Txt_SW" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            <asp:HiddenField ID="Txt_PosGV" runat="server" />
            <asp:HiddenField ID="HF_CNC" runat="server" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Contactos" />
            <asp:PostBackTrigger ControlID="IB_Dias_Pago" />
            <asp:PostBackTrigger ControlID="IB_Obs_Deudor" />
            <asp:PostBackTrigger ControlID="IB_Cal_Pago" />
            <asp:PostBackTrigger ControlID="IB_Volver" />
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
            <asp:PostBackTrigger ControlID="IB_Inf_Confir" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Valida_Fechas" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Buscar_Deu" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Refrescar" runat="server" CausesValidation="False"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Eliminar" runat="server"></asp:LinkButton>
    
    </form>
    
</body>
</html>
