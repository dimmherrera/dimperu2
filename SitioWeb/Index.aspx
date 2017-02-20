<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Index.aspx.vb" Inherits="Index" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="Modulos/Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>DESCUENTO TITULO VALOR</title>
    <link href="CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script src="FuncionesJS/Ventanas.js" type="text/javascript"></script>
    
   
    <script language="javascript" type="text/javascript">

        window.moveTo(0, 0);
        window.resizeTo(screen.availWidth, screen.availHeight);
        
    </script>

</head>
<body>

    <form id="form1" runat="server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <uc1:Mensaje ID="Mensaje1" runat="server" />
   
    </form>
    
</body>
</html>
