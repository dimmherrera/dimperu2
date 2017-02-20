<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Asig_Cobrador.aspx.vb" Inherits="ClsAsigEjeCob" Title="Cobradores Telefonicos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server" ID="UP_AsigCliente">
        <ContentTemplate>

            <table cellspacing="1" cellpadding="0" width="100%" class="Contenido">
                <tbody>
                    <tr>
                        <td style="height: 31px" class = "Cabecera">
                            <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Cobranza - Asignación de Cobradores Telefónicos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="center" height="590" valign="top" style="padding: 5px">
                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="left">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:CheckBox ID="CB_Eje" runat="server" CssClass="Label" Text="Cobrador Telefónico"
                                                                AutoPostBack="True"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dp_Ejecutivos" runat="server" CssClass="clsDisabled" Width="232px"
                                                                Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:CheckBox ID="CB_Cli" runat="server" CssClass="Label" Text="Pagador" 
                                                                AutoPostBack="True">
                                                            </asp:CheckBox>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" Width="90px" 
                                                                ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" 
                                                                Width="15px" MaxLength="1"
                                                                ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                                                                
                                                         <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagador" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" Style="margin-top: 0px" Enabled="False" />    
                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                            </cc1:MaskedEditExtender>
                                                            &nbsp;
                                                            <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" Width="250px"
                                                                 ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Pagador a Reasignar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="center" valign="top">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                   
                                                    <tr>
                                                        <td align="left" valign="top">
                                                            <asp:Panel ID="Panel_GV_Deudores" runat="server" Width="600px" Height="180px" ScrollBars="Auto">
                                                                <asp:GridView ID="GV_Deudores" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    EnableTheming="True" HorizontalAlign="Center" ShowHeader="True" Width="97%" CellPadding="2">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="IB_Seleccionar" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                                                    OnClick="IB_Seleccionar_Click" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CB_Seleccionar" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                            <FooterStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Deudor" HeaderText="Razón Social">
                                                                            <ItemStyle HorizontalAlign="Left" Width="300" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cantidad" HeaderText="#Clientes">
                                                                            <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="suma" HeaderText="#Doctos.">
                                                                            <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="id_eje" Visible="False">
                                                                            <ItemStyle Width="0px" />
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
                                                        
                                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" AlternateText="Anterior" />
                                                                
                                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" AlternateText="Siguiente" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Cobradores a Reasignar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="center" valign="top">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Panel ID="Panel_GV_Ejecutivos" runat="server" Width="500px" Height="120px">
                                                                <asp:GridView ID="GV_Ejecutivos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    EnableTheming="True" HorizontalAlign="Center" ShowHeader="true" Width="96%" CellPadding="1">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-Width="90px">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click" ToolTip='<%# Eval("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Codigo" HeaderText="Código">
                                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Nombre" HeaderText="Cobrador">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="sumaDeu" HeaderText="#Pagadores">
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="sumaDoc" HeaderText="#Doctos.">
                                                                            <ItemStyle Width="50px" />
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
                                                                    <td >
                                                                        <asp:ImageButton ID="IB_Prev_Cob" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                            AlternateText="Anterior" />
                                                                        <asp:ImageButton ID="IB_Next_Cob" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                            AlternateText="Siguiente" />
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
                                </tbody>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                                OnClick="IB_Buscar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" ToolTip="Buscar Pagadores" />
                                        </td>
                                        <td>
                                            <%--<asp:ImageButton ID="IB_AsigAnt" runat="server" AlternateText="Asignaciones Anteriores"
                                                ImageUrl="~/Imagenes/Botones/Btn_asig_ant_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Btn_asig_ant_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/Btn_asig_ant_in.gif';" ToolTip="Asignaciones Anteriores" />--%>
                                                
                                            <asp:ImageButton ID="IB_AsigAnt" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Gestion_Anterior_out.gif"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Gestion_Anterior_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Gestion_Anterior_In.gif';" ToolTip="Gestiones Anteriores"/>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="IB_Reemplazo" runat="server" AlternateText="Reemplazos" ImageUrl="~/Imagenes/Botones/Btn_Reemp_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/Btn_Reemp_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Btn_Reemp_in.gif';" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif ';" OnClick="IB_Guardar_Click"
                                                runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" AlternateText="Guardar Datos">
                                            </asp:ImageButton>
                                            
                                            <asp:ImageButton ID="IB_Limpiar" AlternateText="Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                                                onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>

            <asp:CheckBox ID="CB_Sel" runat="server" Visible="False"></asp:CheckBox>
            <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="TxtCodEje" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
</asp:Content>
