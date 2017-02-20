<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarCliente.aspx.vb" Inherits="Modulos_Deudor_Agregar_Cliente" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregar Clientes</title>
    <base target="_self"/>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
    <script type="text/javascript" language="javascript" src="../../../FuncionesJS/Grilla.js"></script>
    <script type="text/javascript" language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>
    <script src="../FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            border-bottom: solid 1px #666666;
            border-right: solid 1px #666666;
            border-left: solid 1px #666666;
            filter: progid:dximagetransform.microsoft.gradient(gradienttype=0,startcolorstr=#DEE9EC,endcolorstr=#ffffff);
            height: 222px;
        }
    </style>
    
   

</head>
<body leftmargin="15" topmargin="20" >

    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    
            <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="550">
                <tr>
                    <td  height="15" class="Cabecera">
                        <asp:Label ID="Label3" runat="server" CssClass="Titulos" Text="Ayuda Cliente"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido">
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" style="position: static">
                                <tr height="40">
                                    <td align="right">
                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Identificación"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                            TargetControlID="Txt_Rut">
                                        </cc1:MaskedEditExtender>
                                        <asp:TextBox ID="Txt_Dig" runat="server" CssClass="clsTxt" Width="15px" 
                                            MaxLength="1"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" 
                                            runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                            TargetControlID="Txt_Dig" ValidChars="K,k">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Razón Social"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Txt_Rzo" runat="server" CssClass="clsTxt" Width="200px"></asp:TextBox></td>
                                    <td align="center">
                                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif"
                                            OnClick="IB_Buscar_Click"  AlternateText="Buscar Cliente" 
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';"
                                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td align=left valign="top" class="style1" >
                                        
                                            <asp:Panel ID="Panel_GV_Clientes" runat="server" height="200px" width="100%">
                                            
                                            <asp:GridView ID="GV_Clientes" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                EnableSortingAndPagingCallbacks="True" EnableTheming="True" Width="100%" 
                                                ShowHeader="true" Style="position: static">
                                                <FooterStyle BackColor="White" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:BoundField DataField="cli_idc" HeaderText="Identificación">
                                                        <ControlStyle Width="100px" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cli_rso" HeaderText="Razón Social">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("cli_idc") %>' OnClick="Img_Ver_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                                <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                <RowStyle CssClass="formatUltcell" />
                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                
                                            </asp:GridView>
                                            </asp:Panel>
                                        <%--</div>--%>
                    </td>
                </tr>
                <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                               
                                        <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                            AlternateText="Anterior" />
                                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                            AlternateText="Siguiente" />                                  
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr>
                    <td align="right" height="50" valign="bottom" >
                        
                                        <asp:ImageButton ID="IB_Aceptar" runat="server" 
                                            ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_out.gif';"
                                           onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';" 
                                            AlternateText="Aceptar" />
                                    <a href="javascript:window.close();" title="Volver">
                                        <img src="../../../Imagenes/Botones/Boton_Volver_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_out.gif';"
                                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" border="0" alt="Volver" />
                                    </a>
                        
                    </td>
                </tr>
            </table>
            
        <uc1:Mensaje ID="Mensaje1" runat="server" />            
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="IB_Aceptar" />
    </Triggers>
    </asp:UpdatePanel>        
            
    </form>
    
</body>
</html>
