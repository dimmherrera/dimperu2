<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lista_Operaciones.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Lista_Operaciones" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
  <%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Consulta Operaciones </title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="730px" Width="890px">
    </rsweb:ReportViewer>
    <asp:ImageButton ID="ib_generar" runat="server" ImageUrl="~/Imagenes/Botones/hoja de ruta_out.gif"
        onmouseout="this.src='../../../Imagenes/Botones/hoja de ruta_out.gif'" onmouseover="this.src='../../../Imagenes/Botones/hoja de ruta_in.gif'"
        ToolTip="Generar Reporte" />
    <asp:HiddenField ID="rut_cli" runat="server" />
    <asp:HiddenField ID="rut_cli2" runat="server" />
    <asp:HiddenField ID="cli_rso" runat="server" />
    <asp:HiddenField ID="rut_deu" runat="server" />
    <asp:HiddenField ID="rut_deu2" runat="server" />
    <asp:HiddenField ID="deu_rso" runat="server" />
    <asp:HiddenField ID="suc" runat="server" />
    <asp:HiddenField ID="suc2" runat="server" />
    <asp:HiddenField ID="eje" runat="server" />
    <asp:HiddenField ID="eje2" runat="server" />
    <asp:HiddenField ID="fec_dde" runat="server" />
    <asp:HiddenField ID="fec_hta" runat="server" />
    <asp:HiddenField ID="n_ope1" runat="server" />
    <asp:HiddenField ID="n_ope2" runat="server" />
    <asp:HiddenField ID="nro_doc" runat="server" />
    <asp:HiddenField ID="nro_doc2" runat="server" />
    <asp:HiddenField ID="vcto_dde" runat="server" />
    <asp:HiddenField ID="vcto_hta" runat="server" />
    <asp:HiddenField ID="mon2" runat="server" />
    <asp:HiddenField ID="est_ope" runat="server" />
    <asp:HiddenField ID="est_ope2" runat="server" />
    <asp:HiddenField ID="mon" runat="server" />
    </form>
</body>
</html>
