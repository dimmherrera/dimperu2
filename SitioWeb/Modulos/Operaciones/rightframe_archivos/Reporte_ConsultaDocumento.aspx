<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reporte_ConsultaDocumento.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_ConsultaDocumento" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title>Informe Consulta de Documentos</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ExportContentDisposition="AlwaysAttachment" Width="890px" Height="700px">
        </rsweb:ReportViewer> 
         
    </div>
    </form>
</body>
</html>
