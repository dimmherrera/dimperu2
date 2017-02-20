<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gastosfijos.ascx.vb" Inherits="Modulos_Servicios_gastosfijos" %>

<table class="Contenido" width="600">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Monto"></asp:Label></td>
        <td style="width: 100px">
            <asp:TextBox ID="txt_mto" runat="server" CssClass="clsMandatorio"></asp:TextBox>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Descripción"></asp:Label></td>
        <td colspan="2" style="width: 100px">
            <asp:TextBox ID="txt_des" runat="server" CssClass="clsMandatorio"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td style="width: 100px">
            &nbsp;<asp:HiddenField ID="Txt_Itemgas" runat="server" />
        </td>
        <td colspan="" style="width: 100px">
            <asp:ImageButton ID="btn_guar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Agregar_In.jpg" /></td>
        <td colspan="" style="width: 100px">
            <asp:ImageButton ID="btn_eli" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_In.jpg" /></td>
    </tr>
    <tr>
        <td align="right" colspan="3">
            <asp:Panel ID="Panel1" runat="server" Height="250px" Width="100%">
                <asp:GridView ID="gr_gastofijo" runat="server" CssClass="formatUltcell" Width="98%">
                    <HeaderStyle CssClass="cabeceraGrilla" />
                </asp:GridView>
            </asp:Panel>
            <table>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Totales"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:TextBox ID="txt_totales" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                        </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
