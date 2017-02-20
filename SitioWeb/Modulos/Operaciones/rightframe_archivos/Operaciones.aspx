<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Operaciones.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Operaciones"
    Title="Consulta de Operaciones" %>

<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/WFIngresoOperaciones.js" type="text/javascript"></script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script src="../FuncionesPrivadasJS/Exportacion.js" type="text/javascript"></script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tbody>
                    <tr>
                        <td style="height: 31px" class="Cabecera">
                            <asp:Label ID="Label14" runat="server" CssClass="Titulos" Text="Operaciones - Consulta Operaciones"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="Contenido">
                            <table>
                                <tr>
                                    <td>
                                        <table cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Ejecutivo"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rb_ejec" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                    Text="Todos" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="dr_ejec" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dr_ejec_SelectedIndexChanged"
                                                                    Width="200px" CssClass="clsTxt">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:CheckBox ID="Ch_cliente" runat="server" AutoPostBack="True" CssClass="SubTitulos"
                                                        Text="Cliente" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="90px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                    Width="20px" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="ftx" runat="server" TargetControlID="Txt_Dig_Cli"
                                                                    FilterType="Numbers,Custom" ValidChars="k,K">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <%--Deudor--%>
                                        <table cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:CheckBox ID="Ch_deu" runat="server" AutoPostBack="True" CssClass="SubTitulos"
                                                        Text="Pagador" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" Width="90px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                    Width="20px" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Dig_Deu"
                                                                    FilterType="Numbers,Custom" ValidChars="k,K">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                             
                                                                <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Deudores" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <%--Tipo documento - moneda - estado--%>
                            <table>
                                <tr>
                                    <td valign="top">
                                        <table cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Estado"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height:33px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="Rb_est" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                    Text="Todos" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="dr_estado" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="dr_estado_SelectedIndexChanged" Width="200px" CssClass="clsTxt">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Moneda"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height:33px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="Rb_moneda" runat="server" AutoPostBack="True" Checked="True"
                                                                    CssClass="Label" Text="Todos" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="dr_moneda" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="dr_moneda_SelectedIndexChanged" Width="100px" CssClass="clsTxt">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellspacing="0" >
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" Text="C/S Recurso"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rb_resp" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="fec" runat="server" CssClass="SubTitulos" Text="Fecha de Operación"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height:33px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_fec_des" runat="server" CssClass="clsMandatorio" Width="90px"
                                                                    AutoPostBack="false"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txt_fec_des_CalendarExtender" runat="server" Enabled="True"
                                                                    TargetControlID="txt_fec_des" CssClass="radcalendar" FirstDayOfWeek="Monday">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txt_fec_des"
                                                                    Mask="99/99/9999" MaskType="Date">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_fec_has" runat="server" CssClass="clsMandatorio" Width="90px"
                                                                    AutoPostBack="false"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txt_fec_has_CalendarExtender" runat="server" Enabled="True"
                                                                    TargetControlID="txt_fec_has" CssClass="radcalendar" FirstDayOfWeek="Monday">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="mask" runat="server" TargetControlID="txt_fec_has" Mask="99/99/9999"
                                                                    MaskType="Date">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <%-- Rangos varios --%>
                            <table>
                                <tr>
                                    <td valign="top">
                                        <table cellspacing="0" width="270">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="Nº Otorgamiento"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_oto_des" runat="server" ReciveDecimal="False" Width="90px" CssClass="clsTxt"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_oto_des">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_oto_has" runat="server" ReciveDecimal="False" Width="90px" CssClass="clsTxt"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_oto_has">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table cellspacing="0" width="270">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Nº Documento"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_doc_des" runat="server" ReciveDecimal="False" Width="120px" MaxLength="20" CssClass="clsTxt"></asp:TextBox>
                                                              
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_doc_has" runat="server" ReciveDecimal="False" Width="120px" MaxLength="20" CssClass="clsTxt"></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table cellspacing="0" width="270">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label13" runat="server" CssClass="SubTitulos" Text="Fecha Vcto. de  Operación"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lal" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_venc_des" runat="server" Width="90px" CssClass="clsTxt" AutoPostBack="false"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txt_venc_des_CalendarExtender" runat="server" Enabled="True"
                                                                    TargetControlID="txt_venc_des" CssClass="radcalendar" FirstDayOfWeek="Monday">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txt_venc_des"
                                                                    Mask="99/99/9999" MaskType="Date">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_venc_has" runat="server" Width="90px" CssClass="clsTxt" AutoPostBack="false"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txt_venc_has_CalendarExtender" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                    TargetControlID="txt_venc_has" CssClass="radcalendar">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="txt_venc_has"
                                                                    Mask="99/99/9999"  MaskType="Date">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <%--Grillas --%>
                            <div style="text-align: left">
                                <cc1:TabContainer ID="TabGrillas" runat="server" Width="100%" 
                                    ActiveTabIndex="2" Height="350px">
                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Digitadas" >
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel2" runat="server" Height="330px" ScrollBars="Horizontal" Width="100%">
                                                        <asp:GridView ID="gr_dig" runat="server"  AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="NIT Cliente">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Razón Social">
                                                                    <ItemStyle Width="400px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="opn_fec_neg" HeaderText="Fec.Ope." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Ant." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Oper" DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Recurso" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Lineal" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Ope.">
                                                                    <ItemStyle Width="220px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Selección">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ID_OPE") %>'
                                                                            OnClick="Img_Ver_Click" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Label ID="lb_pag_dig" runat="server" CssClass="Label"></asp:Label><asp:HiddenField
                                                        ID="hf_nro_pag_dig" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Simuladas">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel1" runat="server" Height="330px" ScrollBars="Horizontal" Width="100%">
                                                        <asp:GridView ID="gr_sim" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            Width="100%" ShowHeader="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="NIT Cliente">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Razón Social">
                                                                    <ItemStyle Width="400px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="opn_fec_neg" HeaderText="Fec.Ope." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Ant." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Ope." DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Recurso" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Lineal" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Ope.">
                                                                    <ItemStyle Width="220px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ID_OPE") %>'
                                                                            OnClick="Img_Ver_Click1" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Label ID="lb_pag_sim" runat="server" CssClass="Label"></asp:Label><asp:HiddenField
                                                        ID="hf_nro_pag_sim" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Otorgadas">
                                        <ContentTemplate>
                                        
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel3" runat="server" Height="330px" ScrollBars="Horizontal" Width="100%">
                                                        <asp:GridView ID="gr_otg" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            ShowHeader="True" PageSize="12">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPO_OTG" HeaderText="Nº Otg.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="NIT Cliente">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Razón Social">
                                                                    <ItemStyle Width="400px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="opn_fec_neg" HeaderText="Fec.Ope." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Ant." NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Ope.">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Recurso" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Lineal" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Ope.">
                                                                    <ItemStyle Width="220px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ID_OPE") %>' OnClick="Img_Ver_Click2" />
                                                                        <asp:HiddenField ID="HF_ID_EVA" runat="server" Value='<%# Eval("ID_EVA")%>' />
                                                                        <asp:HiddenField ID="HF_ID_OPN" runat="server" Value='<%# Eval("ID_OPN")%>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Label ID="Lbl_pg_otg" runat="server" CssClass="Label"></asp:Label><asp:HiddenField
                                                        ID="hf_can_pag" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Pagadas">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel4" runat="server" Height="330px" ScrollBars="Horizontal" Width="100%">
                                                        <asp:GridView ID="gr_pag" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="NIT Cliente">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Razón Social">
                                                                    <ItemStyle Width="400px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="opn_fec_neg" HeaderText="Fec.Ope." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Ant." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Ope." DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Recurso" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Lineal" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Ope.">
                                                                    <ItemStyle Width="220px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ID_OPE") %>'
                                                                            OnClick="Img_Ver_Click3" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Label ID="Lbl_pag_pgd" runat="server" CssClass="Label"></asp:Label><asp:HiddenField
                                                        ID="Hf_pag_pgd" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Anuladas">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel5" runat="server" Height="330px" ScrollBars="Horizontal" Width="100%">
                                                        <asp:GridView ID="gr_anul" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="NIT Cliente">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Razón Social">
                                                                    <ItemStyle Width="400px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="opn_fec_neg" HeaderText="Fec.Ope" DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Ant." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Ope." DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Recurso" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Lineal" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Ope.">
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ID_OPE") %>'
                                                                            OnClick="Img_Ver_Click4" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Label ID="lbl_pag_anu" runat="server" CssClass="Label"></asp:Label>
                                                    <asp:HiddenField ID="Hf_pag_anu" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btn_prev_otg" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif'"
                                            ToolTip="Anterior" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_next_otg" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif'"
                                            ToolTip="Siguiente" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="Btn_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif'"
                                                ToolTip="Buscar Operaciones" ValidationGroup="Busca" />
                                        </td>
                                        <td>
                                           <asp:ImageButton ID="Btn_AdjDoc" runat="server" ImageUrl="~/Imagenes/Botones/boton_AdjDoc_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_AdjDoc_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_AdjDoc_in.gif'"
                                                tooltip="Adjuntar Digitalización" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_mod_ope" runat="server" ImageUrl="~/Imagenes/Botones/boton_mod_ope_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_mod_ope_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_mod_ope_in.gif'"
                                                ToolTip="Modificar Operaciones" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_inf_otg" runat="server" ImageUrl="~/Imagenes/Botones/boton_inf_otorg_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_inf_otorg_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_inf_otorg_in.gif'"
                                                ToolTip="Informe Otorgamiento" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_inf_eva" runat="server" ImageUrl="~/Imagenes/Botones/btn_infeva_in.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/btn_infeva_in.gif'" onmouseover="this.src='../../../Imagenes/Botones/btn_infeva_out.gif'"
                                                ToolTip="Informe Otorgamiento" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_inf_neg" runat="server" ImageUrl="~/Imagenes/Botones/btn_infneg_in.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/btn_infneg_in.gif'" onmouseover="this.src='../../../Imagenes/Botones/btn_infneg_out.gif'"
                                                ToolTip="Informe Otorgamiento" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_detope" OnClick="btn_detope_Click" runat="server" ImageUrl="~/Imagenes/Botones/boton_detalle_ope_out.gif"
                                                ForeColor="Transparent" BorderColor="Transparent" BackColor="Transparent" BorderStyle="None"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_ope_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_detalle_ope_in.gif'"
                                                ToolTip="Detalle Operacion" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="Btn_Anu" runat="server" OnClick="Btn_Anu_Click" ImageUrl="~/Imagenes/Botones/boton_anular_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_anular_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_anular_in.gif'"
                                                ToolTip="Anular Otorgamiento" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_imp" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif'"
                                                ToolTip="Reporte Operaciones" Enabled="False"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="Btn_Limpiar" OnClick="Btn_Limpiar_Click" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif'"
                                                ToolTip="Limpiar"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            
            
            <asp:LinkButton ID="lb_cli" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lb_deu" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="RetornaDoctos" runat="server" ForeColor="#0000CC"></asp:LinkButton>
            
            <asp:HiddenField ID="Txt_ItemOPE" runat="server" />
            <asp:HiddenField ID="pos_sim" runat="server" />
            <asp:HiddenField ID="pos_otg" runat="server" />
            <asp:HiddenField ID="pos_pag" runat="server" />
            <asp:HiddenField ID="pos_anu" runat="server" />
            <asp:HiddenField ID="ope_id" runat="server" />
            
            <asp:LinkButton ID="Lb_det_opes" TabIndex="52" OnClick="Lb_det_opes_Click" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender
                ID="ModalPopupExtender1" runat="server" PopupControlID="paneldet"
                EnableViewState="False" BackgroundCssClass="modalBackground" TargetControlID="Lb_det_opes"
                OkControlID="ok">
            </cc1:ModalPopupExtender>
            
            <asp:LinkButton ID="Doc_otg" TabIndex="52" OnClick="Lb_det_opes_Click" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender
                ID="ModalPopupExtender2" runat="server" PopupDragHandleControlID="Panel6" PopupControlID="Panel6"
                EnableViewState="False" BackgroundCssClass="modalBackground" TargetControlID="Doc_otg"
                OkControlID="ok">
            </cc1:ModalPopupExtender>
            
            <asp:Panel ID="Paneldet" runat="server" Width="800px" Height="600px" HorizontalAlign="Center" Style="display: none">
                <table style="width: 100%" class="Contenido">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="center">
                                <asp:Label ID="lbl_titulo" runat="server" CssClass="Titulos" Width="200px">Detalle Operación</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido">
                                <table>
                                   
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel8" runat="server" Height="420px" ScrollBars="Horizontal" Width="100%">
                                                <asp:GridView ID="Gr_Documentos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                    Height="0px" ShowHeader="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="deu_ide" HeaderText="Nit Pagador">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_num" HeaderText="Nº Docto.">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_flj_num" HeaderText="Nº Cuota">
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_mto" DataFormatString="{0:###,###,###.00}" HeaderText="Mto.Fin">
                                                            <ItemStyle Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="F.Vcto.">
                                                            <ItemStyle Width="90px" />
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
                                                    </Columns>
                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                    <RowStyle CssClass="formatUltcell" />
                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px" align="center">
                                <asp:ImageButton ID="ok" runat="server" ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif"
                                    BorderColor="Black"></asp:ImageButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            
            <asp:Panel ID="Panel6" runat="server" Width="1000px" Height="550px" HorizontalAlign="Center"
                Style="display: none">
                <table style="width: 100%" class="Contenido">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="center">
                                <asp:Label ID="Label17" runat="server" CssClass="Titulos" Width="200px">Detalle Operación</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido">
                                <table cellspacing="0">
                                    <tr>
                                        <td class="Cabecera">
                                            <table>
                                                <tr>
                                                    <td width="80">
                                                        <asp:Label ID="Label83" runat="server" CssClass="LabelCabeceraGrilla" Text="NIT Pagador"
                                                            Width="90px"></asp:Label>
                                                    </td>
                                                    <td width="200">
                                                        <asp:Label ID="Label84" runat="server" CssClass="LabelCabeceraGrilla" Text="Razón Social"></asp:Label>
                                                    </td>
                                                    <td width="100">
                                                        <asp:Label ID="Label85" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Docto."></asp:Label>
                                                    </td>
                                                    <td width="80">
                                                        <asp:Label ID="Label86" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Cuota"></asp:Label>
                                                    </td>
                                                    <td width="120">
                                                        <asp:Label ID="Label87" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Fin"></asp:Label>
                                                    </td>
                                                    <td width="80">
                                                        <asp:Label ID="Label88" runat="server" CssClass="LabelCabeceraGrilla" Text="F.Vcto"></asp:Label>
                                                    </td>
                                                    <td width="100">
                                                        <asp:Label ID="Label89" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                                    </td>
                                                    <td width="100">
                                                        <asp:Label ID="Label90" runat="server" CssClass="LabelCabeceraGrilla" Text="Est. Verifi."></asp:Label>
                                                    </td>
                                                    <td width="120">
                                                        <asp:Label ID="Label91" runat="server" CssClass="LabelCabeceraGrilla" Text="Saldo Cliente"></asp:Label>
                                                    </td>
                                                    <td width="120">
                                                        <asp:Label ID="Label92" runat="server" CssClass="LabelCabeceraGrilla" Text="Saldo Pagador"></asp:Label>
                                                    </td>
                                                    <td width="90">
                                                        <asp:Label ID="Label15" runat="server" CssClass="LabelCabeceraGrilla" Text="Cal. Oto"></asp:Label>
                                                    </td>
                                                    <td width="90">
                                                        <asp:Label ID="Label18" runat="server" CssClass="LabelCabeceraGrilla" Text="Cal. Obj"></asp:Label>
                                                    </td>
                                                    <td width="90">
                                                        <asp:Label ID="Label19" runat="server" CssClass="LabelCabeceraGrilla" Text="Cal. Sub"></asp:Label>
                                                    </td>
                                                    <td width="90">
                                                        <asp:Label ID="Label20" runat="server" CssClass="LabelCabeceraGrilla" Text="Cal. Arr"></asp:Label>
                                                    </td>
                                                    <td width="90">
                                                        <asp:Label ID="Label21" runat="server" CssClass="LabelCabeceraGrilla" Text="Cal. Def"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <asp:Panel ID="Panel7" runat="server" CssClass="Contenido" Height="400px" ScrollBars="Vertical" Width="100%">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                    Height="0px" ShowHeader="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Deudor" HeaderText="Razón Social">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_num" HeaderText="Nro.Documento">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_flj_num" HeaderText="Nro.C">
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dsi_mto" DataFormatString="{0:###,###,###.00}" HeaderText="Monto Financiado">
                                                            <ItemStyle Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="doc_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto">
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EstadoDocto" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Estado">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EstadoVerifica" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Estado Ver.">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="doc_sdo_cli" DataFormatString="{0:###,###,###.00}" HeaderText="Sdo.Cli">
                                                            <ItemStyle Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="doc_sdo_ddr" DataFormatString="{0:###,###,###.00}" HeaderText="Sdo.Deu">
                                                            <ItemStyle Width="120px" />
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
                                                    </Columns>
                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px" align="center">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/boton_aceptar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_aceptar_in.gif';"
                                    BorderColor="Black"></asp:ImageButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel1">
                <ProgressTemplate>
                    <uc1:Cargando ID="Cargando1" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_imp" />
            <asp:PostBackTrigger ControlID="btn_inf_otg" />
            <asp:PostBackTrigger ControlID="Btn_AdjDoc" />
            <asp:PostBackTrigger ControlID="btn_inf_eva" />
            <asp:PostBackTrigger ControlID="btn_inf_neg" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="lb_anu" TabIndex="52" runat="server"></asp:LinkButton>
    
</asp:Content>
