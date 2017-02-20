<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Inf_pagos.aspx.vb" Inherits="Modulos_Pagos_rightframe_archivos_Inf_pagos" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" style="width: 100%" cellspacing="1" class="Contenido">
                <tr>
                    <td align="center"  style="height: 31px" valign="middle" class="Cabecera">
                        <asp:Label ID="Label25353" runat="server" CssClass="Titulos" 
                            Text="Cancelaciòn - Informe de Pagos Realizados"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" height="590" style="padding: 5px" valign="top" align="center">
                    
                        <table id="Info" runat="server" cellpadding="0" cellspacing="0" width="950px">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label25392" runat="server" CssClass="SubTitulos" Text="Criterios Informe"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label25393" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Ch_suc0" runat="server" CssClass="Label" Text="Todas las Sucursales" Checked="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label25391" runat="server" CssClass="Label" Text="Ejecutivo"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Ch_cob" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                Text="Todos" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dr_eje" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                Width="150px">
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
                        <br />
                        <table id="cli_ddr" runat="server" cellspacing="0" width="950px">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label25356" runat="server" CssClass="SubTitulos" Text="Criterio Búsqueda por Cliente/Pagador"></asp:Label>
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
                                                            <asp:CheckBox ID="Ch_cli" runat="server" AutoPostBack="True" CssClass="Label"
                                                                TabIndex="3" Text="Cliente" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle">
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="90px"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="false" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="Txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                        <maskededitextender id="Txt_deu_Cli_Masked" runat="server" acceptnegative="Left"
                                                                            cultureampmplaceholder="" culturecurrencysymbolplaceholder="" culturedateformat=""
                                                                            culturedateplaceholder="" culturedecimalplaceholder="" culturethousandsplaceholder=""
                                                                            culturetimeplaceholder="" enabled="True" errortooltipenabled="True" inputdirection="RightToLeft"
                                                                            Mask="999,999,999,999" masktype="Number" targetcontrolid="Txt_Rut_Cli">
                                                                                    </maskededitextender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" AutoPostBack="true" CssClass="clsDisabled"
                                                                            MaxLength="1" Width="15px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20" Enabled="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            Width="300px"></asp:TextBox>
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
                                                        <td class="Cabecera" valign="top">
                                                            <asp:CheckBox ID="Ch_deu" runat="server" AutoPostBack="True" CssClass="Label"
                                                                TabIndex="3" Text="Pagador" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            TabIndex="4" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="false" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="Txt_Rut_Deu">
                                                                        </cc1:MaskedEditExtender>
                                                                        <maskededitextender id="Txt_deu_Cli_MaskedEditExtender1" runat="server" acceptnegative="Left"
                                                                            cultureampmplaceholder="" culturecurrencysymbolplaceholder="" culturedateformat=""
                                                                            culturedateplaceholder="" culturedecimalplaceholder="" culturethousandsplaceholder=""
                                                                            culturetimeplaceholder="" enabled="True" errortooltipenabled="True" inputdirection="RightToLeft"
                                                                            Mask="999,999,999,999" masktype="Number" targetcontrolid="Txt_Rut_Deu">
                                                                                </maskededitextender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" AutoPostBack="true" CssClass="clsDisabled"
                                                                            MaxLength="1" ReadOnly="True" TabIndex="5" Width="15px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="Ib_ayuda_deu" runat="server" AlternateText="Ayuda Deudores"
                                                                            Enabled="False" ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            TabIndex="6" Width="300px"></asp:TextBox>
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
                        <br />
                        <table id="Doc_Ope" runat="server" cellspacing="0" width="950px">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label25355" runat="server" CssClass="SubTitulos" Text="Criterio Búsqueda por Operación y Documentos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label25373" runat="server" CssClass="Label" Text="Tipo de Pago"></asp:Label>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Ch_tip" runat="server" AutoPostBack="True" Checked="True" CssClass="Label" Text="Todos" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dr_ti_pgo" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                Width="150px">
                                                                <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="1">Pago Directo</asp:ListItem>
                                                                <asp:ListItem Value="2">Recaudación</asp:ListItem>
                                                                <asp:ListItem Value="3">Reservas</asp:ListItem>
                                                                <asp:ListItem Value="4">Operación</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Estado de Pago"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Ch_est" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                Text="Todos" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dr_est_doc" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                Width="150px">
                                                                <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="V">Validado</asp:ListItem>
                                                                <asp:ListItem Value="R">Rechazado</asp:ListItem>
                                                                <asp:ListItem Value="I">Ingresado</asp:ListItem>
                                                                <asp:ListItem Value="S">Simulado</asp:ListItem>
                                                                <asp:ListItem Value="C">Canje</asp:ListItem>
                                                                <asp:ListItem Value="L">Liberado</asp:ListItem>
                                                                <asp:ListItem Value="P">Protestado</asp:ListItem>
                                                                <asp:ListItem Value="A">Anulado</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td colspan="0" rowspan="0">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label25378" runat="server" CssClass="Label" Text="Fecha Pago Desde"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_fec_pag_dde" runat="server" CssClass="clsTxt" TabIndex="10"
                                                                            Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_fec_pag_dde_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec_pag_dde"
                                                                            UserDateFormat="DayMonthYear">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_fec_pag_dde_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_fec_pag_dde">
                                                                        </cc1:CalendarExtender>
                                                                        <maskededitextender id="txt_ult_ges_dde_MaskedEditExtender" runat="server" cultureampmplaceholder=""
                                                                            culturecurrencysymbolplaceholder="" culturedateformat="" culturedateplaceholder=""
                                                                            culturedecimalplaceholder="" culturethousandsplaceholder="" culturetimeplaceholder=""
                                                                            enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txt_ult_ges_dde">
                                                                                    </maskededitextender>
                                                                        <calendarextender id="txt_ult_ges_dde_CalendarExtender" runat="server" cssclass="radcalendar"
                                                                            enabled="True" format="dd-MM-yyyy" popupposition="TopLeft" targetcontrolid="txt_ult_ges_dde">
                                                                                </calendarextender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label25379" runat="server" CssClass="Label" Text="Fecha Pago Hasta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_fec_pag_hta" runat="server" CssClass="clsTxt" TabIndex="11"
                                                                            Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_fec_pag_hta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec_pag_hta">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_fec_pag_hta_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" Format="dd-MM-yyyy" TargetControlID="txt_fec_pag_hta">
                                                                        </cc1:CalendarExtender>
                                                                        <maskededitextender id="txt_ult_ges_hta_MaskedEditExtender" runat="server" cultureampmplaceholder=""
                                                                            culturecurrencysymbolplaceholder="" culturedateformat="" culturedateplaceholder=""
                                                                            culturedecimalplaceholder="" culturethousandsplaceholder="" culturetimeplaceholder=""
                                                                            enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txt_ult_ges_hta">
                                                                                    </maskededitextender>
                                                                        <calendarextender id="txt_ult_ges_hta_CalendarExtender" runat="server" cssclass="radcalendar"
                                                                            enabled="True" format="dd-MM-yyyy" popupposition="TopLeft" targetcontrolid="txt_ult_ges_hta">
                                                                                </calendarextender>
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
                                                        <td colspan="0" rowspan="0">
                                                            <asp:Label ID="Label25394" runat="server" CssClass="Label" Text="Nº de Documento"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="0" rowspan="0">
                                                            <asp:TextBox ID="txt_nro_dde" runat="server" CssClass="clsTxt" TabIndex="10" Width="120px" MaxLength ="20"></asp:TextBox>
                                                           
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
                    <td align="right">
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Buscar" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" 
                                        TabIndex="23" ToolTip="Buscar Documentos" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" 
                                        AlternateText="Limpiar Selección" 
                                        ImageUrl="~/Imagenes/Botones/Boton_limpiar_Out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_Out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_limpiar_in.gif';" 
                                        TabIndex="25" ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Buscar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

