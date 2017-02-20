<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="MDeudores.aspx.vb" Inherits="_Deudores_MDeudores" Title="Mantención Pagadores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanelDeudores" runat="server">
        <ContentTemplate>
        
          <table style="width: 100%;" cellspacing="1" class="Contenido"  cellpadding="0" border="0">
                <tbody>
                   <tr>
                    <td  height="31" class="Cabecera">
                        &nbsp;
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Administración de Pagadores"></asp:Label>
                    </td>
                </tr>
                
                    <tr style="height: 580px">
                        <td  valign="top" align="center" class="Contenido" style="padding:5px">
                             <br />
                             <%--*********************************************************************************************--%>
                             <%--Criterio de Busqueda--%>
                             <table id="Table1" border="0" cellpadding="0"  cellspacing="0" width="740px">
                            
                                <tr>
                                    <td align="left" class="Cabecera">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="center" style="height: 80px" valign="middle">
                                        
                                        <table border="0"align="center"  cellpadding="0" cellspacing="0" width="98%">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label12" runat="server" __designer:wfdid="w285"  
                                                            CssClass="Label" Text="Identificación" Width="90px"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 224px">
                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" __designer:wfdid="w286" CssClass="clsTxt"
                                                             Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                        </cc1:MaskedEditExtender>
                                                      
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label4" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Nombre / Razón Social"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" __designer:wfdid="w289" 
                                                            CssClass="clsTxt" MaxLength="80" Style="position: static; margin-left: 28px;" 
                                                            Width="195px"></asp:TextBox>
                                                    </td>
                                                  
                                                </tr>
                                                <tr>
                                                    <td align="right"style="width:100px">
                                                        <asp:Label ID="Label7" Width="90px" runat="server" CssClass="Label" Text="Tipo Pagador"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 224px">
                                                        <asp:DropDownList ID="DP_TipoDeudor" runat="server" CssClass="clsTxt" 
                                                            Width="161px" AutoPostBack="True" Height="16px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right">
                                                        
                                                        <asp:Label ID="Label46" runat="server" CssClass="Label" Text="Segmentos"></asp:Label>
                                                        
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DP_Segmentos" runat="server" CssClass="clsTxt" 
                                                            Width="224px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    
                                                </tr>
                                                
                                         
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                             <br />
                             <%--*********************************************************************************************--%>
                             <%--Grilla Deudores--%>
                             <table border=0 cellpadding=0 cellspacing=0 >
                               
                             <tr>
                                <td align="left">
                                
                                    <asp:Panel ID="Panel_GrDeudor" runat="server" height="378px" ScrollBars="None">
                                        <asp:GridView ID="GrDeudor" runat="server" CssClass="formatUltcell" Width="100%"
                                            AutoGenerateColumns="False" AllowSorting="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="90px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("deu_ide") %>'
                                                            OnClick="Img_Ver_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                                    <ItemStyle HorizontalAlign="left" Width="350px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pnu_tip_deu_des" HeaderText="Tipo Pagador">
                                                    <ItemStyle HorizontalAlign="center" Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pnu_est_des" HeaderText="Estado" />
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                </asp:Panel>
                                   <%-- </div>--%>
                                    
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
                            </table>
                            
                             <%--*********************************************************************************************--%>
                                                    
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="height: 40px" valign="bottom" align="right">
                            
                            <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/boton_buscar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_buscar_out.gif';"
                                runat="server" 
                                ImageUrl="~/Imagenes/Botones/boton_buscar_out.gif" 
                                ToolTip="Buscar Pagadores"><%--FY 19-05-2012--%>
                            </asp:ImageButton>
                             
                            <asp:ImageButton ID="IB_Nuevo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                                OnClick="IB_Nuevo_Click" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" ToolTip="Nuevo Deudor">
                            </asp:ImageButton>
                            <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" AlternateText="Limpiar" />
                            
                        </td>
                    </tr>
                </tbody>
            </table>
           
          <asp:HiddenField ID="TxtNro" runat="server" />
          
     </ContentTemplate>
        
           
    </asp:UpdatePanel>
    <asp:LinkButton ID="LB_Rut" runat="server" Visible="false" Text="Identificación"></asp:LinkButton>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelDeudores">
     <ProgressTemplate>
        <uc1:Cargando ID="Cargando1" runat="server" /> 
     </ProgressTemplate>
</asp:UpdateProgress>
            
</asp:Content>
