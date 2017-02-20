<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Asig_Ejecutivo.aspx.vb" Inherits="ClsAsigEje" Title="Asignación de Ejecutivos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server" ID="UP_AsigCliente">
        <ContentTemplate>
        
            <table cellspacing="1" cellpadding="0" width="100%" class="Contenido">
                <tbody>
                    <tr>
                        <td style="height: 31px" class = "Cabecera">
                            <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Reasignación de Ejecutivos de Cuentas"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="center" valign="top">
                            <table cellspacing="0" cellpadding="0" width="600px" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" 
                                                Text="Cliente a Buscar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="left">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" width="180">
                                                            <asp:CheckBox ID="CB_Eje" runat="server" CssClass="Label" AutoPostBack="true"
                                                                Text="Ejecutivo de Cuentas"></asp:CheckBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="Dp_Ejecutivos" runat="server" CssClass="clsDisabled" Width="232px"
                                                                Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:CheckBox ID="CB_Cli" runat="server" CssClass="Label" AutoPostBack="true"
                                                                Text="Clientes"></asp:CheckBox>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" Width="90px" ></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="False" 
                                                                ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                            </cc2:MaskedEditExtender>
                                                            <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" Width="15px" MaxLength="1"
                                                             AutoPostBack="true"></asp:TextBox>
                                                            <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                            </cc2:FilteredTextBoxExtender>
                                                        
                                                            <asp:ImageButton ID="IB_AyudaCli" runat="server"  AlternateText="Ayuda Clientes"
                                                             ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" Enabled="false"/>
                                                            </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                                 ReadOnly="True" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" width="700px" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Cliente a Reasignar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="center" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" width="600">
                                             
                                                <tr>
                                                    <td style="height:200px" valign="top">     
                                                        
                                                            <asp:GridView ID="GV_Clientes" runat="server" AutoGenerateColumns="False" CellPadding="1" 
                                                            CssClass="formatUltcell" ShowHeader="true">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-HorizontalAlign ="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:ImageButton ID="IB_Seleccionar" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                                             AlternateText="Seleccionar todos" OnClick="IB_Seleccionar_Click" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CB_Seleccionar" runat="server"  />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="cli_idc" HeaderText="Identificación">
                                                                       <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cli_rso" HeaderText="Razón Social">
                                                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Deudores" HeaderText="#Pagadores" 
                                                                        ItemStyle-Width="70px" ><%--FY 19-05-2012--%>
                                                                        <ItemStyle Width="70px" HorizontalAlign="Right"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Doctos" HeaderText="#Doctos." ItemStyle-Width="70px" >
                                                                        <ItemStyle Width="70px" HorizontalAlign="Right"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cli_fec_act_eje" Visible="False">
                                                                        <ItemStyle Width="0px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="eje_cod_eje" Visible="False">
                                                                        <ItemStyle Width="0px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
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
                                </tbody>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" width="600px" border="0" style="height:180px">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Ejecutivo a Reasignar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="center" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="height:100px" valign="top">
                                                    
                                                        <asp:GridView ID="GV_Ejecutivos" runat="server" CssClass="formatUltcell" ShowHeader="True"
                                                            HorizontalAlign="Center" EnableTheming="True" CellPadding="1" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" OnClick="Button1_Click" ToolTip='<%# Eval("Codigo") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Codigo" HeaderText="Código">
                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                    <ControlStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Ejecutivo de Cuentas">
                                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="sumaCli" HeaderText="#Pagadores"><%--FY 19-05-2012--%>
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="sumaDoc" HeaderText="#Doctos.">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="IB_Prev_GV_Ejecutivos" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                        <asp:ImageButton ID="IB_Next_GV_Ejecutivos" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="IB_Buscar_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" AlternateText="Buscar Clientes">
                            </asp:ImageButton>
                            
                            <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="IB_Guardar_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" AlternateText="Guardar Datos">
                            </asp:ImageButton>
                            <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" 
                                ImageUrl="~/Imagenes/Botones/Boton_limpiar_out.gif"  
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_limpiar_in.gif';" />
                        </td>
                    </tr>
                </tbody>
            </table>
            
            <asp:CheckBox ID="CB_Sel" runat="server" Visible="False"></asp:CheckBox>
            <asp:HiddenField ID="TxtCodEje" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
</asp:Content>
