<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="EvaCliDeu.aspx.vb" Inherits="ClsEvaCliDeu" Title="Evaluación Cliente / Pagadores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc4" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Evaluación Cliente / Pagador"
                            __designer:wfdid="w282"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 5px; height: 583px" class="Contenido">
                        <div style="overflow: auto; height: 550px">
                            <cc2:Accordion ID="Accordion1" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="Limit"
                                RequireOpenedPane="true" SuppressHeaderPostbacks="true" Height="250px" Width="97%">
                                <Panes>
                                    <cc2:AccordionPane ID="AccordionPane2" runat="server">
                                        <Header>
                                            <a href="" class="accordionLink">Cliente: </a>
                                            <asp:Label ID="LB_Cliente" CssClass="accordionLabel" runat="server" Text="Label"></asp:Label>
                                        </Header>
                                        <Content>
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                        </td>
                                                        <td valign="middle" align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                                CssClass="clsDisabled" ReadOnly="true" Width="90px"></asp:TextBox>
                                                            <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                                CssClass="clsDisabled" ReadOnly="true" Width="15px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label13" runat="server" CssClass="Label"
                                                                Text="Razón Soc." __designer:wfdid="w288"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled"
                                                                Width="300px" __designer:wfdid="w289" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label15" runat="server" CssClass="Label"
                                                                Text="Categ. Riesgo" __designer:wfdid="w290"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_CatRiesgo" runat="server" CssClass="clsDisabled"
                                                                Width="300px" __designer:wfdid="w291" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label14" runat="server" CssClass="Label"
                                                                Text="Ejecutivo" __designer:wfdid="w292"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled"
                                                                Width="300px" __designer:wfdid="w293" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label16" runat="server" CssClass="Label"
                                                                Text="Cartera" __designer:wfdid="w294"></asp:Label>
                                                        </td>
                                                        <td style="height: 22px" align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Cartera" runat="server" CssClass="clsDisabled"
                                                                Width="300px" __designer:wfdid="w295" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 22px" align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <%--Validaciones Cliente--%>
                                            <asp:RequiredFieldValidator ID="RF_RutCli" runat="server" ControlToValidate="Txt_Rut_Cli"
                                                ErrorMessage="<b>NIT</b><br />Ingrese el NIT del Cliente." Display="None" ValidationGroup="Cliente" />
                                            <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender1" TargetControlID="RF_RutCli"
                                                HighlightCssClass="validatorCalloutHighlight" />
                                            <asp:RequiredFieldValidator ID="RF_DigCli" runat="server" ControlToValidate="Txt_Dig_Cli"
                                                ErrorMessage="<b>Digito</b><br />Ingrese el Digito del NIT del Cliente." Display="None"
                                                ValidationGroup="Cliente" />
                                            <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender2" TargetControlID="RF_DigCli"
                                                HighlightCssClass="validatorCalloutHighlight" />
                                        </Content>
                                    </cc2:AccordionPane>
                                    <cc2:AccordionPane ID="AP_Cliente" runat="server">
                                        <Header>
                                            <a href="" class="accordionLink">Condiciones Comerciales: </a>
                                        </Header>
                                        <Content>
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label26" runat="server" CssClass="Label"
                                                                Text="% Spread Colocaciòn" __designer:wfdid="w297"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Spread" runat="server" CssClass="clsDisabled"
                                                                Width="100px" __designer:wfdid="w298" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label24" runat="server" CssClass="Label"
                                                                Text="Visitas Cliente" __designer:wfdid="w301"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_VisitasCli" runat="server" CssClass="clsDisabled"
                                                                Width="260px" __designer:wfdid="w302" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label28" runat="server" CssClass="Label"
                                                                Text="Linea Ocupada" __designer:wfdid="w307"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_LineaOcu" runat="server" CssClass="clsDisabled"
                                                                Width="100px" __designer:wfdid="w308" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label25" runat="server" CssClass="Label"
                                                                Text="Fec. Ult. Op." __designer:wfdid="w303"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_FecUltOpe" runat="server" CssClass="clsDisabled"
                                                                Width="70px" __designer:wfdid="w304" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label30" runat="server" CssClass="Label"
                                                                Text="Linea Apro." __designer:wfdid="w305"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_LineaApro" runat="server" CssClass="clsDisabled"
                                                                Width="100px" __designer:wfdid="w306" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label27" runat="server" CssClass="Label"
                                                                Text="Prom. días Pag." __designer:wfdid="w313"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_ProDiaPag" runat="server" CssClass="clsDisabled"
                                                                Width="60px" __designer:wfdid="w314" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label7" runat="server" CssClass="Label" Text="Linea Disponible"
                                                                __designer:wfdid="w309"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_LineaDis" runat="server" CssClass="clsDisabled"
                                                                Width="100px" __designer:wfdid="w310" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label Style="position: static" ID="Label29" runat="server" CssClass="Label"
                                                                Text="F. Vcto. Linea" __designer:wfdid="w311"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_FecVctoLin" runat="server" CssClass="clsDisabled"
                                                                Width="70px" __designer:wfdid="w312" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </Content>
                                    </cc2:AccordionPane>
                                    <cc2:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            <a href="" class="accordionLink">Condiciones de Linea / Deudas: </a>
                                        </Header>
                                        <Content>
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="Cabecera" align="left">
                                                                            <asp:Label ID="Label34" runat="server" CssClass="SubTitulos" Text="Condiciones de Linea"
                                                                                __designer:wfdid="w316"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="Contenido" valign="top" align="center">
                                                                            <asp:Panel ID="Panel2" runat="server" Width="99%" Height="90px" __designer:wfdid="w321"
                                                                                ScrollBars="Horizontal">
                                                                                <asp:GridView ID="GV_LineaCredito" runat="server" __designer:wfdid="w322" AutoGenerateColumns="False"
                                                                                    CellPadding="3" CssClass="formatUltcell" EnableTheming="True" HorizontalAlign="Center"
                                                                                    ShowHeader="true" Width="100%">
                                                                                    <FooterStyle BorderStyle="Dashed" />
                                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="id_P_0031_des" HeaderText="Productos">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                            <FooterStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="apc_pct" HeaderText="%">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
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
                                                        <td valign="top" align="left">
                                                            <table style="position: static" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="Cabecera" align="left">
                                                                            <asp:Label ID="Label39" runat="server" CssClass="SubTitulos" Text="Deuda Consolidada"
                                                                                __designer:wfdid="w323"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                                                                            class="Contenido" valign="top" align="left" height="90">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label41" runat="server" CssClass="Label" Text="Como Cliente" __designer:wfdid="w324"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Txt_Deu_Cli" runat="server" CssClass="clsDisabled" Width="100px"
                                                                                                __designer:wfdid="w325" ReadOnly="True"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label42" runat="server" CssClass="Label" Text="Como Pagador" __designer:wfdid="w326"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Txt_Deu_Deu" runat="server" CssClass="clsDisabled" Width="100px"
                                                                                                __designer:wfdid="w327" ReadOnly="True"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="labelcc2" runat="server" CssClass="Label" Text="Total Deuda" __designer:wfdid="w328"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Txt_Tot_Deu" runat="server" CssClass="clsDisabled" Width="100px"
                                                                                                __designer:wfdid="w329" ReadOnly="True"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label Style="position: static" ID="Label44" runat="server" CssClass="Label"
                                                                                                Text="Nº Pagadores" __designer:wfdid="w330"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox Style="position: static" ID="Txt_Nro_Deu" runat="server" CssClass="clsDisabled"
                                                                                                Width="50px" __designer:wfdid="w331" ReadOnly="True"></asp:TextBox>
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
                                                            <table style="position: static" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="Cabecera" align="left">
                                                                            <asp:Label ID="Label45" runat="server" CssClass="SubTitulos" Text="Resumen Deuda Con Problemas"
                                                                                __designer:wfdid="w332"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                                                                            class="Contenido" valign="top" align="left" height="90">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label46" runat="server" CssClass="Label" Text="Monto" __designer:wfdid="w333"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Txt_Mto_Pro" runat="server" CssClass="clsDisabled" Width="100px"
                                                                                                __designer:wfdid="w334" ReadOnly="True"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label47" runat="server" CssClass="Label" Text="Nº Documentos" __designer:wfdid="w335"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Txt_Doc_Pro" runat="server" CssClass="clsDisabled" Width="100px"
                                                                                                __designer:wfdid="w336" ReadOnly="True"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label48" runat="server" CssClass="Label" Text="Nº Pagadores" __designer:wfdid="w337"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Txt_Deu_Pro" runat="server" CssClass="clsDisabled" Width="100px"
                                                                                                __designer:wfdid="w338" ReadOnly="True"></asp:TextBox>
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
                                        </Content>
                                    </cc2:AccordionPane>
                                </Panes>
                            </cc2:Accordion>
                            <%--Tabla de Ingreso de Deudor y Evaluacion--%>
                            <table cellspacing="0" cellpadding="0" width="97%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label17" runat="server" CssClass="SubTitulos" Text="Pagador" __designer:wfdid="w339"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 5px; height: 60px" class="Contenido" align="left">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w340"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Rut_Deu" TabIndex="5" runat="server" CssClass="clsDisabled"
                                                                Width="90px" __designer:wfdid="w341" ReadOnly="True" ValidationGroup="Agregar_Deudor"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                            </cc2:MaskedEditExtender>
                                                            <asp:TextBox ID="Txt_Dig_Deu" TabIndex="5" runat="server" CssClass="clsDisabled"
                                                                AutoPostBack="true" Width="15px" MaxLength="1" ReadOnly="True" ValidationGroup="Agregar_Deudor"></asp:TextBox>
                                                            <cc2:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                            </cc2:FilteredTextBoxExtender>
                                                            &nbsp;
                                                            <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagadores"
                                                                ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" Style="margin-top: 0px; height: 20px;" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label20" runat="server" __designer:wfdid="w343" CssClass="Label" Text="Razón Soc."></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="Txt_Rso_Deu" runat="server" __designer:wfdid="w344" CssClass="clsDisabled"
                                                                MaxLength="50" ReadOnly="True" Width="300px" ValidationGroup="Agregar_Deudor"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left" colspan="4">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            &nbsp;
                                                            <asp:Label ID="Label31" runat="server" __designer:wfdid="w347" CssClass="Label" Style="position: static"
                                                                Text="% Anticipar"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Por_Ant" runat="server" MaxLength="3" ReadOnly="True" Width="50px"
                                                                CssClass="clsDisabled" ValidationGroup="Agregar_Deudor" AutoPostBack="true"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="Txt_Por_Ant_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="," CultureName="es-ES" CultureThousandsPlaceholder="."
                                                                CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="999.99"
                                                                MaskType="Number" TargetControlID="Txt_Por_Ant">
                                                            </cc2:MaskedEditExtender>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label22" runat="server" __designer:wfdid="w345" CssClass="Label" Style="position: static"
                                                                Text="Moneda"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="DP_TipoMoneda" runat="server" __designer:wfdid="w346" CssClass="clsDisabled"
                                                                Enabled="False" Style="position: static" Width="130px" AutoPostBack="true" ValidationGroup="Agregar_Deudor">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label32" runat="server" __designer:wfdid="w349" CssClass="Label" Style="position: static"
                                                                Text="Monto Docto."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Mto_Doc" runat="server" AutoPostBack="true" CssClass="clsDisabled"
                                                                ValidationGroup="Agregar_Deudor"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="Txt_Mto_Doc_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Mto_Doc">
                                                            </cc2:MaskedEditExtender>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label33" runat="server" __designer:wfdid="w351" CssClass="Label" Style="position: static"
                                                                Text="Monto Evaluado"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Mto_Eva" runat="server" __designer:wfdid="w352" CssClass="clsDisabled"
                                                                Style="position: static" Width="110px" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:ImageButton ID="IB_AgregarDeu" runat="server" __designer:wfdid="w353" AlternateText="Agregar Pagador"
                                                                ImageUrl="~/Imagenes/btn_workspace/Agregar_Out.gif" onmouseover="this.src='../../../Imagenes/btn_workspace/Agregar_in.gif';"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Agregar_Out.gif';" OnClick="IB_AgregarDeu_Click"
                                                                Enabled="false" Style="position: static" ValidationGroup="Agregar_Deudor" ToolTip="Agregar Pagador" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:ImageButton ID="IB_QuitarDeu" runat="server" __designer:wfdid="w353" AlternateText="Quitar Pagador"
                                                                ImageUrl="~/Imagenes/btn_workspace/Quitar_Out.gif" onmouseover="this.src='../../../Imagenes/btn_workspace/Quitar_in.gif';"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Quitar_Out.gif';" Visible="true"
                                                                Style="position: static" ValidationGroup="Agregar_Deudor" ToolTip="Quitar Pagador" />
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <%--Grilla de Deudores evaluados--%>
                            <table id="TABLE1" cellspacing="0" cellpadding="0" width="97%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Lista Pagadores"
                                                __designer:wfdid="w354"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Panel ID="Panel_GV_Deudores" runat="server" Width="100%" Height="120px" ScrollBars="Auto">
                                                <asp:GridView ID="GV_Deudores" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                                    CssClass="formatUltcell" EnableTheming="True" HorizontalAlign="Center" PageSize="100"
                                                    ShowHeader="True" Style="position: static" Width="100%">
                                                    <FooterStyle BorderStyle="Dashed" />
                                                    <Columns>
                                                        <asp:BoundField DataField="RutDeu" HeaderText="Identificación">
                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nomdeu" HeaderText="Razón Social" HtmlEncode="false" HtmlEncodeFormatString="false">
                                                            <ItemStyle Width="250px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="deuact" HeaderText="Deuda Act.">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="deufac" HeaderText="Deuda Tot Fact.">
                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="mtoeva" HeaderText="Mto. Eva.">
                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="mtodoc" HeaderText="Mto. Docto.">
                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="deutot" HeaderText="Deuda Tot.">
                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="porcli" HeaderText="% Cliente">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MtoSbl" HeaderText="Sub Linea">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cupo" HeaderText="Cupo Global">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Disponible" HeaderText="Cupo Disponible">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="90px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" ToolTip='<%# Eval("RutDeu") %>'
                                                                    OnClick="Img_Ver_Click" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                        </asp:TemplateField>
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
                            <br />
                            <%--Totales--%>
                            <table cellspacing="0" cellpadding="0" width="97%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Totales" __designer:wfdid="w365"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px;" class="Contenido" valign="top" align="center">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Mto. Evaluado" __designer:wfdid="w366"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Tot_Eva" runat="server" CssClass="clsDisabled" Width="110px"
                                                                __designer:wfdid="w367" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label Style="position: static" ID="Label10" runat="server" CssClass="Label"
                                                                Text="Mto. Doctos." __designer:wfdid="w368"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Tot_Doc" runat="server" CssClass="clsDisabled"
                                                                Width="110px" __designer:wfdid="w369" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label Style="position: static" ID="Label11" runat="server" CssClass="Label"
                                                                Text="Deuda Total" __designer:wfdid="w370"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Style="position: static" ID="Txt_Deu_Tot" runat="server" CssClass="clsDisabled"
                                                                Width="110px" __designer:wfdid="w371" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td class="Cabecera" align="left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Observaciones"
                                                            __designer:wfdid="w365"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 2px;" class="Contenido" valign="top" align="left">
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:TextBox Style="position: static" ID="Txt_Obs" runat="server" CssClass="clsDisabled"
                                                                        Width="1100px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:TextBox ID="txt_HFEva" runat="server" Visible="false"></asp:TextBox>
                        <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="IB_Guardar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" __designer:wfdid="w376"
                            AlternateText="Guardar Datos"></asp:ImageButton>
                        <cc2:ConfirmButtonExtender ID="IB_Guardar_ConfirmButtonExtender" runat="server" ConfirmText="¿Desea Guardar?"
                            Enabled="True" TargetControlID="IB_Guardar">
                        </cc2:ConfirmButtonExtender>
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif" __designer:wfdid="w377" ToolTip="Limpiar Pagadores">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Informe" onmouseover="this.src='../../../Imagenes/Botones/Btn_informe_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Btn_informe_out.gif';" OnClick="IB_Informe_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Btn_informe_out.gif" __designer:wfdid="w378"
                            ToolTip="Ver Informe de Evaluación"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_out.gif';" OnClick="IB_Volver_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif" __designer:wfdid="w378"
                            AlternateText="Volver a Evaluacion" ToolTip="Volver "></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="10px" Width="10PX">
            </rsweb:ReportViewer>
            <asp:LinkButton ID="LB_BuscaDeudor" runat="server" Style="position: static" ValidationGroup="Deudor"></asp:LinkButton>
            <asp:LinkButton ID="Lb_buscar" OnClick="Lb_buscar_Click" runat="server" ValidationGroup="Cliente"></asp:LinkButton>
            <asp:HiddenField ID="HF_Pos" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Informe" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc4:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:HiddenField ID="HF_NroEva" runat="server" />
    <asp:HiddenField ID="id_ldc" runat="server" />
    <asp:HiddenField ID="HF_Accion" runat="server" />
    <asp:HiddenField ID="HF_Est" runat="server" />
    <asp:HiddenField ID="HF_EstDes" runat="server" />
    <asp:HiddenField ID="HF_IdSbl" runat="server" />
    <asp:LinkButton ID="LB_Eliminar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Deudor" runat="server"></asp:LinkButton>
    <%--Validaciones Evaluacion--%>
    <asp:RequiredFieldValidator ID="RF_PorAnt" runat="server" ControlToValidate="Txt_Por_Ant"
        ErrorMessage="<b>%</b><br />Ingrese Porcentaje de anticipo." Display="None" ValidationGroup="AgregarDeudor" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender5" TargetControlID="RF_PorAnt"
        HighlightCssClass="validatorCalloutHighlight" />
    <asp:RequiredFieldValidator ID="RF_MtoDoc" runat="server" ControlToValidate="Txt_Mto_Doc"
        ErrorMessage="<b>Monto</b><br />Ingrese Monto del documento." Display="None"
        ValidationGroup="AgregarDeudor" />
    <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender7" TargetControlID="RF_MtoDoc"
        HighlightCssClass="validatorCalloutHighlight" />
</asp:Content>
