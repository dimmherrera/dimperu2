<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="DoctosCondicionesPorTipoDocto.aspx.vb" Inherits="DoctosCondicionesPorTipoDocto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="General" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td class="Cabecera" width="100%" align="center" style="text-align: -moz-center;height:31px">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Mantenimiento - Doctos. & Otras Condiciones Por Tipo Documento"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" valign="top" align="center" style="padding: 5px; height: 580px;">
                        <table cellpadding="0" cellspacing="0" >
                            <tr>
                                <td align="center">
                                    <table width="350px" class="Contenido">
                                         <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Opciones"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="Label" 
                                                    RepeatDirection="Horizontal" AutoPostBack="True">
                                                <asp:ListItem Text="Documentos" Value="D"></asp:ListItem>
                                                <asp:ListItem Text="Otras Condiciones" Value="C"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Tipo de Documento"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DP_TipoDocumentos" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="Contenido" align="center">
                                    <div style="overflow: auto; width: 100%; position: static; height: 400px; text-align: -moz-center" align="center">
                                        <asp:GridView ID="Gr_DocCon" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            ShowHeader="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CB_DocCon" runat="server" CssClass="Label" ToolTip='<%# Eval("id") %>'
                                                            oncheckedchanged="CB_DocCon_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id" HeaderText="Código">
                                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="des" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="200" HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="est" HeaderText="Estado">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                                    <asp:ImageButton ID="btn_Guardar" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif" Enabled="False" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lb_gua" runat="server"></asp:LinkButton>
</asp:Content>

