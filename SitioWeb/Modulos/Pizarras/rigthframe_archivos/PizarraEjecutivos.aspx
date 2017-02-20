<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="PizarraEjecutivos.aspx.vb" Inherits="PizarraEjecutivos" Title="Pizarra Comercial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../../FuncionesJS/Prototype.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Tooltip.js" type="text/javascript"></script>
    
    <script src="../FuncionesPrivadasJS/PizarraEjecutivos.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/PizarraAprobaciones.js" type="text/javascript"></script>
    

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript">

        var t1;
      
        function showToolTip(e, custId, custRut, tipo) {
            //init();
            var url = 'DetalleFlujo.aspx';

            if (tipo == 1) {
                var qstr = 'neg=' + custId + "&rut=" + custRut + "&ms=" + new Date().getTime();

                var req = new Ajax.Request(
			url,
        {
            method: 'get',
            parameters: qstr,
            onComplete: showTooltipNeg
        });
            }

            if (tipo == 2) {
                var qstr = 'sim=' + custId + "&rut=" + custRut + "&ms=" + new Date().getTime();

                var req = new Ajax.Request(
			url,
        {
            method: 'get',
            parameters: qstr,
            onComplete: showTooltipSim
        });
            }

            if (tipo == 3) {
                var qstr = 'oto=' + custId + "&rut=" + custRut + "&ms=" + new Date().getTime();

                var req = new Ajax.Request(
			url,
        {
            method: 'get',
            parameters: qstr,
            onComplete: showTooltipOto
        });
            }

            if (t1) t1.Show(e, "<br><br>Cargando...");
        }

        function showTooltipNeg(res) {
            //alert(resres.responseText);
            var t = res.responseText;
            //debugger;
            var x = eval('(' + t + ')');
            var i = x.Negociación.Items.length;
            var str = "<table width=110% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
            str += "<tr bgcolor='skyblue'>"
            str += "<td><b>Fecha de Neg.</b></td>";
            str += "<td><b>Transcurrido desde Sim.(hh:mm)</b></td>";
            str += "<td><b>Ejecutivo</b></td>";

            var fin = 0;
            var navegador = navigator.appName;

            if (navegador == "Microsoft Internet Explorer")
                fin = i - 1;
            else if (navigator.appName == "Netscape")
                fin = i;
            
            for (var c = 0; c < fin; c++) {
                str += "<tr>";
                str += "<td align='left'>" + x.Negociación.Items[c].Fecha + "</td>";
                str += "<td align='right'>" + x.Negociación.Items[c].Transcurrido + "</td>";
                str += "<td align='right'>" + x.Negociación.Items[c].Ejecutivo + "</td>";
                str += "</tr>";
            }

            str += "</tbody></table>";
            t1.SetHTML(str);
        }

        function showTooltipSim(res) {
            var t = res.responseText;
            //debugger;
            var x = eval('(' + t + ')');
            var i = x.Simulacion.Items.length;
            var str = "<table width=110% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
            str += "<tr bgcolor='skyblue'>"
            str += "<td><b>Fecha de Sim.</b></td>";
            str += "<td><b>Transcurrido desde Sim.(hh:mm)</b></td>";
            str += "<td><b>Ejecutivo</b></td>";

            var fin = 0;
            var navegador = navigator.appName;

            if (navegador == "Microsoft Internet Explorer")
                fin = i - 1;
            else if (navigator.appName == "Netscape")
                fin = i;
                
            for (var c = 0; c < fin; c++) {
                str += "<tr>";
                str += "<td align='left'>" + x.Simulacion.Items[c].Fecha + "</td>";
                str += "<td align='right'>" + x.Simulacion.Items[c].Transcurrido + "</td>";
                str += "<td align='right'>" + x.Simulacion.Items[c].Ejecutivo + "</td>";
                str += "</tr>";
            }

            str += "</tbody></table>";
            t1.SetHTML(str);
        }

        function showTooltipOto(res) {
            var t = res.responseText;
            //debugger;
            var x = eval('(' + t + ')');
            var i = x.Otorgamiento.Items.length;
            var str = "<table width=110% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
            str += "<tr bgcolor='skyblue'>"
            str += "<td><b>Fecha de Oto.</b></td>";
            str += "<td><b>Transcurrido desde Sim.(hh:mm)</b></td>";
            str += "<td><b>Ejecutivo</b></td>";

            var fin = 0;
            var navegador = navigator.appName;

            if (navegador == "Microsoft Internet Explorer")
                fin = i - 1;
            else if (navigator.appName == "Netscape")
                fin = i;
                
            for (var c = 0; c < fin; c++) {
                str += "<tr>";
                str += "<td align='left'>" + x.Otorgamiento.Items[c].Fecha + "</td>";
                str += "<td align='right'>" + x.Otorgamiento.Items[c].Transcurrido + "</td>";
                str += "<td align='right'>" + x.Otorgamiento.Items[c].Ejecutivo + "</td>";
                str += "</tr>";
            }

            str += "</tbody></table>";
            t1.SetHTML(str);
        }

        function hideTooltip(e) {
            if (t1) t1.Hide(e);
        }

        function init() {
            t1 = new ToolTip("ToolTipFlujo", true, 30);
        }

        Event.observe(window, 'load', init, false);
        
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tr>
                    <td class = "Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Pizarra Comercial"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 10px; height: 530px;" valign="top" align="center">
                    
                        <table id="Table1" cellspacing="2" cellpadding="2" border="0">
                            <tr>
                                <td align="center">
                                    <table id="tb_Ejecutivos" cellspacing="0" cellpadding="0" border="0" width="330">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Ejecutivo"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CB_TodosEje" runat="server" CssClass="Label" Text="Todos" AutoPostBack="True" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DP_Ejecutivos" runat="server" CssClass="clsMandatorio" Enabled="true"
                                                                Width="250px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center">
                                    <table ID="Table2" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label41" runat="server" CssClass="SubTitulos" 
                                                    Text="Fecha de Evaluación"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="Contenido" style="height: 40px; padding: 3px" 
                                                valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tbody>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Fec_Desde" runat="server" CssClass="clsMandatorio" 
                                                                    Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                <cc2:CalendarExtender ID="Txt_Fec_Desde_CalendarExtender" runat="server" 
                                                                    CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                    Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Desde">
                                                                </cc2:CalendarExtender>
                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Desde_MaskedEditExtender" runat="server" 
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Desde" 
                                                                    UserDateFormat="DayMonthYear">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Hasta" 
                                                                    Width="70px"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Fec_Hasta" runat="server" CssClass="clsMandatorio" 
                                                                    Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                <cc2:CalendarExtender ID="Txt_Fec_Hasta_CalendarExtender" runat="server" 
                                                                    CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                    Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Hasta">
                                                                </cc2:CalendarExtender>
                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Hasta_MaskedEditExtender" runat="server" 
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Hasta" 
                                                                    UserDateFormat="DayMonthYear">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center">
                                    <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:CheckBox ID="CB_Cliente" runat="server" CssClass="SubTitulos" Text="Por Proveedor especifico"
                                                    AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Número Identificación Proveedor" __designer:wfdid="w285"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                                    CssClass="clsDisabled" Width="90px" ReadOnly="true" ></asp:TextBox>
                                                                <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                </cc2:MaskedEditExtender>
                                                                <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                                    CssClass="clsDisabled" Width="15px" ReadOnly="true" MaxLength="1" 
                                                                    onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_Lb_buscar);" AutoPostBack="True"></asp:TextBox>
                                                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                    TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                </cc2:FilteredTextBoxExtender>
                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server"
                                                                   ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" Enabled="False"/>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                                    Text="Nombre / Razón Social Proveedor" Width="70px"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                     ReadOnly="True" Style="position: static" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="center" colspan="3">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Font-Size="8px" 
                                                    Text="Negociación"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Font-Size="8px" 
                                                    Text="Simulación"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Font-Size="8px" 
                                                    Text="Aprobación"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="Label" Font-Size="8px" 
                                                    Text="Otorgamiento"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_01.gif" />
                                            </td>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_07.gif" 
                                                    style="width: 40px; height: 30px" />
                                            </td>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_02.gif" />
                                            </td>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_07.gif" 
                                                    style="width: 40px; height: 30px" />
                                            </td>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_03.gif" />
                                            </td>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_07.gif" 
                                                    style="width: 40px; height: 30px" />
                                            </td>
                                            <td>
                                                <img alt="" src="../../../Imagenes/Flujo/a_04.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center">
                                    &nbsp;</td>
                                <td align="center">
                                    &nbsp;</td>
                            </tr>--%>
                        </table>
                        <br />
                        
                        <table border="0" cellpadding="0" cellspacing="0" width="1000px">
                            <tr>
                                <td style="height:350px" valign="top">
                                        <asp:GridView ID="GV_Evaluaciones" runat="server" CssClass="formatUltcell"
                                            AutoGenerateColumns="False" PageSize="1" AllowSorting="True" CellPadding="4"
                                            HorizontalAlign="Justify" GridLines="Horizontal">
                                            
                                            <Columns>
                                                <asp:BoundField DataField="RutCli" HeaderText="Número Identificación Proveedor">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Razon" HeaderText="Nombre / Razón Social Proveedor" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Monto" HeaderText="Monto Antic." HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Tasa" HeaderText="Tasa" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PorAnt" HeaderText="% Neg." HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Plazo" HeaderText="Plazo" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle HorizontalAlign="center" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Fecha" HeaderText="Fecha Ing." DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Negociación">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Neg" runat="server" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "Neg") > 0, "../../../Imagenes/Flujo/a_01.gif", "../../../Imagenes/Flujo/a_01_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Con1" runat="server" Width="30px" Height="30px" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "Neg") > 0, "../../../Imagenes/Flujo/a_07.gif","../../../Imagenes/Flujo/a_07_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Simulación">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Sim" runat="server" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "Sim") > 0, "../../../Imagenes/Flujo/a_02.gif", "../../../Imagenes/Flujo/a_02_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Con2" runat="server" Width="30px" Height="30px" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "Sim") > 0, "../../../Imagenes/Flujo/a_07.gif","../../../Imagenes/Flujo/a_07_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aprobación">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Apb" runat="server" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "APB") > 0, "../../../Imagenes/Flujo/a_03.gif", "../../../Imagenes/Flujo/a_03_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Con3" runat="server" Width="30px" Height="30px" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "APB") > 0, "../../../Imagenes/Flujo/a_07.gif","../../../Imagenes/Flujo/a_07_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Otorgamiento">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Img_Oto" runat="server" ImageUrl='<%# iif(DataBinder.Eval(Container.DataItem, "Oto") > 0, "../../../Imagenes/Flujo/a_04.gif","../../../Imagenes/Flujo/a_04_B.gif")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Neg" HeaderText="">
                                                    <ItemStyle HorizontalAlign="center" Width="0px" ForeColor="White" />
                                                </asp:BoundField>
                                              
                                            </Columns>
                                                <HeaderStyle  CssClass="cabeceraGrilla" Height="30px" />
                                                <RowStyle CssClass="formatUltcell" />
                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                </td>
                                
                            </tr>
                            <tr>
                            
                                <td align="right">
                                    <asp:Label ID="Lbl_Pagina" runat="server" CssClass="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    
                                    <asp:ImageButton ID="IB_Prev" runat="server" 
                                        ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                    <asp:ImageButton ID="IB_Next" runat="server" 
                                        ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <img src="../../../Imagenes/Infografia/Anuladas.gif" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle" height="50">
                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" 
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" 
                            ToolTip="Buscar" />
                            
                            <asp:ImageButton ID="Ib_limpiar" runat="server" 
                            ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                             onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" 
                            onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"  />
                    </td>
                </tr>
            </table>
            
            <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Lb_buscar" runat="server"></asp:LinkButton>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:HiddenField ID="AscRut" runat="server" />
    <asp:HiddenField ID="AscNom" runat="server" />
    <asp:HiddenField ID="AscMon" runat="server" />
    <asp:HiddenField ID="AscMto" runat="server" />
    <asp:HiddenField ID="AscFec" runat="server" />
    
    <asp:HiddenField ID="HF_NroNeg" runat="server" />
    <asp:HiddenField ID="HF_PosNeg" runat="server" />
    <asp:HiddenField ID="HF_NroNNC" runat="server" />
    <asp:HiddenField ID="HF_PosNNC" runat="server" />
    
    <asp:HiddenField ID="HF_NroCCF" runat="server" />
    <asp:HiddenField ID="HF_Estado" runat="server" />
    <asp:HiddenField ID="HF_NroOpe" runat="server" />
    
    <div id="ToolTipFlujo" style="font-family: Tahoma; font-size: small; background-color: white; width: 400px; height: 50px; border: solid 1px gray; text-align: center; filter: alpha(Opacity=85); opacity: 0.85;display:none">
    </div>
    
</asp:Content>

