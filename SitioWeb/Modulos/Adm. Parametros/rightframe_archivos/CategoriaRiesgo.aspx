<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CategoriaRiesgo.aspx.vb" Inherits="CategoriaRiesgo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Categoria de Riesgo</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Scroll.js" type="text/javascript"></script>

    <script src="../FuncionesPrivadasJS/Sucursal_Plaza.js" type="text/javascript"></script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table id="tabla General">
        <tr>
        <td>
            <table id="Tabla Contenedora" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label1" runat="server" Text="Categoria de Riesgo" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                    <table>
                    <tr>
                    <td>
                    
                    
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Producto" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_Id" runat="server" Width="70px" ReadOnly="true" CssClass="clsDisabled"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txt_Des" runat="server" ReadOnly="true" CssClass="clsDisabled" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="Tipo de Riesgo" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_TpRLetra" runat="server" Width="70px" ReadOnly="true" CssClass="clsDisabled"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_TPRnumero" runat="server" ReadOnly="true" Width="70px" CssClass="clsDisabled"></asp:TextBox>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Día Desde" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Dia_dsd" runat="server" CssClass="clsMandatorio" 
                                                    MaxLength="5" Width="60px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txt_Dia_dsd_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Numbers" 
                                                    TargetControlID="txt_Dia_dsd">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Día hasta" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Dia_hst" runat="server" CssClass="clsMandatorio" 
                                                    MaxLength="5" Width="60px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txt_Dia_hst_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Numbers" 
                                                    TargetControlID="txt_Dia_hst">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="IB_Agregar" runat="server" AlternateText="Agregar a Lista" 
                                                 ImageUrl="../../../Imagenes/Botones/boton_agregar_out.gif"
                                                 onmouseout="this.src='../../../Imagenes/Botones/boton_agregar_out.gif';"
                                                 onmouseover="this.src='../../../Imagenes/Botones/boton_agregar_in.gif';" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                        <asp:GridView ID="Gr_Ctr" runat="server" CssClass="formatUltcell" 
                            AutoGenerateColumns="False" ShowHeader="False" >
                            <Columns>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dsd" HeaderText="dsd" >
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hst" HeaderText="hst" >
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        
                    </td>
                   
                    </tr>
                    <tr>
                    <td align="right">
                       <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:HiddenField ID="HF_Po" runat="server" />
                        <asp:HiddenField ID="HF_Id" runat="server" />
                        <asp:LinkButton ID="LinkB" runat="server"></asp:LinkButton>
                        <asp:ImageButton ID="IB_Guardar" runat="server" AlternateText="Guardar" 
                         ImageUrl="../../../Imagenes/Botones/boton_guardar_out.gif"
                         onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                         onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" Enabled="false" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar"
                          ImageUrl="../../../Imagenes/Botones/boton_limpiar_out.gif"
                         onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                         onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"  />
                        <asp:ImageButton ID="IB_Volver" runat="server"  AlternateText="Volver" 
                         ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                         onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                         onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" />
                    </td>
                    </tr>
                    </table>    
                    </td>
                </tr>
               
            </table>
        
        </td>
        </tr>
        </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="IB_Volver" />
        <asp:PostBackTrigger ControlID="IB_Agregar" />
        </Triggers>
        </asp:UpdatePanel>
        <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
    </div>
    </form>
</body>
</html>
