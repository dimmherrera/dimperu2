<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Suc_Aprobacion.aspx.vb" Inherits="Suc_Aprobacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>

<body>

    <form id="form1" runat="server" style="padding:10px">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table cellpading="0" cellspacing="0" width="600px">
                <tr>
                    <td align="center">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label12" runat="server" CssClass="SubTitulos" Text="Asociación de Sucursales para Aprobación"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top" align="center">
                                    <table> 
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Clasificación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dr_ccf" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Sucursal de Aprobación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dr_suc" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0"  style="width: 98%">
                                        <tr>
                                            <td class="Cabecera">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td align="center" width="100px" style="text-align:-moz-center">
                                                            <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Código"></asp:Label>
                                                        </td>
                                                        <td align="center" width="250px">
                                                            <asp:Label ID="Label4" runat="server" CssClass="LabelCabeceraGrilla" Text="Sucursal para ser aprobada"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" >
                                                <asp:Panel ID="Panel1" runat="server" Height="220px" ScrollBars="Auto" Width="100%">
                                                    <asp:GridView ID="gr_cfc" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="False" Width="582px">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ch_pfl" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="id_cxs" HeaderText="Criterio Condicion">
                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SUC_NOM" HeaderText="Desde">
                                                                <ItemStyle Width="250px" />
                                                            </asp:BoundField>
                                                        </Columns>
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
                    <td align="center" >
                        <table cellpadding="0" cellspacing="0" class="Cuadrado">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="Cabecera" style="width: 98%">
                                        <tr>
                                            <td align="center" width="160">
                                                <asp:Label ID="Label2" runat="server" CssClass="LabelCabeceraGrilla" Text="Código"></asp:Label>
                                            </td>
                                            <td align="center" width="250">
                                                <asp:Label ID="Label3" runat="server" CssClass="LabelCabeceraGrilla" Text="Sucursal para ser aprobada"></asp:Label>
                                            </td>
                                            <td align="center" width="250">
                                                <asp:Label ID="Label6" runat="server" CssClass="LabelCabeceraGrilla" Text="Sucursal que Aprueba"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel2" runat="server" Height="220px" ScrollBars="Auto" Width="100%">
                                        <asp:GridView ID="Gr_apb" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            ShowHeader="False" Width="98%">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Ch_apb" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_cxs">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_suc" HeaderText="Criterio Condicion">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_suc_apb" HeaderText="Desde">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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
                                    <asp:ImageButton ID="btn_asoc" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/boton_asociar_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_asociar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_asociar_in.gif';" 
                                        ToolTip="Asociar " />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_asoc0" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/boton_desasociar_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_desasociar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_desasociar_in.gif';" 
                                        ToolTip="Desasociar"/>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_guardar" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" 
                                        ToolTip="Guardar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_volver" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" 
                                        ToolTip="Cerrar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="cod_frm" runat="server" />
            <asp:HiddenField ID="cod_cxs" runat="server" />
            <asp:HiddenField ID="suc" runat="server" />
            <asp:HiddenField ID="suc_apb" runat="server" />
            
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
    </body>
    
</html>
