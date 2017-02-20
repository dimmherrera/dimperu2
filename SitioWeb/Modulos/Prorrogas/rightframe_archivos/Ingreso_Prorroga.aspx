<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Ingreso_Prorroga.aspx.vb" Inherits="Modulos_Prorrogas_Ingreso_Prorroga" Title="Mantención de Prorrogas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="~/WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../Carp. Comercial/FuncionesPrivadasJS/Negociación.js"   type="text/javascript"></script>
    <script src="../../Ayudas/FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>
    <script src="../FuncionesProvadasJS/SolicitudProrroga.js" type="text/javascript"></script>
       <script language=javascript>

           function SelecionaDocto(Posicion) {
               window.document.forms[0].hf_posicion.value = Posicion;
               return;
           }




           function DoScroll() {
               var _gridView = document.getElementById("GridViewDiv");
               var _header = document.getElementById("HeaderDiv");
               _header.scrollLeft = _gridView.scrollLeft;
           }
</script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
         <ContentTemplate >
         
             <table id="tb_gral" style="position:static;text-align:-moz-center" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                 <tr>
                     <td class = "Cabecera" align="center">
                         <asp:Label ID="Label40" runat="server" CssClass="Titulos" 
                             Text="Prórroga - Solicitud de Prórroga"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td class="Contenido" style="padding: 2px; height:600px;position:static;text-align:-moz-center" valign="top" align="center">
                            <table  cellspacing="0" cellspacing="0" width="100%">
                               <tr>
                                 <td class="Cabecera" align="left" style="width:100%">
                                    <asp:Label ID="Label43" runat="server" __designer:wfdid="w284" 
                                       CssClass="SubTitulos" Style="left: 8px; position: static; top: -14px" 
                                       Text="Criterios de Búsqueda"></asp:Label>
                                     </td>
                               </tr>
                              <tr>
                                <td class="Contenido">
                                <table id="cliente_diarios" border="0" style="height: 100px" width="100%">
                                 <tr>
                                     <%--Cliente--%>
                                     <td valign="top" align="center">
                                         <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0" width="550" 
                                             height="100">
                                             <tbody>
                                                 <tr>
                                                     <td class="Cabecera" align="left">
                                                         <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label1" runat="server"
                                                             CssClass="SubTitulos" Text="Cliente" __designer:wfdid="w284"></asp:Label>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="Contenido" valign="top" style="height: 100px;position:static;text-align:-moz-center" align="center">
                                                         <table cellspacing="0" cellpadding="0" border="0" style="position:static;text-align:-moz-center">
                                                             <tbody>
                                                                 <tr>
                                                                     <td align="right">
                                                                         <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" 
                                                                             __designer:wfdid="w285"></asp:Label>
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                                             CssClass="clsMandatorio" Width="90px" __designer:wfdid="w286" ></asp:TextBox>
                                                                         <asp:TextBox ID="Txt_Dig_Cli"  MaxLength=1 runat="server" AutoPostBack="True" 
                                                                             CssClass="clsMandatorio" Width="20px" Columns="1" TabIndex="1"></asp:TextBox>
                                                                         <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                                             runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                             TargetControlID="Txt_Dig_Cli" ValidChars="k,K">
                                                                         </cc2:FilteredTextBoxExtender>
                                                                         <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="None"
                                                                             CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                             CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                             CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                             Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                         </cc2:MaskedEditExtender>
                                                                         <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" Style="margin-top: 0px" />
                                                                        
                                                                     </td>
                                                                     <td align="right">
                                                                         &nbsp;</td>
                                                                     <td align="left">
                                                                         &nbsp;</td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="right" colspan="1">
                                                                         <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                                             Text="Razón Soc." Width="70px"></asp:Label>
                                                                     </td>
                                                                     <td align="left" colspan="3">
                                                                         <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                              ReadOnly="True" 
                                                                             Style="position: static; margin-bottom: 3px;" Width="465px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="right">
                                                                         <asp:Label Style="position: static" ID="Label15" runat="server" CssClass="Label"
                                                                             Text="Sucursal" __designer:wfdid="w290"></asp:Label>
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:TextBox ID="Txt_Sucursal" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                             Width="180px"></asp:TextBox>
                                                                     </td>
                                                                     <td align="right">
                                                                         <asp:Label Style="position: static" ID="Label14" runat="server" CssClass="Label"
                                                                             Text="Ejecutivo" __designer:wfdid="w292"></asp:Label>
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:TextBox ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                             Width="180px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                             </tbody>
                                                         </table>
                                                     </td>
                                                 </tr>
                                             </tbody>
                                         </table>
                                     </td>
                                     <%--Criterios (Fecha Vcto.)--%>
                                     <td valign="top" align="center">
                                         <table id="Table1" cellspacing="0" cellpadding="0" border="0" 
                                             height="100" width="550px">
                                             <tbody>
                                                 <tr>
                                                     <td class="Cabecera" align="left">
                                                         <asp:Label  runat="server"
                                                             CssClass="SubTitulos" Text="Pagador y Fecha de Vencimiento" 
                                                             __designer:wfdid="w284"></asp:Label>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td class="Contenido" valign="top" style="height: 100px">
                                                         <table cellspacing="0" cellpadding="0" border="0">
                                                             <tbody>
                                                                 <tr>
                                                                     <td align="right">
                                                                         <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Identificación" 
                                                                             __designer:wfdid="w285"></asp:Label>
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:TextBox ID="Txt_Rut_Deu" runat="server" __designer:wfdid="w286" CssClass="clsMandatorio"
                                                                              Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                                         <cc2:MaskedEditExtender ID="Txt_Rut_Cli0_MaskedEditExtender" runat="server" AcceptNegative="None"
                                                                             CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                             CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                             CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                             Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                         </cc2:MaskedEditExtender>
                                                                         <asp:TextBox ID="Txt_Dig_Deu" runat="server" AutoPostBack="True" Columns="1" CssClass="clsMandatorio"
                                                                             MaxLength="1" TabIndex="1" Width="20px"></asp:TextBox>
                                                                         <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Clientes"
                                                                             ImageUrl="../../../Imagenes/Iconos/155.ICO" Style="margin-top: 0px" Width="20px" />
                                                                         <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli0_FilteredTextBoxExtender" runat="server"
                                                                             Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="k,K">
                                                                         </cc2:FilteredTextBoxExtender>
                                                                     </td>
                                                                     <td>
                                                                         &nbsp;
                                                                     </td>
                                                                     <td>
                                                                         &nbsp;
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="right" class="Label">
                                                                         Razón Soc.
                                                                     </td>
                                                                     <td colspan="4">
                                                                         <asp:TextBox ID="Txt_Rso_Deu" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                                              ReadOnly="True" 
                                                                             Style="position: static; margin-bottom: 3px;" Width="465px"></asp:TextBox>
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
                                                                         <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:TextBox ID="txt_FechaVctoDesde" runat="server" CssClass="clsMandatorio" MaxLength="10"
                                                                             Width="90px"></asp:TextBox>
                                                                         <cc2:MaskedEditExtender ID="txt_FechaVctoDesde_MaskedEditExtender" runat="server"
                                                                             CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                             CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                             CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaVctoDesde">
                                                                         </cc2:MaskedEditExtender>
                                                                         <cc2:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar"
                                                                             Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_FechaVctoDesde">
                                                                         </cc2:CalendarExtender>
                                                                     </td>
                                                                     <td>
                                                                         <asp:Label ID="Label44" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                         &nbsp;
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:TextBox ID="txt_FechaVctoHasta" runat="server" CssClass="clsMandatorio" 
                                                                             MaxLength="10" Width="90px"></asp:TextBox>
                                                                         <cc2:MaskedEditExtender ID="txt_FechaVctoHasta_MaskedEditExtender" 
                                                                             runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                             CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                             Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaVctoHasta">
                                                                         </cc2:MaskedEditExtender>
                                                                         <cc2:CalendarExtender ID="CalendarExtender" runat="server" 
                                                                             CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                             Format="dd-MM-yyyy" TargetControlID="txt_FechaVctoHasta">
                                                                         </cc2:CalendarExtender>
                                                                         &nbsp;
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="right">
                                                                         <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Tipo Docto."></asp:Label>
                                                                     </td>
                                                                     <td align="left">
                                                                         <asp:DropDownList ID="DP_Tip_Doc" runat="server" CssClass="clsMandatorio" 
                                                                             Width="150px">
                                                                         </asp:DropDownList>
                                                                     </td>
                                                                     <td>
                                                                         &nbsp;
                                                                     </td>
                                                                     <td>
                                                                         &nbsp;
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
                             </table>
                             </td>
                             </tr>
                            </table>

                             <asp:LinkButton ID="Lb_buscar" runat="server" __designer:wfdid="w372" OnClick="Lb_buscar_Click"
                                 Style="position: static" TabIndex="54" ValidationGroup="Cliente"></asp:LinkButton>
                             <br />
                             <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                 <tbody>
                                     <tr>
                                         <td align="left" class="Cabecera">
                                             <asp:Label ID="Label20" runat="server" Text="Documentos a Prorrogar" CssClass="SubTitulos"></asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td class="Contenido" style="text-align:-moz-center" valign="top" align="center">
                                         
                                             <asp:Panel ID="Panel_GV_Negociacion" runat="server" ScrollBars="Horizontal" Height="220px" Width="1250px">
                                           
                                                                 <asp:GridView ID="GV_Negociacion" runat="server" AutoGenerateColumns="False"  AllowSorting="true"
                                                                     CssClass="formatUltcell" Width="1380px" AllowPaging="True" PageSize="200">
                                                                     <Columns>
                                                                      <asp:TemplateField >
                                                                           <ItemTemplate>
                                                                             <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click" ToolTip='<%# Eval("id_doc") %>' />
                                                                           </ItemTemplate>
                                                                           <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField  HeaderImageUrl="~/Imagenes/Iconos/check.gif" SortExpression="Todos">
                                                                             <ItemTemplate> 
                                                                                 <asp:CheckBox ID="CHB_SelDocto" runat="server" AutoPostBack="True" oncheckedchanged="CHB_SelDocto_CheckedChanged" ToolTip='<%# Eval("id_doc") %>' />
                                                                             </ItemTemplate>
                                                                             <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                                         </asp:TemplateField>
                                                                         <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                                             <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="Deudor" HeaderText="Razón Social">
                                                                             <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="id_opn" HeaderText="Nº Ope.">
                                                                             <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="TipoDoctoCorta" HeaderText="T.D.">
                                                                             <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                             <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="dsi_num" HeaderText="Nro.Docto.">
                                                                             <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota">
                                                                             <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="doc_num_ren" HeaderText="Nº Pro.">
                                                                             <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="doc_fev_rea" HeaderText="Fec.Vcto">
                                                                             <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="dsi_mto" HeaderText="Mto Docto.">
                                                                             <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField DataField="doc_sdo_cli" HeaderText="Sdo. Cli">
                                                                             <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                         </asp:BoundField>
                                                                         <%--     <asp:BoundField HeaderText="EstCob" DataField="cco_num">
                                                                         <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                     </asp:BoundField>--%>
                                                                         <asp:BoundField DataField="cco_des" HeaderText="Estado Cob.">
                                                                             <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Días Mora">
                                                                             <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                         </asp:BoundField>
                                                                     </Columns>
                                                                 <HeaderStyle CssClass="cabeceraGrilla" />
                                                                 <RowStyle CssClass="formatUltcell" />
                                                                 <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                 </asp:GridView>
                                                         <%--</div>--%></asp:Panel>
                                                        <asp:Label ID="Lbl_Pagina" runat="server" Text="Pagina N°: " CssClass="Label"></asp:Label> 
                                                        <img src="../../../Imagenes/Infografia/ConSolicitud.gif" />
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="Center">
                                             <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="~/Imagenes/btn_workspace/flecha_izq_out.gif" 
                                                 onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" />
                                             <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="~/Imagenes/btn_workspace/flecha_der_out.gif" 
                                                 onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" />
                                         </td>
                                     </tr>
                                 </tbody>
                             </table>
                             
                             <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="100%" style="position: static;text-align:-moz-center">
                                 <tbody>
                                     <tr>
                                         <td align="left" class="Cabecera">
                                             <asp:Label ID="Label24" runat="server" Text="Creación de Cuentas Por Cobrar" CssClass="SubTitulos"></asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td class="Contenido" valign="top">
                                             
                                             <table>
                                                 <tr>
                                                     <td align="right">
                                                         <asp:Label ID="Label25" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Fecha Solicitud"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="txt_FechaApremora" runat="server" CssClass="clsDisabled" 
                                                             MaxLength="10" Width="90px" ReadOnly="True"></asp:TextBox>
                                                     </td>
                                                     <td>
                                                         <asp:Label ID="Label27" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Comisión Por Prorroga"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="txt_ComisionProrroga" runat="server" CssClass="clsMandatorio" 
                                                             MaxLength="10" TabIndex="1" Width="90px"></asp:TextBox>
                                                         <cc2:MaskedEditExtender ID="txt_ComisionProrroga_MaskedEditExtender" 
                                                             runat="server" AcceptNegative="Left" AutoComplete="False" 
                                                             CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                             CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                             ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                             MaskType="Number" TargetControlID="txt_ComisionProrroga">
                                                         </cc2:MaskedEditExtender>
                                                     </td>
                                                     <td align="left">
                                                         <asp:Label ID="Label30" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Observación" ></asp:Label>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         <asp:Label ID="Label26" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Tasa Nuevo Periodo"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="txt_TasaPeriodo" runat="server" CssClass="clsMandatorio" 
                                                             MaxLength="4" TabIndex="2" Width="90px"></asp:TextBox>
                                                        
                                                        <%-- <cc2:FilteredTextBoxExtender ID="txt_TasaPeriodo_FilteredTextBoxExtender" 
                                                             runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                             TargetControlID="txt_TasaPeriodo" ValidChars=",.">
                                                         </cc2:FilteredTextBoxExtender>--%>
                                                        
                                                         <cc2:MaskedEditExtender ID="Txt_Por_Ant_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                                                CultureName="es-ES" CultureThousandsPlaceholder="." CultureTimePlaceholder="" 
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999.99" MaskType="Number" 
                                                                TargetControlID="txt_TasaPeriodo">
                                                            </cc2:MaskedEditExtender>
                                                     </td>
                                                     <td align="right">
                                                         <asp:Label ID="Label28" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Nva. Fecha Vcto."></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="txt_fecvcto" runat="server" 
                                                             CssClass="clsMandatorio" MaxLength="10" TabIndex="2" Width="90px" AutoPostBack="true"></asp:TextBox>
                                                         <cc2:MaskedEditExtender ID="txt_fecvcto_MaskedEditExtender" runat="server" 
                                                             CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                             CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                             Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecvcto">
                                                         </cc2:MaskedEditExtender>
                                                         <cc2:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                             CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                             Format="dd-MM-yyyy" TargetControlID="txt_fecvcto">
                                                         </cc2:CalendarExtender>
                                                     </td>
                                                     <td align="1" colspan="1" rowspan="2" valign="1">
                                                         <asp:TextBox ID="txt_observacion" runat="server" CssClass="clsMandatorio" MaxLength="250" TabIndex="2" TextMode="MultiLine" Width="450px" Wrap="False"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                 <td align="right">
                                                      <asp:Label ID="Label4" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Mod. Calc."></asp:Label>
                                                 </td>
                                                 <td>
                                                     <asp:RadioButton ID="RB_Cal1" runat="server" GroupName="TipCal" CssClass="Label" Text="Lineal" Enabled="false"/>
                                                     <asp:RadioButton ID="RB_Cal2" runat="server" GroupName="TipCal" CssClass="Label" Text="Exponencial" Enabled="false" Checked="true"/>
                                                 </td>
                                                     <td>
                                                         <asp:Label ID="Label3" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Nva. Fecha Vcto. Real"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:TextBox ID="txt_fecVctoReal" runat="server" CssClass="clsDisabled" 
                                                             MaxLength="10" ReadOnly="true" TabIndex="100" Width="90px"></asp:TextBox>
                                                     </td>
                                                     <td>
                                                         &nbsp;</td>
                                                 </tr>
                                                 <tr>
                                                     <td align="right">
                                                        <asp:Label ID="Label5" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                             Text="Tip. Interes"></asp:Label>
                                                     </td>
                                                     <td>
                                                        <asp:RadioButton ID="RB_Int1" runat="server" GroupName="TipInt" CssClass="Label" Text="Mensual" Enabled="false"/>
                                                        <asp:RadioButton ID="RB_Int2" runat="server" GroupName="TipInt" CssClass="Label" Text="Anual" Enabled="false" Checked="true"/>
                                                         
                                                     </td>
                                                     <td>
                                                     <asp:HiddenField ID="HF_Fev_Cal" runat="server" />
                                                     </td>
                                                     <td>
                                                     </td>
                                                     <td>
                                                         &nbsp;</td>
                                                 </tr>
                                             </table>
                                         </td>
                                     </tr>
                                 </tbody>
                             </table>                                      
                  
                     </td>
                 </tr>
                 <tr>
                     <td align="right">
                        
                         <asp:HiddenField ID="HF_Nro_Neg" runat="server" />
                         
                         <asp:ImageButton Style="position: static" ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_In.gif';"
                             onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="IB_Buscar_Click"
                             runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" __designer:wfdid="w375"
                             AlternateText="Buscar Clientes" ValidationGroup="Cliente" ToolTip="Buscar"></asp:ImageButton>
                         <asp:ImageButton ID="IB_GestAnt" onmouseover="this.src='../../../Imagenes/Botones/boton_gestionar_in.gif';"
                             onmouseout="this.src='../../../Imagenes/Botones/boton_gestionar_out.gif';" OnClick="IB_GestAnt_Click"
                             runat="server" ImageUrl="~/Imagenes/Botones/boton_gestionar_out.gif"
                             AlternateText="Gestiones Anteriores" ToolTip="Gestiones Anteriores"></asp:ImageButton>
                         <%--<a href="javascript:Negociación('PopUpNegociacion.aspx', 1280, 1024, 0, 0);"  >
              <img alt="" src="../../../Imagenes/Botones/boton_detalle_out.gif" onmouseover="this.src='../../../Imagenes/Botones/boton_detalle_in.gif';"
                  onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_out.gif';"  /></a>--%>
                  <asp:ImageButton ID="IB_Calcular" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                      onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" OnClick="IB_Calcular_Click"
                      runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" __designer:wfdid="w376"
                      AlternateText="Guarda Solicitud"  TabIndex="6"></asp:ImageButton>
                         <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                             onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                             runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                             AlternateText="Buscar Clientes" ToolTip="Limpiar"></asp:ImageButton>
                     </td>
                 </tr>
             </table>

     </ContentTemplate>
         <Triggers>
 <%--            <asp:PostBackTrigger ControlID="Ib_calcular" />--%>
             <asp:PostBackTrigger ControlID="ib_gestant" />
         </Triggers>
    </asp:UpdatePanel>
                 <asp:LinkButton ID="Lb_guardar" runat="server"></asp:LinkButton>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                        <uc1:Cargando ID="Cargando1" runat="server" />
                </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>


