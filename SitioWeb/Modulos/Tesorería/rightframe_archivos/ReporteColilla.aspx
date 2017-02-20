<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteColilla.aspx.vb" Inherits="Modulos_Tesorería_rightframe_archivos_ReporteColilla" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colilla Deposito</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ExportContentDisposition="AlwaysAttachment"
         Width="1000px" Height="800px">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
