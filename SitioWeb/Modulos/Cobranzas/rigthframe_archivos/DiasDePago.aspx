<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DiasDePago.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_DiasDePago" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Días de Pago</title>
    <base target="_self"></base>
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
      
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    
    <table cellpadding="0" cellspacing="1" class="style2" border="1">
        <tr>
            <td class="style63">
                <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="Días de Pago"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <table id="Table2" border="0" cellpadding="1" cellspacing="1" style="height: 26px;
                    width: 100%; margin-right: 100px;" align="left" frame="border">
                    <tr>
                        <td align="justify" class="style3">
                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Días"></asp:Label>
                        </td>
                        <td align="center" class="style4">
                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Horario de Pago"></asp:Label>
                        </td>
                        <td align="center" class="style1">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="justify" class="style3">
                            <asp:CheckBox ID="Chck_Lunes" runat="server" CssClass="Label" Text="Lunes" />
                        </td>
                        <td align="center" class="style4">
                            <asp:TextBox ID="Txt_HorLun" runat="server" CssClass="clsMandatorio" Height="22px"
                                Width="197px"></asp:TextBox>
                        </td>
                        <td align="center" class="style1">
                            <asp:RadioButton ID="Rbt_AM_LU" runat="server" CssClass="Label" GroupName="Lunes"
                                Text="AM" />
                            <asp:RadioButton ID="Rbt_PM_LU" runat="server" CssClass="Label" GroupName="Lunes"
                                Text="PM" />
                        </td>
                    </tr>
                    <tr>
                        <td align="justify" class="style3">
                            <asp:CheckBox ID="Chck_Martes" runat="server" CssClass="Label" Text="Martes" />
                        </td>
                        <td align="center" class="style4">
                            <asp:TextBox ID="Txt_HorMar" runat="server" CssClass="clsMandatorio" Height="22px"
                                Width="197px"></asp:TextBox>
                        </td>
                        <td align="center" class="style1">
                            <asp:RadioButton ID="Rbt_AM_MA" runat="server" CssClass="Label" GroupName="Martes"
                                Text="AM" />
                            <asp:RadioButton ID="Rbt_PM_MA" runat="server" CssClass="Label" GroupName="Martes"
                                Text="PM" />
                        </td>
                    </tr>
                    <tr>
                        <td align="justify" valign="top" class="style3">
                            <asp:CheckBox ID="Chck_Miercoles" runat="server" CssClass="Label" Text="Miércoles" />
                        </td>
                        <td align="center" valign="top" class="style4">
                            <asp:TextBox ID="Txt_HorMie" runat="server" CssClass="clsMandatorio" Height="22px"
                                Width="197px"></asp:TextBox>
                        </td>
                        <td align="center" valign="top" class="style1">
                            <asp:RadioButton ID="Rbt_AM_MI" runat="server" CssClass="Label" GroupName="Miercoles"
                                Text="AM" />
                            <asp:RadioButton ID="Rbt_PM_MI" runat="server" CssClass="Label" GroupName="Miercoles"
                                Text="PM" />
                        </td>
                    </tr>
                    <tr>
                        <td align="justify" valign="top" class="style3">
                            <asp:CheckBox ID="Chck_Jueves" runat="server" CssClass="Label" Text="Jueves" />
                        </td>
                        <td align="center" valign="top" class="style4">
                            <asp:TextBox ID="Txt_HorJue" runat="server" CssClass="clsMandatorio" Height="22px"
                                Width="197px"></asp:TextBox>
                        </td>
                        <td align="center" valign="top" class="style1">
                            <asp:RadioButton ID="Rbt_AM_JU" runat="server" CssClass="Label" GroupName="Jueves"
                                Text="AM" />
                            <asp:RadioButton ID="Rbt_PM_JU" runat="server" CssClass="Label" GroupName="Jueves"
                                Text="PM" />
                        </td>
                    </tr>
                    <tr>
                        <td align="justify" valign="top" class="style3">
                            <asp:CheckBox ID="Chck_Viernes" runat="server" CssClass="Label" Text="Viernes" />
                        </td>
                        <td align="center" valign="top" class="style4">
                            <asp:TextBox ID="Txt_HorVie" runat="server" CssClass="clsMandatorio" Height="22px"
                                Width="197px"></asp:TextBox>
                        </td>
                        <td align="center" valign="top" class="style1">
                            <asp:RadioButton ID="Rbt_AM_VI" runat="server" CssClass="Label" GroupName="Viernes"
                                Text="AM" />
                            <asp:RadioButton ID="Rbt_PM_VI" runat="server" CssClass="Label" GroupName="Viernes"
                                Text="PM" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table cellpadding="0" cellspacing="1" class="style6" border="0">
                    <tr>
                        <td align="right" class="style7">
                            <asp:LinkButton ID="LinkB_Guardar" runat="server"></asp:LinkButton>
                            <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                OnClick=" IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" ToolTip="Guardar" />
                        </td>
                        <td>
                            <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';"
                                ToolTip="Volver" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="IB_Volver" />
    <asp:PostBackTrigger  ControlID="IB_Guardar" />
    </Triggers>
    </asp:UpdatePanel>
    <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
