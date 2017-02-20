<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="CtasCxP.aspx.vb" Inherits="ClsCxP" Title="Mantención Ctas. Por Pagar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");
            var _header = document.getElementById("HeaderDiv");
            _header.scrollLeft = _gridView.scrollLeft;
        }

    </script>

    <%--<script src="../FuncionesPrivadasJS/CXC.js" type="text/javascript"></script>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" class="Contenido" cellspacing="1" width="100%">
                <tr>
                    <td class="Cabecera" height="31">
                        <asp:Label ID="Label7" runat="server" CssClass="Titulos" Text="Administración - Cuentas por Pagar"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido" align="center">
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="580px">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-left: 5px;" align="left">
                                        <br />
                                        <table border="0" cellpadding="0" cellspacing="0" style="position: static; width: 733px;
                                            margin-right: 4px;">
                                            <tr>
                                                <td class="Cabecera" height="21">
                                                    <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Clientes"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td_Cli" class="Contenido">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                                            </td>
                                                            <td style="width: 148px">
                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                </cc1:MaskedEditExtender>
                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                    MaxLength="1" Width="16px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" />
                                                            </td>
                                                            <td align="right" style="width: 75px">
                                                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Razón Soc."></asp:Label>
                                                            </td>
                                                            <td style="width: 121px">
                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="400px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px;" align="left">
                                        <br />
                                        <table border="0" cellpadding="0" cellspacing="0" style="height: 35px">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label24" runat="server" CssClass="SubTitulos" Text="Tipo de Cuenta"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:DropDownList ID="dropTipoCuenta" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                        Enabled="False" Height="20px" Width="180px" EnableTheming="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px;" align="left">
                                        <br />
                                        <table border="0" cellpadding="0" cellspacing="0" style="position: static; width: 642px;">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label9" runat="server" CssClass="SubTitulos" Text="Datos Ctas. por Pagar"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left" class="Contenido">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right" style="height: 23px">
                                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Tipo de Moneda"></asp:Label>
                                                            </td>
                                                            <td style="height: 19px" align="left">
                                                                <asp:DropDownList ID="drop_TpMoneda" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Fecha"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Fec_Cxc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Monto"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Monto" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="100px" AutoPostBack="false" MaxLength="10"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Monto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                    TargetControlID="Txt_Monto">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px; height: 19px" align="left">
                                                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="297px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Nro Contrato"></asp:Label>
                                                            </td>
                                                            <td style="width: 200px; height: 19px">
                                                                <asp:TextBox ID="txt_Contrato" runat="server" CssClass="clsDisabled" Width="150px"
                                                                    ReadOnly="false"></asp:TextBox>
                                                                <asp:ImageButton ID="IB_AyudaDoc" runat="server" Height="16px" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" Enabled="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Nro Docto."></asp:Label>
                                                            </td>
                                                            <td style="width: 200px; height: 19px">
                                                                <asp:TextBox ID="txt_nro_doc" runat="server" CssClass="clsDisabled" Width="120px" ReadOnly="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--</asp:Panel>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <br />
                                        <%--*********Cabecera Grilla*******--%>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label5" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:Panel ID="Panel_GrCXP" runat="server" ScrollBars="Horizontal" Width="1200px"
                                                        Height="180px" HorizontalAlign="Right">
                                                        <asp:GridView ID="GV_Cuentas" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            PageSize="8" Width="1230px">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                    ItemStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_cxp") %>'
                                                                            OnClick="Img_Ver_Click" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cuenta">
                                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="id_cxp" HeaderText="Nº Cuenta">
                                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxp_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Cta X Pa">
                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Contrato" HeaderText="Nº Contrato">
                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Numero" HeaderText="Número Docto.">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Moneda" HeaderText="Tipo Moneda">
                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxp_mto" HeaderText="Monto Cta">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxp_des" HeaderText="Descripción">
                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                           <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla"  />
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="lb_id_doc" Visible="false" runat="server">LinkButton</asp:LinkButton>
                        <asp:ImageButton ID="IB_Buscar" runat="server" OnClick="IB_Buscar_Click" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                            Style="position: static" AlternateText="Buscar" />
                        <asp:ImageButton ID="IB_Nuevo" runat="server" OnClick="IB_Nuevo_Click" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';"
                            Style="position: static" Enabled="False" AlternateText="Nuevo" />
                        <asp:ImageButton ID="IB_Anular" runat="server" OnClick="IB_Anular_Click" ImageUrl="~/Imagenes/Botones/boton_anular_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_anular_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_anular_in.gif';"
                            Style="position: static" Enabled="False" AlternateText="Anular" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" OnClick="IB_Guardar_click" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                            Style="position: static" Enabled="False" AlternateText="Guardar" />
                        <asp:ImageButton ID="IB_Imprimir" runat="server" OnClick="IB_Imprimir_Click" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            Style="position: static" AlternateText="Imprime Informe" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" OnClick="IB_Limpiar_click" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            Style="position: static" AlternateText="Limpia Pantalla" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_NroCuenta" runat="server" />
            <asp:HiddenField ID="HF_Pos" runat="server" />
            <asp:HiddenField ID="HF_Id" runat="server" />
            <asp:HiddenField ID="hf_id_doc" runat="server" />
            <asp:TextBox ID="TipoCuenta" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Solid" Visible="False"></asp:TextBox>
            <asp:TextBox ID="NroCuenta" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Solid" Visible="False"></asp:TextBox>
            <asp:LinkButton ID="LinkbN_Cuenta" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="IB_Deu" runat="server" Visible="False"></asp:LinkButton>
            <asp:LinkButton ID="IB_Cli" runat="server"></asp:LinkButton>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
            <asp:PostBackTrigger ControlID="lb_id_doc" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Link_Anular" runat="server"></asp:LinkButton>
</asp:Content>
