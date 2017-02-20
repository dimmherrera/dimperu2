<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="AnulacionProrroga.aspx.vb" Inherits="Modulos_Prorrogas_rightframe_archivos_AnulaProrrogas"
    Title="Anula Prorroga" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Carp. Comercial/FuncionesPrivadasJS/Negociacion.js" type="text/javascript"></script>

    <script src="../../Ayudas/FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>

    <script src="../FuncionesProvadasJS/SolicitudProrroga.js" type="text/javascript"></script>

    <script src="../FuncionesProvadasJS/VBProrroga.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript">

        function SelecionaDocto(Posicion) {
            window.document.forms[0].hf_posicion.value = Posicion;
            return;
        }




        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");
            var _header = document.getElementById("HeaderDiv");
            _header.scrollLeft = _gridView.scrollLeft;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tb_gral" style="position: static; text-align: -moz-center" cellspacing="1"
                cellpadding="0" width="100%" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" height="31px" align="center" valign="middle">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Control Dual - VºBº Prorroga"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px; height: 590px; position: static; text-align: -moz-center"
                        align="center" valign="top">
                        <table style="width: 100%; position: static; text-align: -moz-center" cellspacing="0">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label43" runat="server" __designer:wfdid="w284" CssClass="SubTitulos"
                                        Style="left: 8px; position: static; top: -14px" Text="Criterios de Búsqueda"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center">
                                    <table id="cliente_diarios" border="0">
                                        <tr class="0">
                                            <%--Cliente--%>
                                            <td valign="top">
                                                <table id="tb_cliente" border="0" cellpadding="0" cellspacing="0" width="550">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:CheckBox ID="CBX_Cliente" runat="server" Text="Cliente" CssClass="SubTitulos"
                                                                    AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" style="height: 80px" valign="top">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label12" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Identificación"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 130px">
                                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" __designer:wfdid="w286" CssClass="clsMandatorio"
                                                                                    Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                                <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="None"
                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                    CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                                </cc2:MaskedEditExtender>
                                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                                    MaxLength="1" TabIndex="1" Width="15px"></asp:TextBox>
                                                                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                                    Enabled="True" TargetControlID="Txt_Dig_Cli" FilterType="Custom, Numbers" ValidChars="K,k">
                                                                                </cc2:FilteredTextBoxExtender>
                                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                    Width="20px" Style="margin-top: 0px" Enabled="False" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label41" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                                                    Text="Tipo de Cliente" Width="100px"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_TipoCliente" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                    Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" colspan="1">
                                                                                <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                                                    Text="Razón Soc." Width="70px"></asp:Label>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                                    ReadOnly="True" Style="position: static" Width="465px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label15" runat="server" __designer:wfdid="w290" CssClass="Label" Style="position: static"
                                                                                    Text="Sucursal"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 160px">
                                                                                <asp:TextBox ID="Txt_Sucursal" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                    Width="180px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label14" runat="server" __designer:wfdid="w292" CssClass="Label" Style="position: static"
                                                                                    Text="Ejecutivo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                    Width="180px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label42" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                                                    Text="Banca"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 160px">
                                                                                <asp:TextBox ID="Txt_Banca" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                    Width="180px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <%--Criterios (Fecha Vcto.)--%>
                                            </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="left" class="Cabecera">
                                        <asp:Label ID="Label20" runat="server" Text="Solicitudes de Prorroga" CssClass="SubTitulos"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height: 150px" valign="top" align="center">
                                        <table id="Table4" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="Contenido" align="center" width="1450px">
                                                    <asp:Panel ID="Panel_GV_Negociacion" runat="server" Width="790px" Height="150px">
                                                        <asp:GridView ID="GV_Negociacion" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Seleccion" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                    ItemStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                                            ToolTip='<%# Eval("id_spg") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="NIT Cliente" DataField="cli_idc">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Razón Social" DataField="Cliente">
                                                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Fecha" DataField="spg_fec">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Nº Pro." DataField="id_spg">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Ejecutivo" DataField="eje_des_cra">
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Tasa" DataField="spg_tas">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Comision" DataField="spg_com">
                                                                    <ItemStyle Width="110px" HorizontalAlign="Right" />
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
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <img src="../../../Imagenes/Infografia/Aprobado.gif" />
                                                </td>
                                                <td>
                                                    <img src="../../../Imagenes/Infografia/Rechazado.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <asp:LinkButton ID="Busqueda_GV_SOLICITUD" runat="server" OnClick="Busqueda_GV_SOLICITUD_Click"
                            CausesValidation="False" ForeColor="Black"></asp:LinkButton>
                        <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="left" class="Cabecera">
                                        <asp:Label ID="Label24" runat="server" Text="Nómina de Documentos a Prorrogar" CssClass="SubTitulos"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 100px" align="center">
                                        <asp:Panel ID="Panel_GV_DetalleSolicitud" runat="server" Height="150px" ScrollBars="Horizontal"
                                            Width="1200px">
                                            <asp:GridView ID="GV_DetalleSolicitud" runat="server" AutoGenerateColumns="False"
                                                CssClass="formatUltcell" ShowHeader="true">
                                                <Columns>
                                                    <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Deudor" HeaderText="Razón Social">
                                                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="opo_otg" HeaderText="N° Otorg.">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TipoDocto" HeaderText="Tipo Doc.">
                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="dsi_num" HeaderText="N° Doc.">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="dsi_mto_fin" HeaderText="Monto Doc.">
                                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="doc_sdo_cli" HeaderText="Saldo Doc.">
                                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="doc_fev_rea" HeaderText="Fecha Vcto.">
                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nva_doc_fev_rea" HeaderText="Nva. Fecha Vcto.">
                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="dpg_int_ere" HeaderText="Interes">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="dpg_com_isi" HeaderText="Comisión">
                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="dpg_iva_com" HeaderText="IVA">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TotalGastos" HeaderText="Gastos">
                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                <RowStyle CssClass="formatUltcell" />
                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <%-- <td align="center">
                                                       
                                                  </td>--%>
                                        <%--</div>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="IB_Prev_GvDetalle" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                        <asp:ImageButton ID="IB_Next_GvDetalle" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;
                        <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="IB_Buscar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" __designer:wfdid="w375"
                            AlternateText="Buscar " ValidationGroup="Cliente"></asp:ImageButton>
                        <asp:ImageButton ID="IB_AnularSolicitud" runat="server" AlternateText="Anular Solicitud de Prorroga"
                            ImageUrl="~/Imagenes/Botones/Boton_Anular_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Anular_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Anular_in.gif';" />
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" AlternateText="Limpiar">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_Nro_Neg" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_AnularSolicitud" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:LinkButton ID="Lb_aprobar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Lb_rechazar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Lb_Bus_Pag" runat="server"></asp:LinkButton>
</asp:Content>
