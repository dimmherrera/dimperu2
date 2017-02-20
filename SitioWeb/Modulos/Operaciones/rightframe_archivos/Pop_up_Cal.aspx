<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pop_up_Cal.aspx.vb" Inherits="Modulos_Linea_de_Credito_rigthframe_archivos_Pop_up_Cal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calificaciones por Documento</title>
    <base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="Stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 154px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="Label">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="overflow:auto">
     <table border="0" cellpadding="0" cellspacing="0" width="600px">
                <tr>
                    <td align="left" style="text-align: -moz-left" class="Cabecera">
                        <asp:Label ID="Label1" runat="server" Height="20px" Text="Calificaciones por Documento"
                            CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" align="center" style="text-align: -moz-center">
                        <table style="width: 100%">
                            <tr>
                                <td align="left" style="text-align: -moz-left" class="style1">
                                    <asp:Label ID="Label3" runat="server" CssClass="Label">Calificación de Otorgamiento</asp:Label>
                                </td>
                                <td align="left" style="text-align: -moz-left">
                                    <asp:DropDownList ID="Dp_Cal_Oto" runat="server" CssClass="clsDisabled" Enabled="false" Width="100px">
                                    <asp:ListItem Text = "Seleccionar" Value=""></asp:ListItem>
                                    <asp:ListItem Text = "AA" Value = "AA"></asp:ListItem>
                                    <asp:ListItem Text = "A" Value = "A"></asp:ListItem>
                                    <asp:ListItem Text = "BB" Value = "BB"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="Txt_Dsi" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="true"
                                        Visible="False"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" style="text-align: -moz-left" class="style1">
                                    <asp:Label ID="Label4" runat="server" CssClass="Label">Calificación Objetiva</asp:Label>
                                </td>
                                <td align="left" style="text-align: -moz-left">
                                    <asp:DropDownList ID="DP_Cal_Obj" runat="server" CssClass="clsDisabled" Enabled="false" Width="100px">
                                    <asp:ListItem Text = "Seleccionar " Value=""></asp:ListItem>
                                    <asp:ListItem Text = "AA" Value = "AA"></asp:ListItem>
                                    <asp:ListItem Text = "A" Value = "A"></asp:ListItem>
                                    <asp:ListItem Text = "BB" Value = "BB"></asp:ListItem>
                                    <asp:ListItem Text = "B" Value = "B"></asp:ListItem>
                                    <asp:ListItem Text = "CC" Value = "CC"></asp:ListItem>
                                    <asp:ListItem Text = "D" Value = "D"></asp:ListItem>
                                    <asp:ListItem Text = "E" Value = "E"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="text-align: -moz-left" class="style1">
                                    <asp:Label ID="Label5" runat="server" CssClass="Label">Calificación Subjetiva</asp:Label>
                                </td>
                                <td align="left" style="text-align: -moz-left">
                                    <asp:DropDownList ID="DP_Cal_Sub" runat="server" CssClass="clsMandatorio" Enabled="true" Width="100px">
                                    <asp:ListItem Text = "Seleccionar" Value=""></asp:ListItem>
                                    <asp:ListItem Text = "AA" Value = "AA"></asp:ListItem>
                                    <asp:ListItem Text = "A" Value = "A"></asp:ListItem>
                                    <asp:ListItem Text = "BB" Value = "BB"></asp:ListItem>
                                    <asp:ListItem Text = "B" Value = "B"></asp:ListItem>
                                    <asp:ListItem Text = "CC" Value = "CC"></asp:ListItem>
                                    <asp:ListItem Text = "D" Value = "D"></asp:ListItem>
                                    <asp:ListItem Text = "E" Value = "E"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="text-align: -moz-left" class="style1">
                                    <asp:Label ID="Label6" runat="server" CssClass="Label">Calificación de Arrastre</asp:Label>
                                </td>
                                <td align="left" style="text-align: -moz-left">
                                    <asp:DropDownList ID="DP_Cal_Arr" runat="server" CssClass="clsMandatorio" Enabled="true" Width="100px">
                                    <asp:ListItem Text = "Seleccionar" Value=""></asp:ListItem>
                                    <asp:ListItem Text = "A" Value = "A"></asp:ListItem>
                                    <asp:ListItem Text = "B" Value = "B"></asp:ListItem>
                                    <asp:ListItem Text = "C" Value = "C"></asp:ListItem>
                                    <asp:ListItem Text = "D" Value = "D"></asp:ListItem>
                                    <asp:ListItem Text = "E" Value = "E"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:ImageButton ID="IB_Adj" runat="server" ImageUrl="~/images/lupa.gif" Visible="false" ToolTip="Obtener desde interfaz de endeudamiento"/>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="text-align: -moz-left" class="style1">
                                    <asp:Label ID="Label7" runat="server" CssClass="Label">Calificación Definitiva</asp:Label>
                                </td>
                                <td align="left" style="text-align: -moz-left">
                                    <asp:DropDownList ID="DP_Cal_Def" runat="server" CssClass="clsDisabled" Enabled="false" Width="100px">
                                    <asp:ListItem Text = "Seleccionar" Value=""></asp:ListItem>
                                    <asp:ListItem Text = "AA" Value = "AA"></asp:ListItem>
                                    <asp:ListItem Text = "A" Value = "A"></asp:ListItem>
                                    <asp:ListItem Text = "BB" Value = "BB"></asp:ListItem>
                                    <asp:ListItem Text = "B" Value = "B"></asp:ListItem>
                                    <asp:ListItem Text = "CC" Value = "CC"></asp:ListItem>
                                    <asp:ListItem Text = "D" Value = "D"></asp:ListItem>
                                    <asp:ListItem Text = "E" Value = "E"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <asp:HiddenField ID="TIP_ING" runat="server" />
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="text-align: -moz-left">
                        <table id="Tabla2" runat="server" width="400px" visible="false">
                            <tr>
                                <td class="Cabecera" width="400px">
                                    <asp:Label ID="Label2" runat="server" Text="Calificación de Arrastre" CssClass="Titulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center" style="text-align: -moz-center">
                                    <table>
                                        <tr>
                                            <td>
                                                <table width="400px">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Archivo"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:FileUpload ID="FileUpload_Cal" runat="server" CssClass="Label" />
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td align="right" height="15px">
                                               <asp:Button ID="Btn_Aceptar" runat="server" Text="Aceptar" CssClass="boton"/>
                                               <asp:Button ID="Btn_Limpiar" runat="server" Text="Limpiar" CssClass="boton"/>
                                           </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="40px">
                        &nbsp;
                        <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" AlternateText="Guardar Datos">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" ToolTip="Volver" Style="height: 25px">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
             </div>
         <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
