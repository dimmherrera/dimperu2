<%@ Page Language="VB" AutoEventWireup="false" CodeFile="gastos.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_gastos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"></base>
    <title>Gastos</title>
</head>
<body>

    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <%--*********************************************************************************************--%>
    <%--PopUp de Gastos Operaciones--%>
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
            <table id="Table16" border="0" cellpadding="1" cellspacing="2">
                <tr>
                    <td align="center" valign="middle" class="Cabecera">
                        <asp:Label ID="Label93" runat="server" CssClass="Titulos" Style="position: static"
                            Width="95%">Gastos Operacionales</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="Contenido">
                        <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                            Width="100%">
                            <cc2:TabPanel ID="TabPanel0" runat="server" HeaderText="Gastos Definidos">
                                <ContentTemplate>
                                    <table width="400" border="0" cellpadding="0" cellspacing="0" class="TD_BORDES">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Panel ID="Panel1" runat="server" Height="184px" ScrollBars="Auto" Width="641px">
                                                    <asp:GridView ID="gd_gastdef" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        Width="98%">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ch_sel" runat="server" AutoPostBack="True" OnCheckedChanged="ch_sel_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Codigo" HeaderText="Cod.Gasto">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo Gasto">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IVA" HeaderText="Afecto">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_p_0036">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="Label95" runat="server" CssClass="Label" Text="Totales"></asp:Label>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txt_total" runat="server" CssClass="clsDisabled" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc2:TabPanel>
                            <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Gastos Fijos">
                                <ContentTemplate>
                                    <table width="650" class="TD_BORDES">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label89" runat="server" CssClass="Label" Text="Monto"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txt_mto" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                                                <cc2:MaskedEditExtender ID="txt_mto_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="txt_mto">
                                                </cc2:MaskedEditExtender>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label90" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 100px">
                                                <asp:TextBox ID="txt_des" runat="server" CssClass="clsMandatorio" MaxLength="80"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:HiddenField ID="Txt_Itemgas" runat="server" />
                                            </td>
                                            <td colspan="" style="width: 100px">
                                                
                                                    
                                                    <table>
                                                    <tr>
                                                    <td>
                                                    <asp:ImageButton ID="btn_guar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Agregar_out.gif"
                                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Agregar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Agregar_out.gif';"
                                                    ToolTip="Agregar Gasto Fijo" />
                                                    </td>
                                                    <td>
                                                      <asp:ImageButton ID="btn_eli" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif"
                                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';"
                                                    ToolTip="Eliminar Gasto Fijo" />
                                                    </td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td colspan="" style="width: 100px">
                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Height="100px" Width="100%">
                                                    <asp:GridView ID="gr_gastofijo" runat="server" CssClass="formatUltcell" Width="95%">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="Ch_fijos" runat="server" AutoPostBack="True" OnCheckedChanged="Ch_fijos_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="3">
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="Label94" runat="server" CssClass="Label" Text="Totales"></asp:Label>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txt_totales" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc2:TabPanel>
                        </cc2:TabContainer>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%--<asp:Button ID="Btn_AceptarGastos" runat="server" Text="Aceptar" CssClass="boton" />--%><asp:HiddenField
                            ID="Hf_can_ddr" runat="server" />
                        <asp:HiddenField ID="HF_Id_Opn" runat="server" />
                        <asp:HiddenField ID="HF_Id_Ope" runat="server" />
                        <asp:HiddenField ID="Hf_can_doc" runat="server" />
                        <asp:ImageButton ID="Btn_AceptarGastos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_out.gif"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_out.gif';"
                            AlternateText="Aceptar" />
                        <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                            AlternateText="Volver" />
                    </td>
                </tr>
            </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_aceptargastos" />
        </Triggers>
    </asp:UpdatePanel>--%>
    
    </form>
</body>
</html>
