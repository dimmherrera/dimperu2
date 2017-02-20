<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Informes_cobranza.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Informes_cobranza" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 97%" cellpadding="0">
                <tr>
                    <td style="background-image: url('../../../Imagenes/Barras/barramain2.jpg');
                        height: 31px" valign="top" align="left">
                        <asp:Label ID="Label25353" runat="server" CssClass="Titulos" 
                            Text="Informes de Cobranza"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="600" class="Contenido" valign="top" style="padding: 5px">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                
                                    <table id="Info" runat="server" style="width: 100%" cellpadding="0" cellspacing="0">
                                         <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label25392" runat="server" CssClass="SubTitulos" 
                                                    Text="Criterios Informe"></asp:Label>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top">
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label25390" runat="server" CssClass="SubTitulos" 
                                                                            Text="Tipo de Informe"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido">
                                                                        <asp:DropDownList ID="Dr_tip_inf" runat="server" CssClass="clsMandatorio" 
                                                                            Width="300px" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                            <asp:ListItem Value="1">Gest.No Realizadas en el dia</asp:ListItem>
                                                                            <asp:ListItem Value="2">Gest.con Est. 100,310 o mayor 400</asp:ListItem>
                                                                            <asp:ListItem Value="3">Doctos. con 310 o 320 con Proc.Auto.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25393" runat="server" CssClass="SubTitulos" Text="Sucursal"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Ch_suc0" runat="server" CssClass="Label" 
                                                                                        Text="Todas las Sucursales" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25391" runat="server" CssClass="SubTitulos" Text="Cobrador"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Ch_cob" runat="server" CssClass="Label" Text="Todos" 
                                                                                        AutoPostBack="True" Checked="True" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dr_eje" runat="server" Width="150px" 
                                                                                        CssClass="clsDisabled" Enabled="False">
                                                                                    </asp:DropDownList>
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
                                        <table id="cli_ddr" runat="server" cellspacing="0" width="978">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label25356" runat="server" CssClass="SubTitulos" 
                                                    Text="Criterio Busqueda por Cliente/Deudor"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                       <table>
                                        <tr>
                                            <td align="left"> 
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:CheckBox ID="Ch_cli" runat="server" AutoPostBack="True" 
                                                                CssClass="SubTitulos" TabIndex="3" Text="Cliente" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle">
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" 
                                                                            Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender1" runat="server" 
                                                                            AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                            MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" AutoPostBack="true" 
                                                                            CssClass="clsDisabled" MaxLength="1" Width="20px"></asp:TextBox>
                                                                      
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes"
                                                                         ImageUrl="../../../Imagenes/Iconos/155.ICO" width="20" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="220px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" >
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" valign="top">
                                                            <asp:CheckBox ID="Ch_deu" runat="server" AutoPostBack="True" 
                                                                CssClass="SubTitulos" TabIndex="3" Text="Deudor" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" 
                                                                            TabIndex="4" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender" runat="server" 
                                                                            AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                            MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" AutoPostBack="true" 
                                                                            CssClass="clsDisabled" MaxLength="1" TabIndex="5" Width="20px" 
                                                                            ReadOnly="True"></asp:TextBox>
                                                                         <asp:ImageButton ID="Ib_ayuda_deu" runat="server" AlternateText="Ayuda Deudores"
                                                                         
                                                                             ImageUrl="../../../Imagenes/Iconos/155.ICO" width="20" Enabled="False" />
                                                                    
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" TabIndex="6" Width="220px"></asp:TextBox>
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
                                
                                
                              
                                    <table  id="Doc_Ope" runat=server cellspacing="0" width="978px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label25355" runat="server" CssClass="SubTitulos" 
                                                    Text="Criterio Busqueda por Operación y Documentos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25373" runat="server" CssClass="SubTitulos" 
                                                                            Text="Tipo Documento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Ch_tip0" runat="server" CssClass="Label" Text="Todos" 
                                                                                        AutoPostBack="True" Checked="True" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dr_tip_doc" runat="server" Width="150px" 
                                                                                        CssClass="clsDisabled" Enabled="False">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25374" runat="server" CssClass="SubTitulos" 
                                                                            Text="Fecha Vencimiento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25375" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_venc_des" runat="server" CssClass="clsTxt" TabIndex="9" 
                                                                                        Width="90px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_venc_des_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_venc_des">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_venc_des_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        PopupPosition="TopLeft" TargetControlID="txt_venc_des">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25376" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_venc_has" runat="server" CssClass="clsTxt" TabIndex="9" 
                                                                                        Width="90px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_venc_has_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_venc_has">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_venc_has_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        PopupPosition="TopLeft" TargetControlID="txt_venc_has">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25377" runat="server" CssClass="SubTitulos" 
                                                                            Text="Fecha Ult. Gestión"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25378" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_ult_ges_dde" runat="server" CssClass="clsTxt" TabIndex="10" 
                                                                                        Width="90px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_ult_ges_dde_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ult_ges_dde">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_ult_ges_dde_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        PopupPosition="TopLeft" TargetControlID="txt_ult_ges_dde">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25379" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_ult_ges_hta" runat="server" CssClass="clsTxt" TabIndex="11" 
                                                                                        Width="90px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_ult_ges_hta_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ult_ges_hta">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_ult_ges_hta_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        PopupPosition="TopLeft" TargetControlID="txt_ult_ges_hta">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25380" runat="server" CssClass="SubTitulos" 
                                                                            Text="Monto Documento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25381" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_mto_des0" runat="server" CssClass="clsTxt" TabIndex="12" 
                                                                                        Width="90px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_mto_des0_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_des0" 
                                                                                        InputDirection="RightToLeft">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25382" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_mto_has0" runat="server" CssClass="clsTxt" TabIndex="13" 
                                                                                        Width="90px"></asp:TextBox>
                                                                                      <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_has0" 
                                                                                        InputDirection="RightToLeft">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25383" runat="server" CssClass="SubTitulos" 
                                                                            Text="Estado Cobranza"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25384" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_cco_des0" runat="server" CssClass="clsTxt" TabIndex="14" 
                                                                                        Width="90px" MaxLength="4"></asp:TextBox>
                                                                                        
                                                                                    <cc1:FilteredTextBoxExtender ID="txt_cco_des0_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers" 
                                                                                        TargetControlID="txt_cco_des0">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                        
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25385" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_cco_has0" runat="server" CssClass="clsTxt" TabIndex="15" 
                                                                                        Width="90px" MaxLength="4"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="txt_cco_has0_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" TargetControlID="txt_cco_has0" 
                                                                                        FilterType="Numbers">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                       
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera" colspan="0" rowspan="0">
                                                                        <asp:Label ID="Label25386" runat="server" CssClass="SubTitulos" 
                                                                            Text="Fecha Proceso"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="0" rowspan="0">
                                                                        <table class="Contenido">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25388" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_proc_des" runat="server" CssClass="clsDisabled" TabIndex="16" 
                                                                                        Width="90px" Enabled="False"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_proc_des_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_proc_des">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_proc_des_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        PopupPosition="TopLeft" TargetControlID="txt_proc_des">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label25389" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_proc_has" runat="server" CssClass="clsDisabled" TabIndex="17" 
                                                                                        Width="90px" Enabled="False"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_proc_has_MaskedEditExtender" runat="server" 
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_proc_has">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_proc_has_CalendarExtender" runat="server" 
                                                                                        CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                                                        PopupPosition="TopLeft" TargetControlID="txt_proc_has">
                                                                                    </cc1:CalendarExtender>
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
                                
                                    <table width="980px">
                                        <tr>
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
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        TabIndex="23" ToolTip="Buscar Documentos" />
                                    
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar Selección"
                                        ImageUrl="~/Imagenes/Botones/Boton_limpiar_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_limpiar_in.gif';" TabIndex="25"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
  
                    </td>
                </tr>
            </table>
 </ContentTemplate>
 <Triggers>
 <asp:PostBackTrigger ControlID="IB_Buscar" />
 <asp:PostBackTrigger ControlID="IB_Limpiar" />
 
 </Triggers>
    </asp:UpdatePanel>
</asp:Content>

