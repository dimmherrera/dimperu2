<%@ Page Title="Simulación" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="simulation.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_simulation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript">
        function DenegaAcceso() {
            __doPostBack('ctl00$ContentPlaceHolder1$acceso', '');
            return;
        }
    </script>

    <script src="../Pizarras/FuncionesPrivadasJS/PizarraOperaciones.js"></script>
    <script src="../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td style="height: 31px" valign="middle" align="center" class="Cabecera">
                        <asp:Label ID="Label6" runat="server" CssClass="Titulos" __designer:wfdid="w119"
                            Text="Operaciones - Simulación de Operaciones"></asp:Label>
                    </td>
                </tr>
              
                <tr>
                    <td class="Contenido" valign="top" align="center">
                        <table>
                            <tr>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <%-- aqui va cliente --%>
                                                <table width="600px" cellspacing="1">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Proveedor"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td align="right" width="120px">
                                                                         <asp:Label ID="labelnitprov" runat="server" CssClass="Label" Text="Número Identificación"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <nobr>
                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" Width="90px" 
                                                            CssClass="clsDisabled"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" 
                                                            AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                            MaskType="Number" TargetControlID="Txt_Rut_Cli" AutoComplete="False" 
                                                            MessageValidatorTip="False">
                                                        </cc2:MaskedEditExtender>
                                                        </nobr>
                                                                    </td>
                                                                    <td>
                                                                        <nobr>
                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsTxt" MaxLength="1" 
                                                            Width="20px" AutoPostBack="True"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                            runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                            TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                        </cc2:FilteredTextBoxExtender>
                                </nobr>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Proveedor" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Style="margin-top: 0px" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" TabIndex="38" Width="250px" style="margin-left:55px" ></asp:TextBox>
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
                                        <tr>
                                            <td>
                                                <%-- DIgitadas o Simuladas --%>
                                                <table width="600px" cellspacing="1">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label25351" runat="server" CssClass="SubTitulos" Text="Nómina Operaciones a Simular"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="Rb_dig" runat="server" AutoPostBack="True" Checked="True" CssClass="Label"
                                                                            Enabled="False" GroupName="ops" OnCheckedChanged="Rb_dig_CheckedChanged" Text="Digitadas" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="Rb_Sim" runat="server" AutoPostBack="True" CssClass="Label"
                                                                            Enabled="False" GroupName="ops" OnCheckedChanged="Rb_Sim_CheckedChanged" Text="Simuladas" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btn_det_ope" runat="server" ImageUrl="~/Imagenes/btn_workspace/Detalle_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/Detalle_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Detalle_in.gif';"
                                                                            ToolTip="Detalle Operación" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <table cellpadding="0" cellspacing="0" width="500" border="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="Panel1" runat="server" CssClass="" Height="170px" ScrollBars="Horizontal">
                                                                                        <asp:GridView ID="Gr_Operaciones" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Seleccion" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    ItemStyle-Width="70px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_ope") %>'
                                                                                                          AlternateText='<%# Eval("id_opn") %>' OnClick="Img_Ver_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Docto.">
                                                                                                    <ItemStyle Width="150" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="opn_can_doc" HeaderText="# Doctos.">
                                                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="opn_mto_doc" DataFormatString="{0:c}" HeaderText="Monto Ope.">
                                                                                                    <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                                                                </asp:BoundField>
                                                                                               
                                                                                                <asp:TemplateField Visible="False">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="TIP_OPE" runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="Tipo Producto" >
                                                                                                    <ItemStyle Width="120px" HorizontalAlign="Right" />
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
                                                                                <td style="width: 100%" align="center">
                                                                                    <table>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="1" width="600px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label2222" runat="server" CssClass="SubTitulos" Text="Datos Simulación"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td width="120px" align="right">
                                                                        <asp:Label ID="Label25343" runat="server" CssClass="Label" Text="DTF T.A."></asp:Label>
                                                                    </td>
                                                                    <td width="110" align="left" >
                                                                        <asp:TextBox ID="Txt_Tasa_Base" runat="server" CssClass="clsDisabled" 
                                                                            MaxLength="5" Width="50px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right" width="90">
                                                                        <asp:Label ID="Label264" runat="server" CssClass="Label" Text="%Spread"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Spread" runat="server" CssClass="clsDisabled" MaxLength="5"
                                                                            Width="50px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label273" runat="server" CssClass="Label" Text="%Ptos"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Puntos" runat="server" MaxLength="5" Width="50px" CssClass="clsDisabled"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label283" runat="server" CssClass="Label" 
                                                                            Text="Descuento E.A."></asp:Label>
                                                                        </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Tnego" runat="server" CssClass="clsDisabled" 
                                                                            MaxLength="5" Width="50px"></asp:TextBox>
                                                                        </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label37" runat="server" CssClass="Label" Text="%Anticipo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Porc_Antic" runat="server" CssClass="clsDisabled" 
                                                                            MaxLength="5" Width="40px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
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
                                        <tr>
                                            <td>
                                                <table style="width: 600px" cellspacing="1">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label34" runat="server" CssClass="SubTitulos" Text="Comisión Flat"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td align="right" width="120px">
                                                                        <asp:Label ID="Label35" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                                    </td>
                                                                    <td  align="left" width="110">
                                                                        <asp:DropDownList ID="Dr_mone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Dr_mone_SelectedIndexChanged"
                                                                            Width="100px" CssClass="clsDisabled" Enabled="False">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                             
                                                                    <td width="90" align="right">
                                                                        <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Comisión"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Comi" runat="server" CssClass="clsDisabled" Width="120px"></asp:TextBox>
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
                                                <table style="width: 600px" cellspacing="1">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label295" runat="server" CssClass="SubTitulos" Text="Comisión x Docto."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td align="right" width="120px">
                                                                        <asp:Label ID="Label304" runat="server" CssClass="Label" Text="Moneda" ></asp:Label>
                                                                    </td>
                                                                    <td  align="left" width="110">
                                                                        <asp:DropDownList ID="Dr_Moneda" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                            Enabled="False" OnSelectedIndexChanged="Dr_Moneda_SelectedIndexChanged" Width="100px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" width="90">
                                                                        <asp:Label ID="Label31" runat="server" CssClass="Label" Text="%Com"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Porc_com" runat="server" CssClass="clsDisabled" MaxLength="5"
                                                                            Width="50px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="120px">
                                                                        <asp:Label ID="Label32" runat="server" CssClass="Label" Text="Minimo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Min" runat="server" CssClass="clsDisabled" Width="90px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label33" runat="server" CssClass="Label" Text="Maximo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Max" runat="server" CssClass="clsDisabled" Width="90px"></asp:TextBox>
                                                                        <asp:ImageButton ID="btn_calc" runat="server" Height="30px" ImageUrl="../../../Imagenes/Botones/calc.gif"
                                                                            OnClick="btn_calc_Click" ToolTip="Calcular Datos Simulación" Visible="False" />
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
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <%-- Datos Diarios --%>
                                                <table cellspacing="1" width="600px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label25350" runat="server" CssClass="SubTitulos" Text="Datos Diarios"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td width="107" align="right" >
                                                                        <asp:Label ID="Label5" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_FecSimulacion" runat="server" Width="90px" AutoPostBack="True"
                                                                            CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                        <%--<cc2:CalendarExtender ID="Txt_FecSimulacion_CalendarExtender" runat="server" Enabled="True"
                                                                            FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FecSimulacion"
                                                                            CssClass="radcalendar">
                                                                        </cc2:CalendarExtender>--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="US$"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="vdolar" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                            ReadOnly="True" Width="60px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="TMC"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="vtmc" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                            ReadOnly="True" Width="40px"></asp:TextBox>
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
                                                <%--Parametros de simulacion--%>
                                                <table cellspacing="1" width="600px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label8" runat="server" CssClass="SubTitulos" Text="Parámetros de Simulación"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                            
                                                                    <td align="right">
                                                                      
                                                                    </td>
                                                                    <td>
                                                                         <asp:RadioButtonList ID="Rb_mora" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                                                            Enabled="False">
                                                                            <asp:ListItem Selected="True" Value="M">Mensual</asp:ListItem>
                                                                            <asp:ListItem Value="A">Anual</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label25346" runat="server" CssClass="Label" Text="Tasa de Negocio"
                                                                            Width="110px" Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Negocio" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                            Height="19px" TabIndex="43" Width="40px" Visible="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="107">
                                                                        <asp:Label ID="Label25347" runat="server" CssClass="Label" Text="Comisión Flat" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Comflat" runat="server" CssClass="clsDisabled" TabIndex="44" Width="100px" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label25352" runat="server" CssClass="Label" Text="Descuentos" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dsctos" runat="server" CssClass="clsDisabled" MaxLength="50"
                                                                            TabIndex="44" Width="100px" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:ImageButton ID="btn_descto" runat="server" Enabled="False" Height="25px" ImageUrl="~/Imagenes/btn_workspace/descuento2_out.gif"
                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/descuento2_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/descuento2_in.gif';"
                                                                                        ToolTip="Descuentos" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label25358" runat="server" CssClass="Label" Text="Tasa Pago" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_tas_pag" runat="server" AutoPostBack="True" CssClass="clsMandatorio" Visible="false"
                                                                                        Height="19px" TabIndex="43" Width="40px"></asp:TextBox>
                                                                                    <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                        CultureTimePlaceholder="" Enabled="True" InputDirection="RightToLeft" Mask="99,99"
                                                                                        MaskType="Number" TargetControlID="Txt_tas_pag" AutoComplete="False" MessageValidatorTip="False">
                                                                                    </cc2:MaskedEditExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Antic" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                            TabIndex="44" Visible="False" Width="90px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label25357" runat="server" CssClass="Label" Text="Gastos" Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_GastImp" runat="server" CssClass="clsDisabled" TabIndex="44"
                                                                            Visible="False" Width="100px" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:ImageButton ID="btn_gast_imp" runat="server" Enabled="False" Height="25px" ImageUrl="~/Imagenes/btn_workspace/gasto_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/gasto_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/gasto_in.gif';"
                                                                            ToolTip="Gastos" TabIndex="1000" Visible="False" />
                                                                        <asp:Label ID="Label25349" runat="server" CssClass="Label" Text="Comisión Docto."
                                                                            Visible="False" Width="120px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_ComDocto" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                            TabIndex="44" Visible="False" Width="100px"></asp:TextBox>
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
                                                <%--Forma de pago--%>
                                                <table width="600px" cellspacing="1">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label25355" runat="server" CssClass="SubTitulos" Text="Forma de Pago"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td align="right" width="107">
                                                                        <asp:Label ID="Label25345" runat="server" CssClass="Label" Text="F. de Pago"></asp:Label>
                                                                    </td>
                                                                    <td  align="left" width="195">
                                                                        <asp:DropDownList ID="Dr_For_Pgo" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                            Width="184px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" width="100">
                                                                        <asp:Label ID="Label39" runat="server" CssClass="Label" Text="Banco"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Dr_Bco" runat="server" Width="120px" CssClass="clsDisabled">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label40" runat="server" CssClass="Label" Text="Nº Cuenta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Cta_Cte" runat="server" Width="100px" CssClass="clsDisabled"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100" align="right">
                                                                        <asp:Label ID="Label25356" runat="server" CssClass="Label" Text="Ant.14 hrs" Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div width="30"></div>
                                                                        <asp:CheckBox ID="Ch_Ats_14hrs" runat="server" CssClass="Label" Visible="False"/>
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
                                                <%--Detalle Simulacion--%>
                                                <table width="600px" cellspacing="1">
                                                    <tbody>
                                                        <tr>
                                                            <td class="Cabecera" align="center">
                                                                <asp:Label ID="Label17" runat="server" CssClass="SubTitulos" Text="Detalle Simulación"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido">
                                                                <table>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="MontoDoctos" runat="server" CssClass="Label" __designer:wfdid="w180"
                                                                                Text="Monto Doctos."></asp:Label>
                                                                        </td>
                                                                        <td align="left" width="195">
                                                                            <asp:TextBox ID="txt_montos_doctos" runat="server" CssClass="clsDisabled" __designer:wfdid="w181"
                                                                                Width="100px" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right" width="100">
                                                                            <asp:Label ID="Label23" runat="server" CssClass="Label" __designer:wfdid="w182" 
                                                                                Text="Comisión Variable"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Com_x_dcto" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w183" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="sd" runat="server" CssClass="Label" __designer:wfdid="w184" Text="Base Negociacion"
                                                                                Width="107px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:TextBox ID="Txt_Monto_anticipar" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w185" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label24" runat="server" CssClass="Label" __designer:wfdid="w186" 
                                                                                Text="Comisión Fija"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Com_esp" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w187" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" __designer:wfdid="w188" 
                                                                                Text="Descuento"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:TextBox ID="Txt_dif_precio" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w189" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label25360" runat="server" __designer:wfdid="w194" 
                                                                                CssClass="Label" Text="Gastos Afectos" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_GastosAfectos" runat="server" __designer:wfdid="w195" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" __designer:wfdid="w192" Text="Precio de Compra"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px;" align="left">
                                                                            <asp:TextBox ID="Txt_Precio_compra" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w193" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label41" runat="server" __designer:wfdid="w190" CssClass="Label" 
                                                                                Text="IVA" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Iva" runat="server" __designer:wfdid="w191" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label20" runat="server" CssClass="Label" __designer:wfdid="w196" 
                                                                                Text="Reserva"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:TextBox ID="Txt_Saldo_pendiente" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w197" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label42" runat="server" __designer:wfdid="w194" CssClass="Label" 
                                                                                Text="Gastos Exentos" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Gastos" runat="server" __designer:wfdid="w195" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label22" runat="server" CssClass="Label" __designer:wfdid="w200" 
                                                                                Text="Saldo Pagar"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:TextBox ID="Txt_Saldo_pagar" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                                __designer:wfdid="w201" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label43" runat="server" __designer:wfdid="w198" CssClass="Label" 
                                                                                ReadOnly="True" Text="Impuestos" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Impuestos" runat="server" __designer:wfdid="w199" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label44" runat="server" __designer:wfdid="w202" CssClass="Label" 
                                                                                Text="Aplicación Deuda" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_DeScuentos" runat="server" __designer:wfdid="w203" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label25359" runat="server" __designer:wfdid="w200" 
                                                                                CssClass="Label" Text="GMF" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Valor_GMF" runat="server" __designer:wfdid="w201" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label16" runat="server" __designer:wfdid="w204" CssClass="Label" 
                                                                                Font-Bold="True" Text="Total Desembolso"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_saldo_total" runat="server" __designer:wfdid="w205" 
                                                                                CssClass="clsDisabled" ReadOnly="True" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="height:60px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Ch_Op_Ptal" runat="server" Enabled="False" Text="Ope.Puntual" Font-Size="X-Small"
                                                    CssClass="Label" Visible="False" />
                                                
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
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
                                    <asp:ImageButton ID="Btn_Buscar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        ToolTip="Buscar" />
                                    <asp:HiddenField ID="privez" runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_guardar" runat="server" __designer:wfdid="w206" ImageUrl="../../../Imagenes/Botones/boton_guardar_out.gif"
                                        OnClick="btn_guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" TabIndex="26"
                                        ToolTip="Guardar Simulación" Enabled="false" />
                                    <cc2:ConfirmButtonExtender ID="btn_guardar_ConfirmButtonExtender" 
                                        runat="server" ConfirmText="¿Esta Seguro de Simular la Operación?" 
                                        Enabled="True" TargetControlID="btn_guardar">
                                    </cc2:ConfirmButtonExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_Anu_Sim" runat="server" ImageUrl="../../../Imagenes/Botones/boton_anu_sim_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_anu_sim_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_anu_simu_in.gif';"
                                        OnClick="Btn_Anu_Sim_Click" ToolTip="Anular Simulación" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_Imp_Sim" runat="server" ImageUrl="../../../Imagenes/Botones/boton_imp_simu_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_imp_simu_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imp_simu_in.gif';"
                                        ToolTip="Imprimir" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Imp_Arc" runat="server" ImageUrl="../../../Imagenes/Botones/boton_Documentos_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_Documentos_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_Documentos_in.gif';"
                                        ToolTip="Archivos a Imprimir" Enabled="False" Visible="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Btn_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        OnClick="Btn_Limpiar_Click" ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:TextBox ID="Txt_ItemOPE" TabIndex="65" runat="server" Width="0px" BorderStyle="None" BackColor="Transparent" ForeColor="Transparent" Height="0px"></asp:TextBox>
            
            <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" __designer:wfdid="w170"
                PopupControlID="Panel5" EnableViewState="False"
                BackgroundCssClass="modalBackground" TargetControlID="Lb_det_ope" OkControlID="ok">
            </cc2:ModalPopupExtender>
            
            <asp:Panel ID="Panel5" runat="server" __designer:wfdid="w173" Width="750px" Height="500px"
                Style="display: none">
                <table style="width: 100%" class="Contenido">
                    <tbody>
                        <tr>
                            <td class="Content" align="center">
                                <asp:Label ID="lbl_titulo" runat="server" CssClass="Titulos" __designer:wfdid="w174"
                                    Width="200px">Detalle Operación</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" CssClass="Contenido" __designer:wfdid="w175"
                                    Width="100%" ScrollBars="Vertical" Height="600px">
                                    <asp:GridView ID="Gr_Documentos" runat="server" __designer:wfdid="w176" AutoGenerateColumns="False"
                                        CssClass="formatUltcell" PageSize="1">
                                        <Columns>
                                            <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dsi_num" HeaderText="Nro.Documento">
                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dsi_flj_num" HeaderText="Nro.Cuota">
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dsi_mto_fin" DataFormatString="{0:###,###,###.00}" HeaderText="Monto Financiado">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dsi_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto"
                                                HtmlEncode="False" ItemStyle-Width="90px" />
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
                                <asp:ImageButton ID="ok" runat="server" ImageUrl="../../../Imagenes/Botones/boton_aceptar_out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/boton_aceptar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_aceptar_in.gif';"
                                    __designer:wfdid="w177" BorderColor="Black"></asp:ImageButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            
            <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" __designer:wfdid="w209"
                PopupDragHandleControlID="PanelSimulacion" PopupControlID="PanelSimulacion" BackgroundCssClass="modalBackground"
                TargetControlID="Lb_mod_simula">
            </cc2:ModalPopupExtender>
                        
            <asp:Panel ID="PanelSimulacion" runat="server" CssClass="Contenido" Style="display: none"
                __designer:wfdid="w178">
                <asp:ImageButton ID="Btn_Ing_pag" runat="server" ImageUrl="../../../Imagenes/Botones/boton_ing_pagare_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/boton_ing_pagare_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_ing_pagare_in.gif';"
                    Visible="False" />
                <asp:ImageButton ID="Btn_negoc" runat="server" ImageUrl="~/Imagenes/Botones/negociacion_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/negociacion_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/negociacion_in.gif';"
                    Visible="False" />
                <asp:ImageButton ID="Btn_Ope" runat="server" ImageUrl="../../../Imagenes/Botones/boton_ope_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/boton_ope_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_ope_in.gif';"
                    Enabled="False" ToolTip="Pizarra Operaciones" Visible="False" />
                <asp:ImageButton ID="Btn_Asoc" runat="server" ImageUrl="../../../Imagenes/Botones/boton_asoc_egre_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/boton_asoc_egre_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_asoc_egre_in.gif';"
                    ToolTip="Asociar Cheque" Visible="False" />
            </asp:Panel>
            
            <asp:LinkButton ID="Lb_aux" TabIndex="51" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Lb_det_ope" TabIndex="52" runat="server" ></asp:LinkButton>
            <asp:LinkButton ID="Lb_buscar" TabIndex="54" OnClick="Lb_buscar_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Lb_mod_simula" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_CargaOpe" OnClick="LB_CargaOpe_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="modalsim" OnClick="LinkButton1_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lb_desctos" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_RefrescaGastos" runat="server" OnClick="LB_RefrescaGastos_Click"></asp:LinkButton>
            <asp:HiddenField ID="uf" runat="server" />
            <asp:HiddenField ID="dolar" runat="server" />
            <asp:HiddenField ID="tmc" runat="server" />
            <asp:HiddenField ID="num_ope" runat="server" />
            <asp:HiddenField ID="ope_est" runat="server" />
            <asp:HiddenField ID="HF_Mto" runat="server" />
            <asp:HiddenField ID="HF_Imp" runat="server" />
            <asp:HiddenField ID="HF_NroNeg" runat="server" />
            <asp:HiddenField ID="HF_N_Ope" runat="server" />
            <asp:HiddenField ID="HF_N_Opn" runat="server" />
            <asp:HiddenField ID="HF_Pos" runat="server" />
            <asp:HiddenField ID="HF_TOp" runat="server" />
            <asp:HiddenField ID="HF_Moneda" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_guardar" />
            <asp:PostBackTrigger ControlID="btn_imp_sim" />
            <asp:PostBackTrigger ControlID="btn_gast_imp" />
            <asp:PostBackTrigger ControlID="IB_Imp_Arc" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:LinkButton ID="acceso" runat="server"></asp:LinkButton>
    
    <asp:LinkButton ID="lb_anu" TabIndex="51" runat="server" __designer:wfdid="w166"></asp:LinkButton>
    
</asp:Content>
