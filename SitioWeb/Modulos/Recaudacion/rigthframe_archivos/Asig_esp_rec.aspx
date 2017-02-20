<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Asig_esp_rec.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_Asi_esp_rec" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Ajax.js"></script>
    <script language="javascript" src="../../../FuncionesJS/PopCalendar.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Excel.js"></script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" ScriptMode="Release">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table cellpadding="0" cellspacing="1" class="Contenido">
                <tr>
                    <td class="Cabecera" align="center" style="text-align: -moz-center;height:31px">
                        <asp:Label ID="Label25" runat="server" CssClass="Titulos" Text="Asignación Especial Recaudadores"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="width: 100%">
                        
                        <table width="100%">
                            <tr>
                                <td>
                                    <table width="50%" cellspacing="0">
                            <tr>
                                <td align="left" class="Cabecera" style="width: 100%">
                                    <asp:Label ID="Label1" runat="server" Text="Proveedor" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" style="width: 100%">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                    Width="20px" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                    Width="20px" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="220px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                                </td>
                                <td>
                                    <table width="50%" cellspacing="0">
                            <tr>
                                <td align="left" class="Cabecera">
                                    <asp:CheckBox ID="Ch_deu" runat="server" Text="Pagador" CssClass="SubTitulos" AutoPostBack="True"
                                        TabIndex="3" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" TabIndex="4"
                                                    Width="90px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                    TabIndex="5" Width="20px" ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="Ib_ayudadeu" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                    Width="20px" Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    TabIndex="6" Width="220px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                                </td>
                            </tr>
                        </table>
                        
                        <table width="100%" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label25364" runat="server" CssClass="SubTitulos" Text="Datos Documentos"
                                        Width="140px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                    <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="Tipo Docto" CssClass="Label" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dr_tip_doc" runat="server" Width="200px" TabIndex="7" 
                                        CssClass="clsTxt">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Nro Docto" Width="100px" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_doc_des" runat="server" CssClass="clsTxt" Width="90px" TabIndex="9"></asp:TextBox>
                                </td>
                                <td width="30">
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label7" runat="server" Text="Monto Desde" CssClass="Label" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_mto_des" runat="server" CssClass="clsTxt" Width="90px" TabIndex="11"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label9" runat="server" Text="Fec Venc. Desde" CssClass="Label" Width="120px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_venc_des" runat="server" CssClass="clsTxt" Width="90px" TabIndex="13"></asp:TextBox>
                                </td>
                                <td rowspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Est Docto" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dr_est_doc" runat="server" Width="200px" TabIndex="8" 
                                        CssClass="clsTxt">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server" Text="Nro Otorg." CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_oto_des" runat="server" CssClass="clsTxt" Width="90px" TabIndex="10"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label8" runat="server" Text="Monto Hasta" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_mto_has" runat="server" CssClass="clsTxt" Width="90px" TabIndex="12"></asp:TextBox>
                                </td>
                                <td width="30">
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label10" runat="server" Text="Fec Venc. Hasta" CssClass="Label" Width="120px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_venc_has" runat="server" CssClass="clsTxt" Width="90px" TabIndex="14"></asp:TextBox>
                                </td>
                            </tr>
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                        
                        <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                ShowHeader="true" Width="100%" Height="80px">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ch_grid" runat="server" OnCheckedChanged="ch_grid_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="deu_ide" HeaderText="Nro. Identificación
">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pnu_des" HeaderText="Tipo Doc.">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="opo_otg" HeaderText="Nº Otg.">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_num" HeaderText="Nº Docto">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota">
                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec.Vcto">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsi_mto" HeaderText="Monto">
                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Moneda_ope" HeaderText="Moneda">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_cco" HeaderText="Cod.Cobr">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                            </asp:GridView>
                        </asp:Panel>
                        
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera" style="text-align: -moz-left" align="left">
                                    <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Datos Generales"
                                        Width="140px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td align="right" width="100">
                                                <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Depto."></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_Depto" runat="server" AutoPostBack="true" 
                                                    CssClass="clsMandatorio" Width="200px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender3" runat="server" IsSorted="true" PromptCssClass="Label"
                                                    PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains"
                                                    TargetControlID="DP_Depto">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label51" runat="server" CssClass="Label" Text="Municipio"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_Ciudad" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" TabIndex="18" Width="200px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" IsSorted="true" 
                                                    PromptCssClass="Label" PromptPosition="Bottom" PromptText="Escriba Para Buscar" 
                                                    QueryPattern="Contains" TargetControlID="DP_Ciudad">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="100">
                                                <asp:Label ID="Label25352" runat="server" CssClass="Label" Text="Fecha Pago"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_FechaPago" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" TabIndex="15" Width="90px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label25359" runat="server" CssClass="Label" Text="Horario"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RB_HORA" runat="server" CssClass="Label" 
                                                    RepeatDirection="Horizontal" TabIndex="16">
                                                    <asp:ListItem Selected="True">AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label25360" runat="server" CssClass="Label" 
                                                    Text="Suc.Recaudación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_SucRecaudacion" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" TabIndex="17" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="100">
                                                <asp:Label ID="Label52" runat="server" CssClass="Label" Text="Localidad/Barrio"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_Comuna" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" TabIndex="19" Width="150px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" IsSorted="true" 
                                                    PromptCssClass="Label" PromptPosition="Bottom" PromptText="Escriba Para Buscar" 
                                                    QueryPattern="Contains" TargetControlID="DP_Comuna">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label25361" runat="server" CssClass="Label" Text="Zona"></asp:Label>
                                            </td>
                                            <td width="240">
                                                <asp:TextBox ID="txt_GESIdZona" runat="server" CssClass="clsDisabled" 
                                                    Width="30px"></asp:TextBox>
                                                <asp:TextBox ID="txt_GESZona0" runat="server" CssClass="clsDisabled" 
                                                    ReadOnly="true" TabIndex="20" ToolTip="Volver" Width="190px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label50" runat="server" CssClass="Label" Text="Est.Cobranza"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_CodCobranza" runat="server" CssClass="clsMandatorio" 
                                                    TabIndex="22" Width="200px" />
                                                <cc1:ListSearchExtender ID="LSE_Dp_CodCodbranza" runat="server" IsSorted="true" 
                                                    PromptCssClass="Label" PromptPosition="Bottom" PromptText="Escriba Para Buscar" 
                                                    QueryPattern="Contains" TargetControlID="DP_CodCobranza">
                                                </cc1:ListSearchExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="100">
                                                <asp:Label ID="Label25362" runat="server" CssClass="Label" 
                                                    Text="Dirección de Pago"></asp:Label>
                                            </td>
                                            <td colspan="6">
                                                <asp:TextBox ID="Txt_direccion" runat="server" autocomplete="off" 
                                                    CssClass="clsMandatorio" TabIndex="21" Width="350px" />
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="100">
                                                <asp:Label ID="Label56" runat="server" CssClass="Label" Text="Observación"></asp:Label>
                                            </td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txt_ObservacionGestion" runat="server" 
                                                    CssClass="clsMandatorio" TabIndex="23" Width="350px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td> 
                    </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="ib_buscar" runat="server" TabIndex="23" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                    ToolTip="Buscar Documentos" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="IB_GuardaGestion" runat="server" AlternateText="Guardar Datos"
                                    ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" OnClick="IB_GuardaGestion_Click"
                                    TabIndex="24" ValidationGroup="VG2" ToolTip="Guardar" Enabled="false" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="IB_CancelarGestion" runat="server" AlternateText="Limpiar Selección"
                                    ImageUrl="~/Imagenes/Botones/Boton_limpiar_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_out.gif';"
                                    onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" TabIndex="25"
                                    ToolTip="Limpiar" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="IB_VolverGestion" runat="server" AlternateText="Volver" ImageUrl="~/Imagenes/Botones/Boton_volver_Out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_volver_in.gif';"
                                    TabIndex="26" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
            
            <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender1" runat="server" AcceptNegative="Left"
                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
            </cc1:MaskedEditExtender>
            <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars=" k,K">
            </cc1:FilteredTextBoxExtender>
            <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_rut_deu">
            </cc1:MaskedEditExtender>
            <cc1:FilteredTextBoxExtender ID="txt_dig_deu_FilteredTextBoxExtender" runat="server"
                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_dig_deu" ValidChars="k,K">
            </cc1:FilteredTextBoxExtender>
            <cc1:MaskedEditExtender ID="mask1" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder=""
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                MaskType="Number" TargetControlID="txt_doc_des">
            </cc1:MaskedEditExtender>
            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_des">
            </cc1:MaskedEditExtender>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar"
                Enabled="True" TargetControlID="txt_venc_des" Format="dd-MM-yyyy" PopupPosition="TopRight"
                FirstDayOfWeek="Monday">
            </cc1:CalendarExtender>
            <cc1:MaskedEditExtender ID="mask" runat="server" TargetControlID="txt_venc_des" Mask="99/99/9999"
                MaskType="Date">
            </cc1:MaskedEditExtender>
            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left"
                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_oto_des">
            </cc1:MaskedEditExtender>
            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_has">
            </cc1:MaskedEditExtender>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="radcalendar"
                Enabled="True" TargetControlID="txt_venc_has" Format="dd-MM-yyyy" PopupPosition="BottomRight"
                FirstDayOfWeek="Monday">
            </cc1:CalendarExtender>
            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_venc_has"
                Mask="99/99/9999" MaskType="Date">
            </cc1:MaskedEditExtender>
            <cc1:CalendarExtender ID="CALE_FechaPago" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                Format="dd-MM-yyyy" TargetControlID="txt_FechaPago" CssClass="radcalendar">
            </cc1:CalendarExtender>
            <asp:LinkButton ID="LinkButton2" runat="server" TabIndex="23"></asp:LinkButton>
            <asp:LinkButton ID="LinkButton3" runat="server"></asp:LinkButton>
            <asp:UpdateProgress ID="up" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <%--       <uc1:Cargando ID="Cargando1" runat="server" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <uc2:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
    </form>
    
</body>
</html>
