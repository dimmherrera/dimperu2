<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ayuda_Deudor_Postback.aspx.vb" Inherits="Modulos_Ayudas_Ayuda_Deudor_Postback" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Ayuda Pagador</title>
    <base target="_self">
    </base>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../FuncionesJS/Ventanas.js"></script>
    <script language="javascript" src="FuncionesPrivadasJS/AyudaDeudor.js"></script>
    <script src="../Adm.Deudores/FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">


// -->
    </script>
</head>
<body leftmargin="15" topmargin="20">
    <form id="form1" runat="server">
        <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="518">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label3" runat="server" CssClass="Titulos" Text="Ayuda Pagador" Style="position: static"></asp:Label></td><%--FY 19-05-2012--%>
                </tr>
                <tr>
                    <td valign="top" style="height: 22px" class="Contenido">
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="position: static"
                            width="100%">
                            <tr height="40">
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Identificación" Height="1px" Width="18px"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsTxt" Width="70px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                        TargetControlID="Txt_Rut">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Dig" runat="server" CssClass="clsTxt" Width="15px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                        TargetControlID="Txt_Dig" ValidChars="K,k">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Razón Social" Width="128px"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Txt_Rzo" runat="server" CssClass="clsTxt" Width="200px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" class="Cabecera"
                            style="width: 100%; position: static;">
                            <tr>
                                <td align="right" width="100">
                                    <asp:Label ID="Label8" runat="server" CssClass="LabelCabeceraGrilla" Text="Identificación"></asp:Label></td>
                                <td align="left">
                                    <asp:Label ID="Label9" runat="server" CssClass="LabelCabeceraGrilla" 
                                        Text="Razón Social Pagador"></asp:Label></td>
                            </tr>
                        </table>
                        <div style="overflow: auto; height: 220px; position: static;">
                            <asp:GridView ID="GV_Deudores" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                EnableTheming="True" Width="95%" ShowHeader="False" Style="position: static">
                                <FooterStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="deu_ide" HeaderText="RUT">
                                        <ControlStyle Width="100px" />
                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                        <HeaderStyle BackColor="#0033CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deu_rso" HeaderText="Razón Social">
                                        <HeaderStyle BackColor="#0033CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
                    <td align="right" style="height: 50px">
                    
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_Out.gif"
                                        OnClick="IB_Buscar_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_Buscar_Out.gif';"
                                        onmouseover="this.src='../../Imagenes/Botones/Boton_Buscar_In.gif';" 
                                        ToolTip="Buscar Deudor" />
                                    <a href="javascript:AceptaDeudorPostback(GV_Deudores);">
                                        <img border="0" onmouseout="this.src='../../Imagenes/Botones/Boton_Aceptar_Out.gif';"
                                            onmouseover="this.src='../../Imagenes/Botones/Boton_Aceptar_In.gif';" 
                                        src="../../Imagenes/Botones/Boton_Aceptar_Out.gif" id="IMG1" alt="Aceptar"  />
                                    </a>
                                
                            
                                    <a href="javascript:window.close();">
                                        <img border="0" onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                            onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_In.gif';" 
                                        src="../../Imagenes/Botones/Boton_Volver_Out.gif" alt="Volver" />
                                    </a>
                            
                    </td>
                </tr>
            </table>
      
    </form>
</body>
</html>
