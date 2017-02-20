<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pop_up_plaza.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_pop_up_plaza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Plazas</title>
    <base target="_self" />
     <script language="javascript">

         function Plaza(nro,dias) {
             window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_HF_IdPlaza.value = nro;
             window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_HF_Dias.value = dias;
             //window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Pza.Text = dias;
             window.dialogArguments.__doPostBack('ctl00$ContentPlaceHolder1$LB_Dias', '')
             window.close();

         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>

    <script language="javascript" src="../Funciones_modulo_js/Recaudacion.js"></script>

    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" CssClass="Contenido" Height="350px" ScrollBars="None">
                    <asp:GridView ID="gr_fact" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                        ShowHeader="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_PL_000047" HeaderText="Código">
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pal_des" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pds_ret" HeaderText="Cantidad de Días">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Selección">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Btn_ver" runat="server" ToolTip='<%# Eval("id_PL_000047") %>'
                                        ImageUrl="~/Images/bt_ver.gif" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="cabeceraGrilla" />
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
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
