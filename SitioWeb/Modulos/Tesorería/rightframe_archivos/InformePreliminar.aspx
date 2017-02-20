<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="InformePreliminar.aspx.vb" Inherits="Modulos_Tesorería_rightframe_archivos_Default" title="Página sin título" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <table style="width: 100%; height: 154px">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" 
                                                Text="Fecha Negociación"></asp:Label>
                                        </td>
                                        <td style="width: 485px">
                                            &nbsp;</td>
                                        <td style="width: 485px">
                                            &nbsp;</td>
                                        <td style="width: 85px">
                                            <asp:Label ID="Label5" runat="server" CssClass="LabelCabeceraGrilla" 
                                                Text="Ejecutivo"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fec_desde" runat="server" CssClass="clsMandatorio" 
                                                            Width="100px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txt_fec_desde_CalendarExtender" runat="server" 
                                                            Enabled="True" TargetControlID="txt_fec_desde" FirstDayOfWeek="Monday" 
                                                            Format="dd-MM-yyyy">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="txt_fec_desde_MaskedEditExtender" runat="server" 
                                                            TargetControlID="txt_fec_desde" Mask="99/99/9999" MaskType="Date">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fec_hasta" runat="server" CssClass="clsMandatorio" 
                                                            Width="102px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txt_fec_hasta_CalendarExtender" runat="server" 
                                                            Enabled="True" TargetControlID="txt_fec_hasta" FirstDayOfWeek="Monday" 
                                                            Format="dd-MM-yyyy">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="txt_fec_hasta_MaskedEditExtender" runat="server" 
                                                            TargetControlID="txt_fec_hasta" Mask="99/99/9999" MaskType="Date">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left" style="width: 485px">
                                            &nbsp;</td>
                                        <td align="left" style="width: 485px">
                                            &nbsp;</td>
                                        <td style="width: 85px">
                                            <asp:RadioButton ID="RadioButton1" runat="server" CssClass="Label" 
                                                Text="Todos" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dp_ejecutivos" runat="server" CssClass="clsMandatorio" 
                                                Height="16px" Width="227px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="480px" 
                                Width="100%">
                            </rsweb:ReportViewer>
                                <br />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 721px">
                                            &nbsp;</td>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 104px">
                                                        &nbsp;</td>
                                                    <td>
                <asp:ImageButton ID="ib_buscar" runat="server" 
                    ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"   onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" ToolTip="Buscar" />
                                                    </td>
                                                    <td>
                <asp:ImageButton ID="ib_limpiar" runat="server" 
                    ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"   onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"  ToolTip="Limpiar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>

