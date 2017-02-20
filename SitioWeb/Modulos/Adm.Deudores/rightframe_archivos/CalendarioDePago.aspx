<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalendarioDePago.aspx.vb" Inherits="CalendarioDePago" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
</head>

<body>
    
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    
    <div style="text-align: -moz-center">
        <table cellspacing="0" cellpadding="0" border="0" width="600px">
            <tr>
                <td align="center">
                    <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="Mantencion Calendario Pago"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Contenido" align="left">
                    <table style="position: static" id="TABLE1" cellspacing="1" cellpadding="0" width="100%"
                        border="0">
                        <tr>
                            <td class="Cabecera" align="left" style="height: 25px" colspan="2">
                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label1" runat="server"
                                    CssClass="SubTitulos" Text="Calendario Pago"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" width="50%">
                                <table cellspacing="2" cellpadding="2" border="0">
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label Style="position: static" ID="Label15" runat="server" CssClass="Label" Text="Dìas de Pago"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBoxList ID="CB_Dias" runat="server" CssClass="Label">
                                                <asp:ListItem Text="Lunes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Martes" Value="2" ></asp:ListItem>
                                                <asp:ListItem Text="Miercoles" Value="3" ></asp:ListItem>
                                                <asp:ListItem Text="Jueves" Value="4" ></asp:ListItem>
                                                <asp:ListItem Text="Viernes" Value="5" ></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>    
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Año"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="Txt_Ano" runat="server" CssClass="clsTxt" Width="30px" MaxLength="4"></asp:TextBox>
                                            
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="Contenido" align="center" valign="top" width="50%">
                                <asp:Panel Style="position: static" ID="Panel1" runat="server" Width="100%" ScrollBars="Vertical"
                                    Height="500px">
                                    <asp:GridView ID="GV_Fechas" runat="server" AutoGenerateColumns="false" CellPadding="3"
                                        CssClass="formatUltcell" EnableTheming="true" Style="position: static">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/images/cerrar.png" ToolTip='<%# Eval("id_cpg") %>'
                                                        OnClick="IB_Eliminar_Click" />
                                                    <cc2:ConfirmButtonExtender ID="CBE_Eliminar" runat="server" TargetControlID="IB_Eliminar"
                                                        ConfirmText="¿Desea eliminar esta fecha?">
                                                    </cc2:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fec_cpg" HeaderText="Fecha" DataFormatString="{0:dd ddd MMM yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                        <RowStyle CssClass="formatUltcell" />
                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                          </tr>   
                        </tr>     
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 10px">
                    <asp:Button ID="Btn_Nuevo" runat="server" Text="Nuevo" CssClass="button" Width="90px"
                        OnClick="Btn_Nuevo_Click" />
                    <asp:Button ID="Btn_Guardar" runat="server" Text="Guardar" CssClass="button" Width="90px"
                        OnClick="Btn_Guardar_Click" />
                    <cc2:confirmbuttonextender id="CBE_Guardar" runat="server" confirmtext="¿Desea insertar fecha?"
                        targetcontrolid="Btn_Guardar">
                                 </cc2:confirmbuttonextender>
                    <asp:Button ID="Btn_Limpiar" runat="server" Text="Limpiar" CssClass="button" Width="90px"
                        OnClick="Btn_Limpiar_Click" />
                    <asp:Button ID="Btn_Cerrar" runat="server" Text="Cerrar" CssClass="button" Width="90px" />
                </td>
            </tr>
        </table>
    </div>
    
    </form>
</body>

</html>
