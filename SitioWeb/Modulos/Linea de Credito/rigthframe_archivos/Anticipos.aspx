<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Anticipos.aspx.vb" Inherits="Modulos_Linea_de_Credito_rigthframe_archivos_Anticipos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target=_self />
    <title>Anticipos de Linea</title>
    <link href="../../../CSS/Estilos.css"  rel="stylesheet" type="text/css" />
    <script language="javascript"    src="../../../FuncionesJS/Number.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../FuncionesPrivadasJS/LineaCredito.js"></script>

    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 5px">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table border="0" cellpadding="0" cellspacing="0" width="600">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label4" runat="server" Text="Anticipos" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido"  align="center" style="text-align:-moz-center"
                        valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" style="position: static" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label30" runat="server" CssClass="SubTitulos" Style="position: static"
                                        Text="Porcentaje a Anticipar"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table border="0" cellpadding="0" cellspacing="0" style="position: static">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label32" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Tipo Documento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_Pro_Tip" runat="server" CssClass="clsDisabled" Style="position: static"
                                                    Width="150px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Lbl_Valor_Deu" runat="server" CssClass="Label" Style="position: static"
                                                    Text="% Anticipar"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Pro_Val" runat="server" CssClass="clsDisabled" MaxLength="6"
                                                    Style="position: static" Width="96px" Enabled="False"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="Txt_Pro_Val_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                    Mask="999,99" MaskType="Number" TargetControlID="Txt_Pro_Val">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="Txt_Pro_Val"
                                                    Display="None" 
                                                    ErrorMessage="<b>Porcentaje</b><br />Ingrese Porcentaje de Anticipo." 
                                                    ValidationGroup="Guardar" />
                                                <cc1:ValidatorCalloutExtender runat="Server" ID="NReqE" TargetControlID="NReq" HighlightCssClass="validatorCalloutHighlight" />
                                            </td>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="150">
                                                <table id="Table4" border="0" cellpadding="0" cellspacing="0" width="150">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:Label ID="Label27" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                    top: -14px" Text="Verificación"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" class="Contenido" valign="top">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center" valign="middle">
                                                                                <asp:RadioButtonList ID="Rb_ver" runat="server" CssClass="Label" 
                                                                                    RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td width="75">
                                            
                                            
                                            </td>
                                            <td width="150">
                                                <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="150">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                    top: -14px" Text="Notificación"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" class="Contenido" valign="top">
                                                                <asp:RadioButtonList ID="Rb_Not" runat="server" CssClass="Label" 
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td width="75">
                                            
                                            </td>
                                            <td>
                                                <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="150">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" class="Cabecera">
                                                                <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                    top: -14px" Text="Cobranza"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" class="Contenido" valign="top">
                                                                <asp:RadioButtonList ID="Rb_Cob" runat="server" CssClass="Label" 
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Style="position: static"
                                        Text="Tipo de Producto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" height="100">
                                    
                                    <%--<table class="cabeceraGrilla">
                                        <tr>
                                            <td width="80"><asp:Label ID="Label5" runat="server" CssClass="LabelCabeceraGrilla" Text="N°"></asp:Label></td>
                                            <td width="200"><asp:Label ID="Label6" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D."></asp:Label></td>
                                            <td width="80"><asp:Label ID="Label10" runat="server" CssClass="LabelCabeceraGrilla" Text="%"></asp:Label></td>
                                            <td width="80"><asp:Label ID="Label7" runat="server" CssClass="LabelCabeceraGrilla" Text="Ver."></asp:Label></td>
                                            <td width="80"><asp:Label ID="Label8" runat="server" CssClass="LabelCabeceraGrilla" Text="Not."></asp:Label></td>
                                            <td width="80"><asp:Label ID="Label9" runat="server" CssClass="LabelCabeceraGrilla" Text="Cob."></asp:Label></td>
                                        </tr>
                                    </table>--%>
                                    
                                    <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" Style="position: static">
                                        <asp:GridView ID="GV_PorcentajeAnt" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                            CssClass="formatUltcell" EnableTheming="True" HorizontalAlign="Center" PageSize="1" ShowHeader="true">
                                            <FooterStyle BorderStyle="Dashed" />
                                            <Columns>
                                              <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                                   <ItemTemplate>
                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                      OnClick="Button1_Click" ToolTip='<%# Eval("apc_num") %>' style="height: 13px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="apc_num" HeaderText="Nº">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="id_P_0031_des" HeaderText="Tipo Docto.">
                                                    <ItemStyle HorizontalAlign="Left" Width="200" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="apc_pct" HeaderText="%">
                                                    <ItemStyle HorizontalAlign="Center" Width="80"  />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="apc_ver_son" HeaderText="Ver.">
                                                    <ItemStyle HorizontalAlign="Center" Width="80"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="apc_not_son" HeaderText="Not.">
                                                    <ItemStyle HorizontalAlign="Center" Width="80"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="apc_cob_son" HeaderText="Cob.">
                                                    <ItemStyle HorizontalAlign="Center" Width="80"/>
                                                </asp:BoundField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_prod" text='<% #eval("id_P_0031") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                           <%-- <tr>
                            <td align="center">
                                <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                             onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                             onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                
                                <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                             onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                             onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                            </td>
                            </tr>--%>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="40">
                        &nbsp;
                        <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                            OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';" Style="position: static"
                            CausesValidation="False" ToolTip="Nuevo" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                            OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" Style="position: static"
                            Enabled="False" ToolTip="Guardar" ValidationGroup="Guardar" />&nbsp; &nbsp;
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                            OnClick="IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';" Style="position: static"
                            CausesValidation="False" ToolTip="Limpiar" />
                        <asp:ImageButton ID="IB_Volver" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_volver_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_volver_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_volver_in.gif';"
                            Style="position: static" ToolTip="Volver" />
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="Posicion" runat="server" />
            <asp:HiddenField ID="apc_num" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            <asp:LinkButton ID="LB_DetalleAPC" runat="server" CausesValidation="False" OnClick="LB_DetalleAPC_Click" Style="position: static"></asp:LinkButton>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
  </form>
  
</body>

</html>
