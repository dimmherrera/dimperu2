<%@ Page Language="VB" ValidateRequest="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="MClientes.aspx.vb" Inherits="MClientes" Title="Mantención de Clientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:UpdatePanel runat="server" ID="Updatepanel_clientes">
    
        <ContentTemplate>
            
            <table cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
            
                <tr>
                    <td height="31" class="Cabecera">
                        &nbsp;
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Administración de Proveedor"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" 
                        style="padding: 10px; Height:453px; text-align:-moz-center" align="center" 
                        valign="top">
                     <br />   
                            <table  border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <%--Criterio de Busqueda--%>
                                            
                                            <table id="Table1" border="0" cellpadding="0"cellspacing="0" width="100%">
                                                        <tr>
                                                            <td class="Cabecera" align="left">
                                                                <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" 
                                                                    Text="Criterio de Búsqueda"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" align="center" style="height: 80px" valign="middle">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label12" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                                                    Text="Número Identificación Proveedor"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 350px">
                                                                                <asp:TextBox ID="Txt_Rut" runat="server" __designer:wfdid="w286" 
                                                                                    CssClass="clsTxt" Style="position: static" TabIndex="1" 
                                                                                    Width="150px"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" 
                                                                                    AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                                    MaskType="Number" TargetControlID="Txt_Rut">
                                                                                </cc1:MaskedEditExtender>
                                                                            </td>
                                                                            <td align="right" style="width: 180px">
                                                                                <asp:Label ID="Label4" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                                                    Text="Nombre / Razón Social Proveedor"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="Txt_Nom" runat="server" __designer:wfdid="w289" 
                                                                                    CssClass="clsTxt" MaxLength="50" Style="position: static" Width="262px" TabIndex="2"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Tipo Proveedor"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 307px">
                                                                                <asp:DropDownList ID="DP_TipoCli" runat="server" CssClass="clsTxt" 
                                                                                    Width="300px" AutoPostBack="True" TabIndex="3">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td align="right" style="width: 53px">
                                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Ejecutivo"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DP_Ejecutivo" runat="server" CssClass="clsTxt" 
                                                                                    Width="200px" TabIndex="4">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                            </table>
                                         
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center">
                                            <br />
                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 720px">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                           <asp:Panel ID="Panel1" runat="server" __designer:wfdid="w46" 
                                                                 Height="375px"  Width="100%">
                                                                <asp:GridView ID="GV_Clientes" runat="server" __designer:wfdid="w47" 
                                                                    AutoGenerateColumns="False" CellPadding="3" CssClass="formatUltcell" 
                                                                    EnableTheming="True" HorizontalAlign="left" Style="position: static" 
                                                                    Width="100%" >
                                                                    <Columns>
                                                                 <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/images/bt_ver.gif" 
                                                                                ToolTip='<%# Eval("cli_idc") %>' onclick="Img_Ver_Click" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                        <asp:BoundField DataField="cli_idc" HeaderText="Identificación">
                                                                            <FooterStyle HorizontalAlign="Left" />
                                                                            <ControlStyle Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cli_rso" HeaderText="Razón Social">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PNU_CLI_TIP_DES" HeaderText="Tipo de Proveedor" />
                                                                        <asp:BoundField DataField="PNU_CLI_EST_DES" HeaderText="Estado" />
                                                                        
                                                                   
                                                                    </Columns>
                                                                    <FooterStyle BorderStyle="Dashed" />
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
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" align="right" height="50">
                    
                                        <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                                            runat="server" ImageUrl="~/Imagenes/Botones/boton_buscar_out.gif" 
                                            BorderStyle="None" ToolTip="Buscar Clientes" TabIndex="5">
                                        </asp:ImageButton>
                                        
                                      <%-- <asp:ImageButton ID="IB_Detalle" onmouseover="this.src='../../../Imagenes/Botones/Boton_Detalle_in.gif';"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Detalle_out.gif';" OnClick="IB_Detalle_Click"
                                            runat="server" ImageUrl="~/Imagenes/Botones/boton_detalle_out.gif" 
                                            BorderStyle="None" Enabled="False" ToolTip="Ver Detalle">
                                        </asp:ImageButton> --%>
                    
                                        <asp:ImageButton ID="IB_Nuevo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" OnClick="IB_Nuevo_Click"
                                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" 
                                            ToolTip="Nuevo Proveedor" TabIndex="6"></asp:ImageButton>
                                            
                                        <asp:ImageButton ID="IB_Limpiar" AlternateText="Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" TabIndex="7" />
                    </td>
                </tr>
                
            </table>
            
            <asp:HiddenField ID="Posicion" runat="server" />
            <asp:HiddenField ID="Txt_Orden" runat="server" />
            
        </ContentTemplate>
        
     </asp:UpdatePanel>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel_clientes">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
</asp:Content>
