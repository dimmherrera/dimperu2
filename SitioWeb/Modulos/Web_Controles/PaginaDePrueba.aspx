<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PaginaDePrueba.aspx.vb" Inherits="Modulos_Web_Controles_PaginaDePrueba" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="WC_QuePaga.ascx" tagname="WC_QuePaga" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <base target="_self" />
    <title>Documentos por Pagar</title>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
     EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
        
        <uc1:WC_QuePaga ID="WC_QuePaga1" runat="server" />
        
     </div>
    </form>
</body>
</html>
