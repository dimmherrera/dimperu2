<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeDNC.aspx.vb" Inherits="InformeDNC" title="Untitled Page" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
<title>Informe Documentos no Cedidos</title>
<base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ExportContentDisposition="AlwaysAttachment"
        width="1270px" Height="900px">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
