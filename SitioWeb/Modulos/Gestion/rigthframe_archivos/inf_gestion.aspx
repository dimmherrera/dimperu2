00<%@ Page Language="VB" AutoEventWireup="false" CodeFile="inf_gestion.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_inf_gestion" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informes de Gestión</title>
    <base target=_self></base>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div  style="overflow:auto">
     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1100px" 
        Height="1000px">
    </rsweb:ReportViewer>
    </div>
   
    </form>
</body>
</html>
