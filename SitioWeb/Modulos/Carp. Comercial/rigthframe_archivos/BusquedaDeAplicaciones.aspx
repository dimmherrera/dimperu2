<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BusquedaDeAplicaciones.aspx.vb" Inherits="BusquedaDeAplicaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Busqueda de Aplicaciones</title>
    <base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
    
    <script language="javascript">
    function DoScroll()
     {
        var _gridView = document.getElementById("GridViewDiv");
        var _header = document.getElementById("HeaderDiv");
        _header.scrollLeft = _gridView.scrollLeft;
     }
    </script>    
</head>

<body>

    <form id="form1" runat="server" style="margin:10px">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table id="tb_gral" cellspacing="0" cellpadding="0" width="700px" border="0">
        <tr>
            <td class="Cabecera" height="31px">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Aplicaciones"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" style="padding: 5px">
                 
                <table id="tb_Criterio">
                    <tr>
                        <td valign="top" colspan="2">
                            <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:CheckBox ID="CB_Cliente" runat="server" CssClass="SubTitulos" 
                                            Text="Cliente" AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" 
                                                            __designer:wfdid="w285"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                            CssClass="clsMandatorio" Width="90px" __designer:wfdid="w286" ></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                        </cc2:MaskedEditExtender>
                                                        <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                            CssClass="clsMandatorio" Width="15px" __designer:wfdid="w286" MaxLength="1" AutoPostBack="true" ></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                            runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                            TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                        </cc2:FilteredTextBoxExtender>
                                                      
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes"
                                                         ImageUrl="../../../Imagenes/Iconos/155.ICO" width="20" Enabled="false" />
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                            Text="Razón Soc." Width="70px"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                             ReadOnly="True" Style="position: static" Width="400px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                        
                    </tr>
                    <tr>
                        <td>
                             <table id="tb_Ejecutivos" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Ejecutivo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:CheckBox ID="CB_TodosEje" runat="server" CssClass="Label" Text="Todos" 
                                                        AutoPostBack="True" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DP_Ejecutivos" runat="server" CssClass="clsDisabled" Enabled="false"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" >
                            <table id="tb_Aplicacion" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Aplicaciones"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fecha_Desde" runat="server" CssClass="clsMandatorio" 
                                                        Width="90px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_Fecha_Desde_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Desde">
                                                    </cc2:MaskedEditExtender>
                                                    <cc2:CalendarExtender ID="Txt_Fecha_Desde_CalendarExtender" runat="server" 
                                                        CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                        Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Desde">
                                                    </cc2:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fecha_Hasta" runat="server" CssClass="clsMandatorio" 
                                                        Width="90px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_Fecha_Hasta_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Hasta">
                                                    </cc2:MaskedEditExtender>
                                                    <cc2:CalendarExtender ID="Txt_Fecha_Hasta_CalendarExtender" runat="server" 
                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                                        TargetControlID="Txt_Fecha_Hasta" CssClass="radcalendar">
                                                    </cc2:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:RadioButton ID="RB_Pendiente" runat="server" CssClass="Label" GroupName="Estado"
                                                        Text="Pendientes" />
                                                </td>
                                                <td align="center">
                                                    <asp:RadioButton ID="RB_VB" runat="server" CssClass="Label" GroupName="Estado"
                                                        Text="V°B°" />
                                                </td>
                                                <td align="center" colspan="2">
                                                    <asp:RadioButton ID="RB_Cursadas" runat="server" CssClass="Label" GroupName="Estado"
                                                        Text="Cursadas" />
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
                </table>
                
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <%--<div id="HeaderDiv" style="overflow: hidden; width: 650px">
                                <table class="Cabecera" width="1380px">
                                    <tr>
                                        <td width="100" align="left">
                                            <asp:Label ID="Label75" runat="server" Text="NIT Cliente" 
                                                CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="200" align="left">
                                            <asp:Label ID="Label78" runat="server" Text="Razón Social" 
                                                CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="80" align="left">
                                            <asp:Label ID="Label82" runat="server" Text="Fecha" CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="left">
                                            <asp:Label ID="Label83" runat="server" Text="Ejecutivo" CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label84" runat="server" Text="Monto Exc." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label1" runat="server" Text="Monto DNC." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label6" runat="server" Text="Monto CXP." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label7" runat="server" Text="Monto CXC." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Monto DOC." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label9" runat="server" Text="Tasa Cli." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label10" runat="server" Text="Tasa Apli." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="100" align="right">
                                            <asp:Label ID="Label11" runat="server" Text="Devolver" CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                        <td width="150" align="right">
                                            <asp:Label ID="Label14" runat="server" Text="Observación." CssClass="LabelCabeceraGrilla"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <div id="GridViewDiv" style="overflow: scroll; width: 650px; height: 300px" onscroll="DoScroll()">
                                <asp:GridView ID="GV_Aplicaciones" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                    PageSize="1" AllowSorting="True" ShowHeader="True" Width="1380px">
                                    <FooterStyle CssClass="cabeceraGrilla" />
                                    <Columns>
                                        <asp:BoundField DataField="Rut" HeaderText="NIT Cliente">
                                            <ItemStyle HorizontalAlign="left" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Razón Social">
                                            <ItemStyle HorizontalAlign="Left" Width="200" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="apl_fec" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="center" Width="80" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo">
                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_Exc" HeaderText="Monto Rec.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_DNC" HeaderText="Monto DNC.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_CXP" HeaderText="Monto CXP.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_CXC" HeaderText="Monto CXC.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_DVG" HeaderText="Monto DOC.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tasa_Cli" HeaderText="Tasa Cli.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tasa_Apli" HeaderText="Tasa Apli.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Devuelto" HeaderText="Devolver">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Observacion" HeaderText="Observacion">
                                            <ItemStyle HorizontalAlign="Right" Width="150" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="350" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td class="Label" bgcolor="#CCFFCC" align="center">
                                        Aprobada
                                    </td>
                                    <td class="Label" bgcolor="#99CCFF" align="center">
                                        Por Aprobar
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <asp:LinkButton ID="Lb_buscar" runat="server" __designer:wfdid="w372" OnClick="Lb_buscar_Click"
                     Style="position: static" TabIndex="54" ValidationGroup="Cliente"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="height: 75px" align="right">
                
                <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                     onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" runat="server"
                     ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" AlternateText="Buscar Clientes"
                     ToolTip="Buscar"></asp:ImageButton>
            </td>
        </tr>
    </table>
    
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
    
</body>
</html>
