<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Empresas.aspx.vb" Inherits="Empresas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
		<title>Mantención de Empresa</title>
		<base target =_self></base>
        <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
		<script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
		<script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>
		<script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>
        <script src="../FuncionesPrivadasJS/Empresas.js" type="text/javascript"></script>
        <style type="text/css">
            .style1
            {
                /*font-weight: bold;
    font: 12px "Arial Blck";
    */ /*font: 14px "Trebuchet MS" , "Lucida Grande" , "Bitstream Vera Sans" , Arial, Helvetica, sans-serif;*/
	        font: 12px "verdana";
                float: none;
                height: 21px;
                width: 83px;
            }
            .style2
            {
                /*font-weight: bold;
    font: 12px "Arial Blck";
    */ /*font: 14px "Trebuchet MS" , "Lucida Grande" , "Bitstream Vera Sans" , Arial, Helvetica, sans-serif;*/
	        font: 12px "verdana";
                float: none;
                width: 83px;
            }
        </style>
</head>
<body leftmargin="15" topmargin="20">
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="516">
            <tr>
                <td class="Cabecera">
                    <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Datos Generales de la Empresa"></asp:Label></td>
            </tr>
            <tr>
                <td class="Contenido">
                
                    <table id="Table2" border="0" cellpadding="1" cellspacing="1"
                        width="100%">
                        <tr>
                            <td align="right" class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Identificación" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                 <cc2:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="False" 
                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                    TargetControlID="Txt_Rut">
                                </cc2:MaskedEditExtender> 
                                <asp:TextBox ID="Txt_Dig" runat="server" CssClass="clsMandatorio" Width="15px" 
                                    MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                <cc2:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" 
                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                    TargetControlID="Txt_Dig" ValidChars="K,k">
                                </cc2:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label2" runat="server" Text="Razón Social" CssClass="Empresa"></asp:Label>
                            </td>
                            <td style="width: 92px">
                                <asp:TextBox ID="Txt_Des" runat="server" CssClass="clsMandatorio" MaxLength="50" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Panel ID="Panel_GV_Empresas" runat="server" height="250px" ScrollBars="None">
                                <asp:GridView ID="GV_Empresas" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell" 
                                     EnableTheming="True"
                                    Width="97%" ShowHeader="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_emp") %>'
                                                        OnClick="Img_Ver_Click" style="height: 13px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:TemplateField>
                                       <asp:BoundField DataField="id_emp" HeaderText="Código">
                                        <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="emp_rut" HeaderText="Identificación">
                                            <ControlStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="emp_des" HeaderText="Descripci&#243;n">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
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
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';"
                                                 onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                AlternateText="Anterior" Height="25px" />
                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
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
                        Style="position: static" ToolTip="Guardar Empresa" Enabled="False" />
                    
                    <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                        OnClick="IB_Eliminar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';" 
                        Style="position: static" ToolTip="Eliminar Empresa" Enabled="False" />
                    
                    <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                        OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';" 
                        Style="position: static" ToolTip="Nuevo" />
                    
                    <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif"
                        OnClick="IB_Volver_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" 
                        Style="position: static" ToolTip="Volver a pantalla anterior" /></td>
            </tr>
        </table>
        <asp:LinkButton ID="ActEmpresa" runat="server"></asp:LinkButton>
        <asp:HiddenField ID="RutEmp" runat="server" />
        <asp:HiddenField ID="HF_Pos" runat="server" />
        <uc1:Mensaje ID="Mensaje1" runat="server" />
        <asp:HiddenField ID="sw" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>
      <p>
        <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="LB_Eliminar" runat="server"></asp:LinkButton>
        </p>
      </form>
</body>
</html>
