<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AyudaDeudor.aspx.vb" Inherits="Modulos_Ayudas_AyudaDeudor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Ayuda Pagadores</title>
    <base target="_self">
    </base>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../FuncionesJS/Ventanas.js"></script>
    <script src="FuncionesPrivadasJS/AyudaDeudor.js" type="text/javascript"></script>

    
    <%--<script src="../Adm.Deudores/FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>--%>
    
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
   <table id="Table2" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label3" runat="server" CssClass="Titulos" Text="Ayuda Pagador" Style="position: static"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="position: static" width="100%" class="Contenido">
                            <tr height="40">
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Identificación"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut" 
                                        InputDirection="RightToLeft">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Razón Social"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Txt_Rzo" runat="server" CssClass="clsTxt" Width="200px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <div style="overflow: auto; height: 250px; position: static;">
                            <asp:GridView ID="GV_Deudores" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                EnableTheming="True" Width="100%" ShowHeader="true" Style="position: static">
                                <FooterStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("deu_ide") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                        <ControlStyle Width="100px" />
                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                    </asp:BoundField>
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
                    <td align="right" style="height: 50px">
                    
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_Out.gif"
                                        OnClick="IB_Buscar_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_Buscar_Out.gif';"
                                        onmouseover="this.src='../../Imagenes/Botones/Boton_Buscar_In.gif';" 
                                        ToolTip="Buscar Deudor" />
                                        
                                    <%--<a href="javascript:AceptaDeudorNormal(GV_Deudores);">
                                        <img border="0" onmouseout="this.src='../../Imagenes/Botones/Boton_Aceptar_Out.gif';"
                                            onmouseover="this.src='../../Imagenes/Botones/Boton_Aceptar_In.gif';" 
                                        src="../../Imagenes/Botones/Boton_Aceptar_Out.gif" id="IMG1" alt="Aceptar"/>
                                    </a>--%>
                                
                            
                                    <a href="javascript:window.close();">
                                        <img border="0" onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                            onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_In.gif';" 
                                        src="../../Imagenes/Botones/Boton_Volver_Out.gif" alt="Volver" />
                                    </a>
                            
                    </td>
                </tr>
            </table>
      </ContentTemplate>
      
    </asp:UpdatePanel>
    </form>
</body>
</html>
