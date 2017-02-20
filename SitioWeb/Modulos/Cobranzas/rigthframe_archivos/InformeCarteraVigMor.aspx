<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeCarteraVigMor.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_InformeCarteraVigMor" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe Cartera Vigente/Morosa</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" BorderStyle="None"
         ExportContentDisposition="AlwaysAttachment" Font-Names="Verdana" 
        Font-Size="8pt" Height="750px" 
        InternalBorderColor="White" ShowDocumentMapButton="False" 
        Width="1190px">
        <LocalReport ReportPath="Modulos\Cobranzas\Reportes\ReporteVerificacion.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
    <uc1:Mensaje ID="Mensaje1" runat="server"/>
    <asp:LinkButton  ID="Lb_close" runat="server" AutoPostBack="True"></asp:LinkButton> 
    </form>
</body>
</html>
