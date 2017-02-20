<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reporte_SolicitudProrroga.aspx.vb" Inherits="Modulos_Prorrogas_rightframe_archivos_Reporte" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<base target=_self />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
    <table cellspacing="0">
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label1" runat="server" Text="Nómina de Documentos a Prorrogar" 
                    CssClass="SubTitulos"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="850px" 
        Width="1200px">
    </rsweb:ReportViewer>
    
            </td>
        </tr>
        <tr>
            <td align="right">
                <uc1:Mensaje ID="Mensaje1" runat="server" />
                <asp:ImageButton ID="btn_guardar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                    onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                    OnClick="btn_guardar_Click" ValidationGroup="Guardar Solicitud" Visible="false"  />
            </td>
        </tr>
    </table>
      <asp:LinkButton ID="lb_guardar" runat="server"></asp:LinkButton>
  </form>
</body>
</html>
