<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Documentacion.ascx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_Documentacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UP_Requisitos" runat="server">
    <ContentTemplate>
        <table width="700" cellspacing="10">
            <tr>
                <td valign="top" align="left">
                    <asp:DropDownList ID="DP_Tipo" runat="server" CssClass="clsMandatorio" Enabled="true"
                        Width="250px" AutoPostBack="True">
                        <asp:ListItem Value="1">Documentos Legales</asp:ListItem>
                        <asp:ListItem Value="2">Documentos Operación</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Panel ID="Panel_Gr_DocCom" runat="server" Height="400px" ScrollBars="Auto">
                        <asp:GridView ID="Gr_DocCom" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                            ShowHeader="true">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="IB_TodosDoc" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                            ToolTip="Seleccionar todos" OnClick="IB_TodosDoc_Click" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB_DOC" runat="server" CssClass="Label" 
                                            ToolTip='<%#eval("id") %>' AutoPostBack="True" 
                                            oncheckedchanged="CB_DOC_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="des" HeaderText="Descripción">
                                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="usuario" HeaderText="usuario">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="fecha aprobación">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="cabeceraGrilla" />
                            <RowStyle CssClass="formatUltcell" />
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

