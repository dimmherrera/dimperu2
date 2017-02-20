<%@ Page Language="VB" AutoEventWireup="false"  CodeFile="ReporteOtg.aspx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_ReporteOtg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Evaluación Cliente / Deudor</title>
    <base target="_self" />

    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>

    <script src="../../Carp.%20Comercial/FuncionesPrivadasJS/Exportacion.js" type="text/javascript"></script>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            width: 101px;
        }
        .style4
        {
            border-bottom: solid 1px #666666;
            border-right: solid 1px #666666;
            border-left: solid 1px #666666;
            filter: progid:dximagetransform.microsoft.gradient(gradienttype=0,startcolorstr=#F6F9FC,endcolorstr=white);
            height: 111px;
            width: 352px;
        }
        #Table1
        {
            width: 337px;
            height: 105px;
        }
        #tb_gral
        {
            width: 37%;
        }
        .style6
        {
            width: 4px;
        }
        .style7
        {
            width: 352px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <table id="tb_gral" cellspacing="0" cellpadding="0" border="0" >
        <tr>
            <td class="style4" style="padding: 10px">
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="Contenido">
                            <tbody>
                                <tr>
                                    <td align="left" class="Cabecera">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" 
                                            Text="Reportes Otorgamiento"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" style="height: 100px" valign="middle">
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 298px; height: 34px;">
                                            <tbody>
                                                <tr>
                                                    <td align="right" class="style6">
                                                        &nbsp;</td>
                                                    <td align="left" class="style3">
                                                        <asp:Label ID="Label42" runat="server" __designer:wfdid="w285" CssClass="Label" 
                                                            Text="Tipo de Reporte"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DP_Reporte" runat="server" clsTxt="clsMandatorio" 
                                                            CssClass="clsMandatorio" Width="150px">
                                                            <asp:ListItem>Seleccionar</asp:ListItem>
                                                            <asp:ListItem>Liquidación de operaciones</asp:ListItem>
                                                            <asp:ListItem>Evaluación cliente deudor</asp:ListItem>
                                                            <asp:ListItem>Negociación</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            </table>
            </td>
        </tr>
              <tr>
                <td align="right" class="style7">

                        <asp:ImageButton Style="position: static" ID="IB_Informe" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                        onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" 
                        runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"  />
                        
                </td>
            </tr>
        
</table>
    
    <asp:HiddenField ID="HF_NroEva" runat="server" />
    </form>
</body>

</html>
