﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformePagare.aspx.vb" Inherits="InformePagare" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server">
    <title>Informe Pagare</title>
    <base target="_self" />
</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 100%; text-align: center">        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowFindControls="false" Height="600px" ProcessingMode="Local" >
        </rsweb:ReportViewer>
    </div>
    
    </form>
    
</body>
</html>
