<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="~/Modulos/Master/MasterPage.master" CodeFile="InformesGenerales.aspx.vb" Inherits="InformesGenerales" Title="Informes Generales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <script language="javascript">
function DoScroll()
 {
    var _gridView = document.getElementById("GridViewDiv");
    var _header = document.getElementById("HeaderDiv");
     _header.scrollLeft = _gridView.scrollLeft;
 }
    </script>


    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table id="Tabla Contenedora" border="0" cellpadding="0" cellspacing="1" width="100% "class="Contenido" align="center">
            <tr>
                <td class = "Cabecera" style="height:31px">
                    <asp:Label ID="Label1" runat="server" Text="Legal - Informes Generales" 
                        CssClass="Titulos"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  class="Contenido" height="590px" valign="top" align="center">
                    <table id="Tabla General" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td style="padding:5px">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="Cabecera">
                                            <asp:Label ID="Label2" runat="server" Text="Criterio de Busqueda" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" style="height:160px;padding:5px">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="700px"
                                                        Height="100px">
                                                        <cc1:TabPanel ID="Tab_TipoPagare" runat="server" HeaderText="Tipo Pagaré">
                                                            <HeaderTemplate>
                                                                Tipo Pagaré</HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="ChecSuc" runat="server" Text="Todas las Sucursales" CssClass="Label" />
                                                                                    </td>
                                                                                    <td width="200px" align="right">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Días después fecha proceso" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_Dia" runat="server" CssClass="clsMandatorio" Width="40px" MaxLength="2"
                                                                                            AutoPostBack="True"></asp:TextBox><cc1:FilteredTextBoxExtender ID="txt_Dia_FilteredTextBoxExtender"
                                                                                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Dia">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="Cabecera">
                                                                                        <asp:Label ID="Label4" runat="server" Text="Clientes" CssClass="SubTitulos"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="Contenido">
                                                                                                    <asp:RadioButtonList ID="Radio_Cliente" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                                                                                        <asp:ListItem Value="1" Selected="True">Todos</asp:ListItem>
                                                                                                        <asp:ListItem Value="2">Activos</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="Cabecera">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Tipo Pagare" CssClass="SubTitulos"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="Contenido" style="height:29px">
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:RadioButton ID="Radio_Todos" runat="server" Text="Todos" CssClass="Label" AutoPostBack="True"
                                                                                                        Checked="True" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="Drop_Est" runat="server" CssClass="clsTxt" AutoPostBack="True">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Fecha Proceso" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_FPro" runat="server" Width="90px" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Label ID="Label7" runat="server" Text="Fecha Hasta" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_FHasta" runat="server" Width="90px" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="Tab_VencPgr" runat="server" HeaderText="Venc. Pagaré">
                                                            <HeaderTemplate>
                                                                Venc. Pagaré</HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td valign="top" >
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="CheckBox_TodSuc" runat="server" Text="Todas las Sucursales" CssClass="Label" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td class="Cabecera">
                                                                                                    <asp:Label ID="Label10" runat="server" Text="Fechas" CssClass="SubTitulos"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="Contenido" style="height:30px"> 
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label11" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txt_dsd" runat="server" CssClass="clsTxt" Width="90px" AutoPostBack="false"></asp:TextBox><cc1:MaskedEditExtender
                                                                                                                    ID="txt_dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dsd">
                                                                                                                </cc1:MaskedEditExtender>
                                                                                                                <cc1:CalendarExtender ID="txt_dsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_dsd">
                                                                                                                </cc1:CalendarExtender>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label12" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txt_hst" runat="server" CssClass="clsTxt" Width="90px" AutoPostBack="false"></asp:TextBox><cc1:MaskedEditExtender
                                                                                                                    ID="txt_hst_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_hst">
                                                                                                                </cc1:MaskedEditExtender>
                                                                                                                <cc1:CalendarExtender ID="txt_hst_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_hst">
                                                                                                                </cc1:CalendarExtender>
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
                                                                        <td valign="top">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="Cabecera">
                                                                                        <asp:Label ID="Label8" runat="server" Text="Clientes" CssClass="SubTitulos"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="Contenido">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:RadioButtonList ID="RB_EstCli" runat="server" RepeatDirection="Horizontal" CssClass="Label"
                                                                                                        AutoPostBack="True">
                                                                                                        <asp:ListItem Selected="True" Value="1">Todos</asp:ListItem>
                                                                                                        <asp:ListItem Value="2">Activos</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="Cabecera">
                                                                                        <asp:Label ID="Label9" runat="server" Text="Ejecutivos" CssClass="SubTitulos"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="Contenido" style="height:29px">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:RadioButton ID="RB_Eje" runat="server" Text="Todos" CssClass="Label" Checked="True"
                                                                                                        AutoPostBack="True" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="Drop_Eje" runat="server" CssClass="clsTxt" AutoPostBack="True">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    </cc1:TabContainer>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <%-- Width="1191px" se cambia dimensiones de panel que contiene gv --%>
                                                        <asp:Panel ID="Panel_Gr_PGR" runat="server" Width="1380px" Height="305px" ScrollBars="Horizontal">
                                                            <asp:GridView ID="Gr_PGR" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                ShowHeader="True" Width="1380px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Rut_Cliente" HeaderText="NIT Cliente">
                                                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Razon_Social" HeaderText="Razón Social">
                                                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FechaVecto" HeaderText="Fec. Venc." DataFormatString="{0:dd/MM/yyyy}">
                                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="NumeroDocto" HeaderText="Nº Pgr.">
                                                                        <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Tipo_Pagare" HeaderText="Tipo">
                                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Mandato" HeaderText="Mandato">
                                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Deuda_Total" HeaderText="Deuda Total">
                                                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <%--</div>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
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
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:HiddenField ID="HF_Pos" runat="server" />
                    <asp:HiddenField ID="HF_Id" runat="server" />
                    <asp:ImageButton ID="IB_Buscar" runat="server" AlternateText="Buscar" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                    <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprimir" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif" Enabled="false"
                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" />
                    <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/boton_limpiar_out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="IB_Imprimir" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>