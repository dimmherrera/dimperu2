<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ObservacionVB.aspx.vb" Inherits="ObservacionVB" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OBSERVACIONES</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <base target=_self />
</head>

<body>
    
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    
    <table id="Table16" border="0" cellpadding="1" cellspacing="0" width="450">
        <tr>
            <td align="center" valign="middle" class="Cabecera">
                <asp:Label ID="Label_Obs" runat="server" CssClass="Titulos">Observación</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="Contenido">
                <asp:TextBox ID="Txt_Observacion" runat="server" CssClass="clsMandatorio" Height="70px"
                    TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:ImageButton ID="Btn_GuardarObs" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" AlternateText="Guardar Observación de Aplicación"
                    ToolTip="Guardar Observación de Aplicación"></asp:ImageButton>
                    
                <asp:ImageButton ID="Btn_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" AlternateText="Guardar Observación de Aplicación"
                    ToolTip="Volver"></asp:ImageButton>
            </td>
        </tr>
    </table>
    
        <uc1:Mensaje ID="Mensaje1" runat="server" />
    
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Btn_Volver" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:HiddenField ID="HF_NroApl" runat="server" />
    <asp:HiddenField ID="HF_Estado" runat="server" />
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>

    </form>
</body>
</html>
