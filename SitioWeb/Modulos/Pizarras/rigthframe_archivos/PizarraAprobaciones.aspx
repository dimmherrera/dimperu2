<%@ Page Title="Pizarra de Aprobaciones" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="PizarraAprobaciones.aspx.vb" Inherits="PizarraAprobaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="ResumenOperacion.ascx" tagname="ResumenOperacion" tagprefix="uc1" %>
<%@ Register src="DetalleOperacion.ascx" tagname="DetalleOperacion" tagprefix="uc2" %>
<%@ Register src="Preliminar.ascx" tagname="Preliminar" tagprefix="uc3" %>
<%@ Register src="LineaFinanciamiento.ascx" tagname="LineaFinanciamiento" tagprefix="uc4" %>
<%@ Register src="MRequisitos.ascx" tagname="MRequisitos" tagprefix="uc5" %>
<%@ Register src="MCondiciones.ascx" tagname="MCondiciones" tagprefix="uc6" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>
<%@ Register src="Documentacion.ascx" tagname="Documentacion" tagprefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script src="../FuncionesPrivadasJS/PizarraAprobaciones.js" type="text/javascript"></script>
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
        str += "<tr bgcolor='skyblue'><td><b>Descripción</b></td>";
        str += "<td><b>Desde</b></td>";
        str += "<td><b>Hasta</b></td>";
        //str += "<td><b>Quantity</b></td>";

        var fin = 0;
        var navegador = navigator.appName;

        if (navegador == "Microsoft Internet Explorer")
            fin = i - 1;
        else if (navigator.appName == "Netscape")
            fin = i;

        for (var c = 0; c < fin; c++) 
        {
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
        str += "<td><b>Observación</b></td>";
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
    
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_Botonera" >
        <ProgressTemplate>
            <uc7:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" >
        <ProgressTemplate>
            <uc7:Cargando ID="Cargando2" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
    
        <tr>
            <td class = "Cabecera" height="31px">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Control Dual - Pizarra de Aprobaciones"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="Contenido" style="padding: 5px;height:590px" align="center" valign="top">
            
               <%--*********************************************************************************************--%>
               <asp:UpdatePanel ID="UP_Criterio" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td valign="top">
                                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="420">
                                        <tbody>
                                            <tr>
                                                <td align="left" class="Cabecera">
                                                    <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height: 40px" valign="middle">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Ejecutivos Comerciales"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="DP_Ejecutivos" runat="server" CssClass="clsTxt" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:CheckBox ID="CB_TodasOpe" runat="server" AutoPostBack="True" 
                                                                        CssClass="Label" Text="Todos los ejecutivos" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>
                                    <table id="tb_cliente" border="0" cellpadding="0" cellspacing="0" width="650px">
                                        <tbody>
                                            <tr>
                                                <td align="left" class="Cabecera">
                                                    <asp:CheckBox ID="CB_Cliente" runat="server" CssClass="SubTitulos" AutoPostBack="true"
                                                        Text="Proveedor" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" style="height: 40px" valign="middle">
                                                
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right" width="180">
                                                                    <asp:Label ID="Label12" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Número Identificación Proveedor"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Rut_Cli" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                         ReadOnly="True" Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                    <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                        TargetControlID="Txt_Rut_Cli">
                                                                    </cc2:MaskedEditExtender>
                                                                    <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" AutoPostBack="true"
                                                                        MaxLength="1" ReadOnly="True" Style="position: static" TabIndex="1" Width="15px"></asp:TextBox>
                                                                    <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                    </cc2:FilteredTextBoxExtender>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Enabled="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Style="position: static" Width="300px"></asp:TextBox>
                                                                    </td>
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
                        
                        <asp:LinkButton ID="LB_Cliente" runat="server"></asp:LinkButton>
                           
                    </ContentTemplate>
                </asp:UpdatePanel>
               <%--*********************************************************************************************--%>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="width: 100%; text-align: left">
                            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="99%"
                                AutoPostBack="true">
                                <cc2:TabPanel ID="TabPanel0" runat="server" HeaderText="Operaciones">
                                    <HeaderTemplate>
                                        Operaciones
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table id="Contenedora1" border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" class="Cabecera">
                                                                    <asp:Label ID="Label20" runat="server" CssClass="SubTitulos" Text="Operaciones"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="Contenido" valign="top">
                                                                    <table id="Table4" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td valign="top" align="left">
                                                                                <asp:Panel ID="Panel_GV_OPE" runat="server" Width="100%" Height="205px" ScrollBars="Horizontal">
                                                                                    <asp:GridView ID="GV_OPE" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="NroNeg" HeaderText="N° Ope.">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="rut" HeaderText="Número Identificación Proveedor">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="cliente" HeaderText="Nombre / Razón Social Proveedor">
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="TipDoc" HeaderText="Tipo Doc.">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="FechaNeg" HeaderText="F. Negociación">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="PorAnt" HeaderText="% Ant.">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MtoOpe" HeaderText="Monto">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="spread" HeaderText="Spread">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="comision" HeaderText="Comisión">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="deuda" HeaderText="Deuda %">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="sobregiro" HeaderText="Sobregiro %">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="Selección">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("NroOpe") %>'
                                                                                                        OnClick="Img_Ver_Click" Style="height: 13px" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                        <RowStyle CssClass="formatUltcell" />
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
                                                                            <td align="right">
                                                                                <asp:ImageButton ID="IB_Evaluacion" onmouseover="this.src='../../../Imagenes/btn_workspace/ver_eva_in.gif';"
                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/ver_eva_out.gif';" ImageUrl="~/Imagenes/btn_workspace/ver_eva_out.gif"
                                                                                    runat="server" ToolTip="ver evaluación" />
                                                                                <asp:ImageButton ID="IB_Negociacion" onmouseover="this.src='../../../Imagenes/btn_workspace/ver_neg_in.gif';"
                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/ver_neg_out.gif';" ImageUrl="~/Imagenes/btn_workspace/ver_neg_out.gif"
                                                                                    runat="server" ToolTip="ver negociación" />
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
                                                    <table id="tres" cellpadding="2">
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
                                                                            <td align="left" valign="top" class="Contenido">
                                                                                <div id="DIV1" align="left" style="overflow-y: scroll; display: block; overflow: hidden;
                                                                                    height: 120px; width: 99%;">
                                                                                    <asp:GridView ID="GV_Clasificacion" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Nro" HeaderText="Nro">
                                                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Selección">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("Nro") %>'
                                                                                                        Style="height: 13px" OnClick="Img_Ver_Click1" />
                                                                                                    <asp:HiddenField ID="HF_CCF" runat="server" Value='<%# Eval("ccf") %>' />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                        <RowStyle CssClass="formatUltcell" />
                                                                                    </asp:GridView>
                                                                                </div>
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
                                                                            <td align="left" class="Contenido" valign="top">
                                                                                <div id="DIV2" align="left" style="overflow-y: scroll; display: block; overflow: auto;
                                                                                    height: 120px; width: 99%;">
                                                                                    <asp:Table ID="Table_Firmas" runat="server" BorderWidth="0px" CellPadding="3" CellSpacing="0">
                                                                                    </asp:Table>
                                                                                </div>
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
                                </cc2:TabPanel>
                               <%-- <cc2:TabPanel ID="TabPanelConfirmacion" runat="server" HeaderText="Confirmación">
                                    <ContentTemplate>
                                            <uc8:Documentacion ID="Documentacion1" runat="server" />
                                    </ContentTemplate>
                                </cc2:TabPanel>--%>
                                <cc2:TabPanel ID="TabPanelResumen" runat="server" HeaderText="Resumen Ope.">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <uc1:ResumenOperacion ID="ResumenOperacion1" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel ID="TabPanelReq" runat="server" HeaderText="Requisitos">
                                    <ContentTemplate>
                                        <uc5:MRequisitos ID="MRequisitos1" runat="server" />
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel ID="TabPanelCon" runat="server" HeaderText="Condiciones">
                                    <ContentTemplate>
                                        <uc6:MCondiciones ID="MCondiciones1" runat="server" />
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel ID="TabPanelDetOpe" runat="server" HeaderText="Detalle Ope.">
                                    <ContentTemplate>
                                        <table id="Table11" border="0" width="95%">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <uc2:DetalleOperacion ID="DetalleOperacion1" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel ID="TabPanelLinea" runat="server" HeaderText="Linea">
                                    <ContentTemplate>
                                        <table id="Table15" border="0" width="95%">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelLinea" runat="server">
                                                        <ContentTemplate>
                                                            <uc4:LineaFinanciamiento ID="LineaFinanciamiento1" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                            </cc2:TabContainer>
                        </div>
                        <asp:HiddenField ID="HF_NroOpe" runat="server" />
                        <asp:HiddenField ID="HF_NroEva" runat="server" />
                        <asp:HiddenField ID="HF_NroNeg" runat="server" />
                        <asp:HiddenField ID="HF_NroNNC" runat="server" />
                        <%--Clasificacion--%>
                        <asp:HiddenField ID="HF_NroCCF" runat="server" />
                        <%--Firma--%>
                    </ContentTemplate>
                    <%--
                    <Triggers>
                        <asp:PostBackTrigger ControlID="IB_Evaluacion" />
                        <asp:PostBackTrigger ControlID="IB_Negociacion" />
                    </Triggers>
                        --%>
                </asp:UpdatePanel>
                              
            </td>
        </tr>
        
        <tr>
            <td align="right">
                <asp:UpdatePanel ID="UP_Botonera" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    
                   
                    <asp:LinkButton ID="LB_RefrescaFirmas" runat="server"></asp:LinkButton>
                    <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>   
                     
                     <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="IB_Buscar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" AlternateText="Buscar Operaciones">
                        </asp:ImageButton>
                        <asp:ImageButton Style="position: static" ID="IB_Aprobar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" OnClick="IB_Aprobar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" __designer:wfdid="w375"
                            AlternateText="Dar Visto Bueno a Operación" ValidationGroup="Cliente" Enabled="False">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Rechazar" onmouseover="this.src='../../../Imagenes/Botones/boton_rechazar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_rechazar_out.gif';" OnClick="IB_Rechazar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/boton_rechazar_out.gif" __designer:wfdid="w378"
                            AlternateText="Rechazar Operación" Enabled="False"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Detalle" onmouseover="this.src='../../../Imagenes/Botones/boton_detalle_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_out.gif';" OnClick="IB_Detalle_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/boton_detalle_out.gif" __designer:wfdid="w376"
                            AlternateText="Ver Detalles" Visible="False"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" __designer:wfdid="w378"
                            AlternateText="Limpiar Pantalla"></asp:ImageButton>
                            
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="HF_NroEva" />
                        <asp:PostBackTrigger ControlID="IB_Limpiar" />
                        <asp:PostBackTrigger ControlID="IB_Buscar" />
                    </Triggers>
                </asp:UpdatePanel>
                 
            </td>
        </tr>
     
    </table>
    
    
    
    
    <asp:HiddenField ID="HF_Estado" runat="server" />
    
   
    <div id="DetalleCCF" style="font-family: Tahoma; font-size: small; background-color: white;
        width: 384px; height: 135px; border: solid 1px gray; text-align: center; filter: alpha(Opacity=85); display:none; 
        opacity: 0.85">
    </div>
    
    <div id="DetalleOBS" style="font-family: Tahoma; font-size: small; background-color: white;
        width: 350px; height: 100px; border: solid 1px gray; text-align: center; filter: alpha(Opacity=85); display:none;
        opacity: 0.85">
    </div>
     
 
</asp:Content>

