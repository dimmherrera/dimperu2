<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="CartolaDoctos.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_CartolaDoctos" Title="Documentos"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc2" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
    <script src="../FuncionesPrivadasJS/CartolaDoctos.js" type="text/javascript"></script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
    
    function DoScroll()
     {
        var _gridView = document.getElementById("GridViewDiv_Documentos");
        var _header = document.getElementById("HeaderDiv_Documentos");
         _header.scrollLeft = _gridView.scrollLeft;
     }
     function DoScroll_Otorgamientos()
     {
        var _gridView = document.getElementById("GridViewDiv_Otorgamientos");
        var _header = document.getElementById("HeaderDiv_Otorgamientos");
         _header.scrollLeft = _gridView.scrollLeft;
     }
     function DoScroll_Recaudacion()
     {
        var _gridView = document.getElementById("GridViewDiv_Recaudacion");
        var _header = document.getElementById("HeaderDiv_Recaudacion");
         _header.scrollLeft = _gridView.scrollLeft;
     }
     function DoScroll_Excedentes()
     {
        var _gridView = document.getElementById("GridViewDiv_Excedentes");
        var _header = document.getElementById("HeaderDiv_Excedentes");
         _header.scrollLeft = _gridView.scrollLeft;
     }
     function DoScroll_Cuentas()
     {
        var _gridView = document.getElementById("GridViewDiv_Cuentas");
        var _header = document.getElementById("HeaderDiv_Cuentas");
         _header.scrollLeft = _gridView.scrollLeft;
     }
     function DoScroll_Gestion()
     {
        var _gridView = document.getElementById("GridViewDiv_Gestion");
        var _header = document.getElementById("HeaderDiv_Gestion");
         _header.scrollLeft = _gridView.scrollLeft;
     }
    </script>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_CartolaDoctos">
        <ProgressTemplate>
            <uc2:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdatePanel runat="server" ID="UP_CartolaDoctos">
        <ContentTemplate>
            
            <table id="tb_gral" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td class = "Cabecera" style="height: 31px">
                        <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="Operaciones - Cartola Documentos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="Contenido" style="height: 580px; padding:5px" valign="top">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <cc2:TabContainer ID="TabContainer1" runat="server" Width="815px" ActiveTabIndex="0" >
                                        <cc2:TabPanel runat="server" ID="SucursalesyEje">
                                            <HeaderTemplate>
                                                Sucursales y Ejecutivos</HeaderTemplate>
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label60" runat="server" CssClass="SubTitulos" Text="Sucursales"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" style="height: 50px">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChKB_Suc" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DropSucursal" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                                        TabIndex="11" Width="300px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <%--<td align="center" class="Contenido" style="height: 50px">
                                                                        <asp:CheckBox ID="ChKB_Suc" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                        <asp:DropDownList ID="DropSucursal" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                            TabIndex="11" Width="300px">
                                                                        </asp:DropDownList>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label61" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                            top: 14px" Text="Ejecutivos"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" style="height: 50px">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Rb_Eje" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dp_Ejecu" runat="server" CssClass="clsTxt" Width="250px" AutoPostBack="True">
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
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="Cliente" runat="server">
                                            <HeaderTemplate>
                                                Cliente-Pagador</HeaderTemplate>
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left" class="Cabecera">
                                                            <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" Text="Cliente-Pagador"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" style="height: 50px">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChKB_Cli" runat="server" AutoPostBack="True" CssClass="Label" Text="Cliente" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                        ReadOnly="True"></asp:TextBox><asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled"
                                                                                            MaxLength="1" Width="15px" ReadOnly="True" AutoPostBack="True"></asp:TextBox><cc2:FilteredTextBoxExtender
                                                                                                ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                                TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                                            </cc2:FilteredTextBoxExtender>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                        Width="20px" Style="margin-top: 0px" Enabled="False" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChKB_Deu" runat="server" AutoPostBack="True" CssClass="Label" Text="Pagador" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                        ReadOnly="True"></asp:TextBox><asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled"
                                                                                            MaxLength="1" Width="15px" ReadOnly="True" AutoPostBack="True"></asp:TextBox><cc2:FilteredTextBoxExtender
                                                                                                ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                                TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                                                            </cc2:FilteredTextBoxExtender>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagador" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                        Width="20px" Style="margin-top: 0px" Enabled="False" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                        Width="350px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                        Width="350px"></asp:TextBox>
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
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="DatosDocumentos" runat="server">
                                            <HeaderTemplate>
                                                Datos Documentos</HeaderTemplate>
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="4">
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="310">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Estado"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Rb_Est" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dp_Est" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                                        Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="310">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Moneda"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Rb_Mon" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dp_Moneda" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                                        Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="310">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Con/Sin Cobranza"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="Rb_Cob" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Dp_Cobranza" runat="server" CssClass="clsTxt" Width="150px"
                                                                                        AutoPostBack="True">
                                                                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Con Cobranza</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Sin Cobranza</asp:ListItem>
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
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="NOtorDocto" runat="server">
                                            <HeaderTemplate>
                                                Nº de Otorgamiento y Documento</HeaderTemplate>
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="230">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="N° Otorgamiento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Oto_Dsd" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox><cc2:MaskedEditExtender
                                                                                        ID="Txt_Oto_Dsd_MaskedEditExtender" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                                        MaskType="Number" TargetControlID="Txt_Oto_Dsd">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Oto_Hta" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox><cc2:MaskedEditExtender
                                                                                        ID="Txt_Oto_Hta_MaskedEditExtender" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                                        MaskType="Number" TargetControlID="Txt_Oto_Hta">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="230">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="N° Documento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Doc_Dsd" runat="server" CssClass="clsTxt" Width="120px" MaxLength="20"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Doc_Hta" runat="server" CssClass="clsTxt" Width="120px" MaxLength="20"></asp:TextBox>
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
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="Fechas" runat="server">
                                            <HeaderTemplate>
                                                Fechas</HeaderTemplate>
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="230">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Fecha Vencimiento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_Ini" runat="server" CssClass="clsTxt" AutoPostBack="false"
                                                                                        TabIndex="1" Width="76px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="Txt_Fec_Ini"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc2:MaskedEditExtender>    
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_Ini_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" TargetControlID="Txt_Fec_Ini">
                                                                                    </cc2:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_Fin" runat="server" CssClass="clsTxt" AutoPostBack="false"
                                                                                        TabIndex="1" Width="76px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="Txt_Fec_Fin"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc2:MaskedEditExtender>        
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_Fin_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" TargetControlID="Txt_Fec_Fin">
                                                                                    </cc2:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="230">
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label14" runat="server" CssClass="SubTitulos" Text="Fecha Otorgamiento"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" align="center">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_OtoDsd" runat="server" CssClass="clsTxt" AutoPostBack="false"
                                                                                        TabIndex="1" Width="76px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Txt_Fec_OtoDsd"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc2:MaskedEditExtender>    
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_OtoDsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" TargetControlID="Txt_Fec_OtoDsd">
                                                                                    </cc2:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_OtoHta" runat="server" CssClass="clsTxt" AutoPostBack="false"
                                                                                        TabIndex="1" Width="76px"></asp:TextBox>
                                                                                        
                                                                                    <cc2:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="Txt_Fec_OtoHta"
                                                                                        Mask="99/99/9999" UserDateFormat="DayMonthYear" MaskType="Date">
                                                                                    </cc2:MaskedEditExtender>
                                                                                     
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_OtoHta_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" TargetControlID="Txt_Fec_OtoHta" PopupPosition="BottomRight">
                                                                                    </cc2:CalendarExtender>
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
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="Cobranza" runat="server">
                                            <HeaderTemplate>
                                                Estado Cobranza</HeaderTemplate>
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left" class="Cabecera">
                                                            <asp:Label ID="Label18" runat="server" CssClass="SubTitulos" Text="Estado Cobranza"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="Dp_EstCob" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                            Width="350px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="Responsabilidad" runat="server">
                                            <HeaderTemplate>
                                                Recurso</HeaderTemplate>
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label17" runat="server" CssClass="SubTitulos" Text="Con/Sin Recurso"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="Rb_Res" runat="server" AutoPostBack="True" CssClass="Label" Text="Todas" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Dp_Res" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                            Width="200px">
                                                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                            <asp:ListItem Value="1">Con Recurso</asp:ListItem>
                                                                            <asp:ListItem Value="2">Sin Recurso</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                    </cc2:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <asp:Panel ID="Panel_GV_Documentos" runat="server" ScrollBars="Horizontal" Width="1850px"
                                        Height="300px">
                                        <asp:GridView ID="GV_Documentos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            Width="1980px" ShowHeader="True">
                                            <FooterStyle BorderStyle="Dashed" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkB_CarDocto" runat="server" AutoPostBack="true" OnCheckedChanged="ChkB_CarDocto_Click"
                                                            Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cli_idc" HeaderText="NIT Cliente" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cli_rso" HeaderText="Razón Social" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="left" Width="250px" />
                                                    <FooterStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deu_rso" HeaderText="Razón Social" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="left" Width="250px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="id_opn" HeaderText="N° Ope.">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_num" HeaderText="N° Docto." ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="N° Cuotas" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_mto" HeaderText="Monto Docto." ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_mto_fin" HeaderText="Monto Financ." ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_fev_ori" HeaderText="Fecha Vto. Orig." ReadOnly="True"
                                                    NullDisplayText="  /  /">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_fev_rea" HeaderText="Fecha Vto. Real" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Est_Docto" HeaderText="Estado Docto." ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="est_veri" HeaderText="Estado Verif." ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="doc_sdo_cli" HeaderText="Sdo. Cliente">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="doc_sdo_ddr" HeaderText="Sdo. Pagador" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="moneda" HeaderText="Moneda" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="tipo_docto" HeaderText="Tipo Docto.">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_cbz_son" HeaderText="Con Cob.">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ope_res_son" HeaderText="Con Res.">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                    <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label30" runat="server" CssClass="SubTitulos" Text="Impresión de Cartolas"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="Contenido">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:CheckBox ID="Chk_Oto" runat="server" CssClass="Label" Enabled="False" Text="Otorgamiento" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:CheckBox ID="ChK_Rec" runat="server" CssClass="Label" Enabled="False" Text="Recaudación" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:CheckBox ID="Chk_Exce" runat="server" CssClass="Label" Enabled="False" Text="Reservas" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:CheckBox ID="Chk_Cuentas" runat="server" CssClass="Label" Enabled="False" Text="Otras Cuentas" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:CheckBox ID="Chk_Ges" runat="server" CssClass="Label" Enabled="False" Text="Gestiones" />
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
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="IB_Modificar" runat="server" Enabled="true" ImageUrl="~/Imagenes/Botones/boton_Modificar_Out.gif"
                                        OnClick="IB_Modificar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Modificar_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Modificar_In.gif';" ToolTip="Modificar Documentos" />
                                </td>
                                <td >
                                    <asp:ImageButton ID="IB_Cartola" runat="server" Enabled="true" ImageUrl="../../../Imagenes/Botones/cartola_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/cartola_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/cartola_in.gif';"
                                        ToolTip="Cartola Documentos" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        ToolTip="Buscar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Imprimir" runat="server" ImageUrl="~/Imagenes/Botones/boton_Imprimir_out.gif"
                                        OnClick="IB_Imprimir_Click" onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" ToolTip="Imprimir" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="Txt_PosGv" runat="server" />
            <asp:HiddenField ID="hf_otor" runat="server" />
            <asp:HiddenField ID="hf_rec" runat="server" />
            <asp:HiddenField ID="hf_exc" runat="server" />
            <asp:HiddenField ID="hf_otr_cta" runat="server" />
            <asp:HiddenField ID="hf_ges" runat="server" />
            <asp:LinkButton ID="LB_Buscar_Cli" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Buscar_Deu" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_DetalleDoctos" runat="server" OnClick="LB_DetalleDoctos_Click"></asp:LinkButton>
            <asp:LinkButton ID="LB_Valida_FechasVto" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Valida_FechasOto" runat="server"></asp:LinkButton>
            
        </ContentTemplate>
        
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
            <asp:PostBackTrigger ControlID="IB_Cartola" />
        </Triggers>
        
    </asp:UpdatePanel>
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <%--*********************************************************************************************--%>
            <%--Modificar Detalle Documentos--%>
            <asp:Panel ID="Panel_ModDoctos" runat="server" Height="1050px" Width="700px">
            
                <table class="Contenido" cellpadding="5" cellspacing="5" width="100%">
                    <tbody>
                        <tr>
                            <td align="left" class="Cabecera">
                                <asp:Label ID="Label105" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                    top: 14px" Text="Detalle documento a modificar"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <table cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label106" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                top: 14px" Text="Cliente"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table7" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <asp:TextBox ID="Txt_Rut_Cli2" runat="server" CssClass="clsDisabled" Width="90px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:TextBox ID="Txt_Dig_Cli2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                            Width="15px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td width="80%">
                                                        <asp:TextBox ID="Txt_Nom_Cli2" runat="server" CssClass="clsDisabled" Width="400px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <table id="Table9" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:CheckBox ID="ChkB_Deudor" runat="server" Text="Pagador" CssClass="SubTitulos"
                                                AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table8" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <asp:TextBox ID="Txt_Rut_Deu2" runat="server" CssClass="clsDisabled" Width="90px"
                                                            ReadOnly="True"> </asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Deu2_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu2" InputDirection="RightToLeft">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:TextBox ID="Txt_Dig_Deu2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                            Width="15px" onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_LB_Buscar_Deu);"
                                                            ReadOnly="True"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Dig_Deu2"
                                                         FilterType="Custom,Numbers" ValidChars="F,f">
                                                        </cc2:FilteredTextBoxExtender>
                                                    </td>
                                                    <td width="80%">
                                                        <asp:TextBox ID="Txt_Nom_Deu2" runat="server" CssClass="clsDisabled" Width="400px"
                                                            ReadOnly="True"> </asp:TextBox>
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
                                <br />
                                <table id="Table10" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label107" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                top: 14px" Text="Detalle documento"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table11" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                <tr>
                                                    <td align="right" width="50%">
                                                        <table id="Table12" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label108" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Nro. Ope."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Nro_Ope2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label109" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Nro. Otor."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Nro_Oto2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table id="Table13" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label110" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Moneda"></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Mon" runat="server" CssClass="clsDisabled" MaxLength="1" Width="100px"
                                                                        ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label111" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Monto"></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Mto" runat="server" CssClass="clsDisabled" MaxLength="1" Width="100px"
                                                                        ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <table id="Table14" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label112" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Tipo Docto."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Tip_Doc2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="150px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label113" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Est. Docto."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Est_Doc" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="120px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    <asp:Label ID="Label114" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                        top: 14px" Text="Fecha Vto."></asp:Label>
                                                                </td>
                                                                <td align="left" width="70%">
                                                                    <asp:TextBox ID="Txt_Fec_Vto" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                        Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center" width="50%">
                                                        <table id="Table15" cellpadding="0" cellspacing="0" runat="server" width="99%" border="1">
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:CheckBox ID="ChkB_Docto" runat="server" Text="Documento" CssClass="SubTitulos"
                                                                        AutoPostBack="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table id="table6" cellpadding="0" cellspacing="0" runat="server" width="100%">
                                                                        <tr>
                                                                            <td align="right" width="30%">
                                                                                <asp:Label ID="Label115" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                                    top: 14px" Text="Nro. Docto."></asp:Label>
                                                                            </td>
                                                                            <td align="left" width="70%">
                                                                                <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                                    Width="90px" ReadOnly="True"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Nro_Doc_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                    Enabled="True" Mask="999999999999" MaskType="Number" TargetControlID="Txt_Nro_Doc" InputDirection="RightToLeft">
                                                                                </cc2:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="30%">
                                                                                <asp:Label ID="Label116" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                                    top: 14px" Text="Cuota"></asp:Label>
                                                                            </td>
                                                                            <td align="left" width="70%">
                                                                                <asp:TextBox ID="Txt_Cuota2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                                    Width="90px" ReadOnly="True"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Cuota2_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                    Enabled="True" Mask="999" MaskType="Number" TargetControlID="Txt_Cuota2">
                                                                                </cc2:MaskedEditExtender>
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
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                
                                <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Guardar_Out.gif"
                                    OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" Enabled="true" 
                                    AlternateText="Guardar" />
                               
                                <asp:ImageButton ID="IB_Cancelar2" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                    Enabled="true" AlternateText="Cerrar" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <asp:LinkButton runat="server" ID="LB_ModDoctos"></asp:LinkButton>
            <cc2:ModalPopupExtender ID="MlPopupExt_ModDoctos" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel_ModDoctos" TargetControlID="LB_ModDoctos"
                X="615" Y="300">
            </cc2:ModalPopupExtender>
            <%--Fin Modificar Detalle Documentos--%>
            <%--*********************************************************************************************--%>
            <%--*********************************************************************************************--%>
            <%--Detalle Documentos--%>
            <asp:Panel ID="Panel_DetalleDoctos" runat="server" Height="700px" Width="900px"  >
                <table class="Contenido" cellpadding="5" cellspacing="5" width="100%">
                    <tr>
                        <td align="center">
                            <table class="Contenido" cellpadding="0" cellspacing="0">
                                <tbody>
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label31" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                top: 14px" Text="Cartolas"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table cellpadding="0" cellspacing="0" border="1" width="99%">
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="right" width="22%">
                                                                                            <asp:Label ID="Label62" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="NIT Cliente"></asp:Label>
                                                                                        </td>
                                                                                        <td width="22%">
                                                                                            <asp:TextBox ID="Txt_Rut_Cli_2" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                                Width="90px"></asp:TextBox>
                                                                                            <cc2:MaskedEditExtender ID="Txt_Rut_Cli_2_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli_2">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td width="12%">
                                                                                            <asp:TextBox ID="Txt_Dig_Cli_2" runat="server" CssClass="clsDisabled" MaxLength="1"
                                                                                                onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_LB_Buscar_Cli);" Width="15px" ReadOnly="true"></asp:TextBox>
                                                                                        </td>
                                                                                        <td width="22%">
                                                                                            <asp:Label ID="Label63" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Tipo Docto."></asp:Label>
                                                                                        </td>
                                                                                        <td width="22%">
                                                                                            <asp:TextBox ID="Txt_Tip_Doc" runat="server" CssClass="clsDisabled" Width="180px" ReadOnly="true"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="right" width="17%">
                                                                                            <asp:Label ID="Label64" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="N° Docto."></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" width="17%">
                                                                                            <asp:TextBox ID="Txt_Num_Doc" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                ReadOnly="True"></asp:TextBox>
                                                                                            <cc2:MaskedEditExtender ID="Txt_Num_Doc_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Num_Doc">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td align="right" width="16%">
                                                                                            <asp:Label ID="Label65" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Cod. Flujo"></asp:Label>
                                                                                        </td>
                                                                                        <td width="17%">
                                                                                            <asp:TextBox ID="Txt_Cod_Flj" runat="server" CssClass="clsDisabled" Width="90px"
                                                                                                ReadOnly="True"></asp:TextBox>
                                                                                            <cc2:MaskedEditExtender ID="Txt_Cod_Flj_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Cod_Flj">
                                                                                            </cc2:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td align="right" width="16%">
                                                                                            <asp:Label ID="Label66" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Cuota"></asp:Label>
                                                                                        </td>
                                                                                        <td width="17%">
                                                                                            <asp:TextBox ID="Txt_Cuota" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                                            <cc2:MaskedEditExtender ID="Txt_Cuota_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Cuota">
                                                                                            </cc2:MaskedEditExtender>
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
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="right" width="22%">
                                                                                            <asp:Label ID="Label67" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Razón Social"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" width="78%">
                                                                                            <asp:TextBox ID="Txt_Raz_SocCli" runat="server" CssClass="clsDisabled" Width="450px" ReadOnly="true"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="right" width="17%">
                                                                                            <asp:Label ID="Label68" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Fecha Venc."></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" width="83%">
                                                                                            <asp:TextBox ID="Txt_Fec_Ven" runat="server" CssClass="clsDisabled" onkeypress="fnTrapKD(LB_Valida_FechasVto)"
                                                                                                Height="20px" TabIndex="1" Width="90px" ReadOnly="True"></asp:TextBox>
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
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="right" width="22%">
                                                                                            <asp:Label ID="Label32" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Pagador"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" width="78%">
                                                                                            <asp:TextBox ID="Txt_Raz_SocDeu" runat="server" CssClass="clsDisabled" Width="450px" ReadOnly="true"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="right" width="17%">
                                                                                            <asp:Label ID="Label33" runat="server" CssClass="Label" Style="left: 8px; position: static;
                                                                                                top: 14px" Text="Fecha Últ. Pago"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" width="83%">
                                                                                            <asp:TextBox ID="Txt_Fec_Pag" runat="server" CssClass="clsDisabled" onkeypress="fnTrapKD(LB_Valida_FechasVto)"
                                                                                                Height="20px" TabIndex="1" Width="90px" ReadOnly="True"></asp:TextBox>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <br />
                                            <cc2:TabContainer ID="TabCntr_DetalleDocto" runat="server" ActiveTabIndex="0">
                                                <cc2:TabPanel ID="Pn_Otorgamientos" HeaderText="Otorgamientos" runat="server">
                                                    <HeaderTemplate>
                                                        Otorgamientos</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                        
                                                                        <asp:Panel ID="Panel_GV_Otorgamiento" runat="server" ScrollBars="Horizontal" width="1100px" height="180px">
                                                                       
                                                                        <asp:GridView ID="GV_Otorgamiento" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                                            CssClass="formatUltcell" Width="1970px">
                                                                            <FooterStyle BorderStyle="Dashed" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="id_ope" HeaderText="N° Ope." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="opo_otg" HeaderText="N° Otg." ReadOnly="True">
                                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ope_fec_sim" HeaderText="Fec. Sim." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="opo_fec_oto" HeaderText="Fec. Otg." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="opn_por_ant" HeaderText="% Antic.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="pnu_des" HeaderText="Tipo Operación" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="opn_can_doc" HeaderText="Cant." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_mto" HeaderText="Mto. Docto." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_mto_ant" HeaderText="Monto Antic." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ope_tot_gir" HeaderText="Mto. Gir." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ope_pre_com" HeaderText="Prec. Com." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ope_dif_pre" HeaderText="Dif. Prec." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ope_sal_pen" HeaderText="Sdo. Pen." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ope_sal_pag" HeaderText="Sdo. Pag.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        </asp:GridView>
                                                                         </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </cc2:TabPanel>
                                                <cc2:TabPanel ID="TP_Recaudacion" HeaderText="Recaudacion" runat="server">
                                                    <HeaderTemplate>
                                                        Recaudación</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%--<div id="HeaderDiv_Recaudacion" style="overflow: hidden; width: 1100px">
                                                                        <table id="Table2" border="0" cellpadding="0" cellspacing="0" class="cabeceraGrilla"
                                                                            width="1970px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label49" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha"></asp:Label>
                                                                                    </td>
                                                                                    <td width="150px">
                                                                                        <asp:Label ID="Label70" runat="server" CssClass="LabelCabeceraGrilla" Text="Tipo"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label71" runat="server" CssClass="LabelCabeceraGrilla" Text="N° Docto."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label72" runat="server" CssClass="LabelCabeceraGrilla" Text="Cuota"></asp:Label>
                                                                                    </td>
                                                                                    <td width="110px">
                                                                                        <asp:Label ID="Label73" runat="server" CssClass="LabelCabeceraGrilla" Text="Quien Paga"></asp:Label>
                                                                                    </td>
                                                                                    <td width="110px">
                                                                                        <asp:Label ID="Label74" runat="server" CssClass="LabelCabeceraGrilla" Text="Forma"></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label69" runat="server" CssClass="LabelCabeceraGrilla" Text="Interés Reem."></asp:Label>
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        <asp:Label ID="Label75" runat="server" CssClass="LabelCabeceraGrilla" Text="Int."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label76" runat="server" CssClass="LabelCabeceraGrilla" Text="Reaj."></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label77" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto. Tot."></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label78" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto. Abo."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label79" runat="server" CssClass="LabelCabeceraGrilla" Text="Tasa"></asp:Label>
                                                                                    </td>
                                                                                    <td width="110px">
                                                                                        <asp:Label ID="Label80" runat="server" CssClass="LabelCabeceraGrilla" Text="Est."></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label81" runat="server" CssClass="LabelCabeceraGrilla" Text="Sdo. Docto."></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>--%>
                                                                    <%--<div id="GridViewDiv_Recaudacion" style="overflow: scroll; width: 1100px; height: 150px"
                                                                        onscroll="DoScroll_Recaudacion()">--%>
                                                                        <asp:Panel ID="Panel_GV_Recaudacion" runat="server"  ScrollBars="Horizontal" width="1100px" height="180px">
                                                                        
                                                                        <asp:GridView runat="server" AutoGenerateColumns="False" CellPadding="0" ShowHeader="True"
                                                                            CssClass="formatUltcell" Width="1970px" ID="GV_Recaudacion">
                                                                            <FooterStyle BorderStyle="Dashed"></FooterStyle>
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ing_fec" HeaderText="Fecha" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="pnu_des" HeaderText="Tipo" ReadOnly="True">
                                                                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_num" HeaderText="N&#176; Docto." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_qpa" HeaderText="Quien Paga">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_tot_par" HeaderText="Forma" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Inter&#233;s Reem." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_mto_int" HeaderText="Int." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_rea_mon" HeaderText="Reaj." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_mto_tot" HeaderText="Mto. Tot." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_mto_abo" HeaderText="Mto. Abo." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_tas_apl" HeaderText="Tasa" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ing_vld_rcz" HeaderText="Est." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="doc_sdo_cli" HeaderText="Sdo. Docto.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                        </asp:GridView>
                                                                        </asp:Panel>
                                                                   <%-- </div>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </cc2:TabPanel>
                                                <cc2:TabPanel ID="Tb_Excedentes" HeaderText="Reservas" runat="server">
                                                    <HeaderTemplate>
                                                        Reservas</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%--<div id="HeaderDiv_Excedentes" style="overflow: hidden; width: 1100px">
                                                                        <table id="Table3" border="0" cellpadding="0" cellspacing="0" class="cabeceraGrilla"
                                                                            width="1970px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label82" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha"></asp:Label>
                                                                                    </td>
                                                                                    <td width="150px">
                                                                                        <asp:Label ID="Label83" runat="server" CssClass="LabelCabeceraGrilla" Text="Tipo"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label84" runat="server" CssClass="LabelCabeceraGrilla" Text="N° Docto."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label85" runat="server" CssClass="LabelCabeceraGrilla" Text="Cuota"></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label86" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto. Docto."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label87" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label88" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto. Reeb."></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>--%>
                                                                    <%--<div id="GridViewDiv_Excedentes" style="overflow: scroll; width: 1100px; height: 150px"
                                                                        onscroll="DoScroll_Excedentes()">--%>
                                                                        <asp:Panel ID="Pane_GV_Excedentes" runat="server" ScrollBars="Horizontal" width="1100px" height="180px">
                                                                        
                                                                        <asp:GridView ID="GV_Excedentes" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                                            CssClass="formatUltcell" ShowHeader="True" Width="1970px">
                                                                            <FooterStyle BorderStyle="Dashed" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="egr_fec" HeaderText="Fecha" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="pnu_des" HeaderText="Tipo" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_num" HeaderText="N° Docto." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_mto" HeaderText="% Antic.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="moneda" HeaderText="Moneda" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="egr_mto" HeaderText="Mto. Reeb." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        </asp:GridView>
                                                                        </asp:Panel>
                                                                    <%--</div>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </cc2:TabPanel>
                                                <cc2:TabPanel ID="Tb_Cuentas" HeaderText="Cuentas" runat="server">
                                                    <HeaderTemplate>
                                                        Cuentas</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%--<div id="HeaderDiv_Cuentas" style="overflow: hidden; width: 1100px">
                                                                        <table id="Table4" border="0" cellpadding="0" cellspacing="0" class="cabeceraGrilla"
                                                                            width="1970px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label89" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha"></asp:Label>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                        <asp:Label ID="Label90" runat="server" CssClass="LabelCabeceraGrilla" Text="Descripción"></asp:Label>
                                                                                    </td>
                                                                                    <td width="110px">
                                                                                        <asp:Label ID="Label91" runat="server" CssClass="LabelCabeceraGrilla" Text="N° Cuenta"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label92" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto. Cuenta"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label93" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto. Cuenta"></asp:Label>
                                                                                    </td>
                                                                                    <td width="110px">
                                                                                        <asp:Label ID="Label94" runat="server" CssClass="LabelCabeceraGrilla" Text="Saldo"></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label95" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec. Ult. Pago"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label96" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>--%>
                                                                   <%-- <div id="GridViewDiv_Cuentas" style="overflow: scroll; width: 1100px; height: 150px"
                                                                        onscroll="DoScroll_Cuentas()">--%>
                                                                        <asp:Panel ID="Panel_GV_Cuentas" runat="server" ScrollBars="Horizontal" width="1100px" height="180px">
                                                                        
                                                                        <asp:GridView ID="GV_Cuentas" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                                            CssClass="formatUltcell" ShowHeader="True" Width="1970px">
                                                                            <FooterStyle BorderStyle="Dashed" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="cxc_fec" HeaderText="Fecha" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxc_des" HeaderText="Descripción" ReadOnly="True">
                                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="id_cxc" HeaderText="N° Cuenta" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxc_mto" HeaderText="Mto. Cuenta" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxc_ful_pgo" HeaderText="Fec. Ult. Pago" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="pnu_des" HeaderText="Moneda" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        </asp:GridView>
                                                                        </asp:Panel>
                                                                   <%-- </div>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </cc2:TabPanel>
                                                <cc2:TabPanel ID="Tb_GestionDocto" HeaderText="Gestión Documentos" runat="server">
                                                    <HeaderTemplate>
                                                        Gestión Documentos</HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%--<div id="HeaderDiv_Gestion" style="overflow: hidden; width: 1100px">
                                                                        <table id="Table6" border="0" cellpadding="0" cellspacing="0" class="cabeceraGrilla"
                                                                            width="1970px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label97" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha Ges."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label98" runat="server" CssClass="LabelCabeceraGrilla" Text="Hora Ges."></asp:Label>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                        <asp:Label ID="Label99" runat="server" CssClass="LabelCabeceraGrilla" Text="Telefonista"></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label100" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha Pago"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label101" runat="server" CssClass="LabelCabeceraGrilla" Text="Hora Pago"></asp:Label>
                                                                                    </td>
                                                                                    <td width="120px">
                                                                                        <asp:Label ID="Label102" runat="server" CssClass="LabelCabeceraGrilla" Text="Fec. Prox. Ges."></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:Label ID="Label103" runat="server" CssClass="LabelCabeceraGrilla" Text="Hora Prox. Ges."></asp:Label>
                                                                                    </td>
                                                                                    <td width="400px">
                                                                                        <asp:Label ID="Label104" runat="server" CssClass="LabelCabeceraGrilla" Text="Observaciones"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>--%>
                                                                    <%--<div id="GridViewDiv_Gestion" style="overflow: scroll; width: 1100px; height: 150px"
                                                                        onscroll="DoScroll_Gestion()">--%>
                                                                        <asp:Panel ID="Panel_GV_Gestion" runat="server" ScrollBars="Horizontal" width="1100px" height="180px">
                                                                        
                                                                        <asp:GridView ID="GV_Gestion" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                                            CssClass="formatUltcell" ShowHeader="True" Width="1970px">
                                                                            <FooterStyle BorderStyle="Dashed" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="gsn_fec" HeaderText="Fecha Ges." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="gsn_hor" HeaderText="Hora Ges." ReadOnly="True">
                                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="eje_nom" HeaderText="Telefonista" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="gsn_fec_pag" HeaderText="Fecha Pago" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="gsn_hor_pag" HeaderText="Hora Pago">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="gsn_fec_prx" HeaderText="Fec. Prox. Ges." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="gsn_hor_prx" HeaderText="Hora Prox. Ges." ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="gsn_obs" HeaderText="Observaciones" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="400px" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        </asp:GridView>
                                                                        </asp:Panel>
                                                                    <%--</div>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </cc2:TabPanel>
                                            </cc2:TabContainer>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                       
                                                        <asp:ImageButton ID="IB_Prev_Detalle" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                        <asp:ImageButton ID="IB_Next_Detalle" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%--<asp:ImageButton ID="IB_Imprimir_2" runat="server" ImageUrl="~/Imagenes/Botones/boton_Imprimir_out.gif"
                                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                                            onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" ToolTip="Imprimir" />--%>
                                            <asp:ImageButton ID="IB_Cancelar" runat="server" ImageUrl="~/Imagenes/Botones/boton_volver_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_volver_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_volver_in.gif';"
                                                Enabled="true" AlternateText="Cancelar" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:LinkButton runat="server" ID="LB_DetalleDoumentos"></asp:LinkButton>
            <asp:LinkButton ID="L_cli" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="L_deu" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscaDeudor" runat="server"></asp:LinkButton>
            
            <cc2:ModalPopupExtender ID="MlPopupExt_DetalleDoctos" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="Panel_DetalleDoctos" 
                TargetControlID="LB_DetalleDoumentos" X="390" Y="300">
            </cc2:ModalPopupExtender>
            <%--Fin Detalle Documentos--%>
            <%--*********************************************************************************************--%>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="LB_Valida_FechasVto" />
        </Triggers>
    </asp:UpdatePanel>
   <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
</asp:Content> 