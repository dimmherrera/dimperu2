<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ayudaclipopup.aspx.vb" Inherits="Modulos_Ayudas_ayudaclipopup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Ayuda Clientes</title>
    <base target="_self"></base>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../FuncionesJS/Ventanas.js"></script>
    <script src="FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>
</head>
<body leftmargin="15" topmargin="20">
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="600px">
                <tr>
                    <td height="15" class="Cabecera">
                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Busqueda"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido">
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" style="position: static">
                            <tr height="40">
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Identificación"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                        TargetControlID="Txt_Rut">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Razón Social"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Rzo" runat="server" CssClass="clsTxt" Width="200px"></asp:TextBox>
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <br />  
                        
                        <div style="overflow: auto; height: 200px; position: static;">
                        <asp:GridView ID="GV_Clientes" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                            EnableSortingAndPagingCallbacks="True" EnableTheming="True" Width="100%" PageSize="8" ShowHeader="true">
                            <Columns>
                                <asp:BoundField DataField="cli_idc" HeaderText="Identificación">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cli_rso" HeaderText="Razón Social">
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="TIPO CLIENTE" DataField="PNU_CLI_TIP_DES">
                                    <ItemStyle HorizontalAlign="center" Width="170px" />
                                </asp:BoundField>--%>
                                 <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" ToolTip='<%# Eval("cli_idc") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                        </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="IB_Prev" runat="server" 
                            ImageUrl="../../Imagenes/btn_workspace/flecha_izq_out.gif" 
                            onmouseout="this.src='../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                            onmouseover="this.src='../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                        <asp:ImageButton ID="IB_Next" runat="server" 
                            ImageUrl="../../Imagenes/btn_workspace/flecha_der_out.gif" 
                            onmouseout="this.src='../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                            onmouseover="this.src='../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                    </td>
                </tr>
                <tr>
                    <td align="right" height="50" valign="bottom">
                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            OnClick="IB_Buscar_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_buscar_out.gif';"
                            onmouseover="this.src='../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                        <a href="javascript:AceptaCliente(GV_Clientes);">
                            <img src="../../Imagenes/Botones/boton_aceptar_out.gif" onmouseout="this.src='../../Imagenes/Botones/boton_aceptar_out.gif';"
                                onmouseover="this.src='../../Imagenes/Botones/boton_aceptar_in.gif';" border="0" />
                        </a><a href="javascript:window.close();">
                            <img src="../../Imagenes/Botones/Boton_Volver_Out.gif" onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_In.gif';" 
                            border="0" alt="Volver" />
                        </a>
                    </td>
                </tr>
            </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    

    </form>
    
</body>
</html>
