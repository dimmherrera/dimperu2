<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeCXC.aspx.vb" Inherits="InformeCXC" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <title>Informe de Cuentas por Cobrar</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
     EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ExportContentDisposition="AlwaysAttachment"
          Width="1270px" Height="875px" Font-Names="Arial" >
          
        </rsweb:ReportViewer>
    </div>
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
