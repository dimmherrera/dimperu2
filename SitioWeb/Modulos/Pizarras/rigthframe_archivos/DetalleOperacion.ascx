<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DetalleOperacion.ascx.vb"
    Inherits="DetalleOperacion" %>
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Panel ID="Panel3" runat="server" __designer:wfdid="w175" Width="100%" ScrollBars="Auto"
    Height="400px">
    <asp:GridView ID="Gr_Documentos" runat="server" CssClass="formatUltcell" __designer:wfdid="w176"
        Width="98%" AutoGenerateColumns="False" PageSize="1">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Image ID="IB_Docto" runat="server" ImageUrl="~/images/bt_ver.gif" />
                    <asp:Label ID="id_dsi" runat="server" Text='<%#Eval("id_dsi")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:BoundField DataField="deu_ide" HeaderText="Número Identificación Pagador">
                <ItemStyle HorizontalAlign="Right" Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="deu_rso" HeaderText="Pagador">
                <ItemStyle HorizontalAlign="left" Width="300" />
            </asp:BoundField>
            <asp:BoundField DataField="dsi_num" HeaderText="Número Documento">
                <ItemStyle HorizontalAlign="center" Width="100" />
            </asp:BoundField>
            <asp:BoundField DataField="dsi_flj_num" HeaderText="Nro Cuota">
                <ItemStyle HorizontalAlign="center" Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="dsi_mto_fin" HeaderText="Monto Financiado">
                <ItemStyle HorizontalAlign="Right" Width="100" />
            </asp:BoundField>
            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="dsi_fev_rea"
                HeaderText="Fecha Vcto">
                <ItemStyle HorizontalAlign="center" Width="80" />
            </asp:BoundField>
            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="dvf_fev_rea"
                HeaderText="Fec. Vcto. Ver.">
                <ItemStyle HorizontalAlign="center" Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="cal_oto_gam" HeaderText="Cal. Oto">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="cal_obj_eti" HeaderText="Cal. Obj">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="cal_sub_jet" HeaderText="Cal. Sub">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="cal_arr_ast" HeaderText="Cal. Arr">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="cal_def_ini" HeaderText="Cal. Def">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="Txt_Verificacion" runat="server" ReadOnly="true" Width="25px" CssClass="clsTxt"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="30px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="cabeceraGrilla" />
        <RowStyle CssClass="formatUltcell" />
        <AlternatingRowStyle CssClass="formatUltcellAlt" />
    </asp:GridView>
    <%--Controles de Mensaje--%>
    <asp:LinkButton ID="LinkMensajeCon" runat="server"></asp:LinkButton>
    <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkMensajeCon"
        EnableViewState="False" PopupControlID="Panel_Msj_Con" BackgroundCssClass="modalBackground"
        PopupDragHandleControlID="Panel_Msj_Con">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="Panel_Msj_Con" runat="server" Width="300px" Height="200px" Style="display: none;">
        <table class="Contenido">
            <tbody>
                <tr>
                    <td style="width: 325px" align="left">
                        <nobr>
                                                                <asp:Image ID="imginfo12" runat="server" ImageUrl="~/Imagenes/Iconos/Info.gif"></asp:Image>
                                                                <asp:Label ID="Lbl_errorCon" runat="server" CssClass="Titulos" Width="200px" Height="12px"></asp:Label>&nbsp;
                                                            </nobr>
                    </td>
                </tr>
                <tr>
                    <td style="width: 325px; height: 91px">
                        <asp:TextBox ID="Txt_Msj_Con" runat="server" CssClass="clsTxt" Width="299px" Height="78px"
                            TextMode="MultiLine" Text="Prueba"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="IB_Ok_Con" OnClick="IB_Ok_Con_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_Out.gif"
                            BorderColor="Black"></asp:ImageButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
</asp:Panel>
<table width="100%">
    <tr>
        <td align="center">
            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                Enabled="false" />
            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                Enabled="false" />
        </td>
    </tr>
</table>
<asp:Label ID="Label1" runat="server" Text="Al Docto. se le cambio la fecha de vcto."
    CssClass="Label"></asp:Label>
<asp:TextBox ID="Txt_Verificacion" runat="server" ReadOnly="true" Width="25px" CssClass="clsTxt"></asp:TextBox>
