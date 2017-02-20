<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModDoctos.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_ModDoctos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc2" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target=_self></base>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
    <script src="../FuncionesPrivadasJS/CartolaDoctos.js" type="text/javascript"></script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:Panel ID="Panel_ModDoctos" runat="server" Height="350px" Width="700px">
            
                <table class="Contenido" cellpadding="0" cellspacing="0" width="99%">
                    <tbody>
                        <tr>
                            <td align="left" class="Cabecera">
                                <asp:Label ID="Label105" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                    top: 14px" Text="Detalle documento a modificar"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <table id="Table1" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label106" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                top: 14px" Text="Cliente"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table7" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <asp:TextBox ID="Txt_Rut_Cli2" runat="server" CssClass="clsDisabled" Width="90px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:TextBox ID="Txt_Dig_Cli2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                            Width="15px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td width="80%">
                                                        <asp:TextBox ID="Txt_Nom_Cli2" runat="server" CssClass="clsDisabled" Width="400px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <table id="Table9" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:CheckBox ID="ChkB_Deudor" runat="server" Text="Pagador" CssClass="SubTitulos"
                                                AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table8" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <asp:TextBox ID="Txt_Rut_Deu2" runat="server" CssClass="clsDisabled" Width="90px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Deu2_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu2" InputDirection="RightToLeft">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:TextBox ID="Txt_Dig_Deu2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                            Width="15px" onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_LB_Buscar_Deu);"
                                                            ReadOnly="True"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Dig_Deu2"
                                                         FilterType="Custom,Numbers" ValidChars="F,f">
                                                        </cc2:FilteredTextBoxExtender>
                                                    </td>
                                                    <td width="80%">
                                                        <asp:TextBox ID="Txt_Nom_Deu2" runat="server" CssClass="clsDisabled" Width="400px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <table id="Table10" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label107" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                top: 14px" Text="Detalle documento"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table11" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                <tr>
                                                    <td align="right" width="50%">
                                                        <table id="Table12" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label108" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Nro. Ope."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Nro_Ope2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label109" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Nro. Otor."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Nro_Oto2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table id="Table13" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label110" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Moneda"></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Mon" runat="server" CssClass="clsDisabled" MaxLength="1" Width="100px"
                                                                        ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label111" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Monto"></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Mto" runat="server" CssClass="clsDisabled" MaxLength="1" Width="100px"
                                                                        ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <table id="Table14" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label112" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Tipo Docto."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Tip_Doc2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="150px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label113" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Est. Docto."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Est_Doc" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="120px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label114" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Fecha Vto."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Fec_Vto" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center" width="50%">
                                                        <table id="Table15" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:CheckBox ID="ChkB_Docto" runat="server" Text="Documento" CssClass="SubTitulos"
                                                                        AutoPostBack="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table id="table6" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                                        <tr>
                                                                            <td align="right" width="30%">
                                                                                <asp:Label ID="Label115" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                                    top: 14px" Text="Nro. Docto."></asp:Label>
                                                                            </td>
                                                                            <td align="left" width="70%">
                                                                                <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                                    Width="90px" ReadOnly="True"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Nro_Doc_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                    Enabled="True" Mask="999999999999" MaskType="Number" TargetControlID="Txt_Nro_Doc" InputDirection="RightToLeft">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="30%">
                                                                                <asp:Label ID="Label116" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                                    top: 14px" Text="Cuota"></asp:Label>
                                                                            </td>
                                                                            <td align="left" width="70%">
                                                                                <asp:TextBox ID="Txt_Cuota2" runat="server" CssClass="" MaxLength="1"
                                                                                    Width="90px" ReadOnly="True"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Cuota2_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                    Enabled="True" Mask="9,999" MaskType="Number" TargetControlID="Txt_Cuota2">
                                                                                </cc2:MaskedEditExtender>
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
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                
                                <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Guardar_Out.gif"
                                    OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" Enabled="true" 
                                    AlternateText="Guardar" />
                               
                                <asp:ImageButton ID="IB_Cancelar2" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                    Enabled="true" AlternateText="Cerrar" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        <asp:LinkButton runat="server" ID="LB_ModDoctos"></asp:LinkButton>
            <%--<cc2:ModalPopupExtender ID="MlPopupExt_ModDoctos" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel_ModDoctos" TargetControlID="LB_ModDoctos"
                X="300" Y="300">
            </cc2:ModalPopupExtender>--%>
        <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
        <asp:HiddenField ID="Txt_PosGv" runat="server" />
    </form>
</body>
</html>
