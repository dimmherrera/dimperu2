<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="MDoctosCondiciones.aspx.vb" Inherits="MDoctosCondiciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="General" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td style="height:31px; text-align: -moz-center" align="center" class="Cabecera">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" 
                            Text="Mantenimiento - Doctos. &amp; Otras Condiciones"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" valign="top" align="center" style="padding: 5px; height: 590px; text-align: -moz-center;">
                        <table width="350px" class="Contenido">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Opciones"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="RB_Opcion" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                        AutoPostBack="True">
                                        <asp:ListItem Text="Documentos" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Otras Condiciones" Value="C"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="600" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                    <table class="Contenido" width="600px">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Codigo"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Codigo" runat="server" CssClass="clsDisabled" Width="70px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsDisabled" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DP_Estados" runat="server" CssClass="clsDisabled" Enabled="false">
                                                    <asp:ListItem Selected="True" Value="S">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                                    <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="Contenido" align="center">
                                    <div style="overflow: auto; width: 100%; position: static; height: 400px; text-align: -moz-center"
                                        align="center">
                                        <asp:GridView ID="Gr_DocCon" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            ShowHeader="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("id") %>'  ImageUrl="~/Images/bt_ver.gif" onclick="Button1_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id" HeaderText="Código" ItemStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="des" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="250" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="est" HeaderText="Estado" ItemStyle-HorizontalAlign="left">
                                                    <ItemStyle Width="150" />
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
                        <table>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="btn_nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif"
                                        Enabled="False" ToolTip="Guardar " />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:LinkButton ID="Lb_mod" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="textbox1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:HiddenField ID="Hf_pos" runat="server" />
    <asp:LinkButton ID="Lb_gua" runat="server"></asp:LinkButton>
    
</asp:Content>

