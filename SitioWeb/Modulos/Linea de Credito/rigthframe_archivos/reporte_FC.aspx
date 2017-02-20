<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporte_FC.aspx.vb" Inherits="Modulos_Linea_de_Credito_rigthframe_archivos_reporte_FC" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<script src="../FuncionesPrivadasJS/VidaPopup.js" type="text/javascript"></script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hoja de Firmas</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div style="width:100%">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="true" Width="100%" Height="850px">
    </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
