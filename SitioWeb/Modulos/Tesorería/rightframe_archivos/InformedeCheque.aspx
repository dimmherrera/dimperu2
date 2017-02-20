<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformedeCheque.aspx.vb" Inherits="Modulos_ArqueoDeCheques_rightframe_archivos_InformedeCheque" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <title>Informe de Arqueo</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div>
        <rsweb:reportviewer id="ReportViewer1" runat="server" exportcontentdisposition="AlwaysAttachment"
            width="1300px" height="800px" font-names="Verdana" font-size="8pt" 
            ShowRefreshButton="False">
        </rsweb:reportviewer>
    </div>
    </form>
</body>
</html>
