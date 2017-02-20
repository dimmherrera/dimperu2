<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteIngresoNomina.aspx.vb" Inherits="Modulos_Tesorería_rightframe_archivos_ReporteIngresoNomina" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Nómina</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1100px" Height="800px" ShowRefreshButton="false">
        </rsweb:ReportViewer>
    </div>
    <asp:HiddenField ID="hf_ing" runat="server" />
     <uc3:Mensaje ID="Mensaje1" runat="server" />
     <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
    </form>
    
</body>
</html>
