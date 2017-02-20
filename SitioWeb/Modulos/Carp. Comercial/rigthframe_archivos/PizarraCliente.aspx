<%@ Page Language="VB" ValidateRequest="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="PizarraCliente.aspx.vb" Inherits="PizzarraCliente" Title="Pizarra de Clientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:UpdatePanel runat="server" ID="Updatepanel_clientes">
    
        <ContentTemplate>
            
            <table width="100%" cellspacing="1" cellpadding="0" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" height="31"> 
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Control Dual - Pizarra Aprobacion de clientes"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px; Height:590px; text-align:-moz-center" align="center" valign="top">
                      <%--Criterio de Busqueda--%>
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" style="height: 50px" valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label12" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                                                Text="Identificación"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Rut" runat="server" __designer:wfdid="w286" 
                                                                                CssClass="clsTxt"  Style="position: static" TabIndex="1" 
                                                                                Width="90px"></asp:TextBox>
                                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" 
                                                                                AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                                MaskType="Number" TargetControlID="Txt_Rut">
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label4" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                                                Text="Razón Social"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Nom" runat="server" __designer:wfdid="w289" 
                                                                                CssClass="clsTxt" MaxLength="50" Style="position: static" Width="300px"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Tipo Cliente"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 189px">
                                                                            <asp:DropDownList ID="DP_TipoCli" runat="server" CssClass="clsTxt" 
                                                                                Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Ejecutivo"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DP_Ejecutivo" runat="server" CssClass="clsTxt" 
                                                                                Width="200px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            
                        <br />  
                        <%--Grilla--%>                                            
                        <table cellspacing="0" cellpadding="0" border="0" style="width: 720px">
                                                <tbody>
                                                <tr>
                                                    <td align="left" class="Cabecera">
                                                            <asp:Label ID="Label20" runat="server" Text="Listado de Clientes" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" style="height: 340px" valign="top" align="left">
                                                               <asp:GridView ID="GV_Clientes" runat="server" __designer:wfdid="w47" 
                                                                   AutoGenerateColumns="False" CellPadding="3" CssClass="formatUltcell" 
                                                                   EnableTheming="True" HorizontalAlign="Left" Style="position: static" 
                                                                   Width="96%">
                                                                   <Columns>
                                                                       <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
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
                                                                       <asp:BoundField DataField="PNU_CLI_TIP_DES" HeaderText="Tipo de Cliente" ItemStyle-HorizontalAlign="Center" />
                                                                       <asp:BoundField DataField="PNU_CLI_EST_DES" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
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
                <tr>
                    <td valign="bottom" align="right">
                    
                                        <asp:ImageButton ID="IB_Buscar" runat="server" BorderStyle="None" 
                                            ImageUrl="~/Imagenes/Botones/boton_buscar_out.gif" 
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                            onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" 
                                            ToolTip="Buscar Clientes" />
                    
                                        <asp:ImageButton ID="IB_Aprobar" runat="server" __designer:wfdid="w375" 
                                            AlternateText="Dar Visto Bueno a Vliente" 
                                            ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" OnClick="IB_Aprobar_Click" 
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" 
                                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';" 
                                            Style="position: static" />
                    
                                        <asp:ImageButton ID="IB_Rechazar" runat="server" 
                                            ImageUrl="~/Imagenes/Botones/boton_rechazar_out.gif" />
                                        <asp:ImageButton ID="ImageButton4" runat="server" 
                                            ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" />
                                        <br />
                    </td>
                </tr>
                
            </table>
            
            <asp:HiddenField ID="Posicion" runat="server" />
            <asp:HiddenField ID="Txt_Orden" runat="server" />
          
            <asp:HiddenField ID="SW" runat="server" />
            
        </ContentTemplate>
        
     </asp:UpdatePanel>
      <asp:LinkButton ID="LB_Aprobar" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Rechazar" runat="server"></asp:LinkButton>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel_clientes">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
</asp:Content>
