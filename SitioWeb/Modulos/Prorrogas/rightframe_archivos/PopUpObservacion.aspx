<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopUpObservacion.aspx.vb" Inherits="Modulos_Prorrogas_rightframe_archivos_PopUpObservacion" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<head runat="server">
    <title></title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <%--  <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>--%>

    <script src="../FuncionesProvadasJS/VBProrroga.js" type="text/javascript"></script>

    <table width="450px">
        <tr>
            <td class="Cabecera" align="center">
                <asp:Label ID="Label1" runat="server" Text="Observación" CssClass="Titulos">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt_apro" runat="server" Width="440px" CssClass="clsMandatorio"
                    Height="100px" TextMode="MultiLine" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btn_apro" runat="server" Text="Aprobar" CssClass="boton" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HF_ID" runat="server" />
    <%--    </ContentTemplate>
    <Triggers>
     <asp:PostBackTrigger ControlID="btn_apro" />
    </Triggers>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
