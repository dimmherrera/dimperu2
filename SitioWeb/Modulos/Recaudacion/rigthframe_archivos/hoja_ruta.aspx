<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    Title="Emision Hojas de Ruta" CodeFile="hoja_ruta.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_hoja_ruta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Funciones_modulo_js/Recaudacion.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; height: 550px" cellpadding="0" cellspacing="1" class="Contenido">
                <tr>
                    <td class="Cabecera" style="width: 100%;height:31px" align="center">
                        <asp:Label ID="Label16" runat="server" CssClass="Titulos" Text="Recaudación-Hoja de Ruta"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" height="590px" valign="top" style="padding: 5px; width: 100%"
                        align="center">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 100%">
                                    <table style="width: 850px" cellspacing="0">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label17" runat="server" CssClass="SubTitulos" Text="Datos de Hoja de Ruta"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" align="left">
                                                <table>
                                                    <tr>
                                                        <td style="width: 120px">
                                                            <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Sucursales" Visible="False"></asp:Label>
                                                            <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Fecha de Generación"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <nobr>
                                                <asp:TextBox ID="txt_fec" runat="server" AutoPostBack="True" 
                                                    CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="txt_fec_MaskedEditExtender" runat="server" 
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec">
                                                </cc1:MaskedEditExtender>
                                                <cc1:CalendarExtender ID="txt_fec_CalendarExtender" runat="server" 
                                                    CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                                    TargetControlID="txt_fec">
                                                </cc1:CalendarExtender>
                                                <nobr>
                                                <asp:RequiredFieldValidator ID="rv_dr_tip1" runat="server" 
                                                    ControlToValidate="txt_fec" Display="None" 
                                                    ErrorMessage="&lt;b&gt;Clasificación&lt;/b&gt;&lt;br /&gt;Ingrese una fecha " 
                                                    Font-Size="8pt" ValidationGroup="ingreso" />
                                                <cc1:ValidatorCalloutExtender ID="rv_dr_tip1_ValidatorCalloutExtender" 
                                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                                    TargetControlID="rv_dr_tip1" />
                                                </nobr>
                                                </nobr>
                                                        <asp:CheckBox ID="ch_suc" runat="server" CssClass="Label" Text="Todas" Width="200px"
                                                                Visible="False" />
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
                                                            <asp:Label ID="Label25" runat="server" CssClass="Label" Text="Horario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="RB_HORA" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                                                                <asp:ListItem Value="A">AM</asp:ListItem>
                                                                <asp:ListItem Value="P">PM</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Zonas"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <nobr>
                                        <asp:DropDownList ID="dr_zona" runat="server" AutoPostBack="True" 
                                            CssClass="clsTxt" Width="220px">
                                        </asp:DropDownList>
                                        <asp:RadioButton ID="rb_zona" runat="server" AutoPostBack="True" 
                                            Checked="True" CssClass="Label" Text="Todas" />
                                        </nobr>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label1" runat="server" Text="Recaudador" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="Drop_Recaudador" runat="server" CssClass="clsMandatorio" Width="280px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <table cellspacing="0" cellpadding="3" width="850">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Recaudaciones AM"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="Contenido">
                                                <asp:Panel ID="Panel1" runat="server" Height="160px" ScrollBars="Vertical">
                                                    <asp:GridView ID="gr_rec_am" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                        CssClass="formatUltcell">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Btn_verAM" runat="server" ToolTip='<%# Eval("id_eje") %>' 
                                                                        ImageUrl="~/Images/bt_ver.gif" onclick="Btn_verAM_Click" style="height: 13px" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="suc_des_cra" HeaderText="Sucursal">
                                                                <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_eje" HeaderText="Código">
                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="eje_nom" HeaderText="Recaudadores">
                                                                <ItemStyle Width="230px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deudores" HeaderText="Cant.Pagadores">
                                                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="documentos" HeaderText="Cant.Documentos">
                                                                <ItemStyle Width="120px" HorizontalAlign="Right" />
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
                                <td align="center">
                                    <br />
                                    <table cellspacing="0" cellpadding="3" width="850">
                                        <tr>
                                            <td class="Cabecera" align="left">
                                                <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Recaudaciones PM"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="Contenido">
                                                <asp:Panel ID="Panel2" runat="server" Height="160px" ScrollBars="Vertical">
                                                    <asp:GridView ID="gr_rec_pm" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                        CssClass="formatUltcell">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Btn_verPM" runat="server" ToolTip='<%# Eval("id_eje") %>' 
                                                                        ImageUrl="~/Images/bt_ver.gif" onclick="Btn_verPM_Click" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="suc_des_cra" HeaderText="Sucursal">
                                                                <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_eje" HeaderText="Código">
                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="eje_nom" HeaderText="Recaudadores">
                                                                <ItemStyle Width="230px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deudores" HeaderText="Cant.Pagadores">
                                                                <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="documentos" HeaderText="Cant.Documentos">
                                                                <ItemStyle Width="120px" HorizontalAlign="Right" />
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="Desmarque" runat="server"></asp:LinkButton>
                                    <asp:HiddenField ID="pos_am" runat="server" />
                                </td>
                                <td>
                                    <asp:HiddenField ID="pos_pm" runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                        ValidationGroup="ingreso" ToolTip="Buscar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_rpt" runat="server" ImageUrl="~/Imagenes/Botones/btn_hoja-de-ruta_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/btn_hoja-de-ruta_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/btn_hoja-de-ruta_in.gif';"
                                        ToolTip="Informe Hoja de Ruta" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel1">
                <ProgressTemplate>
                    <uc1:Cargando ID="Cargando1" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_rpt" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
