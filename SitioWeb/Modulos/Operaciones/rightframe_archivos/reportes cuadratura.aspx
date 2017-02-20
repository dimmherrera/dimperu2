<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="reportes cuadratura.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_reportes_cuadratura" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <body>
    <table style="width: 97%" cellspacing="0">
        <tr>
            <td class = "Cabecera" 
                width="width: 941px; height: 31px">
                <asp:Label ID="Label3" runat="server" CssClass="Titulos" 
                    Text="Informes Cuadratura"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="580" valign="top" class="Contenido">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <table cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Informes"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <asp:DropDownList ID="dr_informes" runat="server" CssClass="clsMandatorio">
                                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                        <asp:ListItem Value="1">Hoja de Negocio</asp:ListItem>
                                                         <asp:ListItem Value="2">Informe Documentos con Cobr.</asp:ListItem>
                                                         <asp:ListItem Value="3">Informe Documentos sin Cobr.</asp:ListItem>
                                                        <asp:ListItem Value="4">Informe Documentos con Resp.</asp:ListItem>
                                                         <asp:ListItem Value="5">Informe Documentos sin Resp.</asp:ListItem>
                                                         <asp:ListItem Value="6">Informe Cartera Vigente Totales.</asp:ListItem>
                                                        <asp:ListItem Value="7">Egresos con deposito antes de 14 hrs.</asp:ListItem>
                                                        <asp:ListItem Value="8">Egresos con deposito despues de 14 hrs.</asp:ListItem>
                                                        <asp:ListItem Value="9">Informe Otorgamientos Diarios.</asp:ListItem>
                                                        <asp:ListItem Value="10">Informe Operaciones Diarias Monto a Girar.</asp:ListItem>
                                                        <asp:ListItem Value="11">Informe Cuadratura Excedentes antes de 14 hrs.</asp:ListItem>
                                                        <asp:ListItem Value="12">Informe Cuadratura Excedentes despues de 14 hrs.</asp:ListItem>
                                                         <asp:ListItem Value="13">Informe de Comisiones a Facturar </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" 
                                                        Text="Fecha Proceso" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="Contenido">
                                                    <asp:TextBox ID="txt_fec" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txt_fec_CalendarExtender" runat="server" 
                                                        CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                        Format="dd-MM-yyyy" TargetControlID="txt_fec">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            &nbsp;<rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="480px" 
                                Width="100%">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:ImageButton ID="ib_buscar" runat="server" 
                    ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"   onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" ToolTip="Buscar" />
                <asp:ImageButton ID="ib_limpiar" runat="server" 
                    ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"   onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"  ToolTip="Limpiar" />
            </td>
        </tr>
    </table>
    </body>
</asp:Content>

