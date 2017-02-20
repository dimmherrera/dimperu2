<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionCobranza.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_GestionCobranza" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Documentos</title>
    <base target="_self" />
    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>
    <script src="../FuncionesProvadasJS/GestionCobranza.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Tooltip.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Prototype.js" type="text/javascript"></script>
    
    <script language="javascript">

        var t1;

        function showToolTip(e, custId) {
            var url = 'ObsVerificacion.aspx';

            var qstr = 'id=' + custId + "&ms=" + new Date().getTime();

            var req = new Ajax.Request(
			url,
            {
                method: 'get',
                parameters: qstr,
                onComplete: showTooltipVeri
            });

            if (t1) t1.Show(e, "<br><br>Cargando...");
        }

        function showTooltipVeri(res) {
            var t = res.responseText;
            //debugger;
            var x = eval('(' + t + ')');
            var i = x.Verificacion.Items.length;
            var str = "<table width=100% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
            str += "<tr bgcolor='skyblue'>"
            str += "<td><b>Estado Verificación</b></td>";
            str += "<td><b>Observación</b></td>";
            
            for (var c = 0; c < i - 1; c++) {
                str += "<tr>";
                str += "<td align='right'>" + x.Verificacion.Items[c].Estado + "</td>";
                str += "<td align='right'>" + x.Verificacion.Items[c].Observacion + "</td>";
                str += "</tr>";
            }

            str += "</tbody></table>";
            t1.SetHTML(str);
        }

    
        function hideTooltip(e) {
            if (t1) t1.Hide(e);
        }

        function init() {
            t1 = new ToolTip("ToolTipVeri", true, 30);
        }

        Event.observe(window, 'load', init, false);
        
    </script>
    
    <style type="text/css">
        .style1
        {
            width: 183px;
        }
    </style>
    
</head>
<body>

    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        LoadScriptsBeforeUI="false" EnablePartialRendering="true">
    </asp:ScriptManager>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_DEUCLI" >
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_Gestion" >
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando2" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UP_Botonera" >
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando3" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <table border="0" cellpadding="2" cellspacing="2">
        <tr>
            <%--Deudor y Contactos--%>
            <td valign="top" align="center">
                <asp:UpdatePanel ID="UP_DEUCLI" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="1300px">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label6" runat="server" Text="Documentos a Gestionar" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center" style="padding: 10px">
                                
                                    <table width="100%">
                                        <tr>
                                            <td align="left" valign="top">
                                                <!-- TABLA CRITERIOS -->
                                                <table border="0" cellpadding="0" cellspacing="0" width="400px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label7" runat="server" Text="Seleccione Criterios de Busqueda" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" align="center">
                                                            <table>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label39" runat="server" Text="Estado Docto" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dp_EstadoDoctos" runat="server" Width="210px" CssClass="clsTxt">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Ordenar" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Dp_Orden_GV_DEUCLI" runat="server" AutoPostBack="True" 
                                                                            CssClass="clsTxt" Width="210">
                                                                            <asp:ListItem Value="1">Por NIT</asp:ListItem>
                                                                            <asp:ListItem Value="2">Por Nombre</asp:ListItem>
                                                                            <asp:ListItem Value="3">Por Pago No Liberado</asp:ListItem>
                                                                            <asp:ListItem Value="4">Por Solo Hoy</asp:ListItem>
                                                                            <asp:ListItem Value="5">Por Fecha Seguimiento</asp:ListItem>
                                                                            <asp:ListItem Value="6">Por Monto</asp:ListItem>
                                                                            <asp:ListItem Value="7">Por Cob. Anticipada</asp:ListItem>
                                                                            <asp:ListItem Value="8">Por No Recaudado</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label37" runat="server" Text="Códigos de Cobranza" 
                                                                            CssClass="Label" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Cuadrado">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label38" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_CCO_DSD" runat="server" CssClass="clsTxt" Width="90px" 
                                                                                        AutoPostBack="True">
                                                                                    </asp:DropDownList>
                                                                                    <cc1:ListSearchExtender ID="ListSearchExtender3" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                                                        PromptPosition="top" PromptText="Codigo" QueryPattern="Contains"
                                                                                        TargetControlID="DP_CCO_DSD">
                                                                                    </cc1:ListSearchExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_CCO_DSD" runat="server" Width="240px" 
                                                                                        CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_CCO_HTA" runat="server" CssClass="clsTxt" Width="90px" 
                                                                                        AutoPostBack="True">
                                                                                    </asp:DropDownList>
                                                                                    <cc1:ListSearchExtender ID="ListSearchExtender4" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                                                        PromptPosition="top" PromptText="Codigo" QueryPattern="Contains"
                                                                                        TargetControlID="DP_CCO_HTA">
                                                                                    </cc1:ListSearchExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_CCO_HTA" runat="server" Width="240px" 
                                                                                        CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- FIN TABLA CRITERIOS -->
                                            </td>
                                            <td valign="top">
                                                <%--Deudores y Contactos--%>
                                                <asp:Panel ID="Panel_GV_Ope_Ope" runat="server" height="180px" ScrollBars="Horizontal">
                                                <%--<div id="HeaderDiv" style="overflow-y: scroll; display: block; overflow: hidden; height: 180px">--%>
                                                     <asp:GridView ID="GV_DEUCLI" runat="server" CssClass="grid" AlternatingRowStyle-CssClass="altrow"
                                                        DataKeyNames="NombreDeudor" ShowHeader="false" AllowPaging="True" AutoGenerateColumns="False"
                                                        PageSize="5" Width="600" Caption="Pagadores">
                                                        <PagerStyle CssClass="pagerstyle" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Panel ID="POrderHeader" runat="server" HorizontalAlign="Left">
                                                                        <asp:Image ImageUrl="~/Imagenes/Iconos/expand.jpg" runat="server" ID="imgplus" />
                                                                        <asp:Label ID="Deudor" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="Rut_Deudor" runat="server" Text='<%#Eval("Rut")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="Nombre_Deudor" runat="server" Text='<%#Eval("NombreDeudor")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="Cantidad" runat="server" Text='<%#Eval("Cantidad")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="Sumatoria" runat="server" Text='<%#Eval("MontoDocto")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="Gestion" runat="server" Text='<%#Eval("Gestion")%>' Visible="false"></asp:Label>
                                                                    </asp:Panel>
                                                                    <asp:Panel Style="margin-left: 10px; margin-right: 10px" ID="OrdersDetailPanel" runat="server">
                                                                        <asp:GridView AutoGenerateColumns="false" CssClass="grid" ID="GV_Contactos" runat="server"
                                                                            ShowHeader="true" EnableViewState="false" Caption="Contactos">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="id_cnc" HeaderText="Id">
                                                                                    <ItemStyle Width="20px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cnc_nom" HeaderText="Nombre">
                                                                                    <ItemStyle Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cnc_car" HeaderText="Cargo">
                                                                                    <ItemStyle Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cnc_tel" HeaderText="Teléfono">
                                                                                    <ItemStyle Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cnc_fax" HeaderText="Fax">
                                                                                    <ItemStyle Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cnc_ema" HeaderText="e-Mail">
                                                                                    <ItemStyle Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                    ItemStyle-Width="90px">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/images/bt_sel.gif" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                    <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="OrdersDetailPanel"
                                                                        CollapsedSize="0" Collapsed="True" ExpandControlID="POrderHeader" CollapseControlID="POrderHeader"
                                                                        AutoCollapse="false" AutoExpand="False" ScrollContents="false" ImageControlID="imgplus"
                                                                        ExpandedImage="~/Imagenes/Iconos/collapse.jpg" CollapsedImage="~/Imagenes/Iconos/expand.jpg"
                                                                        ExpandDirection="Vertical" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="altrowstyle"></AlternatingRowStyle>
                                                    </asp:GridView>
                                                <%--</div>--%></asp:Panel>
                                            </td>
                                            <td>
                                                <%--Observacion, Contacto y Dias de Pago--%>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="IB_Contactos" runat="server" ImageUrl="~/Imagenes/btn_workspace/Contactos_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/Contactos_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Contactos_in.gif';"
                                                                ToolTip="Contactos" Width="50" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="IB_Dias_Pago" runat="server" ImageUrl="~/Imagenes/btn_workspace/DiaPago_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/DiaPago_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/DiaPago_in.gif';"
                                                                ToolTip="Días de Pago" Width="50" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="IB_Obs_Deudor" runat="server" ImageUrl="~/Imagenes/btn_workspace/ObsDeu_Out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/ObsDeu_Out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/ObsDeu_in.gif';"
                                                                ToolTip="Observación del Pagador" Width="50" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                                                        
                                    
                                    
                                    
                                   
                                    
                                    <%--Clientes y Documentos--%>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="IB_Seleccionar" runat="server" ToolTip="Seleccionar todos los doctos." ImageUrl="~/Imagenes/Iconos/check.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" >
                                                <asp:Label ID="Label4" runat="server" Text="Seleccionar todos los documentos del Pagador"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <asp:Panel ID="Panel1" runat="server" height="226px" ScrollBars="Horizontal">
                                       <asp:GridView ID="GV_Clientes" runat="server" CssClass="grid" AlternatingRowStyle-CssClass="altrow"
                                            DataKeyNames="RazonSocial" ShowHeader="false" AllowPaging="True" AutoGenerateColumns="False"
                                            PageSize="5" Caption="Clientes">
                                            <PagerStyle CssClass="pagerstyle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Panel ID="ClientesHeader" runat="server">
                                                            <asp:Image ImageUrl="~/Imagenes/Iconos/expand.jpg" runat="server" ID="imgplus" />
                                                            <asp:Label ID="Cliente" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="Rut_Cliente" runat="server" Text='<%#Eval("Rut")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Nombre_Cliente" runat="server" Text='<%#Eval("RazonSocial")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Cantidad" runat="server" Text='<%#Eval("Cant")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Sumatoria" runat="server" Text='<%#Eval("Suma")%>' Visible="false"></asp:Label>
                                                        <%--<asp:Label ID="Gestion" runat="server" Text='<%#Eval("Gestion")%>' Visible="false"></asp:Label>--%></asp:Panel>
                                                        <asp:Panel Style="margin-left: 10px; margin-right: 10px" ID="DoctosPanel" runat="server">
                                                            <asp:GridView AutoGenerateColumns="False" CssClass="grid" ID="GV_Doctos" runat="server"
                                                                EnableViewState="False" Caption="Documentos">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CHB_SelDocto" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="pnu_des" HeaderText="Tipo Docto" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="dor_num_doc" HeaderText="Nro. Docto." HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="doc_flj_num" HeaderText="Cuota" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="doc_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto."
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="doc_fev_ori" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Vcto. Orig."
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cco_num" HeaderText="Estado Cob." HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="doc_sdo_ddr" DataFormatString="{0:###,###,###}" HeaderText="Saldo Pagador"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="doc_sdo_cli" DataFormatString="{0:###,###,###}" HeaderText="Saldo Cliente"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" DataFormatString="{0:###,###,###}" HeaderText="Monto Dcto."
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Estado Gestion" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Image1" runat="server" Height="20px" Width="20px" />
                                                                            <asp:Image ID="Image2" runat="server" Height="20px" Width="20px" />
                                                                            <asp:Image ID="Image3" runat="server" Height="20px" Width="20px" />
                                                                            <asp:Image ID="Image4" runat="server" Height="20px" Width="20px" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="130px" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="id_opo" runat="server" Text='<%#Eval("id_opo")%>'></asp:Label>
                                                                            <asp:Label ID="id_doc" runat="server" Text='<%#Eval("id_doc")%>'></asp:Label>
                                                                            <asp:Label ID="TipDoc" runat="server" Text='<%#Eval("id_p_0011")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Estado" HeaderText="Estado Dcto." HeaderStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="100px" HorizontalAlign="center" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Verificacón" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Img_Veri" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="50px" HorizontalAlign="center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gestiones">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Img_GestionesAnt" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Docto." HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Img_Docto" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="50px" HorizontalAlign="center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Carta" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Img_Carta" runat="server" ImageUrl="~/Imagenes/Iconos/lupa.gif" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="50px" HorizontalAlign="center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="cpc" runat="Server" TargetControlID="DoctosPanel"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="ClientesHeader" CollapseControlID="ClientesHeader"
                                                            AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgplus"
                                                            ExpandedImage="~/Imagenes/Iconos/collapse.jpg" CollapsedImage="~/Imagenes/Iconos/expand.jpg"
                                                            ExpandDirection="Vertical" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="altrowstyle"></AlternatingRowStyle>
                                        </asp:GridView>
                                    </asp:Panel>
                                    
                                    <%--Leyenda de estados de cobranza para grilla documentos--%>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Iconos/desactivada.gif"
                                                    ToolTip="Sin Gestionar" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Imagenes/Iconos/verde.gif" ToolTip="Gestión Pendiente" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Iconos/violeta.gif" ToolTip="Doctos. Pendientes" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Iconos/rojo.gif" ToolTip="Pago en Linea" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Iconos/amarillo.gif" ToolTip="Nunca Recaudado" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label74" runat="server" CssClass="Label" Text="Sin Gestionar"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label78" runat="server" CssClass="Label" Text="Gestión Pendiente"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label79" runat="server" CssClass="Label" Text="Doctos.Pendientes"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label80" runat="server" CssClass="Label" Text="Pago en Linea"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label81" runat="server" CssClass="Label" Text="Nunca Recaudado"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                        </table>
                        <asp:LinkButton ID="Busqueda_GV_Contactos" runat="server" CausesValidation="False"></asp:LinkButton>
                        <asp:LinkButton ID="Busqueda_GV_Doctos" runat="server" CausesValidation="False"></asp:LinkButton>
                        <asp:LinkButton ID="LB_Refrescar" runat="server" CausesValidation="False"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UP_Gestion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Datos de Gestión"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td class="Contenido">
                                    <table>
                                        
                                        
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Fec.Prox.Gestión"></asp:Label>
                                            </td>
                                            <td class="style1">
                                                
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txt_FechaProxGestion" runat="server" CssClass="clsMandatorio" MaxLength="10"
                                                                Width="90px"></asp:TextBox>
                                                                
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                  AcceptNegative="Left" Enabled="False" Mask="99/99/9999"
                                                                  TargetControlID="txt_FechaProxGestion" UserDateFormat="DayMonthYear" MaskType="Date">
                                                            </cc1:MaskedEditExtender>    
                                                            
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar"
                                                                Enabled="True" FirstDayOfWeek="Monday" TargetControlID="txt_FechaProxGestion">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Hora"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_HoraProxGestion" runat="server" CssClass="clsMandatorio" 
                                                                MaxLength="4" Width="50px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_HoraProxGestion_MaskedEditExtender" 
                                                                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                Mask="99:99" MaskType="Time" TargetControlID="txt_HoraProxGestion">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Suc.Cobranza"></asp:Label>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="DP_SucCobranza2" runat="server" CssClass="clsMandatorio" Width="200px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender7" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                    PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains"
                                                    TargetControlID="DP_SucCobranza2">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td colspan="1">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                                </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label31" runat="server" CssClass="Label" Text="Suc.Recaudación"></asp:Label>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="DP_SucRecaudacion" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" Width="200px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender6" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                    PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains"
                                                    TargetControlID="DP_SucRecaudacion">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td colspan="1">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label50" runat="server" CssClass="Label" Text="Est.Cobranza"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DP_CodCobranza" runat="server" CssClass="clsMandatorio" Width="350px" />
                                                <cc1:ListSearchExtender ID="LSE_Dp_CodCodbranza" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                    PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains"
                                                    TargetControlID="DP_CodCobranza">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td align="right" rowspan="2" valign="top">
                                                <asp:Label ID="Label55" runat="server" CssClass="Label" 
                                                    Text="Doc.Necesaria Ret.Pago"></asp:Label>
                                            </td>
                                            <td rowspan="2">
                                                <asp:TextBox ID="txt_DoctoNecGestion" runat="server" CssClass="clsTxt" 
                                                    Height="50px" Width="350px"></asp:TextBox>
                                                </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Fecha Pago"></asp:Label>
                                            </td>
                                            <td colspan=2>
                                                
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txt_FechaPago" runat="server" AutoPostBack="True" 
                                                                CssClass="clsTxt" Width="90px"></asp:TextBox>
                                                             <cc1:MaskedEditExtender ID="txt_FechaPago_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="false" 
                                                                Mask="99/99/9999" TargetControlID="txt_FechaPago" UserDateFormat="DayMonthYear" MaskType="Date">
                                                            </cc1:MaskedEditExtender>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                TargetControlID="txt_FechaPago">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Hora Pago Desde"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_HoraPagoDde" runat="server" CssClass="clsTxt" 
                                                                MaxLength="4" Width="50px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_HoraPagoDde_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="false" 
                                                                Mask="99:99" MaskType="Time" TargetControlID="Txt_HoraPagoDde">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Hora Pago Hasta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_HoraPagoHta" runat="server" CssClass="clsTxt" 
                                                            MaxLength="4" Width="50px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_HoraPagoHta_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="false" 
                                                                Mask="99:99" MaskType="Time" TargetControlID="Txt_HoraPagoHta">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="right" rowspan="3">
                                                
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label83" runat="server" CssClass="Label" Text="Departamento"></asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="DP_Depto" runat="server" AutoPostBack="True" 
                                                                CssClass="clsTxt" Width="150px">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender ID="ListSearchExtender5" runat="server" IsSorted="true" 
                                                                PromptCssClass="LabelDrop" PromptPosition="Bottom" 
                                                                PromptText="Escriba Para Buscar" QueryPattern="Contains" 
                                                                TargetControlID="DP_Depto">
                                                            </cc1:ListSearchExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Municipio"></asp:Label>
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:DropDownList ID="DP_Ciudad" runat="server" AutoPostBack="True" 
                                                                CssClass="clsTxt" Width="150px">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" IsSorted="true" 
                                                                PromptCssClass="LabelDrop" PromptPosition="Bottom" 
                                                                PromptText="Escriba Para Buscar" QueryPattern="Contains" 
                                                                TargetControlID="DP_Ciudad">
                                                            </cc1:ListSearchExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="right" rowspan="3" valign="top">
                                                <asp:Label ID="Label56" runat="server" CssClass="Label" Text="Observación"></asp:Label>
                                            </td>
                                            <td rowspan="3" valign="top">
                                                <asp:TextBox ID="txt_ObservacionGestion" runat="server" CssClass="clsTxt" 
                                                    Height="50px" Width="350px"></asp:TextBox>
                                                </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label82" runat="server" CssClass="Label" Text="Localidad/Barrio"></asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="DP_Comuna" runat="server" AutoPostBack="True" 
                                                                CssClass="clsTxt" Width="150px">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" IsSorted="true" 
                                                                PromptCssClass="LabelDrop" PromptPosition="Bottom" 
                                                                PromptText="Escriba Para Buscar" QueryPattern="Contains" 
                                                                TargetControlID="DP_Comuna">
                                                            </cc1:ListSearchExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label53" runat="server" CssClass="Label" Text="Zona"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GESIdZona" runat="server" CssClass="clsDisabled" 
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GESZona" runat="server" CssClass="clsDisabled" 
                                                                Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label54" runat="server" CssClass="Label" Text="Dirección"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="Txt_direccion" runat="server" autocomplete="off" CssClass="clsTxt"  
                                                    Width="350px" />
                                                <asp:Panel ID="PanelDireccion" runat="server" CssClass="popupControl">
                                                    <div style="border: 1px outset white; width: 480px; overflow: auto; height:100px">
                                                        <asp:RadioButtonList ID="RB_Direcciones" runat="server" AutoPostBack="true" CssClass="Label">
                                                        
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </asp:Panel>
                                                <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" CommitProperty="value"
                                                    CommitScript="e.value" PopupControlID="PanelDireccion" Position="Bottom" TargetControlID="Txt_direccion" />
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                &nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                            <td align="right" valign="top">
                                                &nbsp;</td>
                                            <td colspan="4">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label57" runat="server" CssClass="Label" Text="A la Orden"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:RadioButton ID="RB_Banco" runat="server" CssClass="Label" GroupName="a_la_orden"
                                                    Text="Banco" />
                                                <asp:CheckBox ID="ChB_GestionPendiente" runat="server" CssClass="Label" Text="Gestión Pendiente" />
                                                <asp:CheckBox ID="CBX_ARecaudar" runat="server" CssClass="Label" 
                                                    Text="A Recaudar" />
                                                <asp:CheckBox ID="CBX_ConfirmarHorario" runat="server" CssClass="Label" 
                                                    Text="Conf. Horario" />
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <!-- BOTONES-->
                <asp:UpdatePanel ID="UP_Botonera" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="IB_BuscarCriterios" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" 
                                     onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"/>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_GuardaGestion" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" runat="server" OnClick="IB_GuardaGestion_Click"
                                        ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" AlternateText="Guardar Datos"
                                        ValidationGroup="VG2" Enabled="False"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_CancelarGestion" onmouseover="this.src='../../../Imagenes/Botones/Boton_limpiar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_limpiar_Out.gif" AlternateText="Limpiar Selección">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_VolverGestion" onmouseover="this.src='../../../Imagenes/Botones/Boton_volver_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_volver_Out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_volver_Out.gif" AlternateText="Volver"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                        <uc2:Mensaje ID="Mensaje1" runat="server" />
                    </ContentTemplate>
                   
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="ID_GV_DEUDOR" runat="server" />
    <asp:HiddenField ID="ID_GV_CLIENTE" runat="server" />
    <asp:HiddenField ID="ID_GV_Contactos" runat="server" />
    <asp:HiddenField ID="ID_Contacto" runat="server" />
    <asp:HiddenField ID="ID_GV_Doctos" runat="server" />
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
    <div id="ToolTipVeri" style="font-family: Tahoma; font-size: small; background-color: white;
        width: 400px; height: 150px; border: solid 1px gray; text-align: center; filter: alpha(Opacity=85);
        opacity: 0.85">
    </div>
    
    </form>
    
</body>
</html>
