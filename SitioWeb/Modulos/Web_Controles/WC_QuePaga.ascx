<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WC_QuePaga.ascx.vb" Inherits="WC_QuePaga" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<%@ Register Src="Mensaje.ascx" TagName="Mensaje" TagPrefix="uc2" %>

<link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

<script language="javascript" src="../../FuncionesJS/Ventanas.js"></script>
<script src="../../FuncionesJS/Grilla.js" type="text/javascript"></script>
<script src="../../FuncionesJS/Funciones.js" type="text/javascript"></script>
<script src="../Ayudas/FuncionesPrivadasJS/AyudaDeudor.js" type="text/javascript"></script>
<script src="../../FuncionesJS/Ajax.js" type="text/javascript"></script>
<script language="javascript">

    function SelecionaDocto(Posicion) {
        window.document.forms[0].WC_QuePaga1_HF_Posicion.value = Posicion;
        return;
    }

    function ClickDocto(pTabla, pClass, jClass, sClass, Posicion) {
        window.document.forms[0].WC_QuePaga1_HF_Posicion.value = Posicion;
      return;
    }

    function SelecionaCxc(Posicion) {
        window.document.forms[0].WC_QuePaga1_HF_Posicion_CxC.value = Posicion;
        return;
    }


    function CB_CriterioDeudor() {

        var cb = window.document.forms[0].WC_QuePaga1_CB_Deudor.checked;

        if (cb) {
            window.document.forms[0].WC_QuePaga1_Txt_Rut_Deu.className = 'clsMandatorio';
            window.document.forms[0].WC_QuePaga1_Txt_Dig_Deu.className = 'clsMandatorio';
            window.document.forms[0].WC_QuePaga1_Txt_Rut_Deu.readOnly = false;
            window.document.forms[0].WC_QuePaga1_Txt_Dig_Deu.readOnly = false;

        }
        else {
            window.document.forms[0].WC_QuePaga1_Txt_Rut_Deu.className = 'clsDisabled';
            window.document.forms[0].WC_QuePaga1_Txt_Dig_Deu.className = 'clsDisabled';
            window.document.forms[0].WC_QuePaga1_Txt_Rut_Deu.readOnly = true;
            window.document.forms[0].WC_QuePaga1_Txt_Dig_Deu.readOnly = true;
        }

        return;

    }

    function DoScroll() {
        var _gridView = document.getElementById("GridViewDiv");
        var _header = document.getElementById("HeaderDiv");
        _header.scrollLeft = _gridView.scrollLeft;
    }

    function DoScroll2() {
        var _gridView = document.getElementById("GridViewDiv2");
        var _header = document.getElementById("HeaderDiv2");
        _header.scrollLeft = _gridView.scrollLeft;
    }


    
</script>

<script type="text/javascript" language="javascript">

    function AbrirPopup() {
        //Abrir Ventana
        popup = window.open('', 'winReport', 'width=500,height=500,left=50,top=100,­menubar=0,toolbar=0,status=0,scrollbars=1,resizable=0,titlebar=0');


        //Armar documento        
        popup.document.write('<html><body><table>');
        popup.document.write(document.all.contenedorGrilla.innerHTML);
        popup.document.write('</table></body></html>');
        popup.print();

        var abierto = true;

        if (abierto) {
            abierto = false;
            timer = setInterval(cerraExp, 10000); //7000=7sg
        }
    }

    function cerraExp() {
        popup.close();
    }
</script>


<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div id="progressBackgroundFilter" style="position: absolute; top: 0px; bottom: 0px;
            left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000;
            filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
        </div>
        <cc2:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="processMessage"
            Radius="10" Enabled="True">
        </cc2:RoundedCornersExtender>
        <asp:Panel ID="processMessage" runat="server" Style="z-index: 1003; position: absolute;
            top: 30%; left: 30%; height: 150px; width: 300px; background-color: WhiteSmoke">
            <table>
                <tr>
                    <td align="center">
                        <img src="../../Imagenes/Iconos/DIMENSION_LOGO_LENTO.gif" width="85" />
                        <img src="../../Imagenes/dimension.png" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <img src="../../Imagenes/Iconos/procesando.gif" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ProgressTemplate>
</asp:UpdateProgress>

<%--<asp:UpdatePanel ID="UP_Principal" runat="server">
    <ContentTemplate>
--%>        

<table id="tb_principal" border="0" cellpadding="0" cellspacing="0" width="95%">
            <tr>
                <td>
                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Height="530px"
                        Width="100%" AutoPostBack="false">
                        <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Ctas. X Cobrar">
                            <HeaderTemplate>
                                Ctas. X Cobrar
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UP_CxC" runat="server">
                                    <ContentTemplate>
                                        <%--Grilla CxC--%>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div id="Div3" style="overflow: scroll; width: 1200px; height: 470px">
                                                        <asp:GridView ID="GV_CxC" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                            AllowSorting="True" AllowPaging="false" CellPadding="0" PageSize="15" Width="1200px">
                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderImageUrl="~/Imagenes/Iconos/check.gif" SortExpression="Todos">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CB_Seleccionar_CxC" runat="server" AutoPostBack="True" OnCheckedChanged="CB_Seleccionar_CxC_CheckedChanged" ToolTip= /></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="30" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_doc" HeaderText="N° Factura">
                                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cta.">
                                                                    <ItemStyle HorizontalAlign="Left" Width="250" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="id_cxc" HeaderText="N° Cta.">
                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Descrip_Cta" HeaderText="Descripción">
                                                                    <ItemStyle HorizontalAlign="LEFT" Width="250" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_int" HeaderText="Interes" NullDisplayText="0">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Mto. A Pagar" ItemStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="Txt_MtoPagar_CxC" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                            Width="100px" AutoPostBack="True" OnTextChanged="Txt_MtoPagar_CxC_TextChanged">
                                                                        </asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="Txt_MtoPagar_CxC_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_MtoPagar_CxC"
                                                                            ValidChars=",.">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cxc_fec" HeaderText="Fecha.Emisión" DataFormatString="{0:dd/MM/yyyy}"
                                                                    NullDisplayText="01/01/1900">
                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_ful_pgo" HeaderText="Fec.Ult.Pag." DataFormatString="{0:dd/MM/yyyy}"
                                                                    NullDisplayText="01/01/1900">
                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Contrato" HeaderText="Contrato">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Lb_NroPaginaCxC" runat="server" CssClass="Label" Text=""></asp:Label><asp:Label
                                                        ID="Label16" runat="server" CssClass="Label" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="Center">
                                                    <asp:ImageButton ID="IB_Prev_CxC" runat="server" ImageUrl="~/Imagenes/btn_workspace/flecha_izq_out.gif"
                                                        onmouseover="this.src='../../Imagenes/btn_workspace/flecha_izq_in.gif';" onmouseout="this.src='../../Imagenes/btn_workspace/flecha_izq_out.gif';" />
                                                    <asp:ImageButton ID="IB_Next_CxC" runat="server" ImageUrl="~/Imagenes/btn_workspace/flecha_der_out.gif"
                                                        onmouseover="this.src='../../Imagenes/btn_workspace/flecha_der_in.gif';" onmouseout="this.src='../../Imagenes/btn_workspace/flecha_der_out.gif';" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table width="150" cellpadding="0" cellspacing="0" border="1">
                                                        <tr>
                                                            <td class="Label" bgcolor="#FFCC99" align="center">
                                                                Pago sin Procesar
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
                        <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Documentos">
                            <HeaderTemplate>
                                Documentos</HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UP_Doc" runat="server">
                                    <ContentTemplate>
                                        <%--Grilla de Doc--%>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="Btn_Criterio" runat="server" CssClass="boton" Text="Criterio" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Ordenar Por:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DP_Orden" runat="server" AutoPostBack="true" CssClass="clsMandatorio">
                                                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                                    <asp:ListItem Value="1">Nit Pagador</asp:ListItem>
                                                                    <asp:ListItem Value="2">N° Operación.</asp:ListItem>
                                                                    <asp:ListItem Value="3">N° Docto.</asp:ListItem>
                                                                    <asp:ListItem Value="4">Fecha Vcto.</asp:ListItem>
                                                                    <asp:ListItem Value="5">Saldo</asp:ListItem>
                                                                    <asp:ListItem Value="6">Estado Docto.</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--Grilla Documentos--%>
                                                    <div id="GridViewDiv" style="overflow: scroll; width: 1200px; height: 400px">
                                                        <asp:GridView ID="Gr_Documentos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            CellPadding="0" CssClass="formatUltcell" ShowHeader="True" Width="1500px">
                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderImageUrl="~/Imagenes/Iconos/check.gif" SortExpression="Todos">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CB_Seleccionar" runat="server" AutoPostBack="True" OnCheckedChanged="CB_Seleccionar_CheckedChanged" /></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="30" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="deudor" HeaderText="Razón Social">
                                                                    <ItemStyle HorizontalAlign="Left" Width="350" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TipoDocto" HeaderText="T.D.">
                                                                    <ItemStyle HorizontalAlign="center" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="id_opn" HeaderText="N° Ope.">
                                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="dsi_num" HeaderText="N° Doc.">
                                                                    <ItemStyle HorizontalAlign="Center" Width="120" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuo">
                                                                    <ItemStyle HorizontalAlign="Center" Width="40" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="doc_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto.">
                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Saldo" DataFormatString="{0:###,###,##0}" HeaderText="Saldo">
                                                                    <ItemStyle HorizontalAlign="Right" Width="110" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Interes" DataFormatString="{0:###,###,##0}" HeaderText="Interes">
                                                                    <ItemStyle HorizontalAlign="Right" Width="110" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Mto. A Pagar" ItemStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="Txt_MtoPagar" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                            OnTextChanged="Txt_MtoPagar_TextChanged" ReadOnly="true" Width="100px"> </asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="Txt_MtoPagar_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_MtoPagar" ValidChars=",.-">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                    <ItemStyle HorizontalAlign="center" Width="70" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="doc_ful_pgo" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec.Ult.Pag.">
                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cco_num" HeaderText="Est. Cob.">
                                                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cco_des" HeaderText="Est.Cob.">
                                                                    <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EstadoDocto" HeaderText="Estado">
                                                                    <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="P.P." ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CB_PD" runat="server" AutoPostBack="True" OnCheckedChanged="CB_PD_CheckedChanged" /></ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Contrato" HeaderText="Contrato">
                                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Lb_NroPagina" runat="server" CssClass="Label" Text=""></asp:Label><asp:Label
                                                        ID="K_Doctos" runat="server" CssClass="Label" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="Center">
                                                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="~/Imagenes/btn_workspace/flecha_izq_out.gif"
                                                        onmouseover="this.src='../../Imagenes/btn_workspace/flecha_izq_in.gif';" onmouseout="this.src='../../Imagenes/btn_workspace/flecha_izq_out.gif';" />
                                                    <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="~/Imagenes/btn_workspace/flecha_der_out.gif"
                                                        onmouseover="this.src='../../Imagenes/btn_workspace/flecha_der_in.gif';" onmouseout="this.src='../../Imagenes/btn_workspace/flecha_der_out.gif';" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table width="350" cellpadding="0" cellspacing="0" border="1">
                                                        <tr>
                                                            <td bgcolor="#FFFFCC" align="center" class="Label">
                                                                Documentos Hoja de Ruta
                                                            </td>
                                                            <td class="Label" bgcolor="#CCFFCC" align="center">
                                                                Pago Pagador
                                                            </td>
                                                            <td align="center" bgcolor="#FFCC99" class="Label">
                                                                Pago sin Procesar
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table width="250" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label37" runat="server" CssClass="Label" Text="Nota Crédito"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Not_Cre" runat="server" AutoPostBack="true" CssClass="clsDisabled"
                                                                    ReadOnly="True"></asp:TextBox><cc2:MaskedEditExtender ID="Txt_Not_Cre_MaskedEditExtender"
                                                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                        MaskType="Number" TargetControlID="Txt_Not_Cre">
                                                                    </cc2:MaskedEditExtender>
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
                    </cc2:TabContainer>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Total a Pagar"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Tot_Pag" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" CssClass="Label" Text="N° Seleccionado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Tot_Sel" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="40px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Total Nota de Crédito"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Tot_Not" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label62" runat="server" CssClass="Label" Text="Tasa a aplicar" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_Tasa" runat="server" CssClass="clsDisabled" Visible="false"
                                            ReadOnly="True" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:ImageButton ID="IB_Aceptar" runat="server" ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif"
                                AlternateText="Aceptar" onmouseover="this.src='../../Imagenes/Botones/boton_aceptar_in.gif';"
                                onmouseout="this.src='../../Imagenes/Botones/boton_aceptar_out.gif';" />
                            <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar selección"
                                ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" onmouseout="this.src='../../Imagenes/Botones/boton_limpiar_out.gif';"
                                onmouseover="this.src='../../Imagenes/Botones/boton_limpiar_in.gif';" />
                            <a href="javascript:window.close();">
                                <img alt="Volver" border="0" runat="server" id="img_volver" onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                    onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_In.gif';" src="../../Imagenes/Botones/Boton_Volver_Out.gif" />
                            </a>
                            <asp:ImageButton ID="IB_volver" runat="server" AlternateText="volver" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_in.gif';" />
                            <%--*********************************************************************************************--%>
                            <%--Criterio de Busqueda--%>
                            <asp:LinkButton ID="LB_Criterio" runat="server"></asp:LinkButton>
                            <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="LB_Criterio"
                                EnableViewState="False" PopupControlID="Panel_Criterio" X="5" Y="70" BackgroundCssClass="modalBackground">
                            </cc2:ModalPopupExtender>
                            <asp:Panel ID="Panel_Criterio" runat="server" Style="display: none;" CssClass="Contenido">
                                <%--Style="display: none;"--%>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="Cabecera" align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Criterio de Búsqueda" CssClass="Titulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table border="0" cellpadding="2" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <table border="0" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <%--Deudor--%>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="CB_Deudor" runat="server" Text="Pagador" CssClass="Label" AutoPostBack="true" />
                                                                                    <asp:TextBox ID="Txt_Rut_Deu" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                                        ReadOnly="true" Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                                    </cc2:MaskedEditExtender>
                                                                                    <asp:TextBox ID="Txt_Dig_Deu" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                                                        ReadOnly="true" MaxLength="1" Style="position: static" TabIndex="1" Width="15px"
                                                                                        AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:ImageButton ID="IB_AyudaDeu" runat="server" ImageUrl="../../Imagenes/Iconos/155.ICO"
                                                                                        Width="20" Enabled="false" AlternateText="Ayuda Pagador" Visible="true" />
                                                                                    <asp:TextBox ID="Txt_Rso_Deu" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                                        ReadOnly="True" Style="position: static" Width="280px"></asp:TextBox>
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
                                                            <table border="0" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label3" runat="server" Text="Tipo Docto." CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_TipoDocto" runat="server" CssClass="clsTxt" Width="100">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label4" runat="server" Text="N° Operación" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Nro_Oto" runat="server" CssClass="clsTxt" Width="70"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Nro_Oto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Nro_Oto"
                                                                                        InputDirection="RightToLeft">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label5" runat="server" Text="N° Docto." CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsTxt" Width="150" MaxLength="20"></asp:TextBox>
                                                                                    <%-- <cc2:MaskedEditExtender ID="Txt_Nro_Doc_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Nro_Doc"
                                                                                InputDirection="RightToLeft">
                                                                            </cc2:MaskedEditExtender>--%>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Est. Cob." CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_CodCobranza" runat="server" Width="300px" CssClass="clsTxt" />
                                                                                    <cc2:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="DP_CodCobranza"
                                                                                        PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                                                        PromptPosition="Bottom" IsSorted="true">
                                                                                    </cc2:ListSearchExtender>
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
                                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <%--Monto Doctos--%>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Monto Doctos." CssClass="Label" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label7" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Mto_Dsd" runat="server" CssClass="clsTxt" Width="100"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Mto_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                                        TargetControlID="Txt_Mto_Dsd">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Mto_Hst" runat="server" CssClass="clsTxt" Width="100"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Mto_Hst_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                                        TargetControlID="Txt_Mto_Hst">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <%--Fecha Vcto--%>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="Label10" runat="server" Text="Fecha Vcto." CssClass="Label" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label11" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_Dsd" runat="server" CssClass="clsTxt" Width="70"></asp:TextBox>
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_Dsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Dsd">
                                                                                    </cc2:CalendarExtender>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Dsd">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_Fec_Hst" runat="server" CssClass="clsTxt" Width="70"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Hst_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Hst">
                                                                                    </cc2:MaskedEditExtender>
                                                                                    <cc2:CalendarExtender ID="Txt_Fec_Hst_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Hst">
                                                                                    </cc2:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <%--Estado de Documentos--%>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label14" runat="server" Text="Estados" CssClass="Label" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_Estados" runat="server" CssClass="clsTxt" Width="150">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:ImageButton ID="btn_acep" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_Out.gif"
                                                                            onmouseover="this.src='../../Imagenes/Botones/boton_Buscar_in.gif';" onmouseout="this.src='../../Imagenes/Botones/boton_Buscar_out.gif';"
                                                                            AlternateText="Buscar"></asp:ImageButton>
                                                                        <asp:ImageButton ID="btn_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                                                            onmouseover="this.src='../../Imagenes/Botones/boton_limpiar_in.gif';" onmouseout="this.src='../../Imagenes/Botones/boton_limpiar_out.gif';"
                                                                            AlternateText="Limpiar" />
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
                            </asp:Panel>
                            <%--*********************************************************************************************--%>
                            <%--Grilla con pagos sin procesar--%>
                            <asp:LinkButton ID="LinkPagos" runat="server"></asp:LinkButton>
                            <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkPagos"
                                EnableViewState="False" PopupControlID="Panel_PagosAnt" BackgroundCssClass="modalBackground"
                                PopupDragHandleControlID="Panel_PagosAnt">
                            </cc2:ModalPopupExtender>
                            <asp:Panel ID="Panel_PagosAnt" runat="server" Style="display: none">
                                <table id="nn" class="Contenido">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridPagos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="formatUltcell" PageSize="1" ShowHeader="true">
                                                <FooterStyle CssClass="cabeceraGrilla" />
                                                <Columns>
                                                    <asp:BoundField DataField="id_doc" HeaderText="N° Docto.">
                                                        <ItemStyle HorizontalAlign="Right" Width="80" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="id_cxc" HeaderText="N° cxc">
                                                        <ItemStyle HorizontalAlign="Right" Width="80" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Estado" HeaderText="Est. Ingreso">
                                                        <ItemStyle HorizontalAlign="center" Width="80" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ing_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec. Ingr">
                                                        <ItemStyle HorizontalAlign="center" Width="80" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ing_mto_tot" DataFormatString="{0:###,###,###}" HeaderText="Monto">
                                                        <ItemStyle HorizontalAlign="Right" Width="150" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Modulo" HeaderText="Modulo">
                                                        <ItemStyle HorizontalAlign="center" Width="100" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ejecutivo" HeaderText="Recaudor">
                                                        <ItemStyle HorizontalAlign="center" Width="300" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%-- <asp:ImageButton ID="IB_Imprir_Pagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Imprimir_Out.gif"
                                        onmouseout="this.src='../../Imagenes/Botones/Boton_Imprimir_Out.gif';" onmouseover="this.src='../../Imagenes/Botones/Boton_Imprimir_in.gif';" />--%>
                                            <asp:ImageButton ID="IB_Cerrar_Pagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                                onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_in.gif';" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:LinkButton ID="LB_MarcaGrilla" runat="server"></asp:LinkButton>
                            <asp:HiddenField ID="HF_Posicion" runat="server" />
                            <asp:HiddenField ID="HF_Posicion_CxC" runat="server" />
                            <uc2:Mensaje ID="Mensaje1" runat="server" />
                            <asp:LinkButton ID="LB_BuscarCliente" runat="server"></asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
 
 
 
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>