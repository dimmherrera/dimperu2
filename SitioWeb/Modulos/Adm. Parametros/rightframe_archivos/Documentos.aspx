<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Documentos.aspx.vb" Inherits="DocumentosCom" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Requisitos.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="General" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td style="text-align: -moz-center" align="center" class="Cabecera">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Mantención - Mantenedor de Documentos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" valign="top" align="center" style="padding: 10px; height: 570px; text-align: -moz-center; width:100%">
                        <table width="600" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                    <table class="Contenido" width="100%" >
                                        <tr>
                                            <td align="left" style="width:60px">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Codigo"></asp:Label>
                                            </td>
                                            <td style="width: 253px" align="left">
                                                <asp:TextBox ID="Txt_Codigo" runat="server" CssClass="clsDisabled" Width="70px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                            </td>
                                            <td style="width: 253px" align="left">
                                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsDisabled" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                            </td>
                                            <td style="width: 253px" align="left">
                                                <asp:DropDownList ID="DP_Estados" runat="server" CssClass="clsDisabled" Enabled="false">
                                                    <asp:ListItem Selected="True" Value="S">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="A">ACTIVO</asp:ListItem>
                                                    <asp:ListItem Value="I">INACTIVO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <tr>
                                <td valign="top" class="Contenido" align="center">
                                    <div style="overflow: auto; width: 100%; position: static; height: 400px; text-align: -moz-center"
                                        align="center">
                                        <asp:GridView ID="Gr_Requisitos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            ShowHeader="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("id_req") %>' 
                                                            ImageUrl="~/Images/bt_ver.gif" onclick="Button1_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_req" HeaderText="Código" ItemStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="req_des" HeaderText="Documento" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="250" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="req_est" HeaderText="Estado" ItemStyle-HorizontalAlign="left">
                                                    <ItemStyle Width="150" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="textbox1" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 26px" valign="top">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="Hf_pos" runat="server" />
    <asp:LinkButton ID="Lb_gua" runat="server"></asp:LinkButton>
</asp:Content>


