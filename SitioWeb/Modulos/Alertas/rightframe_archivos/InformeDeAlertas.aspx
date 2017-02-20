<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeDeAlertas.aspx.vb" Inherits="Modulos_Alertas_rightframe_archivos_InformeDeAlertas" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe de Alertas</title>
</head>
<body>

    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <rsweb:reportviewer id="ReportViewer1" runat="server" width="1200px" height="900px">
    </rsweb:reportviewer>
    
    </form>
    
</body>
</html>
