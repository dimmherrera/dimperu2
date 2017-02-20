<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pop_up_DoctoDig.aspx.vb"
    Inherits="Modulos_Operaciones_rightframe_archivos_Pop_up_DoctoDig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Documentos Digitalizados por Operacion</title>
    <link href="../../../CSS/Estilos.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
           <table width="600px">
             <tr>
                <td class="Cabecera" width="600px">
                   <asp:Label ID="Label1" runat="server" CssClass="Titulos" Text="Documentos Digitalizados por Operacion"></asp:Label>
                </td>
             </tr>
             <tr>
                <td class="Contenido" align="left" style="text-align:-moz-center">
                   <table>
                         <tr>
                            <td align="left">
                              <table>
                                 <tr>
                                   <td>
                                     <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Archivo"></asp:Label>
                                     </td>
                                     <td>
                                        <asp:FileUpload ID="Fileupload_Docto" runat="server" Enabled="false" 
                                             CssClass="clsDisabled" />
                                        <asp:TextBox ID="Txt_id_ope" runat="server" Visible="false"></asp:TextBox> 
                                     </td>
                                 </tr>
                                 <tr>
                                    <td align="left">
                                      <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Descripcion" 
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td>   
                                      <asp:TextBox ID="Txt_descr" runat="server" CssClass="clsMandatorio" 
                                            Visible="False" Width="325px"></asp:TextBox>
                                   </td>
                                 </tr>
                               </table>
                            </td>
                         </tr>
                         <tr>
                            <td width="600px">
                               <asp:Panel runat="server" ScrollBars="Auto" Width="600px" Height="200px">
                                   <asp:GridView ID="GV_Docto" runat="server" AutoGenerateColumns="false" CssClass="formatUltcell" Width="98%">
                                       <Columns>
                                               <asp:TemplateField>
                                                   <ItemTemplate>
                                                     <asp:ImageButton ID="IB_Delete" runat="server"
                                                          ToolTip='<%# Eval("doc_dig_id") %>' ImageUrl="~/images/cerrar.jpg" 
                                                           onclick="IB_Delete_Click"/>
                                                     <cc1:ConfirmButtonExtender ID="Conf_IB_Delete" runat="server" ConfirmText="¿Desea Eliminar el Documento?" TargetControlID="IB_Delete"></cc1:ConfirmButtonExtender>                                                           
                                                   </ItemTemplate>
                                                   <ItemStyle Width="1px" HorizontalAlign="Center" />
                                               </asp:TemplateField>
                                               <asp:BoundField DataField="doc_dig_id" HeaderText="ID">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                               </asp:BoundField>
                                               <asp:BoundField DataField="id_ope" HeaderText="Nº Operacion">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                               </asp:BoundField>
                                               <asp:BoundField DataField="doc_dig_desc" HeaderText="Descripcion">
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                               </asp:BoundField>
                                               <asp:TemplateField>
                                                   <ItemTemplate>
                                                     <asp:ImageButton ID="IB_Ver" runat="server" ToolTip='<%# Eval("doc_dig_id") %>' 
                                                             ImageUrl="~/images/bt_ver.gif" onclick="IB_Ver_Click" />
                                                   </ItemTemplate>
                                                   <ItemStyle Width="1px" HorizontalAlign="Center" />
                                               </asp:TemplateField>
                                       </Columns>
                                       <HeaderStyle CssClass="cabeceraGrilla" />
                                       <RowStyle CssClass="formatUltcell" />
                                       <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                   </asp:GridView>
                               </asp:Panel>
                            </td> 
                         </tr>
                   </table>
                </td>
             </tr>
             <tr>
                <td align="right" height="50px">
                   <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                   onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" Style="position: static" ToolTip="Nuevo" Height="25px" Enabled="false" />
                   <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Cli_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Cli_Out.gif';"
                   onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_Cli_In.gif';" Style="position: static" ToolTip="Guardar" Height="25px" Enabled="false"/>
                   <cc1:ConfirmButtonExtender ID="Conf_IB_Guardar" runat="server" ConfirmText="¿Desea Guardar el Documento?" TargetControlID="IB_Guardar"></cc1:ConfirmButtonExtender>
                   <asp:ImageButton ID="IB_Cancelar" runat="server" ImageUrl="~/Imagenes/Botones/boton_cancelar_out.gif" onmouseout="this.src='../../../Imagenes/Botones/boton_cancelar_out.gif';"
                   onmouseover="this.src='../../../Imagenes/Botones/boton_cancelar_in.gif';" Style="position: static" ToolTip="Cancelar" Height="25px" Enabled="false"/>
                   <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                   onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" Style="position: static" ToolTip="Volver" Height="25px"/>   
                </td> 
             </tr>
           </table>
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
