<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pop_up_gest_hoy.aspx.vb"
    Inherits="Modulos_Recaudacion_rigthframe_archivos_pop_up_gest_hoy" Title="Gestion de hoy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" src="../Funciones_modulo_js/Recaudacion.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td height="500" valign="top">
                        <table>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Sucursales"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <asp:CheckBox ID="ch_suc" runat="server" CssClass="Label" Text="Todas" 
                                                    Width="120px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label19" runat="server" CssClass="SubTitulos" Text="Horario"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <asp:RadioButtonList ID="RB_HORA" runat="server" CssClass="Label" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="A">AM</asp:ListItem>
                                                    <asp:ListItem Value="P">PM</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Pagadores"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                              <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" CssClass="Contenido" Height="170px" ScrollBars="Horizontal"
                                                    Width="970px">
                                                    <asp:GridView ID="gr_deudores" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Btn_VerDeu" runat="server" 
                                                                        ToolTip='<%# Eval("deu_rso") %>' ImageUrl="~/Images/bt_ver.gif" 
                                                                        onclick="Btn_VerDeu_Click" style="height: 13px" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="id_eje_cob" HeaderText="Codigo Rec.">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_cco" HeaderText="Cod Cobr.">
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deu_rso" HeaderText="Pagador">
                                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="zon_des" HeaderText="Zona">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cant_doctos" HeaderText="# Doctos">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="monto" HeaderText="Total Doctos">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="gsn_hor_pag_dde" DataFormatString="{0:hh:mm}" HeaderText="Hora Desde">
                                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="gsn_hor_pag" DataFormatString="{0:hh:mm}" HeaderText="Hora Hasta">
                                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                                                <asp:ImageButton ID="IB_Prev_Deu" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                    AlternateText="Anterior" />
                                                <asp:ImageButton ID="IB_Next_Deu" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                    AlternateText="Siguiente" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Recaudadores"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                       
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" CssClass="Contenido" Height="170px" ScrollBars="Horizontal"
                                                    Width="970px">
                                                    <asp:GridView ID="gr_recaudadores" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="true" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Btn_VerRec" runat="server" 
                                                                        ToolTip='<%# Eval("id_eje") %>' ImageUrl="~/Images/bt_ver.gif" 
                                                                        onclick="Btn_VerRec_Click" style="height: 13px" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="suc_des_cra" HeaderText="Sucursal">
                                                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_eje" HeaderText="Código">
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="eje_nom" HeaderText="Recaudadores">
                                                                <ItemStyle HorizontalAlign="Center" Width="160px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deudores" HeaderText="Cant. Pagadores">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="documentos" HeaderText="Cant. Documentos">
                                                                <ItemStyle HorizontalAlign="Right" Width="160px" />
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
                                                <asp:ImageButton ID="IB_Prev_Recau" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                    AlternateText="Anterior" />
                                                <asp:ImageButton ID="IB_Next_Recau" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                    AlternateText="Siguiente" />
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
                                                <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';"
                                                    ToolTip="Buscar" ValidationGroup="ingreso" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                                                    ToolTip="Guardar" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btn_asoc" runat="server" ImageUrl="~/Imagenes/Botones/boton_asociar_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/Botones/boton_asociar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_asociar_in.gif';"
                                                    ToolTip="Asociar" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                                    ToolTip="Limpiar" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/boton_volver_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/Botones/boton_volver_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_volver_in.gif';"
                                                    ToolTip="Volver" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="pos_rec" runat="server" />
            <asp:HiddenField ID="pos_deu" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_volver" />
        </Triggers>
    </asp:UpdatePanel>
    
    <%-- <uc1:Mensaje ID="Mensaje1" runat="server" />--%>
    </form>
</body>
</html>
