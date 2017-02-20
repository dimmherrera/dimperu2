<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Impuesto.aspx.vb" Inherits="Impuesto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impuesto</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Ajax.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Scroll.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

    <base target="_self" />
    <style type="text/css">
        .style4
        {
            width: 283px;
        }
        .style5
        {
            width: 109px;
        }
        .style6
        {
            width: 202px;
        }
        .style7
        {
            border: 1px solid #666666;
            font-size: 11px;
            font-family: "Trebuchet MS", "Lucida Grande", "Bitstream Vera Sans", Arial, Helvetica, sans-serif;
            width: 480px;
        }
        .style9
        {
            width: 480px;
        }
        .style10
        {
            border: 1px solid #666666;
            background-color: #F3F2F1;
            width: 480px;
        }
        .style11
        {
            border: 1px solid #666666;
            background-color: #F3F2F1;
            width: 483px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td class="style11">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="style7">
                                    <asp:Label ID="Label1" runat="server" Text="Impuesto" CssClass="Titulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    <table id="Tabla general" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="style4">
                                                <table>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label2" runat="server" Text="Nº Pagaré" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_NPgr" runat="server" ReadOnly="true" CssClass="clsDisabled"
                                                                Width="90px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label3" runat="server" Text="Tipo Pagaré" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_TipoPgr" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label4" runat="server" Text="Monto Pagaré" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Mto" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label6" runat="server" Text="Monto Impuesto $" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Mto_Imp" runat="server" ReadOnly="true" CssClass="clsDisabled"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label7" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Fecha" runat="server" ReadOnly="true" CssClass="clsDisabled"
                                                                Width="90px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label11" runat="server" Text="Contrato" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Contrato" runat="server" ReadOnly="false" CssClass="clsDisabled"
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="IB_AyudaDoc" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                Style="margin-top: 0px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Nro. Docto."></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_nro_doc" runat="server" ReadOnly="false" 
                                                                CssClass="clsDisabled"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="style6">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Moneda" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Moneda" runat="server" ReadOnly="true" CssClass="clsDisabled"
                                                                Width="90px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Fact. Cambio" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_factCambio" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                Width="90px" TabIndex="6"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_factCambio_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                MessageValidatorTip="False" TargetControlID="txt_factCambio">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table border="1" cellpadding="3" cellspacing="3" style="width: 158px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label9" runat="server" Text="Tasa Aplicada" CssClass="SubTitulos"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label10" runat="server" Text="%" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_Tasa" runat="server" CssClass="clsDisabled" ReadOnly=" true"
                                                                                        Width="60px"></asp:TextBox>
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
                                <td id="Botonera" align="right" class="style9">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:HiddenField ID="HF_T_Vista" runat="server" />
                                    <asp:HiddenField ID="HF_T_Plaza" runat="server" />
                                    <asp:HiddenField ID="HF_Imp" runat="server" />
                                    <asp:HiddenField ID="HF_Id_Docto" runat="server" />
                                    <asp:HiddenField ID="HF_Rut" runat="server" />
                                    <asp:HiddenField ID="HF_Id_Moneda" runat="server" />
                                    <asp:ImageButton ID="IB_Aceptar" runat="server" ImageUrl="../../../Imagenes/Botones/boton_aceptar_out.gif"
                                        AlternateText="Aceptar" onmouseover="this.src='../../../Imagenes/Botones/boton_aceptar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_aceptar_out.gif';" />
                                    <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Volver" />
            <asp:PostBackTrigger ControlID="IB_Aceptar" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Generar_Imp" runat="server"></asp:LinkButton></form>
</body>
</html>
