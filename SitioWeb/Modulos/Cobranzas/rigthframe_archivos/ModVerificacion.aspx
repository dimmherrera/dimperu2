<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModVerificacion.aspx.vb"
    Inherits="Modulos_Cobranzas_rigthframe_archivos_ModVerificacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>

    <script languaje="javascript" src="../FuncionesProvadasJS/Verificacion.js"></script>

    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

    <title>Modificación de Documentos a Verificar</title>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    
   
   <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
     EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    
    
    <table cellpadding="0" cellspacing="1" border="0">
            <tr>
                <td class="Contenido" align="center">
                    <asp:Label ID="Titulo" runat="server" CssClass="Titulos" 
                        Text="Verificación - Modificar Documentos Verificados"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Contenido" >
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="Cabecera">
                                            <asp:Label ID="Label1" runat="server" Text="Pagador" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Identificación" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" Width="74px" 
                                                            CssClass="clsDisabled"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Dv_Deu" runat="server" Style="margin-left: 0px" 
                                                            Width="27px" CssClass="clsDisabled"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Raz_Deu" runat="server" Width="512px" Style="margin-left: 34px"
                                                            CssClass="clsDisabled"></asp:TextBox>
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
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="Cabecera">
                                <asp:Label ID="Label3" runat="server" Text="Datos Pagador" CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Nombre Contacto" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="/Cargo Contacto" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="IB_Contactos" runat="server" 
                                                            ImageUrl="~/Imagenes/btn_workspace/Contactos_Out.gif" 
                                                            OnClick="IB_Contactos_Click" 
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/Contactos_Out.gif';" 
                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/Contactos_In.gif';" 
                                                            ToolTip="Contactos" />
                                                        <asp:ImageButton ID="IB_Dias_Pago" runat="server" 
                                                            ImageUrl="~/Imagenes/btn_workspace/DiaPago_Out.gif" 
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/DiaPago_Out.gif';" 
                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/DiaPago_In.gif';" 
                                                            ToolTip="Días de Pago" />
                                                        <asp:ImageButton ID="IB_Obs_Deudor" runat="server" 
                                                            ImageUrl="~/Imagenes/btn_workspace/ObsDeu_Out.gif" 
                                                            OnClick="IB_Obs_Deudor_Click" 
                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/ObsDeu_Out.gif';" 
                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/ObsDeu_In.gif';" 
                                                            ToolTip="Observación Deudor" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="Dp_Raz_Cnt" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Teléfono" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Celular" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="E-Mail" CssClass="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fono_Cnt" runat="server" CssClass="clsDisabled" TabIndex="1"
                                                            Width="85px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fax_Cnt" runat="server" CssClass="clsDisabled" TabIndex="1"
                                                            Width="98px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Style="position: static" ID="Txt_Mail_Cnt" runat="server" CssClass="clsDisabled"
                                                            Width="206px"  ReadOnly="True"></asp:TextBox>
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
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="Cabecera">
                                <asp:Label ID="Label9" runat="server" Text="Documentos" CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="Contenido">
                                <div id="GridViewDiv_Verificacion" style="overflow: scroll; width: 680px; height: 150px" align="left">
                                <asp:GridView ID="GV_DoctosVerificar" runat="server" 
                                    AutoGenerateColumns="False" CellPadding="1"
                                    CssClass="formatUltcell" EnableTheming="True" HorizontalAlign="Center" >
                                    <FooterStyle BorderStyle="Dashed" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChckB_GV_Veri" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NroDoc" HeaderText="Nro. Docto." >
                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DesTipoDoc" HeaderText="Tipo Docto." >
                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ope" HeaderText="Nro. Ope.">
                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FecVto" HeaderText="Fecha Vcto.">
                                            <ItemStyle HorizontalAlign="center" Width="80" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" HeaderText="Monto">
                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Moneda" DataField="DesTipoMon" >
                                            <ItemStyle HorizontalAlign="center" Width="80" />
                                        </asp:BoundField>
                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HF_ID_DOC" runat="server" Value="<%eval(iddsi) %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                    <RowStyle CssClass="formatUltcell" />
                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                </asp:GridView>
                                </div> 
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table cellpadding="0" cellspacing="0" width="100%" class="Contenido">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Llegada a Cobranza" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Verificación" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Fecha de Pago" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Fecha 1° Gestión" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label14" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fec_LLeg" runat="server" CssClass="clsTxt"
                                                            Style="margin-bottom: 0px" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Fec_LLeg_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_LLeg">
                                                        </cc2:MaskedEditExtender>
                                                        <cc2:CalendarExtender ID="Txt_Fec_LLeg_CalendarExtender" runat="server" CssClass="radcalendar"
                                                            Enabled="True" TargetControlID="Txt_Fec_LLeg" FirstDayOfWeek="Monday" 
                                                            Format="dd-MM-yyyy">
                                                        </cc2:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label15" runat="server" Text="Hora" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Hor_LLeg" runat="server" CssClass="clsTxt" 
                                                            onkeypress="fnTrapKD(LB_Valida_Fechas)" Width="39px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Hor_LLeg_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="Txt_Hor_LLeg">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label16" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fec_Veri" runat="server" CssClass="clsMandatorio"
                                                            Style="margin-bottom: 0px" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Veri_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Veri">
                                                        </cc2:MaskedEditExtender>
                                                        <cc2:CalendarExtender ID="Txt_Fec_Veri_CalendarExtender" runat="server" CssClass="radcalendar"
                                                            Enabled="True" TargetControlID="Txt_Fec_Veri" Format="dd-MM-yyyy" 
                                                            FirstDayOfWeek="Monday">
                                                        </cc2:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label17" runat="server" Text="Hora" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Hor_Veri" runat="server" CssClass="clsTxt" 
                                                            onkeypress="fnTrapKD(LB_Valida_Fechas)" Width="39px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Hor_Veri_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="Txt_Hor_Veri">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fec_Pag" runat="server" CssClass="clsTxt"
                                                            Style="margin-bottom: 0px" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Pag_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Pag">
                                                        </cc2:MaskedEditExtender>
                                                        <cc2:CalendarExtender ID="Txt_Fec_Pag_CalendarExtender" runat="server" CssClass="radcalendar"
                                                            Enabled="True" TargetControlID="Txt_Fec_Pag" Format="dd-MM-yyyy" PopupPosition="BottomRight"
                                                            FirstDayOfWeek="Monday">
                                                        </cc2:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fec_Ges" runat="server" CssClass="clsTxt"
                                                            Style="margin-bottom: 0px" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Ges_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Ges">
                                                        </cc2:MaskedEditExtender>
                                                        <cc2:CalendarExtender ID="Txt_Fec_Ges_CalendarExtender" runat="server" CssClass="radcalendar"
                                                            Enabled="True" TargetControlID="Txt_Fec_Ges" Format="dd-MM-yyyy" PopupPosition="BottomRight"
                                                            FirstDayOfWeek="Monday">
                                                        </cc2:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="Contenido">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label18" runat="server" Text="Est. Verif." CssClass="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="Dp_Est_Veri" runat="server" CssClass="clsMandatorio"
                                                Width="611px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label ID="Label19" runat="server" Text="Obs." CssClass="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txt_Obs_Docto" runat="server" CssClass="clsTxt" Height="49px"
                                                Width="610px" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
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
                    <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                        OnClick=" IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" ToolTip="Guardar" />
                    <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';"
                        ToolTip="Volver" />
                </td>
            </tr>
        </table>
    
    
    
        <asp:LinkButton ID="lb_id_doc" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lb_gua" runat="server" OnClick="lb_gua_Click"></asp:LinkButton>
        <asp:HiddenField ID="Txt_PosGv" runat="server" />
        <asp:HiddenField ID="HF_pos_grilla" runat="server" />
        <asp:LinkButton ID="LB_Valida_Fechas" runat="server"></asp:LinkButton>
        <uc1:Mensaje ID="Mensaje1" runat="server" />
        <asp:HiddenField ID="HF_Tip_Doc" runat="server" />
        
        
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="IB_Contactos" />
    <asp:PostBackTrigger ControlID="IB_Dias_Pago" />
    <asp:PostBackTrigger ControlID="IB_Obs_Deudor" />
    <asp:PostBackTrigger ControlID="lb_gua" />
    <asp:PostBackTrigger ControlID="IB_Volver" />
    </Triggers>
    </asp:UpdatePanel>
    
    <asp:linkbutton id="LB_Refrescar" runat="server"></asp:linkbutton>
    </form>
    
</body>
</html>
