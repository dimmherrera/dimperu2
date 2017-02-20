<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="CarteraVigMor.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_CarteraVigMor" Title="Cartera Vigente/Morosa"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script src="../FuncionesProvadasJS/Verificacion.js" type="text/javascript"></script>
<script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
<link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel runat="server" ID="UP_CarteraVigMor" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tb_gral" cellpadding="0" cellspacing ="1" width="100%" class="Contenido">
                <tr>
                    <td class = "Cabecera" style="height: 31px">
                        <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="Cobranza - Cartera Vigente/Morosa"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" height="590px" valign="top" align="center" style="text-align:-moz-center">
                        <table cellpadding="5" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <%--Fecha, estado y moneda--%>
                                    <table cellpadding="0" cellspacing="0" border="0" width="700px">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Informe"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="40%">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Fecha de Informe"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Fec_Inf" runat="server" CssClass="clsMandatorio" Height="20px"
                                                                            TabIndex="1" Width="76px" AutoPostBack="True"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Inf_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Inf">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="Txt_Fec_Inf_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Inf">
                                                                        </cc2:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="60%">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                            top: 14px" Text="Estado"></asp:Label>
                                                                    </td>
                                                                    <td align="left" width="70%">
                                                                        <asp:DropDownList ID="Dp_Est" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                            Width="330px">
                                                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                            <asp:ListItem Value="1">Vigente</asp:ListItem>
                                                                            <asp:ListItem Value="2">Morosa</asp:ListItem>
                                                                            <asp:ListItem Value="3">Todas</asp:ListItem>
                                                                            <asp:ListItem Value="4">Documento vence dentro de 30 dias
                                                                            </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                            top: 14px" Text="Moneda"></asp:Label>
                                                                    </td>
                                                                    <td align="left" width="70%">
                                                                        <asp:DropDownList ID="Dp_Tip_Mon" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                            Width="330px">
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Sucursal--%>
                                    <table cellpadding="0" cellspacing="0" border="0" width="700px">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Por Sucursal"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="center">
                                                <table cellpadding="0" cellspacing="0" width="500">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButton ID="Rb_Suc" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                GroupName="Suc" Text="Todas" />
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="Dp_Suc" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                            TabIndex="6" Width="250px">
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Ejecutivos--%>
                                    <table cellpadding="0" cellspacing="0" border="0" width="700px">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="Ejecutivos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="center">
                                                <table cellpadding="0" cellspacing="0" width="500">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:RadioButton ID="Rb_Eje" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                GroupName="Eje" Text="Todos" />
                                                        </td>
                                                        <td align="left">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Ejecutivos"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DP_Eje" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                            TabIndex="6" Width="250px">
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Cliente y Deudor--%>
                                    <table cellpadding="0" cellspacing="0" border="0" width="700px">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" 
                                                    Text="Cliente - Pagador"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" >
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="50%">
                                                                        <asp:CheckBox ID="ChKB_Cli" runat="server" CssClass="Label" Text="Cliente" AutoPostBack="True" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="90px"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc2:MaskedEditExtender>
                                                                     
                                                                        
                                                                    </td>
                                                                    <td>
                                                                       <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                            ReadOnly="true" Width="15px" AutoPostBack="True"></asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td align="left">
                                                                    <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Enabled="False" />    
                                                                    </td>
                                                                    <td>
                                                                    
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="510px"></asp:TextBox>
                                                                    
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="ChKB_Deu" runat="server" CssClass="Label" Text="Pagador" 
                                                                            AutoPostBack="True" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" Width="90px"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                        </cc2:MaskedEditExtender>
                                                                        
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                            MaxLength="1" onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_LB_Buscar_Deu);"
                                                                            Width="15px" AutoPostBack="True"></asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="AyudaDeu" 
                                                                            Enabled="False" ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" />
                                                                    </td>
                                                                    <td>
                                                                    
                                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="510px"></asp:TextBox>
                                                                    
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Cartera Cliente--%>
                                    <table cellpadding="0" cellspacing ="0" border="0" width="700px">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label9" runat="server" CssClass="SubTitulos" Text="Por Cartera Cliente"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr align="left">
                                                        <td align="left" >
                                                        
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="center" width="90">
                                                                        <asp:RadioButton ID="RB_Car" runat="server" AutoPostBack="True" 
                                                                        Checked="True" CssClass="Label" GroupName="Car" Text="Todas" />
                                                                    </td>
                                                                    <td align="right" width="105" >
                                                                        <asp:Label ID="Label10" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                            top: 14px" Text="Cartera"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    <asp:DropDownList ID="DP_Car" runat="server" AutoPostBack="True" 
                                                                            CssClass="clsTxt" TabIndex="6" Width="330px">
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Codigos de Cobranza--%>
                                    <table border="0" cellpadding="0" cellspacing="0" width="700px">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label12" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                top: 14px" Text="Por Código Cobranza"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td align="left">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:RadioButton ID="RB_Cob" runat="server" AutoPostBack="True" Checked="True" 
                                                                            CssClass="Label" GroupName="Cob" Text="Todas" />
                                                                    </td>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0" align="left">
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Est. Cob. Desde"></asp:Label>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:DropDownList ID="DP_CodCobranza_Desde" runat="server" CssClass="clsTxt" 
                                                                                        Width="350px" AutoPostBack="True" />
                                                                                    <cc2:ListSearchExtender ID="LSE_Dp_CodCodbranza" runat="server" IsSorted="true" PromptCssClass="Label"
                                                                                        PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains" 
                                                                                        TargetControlID="DP_CodCobranza_Desde">
                                                                                    </cc2:ListSearchExtender>
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Est. Cob. Hasta"></asp:Label>
                                                                                </td>
                                                                                <td align="center">
                                                                                <asp:DropDownList ID="DP_CodCobranza_Hasta" runat="server" CssClass="clsTxt" 
                                                                                        Width="350px" AutoPostBack="True" />
                                                                                    <cc2:ListSearchExtender ID="ListSearchExtender1" runat="server" IsSorted="true" PromptCssClass="Label"
                                                                                        PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains"
                                                                                        TargetControlID="DP_CodCobranza_Hasta">
                                                                                    </cc2:ListSearchExtender>
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Documentos--%>
                                    <table border="0" cellpadding="0" cellspacing="0" width="700px">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label15" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                top: 14px" Text="Por Documento"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td align="center" style="width: 87px">
                                                            <asp:RadioButton ID="RB_Doc" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                GroupName="Doc" Text="Todas" />
                                                        </td>
                                                        <td align="left">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Tipo Documento"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="Dp_Tip_Doc" runat="server" AutoPostBack="True" 
                                                                            CssClass="clsTxt" TabIndex="6" Width="160px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label20" runat="server" CssClass="Label" Text="N° Otorgamiento"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Nro_Oto" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Nro_Oto_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Nro_Oto">
                                                                        </cc2:MaskedEditExtender>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label22" runat="server" CssClass="Label" Text="N° Documento"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsTxt" Width="120px" MaxLength="20"></asp:TextBox>
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%--Mora--%>
                                    <table border="0" cellpadding="0" cellspacing="0" width="700px">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:CheckBox ID="ChKB_MorCarSup" runat="server" CssClass="SubTitulos" 
                                                    Text="Mora Cartera Super" AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td align="center" width="40%">
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td align="center" width="50%">
                                                                        <asp:Label ID="Label19" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                        top: 14px" Text="Cantidad de días"></asp:Label>
                                                                    </td>
                                                                    <td align="left" width="50%">                                                                    
                                                                        <asp:DropDownList ID="Dp_CanDias" runat="server" AutoPostBack="True" 
                                                                            CssClass="clsTxt" Enabled="False" TabIndex="6" Width="160px">
                                                                        </asp:DropDownList>                                                                    
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left" width="60%">
                                                            <asp:CheckBox ID="ChKB_CanDias" runat="server" CssClass="Label" 
                                                                Text="Solo Facturas" />
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
                        <asp:ImageButton ID="IB_Imprimir" runat="server" ImageUrl="~/Imagenes/Botones/boton_Imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" ToolTip="Imprimir" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Limpiar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            ToolTip="Limpiar" />
                    </td>
                </tr>
            </table>
            

            <asp:LinkButton ID="LB_Buscar_Cli" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Buscar_Deu" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Cobranza_Dsd" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Cobranza_Hst" runat="server"></asp:LinkButton>
            
        </ContentTemplate>
        
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
            <asp:PostBackTrigger ControlID="IB_Limpiar" />
        </Triggers>
        
    </asp:UpdatePanel>
    
</asp:Content>