<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="gene_arch.aspx.vb" Inherits="gene_archivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellspacing="1" cellpadding="0" border="0" style="width: 100%" class="Contenido">
        <tbody>
            <tr>
                <td class="Cabecera" height="31" align="center" style="text-align: -moz-center">
                    <asp:Label ID="Label2" runat="server" Text="Mantenimiento - Generación De Archivos" CssClass="Titulos"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Contenido" style="height:580px; padding:5px" valign="top">
                
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td class="Cabecera" style="width: 100%" align="left" >
                                <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Listado de Archivos a generar"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="left">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" CssClass="Label"
                                                        Height="445px" Width="384px">
                                                        <asp:ListItem Selected="True" Value="1">Archivo de saldo Sinacofi</asp:ListItem>
                                                        <asp:ListItem Value="2">Archivo D24</asp:ListItem>
                                                        <asp:ListItem Value="3">Archivo D24 Diario</asp:ListItem>
                                                        <asp:ListItem Value="4">Archivo auditoría al dia de hoy</asp:ListItem>
                                                        <asp:ListItem Value="5">Archivo auditoría meses anteriores</asp:ListItem>
                                                        <asp:ListItem Value="6">Archivo Sinacofi 01 Cart.Jud - Castigada</asp:ListItem>
                                                        <asp:ListItem Value="7">Cartera Judicial(Sinacofi)</asp:ListItem>
                                                        <asp:ListItem Value="8">Cartera Castigada(Sinacofi)</asp:ListItem>
                                                        <asp:ListItem Value="9">Articulo 8485</asp:ListItem>
                                                        <asp:ListItem Value="10">Nivel de Riesgo al dia de Hoy</asp:ListItem>
                                                        <asp:ListItem Value="11">Nivel de Riesgo Meses Anteriores</asp:ListItem>
                                                        <asp:ListItem Value="12">P14(Estado de Colocaciones)</asp:ListItem>
                                                        <asp:ListItem Value="13">P14(Mensual)</asp:ListItem>
                                                        <asp:ListItem Value="14">P15(Composicion de las colocaciones)</asp:ListItem>
                                                        <asp:ListItem Value="15">P15(Composicion de las colocaciones mensual)</asp:ListItem>
                                                        <asp:ListItem Value="16">P16(Colocaciones por actividad Economica)</asp:ListItem>
                                                        <asp:ListItem Value="17">P16(Colocaciones por actividad Economica Mensual)</asp:ListItem>
                                                        <asp:ListItem Value="18">Historico Mensual por Cliente</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="Label" runat="server" CssClass="LabelCabeceraGrilla" Text="Periodo a buscar:"
                                                        Visible="False"></asp:Label>
                                                    <asp:Panel ID="panel" runat="server" Height="57px" Visible="False" Width="129px">
                                                        <table style="width: 100%; height: 56px;">
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Mes"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:DropDownList ID="dp_mes" runat="server" CssClass="clsMandatorio" Height="20px"
                                                                        Width="100px">
                                                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                                        <asp:ListItem Value="1">Enero</asp:ListItem>
                                                                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                                        <asp:ListItem Value="4">Abril</asp:ListItem>
                                                                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                                        <asp:ListItem Value="6">Junio</asp:ListItem>
                                                                        <asp:ListItem Value="7">Julio</asp:ListItem>
                                                                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                                        <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                                                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Año"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_ano" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                        Height="15px" MaxLength="4" Width="35px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Horizontal">
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                   <asp:ImageButton ID="Btn_Guardar" onmouseover="this.src='../../../Imagenes/Botones/boton_exportar_in.GIF';"
                    onmouseout="this.src='../../../Imagenes/Botones/boton_exportar_out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/boton_exportar_out.gif" __designer:dtid="281474976710732"
                    ToolTip="Generar Archivo"></asp:ImageButton>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
