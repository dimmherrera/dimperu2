<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reemplazos.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table border=0 cellpadding=0 cellspacing=0 width="100%">
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label4" runat="server" Text="Reemplazos" CssClass="Titulos"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="Contenido" height="300" valign="top" style="padding: 5px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Cobrador"></asp:Label>
                        </td>
                        <td >
                            <asp:DropDownList ID="Drop_Cobradores" runat="server" CssClass="clsMandatorio" Width="300">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" width="98%">
              
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" Width="100%" Height="300">
                                <asp:GridView ID="GridView2" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                    ShowHeader="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Codigo" HeaderText="Código">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Cobrador">
                                            <ItemStyle Width="280px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                    <RowStyle CssClass="formatUltcell" />
                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                AlternateText="Anterior" />
                                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                AlternateText="Siguiente" />
                                    
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
                            <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" AlternateText="Guardar Datos">
                            </asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif" AlternateText="Limpiar Selección">
                            </asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" AlternateText="Volver"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
