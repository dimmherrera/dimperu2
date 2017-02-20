<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="VB_Pagos.aspx.vb" Inherits="VB_Pagos" title="Visto Bueno de Pagos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="../FuncionesPrivadasJS/Pagos.js" type="text/javascript"></script>
<link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="UP_General" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0"  style="position:static;text-align:-moz-center" class="Contenido">
                <tr>
                    <td class = "Cabecera" height="31px" align="center" style="position:static;text-align:-moz-center" valign="middle">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Control Dual - Visto Bueno de Pagos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding:5px; height: 590px;position:static;text-align:-moz-center" valign="top"  align="center">
                        <table border=0 cellpadding =0 cellspacing=0 width="99%" style="position:static;text-align:-moz-center">
                        <tr>
                            <td valign="top" align="center">
                                <table id="Table1" border="0" cellpadding="0" cellspacing="0" >
                                    <tbody>
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" valign="top" style="height:150px">
                                              <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label44" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                    Text="Fecha Pago" Width="90px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_FechaPago" runat="server" CssClass="clsMandatorio" 
                                                    MaxLength="10" TabIndex="100" Width="90px"></asp:TextBox>
                                                    
                                                <cc2:MaskedEditExtender ID="Txt_FechaPago_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_FechaPago">
                                                </cc2:MaskedEditExtender>
                                                <cc2:CalendarExtender ID="Txt_FechaPago_CalendarExtender" runat="server" 
                                                    CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                    Format="dd-MM-yyyy" TargetControlID="Txt_FechaPago">
                                                </cc2:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <asp:CheckBox ID="CB_Cliente" runat="server" AutoPostBack="True" 
                                                    CssClass="Label" Font-Bold="true" Height="17px" TabIndex="10" 
                                                    Text="Cliente" Width="60px"   />
                                            </td>
                                            <td>
                                               <asp:TextBox ID="Txt_Rut_Cli" runat="server" __designer:wfdid="w286" 
                                                    CssClass="clsDisabled"  ReadOnly="True" Style="position: static" 
                                                    TabIndex="11" Width="90px"></asp:TextBox>
                                                <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="False" 
                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                    TargetControlID="Txt_Rut_Cli">
                                                </cc2:MaskedEditExtender>
                                            </td>
                                            <td>
                                             
                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" __designer:wfdid="w286" 
                                                    CssClass="clsDisabled" MaxLength="1" ReadOnly="True" Style="position: static" 
                                                    TabIndex="11" Width="20px" AutoPostBack="true" ></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                    TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                </cc2:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" 
                                                    ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" Enabled="false" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" 
                                                    CssClass="clsDisabled" MaxLength="50" ReadOnly="True" Style="position: static" 
                                                    TabIndex="13" Width="270px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td valign="top" >
                                <table width="700px" cellpadding="0" border="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label5" runat="server" Text="Clientes / Pagadores con Pago" 
                                        CssClass="SubTitulos"></asp:Label>
                                </td>
                              
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top">
                                <div id="Div_Pagos" style="overflow: scroll; width: 700px; height: 150px" >
                                
                                    <asp:GridView ID="GV_Cli_Deu" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                        <Columns>
                                          <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                             <ItemTemplate>
                                               <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click" ToolTip='<%# Eval("NroPago") %>' />
                                              </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:BoundField DataField="NroPago" HeaderText="N°Pago">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Rut" HeaderText="Identificación">
                                                <ItemStyle HorizontalAlign="Right" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Razón Social">
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quien" HeaderText="Quien Paga">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Right" Width="90px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                        <RowStyle CssClass="formatUltcell" />
                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                    
                                </div>    
                                </td>
                            </tr>
                        </table>
                            </td>
                        </tr>
                        </table>
                        
                        <br />   
                         
                        <table width="99%" cellpadding="0" border="0" cellspacing="0" style="position:static;text-align:-moz-center">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label1" runat="server" Text="Modos de Pagos" 
                                        CssClass="SubTitulos"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top" align="center" style="position:static;text-align:-moz-center">
                                <div id="Div1" style="overflow: scroll; width: 99%; height: 120px" >
                                    <asp:GridView ID="GV_Pagos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell" Width="95%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Selección">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CB" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"  Width="100px"/>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo Documento">
                                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="id_dpo" HeaderText="N° Docto. Pago">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dpo_mto" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="banco" HeaderText="Banco">
                                                <ItemStyle HorizontalAlign="left" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dpo_fec_emi" HeaderText="Fecha Emi." DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dpo_fev" HeaderText="Fecha Vcto." DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="plaza" HeaderText="Plaza">
                                                <ItemStyle HorizontalAlign="left" Width="150px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                </div> 
                                </td>
                               
                            </tr>
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <img src="../../../Imagenes/Infografia/Aprobado.gif" />
                                        </td>
                                        <td>
                                            <img src="../../../Imagenes/Infografia/Rechazado.gif" />
                                        </td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        
                        <table width="99%" cellpadding="0" border="0" cellspacing="0" style="position:static;text-align:-moz-center">
                            <tr>
                                <td class="Cabecera" style="width:300px" align="left">
                                    <asp:Label ID="Label4" runat="server" Text="Detalle de Pagos" CssClass="SubTitulos"></asp:Label>
                                </td> 
                                <td class="Cabecera" style="width:700px" align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Documentos o Cuentas por Cobrar" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top" style="width:300px" align="center">
                                <div id="Div2" style="overflow: scroll; width: 300; height: 150px" >
                                    <asp:GridView ID="GV_Detalle" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                        <Columns>
                                         <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                             <ItemTemplate>
                                               <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button2_Click" ToolTip='<%# Eval("Id_Tipo") %>' />
                                         </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:BoundField DataField="Id_Tipo" HeaderText="Código">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                                <ItemStyle HorizontalAlign="Center" Width="110px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                </div> 
                                </td>
                                <td class="Contenido" style="width:700px;position:static;text-align:-moz-center" valign="top" align="center">
                                
                                <div id="Div3" style="overflow: scroll; width: 900; height: 150px;position:static;text-align:-moz-center">
                                    <asp:GridView ID="Gv_Doctos" runat="server" AutoGenerateColumns="False" 
                                        CssClass="formatUltcell">
                                        <Columns>
                                            <asp:BoundField DataField="RutDeu" HeaderText="NIT Pagador">
                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NombreDeu" HeaderText="Razón Social">
                                                <ItemStyle HorizontalAlign="left" Width="250px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pnu_atr_003" HeaderText="T.D.">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dsi_num" HeaderText="N° Docto.">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_abo" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_int" HeaderText="Interés">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                    <asp:GridView ID="GV_CxC" runat="server" AutoGenerateColumns="False" 
                                        CssClass="formatUltcell" >
                                        <Columns>
                                            <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cuenta">
                                                <ItemStyle HorizontalAlign="left" Width="240px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="id_cxc" HeaderText="N°Cuenta">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cxc_fec" HeaderText="Fecha Gen." DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cxc_des" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="left" Width="340px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_abo" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_int" HeaderText="Interes">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                </div> 
                                </td>
                                 
                            </tr>
                        </table>
                        
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle" height="50">
                        <asp:ImageButton ID="IB_BuscarPagos" runat="server" 
                            ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" 
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" 
                            TabIndex="1" ToolTip="Buscar Pagos" />
                        <asp:ImageButton ID="IB_Rechazar" runat="server" 
                            ImageUrl="~/Imagenes/Botones/boton_rechazar_out.gif" 
                            onmouseout="this.src='../../../Imagenes/Botones/boton_rechazar_out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/boton_rechazar_in.gif';" 
                            TabIndex="1" ToolTip="Rechazar  Pagos" />
                        <asp:ImageButton ID="IB_Validar" runat="server" 
                            ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" 
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';" 
                            TabIndex="1" ToolTip="Validar Pagos" />
                        <asp:ImageButton ID="IB_Imprimir" runat="server" 
                            onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" 
                            ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif" TabIndex="2" 
                            ToolTip="Imprimir Pago"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" runat="server" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" 
                            ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            AlternateText="Limpiar" TabIndex="3"></asp:ImageButton> 
                    </td>
                </tr>
            </table>
            
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Txt_FechaPago"
                ErrorMessage="<b>Fecha</b><br />Fecha Invalida." Font-Size="8pt" MaximumValue="31/12/3000"
                MinimumValue="01/01/1900" Type="Date" Display="None"></asp:RangeValidator>
            <cc2:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender2" TargetControlID="RangeValidator1"
                HighlightCssClass="validatorCalloutHighlight" />
                
            <asp:LinkButton ID="LB_BuscarDetallePago" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Buscar" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_CargaPagos" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscaDocCxC" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscarCliente" runat="server"></asp:LinkButton>
            
            <asp:HiddenField ID="HF_Pos_Ing" runat="server" />
            <asp:HiddenField ID="HF_Id_Ing" runat="server" />
            
            <asp:HiddenField ID="HF_Pos_Doc_CxC" runat="server" />
            
        </ContentTemplate>
        
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers>
        
    </asp:UpdatePanel>    
    <asp:LinkButton ID="LB_Aprobar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Rechazar" runat="server"></asp:LinkButton>
</asp:Content>

