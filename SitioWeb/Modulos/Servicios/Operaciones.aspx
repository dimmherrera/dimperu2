<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Operaciones.aspx.vb" Inherits="Modulos_Servicios_Operaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Servicio de Informe de Operaciones</title>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <script src="../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../Carp.%20Comercial/FuncionesPrivadasJS/EvaluacionCliDeu.js" type="text/javascript"></script>

    <script src="../../FuncionesJS/Grilla.js" type="text/javascript"></script>

    <script src="../Operaciones/FuncionesPrivadasJS/WFIngresoOperaciones.js" type="text/javascript"></script>
</head>
<body>

    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table id="tb_gral" cellspacing="0" cellpadding="0" width="1020" border="0">
        <tr>
            <td class="Cabecera" height="31px">
                <asp:Label ID="Label3" runat="server" CssClass="Titulos" Text="Servicio de Informes de Operaciones"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" style="padding: 10px">
                
                <%--Criterio de Busqueda--%>
                <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label ID="Label132" runat="server" CssClass="SubTitulos" Text="Cliente" __designer:wfdid="w284"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" style="height: 70px">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                    CssClass="clsMandatorio" Width="90px" __designer:wfdid="w286" ></asp:TextBox>
                                                <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                    CssClass="clsMandatorio" Width="15px" __designer:wfdid="w286" MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                <a href="javascript:WinOpen(2,'../Ayudas/AyudaCli.aspx','PopUpCliente',580,410,200,150);">
                                                    <img id="Img_AyudaCli" tabindex="3" src="../../Imagenes/Iconos/155.ICO" width="20"
                                                        border="0" />
                                                </a>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                    Text="Razon Soc." Width="70px"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                     ReadOnly="True" Style="position: static" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                    Text="Fecha Desde"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Fecha_Dsd" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_Fecha_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Dsd">
                                                </cc1:MaskedEditExtender>
                                                <cc1:CalendarExtender ID="Txt_Fecha_Dsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Dsd">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                    Text="Fecha Hasta"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Fecha_Hst" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Hst">
                                                </cc1:MaskedEditExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar"
                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Hst">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                <br />
                
                <%--Grillas de Operaciones--%>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <cc1:TabContainer ID="TabGrillas" runat="server" Width="100%" ActiveTabIndex="0">
                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Digitadas">
                                    <HeaderTemplate>
                                        Digitadas
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td width="80">
                                                                <asp:Label ID="Label18" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Ope"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label19" runat="server" CssClass="LabelCabeceraGrilla" Text="Rut Cli."></asp:Label>
                                                            </td>
                                                            <td width="200">
                                                                <asp:Label ID="Label20" runat="server" CssClass="LabelCabeceraGrilla" Text="Raz.Soc."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label21" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label22" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D        "></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label23" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label24" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec.Ope."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label25" runat="server" CssClass="LabelCabeceraGrilla" Text="Sdo.Pagar"></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label26" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ant."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label27" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ope."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label28" runat="server" CssClass="LabelCabeceraGrilla" Text="Resp."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label29" runat="server" CssClass="LabelCabeceraGrilla" Text="Lineal"></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label30" runat="server" CssClass="LabelCabeceraGrilla" Text="Puntual"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:Panel ID="Panel2" runat="server" Height="285px" ScrollBars="Auto" Width="100%">
                                                        <asp:GridView ID="gr_dig" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            Width="100%" ShowHeader="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_OPE">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="Rut Cli.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Raz.Soc">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_FEC" HeaderText="Fec.Oper." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Anti." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Oper" DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Con Resp." NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Op.Lin" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Ope.Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lb_pag_dig" runat="server" CssClass="Label"></asp:Label>
                                        <asp:HiddenField ID="hf_nro_pag_dig" runat="server" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Simuladas">
                                    <ContentTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td width="80">
                                                                <asp:Label ID="Label15" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Ope"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label32" runat="server" CssClass="LabelCabeceraGrilla" Text="Rut Cli."></asp:Label>
                                                            </td>
                                                            <td width="200">
                                                                <asp:Label ID="Label33" runat="server" CssClass="LabelCabeceraGrilla" Text="Raz.Soc."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label34" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label35" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D        "></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label36" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label37" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec.Ope."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label38" runat="server" CssClass="LabelCabeceraGrilla" Text="Sdo.Pagar"></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label39" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ant."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label40" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ope."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label41" runat="server" CssClass="LabelCabeceraGrilla" Text="Resp."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label42" runat="server" CssClass="LabelCabeceraGrilla" Text="Lineal"></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label70" runat="server" CssClass="LabelCabeceraGrilla" Text="Puntual"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:Panel ID="Panel1" runat="server" Height="285px" ScrollBars="Auto" Width="100%">
                                                        <asp:GridView ID="gr_sim" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            Width="100%" ShowHeader="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_OPE" HeaderText="Nº Oper.">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="Rut Cli.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Raz.Soc">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_FEC" HeaderText="Fec.Oper." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Anti." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Oper" DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Con Resp." NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Op.Lin" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Ope.Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lb_pag_sim" runat="server" CssClass="Label"></asp:Label>
                                        <asp:HiddenField ID="hf_nro_pag_sim" runat="server" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Otorgadas">
                                    <ContentTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <table>
                                                        <tr>
                                                            <td width="80">
                                                                <asp:Label ID="Label43" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Ope"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label56" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Otg."></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label44" runat="server" CssClass="LabelCabeceraGrilla" Text="Rut Cli."></asp:Label>
                                                            </td>
                                                            <td width="200">
                                                                <asp:Label ID="Label45" runat="server" CssClass="LabelCabeceraGrilla" Text="Raz.Soc."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label46" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label47" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D        "></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label48" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label49" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec.Ope."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label50" runat="server" CssClass="LabelCabeceraGrilla" Text="Sdo.Pagar"></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label51" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ant."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label52" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ope."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label53" runat="server" CssClass="LabelCabeceraGrilla" Text="Resp."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label54" runat="server" CssClass="LabelCabeceraGrilla" Text="Lineal"></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label55" runat="server" CssClass="LabelCabeceraGrilla" Text="Puntual"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:Panel ID="Panel3" runat="server" Height="285px" ScrollBars="Auto" Width="100%">
                                                        <asp:GridView ID="gr_otg" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            ShowHeader="False" PageSize="12">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_OPO" HeaderText="Nº Oper.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPO_OTG" HeaderText="Nº Otg.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="Rut Cli.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Raz.Soc">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_FEC" HeaderText="Fec.Oper." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Anti." NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Oper">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Con Resp." NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Op.Lin" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Ope.Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="Lbl_pg_otg" runat="server" CssClass="Label"></asp:Label>
                                        <asp:HiddenField ID="hf_can_pag" runat="server" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Pagadas">
                                    <ContentTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <table>
                                                        <tr>
                                                            <td width="80">
                                                                <asp:Label ID="Label31" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Ope"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label57" runat="server" CssClass="LabelCabeceraGrilla" Text="Rut Cli."></asp:Label>
                                                            </td>
                                                            <td width="200">
                                                                <asp:Label ID="Label58" runat="server" CssClass="LabelCabeceraGrilla" Text="Raz.Soc."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label59" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label60" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D        "></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label61" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label62" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec.Ope."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label63" runat="server" CssClass="LabelCabeceraGrilla" Text="Sdo.Pagar"></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label64" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ant."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label65" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ope."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label66" runat="server" CssClass="LabelCabeceraGrilla" Text="Resp."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label67" runat="server" CssClass="LabelCabeceraGrilla" Text="Lineal"></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label68" runat="server" CssClass="LabelCabeceraGrilla" Text="Puntual"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:Panel ID="Panel4" runat="server" Height="285px" ScrollBars="Auto" Width="100%">
                                                        <asp:GridView ID="gr_pag" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            Width="98%">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_OPE" HeaderText="Nº Oper." />
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="Rut Cli.">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Raz.Soc">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_FEC" HeaderText="Fec.Oper." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Anti." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Oper" DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Con Resp." NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Op.Lin" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Ope.Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="Lbl_pag_pgd" runat="server" CssClass="Label"></asp:Label>
                                        <asp:HiddenField ID="Hf_pag_pgd" runat="server" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Anuladas">
                                    <ContentTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <table>
                                                        <tr>
                                                            <td width="80">
                                                                <asp:Label ID="Label69" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Ope"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label71" runat="server" CssClass="LabelCabeceraGrilla" Text="Rut Cli."></asp:Label>
                                                            </td>
                                                            <td width="200">
                                                                <asp:Label ID="Label72" runat="server" CssClass="LabelCabeceraGrilla" Text="Raz.Soc."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label73" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label74" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D        "></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label75" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label76" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec.Ope."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label77" runat="server" CssClass="LabelCabeceraGrilla" Text="Sdo.Pagar"></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label78" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ant."></asp:Label>
                                                            </td>
                                                            <td width="100">
                                                                <asp:Label ID="Label79" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Ope."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label80" runat="server" CssClass="LabelCabeceraGrilla" Text="Resp."></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label81" runat="server" CssClass="LabelCabeceraGrilla" Text="Lineal"></asp:Label>
                                                            </td>
                                                            <td width="60">
                                                                <asp:Label ID="Label82" runat="server" CssClass="LabelCabeceraGrilla" Text="Puntual"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:Panel ID="Panel5" runat="server" Height="285px" ScrollBars="Auto" Width="100%">
                                                        <asp:GridView ID="gr_anul" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            ShowHeader="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_OPE" HeaderText="Nº Oper." />
                                                                <asp:BoundField DataField="CLI_IDC" HeaderText="Rut Cli.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cliente" HeaderText="Raz.Soc">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estope" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tipdoc" HeaderText="T.D">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_FEC" HeaderText="Fec.Oper." DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_SAL_PAG" HeaderText="Sdo.Pagar" DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPE_MTO_ANT" HeaderText="Mto.Anti." DataFormatString="{0:###,###,##0}"
                                                                    NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OPN_MTO_DOC" HeaderText="Mto.Oper" DataFormatString="{0:###,###,##0}">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_res_son" HeaderText="Con Resp." NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_lnl" HeaderText="Op.Lin" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ope_ptl" HeaderText="Ope.Puntual" NullDisplayText="NO">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lbl_pag_anu" runat="server" CssClass="Label"></asp:Label>
                                        <asp:HiddenField ID="Hf_pag_anu" runat="server" />
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                     </tr>
                     <tr>
                        <td align="center">
                            <table style="width: 80px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btn_prev_otg" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="../../../Imagenes/btn_workspace/flecha_izq_out.gif'" 
                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif'"
                                            ToolTip="Anterior" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_next_otg" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif'" 
                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif'"
                                            ToolTip="Siguiente" />
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="Txt_ItemOPE" runat="server" />
                            <asp:HiddenField ID="pos_sim" runat="server" />
                            <asp:HiddenField ID="pos_otg" runat="server" />
                            <asp:HiddenField ID="pos_pag" runat="server" />
                            <asp:HiddenField ID="pos_anu" runat="server" />
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../Imagenes/Botones/Boton_Buscar_In.gif';"
                    onmouseout="this.src='../../Imagenes/Botones/Boton_Buscar_out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" ValidationGroup="Cliente"
                    ToolTip="Buscar Evaluaciones"></asp:ImageButton>
                    
                <asp:ImageButton ID="IB_Informe" onmouseover="this.src='../../Imagenes/Botones/boton_imprimir_in.gif';"
                    onmouseout="this.src='../../Imagenes/Botones/boton_imprimir_out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                    ToolTip="Imprimir Evaluación" Enabled="False"></asp:ImageButton>
                    
                <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                    onmouseout="this.src='../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                    runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                    ToolTip="Limpiar Pantalla"></asp:ImageButton>
                    
                <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_in.gif';"
                    onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif" ToolTip="Volver">
                </asp:ImageButton>
                
            </td>
        </tr>
    </table>
    
    <cc1:maskededitextender id="Txt_Rut_Cli_MaskedEditExtender" runat="server" acceptnegative="Left"
        cultureampmplaceholder="" culturecurrencysymbolplaceholder="" culturedateformat=""
        culturedateplaceholder="" culturedecimalplaceholder="" culturethousandsplaceholder=""
        culturetimeplaceholder="" enabled="True" errortooltipenabled="True" inputdirection="RightToLeft"
        Mask="999,999,999,999" masktype="Number" targetcontrolid="Txt_Rut_Cli">
     </cc1:maskededitextender>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="IB_Informe" />
    </Triggers>
    </asp:UpdatePanel>
           
    </form>
    
</body>
</html>
