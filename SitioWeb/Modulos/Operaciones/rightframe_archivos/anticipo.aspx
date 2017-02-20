<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="anticipo.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_anticipo"
    Title="Abono Anticipo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    
            <table width="100%" cellpadding="0" cellspacing="1" class="Contenido">
                <tbody>
                    <tr>
                        <td align="center" style="height: 31px" class="Cabecera">
                            <asp:Label ID="Label17" runat="server" CssClass="Titulos">Operaciones - Abono Anticipo</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="center">
                            <table cellspacing="0" width="680">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                        Width="20px" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                        Width="20px" Style="margin-top: 0px" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                        Width="350px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table cellspacing="0" width="680">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="MontoDoctos0" runat="server" CssClass="SubTitulos" 
                                            Text="Cuenta por Cobrar"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="MontoDoctos1" runat="server" CssClass="Label" Text="Tipo Moneda"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 235px">
                                                    <asp:DropDownList ID="Dr_MON" runat="server" CssClass="clsDisabled" Enabled="False"
                                                        Width="200px" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Monto"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_mto" runat="server" CssClass="clsDisabled" Width="120px" ReadOnly="true"
                                                        AutoPostBack="true"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_mto">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="MontoDoctos" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                                </td>
                                                <td colspan="3" align="left">
                                                    <asp:TextBox ID="txt_des" runat="server" CssClass="clsDisabled" Width="447px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="MontoDoctos2" runat="server" CssClass="Label" Text="Contrato"></asp:Label>
                                                </td>
                                                <td align="left" colspan="1">
                                                    <asp:TextBox ID="txt_Contrato" runat="server" CssClass="clsDisabled" Width="150px"></asp:TextBox>
                                                    <asp:ImageButton ID="IB_AyudaDoc" runat="server" AlternateText="Ayuda Documentos"
                                                        ImageUrl="../../../Imagenes/Iconos/155.ICO" Style="margin-top: 0px" Width="20px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" CssClass="Label" Text="N° Docto."></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_nro_doc" runat="server" CssClass="clsDisabled" 
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table cellspacing="0" width="680">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label18" runat="server" CssClass="SubTitulos" Text="Egreso Asociado"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Tipo Desembolso"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="Dr_For_Pgo" runat="server" CssClass="clsDisabled" Enabled="False"
                                                        Width="200px" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="at_14" runat="server" CssClass="Label" Enabled="False" Text="Antes 14 Hrs." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Banco"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="Dr_Bco" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                        Enabled="False" Width="300px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Cta Corriente" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_Cta_cte" runat="server" CssClass="clsDisabled" Enabled="False"
                                                        ReadOnly="True" Width="200px" Visible="False" MaxLength="15"></asp:TextBox>
                                                    <nobr __designer:dtid="4503599627370504">
                                                      <asp:HiddenField ID="Txt_ItemOPE" runat="server" />
                                                      </nobr>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table style="width: 681px" cellpadding="0">
                                <tr>
                                    <td align="center" class="Contenido">
                                        <asp:Panel ID="Panel2" runat="server" Height="280px" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="Gr_popant" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                Width="100%" ShowHeader="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("ID_CXC") %>'
                                                                OnClick="Img_Ver_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="pnu_DES" HeaderText="Tipo de Cuenta">
                                                        <ItemStyle Width="160px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ID_CXC" HeaderText="Nº Cuenta">
                                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cxc_des" HeaderText="Descripción">
                                                        <ItemStyle Width="220px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
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
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="Btn_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" OnClick="Btn_Buscar_Click" 
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                                onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                                ToolTip="Buscar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                                OnClick="btn_guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_IN.gif';" TabIndex="26"
                                                ToolTip="Guardar" Enabled="False" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_eli" runat="server" Enabled="False" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                                                OnClick="btn_eli_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';" ToolTip="Eliminar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="Btn_Limpia" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                                OnClick="Btn_Limpia_Click" onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" TabIndex="25"
                                                ToolTip="Limpiar" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            
            <asp:LinkButton ID="buscar" runat="server" ForeColor="#6666FF" OnClick="buscar_Click"></asp:LinkButton>
            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lb_aux" runat="server" ForeColor="Black"></asp:LinkButton>
            <asp:LinkButton ID="RetornaDoctos" runat="server" ForeColor="#6699FF"></asp:LinkButton>
            
            <uc1:Mensaje ID="Mensaje2" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
