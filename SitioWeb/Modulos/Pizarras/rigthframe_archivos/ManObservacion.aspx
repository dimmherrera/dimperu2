<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManObservacion.aspx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_ManObservacion" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OBSERVACIONES</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <base target="_self" />
</head>

<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table16" border="0" cellpadding="1" cellspacing="2" width="450">
                <tr>
                    <td align="center" style="height: 37px" valign="middle" class="Cabecera">
                        <asp:Label ID="Label_Obs" runat="server" CssClass="Titulos">Observación</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:TextBox ID="Txt_Observacion" runat="server" CssClass="clsMandatorio" Height="70px"
                            TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="Btn_GuardarObs" runat="server" Text="Aprobar" CssClass="boton" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_NroNeg" runat="server" />
            <asp:HiddenField ID="HF_Estado" runat="server" />
            <asp:HiddenField ID="HF_Clasificacion" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Accion" runat="server"></asp:LinkButton>
    </form>
    
</body>
</html>
