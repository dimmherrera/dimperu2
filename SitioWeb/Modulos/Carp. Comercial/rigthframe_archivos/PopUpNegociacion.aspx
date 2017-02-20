<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopUpNegociacion.aspx.vb" Inherits="ClsPopUpNegociacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../Servicios/gastosdefinidos.ascx" TagName="gastosdefinidos" TagPrefix="uc1" %>
<%@ Register Src="../../Servicios/gastosfijos.ascx" TagName="gastosfijos" TagPrefix="uc2" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc3" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Negociación</title>
    <base target="_self" />
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/Negociacion.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/Exportacion.js" type="text/javascript"></script>
    <script language="javascript">
        function Count(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                alert("Máximo caracteres permitidos " + long + "");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table id="tb_gral" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="Cabecera" height="31px" align="center">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Negociación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        
                         <table id="tb_contenedora" border="0" cellpadding="1" cellspacing="0" width="100%">
                                <tr>
                                    <td valign="top" width="650">
                                        <%--Cliente--%>
                                        <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0" width="99%">
                                            <tbody>
                                                <tr>
                                                    <td class="Cabecera" align="left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Datos del Cliente y Evaluación"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" valign="top">
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Cli" TabIndex="1" runat="server" CssClass="clsDisabled"
                                                                            Width="90px" __designer:wfdid="w286"  ReadOnly="True"></asp:TextBox>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                             ReadOnly="True" TabIndex="1" Width="15px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label41" runat="server" __designer:wfdid="w288" CssClass="Label" Text="Tipo de Cliente"
                                                                            Width="100px"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_TipoCliente" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="1">
                                                                        <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Text="Razon Soc."
                                                                            Width="70px"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:TextBox ID="Txt_Cliente" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                             ReadOnly="True" Width="465px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label14" runat="server" __designer:wfdid="w292" CssClass="Label" 
                                                                            Text="Ejecutivo"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="180px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label15" runat="server" __designer:wfdid="w290" CssClass="Label" 
                                                                            Text="Sucursal Cliente"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Sucursal" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="180px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label42" runat="server" __designer:wfdid="w288" CssClass="Label" Text="Banca"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Banca" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            Width="180px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label98" runat="server" __designer:wfdid="w292" CssClass="Label" 
                                                                            Text="Sucursal Operación"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="DP_Sucursal" runat="server" CssClass="clsDisabled" 
                                                                            Enabled="false" Width="300px">
                                                                        </asp:DropDownList>
                                                                        <%--<cc2:ListSearchExtender ID="ListSearchExtender3" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                                             PromptPosition="top" PromptText="Buscar..." QueryPattern="Contains"
                                                                             TargetControlID="DP_Sucursal">
                                                                        </cc2:ListSearchExtender>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label59" runat="server" __designer:wfdid="w288" CssClass="Label" Text="Evaluaciones"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:DropDownList ID="DP_Evaluaciones" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                            Width="100%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <%--Datos Diarios--%>
                                        <table id="Table14" border="0" cellpadding="0" cellspacing="0" width="430">
                                            <tr>
                                                <td align="left" class="Cabecera">
                                                    <asp:Label ID="Label21" runat="server" CssClass="SubTitulos" Text="Datos Diarios"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height: 105px" valign="top">
                                                    <table id="Table15" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label87" runat="server" CssClass="Label" Text="Fecha Neg."></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_FechaNegociacion" runat="server" CssClass="clsDisabled" Width="70px" AutoPostBack="true"></asp:TextBox>
                                                                <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Txt_FechaNegociacion"
                                                                    Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                </cc2:MaskedEditExtender>
                                                                <cc2:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar"
                                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FechaNegociacion">
                                                                </cc2:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label27" runat="server" CssClass="Label" Text="US$"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Dolar" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                    Width="92px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label43" runat="server" CssClass="Label" Text="TMC"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_TMC" runat="server" CssClass="clsDisabled" Width="92px" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        
                                </tr>
                                <tr>
                                    <td valign="top" width="650">
                                        <%--Datos Negociacion--%>
                                        <table ID="ope3" border="0" cellpadding="1" cellspacing="0">
                                            <tr>
                                                <td align="center" valign="top">
                                                    <%--Datos Operacion--%>
                                                    <table ID="tb_ope" border="0" cellpadding="0" cellspacing="0" width="200">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" 
                                                                        Text="Datos Operación"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido" style="height: 150px" valign="top">
                                                                    <table ID="tb_ope" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Tipo Docto."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DP_TipoDocto" runat="server" AutoPostBack="true" 
                                                                                    CssClass="clsDisabled" Enabled="False" Width="125px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="% Anticipo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_PorAnt" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_Moneda" runat="server" AutoPostBack="true" 
                                                                                    CssClass="clsDisabled" Enabled="False" Width="100px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Cant. Pag."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_CantDeu" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_CantDeu_MaskedEditExtender" runat="server" 
                                                                                    AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="9,999" MaskType="Number" 
                                                                                    TargetControlID="Txt_CantDeu">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Fecha Ing."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_FecIng" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="70px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Valor Evaluado"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_MtoEva" runat="server" AutoPostBack="True" 
                                                                                    CssClass="clsDisabled" MaxLength="14" ReadOnly="True" Width="92px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Estado Neg."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_EstadoNeg" runat="server" CssClass="clsDisabled" 
                                                                                    Enabled="False" Width="100px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td align="Center" valign="top">
                                                    <%--Datos Documentos--%>
                                                    <table ID="tb_doc" border="0" cellpadding="0" cellspacing="0" width="170">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" 
                                                                        Text="Datos Documentos"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido" style="height: 150px" valign="top">
                                                                    <table ID="Table2" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Cantidad"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_CanDocto" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_CanDocto_MaskedEditExtender" runat="server" 
                                                                                    AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="999,999" MaskType="Number" 
                                                                                    TargetControlID="Txt_CanDocto">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Plaza Docto."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_PlazaDocto" runat="server" AutoPostBack="true" 
                                                                                    CssClass="clsDisabled" ReadOnly="True" Width="50px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_PlazaDocto_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                                    TargetControlID="Txt_PlazaDocto">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Dias Retención"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_DiaRet" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_DiaRet_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                                    TargetControlID="Txt_DiaRet">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label100" runat="server" CssClass="Label" Text="+ Días al Vcto."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_DiaVto" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="50px" AutoPostBack="true"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_DiaVto_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="9,999" 
                                                                                    TargetControlID="Txt_DiaVto">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Fecha de Vcto."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_FecVto" runat="server" AutoPostBack="True" 
                                                                                    CssClass="clsDisabled" ReadOnly="True" Width="70px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="mask" runat="server" Mask="99/99/9999" 
                                                                                    MaskType="Date" TargetControlID="Txt_FecVto" UserDateFormat="DayMonthYear">
                                                                                </cc2:MaskedEditExtender>
                                                                                <cc2:CalendarExtender ID="Txt_FecVto_CalendarExtender" runat="server" 
                                                                                    CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                                    Format="dd-MM-yyyy" TargetControlID="Txt_FecVto">
                                                                                </cc2:CalendarExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Fecha Calculo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_FecVctoReal" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="70px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label99" runat="server" CssClass="Label" Text="Calificación"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_Calificacion" runat="server" CssClass="clsMandatorio" Enabled="true" Width="80px">
                                                                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                                    <asp:ListItem Value="AA">AA</asp:ListItem>
                                                                                    <asp:ListItem Value="A">A</asp:ListItem>
                                                                                    <asp:ListItem Value="B">B</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td align="center" valign="top">
                                                    <%--Parametros Operacion--%>
                                                    <table ID="tb_par" border="0" cellpadding="0" cellspacing="0" width="175">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:Label ID="Label37" runat="server" CssClass="SubTitulos" 
                                                                        Text="Parámetros Operación"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido" style="height: 150px" valign="top">
                                                                    <table ID="Table6" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label38" runat="server" CssClass="Label" Text="% Comisión"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_PorCom" runat="server" CssClass="clsDisabled" 
                                                                                    MaxLength="5" ReadOnly="True" Width="50px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_PorCom_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" 
                                                                                    CultureDecimalPlaceholder="es-CL" CultureThousandsPlaceholder="" 
                                                                                    CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" 
                                                                                    Mask="99.99" MaskType="Number" TargetControlID="Txt_PorCom">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label53" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_MonedaCom" runat="server" AutoPostBack="True" 
                                                                                    CssClass="clsDisabled" Enabled="false" Width="80px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label55" runat="server" CssClass="Label" Text="Minimo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_Minimo" runat="server" CssClass="clsDisabled" 
                                                                                    MaxLength="9" ReadOnly="True" Width="80px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Minimo_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                                    TargetControlID="Txt_Minimo">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label56" runat="server" CssClass="Label" Text="Maximo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_Maximo" runat="server" CssClass="clsDisabled" 
                                                                                    MaxLength="9" ReadOnly="True" Width="80px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Maximo_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                                    TargetControlID="Txt_Maximo">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label57" runat="server" CssClass="Label" Text="Mon. Com. Flat"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_MonComFlat" runat="server" AutoPostBack="True" 
                                                                                    CssClass="clsDisabled" Enabled="false" Width="80px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label58" runat="server" CssClass="Label" Text="Com. Flat"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_PorComFlat" runat="server" CssClass="clsDisabled" 
                                                                                    MaxLength="10" ReadOnly="True" Width="80px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_PorComFlat_MaskedEditExtender" runat="server" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                                    TargetControlID="Txt_PorComFlat">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td align="left" valign="top">
                                                    <%--Tasa Actual--%>
                                                    <table ID="Table1" border="0" cellpadding="0" cellspacing="0" width="175">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:Label ID="Label39" runat="server" CssClass="SubTitulos" 
                                                                        Text="Tasa (Ant) / Tasa Actual"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido" style="height: 150px" valign="top">
                                                                    <table ID="Table7" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="LB_TasaBase0" runat="server" CssClass="Label" Text="Tipo Tasa"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_TipoTasa" runat="server" AutoPostBack="True" 
                                                                                    CssClass="clsDisabled" Enabled="False" Width="60px">
                                                                                    <asp:ListItem Selected="True" Text="Fija" Value="F"></asp:ListItem>
                                                                                    <asp:ListItem Text="Variable DTF" Value="V"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="LB_TasaBase" runat="server" CssClass="Label" Text="DTF E.A."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_TasaBase" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="LB_Spread" runat="server" CssClass="Label" Text="Spread E.A"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_TasaSpread" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="LB_Puntos" runat="server" CssClass="Label" Text="Puntos E.A."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_Puntos" runat="server" CssClass="clsDisabled" 
                                                                                    ReadOnly="True" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="LB_TasaNegocio" runat="server" CssClass="Label" Text="Dscto. E.A."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_TasaNegocio" runat="server" AutoPostBack="True" 
                                                                                    CssClass="clsDisabled" ReadOnly="True" Width="50px"></asp:TextBox>
                                                                                <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="Label">DTF</asp:LinkButton>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="LB_AoM" runat="server" CssClass="Label" 
                                                                                    Text="Tipo Periodo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_mora" runat="server" AutoPostBack="false" 
                                                                                    CssClass="clsMandatorio" ToolTip="Tasa mora" Width="60px">
                                                                                    <asp:ListItem Value="A" Selected="True">Anual</asp:ListItem>
                                                                                    <asp:ListItem Value="M">Mensual</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <%--Datos Comerciales--%>
                                        <table ID="tb_Comercial" border="0" cellpadding="0" cellspacing="0" width="430">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label24" runat="server" CssClass="SubTitulos">Datos Comerciales</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height: 150px" valign="top">
                                                    <table ID="Table4" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Cupo Aprob. Lin."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoAprLin" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label29" runat="server" CssClass="Label" Text="Cupo Utilizado"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoUtilizado" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label31" runat="server" CssClass="Label" Text="Saldo Linea"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_SaldoLinea" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label44" runat="server" CssClass="Label" Text="Valor Neg."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoNegociacion" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label45" runat="server" CssClass="Label" Text="Nue. Saldo Lin."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_NuevoSaldoLinea" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label46" runat="server" CssClass="Label" Text="Fec. Vcto. Lin."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_FechaVctoLinea" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label47" runat="server" CssClass="Label" Text="Fec. Vcto. Neg."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_FecVctoNeg" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="70px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label48" runat="server" CssClass="Label" Text="Valor Pag. Vgte."></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoPagareVig" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label49" runat="server" CssClass="Label" Text="Valor Nue. Deuda"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoNuevaDeuda" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label50" runat="server" CssClass="Label" Text="Apli. A Deuda"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoDescuentos" runat="server" AutoPostBack="True" 
                                                                    CssClass="clsDisabled" ReadOnly="true" Width="92"></asp:TextBox>
                                                                <cc2:MaskedEditExtender ID="Txt_MtoDescuentos_MaskedEditExtender" 
                                                                    runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                    TargetControlID="Txt_MtoDescuentos">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label51" runat="server" CssClass="Label" 
                                                                    Text="Valor a Provisionar"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_MtoProvi" runat="server" CssClass="clsDisabled" 
                                                                    ReadOnly="true" Width="92"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <table id="ope3" border="0" cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td valign="top">
                                                    <%--Pago--%>
                                                    <table id="Table8" border="0" cellpadding="0" cellspacing="0" width="280">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:Label ID="Label62" runat="server" CssClass="SubTitulos" Text="Desembolso"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido" style="height: 120px" valign="top">
                                                                    <table id="Table9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label66" runat="server" CssClass="Label" Text="Tipo de Oper."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DP_TipoOperacion" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                                    AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label63" runat="server" CssClass="Label" Text="Tipo Desembolso"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DP_FormaPago" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                                    Width="180px" AutoPostBack="true" >
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label65" runat="server" CssClass="Label" Text="Bco. Cta. Cte."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DP_BancoCuenta" runat="server" CssClass="clsDisabled" Enabled="False" 
                                                                                AutoPostBack="true" Width="180px" >
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label64" runat="server" CssClass="Label" Text="Nro. Cta."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="Txt_NroCuenta" runat="server" CssClass="clsDisabled" Width="92"
                                                                                    ReadOnly="True" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="CB_Antes14" runat="server" CssClass="Label" Enabled="False" Text="Ant. 14 Hrs." />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <%--Pagare--%>
                                                    <table id="tb_pagare" border="0" cellpadding="0" cellspacing="0" width="195">
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:Label ID="Label91" runat="server" CssClass="SubTitulos" Text="Pagaré"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" style="height: 120px" valign="top">
                                                                <table id="Tb_Pagare" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label83" runat="server" CssClass="Label" Text="Tipo Pagaré"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DP_TipoPagare" runat="server" CssClass="clsDisabled" Enabled="false"
                                                                                Width="92px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label84" runat="server" CssClass="Label" Text="Fec. Vcto."></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_FecVctoPagare" runat="server" CssClass="clsDisabled" Width="70px"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                            <cc2:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="Txt_FecVctoPagare"
                                                                                Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                            </cc2:MaskedEditExtender>
                                                                            <cc2:CalendarExtender ID="Txt_FecVctoPagare_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FecVctoPagare">
                                                                            </cc2:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label85" runat="server" CssClass="Label" Text="Monto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_MtoPagare" runat="server" CssClass="clsDisabled" Width="92px"
                                                                                ReadOnly="true" AutoPostBack="True"></asp:TextBox>
                                                                            <cc2:MaskedEditExtender ID="Txt_MtoPagare_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_MtoPagare">
                                                                            </cc2:MaskedEditExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label86" runat="server" CssClass="Label" Text="Valor Impuesto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_MtoImpuesto" runat="server" CssClass="clsDisabled" Width="92px"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                     <asp:LinkButton ID="LB_DocCon" runat="server" CssClass="Label" Visible="false">Verificación y condiciones</asp:LinkButton>
                                                        <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                            Enabled="True" TargetControlID="LB_DocCon" BackgroundCssClass="modalBackground"
                                                            OkControlID="ImageButton1" PopupControlID="Panel_DocCon" CacheDynamicResults="true">
                                                        </cc2:ModalPopupExtender>
                                                    <%--Documentos--%>
                                                    
                                               
                                                </td>
                                                <td valign="top">
                                                    <%--Check--%>
                                                    
                                                    
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <%--Saldos Reales--%>
                                        <table id="tb_Saldos" border="0" cellpadding="0" cellspacing="0" width="430">
                                            <tbody>
                                                <tr>
                                                    <td align="left" class="Cabecera">
                                                        <asp:Label ID="Label22" runat="server" CssClass="SubTitulos" Text="Saldos Reales"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" valign="top" style="height: 120px">
                                                        <table id="Table5" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="right" style="width: 90px">
                                                                    <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Deuda en Canje"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_DeudaCanje" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                                <td align="right" style="width: 100px">
                                                                    <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Deuda Vigente"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_DeudaVigente" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 90px">
                                                                    <asp:Label ID="Label32" runat="server" CssClass="Label" Text="Reserva. Pend."></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_ExcedPendiente" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                                <td align="right" style="width: 100px">
                                                                    <asp:Label ID="Label33" runat="server" CssClass="Label" Text="Letras en Mora"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_LetrasMora" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 90px">
                                                                    <asp:Label ID="Label30" runat="server" CssClass="Label" Text="Deuda Morosa"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_DeudaMorosa" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                                <td align="right" style="width: 100px">
                                                                    <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Deuda Op. Punt."></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_DeudaPuntual" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 90px">
                                                                    <asp:Label ID="Label34" runat="server" CssClass="Label" Text="Ctas. X Cobrar"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:TextBox ID="Txt_CtasPorCobrar" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                                <td align="right" style="width: 100px">
                                                                    <asp:Label ID="Label35" runat="server" CssClass="Label" Text="Ctas. X Pagar"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_CtasPorPagar" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 90px">
                                                                </td>
                                                                <td align="right">
                                                                </td>
                                                                <td align="right" style="width: 100px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 90px">
                                                                </td>
                                                                <td align="right">
                                                                </td>
                                                                <td align="right" style="width: 100px">
                                                                    <asp:Label ID="Label52" runat="server" CssClass="Label" Font-Bold="True" Text="Monto Deuda"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_MontoDeuda" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                        Width="92"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <%--Gastos--%>
                                        <table id="Tb_gastos" border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tbody>
                                                <tr>
                                                    <td align="left" class="Cabecera">
                                                        <asp:LinkButton ID="LB_Gastos" runat="server" CssClass="SubTitulos">Gastos Operacionales</asp:LinkButton>
                                                        <cc2:ModalPopupExtender ID="LB_Gastos_ModalPopupExtender" runat="server" DynamicServicePath=""
                                                            Enabled="True" TargetControlID="LB_Gastos" BackgroundCssClass="modalBackground"
                                                            OkControlID="IB_CloseInt" PopupControlID="Panel_Gastos" CacheDynamicResults="true">
                                                        </cc2:ModalPopupExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" style="height: 80px" valign="top">
                                                        <table id="TB_Grilla_Gastos" border="0" cellpadding="0" cellspacing="0" width="500">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel_GV_Gastos" runat="server" Height="100px" Width="560px" ScrollBars="Vertical">
                                                                  
                                                                    <asp:GridView ID="GV_Gastos" runat="server" AutoGenerateColumns="False" Width="450px"
                                                                        CssClass="formatUltcell">
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Descripción" DataField="Descripción" 
                                                                                HeaderStyle-HorizontalAlign="Left" HtmlEncode="False">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle Width="300px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="monto" HeaderText="Monto">
                                                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AfectoExento" HeaderText="Afecta">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        <RowStyle CssClass="formatUltcell" />
                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                    </asp:GridView>
                                                                      </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="Cabecera" valign="middle">
                                                        <asp:Label ID="Label88" runat="server" CssClass="SubTitulos">Total Gastos:</asp:Label>
                                                        <asp:Label ID="LB_TotalGastos" runat="server" CssClass="SubTitulos"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <br />
                                        <%--Obs. Instrucciones--%>
                                        <table id="tb_ins_exc" border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label92" runat="server" CssClass="SubTitulos">Instruc. al Cursar</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:TextBox ID="Txt_InstrucCursar" runat="server" CssClass="clsDisabled" Height="30px" onKeyUp="Count(this,800)" onChange="Count(this,800)"
                                                        TextMode="MultiLine" Width="98%" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <%--Obs. Excepciones y Condiciones--%>
                                        <table id="Table12" border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label82" runat="server" CssClass="SubTitulos">Excepciones/Condiciones</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:TextBox ID="Txt_ExcepCondi" runat="server" CssClass="clsDisabled" Height="30px" onKeyUp="Count(this,800)" onChange="Count(this,800)"
                                                        TextMode="MultiLine" Width="98%" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <%--Datos de Operacion--%>
                                        <table border="0" cellpadding="0" cellspacing="0" width="430">
                                            <tr>
                                                <td class="Cabecera">
                                                <asp:Label ID="Label54" runat="server" CssClass="SubTitulos" Text="Datos de la Operación"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table id="Table17" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="opmode" runat="server" CssClass="Label">Modo Operación</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="RB_ModoOpera" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                                                    Enabled="False">
                                                                    <asp:ListItem Value="S" Selected="True">Líneal</asp:ListItem>
                                                                    <asp:ListItem Value="N">Exponencial</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label60" runat="server" CssClass="Label" Text="Ope.Puntual"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="RB_OpePuntual" runat="server" CssClass="Label" Enabled="False"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                    <asp:ListItem Value="N" Selected="True" >No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label61" runat="server" CssClass="Label" Text="Recurso"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="RB_Responsabilidad" runat="server" CssClass="Label" Enabled="False"
                                                                    RepeatDirection="Horizontal" AutoPostBack="True">
                                                                    <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label69" runat="server" CssClass="Label" Text="Con Cuotas"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="RBConCuotas" runat="server" CssClass="Label" Enabled="False"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label96" runat="server" CssClass="Label" 
                                                                    Text="Gravamen Mov. Fin."></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="RB_GMF" runat="server" CssClass="Label" 
                                                                    Enabled="False" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                </td>
                                                            <td>
                                                                </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <%--Totales--%>
                                        <table id="Tb_Totales" border="0" cellpadding="0" cellspacing="0" width="430">
                                            <tbody>
                                                <tr>
                                                    <td align="left" class="Cabecera" valign="top">
                                                        <asp:Label ID="Label68" runat="server" CssClass="SubTitulos" Text="Totales"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" style="height: 120px" valign="top">
                                                        <table id="Table13" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label71" runat="server" CssClass="Label" Text="Valor Doc."></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_MtoDoctos" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label77" runat="server" CssClass="Label" Text="Com. Variable"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_ComiPorDocto" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label72" runat="server" CssClass="Label" Text="Base Negociación"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_MtoAnticipo" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label78" runat="server" CssClass="Label" Text="Com. Fija"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_ComiFlat" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label73" runat="server" CssClass="Label" Text="Descuento"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_DifPrecio" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label97" runat="server" CssClass="Label" Text="Gastos Afectos"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_GastosAfectos" runat="server" CssClass="clsDisabled" ReadOnly="True" 
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label76" runat="server" CssClass="Label" Text="Precio de Compra"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_PrecioCompra" runat="server" CssClass="clsDisabled" 
                                                                        ReadOnly="True" Width="92px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label79" runat="server" CssClass="Label" Text="IVA"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Iva" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label75" runat="server" CssClass="Label" Text="Reserva"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_SaldoPendiente" runat="server" CssClass="clsDisabled" 
                                                                        ReadOnly="True" Width="92px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label80" runat="server" CssClass="Label" Text="Gastos Exentos"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="Txt_GastosExentos" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label70" runat="server" CssClass="Label" Text="Saldo Pag."></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_SaldoPagado" runat="server" CssClass="clsDisabled" 
                                                                        ReadOnly="True" Width="92px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label74" runat="server" CssClass="Label" Text="GMF"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_GMF" runat="server" CssClass="clsDisabled" ReadOnly="True" 
                                                                        Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    </td>
                                                                <td>
                                                                    </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label81" runat="server" CssClass="Label" Text="Total Desembolso"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_TotalGirar" runat="server" BorderColor="Red" 
                                                                        CssClass="clsDisabled" ReadOnly="True" Width="92px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                         <asp:LinkButton ID="LB_CargaEvaluacion" runat="server"></asp:LinkButton>   
                        
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    
                        <asp:ImageButton ID="IB_Guardar" runat="server" __designer:wfdid="w376" 
                            ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" OnClick="IB_Guardar_Click" 
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" 
                            ToolTip="Guardar Datos" ValidationGroup="Operacion" />
                        <cc2:ConfirmButtonExtender ID="IB_Guardar_ConfirmButtonExtender" runat="server" 
                            ConfirmText="¿ Seguro de Guardar los Datos ?" Enabled="True" 
                            TargetControlID="IB_Guardar">
                        </cc2:ConfirmButtonExtender>
                    
                        <asp:ImageButton ID="IB_Informe" onmouseover="this.src='../../../Imagenes/Botones/btn_neg_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/btn_neg_out.gif';" 
                            runat="server" ImageUrl="~/Imagenes/Botones/btn_neg_out.gif" 
                            ToolTip="Imprimir Negociación"></asp:ImageButton>
                            
                        <asp:ImageButton ID="IB_InfInstructivo" onmouseover="this.src='../../../Imagenes/Botones/btn_instr_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/btn_instr_out.gif';" OnClick="IB_InfInstructivo_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/btn_instr_out.gif" 
                            ToolTip="Imprimir Instructivo"></asp:ImageButton>
                            
                        <asp:ImageButton ID="IB_Ejecutar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Ejecutar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Ejecutar_out.gif';" OnClick="IB_Ejecutar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Ejecutar_out.gif" ToolTip="Ejecutar Calculos"
                            ValidationGroup="Operacion"></asp:ImageButton>
                         
                        <asp:ImageButton ID="IB_EnviarOpe" onmouseover="this.src='../../../Imagenes/Botones/btn_env_ope_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/btn_env_ope_out.gif';" OnClick="IB_EnviarOpe_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/btn_env_ope_out.gif" ToolTip="Enviar a Operación"
                            Enabled="False"></asp:ImageButton>
                         
                        <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" ToolTip="Volver Atras">
                        </asp:ImageButton>
                        
                    </td>
                </tr>
            </table>
            
            <asp:LinkButton ID="LB_BuscaDatosDiarios" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscaTasas" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="Hf_com_doc" runat="server" />
                        
            <asp:HiddenField ID="HF_Id_Ope" runat="server" />
            <asp:HiddenField ID="HF_Id_Opn" runat="server" />
            <asp:HiddenField ID="HF_LNL" runat="server" />
            <%--*********************************************************************************************--%>
            <%--PopUp de Gastos Operaciones--%>
            <asp:Panel ID="Panel_Gastos" runat="server"  style="display:none" BackColor="White" Height="490px" HorizontalAlign="Left" Width="680px">
                <%-- Style="display: none; position: static" Width="750px"--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table id="Table16" border="0" cellpadding="1" cellspacing="2" width="100%">
                            <tr>
                                <td align="center" style="height: 37px" valign="middle" class="Cabecera">
                                    <asp:Label ID="Label93" runat="server" CssClass="Titulos" Height="12px" Style="position: static"
                                        Width="95%">Gastos Operacionales</asp:Label>
                                    <asp:ImageButton ID="IB_CloseInt" runat="server" ImageUrl="~/Imagenes/Calendario/close.gif"
                                        OnClick="IB_CloseInt_Click" Style="position: static; width: 15px;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                                        <cc2:TabPanel ID="TabPanel0" runat="server" HeaderText="Gastos Definidos">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="Panel1" runat="server" Height="345px" ScrollBars="Auto" Width="641px">
                                                                <asp:GridView ID="gd_gastdef" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    Width="98%">
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ch_sel" runat="server" AutoPostBack="True" OnCheckedChanged="ch_sel_CheckedChanged" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Codigo" HeaderText="Cod.Gasto">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo Gasto">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="IVA" HeaderText="Afecta">
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="id_p_0036">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label95" runat="server" CssClass="Label" Text="Totales"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:TextBox ID="txt_tot_gto_def" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Gastos Fijos">
                                            <ContentTemplate>
                                                <table width="650">
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="Label89" runat="server" CssClass="LabelCabeceraGrilla" Text="Monto"></asp:Label>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txt_mto" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="txt_mto_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto">
                                                            </cc2:MaskedEditExtender>
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="Label90" runat="server" CssClass="LabelCabeceraGrilla" Text="Descripción"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="width: 100px">
                                                            <asp:TextBox ID="txt_des" runat="server" CssClass="clsMandatorio" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:HiddenField ID="Txt_Itemgas" runat="server" />
                                                        </td>
                                                        <td colspan="" style="width: 100px">
                                                            <asp:ImageButton ID="btn_guar" runat="server" ImageUrl="~/Imagenes/btn_workspace/Agregar_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Agregar_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Agregar_in.gif';"
                                                                ToolTip="Agregar Gasto" />
                                                            <asp:ImageButton ID="btn_eli" runat="server" ImageUrl="~/Imagenes/btn_workspace/Quitar_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Quitar_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Quitar_in.gif';"
                                                                ToolTip="Quitar Gasto" />
                                                        </td>
                                                        <td colspan="" style="width: 100px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="3">
                                                            <asp:Panel ID="Panel2" runat="server" Height="250px" Width="100%">
                                                                <asp:GridView ID="gr_gastofijo" runat="server" CssClass="formatUltcell" Width="95%">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="Ch_gfijos" runat="server" AutoPostBack="True" OnCheckedChanged="Ch_gfijos_CheckedChanged"
                                                                                    CausesValidation="True" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="Label94" runat="server" CssClass="Label" Text="Totales"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:TextBox ID="txt_tot_gto_fij" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                    </cc2:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    
                                    <asp:ImageButton ID="Btn_AceptarGastos" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/Boton_Aceptar_out.gif"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';" 
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_out.gif';" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            
            <%--*********************************************************************************************--%>
            <asp:Panel ID="Panel_DocCon" runat="server"  BackColor="White" Style="display: none"
                Height="490px" HorizontalAlign="Left" Width="700px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td align="center" style="height: 37px" valign="middle" class="Cabecera" colspan="2">
                                    <asp:Label ID="Label25" runat="server" CssClass="Titulos" Height="12px" Style="position: static"
                                        Width="95%">Documentos y Otras Condiciones</asp:Label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Calendario/close.gif"
                                       Style="position: static; width: 15px;" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table id="CB_doc" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label67" runat="server" CssClass="SubTitulos" Text="Documentos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top">
                                                <asp:DropDownList ID="DP_Tipo" runat="server" CssClass="clsMandatorio" 
                                                    Enabled="true" Width="250px" AutoPostBack="True">
                                                    <asp:ListItem Value="1">Documentos Legales</asp:ListItem>
                                                    <asp:ListItem Value="2">Documentos Operación</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Panel ID="Panel_Gr_DocCom" runat="server" Height="400px" ScrollBars="Auto">
                                                    <asp:GridView ID="Gr_DocCom" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="true">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:ImageButton ID="IB_TodosDoc" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                                        ToolTip="Seleccionar todos" OnClick="IB_TodosDoc_Click" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CB_DOC" runat="server" CssClass="Label" 
                                                                        ToolTip='<%#eval("id") %>' AutoPostBack="True" 
                                                                        oncheckedchanged="CB_DOC_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="des" HeaderText="Descripción">
                                                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table id="Table3" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label20" runat="server" CssClass="SubTitulos" Text="Otras Condiciones"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top">
                                                <asp:Panel ID="Panel_Gr_ConCom" runat="server" Height="400px" ScrollBars="Auto">
                                                    <asp:GridView ID="Gr_ConCom" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell" ShowHeader="true">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:ImageButton ID="IB_TodosCon" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                                        ToolTip="Seleccionar todos" OnClick="IB_TodosCon_Click" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CB_CON" runat="server" CssClass="Label" ToolTip='<%#eval("id") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="des" HeaderText="Descripción">
                                                                <ItemStyle Width="300px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            
            <uc3:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_InfInstructivo" />
            <asp:PostBackTrigger ControlID="IB_Informe" />
            <%--<asp:PostBackTrigger ControlID="LinkButton1" />--%>
        </Triggers>
    </asp:UpdatePanel>
    
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="10px" Width="10PX" Visible="False">
    </rsweb:ReportViewer>
    
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc4:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    <asp:HiddenField ID="HF_IdSbl" runat="server" />
    <asp:HiddenField ID="HF_Ldc" runat="server" />
    <asp:RequiredFieldValidator ID="RF_TipoDoc" runat="server" ControlToValidate="DP_TipoDocto"
        ErrorMessage="<b>Tipo Documento</b><br />Ingrese Tipo de Documento." Display="None"
        ValidationGroup="Operacion" InitialValue="0" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender7" TargetControlID="RF_TipoDoc"
        HighlightCssClass="validatorCalloutHighlight" />
    <%--Tipo de Documento--%>
    <asp:RequiredFieldValidator ID="RF_PorAnt" runat="server" ControlToValidate="Txt_PorAnt"
        ErrorMessage="<b>%</b><br />Ingrese Porcentaje de Anticipo." Display="None" ValidationGroup="Operacion" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender1" TargetControlID="RF_PorAnt"
        HighlightCssClass="validatorCalloutHighlight" />
    <%--Porcentaje de Anticipo--%>
    <asp:RequiredFieldValidator ID="RF_MtoEva" runat="server" ControlToValidate="Txt_MtoEva"
        ErrorMessage="<b>Monto</b><br />Ingrese Monto Evaluado." Display="None" ValidationGroup="Operacion" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender2" TargetControlID="RF_MtoEva"
        HighlightCssClass="validatorCalloutHighlight" />
    <%--Monto de Evaluacion--%>
    <asp:RequiredFieldValidator ID="RF_CanDeu" runat="server" ControlToValidate="Txt_CantDeu"
        ErrorMessage="<b>Deudores</b><br />Ingrese Cantidad de Deudores." Display="None"
        ValidationGroup="Operacion" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender3" TargetControlID="RF_CanDeu"
        HighlightCssClass="validatorCalloutHighlight" />
    <%--Cantidad de Deudores--%>
    <asp:RequiredFieldValidator ID="RF_Fecha" runat="server" ControlToValidate="Txt_FechaNegociacion"
        ErrorMessage="<b>Fecha</b><br />Ingrese Fecha de Negociacion." Display="None"
        ValidationGroup="Operacion" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender4" TargetControlID="RF_Fecha"
        HighlightCssClass="validatorCalloutHighlight" />
    <%--Fecha de Negociacion--%>
    <asp:RequiredFieldValidator ID="RF_CanDoc" runat="server" ControlToValidate="Txt_CanDocto"
        ErrorMessage="<b>Cantidad</b><br />Ingrese la cantidad de Documentos." Display="None"
        ValidationGroup="Operacion" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender5" TargetControlID="RF_CanDoc"
        HighlightCssClass="validatorCalloutHighlight" />
        
   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_DiaVto"
        ErrorMessage="<b>Sumar días al vcto.</b><br />Ingrese la cantidad de días" Display="None"
        ValidationGroup="Operacion" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender6" TargetControlID="RequiredFieldValidator1"
        HighlightCssClass="validatorCalloutHighlight" />        

    </form>
</body>
</html>
