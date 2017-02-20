<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Firmas.aspx.vb" Inherits="firmas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self"></base>
   

</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
        
            <table class="Contenido" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label12" runat="server" CssClass="SubTitulos" Text="Firmas"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Clasificación" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dr_ccf" runat="server" CssClass="clsMandatorio" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="Prioridad" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dr_prio" runat="server" CssClass="clsMandatorio" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table style="width: 100%" cellpadding="0" cellspacing="0" class="Contenido">
                            <tr>
                                <td class="Cabecera">
                                    <table class="Cabecera" style="width: 100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" width="160px">
                                                <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Código"></asp:Label>
                                            </td>
                                            <td align="center" width="250px">
                                                <asp:Label ID="Label4" runat="server" CssClass="LabelCabeceraGrilla" Text="Perfil"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Panel runat="server" ScrollBars="Auto" Width="100%" Height="220px">
                                        <asp:GridView ID="gr_cfc" runat="server" AutoGenerateColumns="False" Width="95%"
                                            ShowHeader="False" CssClass="formatUltcell">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ch_pfl" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="2px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="codigo" HeaderText="Criterio Condicion">
                                                    <ItemStyle Width="160px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Desde">
                                                    <ItemStyle Width="250px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        
                                        
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btn_guardar" runat="server" AlternateText="Guardar" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" 
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                                                 onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" />
                                                <asp:HiddenField ID="cod_frm" runat="server" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btn_volver" runat="server" AlternateText="Volver"  ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                                 onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                                 onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            <%--*********************************************************************************************--%>
            
        
            <%--*********************************************************************************************--%>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
