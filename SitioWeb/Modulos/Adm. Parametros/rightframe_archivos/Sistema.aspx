<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Sistema.aspx.vb" Inherits="Sistema" Title="Sistema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" align="center" style="text-align: -moz-center">
                        <asp:Label ID="Label19" runat="server" CssClass="Titulos" 
                            Text="Mantenimiento - Sistema"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="605px" class="Contenido" valign="top" style="width:100%">
                        <table>
                            <tr>
                                <td align="left">
                                    <table class="Cuadrado" width="100%" >
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label7" runat="server" CssClass="SubTitulos" Text="Eliminar Cliente-Pagador"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 198px">
                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Cant de días desde Última Ope."></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="Txt_elicli" runat="server" CssClass="clsMandatorio" Width="60px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_elicli_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_elicli">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table class="Cuadrado">
                                        <tr>
                                            <td colspan="2" align="left">
                                                <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" 
                                                    Text="Validación Fec. Prox Gestión y Pago"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Cant de días"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_valfec" runat="server" CssClass="clsMandatorio" TabIndex="1"
                                                    Width="63px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_valfec_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_valfec">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:Label ID="Label13" runat="server" Font-Size="Smaller" ForeColor="Red" Text="(Valor 0 desactiva la validación)"
                                                    Width="171px" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="Cuadrado" width="100%">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label6" runat="server" CssClass="SubTitulos" 
                                                    Text="Interés a Devolver"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 199px">
                                                <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Cant de días"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_int" runat="server" CssClass="clsMandatorio" TabIndex="2" Width="60px"
                                                    Wrap="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_int_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_int">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left">
                                    <table class="Cuadrado" width="200px">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Modificar Iva"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 81px">
                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Valor"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_IVA" runat="server" CssClass="clsMandatorio" TabIndex="3" Width="63px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_IVA_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_IVA" ValidChars=".,">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="Cuadrado" width="100%">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label20" runat="server" CssClass="SubTitulos" 
                                                    Text="Días mínimos Vencimiento"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 198px">
                                                <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Cant de días"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_vto" runat="server" CssClass="clsMandatorio" TabIndex="2" Width="60px"
                                                    Wrap="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_vto_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_vto">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table class="Cuadrado" width="100%">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" 
                                                    Text="Días antes vcto. para solicitar prorroga"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 198px">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Cant de días"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Prorroga" runat="server" CssClass="clsMandatorio" 
                                                    TabIndex="2" Width="60px"
                                                    Wrap="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_vto">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="Cuadrado" width="100%">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Calculo GMF"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 198px">
                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="4 * 1000"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_gmf" runat="server" CssClass="clsMandatorio" TabIndex="2" Width="60px"
                                                    Wrap="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_gmf">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                   <table class="Cuadrado" width="100%">
                                       <tr>
                                          <td align="left" colspan="2">
                                              <asp:Label ID="Label9" runat="server" CssClass="SubTitulos" Text="Días de Vencimiento para Bloqueo de Cupo"></asp:Label>
                                          </td>
                                       </tr>
                                       <tr>
                                          <td align="right" style="width: 198px">
                                             <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Cant de días"></asp:Label>   
                                          </td>                                           
                                          <td align="left">
                                              <asp:TextBox ID="Txt_Vto_LDC" runat="server" CssClass="clsMandatorio" TabIndex="3" Width="60px"
                                                     Wrap="false"></asp:TextBox>
                                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                                                   Enabled="true" FilterType="Numbers" TargetControlID="Txt_Vto_LDC">
                                              </cc1:FilteredTextBoxExtender> 
                                          </td> 
                                       </tr>
                                   </table>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    <table class="Cuadrado" width="100%">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label11" runat="server" CssClass="SubTitulos" 
                                                    Text="¿Valida línea de financiamiento?"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 198px">
                                                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Opciones"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_gmf">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RadioButtonList ID="RB_Val_Lin" runat="server" CssClass="Label" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="S">SI</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">NO</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                  
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                            OnClick="btn_gua1_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" ToolTip="Guardar" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
