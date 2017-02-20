<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Gastos_Rec.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_Gastos_Rec"
    Title="Gastos de Recaudación" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script src="../Funciones_modulo_js/Recaudacion.js" type="text/javascript"></script>
            <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

            <table style="width: 100%" cellpadding="3" cellspacing="1" class="Contenido">
                <tr>
                    <td class="Cabecera" style="width: 100%; text-align: -moz-center;height:31px" valign="top" align="center">
                        <asp:Label ID="Label16" runat="server" CssClass="Titulos" Text="Recaudación-Gastos de Recaudación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="590px" valign="top" class="Contenido" style="width: 100%; text-align: -moz-center"
                        align="center">
                        <table cellspacing="0" width="98%" cellpadding="5">
                            <tr>
                                <td class="Cabecera" width="98%" align="left" style="text-align: -moz-left">
                                    <asp:Label ID="Label3" runat="server" Text="Datos de Hoja Recaudación" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" width="100%" align="left" style="text-align: -moz-left">
                                    <table>
                                        <tr>
                                            <td align="left" style="text-align: -moz-left">
                                                <asp:Label ID="Label131" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Ch_Suc" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas"
                                                    Width="100%" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label132" runat="server" CssClass="Label" Text="Fecha Recaudación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Fec_Rec" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                    Width="90px"></asp:TextBox>
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
                                                <asp:Label ID="Label133" runat="server" CssClass="Label" Text="Horario Recaudación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rb_hora" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="A">AM</asp:ListItem>
                                                    <asp:ListItem Value="P">PM</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label134" runat="server" CssClass="Label" Text="Recaudador Origen"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dr_Rec" runat="server" CssClass="clsMandatorio" Width="250px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Dr_rec"
                                                    Display="None" ErrorMessage="Seleccione Recaudador" Font-Size="8pt" InitialValue="0"
                                                    ValidationGroup="Busca" />
                                                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                                    runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator8" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                        <table cellspacing="0" width="98%" cellpadding="5">
                            <tr>
                                <td class="Cabecera" align="left" style="text-align: -moz-left">
                                    <asp:Label ID="Label1" runat="server" Text="Ingreso de Gastos por Recaudador" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" height="4px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" class="Contenido">
                                                    <tr>
                                                        <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                            <asp:Label ID="lbl_cli_deu" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="text-align: -moz-left">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsMandatorio" Width="78px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                            MaxLength="1" Width="20px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                                                            TargetControlID="Txt_Dig_Deu" ValidChars="Kk">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Style="margin-top: 0px" />
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
                                                <table cellspacing="0" height="50px">
                                                    <tr>
                                                        <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                            <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" Text="Tipo de Gasto"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <asp:DropDownList ID="dr_tip_gas" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                ValidationGroup="add" Width="150px">
                                                                <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="1">Cliente</asp:ListItem>
                                                                <asp:ListItem Value="2">Pagador</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dr_tip_gas"
                                                                Display="None" ErrorMessage="Seleccione Gasto" Font-Size="8pt" InitialValue="0"
                                                                ValidationGroup="add" />
                                                            <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator7_ValidatorCalloutExtender"
                                                                runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator7" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" align="left" style="text-align: -moz-left">
                                                <table cellspacing="0" height="50">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="Monto"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <asp:TextBox ID="Txt_mto_gto" runat="server" CssClass="clsDisabled" MaxLength="20"
                                                                ValidationGroup="add" Width="100px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_mto_gto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="Txt_mto_gto">
                                                            </cc1:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Txt_mto_gto"
                                                                Display="None" ErrorMessage="Ingrese monto de Gasto" Font-Size="8pt" InitialValue="0"
                                                                ValidationGroup="add" />
                                                            <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator6_ValidatorCalloutExtender"
                                                                runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator6" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                        <table cellspacing="0" width="98%">
                            <tr>
                                <td class="Cabecera" align="left" style="text-align: -moz-left">
                                    <asp:Label ID="Label130" runat="server" CssClass="SubTitulos" Text="Gastos por Recaudador"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center" style="text-align: -moz-center; width: 100%">
                                    <table align="Center" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Panel ID="Panel1" runat="server" CssClass="Contenido" Height="240px" Width="650px">
                                                    <asp:GridView ID="Gr_gastos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Btn_ver" runat="server" ImageUrl="~/Images/bt_ver.gif" onclick="Btn_ver_Click" style="height: 13px"
                                                                        />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deudor" HeaderText="Razón Social">
                                                                <ItemStyle Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="tipo_gasto" HeaderText="Tipo Gasto">
                                                                <ItemStyle Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="gga_mto" HeaderText="Monto Gasto">
                                                                <ItemStyle Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="gga_rec_ext" HeaderText="Rec.Ext">
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
                            <%--<tr>
                                <td class="Contenido" align="left">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Infografia/Aprob_Gtos_Rec.gif" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="center" class="Contenido" style="width:100%" >
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btn_ok_gr_pgo" runat="server" ImageUrl="~/Imagenes/btn_workspace/Flecha_abajo_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Flecha_abajo_out.gif';"
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/Flecha_abajo_in.gif';"
                                                                Style="width: 19px" ToolTip="Agregar Gasto" ValidationGroup="add" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btn_canc_gr_pgo" runat="server" ImageUrl="~/Imagenes/btn_workspace/Flecha_arriba_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Flecha_arriba_out.gif';"
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/Flecha_arriba_in.gif';"
                                                                ToolTip="Quitar Gasto seleccionado" Width="19px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="HF_Pos_doc" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table id="Table_Botonera" border="0" cellpadding="1" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 24px">
                                    <asp:LinkButton ID="lb_buscar" runat="server"></asp:LinkButton>
                                    <asp:ImageButton ID="Btn_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        TabIndex="22" ValidationGroup="Busca" ToolTip="Buscar Gastos" />
                                    <asp:LinkButton ID="marcagrilla" runat="server"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_apb" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';"
                                        ToolTip="Aprobación de Gastos" />
                                </td>
                                <td style="height: 24px">
                                    <asp:ImageButton ID="Btn_gua_rec" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                                        ToolTip="Guardar Gastos" />
                                    <confirmbuttonextender id="Btn_gua_ope_ConfirmButtonExtender" runat="server" confirmtext="¿Desea Guardar la Operación ?"
                                        enabled="True" targetcontrolid="Btn_gua_ope">
                            </confirmbuttonextender>
                                </td>
                                <td style="height: 24px">
                                    <asp:ImageButton ID="Btn_imp" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                                        ToolTip="Informe de Gastos de Recaudación" />
                                    <asp:HiddenField ID="nro_hoja" runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_imp_control" runat="server" ImageUrl="~/Imagenes/Botones/boton_imp_simu_OUT.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imp_simu_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imp_simu_in.gif';"
                                        ToolTip="Informe de Control Recaudador" />
                                </td>
                                <td style="height: 24px">
                                    <asp:ImageButton ID="Btn_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        Style="position: static" TabIndex="30" ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_imp" />
            <asp:PostBackTrigger ControlID="btn_imp_control" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
