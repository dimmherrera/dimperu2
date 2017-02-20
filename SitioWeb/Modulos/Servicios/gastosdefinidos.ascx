<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gastosdefinidos.ascx.vb" Inherits="Modulos_Servicios_gastosdefinidos" %>
&nbsp;
<table>
    <tr>
        <td style="width: 100px">
            <asp:Panel ID="Panel1" runat="server" Height="184px" ScrollBars="Auto" Width="641px">
<asp:GridView ID="gd_gastdef" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell" Width="98%">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="ch_sel" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="True" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Codigo" HeaderText="Cod.Gasto" />
        <asp:BoundField DataField="Tipo" HeaderText="Tipo Gasto"/>
        <asp:BoundField DataField="Monto" HeaderText="Monto" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
    </Columns>
    <HeaderStyle CssClass="cabeceraGrilla" />
</asp:GridView>
</asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:TextBox ID="txt_total" runat="server" CssClass="clsDisabled"></asp:TextBox></td>
    </tr>
</table>
