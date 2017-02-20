<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Asg_Anterior.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Asg_Anterior" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión Anterior</title>
    <base target="_self"/>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <%--<script language="javascript" src="../FuncionesProvadasJS/Asg_Anterior.js"></script>--%>

    <script src="../../../FuncionesJS/Ajax.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" >
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            
            <table border=0 cellpadding=0 cellspacing=0>
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label9" runat="server" CssClass="Titulos" Text="Gestión Anterior"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px">
                        <table border="0" cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    <table cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:CheckBox ID="CB_Deudores" runat="server" CssClass="SubTitulos" AutoPostBack="true"
                                                    Text="Pagador"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rut_Deu" runat="server" CssClass="clsDisabled" Width="90px"
                                                                ></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Dig_Deu" runat="server" CssClass="clsDisabled" Width="20px"
                                                                MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Pagador" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                Width="20px" Enabled="False" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" Width="250px"
                                                                 ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:CheckBox ID="CB_FechaAsignacion" runat="server" CssClass="SubTitulos" Text="Fecha Asignación"
                                                    AutoPostBack="true"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td align="right" width="93">
                                                            <asp:Label ID="Label7" runat="server" Text="Fecha Desde" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtFechaAsig1" runat="server" CssClass="clsDisabled" Width="90px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="TxtFechaAsig1_CalendarExtender" runat="server" Enabled="False"
                                                                TargetControlID="TxtFechaAsig1" Format="dd-MM-yyyy" FirstDayOfWeek="Monday" CssClass="radcalendar">
                                                            </cc1:CalendarExtender>
                                                            <cc1:MaskedEditExtender ID="TxtFechaAsig1_MEExt" runat="server" TargetControlID="TxtFechaAsig1"
                                                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                AutoComplete="False" />
                                                        </td>
                                                        <td align="right" width="90">
                                                            <asp:Label ID="Label8" runat="server" Text="Fecha Hasta" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtFechaAsig2" runat="server" CssClass="clsDisabled" Width="90px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="TxtFechaAsig2_CalendarExtender" runat="server" Enabled="False"
                                                                TargetControlID="TxtFechaAsig2" Format="dd-MM-yyyy" FirstDayOfWeek="Monday" CssClass="radcalendar"
                                                                PopupPosition="Left">
                                                            </cc1:CalendarExtender>
                                                            <cc1:MaskedEditExtender ID="TxtFechaAsig2_MEExt" runat="server" TargetControlID="TxtFechaAsig2"
                                                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                AutoComplete="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td height="210px">
                                            <asp:Panel ID="Panel_grilla" runat="server" Height="200px" Width="800px" ScrollBars="Vertical">
                                                <asp:GridView ID="GridView1" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                    ShowHeader="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="RutDeudor" HeaderText="NIT Pagador">
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NombreDeudor" HeaderText="Razón Social">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodEjeRasig" HeaderText="Eje.Reasignado">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodEjeAnterior" HeaderText="Eje. Anterior">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaAsig" HeaderText="Fecha Asig.">
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodQuienRealiza" HeaderText="Quien Realiza">
                                                            <ItemStyle Width="200px" />
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="right">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" AlternateText="Buscar"></asp:ImageButton>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" AlternateText="Limpiar Selección">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" AlternateText="Volver"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
            </table>
            
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
