<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MensajeContacto.ascx.vb" Inherits="Modulos_Web_Controles_MensajeContacto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:LinkButton ID="LinkMensaje" runat="server"></asp:LinkButton>

<cc2:modalpopupextender id="Modal_Mensaje" runat="server" targetcontrolid="LinkMensaje"
    enableviewstate="False" popupcontrolid="Panel_Mensaje" backgroundcssclass="modalBackground">
</cc2:modalpopupextender>
    <%--Style="display: none"--%>


<asp:Panel ID="Panel_Mensaje" runat="server" Width="320px" BorderStyle="Solid" BorderColor="#007DC6" Style="display: none">
    <table width="100%" style="background-color: White; padding: 2px">
        <tbody>
            <tr>
                <td style="height: 35px; background-color: #007DC6" valign="middle" align="left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" width="15%">
                                <asp:Image ID="Img_Mensaje" runat="server" Visible="true"></asp:Image>
                            </td>
                            <td align="left" width="85%">
                                <asp:Label ID="Lbl_error" runat="server" CssClass="Titulos" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="Panel2" runat="server" Width="90%" Height="100px" BackColor="White"
                        ScrollBars="Auto">
                        <asp:Label ID="Txt_Mensaje" runat="server" CssClass="Label" ForeColor="Black"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <asp:ImageButton ID="Okbutton" OnClick="Okbutton_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_Out.gif"
                        onmouseout="this.src='../../Imagenes/Botones/Boton_Aceptar_Out.gif';" onmouseover="this.src='../../Imagenes/Botones/Boton_Aceptar_in.gif';"
                        BorderColor="Black"></asp:ImageButton>
                    <asp:ImageButton ID="canc" OnClick="canc_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Cancelar_Out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Cancelar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Cancelar_in.gif';"
                        Visible="False"></asp:ImageButton>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Panel>