<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="AlertasCondiciones.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_AlertasCondiciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" class="Contenido">
                <tr>
                    <td style="height: 31px" class="Cabecera">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Alertas de Condiciones"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" align="center" height="560" valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%" class="Contenido">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center" valign="top">
                                    <table border="0" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td valign="top">
                                                <table id="tb_Ejecutivos" cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Ejecutivo"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                                            <asp:DropDownList ID="DP_Ejecutivos" runat="server" CssClass="clsMandatorio" Enabled="true"
                                                                Width="250px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:CheckBox ID="CB_Cliente" runat="server" CssClass="SubTitulos" Text="Por Cliente especifico"
                                                                AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                                                CssClass="clsDisabled" Width="90px" ReadOnly="true" ></asp:TextBox>
                                                                            <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                            </cc2:MaskedEditExtender>
                                                                            <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                                                CssClass="clsDisabled" Width="15px" ReadOnly="true" MaxLength="1" AutoPostBack="true"></asp:TextBox>
                                                                            <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                Width="20px" Enabled="False" />
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                                                Text="Razón Soc." Width="70px"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                                 ReadOnly="True" Style="position: static" Width="400px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:LinkButton ID="Lb_buscar" runat="server"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Estado Condición"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                                            <asp:DropDownList ID="DP_EstadoCondicion" runat="server" CssClass="clsMandatorio"
                                                                Width="100px">
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
                        
                        <br />
                        
                        <br />
                        <asp:Panel ID="Panel_Condiciones" runat="server" Width="988px" Height="390px" ScrollBars="Horizontal">
                            <asp:GridView ID="GV_Condiciones" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                            ShowHeader="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" 
                                            ToolTip='<%# Eval("id_cdn") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cdn_des" HeaderText="Descripción">
                                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_fec_com" HeaderText="Fecha Cump." DataFormatString="{0:d}">
                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_usr_ing" HeaderText="Usuario Ingresa">
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_usr_apb" HeaderText="Usuario Aprueba.">
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="operacion" HeaderText="Nº Operación">
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle  CssClass="cabeceraGrilla" />
                            <RowStyle CssClass="formatUltcell" />
                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                        </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 50px" align="right">
                        <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" AlternateText="Buscar Clientes"
                            ToolTip="Buscar Alertas"></asp:ImageButton>
                        <%--<asp:ImageButton ID="IB_Imprimir" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif" AlternateText="Buscar Clientes"
                            ToolTip="Imprimir Alertas" Enabled="False"></asp:ImageButton>--%>
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                            onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                            AlternateText="Limpiar" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>

