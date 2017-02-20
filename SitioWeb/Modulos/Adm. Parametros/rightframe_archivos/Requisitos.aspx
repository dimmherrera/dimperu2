<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Requisitos.aspx.vb" Inherits="ClsRequisitos" Title="Mantenimiento de Requisitos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Requisitos.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="General" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td style="text-align: -moz-center" align="center" class="Cabecera">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Mantenimiento - Mantenedor de Requisitos"></asp:Label>
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
                                                <asp:HiddenField ID="textbox1" runat="server" />
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
                            <td colspan=2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="Cabecera">
                                    <table class="cabecera" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="text-align: -moz-center" Width="90px">
                                                <asp:Label ID="Label6" runat="server" CssClass="LabelCabeceraGrilla" Text="Selección"></asp:Label>
                                            </td>
                                            <td align="center" style="text-align: -moz-center" width="100px">
                                                <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Codigo"></asp:Label>
                                            </td>
                                            <td align="center" style="text-align: -moz-center" width="250px">
                                                <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" Text="Descripcion"></asp:Label>
                                            </td>
                                            <td align="center" style="text-align: -moz-center" width="150px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
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
                                            ShowHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("id_req") %>' 
                                                            ImageUrl="~/Images/bt_ver.gif" onclick="Button1_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_req" HeaderText="Código.Clasif." ItemStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="req_des" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="250" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="req_est" HeaderText="Estado" ItemStyle-HorizontalAlign="left">
                                                    <ItemStyle Width="150" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
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
