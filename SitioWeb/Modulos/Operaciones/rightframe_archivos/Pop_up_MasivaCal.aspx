<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pop_up_MasivaCal.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Pop_up_MasivaCal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calificacion de Arrastre</title>
     <link href="../../../CSS/Estilos.css" rel="Stylesheet" type="text/css" />
     <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server" class="Label">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="overflow:auto">
               <table id="Tabla1" runat="server" width="400px">
                            <tr>
                                <td class="Cabecera" width="400px">
                                    <asp:Label ID="Label1" runat="server" Text="Calificación de Arrastre" CssClass="Titulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" align="center" style="text-align: -moz-center">
                                    <table>
                                        <tr>
                                            <td>
                                                <table width="400px">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Archivo"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:FileUpload ID="FileUpload_Cal" runat="server" CssClass="Label" />
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td align="right" height="15px">
                                               <asp:Button ID="Btn_Aceptar" runat="server" Text="Aceptar" CssClass="boton"/>
                                               <asp:Button ID="Btn_Volver" runat="server" Text="Volver" CssClass="boton" />
                                           </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
    </div>
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
