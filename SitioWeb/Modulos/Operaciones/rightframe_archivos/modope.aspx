<%@ Page Language="VB" AutoEventWireup="false" CodeFile="modope.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_ModOpe_otor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modificar Datos Operación</title>

<link href="../../../CSS/Estilos.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>
<base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
        EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="Cabecera">
                                <asp:Label ID="Label5" runat="server" CssClass="Titulos" 
                                    Text="Datos Operación"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="Contenido">
                      
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label35" runat="server" CssClass="Label" Text="Con Recurso"></asp:Label></td>
                                        <td>
                                            <asp:RadioButtonList ID="rb_res" runat="server" CssClass="Label" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Si</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" CssClass="Label" 
                                                Text="Carac.Operación"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="clsMandatorio" 
                                                Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" CssClass="Label" 
                                                Text="Sucursal Operación"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DP_Sucursal" runat="server" Width="300px" 
                                                CssClass="clsMandatorio">
                                            </asp:DropDownList>
                                          <%--  <cc1:listsearchextender id="ListSearchExtender3" runat="server" issorted="true" promptcssclass="LabelDrop"
                                                promptposition="top" prompttext="Buscar..." querypattern="Contains" targetcontrolid="DP_Sucursal">
                                            </cc1:listsearchextender>--%>
                                        </td>
                                    </tr>
                                </table>
                   
                  </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table>
                                    <tr>
                                        <td style="width: 80px; height: 31px" valign="bottom">
                                            <asp:ImageButton ID="btn_acepta" runat="server" BorderColor="Black" 
                                             ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                             onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                                             onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" /></td>
                                        <td style="width: 80px; height: 31px" valign="bottom">
                                            <asp:ImageButton ID="Btn_Cerrar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" TabIndex="26" 
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" />       
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                       
                      
            <asp:LinkButton ID="Msje" runat="server" TabIndex="53"></asp:LinkButton>
                      <asp:HiddenField ID="txt_itemope" runat="server" />
                      <uc1:Mensaje ID="Mensaje1" runat="server" />  
                
                      </ContentTemplate>
            </asp:UpdatePanel>
   
    </form>
</body>
</html>
