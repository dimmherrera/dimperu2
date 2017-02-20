<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cfc-suc.aspx.vb" Inherits="cfc_suc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title></title>
</head>
<body>
   <form id="form1" runat="server" style="padding:10px">
   
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   
   <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
        <ContentTemplate>
        
            <table class="Contenido" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label12" runat="server" CssClass="SubTitulos" Text="Asociación de  Clasificaciones  para Sucursales"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CB_Todos" runat="server" CssClass="Label" Text="Todos" 
                                        AutoPostBack="True" />
                                </td>
                                
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Clasificación" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dr_ccf" runat="server" CssClass="clsMandatorio" AutoPostBack="True">
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
                                    <table class="cabecera" style="width: 100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" width="105px">
                                                <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Código"></asp:Label>
                                            </td>
                                            <td align="center" width="250">
                                                <asp:Label ID="Label4" runat="server" CssClass="LabelCabeceraGrilla" Text="Sucursal"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%" Height="300px">
                                        <asp:GridView ID="gr_cfc" runat="server" AutoGenerateColumns="False" Width="100%"
                                            ShowHeader="False" CssClass="formatUltcell">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ch_pfl" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="2px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="codigo" HeaderText="Criterio Condicion">
                                                    <ItemStyle Width="100px" />
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
                                                <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" />
                                                <asp:HiddenField ID="cod_frm" runat="server" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" />
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
        </ContentTemplate>
    
   </asp:UpdatePanel>
  
   </form>
   
</body>
</html>
