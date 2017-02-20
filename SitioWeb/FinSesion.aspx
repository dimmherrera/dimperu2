<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FinSesion.aspx.vb" Inherits="FinSesion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sesion Expirada</title>
    <link href="CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="FuncionesJS/Ventanas.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" width="500px" cellspacing="0" cellpadding="0" align="center" border="1">
        <tr>
            <td align="center">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Logotipo_DIM.gif"></asp:Image>
            </td>
        </tr>
        <tr>
            <td align="center" height="200px">
                <img alt="" src="Imagenes/warning.gif" style="width: 160px; height: 158px" /></td>
        </tr>
        <tr>
            <td align="center" >
                <asp:Label ID="Label1" runat="server" CssClass="Label">Su sesión ha expirado o ha sido cerrada, haga clic en el botón para volver al menú de sistemas</asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center">
                <a href="javascript:window.close()">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                     onmouseout="this.src='Imagenes/Botones/Boton_volver_out.gif';" onmouseover="this.src='Imagenes/Botones/Boton_volver_in.gif';" />
                </a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
