<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="PizarraOtorgamiento.aspx.vb" Inherits="PizarraOperaciones" title="Pizarra Operaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="ResumenOperacion.ascx" tagname="ResumenOperacion" tagprefix="uc1" %>
<%@ Register src="DetalleOperacion.ascx" tagname="WebUserControl" tagprefix="uc2" %>
<%@ Register src="DetalleOperacion.ascx" tagname="DetalleOperacion" tagprefix="uc3" %>
<%@ Register src="LineaFinanciamiento.ascx" tagname="LineaFinanciamiento" tagprefix="uc4" %>
<%@ Register src="MRequisitos.ascx" tagname="MRequisitos" tagprefix="uc5" %>
<%@ Register src="MCondiciones.ascx" tagname="MCondiciones" tagprefix="uc6" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>
<%@ Register src="Documentacion.ascx" tagname="Documentacion" tagprefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <script src="../FuncionesPrivadasJS/PizarraOperaciones.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Prototype.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Tooltip.js" type="text/javascript"></script>

    <script language="javascript">
    var t1;
    var t2;
    
    function showClasificacion(e, custId) {
        //init();
        
        var url = 'DetalleClasificacion.aspx';
        var qstr = 'id=' + custId + "&ms=" + new Date().getTime();

        var req = new Ajax.Request(
			url,
        {
            method: 'get',
            parameters: qstr,
            onComplete: showTooltipCla
        });
        if (t1) t1.Show(e, "<br><br>Cargando...");
    }

    function showTooltipCla(res) {
        var t = res.responseText;
        //debugger;
        var x = eval('(' + t + ')');
        var i = x.Clasificacion.Items.length;
        var str = "<table width=100% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
        str += "<tr bgcolor='skyblue'><td><b>Descripcion</b></td>";
        str += "<td><b>Desde</b></td>";
        str += "<td><b>Hasta</b></td>";
        //str += "<td><b>Quantity</b></td>";

        var fin = 0;
        var navegador = navigator.appName;

        if (navegador == "Microsoft Internet Explorer")
            fin = i - 1;
        else if (navigator.appName == "Netscape")
            fin = i;

        for (var c = 0; c < fin; c++) {
            if (x.Clasificacion.Items[c].Marca == 1) {
                str += "<tr style='color: red'>";
            }
            else {
                str += "<tr>";
            }
            str += "<td align='left'>" + x.Clasificacion.Items[c].Descripcion + "</td>";
            str += "<td align='right'>" + x.Clasificacion.Items[c].Desde + "</td>";
            str += "<td align='right'>" + x.Clasificacion.Items[c].Hasta + "</td>";
            //str += "<td>" + x.Clasificacion.Items[c].Quantity + "</td>";
            str += "</tr>";
        }

        str += "</tbody></table>";
        t1.SetHTML(str);
    }

    function showObservacion(e, custId) {
        //init();
        var url = 'ObservacionFirmas.aspx';
        var qstr = 'id=' + custId + "&ms=" + new Date().getTime();

        var req = new Ajax.Request(
			url,
        {
            method: 'get',
            parameters: qstr,
            onComplete: showTooltipObs
        });
        if (t2) t2.Show(e, "<br><br>Loading...");
    }

    function showTooltipObs(res) {
        var t = res.responseText;
        //debugger;
        var x = eval('(' + t + ')');
        var i = x.Observacion.Items.length;
        var str = "<table width=100% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
        str += "<tr bgcolor='skyblue'>";
        str += "<td width='100' align='center'><b>Fecha</b></td>";
        str += "<td><b>Observacion</b></td>";
        str += "<td><b>Usuario</b></td>";
        str += "</tr>";

        var fin = 0;
        var navegador = navigator.appName;

        if (navegador == "Microsoft Internet Explorer")
            fin = i - 1;
        else if (navigator.appName == "Netscape")
            fin = i;
            
        for (var c = 0; c < fin; c++) {
            str += "<tr>";
            str += "<td width='100' align='center'>" + x.Observacion.Items[c].Fecha + "</td>";
            str += "<td valign='top'>" + x.Observacion.Items[c].Observacion + "</td>";
            str += "<td valign='top'>" + x.Observacion.Items[c].Usuario + "</td>";
            str += "</tr>";
        }

        str += "</tbody></table>";
        t2.SetHTML(str);
    }

    function hideTooltip(e) {
        if (t1) t1.Hide(e);
        if (t2) t2.Hide(e);


    }

    function init() {
        t1 = new ToolTip("DetalleCCF", true, 30);
        t2 = new ToolTip("DetalleOBS", true, 30);
        //showDetails();
    }

    Event.observe(window, 'load', init, false);
        
    </script>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <%--<uc7:Cargando ID="Cargando1" runat="server" />--%>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
        <tr>
            <td class = "Cabecera" height="31px">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Operaciones - Pizarra de Otorgamiento"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" style="padding: 5px; height:590px" align="center">
                <%--*********************************************************************************************--%>
                <%--Criterio de Busqueda--%>
                <asp:UpdatePanel ID="UP_Criterio" runat="server">
                    <ContentTemplate>
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="left" class="Cabecera">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="middle">
                                        <table border="0" cellpadding="0" cellspacing="0" width="980px">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label12" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Número Identificación Proveedor"></asp:Label>
                                                    </td>
                                                    <td align="left"  width="170px">
                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" __designer:wfdid="w286" CssClass="clsTxt"
                                                             Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                        </cc2:MaskedEditExtender>
                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" __designer:wfdid="w286" CssClass="clsTxt"
                                                            MaxLength="1" Style="position: static" TabIndex="1" Width="15px" 
                                                            AutoPostBack="True"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                            runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                            TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                        </cc2:FilteredTextBoxExtender>
                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Style="margin-top: 0px" />
                                                        </a>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                             ReadOnly="True" Style="position: static" Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Ejecutivo"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DP_Ejecutivos" runat="server" clsTxt="clsMandatorio" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left" style="width: 130px">
                                                    </td>
                                                    <td align="left" valign="middle">
                                                        <asp:Label ID="Label44" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Fecha Simulación"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label46" runat="server" CssClass="Label" Text="Tipo Documento"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DP_TipoDocumento" runat="server" CssClass="clsTxt" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left" style="width: 130px">
                                                    </td>
                                                    <td align="left">
                                                        <table id="tb_Fechas" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label48" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Desde"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Fec_Dsd" runat="server" __designer:wfdid="w286" CssClass="clsTxt"
                                                                        MaxLength="10" Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Dsd_MaskedEditExtender" runat="server" 
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Dsd">
                                                                    </cc2:MaskedEditExtender>
                                                                    <cc2:CalendarExtender ID="Txt_Fec_Dsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Dsd">
                                                                    </cc2:CalendarExtender>
                                                                    
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label45" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Hasta"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Fec_Hst" runat="server" __designer:wfdid="w286" CssClass="clsTxt"
                                                                        MaxLength="10" Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Hst_MaskedEditExtender" runat="server" 
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                        TargetControlID="Txt_Fec_Hst" Mask="99/99/9999" MaskType="Date">
                                                                    </cc2:MaskedEditExtender>
                                                                    <cc2:CalendarExtender ID="Txt_Fec_Hst_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Hst">
                                                                    </cc2:CalendarExtender>
                                                                   <%-- <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="Txt_Fec_Hst"
                                                                        ErrorMessage="Fecha Erronea" Font-Size="8pt" MaximumValue="31/12/3000" MinimumValue="01/01/1900"
                                                                        Type="Date"></asp:RangeValidator>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label47" runat="server" CssClass="Label" Text="Tipo Operación"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DP_TipoOperacion" runat="server" CssClass="clsTxt" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--*********************************************************************************************--%>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div style="width:100%; text-align:left">
                        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                            Height="460px" Width="100%" AutoPostBack="True">
                            <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Operaciones">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanelOpe" runat="server">
                                        <ContentTemplate>
                                            <table id="Table10" border="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table id="Table11" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Operaciones del Proveedor"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top" class="Contenido">
                                                                        <table id="Table12" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td align="left" valign="top">
                                                                                   
                                                                                    <asp:Panel ID="Panel_GV_Ope_Ope" runat="server" height="170px" ScrollBars="Horizontal">
                                                                                        <asp:GridView ID="GV_Ope_Ope" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell" ShowHeader="True">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("NroOpe") %>'
                                                                                                            AlternateText='<%# Eval("NroNeg") %>' OnClick="Img_Ver_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="NroNeg" HeaderText="N° Operación">
                                                                                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="rut" HeaderText="Número Identificación Proveedor">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="cliente" HeaderText="Nombre / Razón Social Proveedor">
                                                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="TipDoc" HeaderText="Tipo Docto.">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="FechaSim" HeaderText="F. Simulación">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="PorAnt" HeaderText="% Anticipo">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                                                    <ItemStyle HorizontalAlign="center" Width="70px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="MtoOpe" HeaderText="Monto">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Operación">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="170px" />
                                                                                                </asp:BoundField>
                                                                                                <%--<asp:BoundField DataField="NroNeg" HeaderText="N° Neg.">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                                </asp:BoundField>--%>
                                                                                            </Columns>
                                                                                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                                            <RowStyle CssClass="formatUltcell" />
                                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                    
                                                                                </td>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:ImageButton id="IB_Evaluacion" 
                                                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/ver_eva_in.gif';"
                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/ver_eva_out.gif';" 
                                                                                    imageurl="~/Imagenes/btn_workspace/ver_eva_out.gif"  
                                                                                    runat="server" ToolTip="ver evaluación" />
                                                                                    
                                                                                    <asp:ImageButton id="IB_Negociacion" onmouseover="this.src='../../../Imagenes/btn_workspace/ver_neg_in.gif';"
                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/ver_neg_out.gif';" 
                                                                                    imageurl="~/Imagenes/btn_workspace/ver_neg_out.gif"
                                                                                    runat="server"   ToolTip="ver negociación" />
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"  />
                                                                                                    <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="tres">
                                                            <tr valign="top">
                                                                <td align="left">
                                                                    <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="350px">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left" class="Cabecera">
                                                                                    <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" Text="Niveles de Riesgos"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="Contenido" valign="top">
                                                                                    <table id="Table6" border="0" cellpadding="0" cellspacing="0" width="350px">
                                                                                        <tr>
                                                                                            <td valign="top" align="left">
                                                                                                    <asp:Panel ID="Panel_GV_Clasificacion" runat="server" height="118px" width="100%" ScrollBars="none" >
                                                                                                    
                                                                                                        <asp:GridView ID="GV_Clasificacion" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                                                            Width="100%">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="Nro" HeaderText="Nro">
                                                                                                                    <ItemStyle Width="50px" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                                                                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" ToolTip='<%# Eval("ccf") %>'/>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                                    ItemStyle-Width="90px">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="Img_CCF" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ccf") %>' OnClick="Img_Ver1_Click"/>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                                                            <RowStyle CssClass="formatUltcell" />
                                                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                                    
                                                                                                    </asp:GridView>
                                                                                                    </asp:Panel>
                                                                                               
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        <td align="center">
                                                                                        <table>
                                                                                        <tr>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="IB_Prev_Clf" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                                                            <asp:ImageButton ID="IB_Next_Clf" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"/>
                                                                                        </td>
                                                                                        </tr>
                                                                                        </table>
                                                                                        </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td align="left">
                                                                    <table id="Table8" border="0" cellpadding="0" cellspacing="0" width="700">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left" class="Cabecera">
                                                                                    <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" Text="Firmas"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="Contenido" style="height: 130px" valign="top">
                                                                                    <table id="Table9" border="0" cellpadding="0" cellspacing="0" width="98%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="DIV2" align="left" style="overflow-y: scroll; display: block; background-image: url('file:///D:/Dimension/TITULO VALOR/Imagenes/degrade.jpg');
                                                                                                    overflow: auto; height: 150px; width: 100%;">
                                                                                                    <asp:Table ID="Table_Firmas" runat="server" BorderWidth="0px" CellPadding="3" CellSpacing="0">
                                                                                                    </asp:Table>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <img alt="" src="../../../imagenes/iconos/verde02.gif" />
                                                                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Aprobado"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <img alt="" src="../../../imagenes/iconos/Amarillo02.gif" />
                                                                                                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Pendiente"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <img alt="" src="../../../imagenes/iconos/Rojo02.gif" />
                                                                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Rechazo"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </cc2:TabPanel>
                          <%--<cc2:TabPanel ID="TabPanelConfirmacion" runat="server" HeaderText="Confirmación">
                                <ContentTemplate>
                                    <uc8:Documentacion ID="Documentacion1" runat="server" />
                                </ContentTemplate>
                            </cc2:TabPanel>--%>
                            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Detalle Ope.">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <table id="tb_Linea_Financiamiento1" cellpadding="0" cellspacing="1" width="950">
                                                <tr>
                                                    <td class="Cabecera">
                                                        <asp:Label ID="Label9" runat="server" Text="Cambio de Clasificación" CssClass="SubTitulos"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" style="padding: 5px">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text="Clasificaciones"
                                                                        CssClass="Label"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DP_Calificacion" runat="server" CssClass="clsMandatorio" Width="80px">
                                                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                                        <asp:ListItem>AA</asp:ListItem>
                                                                        <asp:ListItem>A</asp:ListItem>
                                                                        <asp:ListItem>B</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Btn_AplicarCla" runat="server" Text="Aplicar" CssClass="boton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <uc1:ResumenOperacion ID="ResumenOperacion1" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </cc2:TabPanel>
                            <cc2:TabPanel ID="TabPanelReq" runat="server" HeaderText="Requisitos">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <uc5:MRequisitos ID="MRequisitos1" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </cc2:TabPanel>
                            <cc2:TabPanel ID="TabPanelCon" runat="server" HeaderText="Condiciones">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <uc6:MCondiciones ID="MCondiciones1" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </cc2:TabPanel>
                            <cc2:TabPanel ID="TabPanelLinea" runat="server" HeaderText="Linea">
                                <ContentTemplate>
                                    <table id="Table15" border="0" width="95%">
                                        <tr>
                                            <td>
                                                <uc4:lineafinanciamiento id="LineaFinanciamiento1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc2:TabPanel>
                        </cc2:TabContainer>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--*********************************************************************************************--%>
            </td>
        </tr>
        <tr>
            <td align="right" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    
                        <asp:ImageButton ID="IB_Buscar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/boton_buscar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_buscar_out.gif';" ImageUrl="~/Imagenes/Botones/boton_buscar_out.gif"
                            AlternateText="Buscar Operaciones"></asp:ImageButton>
                            
                        <asp:ImageButton ID="IB_Otorgar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/btn_otorgar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/btn_otorgar_out.gif';" ImageUrl="~/Imagenes/Botones/btn_otorgar_out.gif" Enabled="false"
                            AlternateText="Otorgar Operación"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            AlternateText="Limpiar"></asp:ImageButton>
                    
                        <asp:HiddenField ID="HF_NroNeg" runat="server" />
                        <asp:HiddenField ID="HF_NroNNC" runat="server" />
                        <asp:HiddenField ID="HF_NroCCF" runat="server" />
                        <asp:HiddenField ID="HF_NroOpe" runat="server" />
                        <asp:HiddenField ID="HF_NroOpn" runat="server" />
                        <asp:HiddenField ID="HF_NroEva" runat="server" />
                        <asp:LinkButton ID="LB_BuscarCliente" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="IB_Otorgar" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>    
      
    <asp:LinkButton ID="LB_Otorgar" runat="server"></asp:LinkButton>
    <asp:HiddenField ID="HF_RutCli" runat="server" />
        
    <%--
    <asp:HiddenField ID="HF_PosNeg" runat="server" />
    <asp:HiddenField ID="HF_PosNNC" runat="server" />
    <asp:HiddenField ID="HF_NroCon" runat="server" />
    <asp:HiddenField ID="HF_Estado" runat="server" />
    --%>
    
    <asp:LinkButton ID="Lb_MJ" runat="server"></asp:LinkButton>
    
    <div id="DetalleCCF" style="font-family:Tahoma ; font-size:small;background-color:white;width: 384px; height: 199px;border: solid 1px gray; text-align: center;filter: alpha(Opacity=85);opacity:0.85;display:none">
    </div>
    
    <div id="DetalleOBS" style="font-family:Tahoma ; font-size:small;background-color:white;width: 300px; height: 100px;border: solid 1px gray; text-align: center;filter: alpha(Opacity=85);opacity:0.85;display:none">
    </div>
    
</asp:Content>

