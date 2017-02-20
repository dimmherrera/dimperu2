<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Informe_Gestion.aspx.vb" Inherits="Informe_Gestion" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<html>
<head>
<base target="_self" />
<title>Informe Documentos en Cobranza</title>
</head>
<body>
<form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
<div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server"
     Width="1260px" Height="875px">
    </rsweb:ReportViewer>
</div>
    <uc1:Mensaje ID="Mensaje1" runat="server"/>
    <asp:LinkButton  ID="Lb_close" runat="server" AutoPostBack="True"></asp:LinkButton>
</form>
</body>
</html>