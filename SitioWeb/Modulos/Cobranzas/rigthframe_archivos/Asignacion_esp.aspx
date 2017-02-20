<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Asignacion_esp.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Default" title="Asignación Especial Cobranza" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <script language="javascript">
      
      function ClickDocto(pTabla, pClass, jClass, sClass, Posicion) {

    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion.value = Posicion;

    return;

}

    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"  AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                  
                </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table style="width: 100%" cellpadding="0"  cellspacing="1" class="Contenido">
                <tr>
                    <td class="Cabecera" style="height:31px"> 
                        <asp:Label ID="Label25353" runat="server" CssClass="Titulos" Text="Cobranza - Asignación de Gestión Especial"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px; Height:590px; text-align:-moz-center" align="center" valign="top">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                    <table cellspacing="0" width="978">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label25356" runat="server" CssClass="SubTitulos" 
                                                    Text="Criterio Búsqueda por Cliente/Pagador"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                       <table>
                                        <tr>
                                            <td align="left"> 
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td valign="middle" style="height: 1px">
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender1" runat="server" 
                                                                            AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                            MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td style="height: 1px">
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" AutoPostBack="true" CssClass="clsMandatorio" MaxLength="1" Width="20px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 1px">
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO" width="20" />
                                                                    </td>
                                                                    <td style="height: 1px">
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True" Width="220px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" >
                                                <table cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" valign="top">
                                                            <asp:CheckBox ID="Ch_deu" runat="server" AutoPostBack="True" 
                                                                CssClass="SubTitulos" TabIndex="3" Text="Pagador" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" 
                                                                            TabIndex="4" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_deu_Cli_MaskedEditExtender" runat="server" 
                                                                            AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                            MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" AutoPostBack="true" 
                                                                            CssClass="clsDisabled" MaxLength="1" TabIndex="5" Width="20px" 
                                                                            ReadOnly="True"></asp:TextBox>
                                                                      
                                                                    
                                                                    </td>
                                                                    <td>
                                                                       <asp:ImageButton ID="Ib_ayuda_deu" runat="server" AlternateText="Ayuda Deudores"
                                                                         
                                                                             ImageUrl="../../../Imagenes/Iconos/155.ICO" width="20" Enabled="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" TabIndex="6" Width="220px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                    <table cellspacing="0" width="978px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label25355" runat="server" CssClass="SubTitulos" 
                                                    Text="Criterio Búsqueda por Operación y Documentos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" Text="Tipo Docto" Width="90px" 
                                                                CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="dr_tip_doc" runat="server" TabIndex="7" Width="200px" 
                                                                CssClass="clsTxt">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label6" runat="server" Text="Nro Docto" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_doc_des" runat="server" CssClass="clsTxt" TabIndex="9" Width="90px"></asp:TextBox>
                                                            
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label7" runat="server" Text="Monto Desde" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_mto_des" runat="server" CssClass="clsTxt" TabIndex="11" Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_des">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label9" runat="server" Text="Fec. Vcto Desde" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_venc_des" runat="server" CssClass="clsTxt" TabIndex="13" Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_venc_des_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txt_venc_des">
                                                            </cc1:MaskedEditExtender>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="radcalendar" FirstDayOfWeek="Monday"
                                                                Enabled="True" Format="dd-MM-yyyy" PopupPosition="TopLeft" TargetControlID="txt_venc_des">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                       
                                                        <td align="right">
                                                            <asp:Label ID="Label5" runat="server" Text="Nro Otorg." CssClass="Label"></asp:Label>
                                                        </td>
                                                        
                                                        <td>
                                                            <asp:TextBox ID="txt_oto_des" runat="server" CssClass="clsTxt" TabIndex="10" Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_oto_des">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td align="right">
                                                            <asp:Label ID="Label8" runat="server" Text="Monto Hasta" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_mto_has" runat="server" CssClass="clsTxt" TabIndex="12" Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto_has">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label10" runat="server" Text="Fec.Vcto Hasta" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_venc_has" runat="server" CssClass="clsTxt" TabIndex="14" Width="90px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txt_venc_has_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txt_venc_has">
                                                            </cc1:MaskedEditExtender>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="radcalendar" FirstDayOfWeek="Monday"
                                                                Enabled="True" Format="dd-MM-yyyy" PopupPosition="TopLeft" TargetControlID="txt_venc_has">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Panel ID="Panel_GridView1" runat="server" width="978px" height="300px"  >
                                        
                                        <asp:GridView ID="GridView1" runat="server" CssClass="formatUltcell" ShowHeader="true"
                                            Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="IB_SeleccionDoctos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif" ToolTip="Seleccionar Todos" OnClick="IB_SeleccionDoctos_Click"/>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ch_grid" runat="server" AutoPostBack="True" oncheckedchanged="ch_grid_CheckedChanged" ToolTip='<%#Eval("id_doc")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pnu_des" HeaderText="Tipo Doc.">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="opo_otg" HeaderText="Nº Otg.">
                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_num" HeaderText="Nº Docto." DataFormatString="{0:###,###,###}">
                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota">
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_fev_rea" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec. Vcto.">
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_mto" HeaderText="Monto">
                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Moneda_ope" HeaderText="Moneda">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cco_num" HeaderText="Cod.Cobr">
                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="id_doc" TEXT='<%#Eval("id_doc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                        </asp:Panel>
                                     
                                    <table width="980px">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        
                                                        <td style="height: 20px" align="center" colspan="2">
                                                            <table style="width: 80px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btn_prev_otg" runat="server" 
                                                                            ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif" 
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif'" 
                                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif'" 
                                                                            ToolTip="Anterior" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btn_next_otg" runat="server" 
                                                                            ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif" 
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif'" 
                                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif'" 
                                                                            ToolTip="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 313px">
                                                            <table border=0>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label25352" runat="server" CssClass="Label" Text="Sucursal" Width="140px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dr_suc" runat="server" CssClass="clsMandatorio" Width="200px"
                                                                        CausesValidation="True" ValidationGroup="ingreso"></asp:DropDownList>
                                                                        
                                                                        <cc1:ListSearchExtender ID="LSE_Dp_CodCodbranza" runat="server" IsSorted="true" PromptCssClass="LabelDrop"
                                                                            PromptPosition="Bottom" PromptText="Escriba Para Buscar" QueryPattern="Contains"
                                                                            TargetControlID="dr_suc">
                                                                        </cc1:ListSearchExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator runat="server" ID="rv_dr_tip" ControlToValidate="dr_suc"
                                                                Display="None" ErrorMessage="Debe seleccionar una Sucursal " ValidationGroup="ingreso"
                                                                Font-Size="8pt" InitialValue="0" />
                                                            <cc1:ValidatorCalloutExtender runat="Server" ID="Validatorcalloutextender1" TargetControlID="rv_dr_tip"
                                                                HighlightCssClass="validatorCalloutHighlight" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            <img src="../../../Imagenes/Infografia/EnCobranza.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                 
                                    
                                 
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    <asp:HiddenField ID="HF_Posicion" runat="server" />
                    <asp:HiddenField ID="hf_nro_pag" runat="server" />
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        TabIndex="23" ToolTip="Buscar Documentos" />
                                    
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="IB_GuardaGestion" runat="server" AlternateText="Guardar Datos"
                                        ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" TabIndex="24"
                                        ValidationGroup="ingreso" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="IB_CancelarGestion" runat="server" AlternateText="Limpiar Selección"
                                        ImageUrl="~/Imagenes/Botones/Boton_limpiar_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_limpiar_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_limpiar_in.gif';" TabIndex="25"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="lb_guarda" runat="server"></asp:LinkButton>
    
</asp:Content>

