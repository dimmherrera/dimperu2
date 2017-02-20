<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Informe_Negociacion.aspx.vb" Inherits="Modulos_Reportes_rigthframe_archivos_Informe_Negociacion"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>Informe de Negociación</title>
<base target="_self" />

</head>
<body>

    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div style="width:100%;text-align:center">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="850px" Height="750px" Font-Names="Verdana" Font-Size="8pt">
        </rsweb:ReportViewer>   
    </div>
    
    </form>
</body>
</html>
