<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contactos.aspx.vb" Inherits="ClsContactos1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%--<%@ Register Src="../Web_Controles/Mensaje_Deudores.ascx" TagName="Mensaje" TagPrefix="uc1" %>--%>

<%@ Register Src="../Web_Controles/MensajeContacto.ascx" TagName="Mensaje" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenedor de Contactos</title>
    <base target="_self" />
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../FuncionesJS/Funciones.js"></script>

    <script type="text/javascript" language="javascript" src="../../FuncionesJS/Grilla.js"></script>

    <script type="text/javascript" language="javascript" src="../../FuncionesJS/Ventanas.js"></script>

    <script src="FuncionesPrivadasJS/Contactos.js" type="text/javascript"></script>

    <style type="text/css">
        .auto-style1
        {
            font: 12px Calibri;
            color: #005797; /*font-weight: bold;*/;
            text-indent: 6px;
            margin-top: 6px;
            text-align: left;
            width: 278px;
        }
        .auto-style2
        {
            width: 268px;
        }
        .auto-style3
        {
            font: 12px Calibri;
            color: #005797; /*font-weight: bold;*/;
            text-indent: 6px;
            margin-top: 6px;
            text-align: left;
            width: 44px;
        }
        .auto-style4
        {
            width: 44px;
        }
    </style>

</head>
<body leftmargin="15" topmargin="20">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Auto" LoadScriptsBeforeUI="false"
        EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="590px">
        <tr>
            <td class="Cabecera" height="21">
                <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Datos Generales del Contacto Proveedor"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" valign="top" style="height: 500px" align="left">
                <table id="Table2" border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Nombre / Razón Social"></asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Nom" runat="server" CssClass="clsDisabled" MaxLength="50" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style1">
                            Número Identificación       <td>
                            <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsDisabled" MaxLength="10" Width="100px"></asp:TextBox>
                            <asp:TextBox ID="Txt_Dig" runat="server" CssClass="clsDisabled" MaxLength="1" Width="20px" AutoPostBack="true"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut"
                                AutoComplete="False" InputDirection="RightToLeft" ClearMaskOnLostFocus="True"
                                AcceptNegative="None" ClearTextOnInvalid="True">
                            </cc2:MaskedEditExtender>
                            <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="Txt_Dig">
                            </cc2:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Cargo"></asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Car" runat="server" CssClass="clsDisabled" MaxLength="50" 
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Dirección"></asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Dir" runat="server" CssClass="clsDisabled" MaxLength="50" 
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" class="auto-style1">
                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Observación"></asp:Label>
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="Txt_Obs" runat="server" CssClass="clsDisabled" MaxLength="50" TextMode="MultiLine"
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" valign="top">
                            <table id="Table3" border="0" cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td align="right" class="auto-style2">
                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Telefono Nº1"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_Tel_Uno" runat="server" CssClass="clsDisabled" MaxLength="10"
                                            Width="100px"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="Txt_Tel_Uno_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Tel_Uno" ValidChars="-">
                                        </cc2:FilteredTextBoxExtender>
                                    </td>
                                    <td align="right" class="auto-style3">
                                        <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Fax"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_Fax" runat="server" CssClass="clsDisabled" MaxLength="10" Width="100px"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="Txt_Fax" ValidChars="-">
                                        </cc2:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style2">
                                        <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Celular"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_Tel_Dos" runat="server" CssClass="clsDisabled" MaxLength="10"
                                            Width="100px"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="Txt_Tel_Dos_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Tel_Dos" ValidChars="-">
                                        </cc2:FilteredTextBoxExtender>
                                    </td>
                                    <td align="right" class="auto-style3">
                                        <asp:Label ID="Label9" runat="server" CssClass="Label" Text="E-mail"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_Mai" runat="server" CssClass="clsDisabled" MaxLength="50" 
                                            Width="165px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style2">
                                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Telefono Nº2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_Tel_Tres" runat="server" CssClass="clsDisabled" MaxLength="10"
                                            Width="100px"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="Txt_Tel_Tres_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Tel_Tres" ValidChars="-">
                                        </cc2:FilteredTextBoxExtender>
                                    </td>
                                    <td align="right" class="auto-style4">
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4" class="Label">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="CHB_Defecto" runat="server" CssClass="label" Text="Por Defecto" />
                                        <asp:CheckBox ID="CHB_SCliente" runat="server" CssClass="label" Text="Solo Cliente" />
                                        <asp:CheckBox ID="CHB_Rep" runat="server" CssClass="label" Text="Representante Legal" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:Panel ID="Panel_GV_Contactos" runat="server" ScrollBars="None" Height="250px">
                    <asp:GridView ID="GV_Contactos" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                        CssClass="formatUltcell" ShowHeader="true">
                        <FooterStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="id_cnc" HeaderText="Nro.">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cnc_nom" HeaderText="Nombre">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cnc_car" HeaderText="Cargo">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cnc_def" HeaderText="X Def.">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cnc_rep_leg" HeaderText="Rep. Legal">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_cnc") %>'
                                        OnClick="Img_Ver_Click" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="cabeceraGrilla" />
                        <RowStyle CssClass="formatUltcell" />
                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                onmouseout="this.src='../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                AlternateText="Anterior" />
                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../Imagenes/btn_workspace/flecha_der_out.gif"
                                onmouseout="this.src='../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                AlternateText="Siguiente" Style="width: 25px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <td align="right" id="Botonores" height="50">
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                            OnClick="IB_Guardar_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_Guardar_out.gif';"
                            onmouseover="this.src='../../Imagenes/Botones/Boton_Guardar_in.gif';" Style="position: static"
                            ToolTip="Guardar Contacto" Enabled="False" />
                        <cc2:ConfirmButtonExtender ID="CB_IB_Guardar" runat="server" ConfirmText="¿Está seguro de guardar contacto?" TargetControlID="IB_Guardar">
                        </cc2:ConfirmButtonExtender>    
                        <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif"
                            OnClick="IB_Eliminar_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_Eliminar_out.gif';"
                            onmouseover="this.src='../../Imagenes/Botones/Boton_Eliminar_in.gif';" Style="position: static"
                            ToolTip="Eliminar" Enabled="False" />
                        <cc2:ConfirmButtonExtender ID="CB_IB_Eliminar" runat="server" ConfirmText="¿Está seguro de guardar contacto?" TargetControlID="IB_Eliminar"></cc2:ConfirmButtonExtender> 
                        <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                            OnClick="IB_Nuevo_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                            onmouseover="this.src='../../Imagenes/Botones/Boton_Nuevo_in.gif';" Style="position: static"
                            ToolTip="Nuevo Contacto" />
                        <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif"
                            onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_out.gif';"
                            onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_in.gif';" Style="position: static"
                            ToolTip="Volver"  OnClick="Img_Ver_Click" />
                    </td>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="IB_Guardar" />
                </Triggers>
            </asp:UpdatePanel>
        </tr>
    </table>
    
    <asp:HiddenField ID="RutDeuCli" runat="server" />
    <asp:HiddenField ID="NroContacto" runat="server" />
    <asp:HiddenField ID="Posicion" runat="server" />
    <asp:HiddenField ID="SW" runat="server" />
    
    <asp:LinkButton ID="ActContactos" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="DetalleCnc" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Eliminar" runat="server"></asp:LinkButton>
    
    <uc1:Mensaje ID="Mensaje1" runat="server" />

    </form>
    
</body>

</html>
