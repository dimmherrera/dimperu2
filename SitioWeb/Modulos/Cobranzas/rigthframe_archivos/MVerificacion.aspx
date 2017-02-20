<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="MVerificacion.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Verificacion"
    Title="Verificacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesProvadasJS/Verificacion.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
    <asp:UpdatePanel runat="server" ID="UP_Verificacion" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tb_gral" cellpadding="0" cellspacing="1" style="width: 100%" class="Contenido">
                <tr>
                    <td class = "Cabecera" style="height: 31px">
                        <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="COBRANZA - Verificación de Documentos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="Contenido" style="padding: 10px; Height:590px; text-align:-moz-center" valign="top">
                        <table align="center" cellpadding="0" cellspacing="0" width="80%">
                            <tr>
                                 <td align="center" style="text-align:-moz-center">  
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Lbl_Cliente" runat="server" CssClass="SubTitulos" Style="left: 8px;
                                                    position: static; top: -14px" Text="Cliente"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  align="left" class="Contenido">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                            </cc2:MaskedEditExtender>
                                                            <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                                Width="15px" AutoPostBack="True" CausesValidation="True"> </asp:TextBox>
                                                            <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                            </cc2:FilteredTextBoxExtender>
                                                            <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes"
                                                             ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="23px" />        
                                                                    
                                                        </td>
                                                        
                                                        <td align="right">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Lbl_Raz_Soc" runat="server" CssClass="Label" Text="Razón Social"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3" rowspan="1">
                                                            <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                Width="365px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 115px">
                                                            <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Tipo Cliente" 
                                                                Style="position: static"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 345px">
                                                            <asp:DropDownList ID="Dp_Tipo_Cli" runat="server" AutoPostBack="True" 
                                                                CssClass="clsDisabled" Enabled="False" TabIndex="2" Width="300px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" style="width: 120px">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 115px">
                                                            <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 345px">
                                                            <asp:DropDownList ID="Dp_Suc" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                Enabled="False" TabIndex="6" Width="300px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" style="width: 120px">
                                                            <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Ejecutivo Factoring"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="Dp_Eje" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                TabIndex="7" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="text-align:-moz-center">
                                    <br />
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label14" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                    top: -14px" Text="Criterio de Búsqueda"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="left">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" width="90%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Tipo Moneda"></asp:Label>
                                                                                </td>
                                                                                <td align="left" colspan="3">
                                                                                    <asp:DropDownList ID="Dp_Tip_Mon" runat="server" CssClass="clsMandatorio" Width="330px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="115">
                                                                                    <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Tipo Doct."></asp:Label>
                                                                                </td>
                                                                                <td align="left" colspan="3">
                                                                                    <asp:DropDownList ID="Dp_Tip_Doc" runat="server" CssClass="clsTxt" Width="330px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Nro. Ope."></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="Txt_Nro_ope" runat="server" CssClass="clsTxt" TabIndex="1" Width="95px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Nro_ope_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Nro_ope">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Nro. Doct."></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsTxt" TabIndex="1" Width="95px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="Txt_Nro_Doc_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Nro_Doc">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="150">
                                                                            <tr>
                                                                                <td class="Cabecera">
                                                                                    <asp:Label ID="Label19" runat="server" CssClass="SubTitulos" Text="Monto Doctos."></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Contenido" align="center">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 68px">
                                                                                                <asp:TextBox ID="Txt_Mto_Dsd" runat="server" CssClass="clsTxt" Height="20px" TabIndex="1"
                                                                                                    Width="95px"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Mto_Dsd_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Mto_Dsd">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Mto_Hta" runat="server" CssClass="clsTxt" Height="20px" TabIndex="1"
                                                                                                    Width="94px"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Mto_Hta_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Mto_Hta">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="150">
                                                                            <tr>
                                                                                <td class="Cabecera">
                                                                                    <asp:Label ID="Label20" runat="server" CssClass="SubTitulos" Text="Fecha Operación"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Contenido" align="center">
                                                                                    <table align="left" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Fec_Dsd" runat="server" CssClass="clsTxt" Height="20px"
                                                                                                    TabIndex="1" Width="76px"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Dsd_MaskedEditExtender" runat="server" 
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Dsd">
                                                                                                </cc2:MaskedEditExtender>
                                                                                                <cc2:CalendarExtender ID="Txt_Fec_Dsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                    Enabled="True" TargetControlID="Txt_Fec_Dsd" FirstDayOfWeek="Monday" 
                                                                                                    Format="dd-MM-yyyy">
                                                                                                </cc2:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_Fec_Hta" runat="server" CssClass="clsTxt" Height="20px"
                                                                                                    TabIndex="1" Width="76px"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Fec_Hta_MaskedEditExtender" runat="server" 
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                                    TargetControlID="Txt_Fec_Hta" Mask="99/99/9999" MaskType="Date">
                                                                                                </cc2:MaskedEditExtender>
                                                                                                <cc2:CalendarExtender ID="Txt_Fec_Hta_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                    Enabled="True" TargetControlID="Txt_Fec_Hta" FirstDayOfWeek="Monday" 
                                                                                                    Format="dd-MM-yyyy">
                                                                                                </cc2:CalendarExtender>
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
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:RadioButton ID="Rb_Est_Veri" runat="server" AutoPostBack="True" Checked="True"
                                                                            CssClass="Label" GroupName="EST" Text="Sin Est. Verificación" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="Dp_Est_Ver" runat="server" CssClass="clsTxt" Width="693px" AutoPostBack="True">
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
                                <td align="center" style="text-align:-moz-center">
                                    <br />
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label25" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                    top: 14px" Text="Doctos. Encontrados"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" height="250" valign="top" align="center">                                             
                                                    <asp:Panel ID="Panel_GV_Verificacion" runat="server" ScrollBars="None" width="600px" height="240px">                                                    
                                                            <asp:GridView ID="GV_Verificacion" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="formatUltcell">  <%--CellPadding="0" CssClass="formatUltcell" EnableTheming="True" HorizontalAlign="Center" ShowHeader="true"--%>
                                                                <FooterStyle BorderStyle="Dashed" Width="100%" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("RutDeu") %>' onclick="Img_Ver_Click" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:BoundField DataField="RutDeu" HeaderText="NIT Pagador">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="NomDeu" HeaderText="Razón Social">
                                                                        <ItemStyle HorizontalAlign="left" Width="300px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="contador" HeaderText="Cant. Doctos." HtmlEncode="False">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="suma" HeaderText="Monto" HtmlEncode="False">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                    </asp:Panel>
                                                <%--</div>--%>
                                            </td>
                                        </tr>
                                       <%-- 
                                          <tr>
                                            <td align="center">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" AlternateText="Anterior" />
                                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" AlternateText="Siguiente" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        --%>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="margin-left: 120px">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Buscar_out.gif"
                                        OnClick=" IB_Buscar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" ToolTip="Buscar"
                                        Style="margin-left: 0px" />
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Modificar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Modificar_Out.gif"
                                        OnClick=" IB_Modificar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Modificar_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Modificar_In.gif';" Enabled="False"
                                        ToolTip="Modificar Documento" />
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Ingreso" runat="server" ImageUrl="~/Imagenes/Botones/boton_Ingreso_Out.gif"
                                        OnClick=" IB_Ingreso_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Ingreso_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Ingreso_In.gif';" Enabled="False"
                                        ToolTip="Ingresar Verificacion" Visible="False" />
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Guardar_Cli" runat="server" ImageUrl="~/Imagenes/Botones/boton_Guardar_Cli_Out.gif"
                                        OnClick=" IB_Guardar_Cli_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Cli_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_Cli_In.gif';"
                                        Enabled="False" ToolTip="Guardar Cliente" />
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Limpiar_out.gif"
                                        OnClick=" IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';" ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LB_Buscar_Cli" runat="server" CausesValidation="false" OnClick="LB_Buscar_Click"></asp:LinkButton>
            <asp:HiddenField ID="Txt_Id_Deudor" runat="server" />
            <asp:HiddenField ID="Txt_Nom_Deudor" runat="server" />
            
        </ContentTemplate>
    
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Modificar" />
            <asp:PostBackTrigger ControlID="IB_Ingreso" />
        </Triggers>
        
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Cliente" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
            
</asp:Content>
