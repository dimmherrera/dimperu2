<%@ Page Language="VB" EnableEventValidation="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="ing-ope.aspx.vb" Inherits="ClsIngOpe" Title="Cuadratura de Operaciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
        
            <table cellspacing="1" width="100%" border="0" class="Contenido">
                <tbody>
                    <tr>
                        <td style="width: 941px; height: 31px" class="Cabecera">
                            <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Operaciones - Cuadratura Operaciones"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" style="padding: 0px" cellpadding="0" cellspacing="0" border="0" align="center" height="580px" valign="top" >
                            <table id="Table_Contenido" cellspacing="0" cellpadding="3" border="0">
                                <tr>
                                    <td valign="top" align="left">
                                        <table id="Table_Cliente" cellspacing="0" cellpadding="0" width="900" border="0">
                                            <tr>
                                                <td class="Cabecera" align="left">
                                                    <asp:Label ID="Label18" runat="server" CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" valign="top">
                                                    <table id="Contenido_Cliente">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" TabIndex="1"
                                                                    Width="90px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                </cc1:MaskedEditExtender>
                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                                    TabIndex="2" Width="20px" AutoPostBack="True"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" />
                                                            </td>
                                                            <td align="rigth">
                                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Razón Social"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 3px">
                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsTxt" TabIndex="4" Width="464px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="1">
                                        <table id="Table_Datos_Generales" cellspacing="1" cellpadding="0" border="0" width="900">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 10px" class="Cabecera">
                                                        <asp:Label ID="Label13" runat="server" CssClass="SubTitulos" Text="Operaciones Ingresadas de Cliente"></asp:Label>
                                                    </td>
                                                    <td style="width: 387px; height: 10px" class="Cabecera">
                                                        <asp:Label ID="Label14" runat="server" CssClass="SubTitulos" Text="Datos de Operación"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" class="Contenido" width="300">
                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class="Contenido">
                                                                                    <asp:Panel ID="panel" runat="server" ScrollBars="none" Height="140px" 
                                                                                        Width="240px">
                                                                                        <asp:GridView ID="GR_OPERACIONES" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_ope") %>'
                                                                                                            OnClick="Img_Ver_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Operación">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="opn_fec_neg"
                                                                                                    HeaderText="Fecha Ope.">
                                                                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                            <RowStyle CssClass="formatUltcell" />
                                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                        </asp:GridView> 
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                        <table style="width: 80px">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btn_prev_ope" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif'"
                                                                                        ToolTip="Anterior" Enabled="False" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btn_next_ope" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif'"
                                                                                        ToolTip="Siguiente" Enabled="False" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Infografia/Ope_cuadrada.gif" />
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Infografia/Ope_descuadrada.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td Valign="top" align="left" class="Contenido">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table id="Table2" cellspacing="1" cellpadding="1" width="300" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 151px" valign="top" align="right">
                                                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Fecha Ing."></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_FecIngreso" TabIndex="6" runat="server" CssClass="clsDisabled"
                                                                                        ReadOnly="true" Width="90px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 26px" valign="top" align="right">
                                                                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Cant. Doctos."></asp:Label>
                                                                                </td>
                                                                                <td style="height: 26px">
                                                                                    <asp:TextBox ID="Txt_CanDocumentos" TabIndex="7" runat="server" CssClass="clsDisabled"
                                                                                        ReadOnly="true" Width="90px"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_CanDocumentos_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Numbers" TargetControlID="Txt_CanDocumentos">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px" align="right">
                                                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_Moneda" TabIndex="8" runat="server" CssClass="clsDisabled"
                                                                                        Width="100px" READONLY="TRUE">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px" align="right">
                                                                                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Monto Financiar"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_MontoFinanciar" TabIndex="9" runat="server" CssClass="clsDisabled"
                                                                                        ReadOnly="true"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="Txt_mto_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_MontoFinanciar"
                                                                                        AutoComplete="False">
                                                                                    </cc1:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 6px" align="right">
                                                                                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Tipo Operación"></asp:Label>
                                                                                </td>
                                                                                <td style="height: 6px">
                                                                                    <asp:DropDownList ID="DP_TipoOperacion" TabIndex="10" runat="server" CssClass="clsDisabled"
                                                                                        READONLY="TRUE" Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px" align="right">
                                                                                        <asp:Label ID="opmode" runat="server" CssClass="Label">Modo Operación</asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="RB_ModoOpera" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                                                                        Enabled="False">
                                                                                        <asp:ListItem Value="S">Líneal</asp:ListItem>
                                                                                        <asp:ListItem Value="N">Exponencial</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Tipo Documento"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DP_TipoDocumento" runat="server" CssClass="clsDisabled" READONLY="TRUE"
                                                                                    TabIndex="11" Width="150px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label11" runat="server" __designer:wfdid="w1" CssClass="Label" Text="Caract.  Ope."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="dp_mod_ope" runat="server" __designer:wfdid="w2" AutoPostBack="True"
                                                                                    CssClass="clsDisabled" TabIndex="11" Width="150px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Con Cuotas"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="RBConCuotas" runat="server" CssClass="Label" Enabled="False"
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Ope.Puntual"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="RB_OpePuntual" runat="server" CssClass="Label" Enabled="False"
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Recurso"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="RB_Responsabilidad" runat="server" CssClass="Label" Enabled="False"
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
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
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        
                                        <br />
                                        <table id="Table_Documentos" cellspacing="0" cellpadding="0" border="0" width="900">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label15" runat="server" CssClass="SubTitulos" Text="Documentos Ingresados de Cliente"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" align="center">
                                                    <table id="Table1" cellspacing="1" cellpadding="1" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center">
                                                                    <table style="width: 100%" cellspacing="0">
                                                                    
                                                                        <tr>
                                                                            <td class="Contenido">
                                                                                <asp:Panel ID="Panel1" runat="server" Height="190px" ScrollBars="Vertical" Width="718px">
                                                                                    <asp:GridView ID="Gr_Documentos" runat="server" CssClass="formatUltcell"                                                                                         AutoGenerateColumns="False" Height="0px" PageSize="8">
                                                                                        <PagerSettings PageButtonCount="15" NextPageImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                            NextPageText="Siguiente" PreviousPageImageUrl="flecha_der_in" PreviousPageText="Anterior" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                ItemStyle-Width="90px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                                                                        ToolTip='<%# Eval("dsi_num") %>' AlternateText='<%# Eval("dsi_flj_num") %>' onclick="Img_Ver_Click1" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="deu_rso" HeaderText="Pagador">
                                                                                                <ItemStyle Width="350px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="dsi_num" HeaderText="Nº Documento">
                                                                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="dsi_flj_num" HeaderText="Nº Cuota">
                                                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="dsi_mto_fin" HeaderText="Monto Financiado">
                                                                                                <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" DataField="dsi_fev_rea" HeaderText="Fecha Vcto">
                                                                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="cal_oto_gam" HeaderText="Cal. Otor">
                                                                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <PagerStyle HorizontalAlign="Center" />
                                                                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                                        <RowStyle CssClass="formatUltcellAlt" />
                                                                                        <%--<AlternatingRowStyle CssClass="formatUltcellAlt" />--%>
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Infografia/documento.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Infografia/Cabecera_doc.gif" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table style="width: 80px">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btn_prev_otg" runat="server" Enabled="False" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif'"
                                                                                    ToolTip="Anterior" Visible="False" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btn_next_otg" runat="server" Enabled="False" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif'" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif'"
                                                                                    ToolTip="Siguiente" Visible="False" />
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
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table id="Table_Botonera" border="0" cellpadding="1" cellpadding="0" cellspacing="1"
                                cellspacing="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                                OnClick="Btn_Buscar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" TabIndex="22"
                                                ToolTip="Buscar" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_Actas" runat="server" ImageUrl="~/Imagenes/Botones/boton_Actas_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_Actas_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_actas_in.gif';"
                                                ToolTip="Adjuntar Actas" Enabled="False" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_IntMas" runat="server" ImageUrl="~/Imagenes/Botones/boton_int_masiva_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_int_masiva_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_int_masiva_in.gif';"
                                                Style="position: static" TabIndex="24" ToolTip="Integración Masiva" Enabled="False" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_Anular" runat="server" Enabled="False" ImageUrl="~/Imagenes/Botones/boton_anular_out.gif"
                                                OnClick="Btn_Anular_Click" onmouseout="this.src='../../../Imagenes/Botones/boton_anular_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/boton_anular_in.gif';" Style="position: static"
                                                TabIndex="25" ToolTip="Anular" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_gua_ope" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                                ToolTip="Guardar" onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_ing_chq" runat="server" Enabled="False" ImageUrl="~/Imagenes/Botones/btn_ing_cheque_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/btn_ing_cheque_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_ing_cheque_in.gif';"
                                                ToolTip="Ingreso de Cheques" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_asoc_cheque" runat="server" ImageUrl="~/Imagenes/Botones/btn_asoc_cheque_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/btn_asoc_cheque_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_asoc_cheque_in.gif';"
                                                Enabled="False" ToolTip="Asociar Documentos" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="Btn_Ingdoc" runat="server" ImageUrl="~/Imagenes/Botones/boton_ing_docto_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_ing_docto_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_ing_docto_in.gif';"
                                                Style="position: static" TabIndex="27" ToolTip="Ingreso de Documentos" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_ModDoc" runat="server" ImageUrl="~/Imagenes/Botones/Boton_mod_docto_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_mod_docto_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_mod_docto_in.gif';"
                                                Style="position: static" TabIndex="28" ToolTip="Modificar Documentos" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_EliDoc" OnClick="Btn_EliDoc_Click" runat="server" ImageUrl="~/Imagenes/Botones/boton_elim_docto_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_elim_docto_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_elim_docto_in.gif';"
                                                Style="position: static" TabIndex="29" ToolTip="Eliminar Documentos" />
                                        </td>
                                        <td style="height: 24px">
                                            <asp:ImageButton ID="Btn_Limpiar" OnClick="Btn_Limpiar_click" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                                Style="position: static" TabIndex="30" ToolTip="Limpiar" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            
            <asp:TextBox ID="Txt_ItemDSI" TabIndex="40" runat="server" Width="0px" Height="0px"
                BorderColor="Transparent" BorderStyle="None" BackColor="Transparent" ForeColor="Transparent"></asp:TextBox>
            <asp:TextBox ID="Txt_ItemDeudorProblemas" TabIndex="41" runat="server" Width="0px"
                Height="0px" BorderColor="Transparent" BorderStyle="None" BackColor="Transparent"
                ForeColor="Transparent"></asp:TextBox>
            <asp:TextBox ID="Txt_RefrescaCollDSI" TabIndex="42" runat="server" Width="0px" Height="0px"
                BorderColor="Transparent" BorderStyle="None" BackColor="Transparent" ForeColor="Transparent"></asp:TextBox>
            <asp:TextBox ID="Txt_ItemOPE" TabIndex="43" runat="server" Width="0px" Height="0px"
                BorderColor="Transparent" BorderStyle="None" BackColor="Transparent" ForeColor="Transparent"></asp:TextBox>
            <asp:TextBox ID="Txt_NroNvaOperacion" TabIndex="44" runat="server" Width="0px" Height="0px"
                BorderColor="Transparent" BorderStyle="None" BackColor="Transparent" ForeColor="Transparent"></asp:TextBox>
            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                TargetControlID="Txt_Dig_Cli" ValidChars="Kk">
            </cc1:FilteredTextBoxExtender>
            <asp:Label ID="Lbl_msje" runat="server" CssClass="Mensaje"></asp:Label>
            <asp:LinkButton ID="RetornaDoctos" TabIndex="30" runat="server"></asp:LinkButton>
            <asp:Label Style="left: 1px; top: 0px" ID="sw" TabIndex="33" runat="server"></asp:Label>
            <asp:LinkButton ID="LB_GuardaOpe" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Lb_grilla" OnClick="Lb_grilla_Click" runat="server"></asp:LinkButton>
            &nbsp;
            <asp:TextBox ID="NoSeleccion" runat="server" Width="0px" Height="0px" BorderColor="Transparent"
                BorderStyle="None" BackColor="Transparent" ForeColor="Transparent"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Btn_Actas" />
            <asp:PostBackTrigger ControlID="Btn_ModDoc" />
            <asp:PostBackTrigger ControlID="Btn_Ingdoc" />
            <asp:PostBackTrigger ControlID="Btn_IntMas" />
            <asp:PostBackTrigger ControlID="btn_ing_chq" />
            <asp:PostBackTrigger ControlID="btn_asoc_cheque" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="lb_eli_doc" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lb_anu" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lb_guar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Lb_buscar" OnClick="Lb_buscar_Click" runat="server"></asp:LinkButton>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
</asp:Content>
