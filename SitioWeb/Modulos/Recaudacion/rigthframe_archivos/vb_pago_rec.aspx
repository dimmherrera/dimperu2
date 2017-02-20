<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="vb_pago_rec.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_Default" title="Visto Bueno Pagos Recaudación" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



 <%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language=javascript src="../Funciones_modulo_js/Recaudacion.js"></script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    
    <table style="width: 100%;position:static;text-align:-moz-center;" cellpadding="0" cellspacing="1" class="Contenido">
        <tr>
            <td class = "Cabecera" style="height: 31px;position:static;text-align:-moz-center" valign="middle" align="center">
                <asp:Label ID="Label48" runat="server" CssClass="Titulos" 
                    Text="Control Dual - Visto Bueno Pagos Recaudación"></asp:Label>
            </td>
      
        </tr>
        <tr>
            <td align="center" valign="top" class="Contenido" style="height: 590px; position: static;text-align: -moz-center;padding:10px">
            
                <table style="position: static; text-align: -moz-center;padding:10px" cellspacing="0" width="65%">
                    <tr>
                        <td valign="top">
                            <table cellspacing="0">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label8" runat="server" CssClass="SubTitulos" Text="Estados de Pago"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height:30px">
                                        <asp:RadioButtonList ID="rb_est_pgo" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="I" Selected="True">Ingresados</asp:ListItem>
                                            <asp:ListItem Value="R">Rechazados</asp:ListItem>
                                            <asp:ListItem Value="V">Validados</asp:ListItem>
                                            <asp:ListItem Value="T">Todos</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="0">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Fecha Recaudación"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height:30px">
                                        <asp:TextBox ID="txt_fec_rec" runat="server" CssClass="clsMandatorio" Width="90px"
                                            MaxLength="10" AutoPostBack="True"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="txt_fec_rec_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec_rec">
                                        </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="txt_fec_rec_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MM-yyyy" TargetControlID="txt_fec_rec" CssClass="radcalendar" FirstDayOfWeek="Monday">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="0">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Horario"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height:30px">
                                        <asp:RadioButtonList ID="rb_horario" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="A">AM</asp:ListItem>
                                            <asp:ListItem Value="P">PM</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table cellspacing="0">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Recaudador de Origen"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height:30px">
                                        <asp:DropDownList ID="dr_recau" runat="server" CssClass="clsMandatorio" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="width: 99.5%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label9" runat="server" Text="Pagos" CssClass="SubTitulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" style="padding: 2px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%; position: static; text-align: -moz-center">
                                <tr>
                                    <td class="Contenido" align="center" style="position: static; text-align: -moz-center">
                                        <asp:Panel ID="Panel1" runat="server" Height="120px" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="gr_ing" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                ShowHeader="True" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                                ToolTip='<%# Eval("id_ing") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id_ing" HeaderText="Nro de Pago">
                                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ing_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Pago">
                                                        <ItemStyle Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="total" DataFormatString="{0:###,###,###,###}" HeaderText="Total Pago">
                                                        <ItemStyle HorizontalAlign="Right" Width="160px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ing_obs" NullDisplayText="Sin Observaciones" HeaderText="Observación Pago">
                                                        <ItemStyle Width="350px" />
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
                        <td align="right" class="Label">
                            <table border="0" cellpadding="0" cellspacing="0">
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
                <table style="width: 100%; position: static; text-align: -moz-center">
                    <tr>
                        <td class="Contenido">
                            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                                <cc1:TabPanel HeaderText="Documentos a Pagar" ID="TabPanel1" runat="server" Width="800px"
                                    CssClass="SubTitulos">
                                    <HeaderTemplate>
                                        Doctos. a Pagar
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel3" runat="server" Width="1180px" Height="200px" ScrollBars="Horizontal">
                                                    <asp:GridView ID="gr_doctos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        Width="1180px">
                                                        <Columns>
                                                            <asp:BoundField DataField="DES_TIP_DOC" HeaderText="T.D">
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="N_DOCTO" HeaderText="Nº DOCTO">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RUT_CLI" HeaderText="NIT CLIENTE">
                                                                <ItemStyle Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="N_CLIENTE" HeaderText="RAZÓN SOCIAL">
                                                                <ItemStyle Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RUT_DEUDOR" HeaderText="NIT PAGADOR">
                                                                <ItemStyle Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_DEUDOR" HeaderText="RAZÓN SOCIAl">
                                                                <ItemStyle Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="interes" DataFormatString="{0:###,###,###,###}" HeaderText="INTERÉS">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="fec_vcto" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FECHA VCTO">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MTO_a_recaudar" DataFormatString="{0:###,###,###,###}"
                                                                HeaderText="MTO REC.">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <table width="1000px">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel HeaderText="Recaudación" ID="tabpanel2" runat="server" CssClass="SubTitulos">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="HF_Pos_DPO" runat="server" />
                                                <asp:Panel ID="Panel2" runat="server" Width="500px" Height="200px" ScrollBars="Horizontal">
                                                    <asp:GridView ID="gr_recau" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="true" Width="500px">
                                                        <Columns>
                                                            <asp:BoundField DataField="Tipo" HeaderText="TIPO DE PAGO">
                                                                <ItemStyle Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_dpo" NullDisplayText="0" HeaderText="Nº DOCTO.">
                                                                <ItemStyle Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="dpo_mto" DataFormatString="{0:###,###,###,###}" HeaderText="MTO REC.">
                                                                <ItemStyle Width="100px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <table width="500px">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ImageButton ID="IB_Prev_gr_recau" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                            <asp:ImageButton ID="IB_Next_gr_recau" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <img src="../../../Imagenes/Infografia/DoctoNoCedido.gif" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                
                <asp:HiddenField ID="nro_hoja" runat="server" />
                <asp:HiddenField ID="HF_Pos_doc" runat="server" />
                <asp:LinkButton ID="marca" runat="server"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
               
                                      <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btn_buscar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" ToolTip="Buscar" 
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                                  onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                ValidationGroup="Busca" />
                            <asp:LinkButton ID="RetornaDoctos" runat="server"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_validar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';"
                                  onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';"
                                ToolTip="Visto Bueno" />
                            <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_rechazar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/boton_rechazar_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_rechazar_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/boton_rechazar_in.gif';" 
                                ToolTip="Rechazo" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_informe" runat="server" 
                                ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" 
                                ToolTip="Informe Hoja Recaudación" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_limpiar" runat="server" 
                                ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif" 
                                       onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                                  onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                ToolTip="Limpiar" />
                        </td>
                    </tr>
                </table>
               
            </td>
        </tr>
    </table>
    
 
    </ContentTemplate>
        <Triggers>
           
            <asp:AsyncPostBackTrigger ControlID="btn_informe" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel2">

<ProgressTemplate>

             <uc1:Cargando ID="Cargando1" runat="server" />
</ProgressTemplate>

</asp:UpdateProgress> 
</asp:Content>

