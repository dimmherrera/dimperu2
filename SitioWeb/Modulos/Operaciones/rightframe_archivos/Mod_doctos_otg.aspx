<%@ Page Title="Consulta de Documentos" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="Mod_doctos_otg.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Mod_doctos_otg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
        pageRequestManager.add_pageLoading(onPageLoading);

        function onPageLoading() {
            var gv = $get("Gr_Documentos");
            if (gv != null) {
                gv.parentNode.removeNode(gv);
            }
        } 
    </script>--%>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" style="width: 100%; height: 630px" class="Contenido">
                <tr>
                    <td style="height: 31px;" class="Cabecera">
                        <asp:Label ID="Label15" runat="server" Text="Operaciones - Consulta de Documentos"
                            CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido" align="center">
                        <%--Criterio de busqueda--%>
                        <table cellspacing="1">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Criterios de  Búsqueda "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:CheckBox ID="Ch_cli" runat="server" AutoPostBack="True" CssClass="SubTitulos"
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
                                                                                    <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_rut_cli">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                                        Width="20px" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="k,K">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="ib_ayudacli" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                        Width="20px" Enabled="False" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                        Width="210px"></asp:TextBox>
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
                                                                                        CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_rut_deu">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                                        Width="20px" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="k,K">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="IB_AyudaDeu" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                        Width="20px" Enabled="False" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                        Width="210px"></asp:TextBox>
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
                                                                        <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Nº Operación"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" height="27px">
                                                                        <asp:TextBox ID="txt_oto_des" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_oto_des_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="txt_oto_des">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" Text="Nro Docto"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Contenido" height="27px">
                                                                        <asp:TextBox ID="txt_doc_des" runat="server" CssClass="clsTxt" Width="120px" MaxLength="20"></asp:TextBox>
                                                                        
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:CheckBox ID="Ch_obl" runat="server" AutoPostBack="True" CssClass="SubTitulos"
                                                                            Text="Oblig." />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" height="27">
                                                                        <asp:RadioButtonList ID="rb_con_obl" runat="server" CellPadding="0" CellSpacing="0"
                                                                            CssClass="Label" Enabled="False" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="S">Si</asp:ListItem>
                                                                            <asp:ListItem Value="N">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label432" runat="server" CssClass="SubTitulos" Text="Est Docto"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" height="27">
                                                                        <asp:DropDownList ID="dr_est_doc" runat="server" Width="200px" CssClass="clsTxt">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="SubTitulos" Text="Fecha Vcto"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" height="27">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label3" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_venc_des" runat="server" CssClass="clsTxt" Width="90px" AutoPostBack="false"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txt_venc_des_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_venc_des">
                                                                                    </cc1:CalendarExtender>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_venc_des"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label16" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_venc_has" runat="server" CssClass="clsTxt" Width="90px" AutoPostBack="false"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txt_venc_has_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_venc_has">
                                                                                    </cc1:CalendarExtender>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txt_venc_has"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc1:MaskedEditExtender>
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
                                                                        <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Tipo Docto"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" height="27">
                                                                        <asp:DropDownList ID="dr_tip_doc" runat="server" Width="200px" CssClass="clsTxt">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" Text="Fecha Otorg"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" height="27">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label7" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_fec_des" runat="server" CssClass="clsTxt" Width="90px" AutoPostBack="false"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txt_fec_des_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_fec_des">
                                                                                    </cc1:CalendarExtender>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_fec_des"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label8" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_fec_has" runat="server" CssClass="clsTxt" Width="90px" AutoPostBack="false"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txt_fec_has_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_fec_has">
                                                                                    </cc1:CalendarExtender>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_fec_has"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Nro. Contrato"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" height="27">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Nro."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Contrato" runat="server" CssClass="clsTxt" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel_Gr_Documentos" runat="server" Width="1900px" Height="400px"
                            ScrollBars="Horizontal" CssClass="Contenido">
                           
                            <asp:GridView ID="Gr_Documentos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                PageSize="1" ShowHeader="True" Width="2500px">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Ch_sel" runat="server" AutoPostBack="True" OnCheckedChanged="Ch_sel_CheckedChanged"
                                                ToolTip='<%#Eval("id_doc")%>' />
                                            <asp:ImageButton ID="IB_ID_Doc" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                ToolTip='<%#Eval("id_dsi")%>' onclick="IB_ID_Doc_Click" />    
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cli_idc" HeaderText="NIT Cli." HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cli_rso" HeaderText="Cliente" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_ide" HeaderText="NIT Pag" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DEUDOR" HeaderText="Pagador" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoDoctoCorta" HeaderText="Tipo Doc." HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_opn" HeaderText="Nº Ope." HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_num" HeaderText="Nº Docto" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Contrato" HeaderText="Contrato" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="doc_fev_rea" HeaderText="Fec.Vcto." DataFormatString="{0:dd/MM/yyyy}"
                                        HtmlEncode="False" HtmlEncodeFormatString="False" >
                                        <ItemStyle Width="90px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EstadoDocto" HeaderText="Estado" HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="200px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="moneda" HeaderText="Moneda" HtmlEncode="False">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="doc_sdo_cli" HeaderText="Saldo Cli." DataFormatString="{0:###,###,##0}"
                                        HtmlEncode="False">
                                        <ItemStyle Width="160px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="doc_sdo_ddr" HeaderText="Saldo Pag." DataFormatString="{0:###,###,##0}"
                                        HtmlEncode="False">
                                        <ItemStyle Width="160px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="doc_not_cre" HeaderText="Not.Crédito" HtmlEncode="False">
                                        <ItemStyle Width="160px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_ntf" HeaderText="Notif." HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_cbz_son" HeaderText="Con Cob." HtmlEncode="False"
                                        HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOC_FEC_DEM" HeaderText="Demanda" DataFormatString="{0:dd/MM/yyyy}"
                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOC_FEC_CAS" HeaderText="Obs.Cobranza" DataFormatString="{0:dd/MM/yyyy}"
                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOC_OBS_COB" HeaderText="Obser.Cobranza" ConvertEmptyStringToNull="False"
                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OPO_FEC_OTO" HeaderText="Fec.Otorg." DataFormatString="{0:dd/MM/yyyy}"
                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_con_cbz" HeaderText="Cod.Cobr." ConvertEmptyStringToNull="False"
                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="id_doc" runat="server" Text='<%#Eval("id_doc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="id_dsi" runat="server" Text='<%#Eval("id_dsi")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="recurso" HeaderText="Recurso" ConvertEmptyStringToNull="false" HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ItemStyle Width="90px" />
                                      </asp:BoundField>
                                    <asp:BoundField DataField="cal_oto_gam" HeaderText="Cal. Oto" ConvertEmptyStringToNull="false" HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cal_obj_eti" HeaderText="Cal. Obj" ConvertEmptyStringToNull="false" HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cal_sub_jet" HeaderText="Cal. Sub" ConvertEmptyStringToNull="false" HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cal_arr_ast" HeaderText="Cal. Arr" ConvertEmptyStringToNull="false" HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ItemStyle Width="90px" />
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="cal_def_ini" HeaderText="Cal. Def" ConvertEmptyStringToNull="false" HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                   </Columns>
                                <HeaderStyle CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                            </asp:GridView>
                            
                        </asp:Panel>
                        <asp:Label ID="paginas" runat="server"></asp:Label>
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="btn_prev_otg" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif'"
                                        ToolTip="Anterior" />
                                    <asp:ImageButton ID="btn_next_otg" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif'"
                                        ToolTip="Siguiente" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="IB_Cal_Arr" runat="server" Style="position: static" onmouseover="this.src='../../../Imagenes/Botones/Btn_CalArr_In.gif';" 
                            onmouseout="this.src='../../../Imagenes/Botones/Btn_CalArr_Out.gif';" ImageUrl="~/Imagenes/Botones/Btn_CalArr_Out.gif" 
                            ToolTip="Carga Masiva Calificacion de Arrastre" AlternateText="Cal. Arrastre"/>
                        <asp:ImageButton Style="position: static" ID="IB_Calif" onmouseover="this.src='../../../Imagenes/Botones/boton_Calif_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_Calif_out.gif';" ImageUrl="~/Imagenes/Botones/boton_Calif_out.gif"
                            runat="server" ToolTip="Calificaciones por Documento" Enabled="false"></asp:ImageButton>
                        <asp:ImageButton ID="btn_clasif" runat="server" ImageUrl="~/Imagenes/Botones/boton_clasif_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_clasif_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_clasif_in.gif';"
                            ToolTip="Clasificación Riesgo" Enabled="False" />
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                            ToolTip="Buscar Documentos" />
                        <asp:ImageButton ID="Ib_imprimir" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            ToolTip="Buscar Documentos" Enabled="False" />
                        <asp:ImageButton ID="Ib_modificar" runat="server" ImageUrl="~/Imagenes/Botones/btn_prot_doc_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/btn_prot_doc_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_prot_doc_in.gif';"
                            ToolTip="Modificar Documentos" Enabled="False" />
                        <asp:ImageButton ID="btn_limp" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                            ValidationGroup="Limpiar" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hf_nro_pag" runat="server" />
            <asp:HiddenField ID="HF_Posicion" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <uc1:Cargando ID="Cargando1" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Calif" />
            <asp:PostBackTrigger ControlID="btn_clasif" />
            <asp:PostBackTrigger ControlID="Ib_modificar" />
            <asp:PostBackTrigger ControlID="btn_buscar" />
            <asp:PostBackTrigger ControlID="Ib_imprimir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
