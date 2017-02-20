<%@ Page Title="Hoja de Recaudación" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="Hoja_rec.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Hoja_rec" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Funciones_modulo_js/Recaudacion.js"></script>

    <table border="0" height="540px" style="position: static" width="100%" cellpadding="0"
        cellspacing="1" class="Contenido">
        <tr>
            <td class="Cabecera" style="width: 100%; text-align: -moz-center;height:31px" align="center">
                <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Recaudacion-Hoja de Recaudación"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" class="Contenido" align="center" style="text-align: -moz-center">
                <table id="Table_Contenido" border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" valign="top" style="width: 100%">
                            <table cellspacing="0" width="100%">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="lbl_cli_deu0" runat="server" CssClass="SubTitulos" Text="Datos Hoja de Recaudación"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="left">
                                        <table style="text-align: -moz-center">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label123" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="Ch_Suc" runat="server" AutoPostBack="True" Text="Todas las Sucursales"
                                                        CssClass="Label" />
                                                </td>
                                                <td width="20">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Fecha Recaudación"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fec_Rec" runat="server" Width="90px" CssClass="clsMandatorio"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="Txt_Fec_Rec"
                                                        Display="None" ErrorMessage="&lt;b&gt;Recaudación&lt;/b&gt;&lt;br /&gt;Ingrese Fecha"
                                                        ValidationGroup="Busca" Font-Size="8pt" />
                                                    <cc1:ValidatorCalloutExtender runat="server" ID="RequiredFieldValidator4_ValidatorCalloutExtender"
                                                        TargetControlID="RequiredFieldValidator4" HighlightCssClass="validatorCalloutHighlight"
                                                        Enabled="True" />
                                                    <cc1:MaskedEditExtender ID="Txt_Fec_Rec_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Rec">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:CalendarExtender ID="Txt_Fec_Rec_CalendarExtender" runat="server" CssClass="radcalendar"
                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Rec">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label124" runat="server" CssClass="Label" Text="Horario"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rb_hora" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="A">AM</asp:ListItem>
                                                        <asp:ListItem Value="P">PM</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label125" runat="server" CssClass="Label" Text="Recaudador"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="Dr_Rec" runat="server" Width="250px" CssClass="clsMandatorio">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="Dr_Rec"
                                                        Display="None" ErrorMessage="Seleccione Recaudador" ValidationGroup="Busca" Font-Size="8pt"
                                                        InitialValue="0" />
                                                    <cc1:ValidatorCalloutExtender runat="server" ID="RequiredFieldValidator5_ValidatorCalloutExtender"
                                                        TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight"
                                                        Enabled="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="Rb_cli_deu" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="C">Cliente</asp:ListItem>
                                        <asp:ListItem Value="D" Selected="True">Pagador</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="lbl_cli_deu" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                                            Width="20px" AutoPostBack="True"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                                                            TargetControlID="Txt_Dig_Deu" ValidChars="Kk">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="Ib_ayu_deu" runat="server" Height="20px" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            ToolTip="Ayuda Deudor" Enabled="false" />
                                                                        <asp:ImageButton ID="Ib_ayuda_cli" runat="server" Height="20px" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            ToolTip="Ayuda Cliente" Visible="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" Width="407px"></asp:TextBox>
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
                                                            <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <asp:DropDownList ID="dr_pgdr" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                Width="150px">
                                                                <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="1">Cliente</asp:ListItem>
                                                                <asp:ListItem Value="2">Pagador</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="Días Retención"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btn_pza" runat="server" ImageUrl="~/Imagenes/btn_workspace/Plazas_out.gif"
                                                                            ToolTip="Plazas" onmouseout="this.src='../../../Imagenes/btn_workspace/Plazas_out.gif';"
                                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/Plazas_in.gif';" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Pza" runat="server" CssClass="clsDisabled" MaxLength="20" Width="40px">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:LinkButton ID="LB_Dias" runat="server"></asp:LinkButton>
                                                <asp:HiddenField ID="HF_IdPlaza" runat="server" />
                                                <asp:HiddenField ID="HF_Dias" runat="server" />
                                            </td>
                                            <td valign="top">
                                                <table cellspacing="0" style="visibility:hidden">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label127" runat="server" CssClass="SubTitulos" Text="Tasa Aplicar" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_tasa" runat="server" CssClass="clsMandatorio" Width="40px" AutoPostBack="True" Visible="false"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Por_Ant_MaskedEditExtender" runat="server" AutoComplete="False"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="," CultureName="es-ES" CultureThousandsPlaceholder="."
                                                                            CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="999.99"
                                                                            MaskType="Number" TargetControlID="Txt_tasa">
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_pza" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                                <cc1:TabPanel HeaderText="Documentos a Pagar" ID="TabPanel1" runat="server" Width="100%"
                                    CssClass="SubTitulos">
                                    <HeaderTemplate>
                                        Documentos a Pagar
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel_gr_doctos" runat="server" Width="1200px" Height="240px" ScrollBars="Horizontal">
                                                    <asp:GridView ID="gr_doctos" runat="server" AutoGenerateColumns="False"
                                                        CssClass="formatUltcell" Width="1380px">
                                                        <Columns>
                                                            <asp:BoundField DataField="CONTRATO" HeaderText="NºContrato">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="150px"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DES_TIP_DOC" HeaderText="T.D">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="N_DOCTO" DataFormatString="{0:###,###,###,###}" HeaderText="Nº Docto.">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NRO_CUOTA" HeaderText="Nº Cuota">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RUT_CLI" HeaderText="Nit Cliente">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="N_CLIENTE" HeaderText="Razón Social">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RUT_DEUDOR" HeaderText="Nit Pagador">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_DEUDOR" HeaderText="Razón Social">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="S_CLIENTE" DataFormatString="{0:###,###,###,###}" HeaderText="Sdo. Cli.">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="S_DEUDOR" DataFormatString="{0:###,###,###,###}" HeaderText="Mto a Rec.">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="interes" DataFormatString="{0:###,###,###,###}" HeaderText="Interés">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="fec_vcto" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nota_cred" DataFormatString="{0:###,###,###,###}" HeaderText="Nota Credito">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MTO_RECAUDADO" DataFormatString="{0:###,###,###,###}"
                                                                HeaderText="Mto. Recaudado">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td class="Label">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Infografia/Pago total.gif"
                                                                Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Infografia/Doc no cedido.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Infografia/pago parcial.gif"
                                                                Visible="False" />
                                                            <asp:HiddenField ID="HF_Pos_doc" runat="server" />
                                                            <asp:HiddenField ID="nro_hoja" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel HeaderText="Recaudación" ID="tabpanel2" runat="server" CssClass="SubTitulos">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td align="center" height="300" valign="top" width="938">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table class="TD_BORDES">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label runat="server" Text="Moneda" CssClass="Label" ID="Label41"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList runat="server" Width="190px" ID="Dr_mon_rec">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table class="TD_BORDES">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label runat="server" Text="Forma de Pago" CssClass="Label" ID="Label42"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList runat="server" Width="250px" ID="Dr_for_pgo" AutoPostBack="True">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table class="TD_BORDES">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label runat="server" Text="Monto Docto Pago" CssClass="Label" ID="Label43"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="txt_mto_pgo"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="Txt_Mto_Rec_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                                        TargetControlID="txt_mto_pgo">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btn_ok_gr_pgo" runat="server" ImageUrl="~/Imagenes/btn_workspace/Flecha_abajo_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/Flecha_abajo_out.gif';"
                                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/Flecha_abajo_in.gif';"
                                                                            Width="20px" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btn_canc_gr_pgo" runat="server" ImageUrl="~/Imagenes/btn_workspace/Flecha_arriba_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/Flecha_arriba_out.gif';"
                                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/Flecha_arriba_in.gif';"
                                                                            Width="20px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="HF_Pos_DPO" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Panel ID="Panel1" runat="server" Width="500px" Height="120px" ScrollBars="Horizontal">
                                                                <asp:GridView ID="gr_recau" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    ShowHeader="true" Width="500px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Seleccion">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="Btn_ver" runat="server" ToolTip='' ImageUrl="~/Images/bt_ver.gif"
                                                                                    OnClick="Btn_ver_Click" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="id_p_0054" HeaderText="TIPO DE PAGO">
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="dpo_num" NullDisplayText="0" HeaderText="Nº DOCTO.">
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="id_p_0023" HeaderText="MONEDA">
                                                                            <ItemStyle Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="dpo_mto" HeaderText="MONTO RECAUDADO">
                                                                            <ItemStyle Width="200px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                            <table class="TD_BORDES" width="930px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btn_hoj_rut" runat="server" ImageUrl="~/Imagenes/Botones/btn_hoja-de-ruta_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/Botones/btn_hoja-de-ruta_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_hoja-de-ruta_in.gif';"
                                                                Enabled="False" ToolTip="Hoja de Ruta" Visible="False" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btn_pend" runat="server" ImageUrl="~/Imagenes/Botones/btn_pendientes_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/Botones/btn_pendientes_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_pendientes_in.gif';"
                                                                ToolTip="Dejar Pendiente" Enabled="False" Visible="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:ImageButton ID="btn_doc_deu" runat="server" ImageUrl="~/Imagenes/btn_workspace/ingDoc_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/ingDoc_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/ingDoc_in.gif';"
                                                                Enabled="False" ToolTip="Documentos Deudor" />
                                                            <asp:ImageButton ID="btn_doc_nce" runat="server" Enabled="False" ImageUrl="~/Imagenes/btn_workspace/ING_nce.png"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/ING_nce.png';" onmouseover="this.src='../../../Imagenes/btn_workspace/ING_nce.png';"
                                                                ToolTip="Ingreso documentos no cedidos" />
                                                        </td>
                                                        <td class="style2">
                                                            <asp:ImageButton ID="btn_pag_docs" runat="server" ImageUrl="~/Imagenes/Botones/btn_pagar-docto_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/Botones/btn_pagar-docto_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_pagar-docto_in.gif';"
                                                                ToolTip="Pagar Documentos" Enabled="False" Visible="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btn_no_rec" runat="server" ImageUrl="~/Imagenes/Botones/btn_no_recaudado_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/Botones/btn_no_recaudado_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_no_recaudado_in.gif';"
                                                                ToolTip="No Recaudados" Enabled="False" Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btn_doc_deu" />
                                                <asp:PostBackTrigger ControlID="btn_doc_nce" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label40" runat="server" CssClass="SubTitulos" Text="TOTALES"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label36" runat="server" CssClass="Label" Text="#Doctos A Pagar" Width="120px"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Total Recaudado"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="Label37" runat="server" CssClass="Label" Text="#Doctos de Pago"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="Label39" runat="server" CssClass="Label" Text="Total Docto Pago"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txt_can_doc_pag" runat="server" CssClass="clsDisabled" ReadOnly="True">0</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_tot_rec" runat="server" CssClass="clsDisabled" ReadOnly="True">0</asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="txt_tot_rec">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_cant_pgo" runat="server" CssClass="clsDisabled" ReadOnly="True">0</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_tot_dcto_pag" runat="server" CssClass="clsDisabled" ReadOnly="True">0</asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="txt_tot_dcto_pag">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:LinkButton ID="LB_NCE" OnClick="LB_NCE_CLICK" runat="server"></asp:LinkButton>
                                        <asp:LinkButton ID="lb_mto" runat="server"></asp:LinkButton>
                                        <asp:LinkButton ID="lb_cre" runat="server"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="1">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table id="Table_Botonera" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lb_buscar" runat="server"></asp:LinkButton>
                                    <asp:ImageButton ID="Btn_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        TabIndex="22" ToolTip="Buscar" ValidationGroup="Busca" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_Imprimir" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                                        TabIndex="24" ToolTip="Informe Hoja de Recaudación" Enabled="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_gua_rec" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                        ToolTip="Guardar" onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" Enabled="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        TabIndex="30" ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_imprimir" />
                        <asp:PostBackTrigger ControlID="Btn_gua_rec" />
                        <asp:PostBackTrigger ControlID="Btn_Limpiar" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <asp:LinkButton ID="lb_calcular" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_DoctoPago" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="MP_DoctoPago" runat="server" TargetControlID="LB_DoctoPago"
        EnableViewState="False" PopupControlID="Panel_DoctoPago" BackgroundCssClass="modalBackground"
        PopupDragHandleControlID="Panel_DoctoPago">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel_DoctoPago" runat="server" Width="650px" Height="300px" Style="display: none">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="Cabecera">
                    <asp:Label ID="Label3" runat="server" Text="Documentos de Pago : Cheque" CssClass="SubTitulos"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Contenido">
                    <table class="Contenido">
                        <tr>
                            <td style="height: 100px" valign="top">
                                <table border="0" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td>
                                            <%--Banco--%>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Bancos" CssClass="Label" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DP_Banco" runat="server" Width="400px" CssClass="clsMandatorio"/>
                                                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="DP_Banco"
                                                            PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                            PromptPosition="Bottom" IsSorted="true">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <%--Plaza--%>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label48" runat="server" Text="Plaza" CssClass="Label" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DP_PlazaBanco" runat="server" Width="400px" CssClass="clsMandatorio"/>
                                                        <cc1:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="DP_PlazaBanco"
                                                            PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                            PromptPosition="Bottom" IsSorted="true">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #000000;">
                                <%--Datos Docto. de Pago--%>
                                <asp:Label ID="Label49" runat="server" Text="Datos Docto. de Pago" CssClass="Label"
                                    Font-Bold="true"></asp:Label>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label50" runat="server" Text="N° Docto." CssClass="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txt_NroDocto" runat="server" CssClass="clsMandatorio" ReadOnly="true"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="Txt_NroDocto">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label51" runat="server" Text="Fecha Emision" CssClass="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txt_Fec_Emi" runat="server" CssClass="clsMandatorio" ReadOnly="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Txt_Fec_Emi"
                                                FirstDayOfWeek="Monday" Format="dd-MM-yyyy" CssClass="radcalendar">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label52" runat="server" Text="Fecha Vcto." CssClass="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txt_Fec_Vto" runat="server" CssClass="clsMandatorio" ReadOnly="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txt_Fec_Vto"
                                                FirstDayOfWeek="Monday" Format="dd-MM-yyyy" CssClass="radcalendar">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label53" runat="server" Text="Cta. Cte." CssClass="Label"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <asp:TextBox ID="Txt_Cta_Cte" runat="server" CssClass="clsMandatorio" Width="100%"
                                                ReadOnly="true"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="Txt_Cta_Cte">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label54" runat="server" CssClass="Label" Text="Monto Docto."></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txt_Mto_Dco" runat="server" CssClass="clsMandatorio" ReadOnly="true"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="Txt_Mto_Dco">
                                            </cc1:MaskedEditExtender>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label55" runat="server" Text="Origen de Fondo" CssClass="Label"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="DP_OrigenFondo" runat="server" CssClass="clsMandatorio" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label56" runat="server" CssClass="Label" Text="A la Orden"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <asp:RadioButton ID="RB_Banco" runat="server" CssClass="Label" GroupName="Orden"
                                                Enabled="false" Text="Banco" Checked="True" />
                                            <asp:RadioButton ID="RB_Cliente" runat="server" CssClass="Label" GroupName="Orden"
                                                Enabled="false" Text="Cliente" />
                                            <asp:RadioButton ID="RB_Tercero" runat="server" CssClass="Label" GroupName="Orden"
                                                Enabled="false" Text="Tercero" />
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td colspan="4">
                                            <asp:TextBox ID="Txt_Orden" runat="server" CssClass="clsMandatorio" Width="100%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Contenido" align="right">
                    <asp:ImageButton ID="IB_AceptarCheque" runat="server" AlternateText="Aceptar" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';" />
                    <asp:ImageButton ID="IB_CancelarCheque" runat="server" AlternateText="Cencelar" ImageUrl="~/Imagenes/Botones/Boton_Cancelar_out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Cancelar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Cancelar_in.gif';" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:LinkButton ID="LB_DOC_DDR" OnClick="LB_DOC_DDR_CLICK" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_NUE_HOJA" runat="server"></asp:LinkButton>
    
</asp:Content>
