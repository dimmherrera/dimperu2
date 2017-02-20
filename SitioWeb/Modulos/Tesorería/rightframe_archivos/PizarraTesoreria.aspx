<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="PizarraTesoreria.aspx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_PizarraTesoreria" title="Pizarra Tesorería" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="../FuncionesPrivadasJS/Pagos.js" type="text/javascript"></script>
<script type="text/ecmascript" src="../../../FuncionesJS/OnDemandTabs.js"></script>

<link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

<%--    <asp:UpdatePanel ID="UP_General" runat="server">
        <ContentTemplate>--%>
        
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" style="text-align:-moz-center" class="Contenido">
                <tr>
                    <td class = "Cabecera" height="31px" valign="middle" align="center">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Módulo de Control Tesorería (Ingresos / Egresos)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px; height: 590px" valign="top">
                    
                        <table id="CriterioGeneral" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="left" class="Cabecera">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Datos Generales"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" heigth="">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <asp:CheckBox ID="CB_Nomina" runat="server" AutoPostBack="True" Checked="True" 
                                                            CssClass="Label" Text="N° Nómina" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Nro_Nomina" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Nro_Nomina_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                            TargetControlID="Txt_Nro_Nomina">
                                                        </cc2:MaskedEditExtender>
                                                        <asp:ImageButton ID="IB_AyudaNom" runat="server" 
                                                            ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="Label46" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                            Text="Fecha Depósito"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_FechaDeposito" runat="server" __designer:wfdid="w286" CssClass="clsMandatorio"
                                                            MaxLength="10" Style="position: static" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="txt_FechaProceso_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_FechaDeposito">
                                                        </cc2:MaskedEditExtender>
                                                        <cc2:CalendarExtender ID="txt_FechaProceso_CalendarExtender" runat="server" Enabled="True"
                                                            FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_FechaDeposito"
                                                            CssClass="radcalendar">
                                                        </cc2:CalendarExtender>    
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br/>
                        <table width="100%">
                         <tr>
                           <td align="left">
                               <cc2:TabContainer ID="TabContainer1" runat="server" AutoPostBack="true" Height="450px"
                                   Width="100%" ActiveTabIndex="0">
                                   <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Ingresos">
                                       <HeaderTemplate>
                                           Ingresos</HeaderTemplate>
                                       <ContentTemplate>
                                                   <table id="Table1" border="0" cellpadding="3" cellspacing="0" width="98%">
                                                       <tbody>
                                                           <tr>
                                                               <td align="left" class="Cabecera">
                                                                   <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td class="Contenido" valign="top" align="center">
                                                                   <table border="0" cellpadding="0" cellspacing="0" heigth="">
                                                                       <tbody>
                                                                           <tr>
                                                                               <td>
                                                                                    &nbsp;</td>
                                                                               <td>
                                                                                   <asp:Label ID="Label45" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                                                       Text="Encargado Depósito"></asp:Label>
                                                                               </td>
                                                                               <td>
                                                                                   <asp:Label ID="Label47" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                                                       Text="Tipo de Desembolso"></asp:Label>
                                                                               </td>
                                                                               <td>
                                                                                   <asp:Label ID="Label2" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Banco"></asp:Label>
                                                                               </td>
                                                                               <td>
                                                                                   <asp:Label ID="Label5" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Plaza"></asp:Label>
                                                                               </td>
                                                                           </tr>
                                                                           <tr>
                                                                               <td align="left">
                                                                                   &nbsp;</td>
                                                                               <td align="left">
                                                                                   <asp:DropDownList ID="DP_EncargadoDep" runat="server" CssClass="clsMandatorio" 
                                                                                       Width="200px">
                                                                                   </asp:DropDownList>
                                                                               </td>
                                                                               <td align="left" colspan="1">
                                                                                   <asp:DropDownList ID="DP_FormaPago_Ing" runat="server" CssClass="clsDisabled" 
                                                                                       Enabled="False" Width="200px">
                                                                                   </asp:DropDownList>
                                                                               </td>
                                                                               <td align="left">
                                                                                   <asp:DropDownList ID="DP_Banco_Ing" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                                       Width="200px">
                                                                                   </asp:DropDownList>
                                                                               </td>
                                                                               <td align="left">
                                                                                   <asp:DropDownList ID="DP_Plaza_Ing" runat="server" CssClass="clsDisabled" Enabled="False"
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
                                                   <table width="98%" cellpadding="0" border="0" cellspacing="0">
                                                       <tr>
                                                           <td class="Cabecera" style="width: 500px" align="left">
                                                               <asp:Label ID="Label4" runat="server" Text="A Nómina" CssClass="SubTitulos"></asp:Label>
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td class="Contenido" style="height: 130px" valign="top" align="center">
                                                               <table cellpadding="0" cellspacing="0" style="text-align: -moz-center">
                                                                   <tr>
                                                                       <td valign="top" align="center">
                                                                           <asp:Panel ID="Panel1" runat="server" CssClass="Contenido" Height="120px" ScrollBars="Vertical" Width="690px">
                                                                               <asp:GridView ID="Gv_Pagos" runat="server" AutoGenerateColumns="False" 
                                                                                   CssClass="formatUltcell">
                                                                                   <Columns>
                                                                                       <asp:TemplateField HeaderText="Selección">
                                                                                           <ItemTemplate>
                                                                                               <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                                                                   ToolTip='<%# Eval("id_ing") %>' /></ItemTemplate>
                                                                                           <HeaderStyle HorizontalAlign="Center" />
                                                                                           <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                                       </asp:TemplateField>
                                                                                       <asp:TemplateField HeaderText="Todo" SortExpression="todo">
                                                                                           <ItemTemplate>
                                                                                               <asp:CheckBox ID="CB" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                                                                           </ItemTemplate>
                                                                                           <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                                       </asp:TemplateField>
                                                                                       <asp:BoundField DataField="id_ing" HeaderText="N° Pago">
                                                                                           <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="ing_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Pago">
                                                                                           <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Monto" DataFormatString="{0:###,###,###,##0}" HeaderText="Total Pago">
                                                                                           <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="ing_obs" HeaderText="Observación">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                       </asp:BoundField>
                                                                                   </Columns>
                                                                                   <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                   <RowStyle CssClass="formatUltcell" />
                                                                                   <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                               </asp:GridView>
                                                                           </asp:Panel>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td align="right">
                                                               <table border="0" cellpadding="0" cellspacing="0">
                                                                   <tr>
                                                                       <td align="right">
                                                                           <asp:Label ID="Label13" runat="server" Text="Total $" CssClass="SubTitulos"></asp:Label>
                                                                       </td>
                                                                       <td>
                                                                           <asp:TextBox ID="Txt_Total_Ing" runat="server" ReadOnly="True" CssClass="clsDisabled"></asp:TextBox>
                                                                       </td>
                                                                       <td>
                                                                           <table border="0" cellpadding="0" cellspacing="0">
                                                                               <tr>
                                                                                   <td align="center" style="background-color: #C2DAFA; font-size: 10px;" class="Label"
                                                                                       width="200">
                                                                                       Pago Directo Normal
                                                                                   </td>
                                                                                   <td align="center" style="background-color: #FFCC99; font-size: 10px;" class="Label"
                                                                                       width="200">
                                                                                       Pago por Hoja de Recaudación
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
                                                   <table width="98%" cellpadding="0" border="0" cellspacing="0">
                                                       <tr>
                                                           <td class="Cabecera" align="left">
                                                               <asp:Label ID="Label12" runat="server" Text="Modos de Pago" CssClass="SubTitulos"></asp:Label>
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td class="Contenido" style="height: 150px" valign="top" align="center">
                                                               <asp:GridView ID="GV_DetallePago" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                                   <Columns>
                                                                       <asp:BoundField DataField="Tipo" HeaderText="Tipo Documento">
                                                                           <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="id_dpo" HeaderText="N° Docto. Pago">
                                                                           <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                           <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="dpo_mto" HeaderText="Monto">
                                                                           <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="banco" HeaderText="Banco">
                                                                           <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="dpo_fec_emi" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Emi.">
                                                                           <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="dpo_fev" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto.">
                                                                           <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                       </asp:BoundField>
                                                                       <asp:BoundField DataField="plaza" HeaderText="Plaza">
                                                                           <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                       </asp:BoundField>
                                                                   </Columns>
                                                                      <HeaderStyle CssClass="cabeceraGrilla" />
                                                                      <RowStyle CssClass="formatUltcell" />
                                                                      <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                               </asp:GridView>
                                                           </td>
                                                       </tr>
                                                   </table>
                                                   <asp:LinkButton ID="LB_BuscarDetallePago" runat="server"></asp:LinkButton></ContentTemplate>
                                           <%--</asp:UpdatePanel>
                                       </ContentTemplate>--%>
                                   </cc2:TabPanel>
                                   <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Egresos">
                                       <ContentTemplate>
                                       <%--    <asp:UpdatePanel ID="UpdatePanelEgresos" runat="server">
                                               <ContentTemplate>--%>
                                                   <table id="Table2" border="0" cellpadding="3" cellspacing="0" width="98%">
                                                       <tbody>
                                                           <tr>
                                                               <td align="left" class="Cabecera">
                                                                   <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td class="Contenido" valign="top">
                                                                   <table border="0" cellpadding="2" cellspacing="0">
                                                                       <tbody>
                                                                           <tr>
                                                                               <td>
                                                                                   <asp:Label ID="Label7" runat="server" CssClass="Label" Font-Bold="True" Text="Tipo de Desembolso"></asp:Label>
                                                                               </td>
                                                                               <td>
                                                                                   <%--<asp:Label ID="Label10" runat="server" CssClass="Label" Font-Bold="True" Text="Origen"></asp:Label>--%>
                                                                               </td>
                                                                               <td>
                                                                                   <%--<asp:Label ID="Label8" runat="server" CssClass="Label" Font-Bold="True" Text="Antes 14 Hrs."></asp:Label>--%>
                                                                               </td>
                                                                               <td>
                                                                                   <%--<asp:Label ID="Label11" runat="server" __designer:wfdid="w285" CssClass="Label" Text="Banco a Depositar"></asp:Label>--%>
                                                                                   <asp:Label ID="Label49" runat="server" CssClass="Label" Font-Bold="True" 
                                                                                       Text="Bancos"></asp:Label>
                                                                               </td>
                                                                               <td>
                                                                               </td>
                                                                           </tr>
                                                                           <tr>
                                                                               <td align="left" valign="top">
                                                                                 <%--  <asp:RadioButtonList ID="RB_Pagos" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                                       BorderWidth="1px" CellPadding="0" CellSpacing="0" CssClass="Label" Width="160px"
                                                                                       Enabled="False">
                                                                                       <asp:ListItem Value="S" Selected="True">Abono a Cta. Cte.</asp:ListItem>
                                                                                       <asp:ListItem Value="N">Cheque - Vale Vista</asp:ListItem>
                                                                                       <asp:ListItem Value="T">Transfer. Electrónica</asp:ListItem>
                                                                                   </asp:RadioButtonList>--%><asp:DropDownList ID="DP_FormaPago_Egr" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                                       Width="200px">
                                                                                   </asp:DropDownList>
                                                                               </td>
                                                                               <td align="left" valign="top">
                                                                                   <%--<asp:RadioButtonList ID="RB_Origen" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                                       BorderWidth="1px" CellPadding="0" CellSpacing="0" CssClass="Label" Width="160px"
                                                                                       Enabled="False">
                                                                                       <asp:ListItem Value="1" Selected="True">Anticipos</asp:ListItem>
                                                                                       <asp:ListItem Value="2">Aplic. RSV, CXP, DNC </asp:ListItem>
                                                                                   </asp:RadioButtonList>--%>
                                                                               </td>
                                                                               <td align="left" valign="top">
                                                                                  <%-- <asp:RadioButtonList ID="RB_Antes" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                                       BorderWidth="1px" CellPadding="0" CellSpacing="0" CssClass="Label" Width="80px"
                                                                                       Enabled="False">
                                                                                       <asp:ListItem Value="S" Selected="True">Si</asp:ListItem>
                                                                                       <asp:ListItem Value="N">No</asp:ListItem>
                                                                                   </asp:RadioButtonList>--%>
                                                                               </td>
                                                                               <td align="left" valign="top">
                                                                                   <asp:DropDownList ID="DP_Banco_Egr" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                                       Width="200px">
                                                                                   </asp:DropDownList>
                                                                               </td>
                                                                               <td align="left" valign="top">
                                                                                   
                                                                               </td>
                                                                           </tr>
                                                                       </tbody>
                                                                   </table>
                                                               </td>
                                                           </tr>
                                                       </tbody>
                                                   </table>
                                                   <br />
                                                   <table width="98%" border="0" cellspacing="0" cellpadding="0">
                                                       <tr>
                                                           <td class="Cabecera" style="width: 500px">
                                                               <asp:Label ID="Label14" runat="server" Text="A Nómina" CssClass="SubTitulos"></asp:Label>
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td style="height: 230px" valign="top" class="Contenido">
                                                               <table cellspacing="0" cellpadding="0" border="0">
                                                                   <tr>
                                                                       <td valign="top">
                                                                           <asp:Panel ID="Panel2" runat="server" Height="340px" Width="1210px" ScrollBars="Auto">
                                                                               <asp:GridView ID="GV_Egresos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell" 
                                                                                   ShowHeader="True" CellPadding="0" Width="1500px">
                                                                                   <Columns>
                                                                                       <asp:TemplateField>
                                                                                           <ItemTemplate>
                                                                                               <asp:Button ID="Btn_Entregar" runat="server" Text="Entregar" 
                                                                                                   ToolTip='<%#Eval("id_egre")%>' CssClass="boton" onclick="Btn_Entregar_Click" />
                                                                                           </ItemTemplate>
                                                                                       </asp:TemplateField>
                                                                                       <asp:BoundField DataField="QuePaga" HeaderText="Origen">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="FormaPago" HeaderText="Tipo Desembolso">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="230px" />
                                                                                       </asp:BoundField>                                                                                       
                                                                                       <asp:BoundField DataField="egr_dep_ant" HeaderText="Antes 14 hrs.">
                                                                                           <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                       </asp:BoundField>                                                                                       
                                                                                       <asp:BoundField DataField="RutCliente" HeaderText="Identificación">
                                                                                           <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social Cliente">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                           <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Monto_Anticipo" DataFormatString="{0:###,###,###,##0}"
                                                                                           HeaderText="Monto Anticipo">
                                                                                           <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Monto_Egreso" DataFormatString="{0:###,###,###,##0}" HeaderText="Monto Egreso">
                                                                                           <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Fecha_Egreso" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Egreso">
                                                                                           <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Nomina" HeaderText="Nomina">
                                                                                           <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Banco" HeaderText="Banco">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="CtaCte" HeaderText="Nº Cuenta">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                       </asp:BoundField>
                                                                                       <asp:BoundField DataField="Observacion" HeaderText="Observación">
                                                                                           <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                       </asp:BoundField>
                                                                                     <%--  <asp:TemplateField Visible="False">
                                                                                           <ItemTemplate>
                                                                                               <asp:Label ID="lb_egr" Text='<%#Eval("id_egre")%>' runat="server"></asp:Label></ItemTemplate>
                                                                                       </asp:TemplateField>--%>
                                                                                   </Columns>
                                                                                   <HeaderStyle CssClass="cabeceraGrilla" />
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                               </asp:GridView>
                                                                           </asp:Panel>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td align="right">
                                                               <table border="0" cellpadding="0" cellspacing="0">
                                                                   <tr>
                                                                       <td align="right">
                                                                           <%--<asp:Label ID="Label15" runat="server" Text="Total $" CssClass="SubTitulos"></asp:Label>--%>
                                                                       </td>
                                                                       <td>
                                                                           <%--<asp:TextBox ID="Txt_Total_Egreso" runat="server" ReadOnly="True" CssClass="clsDisabled"></asp:TextBox>--%>
                                                                       </td>
                                                                       <td>
                                                                           <%--<asp:Label ID="Label48" runat="server" CssClass="SubTitulos" Text="Cant. Egresos"></asp:Label>--%>
                                                                       </td>
                                                                       <td>
                                                                           <%--<asp:TextBox ID="Txt_Cant_Egresos" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>--%>
                                                                       </td>
                                                                       <td>
                                                                           <table border="0" cellpadding="0" cellspacing="0">
                                                                               <tr>
                                                                                   <td align="center" style="background-color: #CCFFCC; font-size: 10px;" class="Label"
                                                                                       width="200">
                                                                                       Entregado al Cliente
                                                                                   </td>
                                                                               </tr>
                                                                           </table>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </td>
                                                       </tr>
                                                   </table>
                                           <%--    </ContentTemplate>
                                           </asp:UpdatePanel>--%>
                                       </ContentTemplate>
                                   </cc2:TabPanel>
                               </cc2:TabContainer>
                           </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle">
                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                            TabIndex="1" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_guardar_out.gif" Enabled="True" 
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_guardar_in.gif';"
                            TabIndex="1" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            AlternateText="Limpiar" TabIndex="3"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="HF_Egreso" runat="server" />        
            
            <asp:LinkButton ID="LB_DoctoPago" runat="server"></asp:LinkButton>
            <cc2:ModalPopupExtender ID="MP_DoctoPago" runat="server" TargetControlID="LB_DoctoPago"
                EnableViewState="False" PopupControlID="Panel_DoctoPago" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="Panel_DoctoPago">
            </cc2:ModalPopupExtender>
            <asp:Panel ID="Panel_DoctoPago" runat="server" Width="650px" Height="300px" style=" display:none"> 
                <table border="0" cellpadding="0" cellspacing="0" class="Contenido">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label33" runat="server" Text="Documentos de Pago : Cheque" CssClass="SubTitulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <table class="Contenido">
                                <tr>
                                    <td style="height: 100px" valign="top">
                                        <table border="0" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td>
                                                    <%--Banco--%>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" Text="Bancos" CssClass="Label" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="DP_Banco" runat="server" Width="400px" CssClass="clsMandatorio"
                                                                    Enabled="true" />
                                                                <cc2:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="DP_Banco"
                                                                    PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                                    PromptPosition="Bottom" IsSorted="true">
                                                                </cc2:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <%--Plaza--%>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" Text="Plaza" CssClass="Label" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="DP_PlazaBanco" runat="server" Width="400px" CssClass="clsMandatorio"
                                                                    Enabled="true" />
                                                                <cc2:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="DP_PlazaBanco"
                                                                    PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                                    PromptPosition="Bottom" IsSorted="true">
                                                                </cc2:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid #000000;">
                                        <%--Datos Docto. de Pago--%>
                                        <asp:Label ID="Label27" runat="server" Text="Datos Docto. de Pago" CssClass="Label"
                                            Font-Bold="true"></asp:Label>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label26" runat="server" Text="N° Docto." CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_NroDocto" runat="server" CssClass="clsMandatorio" ReadOnly="false" Width="100px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_NroDocto_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="999,999,999,999" TargetControlID="Txt_NroDocto" 
                                                        InputDirection="RightToLeft" MaskType="Number" 
                                                        >
                                                    </cc2:MaskedEditExtender>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label28" runat="server" Text="Fecha Emision" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fec_Emi" runat="server" CssClass="clsMandatorio" ReadOnly="false" Width="80px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Emi_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Emi">
                                                    </cc2:MaskedEditExtender>
                                                    <cc2:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Txt_Fec_Emi"
                                                        FirstDayOfWeek="Monday" Format="dd-MM-yyyy" CssClass="radcalendar">
                                                    </cc2:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label29" runat="server" Text="Fecha Vcto." CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fec_Vto" runat="server" CssClass="clsMandatorio" ReadOnly="false" Width="80px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_Fec_Vto_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Vto">
                                                    </cc2:MaskedEditExtender>
                                                    <cc2:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txt_Fec_Vto"
                                                        FirstDayOfWeek="Monday" Format="dd-MM-yyyy" CssClass="radcalendar">
                                                    </cc2:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label30" runat="server" Text="Cta. Cte." CssClass="Label"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="Txt_Cta_Cte" runat="server" CssClass="clsMandatorio" ReadOnly="false"
                                                        Width="340px" MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label32" runat="server" CssClass="Label" Text="Monto Docto."></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Mto_Dco" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label31" runat="server" Text="Origen de Fondo" CssClass="Label"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:DropDownList ID="DP_OrigenFondo" runat="server" CssClass="clsMandatorio" Width="100%"
                                                        Enabled="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label41" runat="server" CssClass="Label" Text="A la Orden"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="Txt_Orden" runat="server" Width="100%" CssClass="clsMandatorio"
                                                        ReadOnly="false"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="right">
                            <asp:ImageButton ID="IB_AceptarCheque" runat="server" AlternateText="Aceptar" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';" />
                            <asp:ImageButton ID="IB_CancelarCheque" runat="server" AlternateText="Cencelar" ImageUrl="~/Imagenes/Botones/Boton_Cancelar_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Cancelar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Cancelar_in.gif';" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
        <%--</ContentTemplate>
        <Triggers>
         <asp:PostBackTrigger ControlID="IB_Buscar" />
        <asp:PostBackTrigger ControlID="IB_AceptarCheque" />
        </Triggers>
    </asp:UpdatePanel>--%>
    
    <asp:HiddenField ID="HF_Pos_Ing" runat="server" />
    <asp:HiddenField ID="HF_Id_Ing" runat="server" />
    <asp:HiddenField ID="HF_NOMINA" runat="server" />
    
            
    <asp:LinkButton ID="LB_Ingreso" runat="server"></asp:LinkButton>   
    <asp:LinkButton ID="LB_Egreso" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_NOMINA" runat="server"></asp:LinkButton>
                           
           
</asp:Content>

