<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Evaluaciones.aspx.vb" Inherits="Cls_Evaluaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<%--<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Servicio de Informe de Evaluaciones</title>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <script src="../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../Carp.%20Comercial/FuncionesPrivadasJS/EvaluacionCliDeu.js" type="text/javascript"></script>

    <script src="../../FuncionesJS/Grilla.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
        
            <table id="tb_gral" cellspacing="0" cellpadding="0" width="700" border="0">
                <tr>
                    <td class="Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Servicio de Informes de Evaluación Cliente / Deudor"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 10px">
                        <asp:Panel Style="position: static" ID="Panel_Contenedor" runat="server" Width="100%" Height="400px" ScrollBars="Auto">
                            <table id="cliente_diarios" border="0" style="height: 100px" width="95%">
                                <tr>
                                    <%--Cliente--%>
                                    <td valign="top">
                                        <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="Cabecera" align="left">
                                                        <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label1" runat="server"
                                                            CssClass="SubTitulos" Text="Cliente" __designer:wfdid="w284"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" valign="top" style="height: 70px">
                                                    
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                                            CssClass="clsMandatorio" Width="90px" __designer:wfdid="w286" ></asp:TextBox>
                                                                        <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                                            CssClass="clsMandatorio" Width="15px" __designer:wfdid="w286" MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                                        <a href="javascript:WinOpen(2,'../Ayudas/AyudaCli.aspx','PopUpCliente',580,410,200,150);">
                                                                            <img id="Img_AyudaCli" tabindex="3" src="../../Imagenes/Iconos/155.ICO" width="20"
                                                                                border="0" />
                                                                        </a>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" 
                                                                            Style="position: static" Text="Razon Soc." Width="70px"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" 
                                                                            CssClass="clsDisabled"  ReadOnly="True" Style="position: static" 
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label41" runat="server" __designer:wfdid="w288" CssClass="Label" 
                                                                            Style="position: static" Text="Fecha Desde"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fecha_Dsd" runat="server" CssClass="clsMandatorio" 
                                                                            Width="90px"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Fecha_Dsd_MaskedEditExtender" runat="server" 
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Dsd">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="Txt_Fecha_Dsd_CalendarExtender" runat="server" 
                                                                            CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                            Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Dsd">
                                                                        </cc2:CalendarExtender>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label42" runat="server" __designer:wfdid="w288" CssClass="Label" 
                                                                            Style="position: static" Text="Fecha Hasta"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Fecha_Hst" runat="server" CssClass="clsMandatorio" 
                                                                            Width="90px"></asp:TextBox>
                                                                         <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Hst">
                                                                        </cc2:MaskedEditExtender>
                                                                        <cc2:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                            CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                            Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Hst">
                                                                        </cc2:CalendarExtender>
                                                                            
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label43" runat="server" __designer:wfdid="w288" CssClass="Label" 
                                                                            Style="position: static" Text="Estados"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DP_Estados" runat="server" CssClass="clsTxt" 
                                                                            Width="100px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label20" runat="server" Text="Listado de Evaluaciones" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" valign="top" align="left">
                                            <table id="Table5" border="0" cellpadding="0" cellspacing="0" class="cabeceraGrilla">
                                                <tr>
                                                    <td width="50">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LabelCabeceraGrilla" Text="Nro."></asp:Label>
                                                    </td>
                                                    <td width="80">
                                                        <asp:Label ID="Label21" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                    </td>
                                                    <td width="80">
                                                        <asp:Label ID="Label5" runat="server" CssClass="LabelCabeceraGrilla" Text="% Anticipo"></asp:Label>
                                                    </td>
                                                    <td width="110">
                                                        <asp:Label ID="Label6" runat="server" CssClass="LabelCabeceraGrilla" Text="Monto"></asp:Label>
                                                    </td>
                                                    <td width="80">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LabelCabeceraGrilla" Text="#  Deudores"></asp:Label>
                                                    </td>
                                                    <%--
                                                <td width="200">
                                                    <asp:Label ID="Label4" runat="server" CssClass="LabelCabeceraGrilla" Text="Ejecutivo"></asp:Label>
                                                </td>
                                                --%>
                                                    <td width="100">
                                                        <asp:Label ID="Label7" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha"></asp:Label>
                                                    </td>
                                                    <td width="110">
                                                        <asp:Label ID="Label9" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado Neg."></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="overflow-y: scroll; display: block; overflow: hidden; height: 200px"
                                                id="DIV3" align="left">
                                                <asp:GridView ID="GV_Evaluaciones" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                    CssClass="formatUltcell">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Nro" DataField="Codigo">
                                                            <ItemStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Moneda" DataField="Moneda">
                                                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Porcentaje" DataField="Porcentaje">
                                                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:###,###,###.00}">
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Deudores" HeaderText="Cantidad Deu.">
                                                            <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <%--
                                                    <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo">
                                                        <ItemStyle Width="200px" />
                                                    </asp:BoundField>
                                                    --%>
                                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha Crea." DataFormatString="{0:dd/MM/yyyy}">
                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Estado" DataField="Estado">
                                                            <ItemStyle Width="100px" HorizontalAlign="left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../Imagenes/Botones/Boton_Buscar_In.gif';"
                            onmouseout="this.src='../../Imagenes/Botones/Boton_Buscar_out.gif';" 
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" 
                            ValidationGroup="Cliente" ToolTip="Buscar Evaluaciones"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Informe" 
                            onmouseover="this.src='../../Imagenes/Botones/boton_imprimir_in.gif';"
                            onmouseout="this.src='../../Imagenes/Botones/boton_imprimir_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                            ToolTip="Imprimir Evaluación" Enabled="False"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                            ToolTip="Limpiar Pantalla"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_in.gif';"
                            onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_out.gif';" 
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif"
                            ToolTip="Volver"></asp:ImageButton>
                                
                    </td>
                </tr>
            </table>
            
           <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
               CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
               CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
               CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
               Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
           </cc2:MaskedEditExtender>
            
            <asp:HiddenField ID="HF_Nro_Eva" runat="server" />
            <asp:HiddenField ID="HF_Posicion" runat="server" />
            
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Informe" />
        </Triggers>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
