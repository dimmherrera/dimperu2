﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeDePagos.aspx.vb" Inherits="Modulos_Pagos_rightframe_archivos_InformeDePagos" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe de Pagos</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server"
        ExportContentDisposition="AlwaysAttachment" 
        Font-Names="Verdana" 
        Font-Size="8pt" 
        Height="800px" 
        Width="1200px">
    </rsweb:ReportViewer>
    </form>
</body>
</html>