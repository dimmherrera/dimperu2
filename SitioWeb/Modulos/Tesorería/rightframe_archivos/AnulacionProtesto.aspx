<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="AnulacionProtesto.aspx.vb" Inherits="Modulos_Tesorería_rightframe_archivos_AnulacionProtesto"
    Title="Anulación y Protestos de Pagos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Pagos.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UP_General" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido" style="text-align: -moz-center">
                <tr>
                    <td class="Cabecera" height="31px" align="center">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Tesorería - Anulación de Pagos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px; height: 590px; width: 100%"
                        valign="top" align="center">
                        <table cellspacing="0" cellpadding="0" border="0" width="95%" style="text-align:-moz-center">
                            <tr>
                                <td align="left">
                                    <cc2:TabContainer ID="TabContainer1" runat="server" Height="500px" Width="100%" AutoPostBack="true"
                                        ActiveTabIndex="0">
                                        <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Recaudaciones">
                                            <HeaderTemplate>
                                                Recaudaciones</HeaderTemplate>
                                            <ContentTemplate>
                                                <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="99%">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" valign="top">
                                                                <table border="0" cellpadding="2" cellspacing="2">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td valign="top" align="left">
                                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="CB_Cliente" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                                TabIndex="10" Text="Cliente" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Rut_Cli" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                                                 ReadOnly="True" Style="position: static" TabIndex="11" Width="90px"></asp:TextBox><asp:TextBox
                                                                                                    ID="Txt_Dig_Cli" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                                                    MaxLength="1" onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_LB_BuscarCliente);"
                                                                                                    ReadOnly="True" Style="position: static" TabIndex="12" Width="15px" AutoPostBack="True"></asp:TextBox><cc2:FilteredTextBoxExtender
                                                                                                        ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                                        TargetControlID="Txt_Dig_Cli" ValidChars="k,K">
                                                                                                    </cc2:FilteredTextBoxExtender>
                                                                                            <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="Ib_ayu_cli" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                                Width="20px" Enabled="False" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Nom_Cli" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                                                MaxLength="50" ReadOnly="True" Style="position: static" TabIndex="13" Width="270px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td valign="top">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table border="0" cellpadding="2" cellspacing="0" style="border: 1px solid #000000;">
                                                                                    <tr>
                                                                                        <td colspan="1">
                                                                                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Fecha de Pago"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_FechaDesde" runat="server" CssClass="clsTxt" MaxLength="10"
                                                                                                Style="position: static" TabIndex="100" Width="90px"></asp:TextBox><cc2:MaskedEditExtender
                                                                                                    ID="Txt_FechaDesde_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_FechaDesde">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            <cc2:CalendarExtender ID="Txt_FechaDesde_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FechaDesde">
                                                                                            </cc2:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_FechaHasta" runat="server" CssClass="clsTxt" MaxLength="10"
                                                                                                Style="position: static" TabIndex="100" Width="90px"></asp:TextBox><cc2:MaskedEditExtender
                                                                                                    ID="Txt_FechaHasta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_FechaHasta">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            <cc2:CalendarExtender ID="Txt_FechaHasta_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FechaHasta">
                                                                                            </cc2:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="Label" Text="N° Docto. Pago"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_NroDpo" runat="server" CssClass="clsTxt" Width="100px"></asp:TextBox><cc2:MaskedEditExtender
                                                                                                ID="Txt_NroDpo_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                                                MaskType="Number" TargetControlID="Txt_NroDpo">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Monto Pago"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Monto" runat="server" CssClass="clsTxt" Width="100px"></asp:TextBox><cc2:MaskedEditExtender
                                                                                                ID="Txt_Monto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                                                MaskType="Number" TargetControlID="Txt_Monto">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td colspan="2">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table border="0" cellpadding="2" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Bancos"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:DropDownList ID="DP_Bancos" runat="server" CssClass="clsTxt" Width="230px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Plaza Pago"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:DropDownList ID="DP_Plazas" runat="server" CssClass="clsTxt" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:DropDownList ID="DP_Estados" runat="server" CssClass="clsTxt" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="CB_Sucursal" runat="server" Text="Todas las Sucursales" CssClass="Label" Checked
                                                                                                            AutoPostBack="True" TabIndex="10" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                                <table width="99%" cellpadding="0" border="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="Cabecera">
                                                                            <asp:Label ID="Label37" runat="server" Text="Resultado de la Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="Contenido" valign="top">
                                                                            <div id="GridViewDiv" style="overflow: scroll; width: 1150px; height: 340px">
                                                                                <asp:GridView ID="GV_Pagos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                                    Width="1670px">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Selección">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button2_Click"
                                                                                                    ToolTip='<%# Eval("id_ing") %>' /></ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo Pago">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="id_ing" HeaderText="N° Pago">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Banco" HeaderText="Banco">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="fec_emision" HeaderText="Fecha Emi." DataFormatString="{0:dd/MM/yyyy}">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="fec_vcto" HeaderText="Fecha Vcto." DataFormatString="{0:dd/MM/yyyy}">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Plaza" HeaderText="Plaza">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Cta" HeaderText="Cta. Cte.">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Motivo" HeaderText="Motivo Protesto">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="id_est" Visible="False">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lb_est" runat="server" Text='<% #eval("dpo_anl") %>' Visible="False"></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lb_tip" runat="server" Text='<% #eval("id_est") %>' Visible="False"></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lb_id_ing" runat="server" Text='<% #eval("Nro") %>' Visible="False"></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                    <RowStyle CssClass="formatUltcell" />
                                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Aplicaciones">
                                            <ContentTemplate>
                                                <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="99%">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:Label ID="Label16" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" valign="top">
                                                                <table border="0" cellpadding="2" cellspacing="2">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:RadioButtonList ID="RB_Clientes" runat="server" CssClass="Label" 
                                                                                                AutoPostBack="True">
                                                                                                <asp:ListItem Text="Todos los Cliente" Value="0" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="Cliente Especifico" Value="1"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Rut_Cli_Apl" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                                                 Style="position: static" TabIndex="11" Width="90px" ReadOnly="True"></asp:TextBox><asp:TextBox
                                                                                                    ID="Txt_Dig_Cli_Apl" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                                                    MaxLength="1" Style="position: static" TabIndex="12" Width="15px" ReadOnly="True"
                                                                                                    onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_LB_BuscarClienteApli);" 
                                                                                                AutoPostBack="True"></asp:TextBox><cc2:FilteredTextBoxExtender
                                                                                                        ID="Txt_Dig_Cli_Apl_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                                        TargetControlID="Txt_Dig_Cli_Apl" ValidChars="k,K">
                                                                                                    </cc2:FilteredTextBoxExtender>
                                                                                            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli_Apl">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="Ib_ayu_cli_apl" runat="server" Enabled="False" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                                Width="20px" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Nom_Cli_Apl" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                                                MaxLength="50" ReadOnly="True" Style="position: static" TabIndex="13" Width="270px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td valign="top" align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table border="0" cellpadding="2" cellspacing="0" style="border: 1px solid #000000;">
                                                                                    <tr>
                                                                                        <td colspan="1">
                                                                                            <asp:Label ID="Label17" runat="server" Text="Fecha de Aplicación" CssClass="Label"
                                                                                                Font-Bold="True"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label18" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Fec_Apl_Dsd" runat="server" CssClass="clsMandatorio" MaxLength="10"
                                                                                                Style="position: static" Width="90px" TabIndex="100"></asp:TextBox><cc2:MaskedEditExtender
                                                                                                    ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Apl_Dsd">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar"
                                                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Apl_Dsd">
                                                                                            </cc2:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label19" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_Fec_Apl_Hst" runat="server" CssClass="clsMandatorio" MaxLength="10"
                                                                                                Style="position: static" Width="90px" TabIndex="100"></asp:TextBox><cc2:MaskedEditExtender
                                                                                                    ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Apl_Hst">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            <cc2:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="radcalendar"
                                                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Apl_Hst">
                                                                                            </cc2:CalendarExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td align="left" valign="top">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                                <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                                    <tr>
                                                                        <td class="Cabecera">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Resultado de la Búsqueda"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="Contenido" valign="top">
                                                                            <div id="GridViewDiv_Apli" style="overflow: scroll; width: 1150px; height: 320px">
                                                                                <asp:GridView ID="GV_Aplicaciones" runat="server" AutoGenerateColumns="False" 
                                                                                    CssClass="formatUltcell" Width="1780px">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Selección">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                                                                    ToolTip='<%# Eval("Nro_Apli") %>' /></ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="Rut" HeaderText="Identificación">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Nombre" HeaderText="Razón Social">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="apl_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto_Exc" HeaderText="Monto Exc.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto_Dnc" HeaderText="Monto Dnc.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto_Cxp" HeaderText="Monto Cxp.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto_Cxc" HeaderText="Monto Cxc.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto_Dvg" HeaderText="Mto Doc Vig.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Monto_Dmr" HeaderText="Mto Doc Mor.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Tasa_Cli" HeaderText="Tasa Cli.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Tasa_Apli" HeaderText="Tasa Apli.">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Devuelto" HeaderText="Devuelto">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Observacion" HeaderText="Observacion">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="apl_fec_anl" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Anl.">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lb_id_apl" runat="server" Text='<% #eval("Nro_Apli") %>' Visible="false"></asp:Label></ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                    <RowStyle CssClass="formatUltcell" />
                                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                    </cc2:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                        AlternateText="Anterior" />
                                    <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                        AlternateText="Siguiente" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <img src="../../../Imagenes/Infografia/Anulado.gif" />
                                    <img src="../../../Imagenes/Infografia/Protestado.gif" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%--Botones--%>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="IB_BuscarPagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        TabIndex="1" ToolTip="Buscar Pagos" />
                                    <asp:ImageButton ID="IB_Protesto" runat="server" ImageUrl="~/Imagenes/Botones/boton_protestar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_protestar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_protestar_in.gif';"
                                        TabIndex="1" ToolTip="Protestar Pago" Enabled="False" />
                                    <asp:ImageButton ID="IB_Anular" runat="server" onmouseover="this.src='../../../Imagenes/Botones/boton_anular_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_anular_out.gif';" ImageUrl="~/Imagenes/Botones/boton_anular_out.gif"
                                        AlternateText="Anular Pago" TabIndex="3" ToolTip="Anular Pago" Enabled="False">
                                    </asp:ImageButton>
                                    <asp:ImageButton ID="IB_Imprimir" runat="server" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                                        TabIndex="2" ToolTip="Imprimir Pago"></asp:ImageButton>
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                                        AlternateText="Limpiar" TabIndex="3"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LB_CargaPagos" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscaDocCxC" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscarCliente" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscarClienteApli" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Lb_gri" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="HF_Pos_Ing" runat="server" />
            <asp:LinkButton ID="Lb_gri_Apl" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="HF_Id_Dpo" runat="server" />
            <asp:HiddenField ID="HF_id_ing" runat="server" />
            <asp:HiddenField ID="HF_Estado" runat="server" />
            <asp:HiddenField ID="HF_Pos_Doc_CxC" runat="server" />
            <asp:HiddenField ID="HF_Tipo" runat="server" />
            <asp:HiddenField ID="HF_ID_APL" runat="server" />
            <asp:HiddenField ID="HF_RUT_CLI" runat="server" />
            <asp:HiddenField ID="HF_RutCliente" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Anular" />
            <asp:PostBackTrigger ControlID="IB_Protesto" />
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="LB_BuscarDetallePago" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Anular" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Buscar" runat="server"></asp:LinkButton>
</asp:Content>
