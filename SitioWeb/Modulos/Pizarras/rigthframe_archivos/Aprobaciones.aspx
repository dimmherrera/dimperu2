<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Aprobaciones.aspx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_Aprobaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <base target=_self />
    <title>Aprobaciones</title>
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Prototype.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Tooltip.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/PizarraEjecutivos.js" type="text/javascript"></script>
    
</head>

<body>

    <form id="form1" runat="server">

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
            str += "</tr>";
        }

        str += "</tbody></table>";
        //alert(str);
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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
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
                                        height: 150px; width: 99%;">
                                        <asp:GridView ID="GV_Clasificacion" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                            <Columns>
                                                <asp:BoundField DataField="Nro" HeaderText="Nro">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("Nro") %>' Style="height: 13px" onclick="Img_Ver_Click" />
                                                        <asp:HiddenField ID="HF_CCF" runat="server" Value='<%# Eval("ccf") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle  CssClass="cabeceraGrilla" Height="30px" />
                                                <RowStyle CssClass="formatUltcell" />
                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td align="left">
                    <table id="Table8" border="0" cellpadding="0" cellspacing="0" width="600">
                        <tbody>
                            <tr>
                                <td align="left" class="Cabecera">
                                    <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" Text="Firmas"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="Contenido" valign="top">
                                    <div id="DIV2" align="left" style="overflow-y: scroll; display: block; overflow: auto;
                                        height: 150px; width: 99%;">
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
        
        <asp:HiddenField ID="HF_NroNeg" runat="server" /> <%--Negociación--%>
        <asp:HiddenField ID="HF_NroNNC" runat="server" /> <%--Clasificacion--%>
        <asp:HiddenField ID="HF_NroCCF" runat="server" /> <%--Firma--%>
                                                
    <%--
    
    <asp:HiddenField ID="HF_PosNeg" runat="server" />
    <asp:HiddenField ID="HF_NroNNC" runat="server" />
    <asp:HiddenField ID="HF_PosNNC" runat="server" />
    <asp:HiddenField ID="HF_NroCCF" runat="server" />
    <asp:HiddenField ID="HF_Estado" runat="server" />
    <asp:HiddenField ID="HF_NroOpe" runat="server" />
    --%>
    
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
    
    <div id="DetalleCCF" 
        style="font-family:Tahoma; font-size:small;background-color:white;width: 384px; height: 135px;border: solid 1px gray; text-align: center;filter: alpha(Opacity=85);opacity:0.85;display:none; ">
    </div>
    
    <div id="DetalleOBS" style="font-family:Tahoma ; font-size:small;background-color:white;width: 350px; height: 100px;border: solid 1px gray; text-align: center;filter: alpha(Opacity=85);opacity:0.85;display:none;">
    </div>
    
    
    </form>
</body>
</html>
