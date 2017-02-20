<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Zonas.aspx.vb" Inherits="Zonas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />

    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>

    <script src="../FuncionesPrivadasJS/Sucursal_Plaza.js" type="text/javascript"></script>

    <%--<base target="_blank" />--%>
    <title>Zonas</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <table style="width: 59%">
                    <tr>
                        <td style="text-align: -moz-center" align="center" class="Cabecera">
                            <asp:Label ID="Label5" runat="server" Text="Mantención-Zonas" CssClass="Titulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <%--*****Tabla Contenido*****--%>
                            <table>
                                <tr>
                                    <td class="style11">
                                        <%--******Tabla datos Zona*****--%>
                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 562px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Datos Zonas" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td class="style15">
                                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Cod Sucursal"></asp:Label>
                                                            </td>
                                                            <td class="style14">
                                                                <asp:TextBox ID="txt_Cod_Suc" runat="server" CssClass="clsDisabled"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15">
                                                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Cod Zona"></asp:Label>
                                                            </td>
                                                            <td class="style14">
                                                                <asp:TextBox ID="txt_Cod_Zon" runat="server" CssClass="clsDisabled"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15">
                                                                <asp:Label ID="Label4" runat="server" Text="Descripcion Zona" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td class="style14">
                                                                <asp:TextBox ID="txt_Des" runat="server" CssClass="clsDisabled"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style11">
                                        <%--*****Tabla Grilla*******--%>
                                        <table border="1" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="style16">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="Panel_GrZona" runat="server" Height="120px" ScrollBars="Auto" Width="553px">
                                                                    <asp:GridView ID="Gr_Zona" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                        Width="540px">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Selección">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Btn_ver" runat="server" ToolTip='<%# Eval("Cod_Zon") %>' 
                                                                                        ImageUrl="~/Images/bt_ver.gif" onclick="Btn_ver_Click" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Cod_Zon" HeaderText="Cod Zona" />
                                                                            <asp:BoundField DataField="Des_Suc" HeaderText="Sucursal" />
                                                                            <asp:BoundField DataField="Des_Zon" HeaderText="Descripcion" />
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                        <RowStyle CssClass="formatUltcell" />
                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style11">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <%--*****tabla Botones*****--%>
                                        <br />
                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 562px">
                                            <tr>
                                                <td align="right">
                                                    <asp:ImageButton ID="Ib_Nuevo" runat="server" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';"
                                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                                                        ToolTip="Nuevo" />
                                                    <asp:ImageButton ID="Ib_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                                                        Visible="False" ToolTip="Eliminar" />
                                                    <asp:ImageButton ID="Ib_Guardar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Guardar_Out.gif"
                                                        onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                                                        AlternateText="Guardar" />
                                                    <asp:ImageButton ID="Ib_Limpiar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                                                        ToolTip="Limpiar" />
                                                    <asp:ImageButton ID="Ib_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                                        ToolTip="Volver a Sucursal Plaza" />
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
                            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="Linkb_Zon" runat="server"></asp:LinkButton>
                            <asp:HiddenField ID="HF_Po" runat="server" />
                            <asp:HiddenField ID="HF_Id" runat="server" />
                        </td>
                    </tr>
                </table>
                <uc1:Mensaje ID="Mensaje1" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Ib_Volver" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
