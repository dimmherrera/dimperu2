<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametrosDeAlertas.aspx.vb" Inherits="Modulos_Alertas_rightframe_archivos_ParametrosDeAlertas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parametros de alertas</title>
    <base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
</head>
<body>
    
    <form id="form1" runat="server" style="border:5px">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table id="tb_gral" cellspacing="0" cellpadding="0" width="400px" border="0">
        <tr>
            <td class="Cabecera" height="31px">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Parámetros de Alertas"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" style="padding: 5px">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label41" runat="server" Text="Por Vencer" CssClass="Label" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="Cant. días antes del vcto." CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Por_Vencer" runat="server" CssClass="clsMandatorio" 
                                Width="50px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" InputDirection="RightToLeft" Mask="999,999" MaskType="Number"
                                TargetControlID="Txt_Por_Vencer">
                            </cc2:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label42" runat="server" Text="Mora" CssClass="Label" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="Cant. días en mora" CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Mora" runat="server" CssClass="clsMandatorio" Width="50px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" InputDirection="RightToLeft" Mask="999,999" MaskType="Number"
                                TargetControlID="Txt_Mora">
                            </cc2:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label43" runat="server" Text="Linea" CssClass="Label" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="Cant. días antes del vcto." CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Linea" runat="server" CssClass="clsMandatorio" 
                                Width="50px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="Txt_Linea_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" InputDirection="RightToLeft" Mask="999,999" MaskType="Number"
                                TargetControlID="Txt_Linea">
                            </cc2:MaskedEditExtender>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 75px" align="right">
                <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif" 
                    ToolTip="Guardar parámetros"></asp:ImageButton>
                <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif" 
                   ToolTip="Limpiar pantalla">
                </asp:ImageButton>
                <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" 
                    ToolTip="Volver">
                </asp:ImageButton>
                
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="HF_Accion" runat="server" />
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    
    
    </form>
    
</body>
</html>
