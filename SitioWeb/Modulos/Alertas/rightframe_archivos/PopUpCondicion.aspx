<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopUpCondicion.aspx.vb" Inherits="Modulos_Alertas_rightframe_archivos_PopUpCondicion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantención de Condiciones</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellspacing="1" cellpadding="0" width="50%" class="Contenido">
            <tr>
                <td class="Cabecera">
                    <asp:Label ID="Label4" runat="server" CssClass="Titulos" Text="Mantención de Condiciones"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Contenido" align="center" valign="top">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Labe2l18" runat="server" CssClass="Label" Text="Fecha Cumplimiento"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_Fecha_Cumplimiento" runat="server" CssClass="clsMandatorio"
                                    Width="90px"></asp:TextBox>
                                <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" Mask="99/99/9999"
                                    TargetControlID="Txt_Fecha_Cumplimiento">
                                </cc2:MaskedEditExtender>
                                <cc2:CalendarExtender ID="Txt_Fecha_Cumplimiento_CalendarExtender" runat="server"
                                    CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                    TargetControlID="Txt_Fecha_Cumplimiento">
                                </cc2:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsMandatorio"
                                    TextMode="MultiLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DP_Estado" runat="server" CssClass="clsMandatorio"
                                    Width="100px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" TabIndex="29"
                        ToolTip="Guardar" />
                        
                    <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                        TabIndex="31" ToolTip="Volver" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_Codigo" runat="server" />
        <uc1:Mensaje ID="Mensaje1" runat="server" />
    </div>
    </form>
</body>
</html>
    