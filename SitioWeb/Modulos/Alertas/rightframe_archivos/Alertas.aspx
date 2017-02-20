<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Alertas.aspx.vb" Inherits="Modulos_Alertas_rightframe_archivos_Alertas" title="Alertas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="../FuncionesPrivadasJS/Alertas.js" type="text/javascript"></script>
   
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <uc7:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
        
    <table cellspacing="1" cellpadding="0" width="100%" class="Contenido"> 
        <tr>
            <td style="height: 31px" class = "Cabecera">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Alertas"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" align="center" height="560" valign="top">
            
                <table border="0" cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
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
                        <td>
                            
                            <asp:UpdatePanel ID="UP_Cliente" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
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
                                                            CssClass="clsDisabled" Width="15px" ReadOnly="true" MaxLength="1"  AutoPostBack="true"></asp:TextBox>
                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes"
                                                         ImageUrl="../../../Imagenes/Iconos/155.ICO"  Width="20px" Enabled="False"/>
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
                                     <asp:HiddenField ID="HF_Ejecutivo" runat="server" />    
                                   </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                </table>
                <br />   
                <table>
                    <tr>
                        <td>
                            <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="400px"
                                Width="1000">
                                <cc2:TabPanel runat="server" ID="TabPanel1" HeaderText="Por Vencer">
                                    <HeaderTemplate>
                                        Por Vencer
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UP_Vencidos" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel_GV_PorVencer" runat="server" Width="988px" Height="390px" ScrollBars="Horizontal">
                                                    <asp:GridView ID="GV_PorVencer" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                        PageSize="1" AllowSorting="True" ShowHeader="true" Width="1700px">
                                                        <FooterStyle CssClass="cabeceraGrilla" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Rut_Cliente" HeaderText="NIT Cliente" DataFormatString="{0:C}"
                                                                HtmlEncode="false" HtmlEncodeFormatString="false">
                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Razon_Cliente" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="Left" Width="280px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Rut_Deudor" HeaderText="NIT Pagador">
                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Razon_Deudor" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="Left" Width="280px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nro_Opn" HeaderText="N° Ope.">
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TD" HeaderText="T.D.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nro_Docto" HeaderText="N° Docto.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nro_Cuota" HeaderText="N° Cuota">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Fec_Vto_Rea" HeaderText="Fec. Vcto." DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Saldo_Cli" HeaderText="Saldo Cli.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo Pag.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Cob_Des" HeaderText="Cobranza">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" ID="TabPanel2" HeaderText="Mora">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UP_Mora" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel_GV_Mora" runat="server" Width="988px" Height="390px" ScrollBars="Horizontal">
                                                    <asp:GridView ID="GV_Mora" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                        PageSize="1" AllowSorting="True" ShowHeader="true" Width="1700px">
                                                        <FooterStyle CssClass="cabeceraGrilla" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Rut_Cliente" HeaderText="NIT Cliente">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Razon_Cliente" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="Left" Width="280" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Rut_Deudor" HeaderText="NIT Pagador">
                                                                <ItemStyle HorizontalAlign="left" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Razon_Deudor" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="Left" Width="280" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nro_Opn" HeaderText="N° Otor..">
                                                                <ItemStyle HorizontalAlign="center" Width="80" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TD" HeaderText="T.D.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nro_Docto" HeaderText="N° Docto.">
                                                                <ItemStyle HorizontalAlign="Right" Width="80" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nro_Cuota" HeaderText="N° Cuota">
                                                                <ItemStyle HorizontalAlign="Right" Width="80" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Fec_Vto_Rea" HeaderText="Fec. Vcto." DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle HorizontalAlign="center" Width="80" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Saldo_Cli" HeaderText="Saldo Cli.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo Pag.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Cob_Des" HeaderText="Cobranza">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" ID="TabPanel3" HeaderText="Lineas">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UP_Lineas" runat="server">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel_GV_Lineas" runat="server" Width="988px" Height="380px" ScrollBars="Horizontal">
                                                                <asp:GridView ID="GV_Lineas" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                    PageSize="1" AllowSorting="True" ShowHeader="true" Width="1060px">
                                                                    <FooterStyle CssClass="cabeceraGrilla" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Rut_Cliente" HeaderText="NIT Cliente">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Razon_Cliente" HeaderText="Razón Social">
                                                                            <ItemStyle HorizontalAlign="Left" Width="280" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Nro_Linea" HeaderText="N° Linea">
                                                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Fecha_Vig" HeaderText="Fecha Vig." DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="center" Width="80" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Fecha_Vto" HeaderText="Fecha Vto." DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Adm_Mora" HeaderText="Admite Mora">
                                                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Mto_Aprobado" HeaderText="Monto Apb.">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Mto_Ocupado" HeaderText="Monto Ocu.">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
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
                                                        <td align="right">
                                                            <table width="150" cellpadding="0" cellspacing="0" border="1">
                                                                <tr>
                                                                    <td class="Label" bgcolor="#FF6666" align="center">
                                                                        Linea Sobregirada
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" ID="TabPanel4" HeaderText="Reserva">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UP_Exc" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel_GridView_Exc" runat="server" Width="988px" Height="390px" ScrollBars="Horizontal">
                                                    <asp:GridView ID="GV_Excedentes" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                        PageSize="1" AllowSorting="True" ShowHeader="true" Width="1280px">
                                                        <FooterStyle CssClass="cabeceraGrilla" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Rut_Cliente" HeaderText="NIT Cliente">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Razon_Cli" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="Left" Width="280" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Mto_Vig" HeaderText="Doc. Vig.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mor_001" HeaderText="0-30">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mor_002" HeaderText="30-60">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mor_003" HeaderText="60-90">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mor_004" HeaderText="+ 90">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Mto_Exc" HeaderText="Monto Rsv.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Mto_CXP" HeaderText="Monto CXP.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Mto_DNC" HeaderText="Monto DNC.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Mto_CXC" HeaderText="Monto CXC.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                            </cc2:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="988px">
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                            AlternateText="Anterior" />
                                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                            AlternateText="Siguiente" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
           
            </td>
           
        </tr>
        
        <tr>
        
            <td style="height: 50px" align="right">
            
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                  
                        <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" AlternateText="Buscar Clientes"
                            ToolTip="Buscar Alertas"></asp:ImageButton>
                              
                        <asp:ImageButton ID="IB_Imprimir" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif" AlternateText="Buscar Clientes"
                            ToolTip="Imprimir Alertas" Enabled="False"></asp:ImageButton>
                            
                        <asp:ImageButton ID="IB_Parametros" onmouseover="this.src='../../../Imagenes/Botones/btn_informe_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/btn_informe_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/btn_informe_out.gif" AlternateText="Buscar Clientes"
                            ToolTip="Parametros de Alertas" ></asp:ImageButton>
                            
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                         onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                         onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" AlternateText="Limpiar" />
                    
                    
                    </ContentTemplate>
                    
                    <Triggers>
                        <asp:PostBackTrigger ControlID="IB_Parametros" />
                        <asp:PostBackTrigger ControlID="IB_Limpiar" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            
        </tr>
        
    </table>
    
   
    
</asp:Content>

