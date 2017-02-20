<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Organigramas.aspx.vb" Inherits="Organigramas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Mantención de Organigrama</title>
		<base target="_self">
		<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
		<script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
		<script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>
		<script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>
        <script src="../FuncionesPrivadasJS/Organigramas.js" type="text/javascript"></script>
        <style type="text/css">
            .auto-style1
            {
                height: 21px;
                width: 453px;
            }
            .auto-style2
            {
                width: 201px;
            }
            .auto-style3
            {
                width: 453px;
            }
        </style>
</head>

<body leftmargin="15" topmargin="20">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>    
    <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="518" height="21">
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Datos Generales del Organigrama"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido">
                <table id="Table2" border="0" cellpadding="1" cellspacing="1" style="height: 26px"
                    width="100%">
                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" CssClass="Label">Número Identificación </asp:Label>
                        </td>
                        <td style="height: 21px">
                            <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="False" 
                                InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                TargetControlID="Txt_Rut">
                            </cc1:MaskedEditExtender>
                            <asp:TextBox
                                ID="Txt_Dig" runat="server" CssClass="clsMandatorio" Width="15px" 
                                MaxLength="1" AutoPostBack="true"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                TargetControlID="Txt_Dig" ValidChars="K,k">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style3">
                            <asp:Label ID="Label2" runat="server" CssClass="Label">Nombre</asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Nom" runat="server" CssClass="clsMandatorio" MaxLength="50"
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style3">
                            <asp:Label ID="Label3" runat="server" CssClass="Label">Cargo</asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Car" runat="server" CssClass="clsMandatorio" MaxLength="50"
                                Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" class="auto-style3">
                            <asp:Label ID="Label4" runat="server" CssClass="Label" DESIGNTIMEDRAGDROP="2125">Atributo</asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Atb" runat="server" CssClass="clsTxt" MaxLength="50" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="2">  
                            <asp:Panel ID="Panel_GV_Organigramas" runat="server" height="250px" ScrollBars="None">
                                <asp:GridView ID="GV_Organigramas" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                    EnableSortingAndPagingCallbacks="True" EnableTheming="True" Width="97%" ShowHeader="true">
                                    <FooterStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("org_rut") %>'
                                                        OnClick="Img_Ver_Click" style="height: 13px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:TemplateField>                                        
                                        
                                        <asp:BoundField DataField="org_rut" HeaderText="Identificación">
                                            <ControlStyle Width="700px" />
                                            <ItemStyle Width="70px" />
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="org_nom" HeaderText="Nombre" HtmlEncode="False">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <ItemStyle Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="org_car" HeaderText="Cargo" HtmlEncode="False">
                                            <HeaderStyle Font-Bold="True" />
                                            <ItemStyle Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="org_atb" HeaderText="Atr. Firma">
                                            <HeaderStyle Font-Bold="True" />
                                            <ItemStyle Width="110px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="formatUltcell" />
                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla" />                               
                                    </asp:GridView>
                                  </asp:Panel>
                        </td>
                    </tr>
                      <tr>
                            <td align="center" colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                AlternateText="Anterior" Height="25px" />
                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                AlternateText="Siguiente" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td align="right" height="50">
            
            <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                        OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                        
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" 
                    Style="position: static" ToolTip="Guardar" Enabled="false" />
                    
                <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                    OnClick="IB_Eliminar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';"
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';" Style="position: static"
                    ToolTip="Eliminar" Enabled="false" />
                    
                    <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                        OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';" 
                    Style="position: static" ToolTip="Nuevo" />
                    
                    <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif"
                        OnClick="IB_Volver_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_out.gif';"
                        
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" 
                    Style="position: static" ToolTip="Volver" />
              </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="sw" runat="server" />
    <asp:HiddenField ID="RutOrg" runat="server" />
        <asp:HiddenField ID="HF_Pos" runat="server" />
    <asp:LinkButton ID="ActOrganigrama" runat="server"></asp:LinkButton>
                        
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    
    </ContentTemplate>
    </asp:UpdatePanel>
     <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
     <asp:LinkButton ID="LB_Eliminar" runat="server"></asp:LinkButton>
    <asp:HiddenField ID="HF_Atr" runat="server" />
    </form>
    
</body>
</html>
