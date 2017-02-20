<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Estado_Deuda.aspx.vb" Inherits="Estado_Deuda" Title="Estado de Deuda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../Ayudas/FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>
    <script language="javascript">


function DoScroll()
 {
    var _gridView = document.getElementById("GridViewDiv");
    var _header = document.getElementById("HeaderDiv");
     _header.scrollLeft = _gridView.scrollLeft;
 }

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="General" width="100%" border="0" cellpadding="0" cellspacing="1" class="Contenido">
                <tr>
                    <td class="Cabecera" style="height: 31px">
                        <asp:Label ID="Label1" runat="server" Text="Operaciones - Estado de Deuda" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="height: 590px; padding: 5px" valign="top" align="center">
                        <%-- ************Tabla Contenedora*************    --%>
                        <table>
                            <tr>
                                <td align="center">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="padding: 5px">
                                                <%--********Cliente*************--%>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:Label ID="Label6" runat="server" Text="Cliente" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="4" align="left">
                                                                        <asp:CheckBox ID="Check_Cli" runat="server" Text="Cliente" CssClass="Label" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999"
                                                                            MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" Width="23px"
                                                                            MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Enabled="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" Width="400px"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px" align="right">
                                                <%--**********Sucursal************--%>
                                                <table id="Sucursal" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:Label ID="Label3" runat="server" Text="Sucursal" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td >
                                                                        <asp:CheckBox ID="Chebox_Suc" runat="server" Text="Todas" CssClass="Label" AutoPostBack="True" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_Suc" runat="server" CssClass="clsDisabled">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="padding: 5px" align="left">
                                                <%--********Con/sim Responsabilidad***********--%>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label7" runat="server" Text="Con/Sin Recurso" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="Check_Resp" runat="server" Text="Todas" CssClass="Label" AutoPostBack="True" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_Resp" runat="server" CssClass="clsDisabled">
                                                                            <asp:ListItem Value="99">Seleccionar</asp:ListItem>
                                                                            <asp:ListItem Value="0">Con Recurso</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sin Recurso</asp:ListItem>
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label20" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <asp:Panel ID="Panel_Gr_Doc" runat="server" ScrollBars="Horizontal" Width="900px" Height="360px">
                                                    <asp:GridView ID="Gr_Doc" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                        ShowHeader="True" Width="1500px">
                                                        <Columns>
                                                            <asp:BoundField DataField="cli_idc" HeaderText="NIT Cliente">
                                                                <ItemStyle HorizontalAlign="center" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nombre_cliente" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="left" Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                                <ItemStyle HorizontalAlign="center" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nombre_Deudor" HeaderText="Razón Social ">
                                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="tip_doc" HeaderText="T.D.">
                                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="n_doc" HeaderText="Nº Docto.">
                                                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="n_de_cuotas" HeaderText="Nº de Cuotas">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Monto_Doc" HeaderText="Mto. Docto.">
                                                                <ItemStyle HorizontalAlign="right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="monto_anticipo" HeaderText="Mto. Antic.">
                                                                <ItemStyle HorizontalAlign="right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="saldo_neto" HeaderText="Saldo Neto">
                                                                <ItemStyle HorizontalAlign="right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="saldo_mora" HeaderText="Saldo Mora">
                                                                <ItemStyle HorizontalAlign="right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="fecha_otog" HeaderText="Fecha Otor." DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle HorizontalAlign="center" Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Fecha_Vto" HeaderText="Fecha Vecto." DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center"  />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ope_res_son" HeaderText="Recurso">
                                                                <ItemStyle HorizontalAlign="Center"  />
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
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
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
                        <asp:ImageButton ID="IB_Buscar" runat="server" AlternateText="Buscar" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                        <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprimir" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                            OnClick="IB_Imprimir_Click" onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" Style="position: static" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" />
                    </td>
                </tr>
            </table>
            
                  <asp:HiddenField ID="HF_Cont" runat="server" />
                        <asp:LinkButton ID="lb_cli" runat="server"></asp:LinkButton>
                     
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <uc1:cargando id="Cargando1" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>