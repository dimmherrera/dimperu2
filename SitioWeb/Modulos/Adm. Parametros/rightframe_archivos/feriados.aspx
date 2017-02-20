<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="feriados.aspx.vb" Inherits="feriados" Title="Mantenimiento Feriados" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tbody>
                    <tr>
                        <td class = "Cabecera" height="31" align="center" style="text-align: -moz-center">
                            <asp:Label ID="Label2" runat="server" Text="Mantenimiento-Administracion De Feriados" CssClass="Titulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" align="center">
                            <asp:Panel ID="Panel1" runat="server" CssClass="Contenido" Height="500px" BackImageUrl="~/Imagenes/degrade.jpg">
                                <table style="width: 100%; height: 60%">
                                    <tbody>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <asp:Calendar ID="Calen_fer" runat="server" BackColor="#FFFFCC" BorderColor="Black"
                                                    BorderStyle="Solid" CellSpacing="1" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue"
                                                    Height="259px" NextPrevFormat="FullMonth" Width="408px" 
                                                    DayNameFormat="Full" FirstDayOfWeek="Monday">
                                                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                                    <OtherMonthDayStyle BorderColor="Black" ForeColor="#999999" />
                                                    <NextPrevStyle BorderColor="Black" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                    <DayHeaderStyle BorderColor="Black" Font-Bold="True" BackColor="#CCCCCC" />
                                                    <TitleStyle BorderStyle="None" Font-Bold="True"
                                                        ForeColor="White" />
                                                    <DayStyle BorderColor="White" />
                                                </asp:Calendar>
                                            </td>
                                            <td valign="top" align="center">
                                                <table border="0" cellpadding="0" cellspacing="0" width="300">
                                                    
                                                        <tr>
                                                            <td align="center" style="text-align:-moz_center">
                                                                <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Feriados"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align:-moz_center;height:"258px">
                                                                <asp:ListBox ID="Lb_fer" runat="server" CssClass="clsTxt" Height="250px" Width="104px" >
                                                                </asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:CheckBox ID="cb_SW" runat="server" Visible="False"></asp:CheckBox>
                                                <asp:Label ID="Label1" runat="server" BackColor="White" BorderColor="Navy" Font-Bold="True"
                                                    ForeColor="Navy" Text="Para que estos cambios sean permanentes haga click en guardar"
                                                    Visible="False" Width="300px"></asp:Label>
                                            </td>
                                            <td>
                                                &#160;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;&nbsp;
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="Btn_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_IN.GIF';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="btn_gua1_Click"
                                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" __designer:dtid="281474976710732"
                                                __designer:wfdid="w4" ToolTip="Guardar Feriados"></asp:ImageButton>
                                          
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="Btn_quit" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';" OnClick="btn_eli_Click"
                                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif" __designer:dtid="281474976710734"
                                                __designer:wfdid="w5" ToolTip="Borrar Feriado"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn_lim" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="btn_limp1_Click"
                                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" __designer:dtid="281474976710738"
                                                __designer:wfdid="w6" ToolTip="Limpiar"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
      <asp:LinkButton ID="lb_eli" runat="server"></asp:LinkButton>
      <asp:LinkButton ID="lb_gua" runat="server"></asp:LinkButton>
</asp:Content>
