<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MRequisitos.ascx.vb"
    Inherits="Modulos_Pizarras_rigthframe_archivos_MRequisitos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UP_Requisitos" runat="server">
    <ContentTemplate>
        <table width="700" cellspacing="10">
            <tr>
                <td valign="top" align="left">
                    <asp:Panel ID="Panel_Gr_Requisitos" runat="server" Height="350px" Width="100%" ScrollBars="Vertical" >
                        <asp:GridView ID="Gr_Requisitos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                            ShowHeader="true">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="IB_Todos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                            ToolTip="Seleccionar todos" OnClick="IB_Todos_Click" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB_Req" runat="server" CssClass="Label" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NroRxo" HeaderText="Código.">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Rxd_Des" HeaderText="Descripción">
                                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="usuario" HeaderText="usuario">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="fecha aprobación">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle  CssClass="cabeceraGrilla" />
                            <RowStyle CssClass="formatUltcell" />
                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="IB_GuardarRequisitos" runat="server" ImageUrl="~/Imagenes/btn_workspace/Guardar_Out.gif"
                        onmouseover="this.src='../../../Imagenes/btn_workspace/Guardar_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/Guardar_Out.gif';"
                        ToolTip="Guardar Requisitos" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_Ope" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
