<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Popup_ver_Actas.aspx.vb"
    Inherits="Modulos_Operaciones_rightframe_archivos_Popup_ver_Actas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actas por Lineas</title>
    <link href="../../../CSS/Estilos.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" src="../FuncionesPrivadasJS/Empresas.js"></script>

</head>
<body>
    <form id="form1" runat="server" style="padding: 5px">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <contenttemplate>
    <table width="600px">
    <tr>
    <td class="Cabecera" width="600px">
       <asp:Label ID="Label1" runat="server" Text="Actas Asociadas al Cliente" CssClass="Titulos"></asp:Label>
    </td>
    </tr>
    <tr>
       <td class="Contenido" align="center" style="text-align:-moz-center">
       <table>
       <tr>
       <td>
          <table width="600px">
          <tr>
             <td>
                 <asp:Panel runat="server" ScrollBars="Auto" Width="600px" Height="180px">
                     <asp:GridView ID="GV_Actas" runat="server" AutoGenerateColumns="false" CssClass="formatUltcell" Width="98%">
                     <Columns>
                        <asp:TemplateField>
                             <ItemTemplate>
                                <asp:ImageButton ID="IB_Ver" runat="server" ToolTip='<%# Eval("act_img_id") %>' 
                                     ImageUrl="~/images/lupa.gif" onclick="IB_Ver_Click" />
                             </ItemTemplate>
                             <ItemStyle Width="1px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="act_img_id" HeaderText="ID">
                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="id_ldc" HeaderText="Nº Linea">
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="act_img_desc" HeaderText="Descripcion">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
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
       <td align="right" height="50px">
           <asp:TextBox ID="Txt_id_cli" runat="server" Enabled="False" Visible="False" 
               Width="1px"></asp:TextBox>
           <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
            onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" Style="position: static" ToolTip="Volver" Height="25px"/>
       </td>
       </tr>
       </table>
       </td>
    </tr>
    </table>
    </contenttemplate>
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
