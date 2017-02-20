    
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sublineas.aspx.vb" Inherits="Modulos_Linea_de_Credito_rigthframe_archivos_Sublineas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <base target="_self" />
    <title>Sub Linea</title>
    <link href="../../../CSS/Estilos.css"  rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Number.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/LineaCredito.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    
</head>

<body>

    <form id="form1" runat="server" style="padding: 5px">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     
        <ContentTemplate>
        
    
        <table border="0" cellpadding="0" cellspacing="0" width="600">
        <tr>
            <td class="Cabecera">  
        
                            <asp:Label ID="Label37" runat="server" CssClass="Titulos" Style="position: static"
                                Text="Sub Linea"></asp:Label>  
        
        </td>
        </tr>
        <tr>
            <td align="center" class="Contenido" height="250" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label29" runat="server" CssClass="SubTitulos" Style="position: static"
                                Text="Criterio"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB_Deudor" runat="server" AutoPostBack="True" CssClass="Label"
                                GroupName="Sub_Linea" Style="position: static" Text="Pagador" Checked="True" />
                            <asp:RadioButton ID="RB_Producto" runat="server" AutoPostBack="True" CssClass="Label"
                                GroupName="Sub_Linea" Style="position: static" Text="Producto" /></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="600">
                                        <tr>
                                            <td align="right" style="width: 76px">
                                                <asp:Label ID="Label30" runat="server" CssClass="SubTitulos" Style="position: static"
                                                    Text="Pagador"></asp:Label></td>
                                            <td align="left">
                                                <asp:RadioButton ID="RB_Deu_Mto" runat="server" CssClass="Label" Enabled="False"
                                                    GroupName="Deudor" Style="position: static" Text="Mto. Sub Linea" AutoPostBack="True"
                                                    Checked="True" />
                                                <asp:RadioButton ID="RB_Deu_Por" runat="server" CssClass="Label" Enabled="False"
                                                    GroupName="Deudor" Style="position: static" Text="Porcentaje" AutoPostBack="True" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 76px">
                                            </td>
                                            <td align="left">
                                                <table border="0" cellpadding="0" cellspacing="0" style="position: static">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label32" runat="server" CssClass="Label" Style="position: static"
                                                                Text="Identificación"></asp:Label></td>
                                                        <td>
                                                                <asp:TextBox ID="Txt_Rut_Deu" runat="server"  Width="90px" 
                                                                    CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                <cc2:MaskedEditExtender ID="Txt_Deu_Rut_MaskedEditExtender" runat="server" 
                                                                    AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                    MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                </cc2:MaskedEditExtender>
                                                                
                                                                <asp:TextBox ID="Txt_Dig_Deu" runat="server" MaxLength="1" Width="15px" 
                                                                    CssClass="clsDisabled" ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                                                             
                                                               <asp:ImageButton ID="ib_ayudadeu" runat="server"  
                                                                    ImageUrl="~/Imagenes/Iconos/155.ICO" Width="20px" Enabled="False"/>
                                                                <cc2:FilteredTextBoxExtender ID="Txt_Deu_Dig_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" 
                                                                    TargetControlID="Txt_Dig_Deu" ValidChars="0,1,2,3,4,5,6,7,8,9,k, K">
                                                                </cc2:FilteredTextBoxExtender>
                                                                <asp:TextBox ID="Txt_Rso_Deu" runat="server" MaxLength="50" Width="200px" 
                                                                    CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                           </td>
                                                        <td>
                                                            <asp:Button ID="Btn_BuscarDeudor" runat="server" CssClass="boton" Text="Buscar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Lbl_Valor_Deu" runat="server" CssClass="Label" Style="position: static"
                                                                Text="Monto"></asp:Label></td>
                                                        <td>
                                                                <asp:TextBox ID="Txt_Deu_Val" runat="server" Width="100px" 
                                                                    CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                <cc2:MaskedEditExtender ID="Txt_Deu_Val_MaskedEditExtender" runat="server" 
                                                                    AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                    MaskType="Number" TargetControlID="Txt_Deu_Val">
                                                                </cc2:MaskedEditExtender>
                                                            
                                                        </td>
                                                        <td>
                                                            </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 76px">
                                            </td>
                                            <td align="left">
                                               <asp:Panel ID="Panel_Gv_Sub_DDR" runat="server" Height="150px" ScrollBars="Vertical">
                                                
                                                <asp:GridView ID="GV_SubDDR" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                    CssClass="formatUltcell" EnableTheming="True" PageSize="3" ShowHeader="true">
                                                    <Columns>
                                                      <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                                           <ItemTemplate>
                                                               <asp:ImageButton ID="Img_Ver2" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                                   OnClick="Button2_Click" ToolTip='<%# Eval("sbl_num") %>' style="height: 13px" />
                                                           </ItemTemplate>
                                                           <HeaderStyle HorizontalAlign="Center" />
                                                           <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                      </asp:TemplateField>
                                                        <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                         </asp:BoundField>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="deu_nom" HeaderText="Razón">
                                                            <ItemStyle HorizontalAlign="left" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="sbl_mto" HeaderText="Mto. Sub-Linea" DataFormatString="{0:###,###,##0}">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sbl_pct" HeaderText="%">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sbl_num" HeaderText="Nº">
                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:BoundField>
                                                        
                                                    </Columns>
                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                    <RowStyle CssClass="formatUltcell" />
                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="600">
                                        <tr>
                                            <td align="right" style="width: 75px">
                                                <asp:Label ID="Label35" runat="server" CssClass="SubTitulos" Style="position: static"
                                                    Text="Producto"></asp:Label></td>
                                            <td align="left" colspan="1">
                                                <asp:RadioButton ID="RB_Pro_Mto" runat="server" CssClass="Label" Enabled="False"
                                                    GroupName="Producto" Style="position: static" Text="Mto. Sub Linea" AutoPostBack="True"
                                                    Checked="True" />
                                                <asp:RadioButton ID="RB_Pro_Por" runat="server" CssClass="Label" Enabled="False"
                                                    GroupName="Producto" Style="position: static" Text="Porcentaje" AutoPostBack="True" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                                <table border="0" cellpadding="0" cellspacing="0" style="position: static">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label36" runat="server" CssClass="Label" Style="position: static"
                                                                Text="Tipo Documento"></asp:Label></td>
                                                        <td style="width: 100px">
                                                            <asp:DropDownList ID="DP_Pro_Tip" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                Style="position: static" Width="100px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Lbl_Valor_Pro" runat="server" CssClass="Label" Style="position: static"
                                                                Text="Monto"></asp:Label></td>
                                                        <td>
                                                         
                                                            <asp:TextBox ID="Txt_Pro_Val" runat="server" CssClass="clsDisabled" Width="100px" ReadOnly="True"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="Txt_Pro_Val_MaskedEditExtender" runat="server" 
                                                                    AcceptNegative="Left" CultureAMPMPlaceholder="" 
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" 
                                                                    MaskType="Number" TargetControlID="Txt_Pro_Val">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 75px; height: 19px;">
                                            </td>
                                            <td align="left">
                                              <asp:GridView ID="GV_SubPro" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                    CssClass="formatUltcell" EnableTheming="True" PageSize="3"
                                                    ShowHeader="true">
                                                    <Columns>
                                                       <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                                           <ItemTemplate>
                                                               <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                                   OnClick="Button1_Click" ToolTip='<%# Eval("sbl_num") %>' style="height: 13px" />
                                                           </ItemTemplate>
                                                           <HeaderStyle HorizontalAlign="Center" />
                                                           <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                       </asp:TemplateField>
                                                        <asp:BoundField DataField="sbl_num" HeaderText="Nº">
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                            <FooterStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="id_P_0031_des" HeaderText="Producto">
                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sbl_mto" HeaderText="Mto Sub-Linea" DataFormatString="{0:###,###,##0}">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sbl_pct" HeaderText="%">
                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lb_prod" Text='<% #eval("id_p_0031") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                    <RowStyle CssClass="formatUltcell" />
                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                </asp:GridView>
                                                
                                                 
                                            </td>
                                        </tr>
                                    </table>
                                   <br />
                                   
                                    <br />
                                    </asp:View>
                            </asp:MultiView></td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td align="right">
                <br />
                
                
                <asp:ImageButton ID="IB_Nuevo" runat="server" AlternateText="Nueva Sub Linea" 
                    ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" 
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                    Style="position: static" OnClick="IB_Nuevo_Click" />
                <asp:ImageButton ID="IB_Guardar" runat="server" AlternateText="Guardar Datos" 
                    ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" 
                    OnClick="IB_Guardar_Click"  Style="position: static" />
                <asp:ImageButton ID="Ib_Eliminar" runat="server" AlternateText="Eliminar Sub Linea"
                    ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif" OnClick="Ib_Eliminar_Click"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';" 
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';"
                    Style="position: static" />
                <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar Datos" 
                    ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';"
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';" 
                    OnClick="IB_Limpiar_Click" Style="position: static" />
                    
                <asp:ImageButton ID="IB_Volver" runat="server" AlternateText="Volver a Linea de Crédito" 
                    ImageUrl="~/Imagenes/Botones/Boton_volver_out.gif"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_volver_out.gif';"
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_volver_in.gif';" 
                    OnClick="IB_Volver_Click" Style="position: static" />
            </td>
        </tr>
    </table>
    
   
    
        <asp:LinkButton ID="Detalle_Pro" runat="server" OnClick="Detalle_Pro_Click" Style="position: static"></asp:LinkButton>
        <asp:LinkButton ID="Detalle_Deu" runat="server" OnClick="Detalle_Deu_Click" Style="position: static"></asp:LinkButton>
        <asp:LinkButton ID="LB_Buscar_Deudor" runat="server" OnClick="LB_Buscar_Click" Style="position: static"></asp:LinkButton>
        <asp:HiddenField ID="sw" runat="server" />
        <asp:HiddenField ID="Pos_Pro" runat="server" />
        <asp:HiddenField ID="sbl_num" runat="server" />
        
     
        <uc1:Mensaje ID="Mensaje1" runat="server" />
        
     
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Volver" />
        </Triggers>
    </asp:UpdatePanel>

    </form>
</body>
</html>
