<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pago_anulación_dnc.aspx.vb"
    Inherits="Modulos_Recaudacion_rigthframe_archivos_pago_anulación_dnc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pago y Anulación de Documentos no Cedidos</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/RADCALENDAR.css" rel="stylesheet" type="text/css" />
    <base target="_self"></base>

    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
            <table>
                <tr>
                    <td class="Cabecera" valign="top">
                        <asp:Label ID="Label1" runat="server" CssClass="Titulos" Text="Anulación y Pago Documentos no Cedidos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" height="570" valign="top">
                        <table cellspacing="0" width="800">
                            <tr>
                                <td class="Cabecera">
                                    <asp:CheckBox ID="Ch_cli" runat="server" CssClass="SubTitulos" Text="Cliente" AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="78px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Dig" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                    MaxLength="1" ReadOnly="True" Width="20px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                                    TargetControlID="Txt_Dig" ValidChars="Kk">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Rso" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="407px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellspacing="0" width="800">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="lbl_cli_deu0" runat="server" CssClass="SubTitulos" Text="Otro Factoring"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Factoring"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dr_fact" runat="server" Width="250px" CssClass="clsMandatorio">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Fecha Gen"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_fec" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="txt_fec_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec">
                                                </cc1:MaskedEditExtender>
                                                <cc1:CalendarExtender ID="txt_fec_CalendarExtender" runat="server" CssClass="radcalendar"
                                                    Enabled="True" Format="dd-MM-yyyy" TargetControlID="txt_fec">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dr_est" runat="server" Width="250px" CssClass="clsMandatorio">
                                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="G">Generado</asp:ListItem>
                                                    <asp:ListItem Value="P">Pagado</asp:ListItem>
                                                    <asp:ListItem Value="T">Todos</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="Panel_gr_fnc" runat="server" Width="960px" Height="400px" ScrollBars="Auto">
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
                                    <asp:ImageButton ID="btn_aprobar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif"
                                        ValidationGroup="Pagar" onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';" ToolTip="Pagar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_anular" runat="server" ImageUrl="~/Imagenes/Botones/boton_anular_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_anular_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_anular_in.gif';"
                                        ToolTip="Anular" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        ToolTip="Buscar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_gen_nom" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                                        ToolTip="Generar Nomina" Enabled="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                        ToolTip="Volver" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
