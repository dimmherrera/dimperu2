<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="MLineaCredito.aspx.vb" Inherits="MLineaCredito" Title="Mantención de Linea de Financiamiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script src="../FuncionesPrivadasJS/LineaCredito.js" type="text/javascript"></script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_LCredito">
        <ProgressTemplate>
            <uc2:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UP_LCredito">
        <ContentTemplate>
            <table style="position: static; text-align: -moz-center" cellspacing="1" cellpadding="0"
                width="100%" border="0" class="Contenido">
                <tbody>
                    <tr>
                        <td style="height: 31px; text-align: -moz-center" class="Cabecera" align="center"
                            valign="middle">
                            <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="Linea de Financiamiento"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="center" style="text-align: -moz-center">
                            <asp:Panel Style="position: static" ID="Panel1" runat="server" Width="100%" Height="600px"
                                ScrollBars="Vertical">
                                <table cellspacing="5" cellpadding="4" border="0" width="1000px">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <table style="position: static" id="TABLE1" cellspacing="0" cellpadding="0" width="100%"
                                                    border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="height: 30px">
                                                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label1" runat="server"
                                                                    CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" valign="top" height="50">
                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                                                            </td>
                                                                            <td valign="middle" align="left">
                                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" 
                                                                                    Width="90px" TabIndex="1"></asp:TextBox>
                                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                                                    Width="15px" TabIndex="2" AutoPostBack="True"></asp:TextBox>
                                                                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                                </cc2:FilteredTextBoxExtender>
                                                                                <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left" 
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                                </cc2:MaskedEditExtender>
                                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                    Width="20px" Style="margin-top: 0px" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label Style="position: static" ID="Label13" runat="server" CssClass="Label"
                                                                                    Text="Razón Soc."></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox Style="position: static" ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled"
                                                                                    Width="300px" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label Style="position: static" ID="Label14" runat="server" CssClass="Label"
                                                                                    Text="Ejecutivo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox Style="position: static" ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled"
                                                                                    Width="300px" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label Style="position: static" ID="Label15" runat="server" CssClass="Label"
                                                                                    Text="Sucursal"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox Style="position: static" ID="Txt_Sucursal" runat="server" CssClass="clsDisabled"
                                                                                    Width="300px" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                  </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <asp:HiddenField ID="Txt_Pos_Lin" runat="server"></asp:HiddenField>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="position: static" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="Cabecera" align="left">
                                                                <asp:Label ID="Label34" runat="server" CssClass="SubTitulos" Text="Lineas de Créditos"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" valign="top" align="left">
                                                                <asp:Panel Style="position: static" ID="Panel2" runat="server" Height="110px" ScrollBars="Vertical">
                                                                    <asp:GridView ID="GV_LineaCredito" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                                                        CssClass="formatUltcell" EnableTheming="True" Style="position: static">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Seleccion" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-Width="90px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                                                        ToolTip='<%# Eval("id_ldc") %>' style="height: 13px" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="id_ldc" HeaderText="Nº Linea">
                                                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_est_des" HeaderText="Estado Linea">
                                                                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_fec_rsn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec. Resolución"
                                                                                HtmlEncode="False">
                                                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_fec_vig_dde" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec. Desde"
                                                                                HtmlEncode="False">
                                                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_fec_vig_hta" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec. Hasta"
                                                                                HtmlEncode="False">
                                                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_mto_sol" DataFormatString="{0:#,###,###,##0}" HeaderText="Mto. Solicitado"
                                                                                HtmlEncode="False">
                                                                                <ItemStyle HorizontalAlign="right" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_mto_apb" DataFormatString="{0:#,###,###,##0}" HeaderText="Mto. Aprobado">
                                                                                <ItemStyle HorizontalAlign="right" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_mto_ocp" DataFormatString="{0:#,###,###,##0}" HeaderText="Mto. Ocupado">
                                                                                <ItemStyle HorizontalAlign="right" Width="100" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ldc_tip_com" HeaderText="Tipo Comisión">
                                                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        <RowStyle CssClass="formatUltcell" />
                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="position: static" cellspacing="2" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <table style="position: static" id="Table2" cellspacing="0" cellpadding="0" width="100%"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label2" runat="server"
                                                                                    CssClass="SubTitulos" Text="Datos Linea"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top" height="70" align="left">
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Nro. de Linea"></asp:Label>
                                                                                            </td>
                                                                                            <td valign="middle" align="left">
                                                                                                <asp:TextBox ID="Txt_NroLinea" runat="server" CssClass="clsDisabled" Width="96px"
                                                                                                    MaxLength="1" ReadOnly="True"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="right">
                                                                                                <asp:Label Style="position: static" ID="Label8" runat="server" CssClass="Label" Text="Monto Solicitud"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="Txt_Mto_Sol" runat="server" CssClass="clsDisabled" Width="96px"
                                                                                                    MaxLength="16" ReadOnly="True"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Mto_Sol_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Mto_Sol">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label Style="position: static" ID="Label5" runat="server" CssClass="Label" Text="Estado Linea"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:DropDownList Style="position: static" ID="DP_EstadoLinea" runat="server" CssClass="clsDisabled"
                                                                                                    Width="100px" Enabled="False">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td align="right">
                                                                                                <asp:Label Style="position: static" ID="Label6" runat="server" CssClass="Label" Text="Monto Disponible"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="Txt_Mto_Dis" runat="server" CssClass="clsDisabled" Width="96px"
                                                                                                    MaxLength="1" ReadOnly="True"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label Style="position: static" ID="Label7" runat="server" CssClass="Label" Text="Fecha Solicitud"></asp:Label>
                                                                                            </td>
                                                                                            <td style="height: 22px" align="left">
                                                                                                <asp:TextBox ID="Txt_Fec_Sol" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                    MaxLength="1" ReadOnly="True"></asp:TextBox>
                                                                                                <%--<cc1:DateBox Style="position: static" ID="Txt_Fec_Sol" runat="server" CssClass="clsDisabled"
                                                                                                    ReadOnly="True"></cc1:DateBox>--%>
                                                                                                <cc2:CalendarExtender ID="Txt_Fec_Sol_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                    Enabled="False" TargetControlID="Txt_Fec_Sol" FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                                                                                                </cc2:CalendarExtender>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                &nbsp;
                                                                <table style="position: static" id="Table4" cellspacing="0" cellpadding="0" width="100%"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label27" runat="server"
                                                                                    CssClass="SubTitulos" Text="Tipo de Comisión"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top">
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:RadioButton Style="position: static" ID="RB_Normal" runat="server" CssClass="Label"
                                                                                                    Text="Normal" Enabled="False" GroupName="Comision"></asp:RadioButton>
                                                                                            </td>
                                                                                            <td valign="middle" align="center">
                                                                                                <asp:RadioButton Style="position: static" ID="RB_Especial" runat="server" CssClass="Label"
                                                                                                    Text="Especial" Enabled="False" GroupName="Comision"></asp:RadioButton>
                                                                                            </td>
                                                                                            <td valign="middle" align="center">
                                                                                                <asp:TextBox Style="position: static" ID="Txt_Obs_Com" runat="server" CssClass="clsDisabled"
                                                                                                    Width="250px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <table style="position: static" id="Table3" cellspacing="0" cellpadding="0" width="100%"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label9" runat="server"
                                                                                    CssClass="SubTitulos" Text="Aprobación Comite"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top" height="70">
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Fecha Desde"></asp:Label>
                                                                                            </td>
                                                                                            <td valign="middle" align="left">
                                                                                                <asp:TextBox ID="Txt_Fec_Dsd" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                    MaxLength="1" ReadOnly="True"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Dsd">
                                                                                                </cc2:MaskedEditExtender>
                                                                                                <cc2:CalendarExtender ID="Txt_Fec_Dsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                    Enabled="false" FirstDayOfWeek="Monday" TargetControlID="Txt_Fec_Dsd" Format="dd-MM-yyyy">
                                                                                                </cc2:CalendarExtender>
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                                <asp:Label ID="Label19" runat="server" CssClass="Label" Style="position: static"
                                                                                                    Text="Fecha Resolución"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                                <asp:TextBox ID="Txt_Fec_Res" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                    MaxLength="1" ReadOnly="True"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Res_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Res">
                                                                                                </cc2:MaskedEditExtender>
                                                                                                <cc2:CalendarExtender ID="Txt_Fec_Res_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                    Enabled="false" TargetControlID="Txt_Fec_Res" FirstDayOfWeek="Monday" Format="dd-MM-yyyy">
                                                                                                </cc2:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label Style="position: static" ID="Label17" runat="server" CssClass="Label"
                                                                                                    Text="Fecha Hasta"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="Txt_Fec_Hta" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                    MaxLength="1" ReadOnly="True"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Hta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Hta">
                                                                                                </cc2:MaskedEditExtender>
                                                                                                <cc2:CalendarExtender ID="Txt_Fec_Hta_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                    Enabled="false" TargetControlID="Txt_Fec_Hta" FirstDayOfWeek="Monday" Format="dd-MM-yyyy">
                                                                                                </cc2:CalendarExtender>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label Style="position: static" ID="Label21" runat="server" CssClass="Label"
                                                                                                    Text="Monto Aprobado"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="Txt_Mto_Apr" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                    MaxLength="16" ReadOnly="True"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Mto_Apr_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Mto_Apr">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        <td style="height: 22px" align="right">
                                                                                                <asp:Label Style="position: static" ID="Label4" runat="server" CssClass="Label" Text="Porc. Exceso"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="Txt_Por_Exc" runat="server" CssClass="clsDisabled" Width="50px"
                                                                                                    MaxLength="5" ReadOnly="true"></asp:TextBox>
                                                                                                <asp:Label ID="Label11" runat="server" Text="%" CssClass="Label"></asp:Label>
                                                                                                <cc2:FilteredTextBoxExtender ID="Txt_Por_Exc_FilteredTextBoxExtender" runat="server"
                                                                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Por_Exc" ValidChars="," >
                                                                                                </cc2:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <br />
                                                                <table style="position: static" id="Table5" cellspacing="0" cellpadding="0" width="100%"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label30" runat="server"
                                                                                    CssClass="SubTitulos" Text="Observación"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top" align="left">
                                                                                <asp:TextBox Style="position: static" ID="Txt_Observacion" runat="server" CssClass="clsDisabled"
                                                                                    Width="90%" Height="40px" MaxLength="250" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="position: static" id="Tcl_Grillas" cellspacing="2" cellpadding="0"
                                                    border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <table style="position: static" cellspacing="0" cellpadding="0" width="300" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label ID="Label26" runat="server" CssClass="SubTitulos" Text="Porcentaje a Anticipar"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top" align="left" height="50">
                                                                                <%--<table class="cabeceraGrilla">
                                                                                    <tr>
                                                                                        <td Width="100"><asp:Label ID="Label37" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D."></asp:Label></td>
                                                                                        <td Width="50"><asp:Label ID="Label39" runat="server" CssClass="LabelCabeceraGrilla" Text="%"></asp:Label></td>
                                                                                        <td Width="50"><asp:Label ID="Label40" runat="server" CssClass="LabelCabeceraGrilla" Text="Ver."></asp:Label></td>
                                                                                        <td Width="50"><asp:Label ID="Label41" runat="server" CssClass="LabelCabeceraGrilla" Text="Not."></asp:Label></td>
                                                                                        <td Width="50"><asp:Label ID="Label43" runat="server" CssClass="LabelCabeceraGrilla" Text="Cob."></asp:Label></td>
                                                                                    </tr>
                                                                                </table>--%>
                                                                                <asp:Panel Style="position: static" ID="Panel3" runat="server" Height="90px" ScrollBars="Vertical">
                                                                                    <asp:GridView ID="GV_PorcentajeAnt" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                                                        CssClass="formatUltcell" EnableTheming="True" HorizontalAlign="Center" PageSize="3"
                                                                                        ShowHeader="true" Style="position: static">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="id_P_0031_des" HeaderText="T.P.">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="apc_pct" HeaderText="%">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="apc_ver_son" HeaderText="Ver.">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="apc_not_son" HeaderText="Not.">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="apc_cob_son" HeaderText="Cob.">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                        <RowStyle CssClass="formatUltcell" />
                                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table style="position: static" cellspacing="0" cellpadding="0" width="220" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label ID="Label31" runat="server" CssClass="SubTitulos" Text="SubLineas (Productos)"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top" align="left" height="50">
                                                                                <asp:Panel Style="position: static" ID="Panel4" runat="server" Height="90px" ScrollBars="Vertical">
                                                                                    <asp:GridView ID="GV_Productos" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                                                        CssClass="formatUltcell" EnableTheming="True" Style="position: static" HorizontalAlign="Center"
                                                                                        PageSize="3" ShowHeader="true">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="id_P_0031_des" HeaderText="Producto">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="sbl_mto" HeaderText="Monto" DataFormatString="{0:###,###,##0}">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="sbl_pct" HeaderText="%">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                        <RowStyle CssClass="formatUltcell" />
                                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table style="position: static" cellspacing="0" cellpadding="0" width="450" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="Cabecera" align="left">
                                                                                <asp:Label ID="Label42" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Contenido" valign="top" align="left" height="50">
                                                                                <asp:Panel Style="position: static" ID="Panel5" runat="server" Height="90px" ScrollBars="Vertical">
                                                                                    <asp:GridView Style="position: static" ID="GV_Deudor" runat="server" CssClass="formatUltcell"
                                                                                        PageSize="3" HorizontalAlign="Center" EnableTheming="True" CellPadding="2" ShowHeader="true"
                                                                                        AutoGenerateColumns="False">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="deu_nom" HeaderText="Razón Social">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="sbl_mto" HeaderText="Mto."
                                                                                                DataFormatString="{0:###,###,##0}">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="sbl_pct" HeaderText="%">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                        <RowStyle CssClass="formatUltcell" />
                                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="IB_Reglamento" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Btn_Reglamento_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/Btn_Reglamento_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Btn_Reglamento_in.gif';" 
                                Style="position: static" ToolTip="Bajar Reglamento" Visible="false" />
                                
                            <asp:ImageButton ID="IB_HFirma" runat="server" 
                                ImageUrl="~/Imagenes/Botones/boton_HF_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_HF_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/boton_HF_in.gif';" 
                                Style="position: static" ToolTip="Generar Hoja de Firmas" Visible="false" />
                                
                            <asp:ImageButton Style="position: static" ID="IB_Pagare" onmouseover="this.src='../../../Imagenes/Botones/boton_pagare_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_pagare_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/boton_pagare_out.gif" Visible="false" ToolTip="Generar Pagare en Blanco">
                            </asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_Actas" onmouseover="this.src='../../../Imagenes/Botones/boton_actas_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_Actas_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/boton_Actas_out.gif" Visible="false" ToolTip="Adjuntar Actas">
                            </asp:ImageButton>
                            
                             <asp:ImageButton Style="position: static" ID="IB_Ficha" onmouseover="this.src='../../../Imagenes/Botones/boton_ficha_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_ficha_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/boton_ficha_out.gif" Visible="false" ToolTip="Adjuntar Fichas Juridicas">
                            </asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_VistoBueno" onmouseover="this.src='../../../Imagenes/Botones/Boton_VB_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_VB_out.gif';" OnClick="IB_VistoBueno_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_VB_out.gif" Visible="False"
                                ToolTip="Dar Visto Bueno"></asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" CausesValidation="false" ID="IB_Buscar"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';"
                                OnClick="IB_Buscar_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif"
                                ToolTip="Buscar Linea de Crédito"></asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_Nuevo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" OnClick="IB_Nuevo_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" ToolTip="Nueva Linea de Crédito">
                            </asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_Anticipo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Anticipo_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Anticipo_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/Boton_Anticipo_out.gif" >
                            </asp:ImageButton>
                           
                            <asp:ImageButton ID="IB_Comision" runat="server" onmouseover="this.src='../../../Imagenes/Botones/button_comi_in.png';"
                                onmouseout="this.src='../../../Imagenes/Botones/button_comi_out.png';" ImageUrl="~/Imagenes/Botones/button_comi_out.png" />
                           
                            <asp:ImageButton ID="IB_Gastos" runat="server" onmouseover="this.src='../../../Imagenes/Botones/button_gastos_in.png';"
                                onmouseout="this.src='../../../Imagenes/Botones/button_gastos_out.png';" ImageUrl="~/Imagenes/Botones/button_gastos_out.png" />
                           
                            <asp:ImageButton Style="position: static" ID="IB_SubLinea" onmouseover="this.src='../../../Imagenes/Botones/Boton_SubLinea_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_SubLinea_out.gif';" OnClick="IB_SubLinea_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_SubLinea_out.gif" ToolTip="Ver Sub Lineas">
                            </asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_Imprimir" onmouseover="this.src='../../../Imagenes/Botones/Boton_Imprimir_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Imprimir_out.gif';" OnClick="IB_Imprimir_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Imprimir_out.gif" CausesValidation="false"
                                ValidationGroup="LineaCredito" ToolTip="Imprimir Linea de Financiamiento" Enabled="False">
                            </asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="IB_Guardar_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" CausesValidation="false"
                                ValidationGroup="LineaCredito" ToolTip="Guardar Datos" Enabled="False"></asp:ImageButton>
                           
                            <asp:ImageButton Style="position: static" ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" ToolTip="Limpiar Pagina">
                            </asp:ImageButton>
                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:LinkButton ID="LB_Refrescar" runat="server"></asp:LinkButton>
            <asp:LinkButton Style="position: static" ID="LB_CargaDatosLinea" OnClick="LB_CargaDatosLinea_Click"
                CausesValidation="false" runat="server"></asp:LinkButton>
            <asp:LinkButton Style="position: static" ID="LB_Buscar" OnClick="LB_Buscar_Click"
                CausesValidation="false" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="Accion" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="NroLinea" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hf_spread" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Reglamento" />
            <asp:PostBackTrigger ControlID="IB_HFirma" />
            <asp:PostBackTrigger ControlID="IB_Pagare" />
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
            <asp:PostBackTrigger ControlID="IB_Actas" />
       </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Aprobar" runat="server"></asp:LinkButton>
</asp:Content>
