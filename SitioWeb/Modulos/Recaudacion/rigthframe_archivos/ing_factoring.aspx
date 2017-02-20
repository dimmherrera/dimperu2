<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ing_factoring.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_ing_factoring" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ingreso de Factoring</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <base target=_self></base>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table cellspacing="0">
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label3" runat="server" CssClass="Titulos" Text="Mantecnción de Factoring"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido">
                <table cellspacing="0">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label4" runat="server" Text="Parámetros" CssClass="SubTitulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Parámetro"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_cod" runat="server" CssClass="clsDisabled" ReadOnly="True" 
                                            MaxLength="6"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txt_cod_FilteredTextBoxExtender" 
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_cod">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dr_fact" runat="server" Width="200px" AutoPostBack="True" 
                                            CssClass="clsMandatorio">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_des" runat="server" CssClass="clsMandatorio" 
                                            Visible="False" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="dr_estado" runat="server" Width="100%" 
                                            CssClass="clsMandatorio">
                                            <asp:ListItem Value="S">Seleccionar</asp:ListItem>
                                            <asp:ListItem Value="A">Activo</asp:ListItem>
                                            <asp:ListItem Value="I">Inactivo</asp:ListItem>
                                        </asp:DropDownList>
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
                        <td>
                            <asp:ImageButton ID="btn_nuevo" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_eli" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_guardar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_limpiar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_volver" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
 
    <uc1:Mensaje ID="Mensaje1" runat="server" />
                     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BTN_VOLVER" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
