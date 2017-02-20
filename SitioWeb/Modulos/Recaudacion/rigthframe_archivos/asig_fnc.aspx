<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="asig_fnc.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_asig_fnc"
    Title="Asignación Factoring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="1" class="Contenido">
                <tr>
                    <td class="Cabecera" style="width: 100%; text-align: -moz-center" align="center">
                        <asp:Label ID="Label1" runat="server" CssClass="Titulos" Text="Recaudación-Asignación de Factoring"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="570px" valign="top" class="Contenido" align="center" style="width: 100%">
                        <table cellspacing="0" width="960px">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:CheckBox ID="Ch_cli" runat="server" CssClass="SubTitulos" Text="Cliente" AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="left">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="78px"
                                                    ReadOnly="True"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_rut_cli">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                    Width="20px" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                                    TargetControlID="Txt_Dig_Cli" ValidChars="Kk">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td align="left">
                                                <a href="javascript:WinOpen(2,'../../../Modulos/Ayudas/AyudaCli.aspx?tipo=2','PopUpCliente',640,480,200,150);">
                                                    <img id="help" runat="server" src="../../../Imagenes/Iconos/155.ICO" style="width: 20px;
                                                        height: 20px; border-top-style: none; border-right-style: none; border-left-style: none;
                                                        border-bottom-style: none;" tabindex="3" /></a>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" Width="407px"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellspacing="0" width="960">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="lbl_cli_deu0" runat="server" CssClass="SubTitulos" Text="Otro Factoring"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="left">
                                    <asp:DropDownList ID="Dr_fact" runat="server" Width="350px" CssClass="clsMandatorio">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="Panel_gr_fnc" runat="server" ScrollBars="Horizontal" Width="1200px"
                            Height="400px">
                            <asp:GridView ID="gr_fnc" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                Width="1560px">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:ImageButton ID="IB_SeleccionDoctos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                OnClick="IB_SeleccionDoctos_Click" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Ch_doc" runat="server" Width="20px" />
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="pal_des" HeaderText="Factoring">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nce_num_doc" HeaderText="Nº Docto">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nce_mto" HeaderText="Mto.Doc.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nce_fec_vcto" HeaderText="Fecha Vcto.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_docto" HeaderText="T.D">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cli_idc" HeaderText="Rut Cliente">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cliente" HeaderText="Razón Social">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_ide" HeaderText="Rut Deudor">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nce_fec_ing" HeaderText="Fecha Ing.">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nce_obs" HeaderText="Obs.">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_ing" HeaderText="Nº Pago">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_hre" HeaderText="Nº Hoja">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fac_cam" HeaderText="Fac.Cambio">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                            </asp:GridView>
                        </asp:Panel>
                        <%--</div>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btn_buscar" runat="server" onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        ToolTip="Buscar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_asig" runat="server" ImageUrl="~/Imagenes/Botones/btn_ing_cheque_out.gif"
                                        ToolTip="Asignar Factoring" onmouseout="this.src='../../../Imagenes/Botones/btn_ing_cheque_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/btn_ing_cheque_in.gif';" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_ing_fac" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Ingreso_Out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Ingreso_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Ingreso_in.gif';"
                                        ToolTip="Ingreso de Factoring" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_gen_nom" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                                        ToolTip="Generar Nomina" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_cons_nom" runat="server" ImageUrl="~/Imagenes/Botones/boton_detalle_ope_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_ope_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_detalle_ope_in.gif';"
                                        ToolTip="Consulta Nomina" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                        <uc1:Mensaje ID="Mensaje1" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_asig" />
            <asp:PostBackTrigger ControlID="btn_ing_fac" />
            <asp:PostBackTrigger ControlID="btn_gen_nom" />
            <asp:PostBackTrigger ControlID="btn_cons_nom" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
