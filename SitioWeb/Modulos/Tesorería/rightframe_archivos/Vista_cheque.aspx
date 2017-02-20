<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Vista_cheque.aspx.vb" Inherits="Modulos_Tesorería_rightframe_archivos_Vista_cheque" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Visualización de cheques</title>

    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
 
    <table cellpadding="0" cellspacing="0" >
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" 
                    Text="Visualización de cheques"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" height="580" valign="top" width="580">
                <table class="style1">
                    <tr>
                        <td align="center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/cheque.JPG" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/cheque 2.jpg" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                <asp:ImageButton ID="ImageButton1"  runat="server" 
                  onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" 
                    ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
