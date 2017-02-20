<%@ Page Title="Mantención de Negociaciones" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Negociacion.aspx.vb" Inherits="FrmNegociacion" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../FuncionesPrivadasJS/Negociación.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Negociación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 10px; Height:590px; text-align:-moz-center" align="center" valign="top">
                            <%--Cliente--%>
                            <table id="cliente_diarios" border="0" style="height: 100px">
                                <tr>
                                    
                                    <td valign="top">
                                        <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="Cabecera" align="left">
                                                        <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label1" runat="server"
                                                            CssClass="SubTitulos" Text="Cliente" __designer:wfdid="w284"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" valign="top">
                                                        <table cellspacing="2" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                                            CssClass="clsMandatorio" Width="90px" __designer:wfdid="w286" ></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="None"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc2:MaskedEditExtender>
                                                                        <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="2" runat="server"
                                                                            CssClass="clsMandatorio" Width="15px" __designer:wfdid="w287" MaxLength="1" OnKeypress_Script="fnTrapKD(ctl00_ContentPlaceHolder1_Lb_buscar);"
                                                                            AutoPostBack="True"></asp:TextBox>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Style="margin-top: 0px" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label41" runat="server" CssClass="Label" Text="Tipo de Cliente"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_TipoCliente" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Razón Soc."></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled"  ReadOnly="True" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Sucursal" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            Width="180px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label42" runat="server" CssClass="Label" Text="Banca"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Banca" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                            Width="180px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Ejecutivo"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="300px"></asp:TextBox>
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
                            
                            
                                
                            <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="70%">
                                <tbody>
                                    <tr>
                                        <td align="left" class="Cabecera">
                                            <asp:Label ID="Label20" runat="server" Text="Listado de Negociaciones" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" style="height: 150px" valign="top" align="left">
                                            <table id="Table4" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td Height="350px" valign="top" >
                                                        
                                                            <asp:GridView ID="GV_Negociacion" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("NroNeg") %>' OnClick="Img_Ver_Click"/>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Nro." DataField="NroNeg">
                                                                        <ItemStyle Width="50px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TipDoc" HeaderText="Tipo Docto.">
                                                                        <ItemStyle Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Fecha Neg." DataField="FechaNeg">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="% Anticipo" DataField="PorAnt">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Moneda" DataField="Moneda">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CanDeu" HeaderText="# Pagadores">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="MtoNeg" HeaderText="Monto Doctos.">
                                                                        <ItemStyle Width="110px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="# Doctos." DataField="CanDoc">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Fecha Vcto." DataField="FechaVctoReal">
                                                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Estado Neg." DataField="Estado">
                                                                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%">
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
                                </tbody>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle">
                        <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="IB_Buscar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" __designer:wfdid="w375"
                            ValidationGroup="Cliente" ToolTip="Buscar Negociación"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Nuevo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" AlternateText="Buscar Clientes"
                            Enabled="False" ToolTip="Nueva Negociación"></asp:ImageButton>
                            
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" ToolTip="Limpiar Pantalla">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="Lb_buscar" runat="server" OnClick="Lb_buscar_Click" ValidationGroup="Cliente"></asp:LinkButton>
            <asp:HiddenField ID="HF_Nro_Neg" runat="server" />
        </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="IB_Nuevo" />
        <asp:PostBackTrigger ControlID="GV_Negociacion" />
    </Triggers>
    
  </asp:UpdatePanel>
  
    <asp:LinkButton ID="LB_Refescar" runat="server" OnClick="LB_Refescar_Click" ></asp:LinkButton>
     
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                        <uc1:Cargando ID="Cargando1" runat="server" />
                </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>


