<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Informe_Llamadas.aspx.vb"
    Inherits="Informe_Llamadas" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html>
<head runat="server">
    
    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <title>INFORME CONTROL DE LLAMADAS</title>
    <base target="_self" />
    <style type="text/css">
    .ToolbarDocMapToggle
{
    display: block;
    float: left;
}

.ToolbarPageNav
{
    display: block;
    float: left;
}

.ToolbarZoom
{
    display: block;
    float: left;
}

.ToolbarFind
{
    display: block;
    float: left;
}

.ToolbarExport
{
    display: none;
}

.ToolbarRefresh
{
    display: block;
    float: left;
}

.ToolbarPrint
{
    display: block;
    float: left;
}

.ToolbarHelp
{
    display: block;
    float: left;
}
.DocMapAndReportFrame
{
   min-height: 860px;
   min-width: 1000px;
   width:100%;
}

.msrs-contentFrame
{
   min-height: 860px;
   min-width: 1000px;
   width:100%;
}

.msrs-contentFrame .msrs-buttonHeaderBackground
{
   min-width:1000px;
   width:100%;
}
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table id="General" border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td>
                <table style="width: 1000px" border="0" cellpadding="0" cellspacing="1" class="Contenido">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label2" runat="server" Text="Criterio de Busqueda" CssClass="SubTitulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table border="1" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="RB_Mes" runat="server" Text="Por Mes" CssClass="Label" AutoPostBack="true" />
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="RB_Año" runat="server" Text="Por Año" CssClass="Label" AutoPostBack="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="1" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Mes" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_mes" runat="server" CssClass="clsMandatorio">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Año" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Año" runat="server" CssClass="clsMandatorio" MaxLength="4" Width="60px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txt_Año_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_Año">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_Año"
                                            Display="None" ErrorMessage="Fecha Erronera" MaximumValue="2999" MinimumValue="1900"></asp:RangeValidator>
                                        <cc1:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server"
                                            Enabled="True" TargetControlID="RangeValidator1">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:ImageButton ID="IB_Imprimir" runat="server" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                                            AlternateText="Imprimir" onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                                            onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" />
                                        <asp:ImageButton ID="IB_Volver" runat="server" AlternateText="Volver" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="990px" ExportContentDisposition="AlwaysAttachment">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
