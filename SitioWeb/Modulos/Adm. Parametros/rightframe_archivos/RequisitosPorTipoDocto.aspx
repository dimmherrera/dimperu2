<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="RequisitosPorTipoDocto.aspx.vb" Inherits="ClsRequisitosPorTipoDocto"
    Title="Requisitos por Tipo Documento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="General" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td class="Cabecera" width="100%" align="center" style="text-align: -moz-center">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Mantenimiento-Configuración de Requisitos por Tipo de Documento"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" valign="top" align="center" style="padding: 10px; height: 560px; width:100%">
                        <table cellpadding="0" cellspacing="0" style="width: 592px">
                            <tr>
                                <td align="left">
                                    <table class="Contenido">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Tipo de Documento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_TipoDocumentos" runat="server" CssClass="clsMandatorio"
                                                    Width="150px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="Cabecera" align="center" >
                                    <table class="Cabecera" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="width: 288px">
                                                <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Codigo"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 283px">
                                                <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" Text="Descripcion"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 276px">
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
                                            ShowHeader="False" Width="842px">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CB_Req" runat="server" CssClass="Label" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_req" HeaderText="Código.Clasif.">
                                                    <ItemStyle Width="288px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="req_des" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="250" HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="req_est" HeaderText="Estado">
                                                    <ItemStyle Width="276" HorizontalAlign="Left"/>
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
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_Guardar" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lb_gua" runat="server"></asp:LinkButton>
</asp:Content>
