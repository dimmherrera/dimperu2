<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="GestCobranza.aspx.vb" Inherits="GestCobranza" Title="Gestión de Cobranza" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table id="Tabla general" border="0" cellpadding="0" cellspacing="1" width="100%"class="Contenido">
                <tr>
                    <td class="Cabecera" height="31">
                        <asp:Label ID="Label1" runat="server" Text="Cobranza - Gestión Cobranza" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="height: 590px;padding:5px" valign="top" align="center" >
                        <table id="Contenedora" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center">
                                    <%--*********Tabla datos Principales********--%>
                                    <table id="datos Principales" border="0" cellpadding="0" cellspacing="0" 
                                        width="400">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label2" runat="server" Text="Datos Principales" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table id="Controles" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" Text="Estado Doctos." CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Drop_Est" runat="server" CssClass="clsMandatorio" Width="100px">
                                                                <asp:ListItem Value="1">VIGENTE</asp:ListItem>
                                                                <asp:ListItem Value="2">MOROSO</asp:ListItem>
                                                                <asp:ListItem Value="3">TODOS</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label4" runat="server" Text="Moneda" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Drop_Moneda" runat="server" CssClass="clsMandatorio" Width="100px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <%--********Cliente Deudor*********--%>
                            <tr>
                                <td align="center">
                                    <table id="Cliente Deudor" border="0" cellpadding="0" cellspacing="0" width="700px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label5" runat="server" Text="Cliente / Pagador" 
                                                    CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table id="Controles Cliente" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:CheckBox ID="Check_Cli" runat="server" Text="Por Cliente" CssClass="Label" 
                                                                AutoPostBack="True" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                Width="15px" AutoPostBack="True" MaxLength="1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                            </cc1:FilteredTextBoxExtender>
                                                            
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                Width="20px" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:CheckBox ID="Check_Deu" runat="server" AutoPostBack="True" CssClass="Label"
                                                                Text="Por Pagador" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_Rut_Deu_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                TargetControlID="txt_Rut_Deu">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                Width="15px" AutoPostBack="True" MaxLength="1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                            </cc1:FilteredTextBoxExtender>
                                                          
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagador" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                border="0" Width="20px" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <%--********Cob Telefonico*********--%>
                            <tr>
                                <td align="center">
                                    <table id="Cob Telefonico" border="0" cellpadding="0" cellspacing="0" 
                                        width="400">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label8" runat="server" Text="Cobrador Telefónico" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="left">
                                                <table id="Controles Cob Telefonico" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Check_Fono" runat="server" Text="Todos" CssClass="Label" AutoPostBack="True" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label9" runat="server" Text="Cobrador" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Drop_Cobradora" runat="server" CssClass="clsMandatorio" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <%--*********tip Doc*********--%>
                            <tr>
                                <td align="center">
                                    <table id="tip Doc" border="0" cellpadding="0" cellspacing="0" width="400">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label10" runat="server" Text="Tipo Documento" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="left">
                                                <table id="Controles Tp doc" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Check_Tp_Doc" runat="server" Text="Todos" CssClass="Label" AutoPostBack="True" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label11" runat="server" Text="Tipo Documento" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Drop_TP_Doc" runat="server" CssClass="clsMandatorio" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <%--********Gestion entre fechas******--%>
                            <tr>
                                <td align="center">
                                    <table id="Gestion entre fechas" border="0" cellpadding="0" cellspacing="0" 
                                        width="400">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label12" runat="server" Text="Gestión Entre Fechas" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="left">
                                                <table id="Controles Gestion entre fechas" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Check_Ult_Gest" runat="server" Text="Ultima Gestión" CssClass="Label"
                                                                AutoPostBack="True" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label13" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Gest_Dsd" runat="server" CssClass="clsMandatorio" Width="90px"
                                                                MaxLength="10" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txt_Gest_Dsd_CalendarExtender" runat="server" Enabled="True"
                                                                FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_Gest_Dsd" CssClass="radcalendar">
                                                            </cc1:CalendarExtender>
                                                            <cc1:MaskedEditExtender ID="txt_Gest_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" Mask="99/99/9999" TargetControlID="txt_Gest_Dsd" MaskType="Date"
                                                                AcceptAMPM="True" AutoComplete="False" ErrorTooltipEnabled="True">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label14" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Gest_Hst" runat="server" CssClass="clsMandatorio" Width="90px"
                                                                MaxLength="10" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_Gest_Hst_MaskedEditExtender" runat="server" AutoComplete="False"
                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Gest_Hst">
                                                            </cc1:MaskedEditExtender>
                                                            <cc1:CalendarExtender ID="txt_Gest_Hst_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_Gest_Hst">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <%--*******Est de cobranza********--%>
                            <tr>
                                <td align="center">
                                    <table id="Est de cobranza" border="0" cellpadding="0" cellspacing="0" 
                                        width="400">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label15" runat="server" Text="Estado de Cobranza" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="left">
                                                <table id="Controles estado de cob" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Check_EstCob" runat="server" Text="Todos" CssClass="Label" AutoPostBack="True" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label16" runat="server" Text="Cod. Desde" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Est_Dsd" runat="server" CssClass="clsMandatorio" Width="80"
                                                                MaxLength="4"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="txt_Est_Dsd_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txt_Est_Dsd">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label17" runat="server" Text="Cod. Hasta" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Est_Hst" runat="server" CssClass="clsMandatorio" Width="80"
                                                                MaxLength="4"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="txt_Est_Hst_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txt_Est_Hst">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                              
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprimir" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            Style="position: static" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            Style="position: static" />
                    </td>
                </tr>
            </table>
            <asp:DropDownList ID="Drop_Segmento" runat="server" CssClass="clsMandatorio" Visible="False"
                Width="200px">
            </asp:DropDownList>
            <asp:DropDownList ID="Drop_Seg" runat="server" CssClass="clsMandatorio" Visible="False"
                Width="200px">
            </asp:DropDownList>
            <asp:LinkButton ID="lb_Deu" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lb_cli" runat="server"></asp:LinkButton>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
