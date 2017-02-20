<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="Evaluacion.aspx.vb" Inherits="_Default" Title="Evaluación Cliente / Pagador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
         
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
         
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updatepanel">
    
        <ContentTemplate>
        
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Evaluación Cliente / Pagador"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 10px; height: 590px; text-align: -moz-center" align="center" valign="top">
                        <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0" width="81%"> 
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top" style="height: 50px">
                                    <table cellspacing="2" cellpadding="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                    CssClass="clsMandatorio" Width="90px" ></asp:TextBox>
                                                <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                </cc2:MaskedEditExtender>
                                                <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                    CssClass="clsMandatorio" Width="15px" MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                </cc2:FilteredTextBoxExtender>
                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                    Width="20px" Style="margin-top: 0px" />
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label41" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Tipo de Cliente"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_TipoCliente" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="300px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Razón Soc."></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                    ReadOnly="True" Style="position: static" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label15" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Sucursal"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Sucursal" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="180px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label42" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Banca"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Banca" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="180px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Ejecutivo"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="65%">
                            <tr>
                                <td align="left" class="Cabecera">
                                    <asp:Label ID="Label20" runat="server" Text="Listado de Evaluaciones" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="410px" class="Contenido" valign="top" align="center">
                                    <asp:GridView ID="GV_Evaluaciones" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                        ToolTip='<%# Eval("Codigo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Nº Neg." DataField="Codigo">
                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Moneda" DataField="Moneda">
                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="% Anticipo" DataField="Porcentaje">
                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Monto" HeaderText="Mto. Evaluado" DataFormatString="{0:###,###,###.00}">
                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Deudores" HeaderText="#  Pagadores">
                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Estado Neg." DataField="Estado">
                                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                        <RowStyle CssClass="formatUltcell" />
                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 2px 0 0 0;">
                                    <table border="0" cellpadding="0" cellspacing="0">
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
                <tr>
                    <td align="right">
                        <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="IB_Buscar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" ValidationGroup="Cliente"
                            ToolTip="Buscar Evaluaciones"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Nuevo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" OnClick="IB_Nuevo_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" ToolTip="Nueva Evaluación"
                            Enabled="False"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" ToolTip="Limpiar Pantalla">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
        
            <asp:HiddenField ID="HF_Nro_Eva" runat="server" />
            <asp:HiddenField ID="HF_Posicion" runat="server" />
            <asp:LinkButton ID="LB_Refescar" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Lb_buscar" runat="server" Style="position: static" TabIndex="54" ValidationGroup="Cliente"></asp:LinkButton>
        </ContentTemplate>
        
    </asp:UpdatePanel>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
</asp:Content>
