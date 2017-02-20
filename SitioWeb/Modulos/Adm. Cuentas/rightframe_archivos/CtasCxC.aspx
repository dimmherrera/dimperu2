<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="CtasCxC.aspx.vb" Inherits="ClsCtasXCobrar" Title="Mantención Ctas. Por Cobrar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">

function DoScroll()
 {
    var _gridView = document.getElementById("GridViewDiv");
//    var _header = document.getElementById("HeaderDiv");
//     _header.scrollLeft = _gridView.scrollLeft;
 }

    </script>
          <table border="0" cellpadding="0" class="Contenido" cellspacing="1"  width="100%">
                <tr>
                    <td class = "Cabecera" height="31">
                        <asp:Label ID="Label7" runat="server" CssClass="Titulos" Text="Administración - Cuentas por Cobrar"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido" align="Center">
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="580px">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-left: 5px;" align="left">
                                        <br />
                                        <table border="0" cellpadding="0" cellspacing="0" style="position: static; width: 1200px;
                                            margin-right: 4px;">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Proveedores"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td_Cli" class="Contenido">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right" style="width: 125px">
                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Número Identificación"></asp:Label>
                                                            </td>
                                                            <td style="width: 400px">
                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                </cc1:MaskedEditExtender>
                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" Width="16px"
                                                                     MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Proveedores" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                    Width="20px" Style="margin-top: 0px" />
                                                            </td>
                                                            <td align="right" style="width: 190px">
                                                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Nombre o Razón Social Proveedor"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="400px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px" align="left">
                                       <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                         <td class="Cabecera" style="width: 1200px">
                                          <asp:Label ID="Label24" runat="server" Text=" Tipo de Cuenta" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                             </tr>
                                                                <tr>
                                                                    <td class="Contenido" style="width: 1200px">
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td width="125px">

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="dropTipoCuenta" CssClass="clsTxt"  runat="server" 
                                                                                        Enabled="true" Width="200px" AutoPostBack="True" >
                                                                                    </asp:DropDownList>
                                                                                  
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                       
                                                
                                    </td>
                                </tr>
                                <tr>
                               
                                    <td height="70" style="padding-left: 5px;" align="left">
                                     <br />
                                        <%--<asp:Panel ID="Panel3" runat="server" CssClass="Contenido" Height="100px" Width="515px">--%>
                                        <table border="0" cellpadding="0" cellspacing="0" style="position: static; width: 1200px;">
                                            <tr>
                                                <td align="left" class="Cabecera">
                                                    <asp:Label ID="Label9" runat="server" CssClass="SubTitulos" Text="Datos Ctas. por Cobrar"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td align="right" width="125px" style="height: 17px">
                                                                <asp:Label ID="Label8" runat="server" Text="Tipo de Moneda" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td width="400px">
                                                                <asp:DropDownList ID="drop_TpMoneda" runat="server"  CssClass="clsDisabled" ReadOnly="True"  Enabled="False" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" width="190px">
                                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Fecha"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="Txt_Fec_Cxc" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Monto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Monto" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                     MaxLength="10" AutoPostBack="false" Width="120px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Monto_MaskedEditExtender" runat="server" 
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="true" 
                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                    TargetControlID="Txt_Monto">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                    Width="390px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                               <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Nro Contrato" 
                                                                    ></asp:Label> 
                                                            </td>
                                                            <td>
                                                            <asp:TextBox ID="txt_Contrato" runat="server" CssClass="clsDisabled" 
                                                                 Width="150px"></asp:TextBox>
                                                                <asp:ImageButton ID="IB_AyudaDoc" runat="server" 
                                                                    AlternateText="Ayuda Documentos" ImageUrl="../../../Imagenes/Iconos/155.ICO" 
                                                                    Style="margin-top: 0px" Width="20px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                    <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Nro Docto."></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:TextBox ID="txt_nro_doc" runat="server" CssClass="clsDisabled" Width="120px" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--</asp:Panel>--%>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="70" style="padding-left: 5px;" align="left">
                                        <%--**********Cabecara Grilla***********--%>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label5" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" >
                                                        <asp:Panel ID="Panel_GrCXC" runat="server" ScrollBars="Horizontal" Width="1200px" Height="230px" >
                                                        
                                                        <asp:GridView ID="GV_Cuentas" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                            PageSize="8" Width="1230px">
                                                            <Columns>
                                                               
                                                               
                                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                    ItemStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_cxc") %>'
                                                                            OnClick="Img_Ver_Click" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cuenta" >
                                                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="id_cxc" HeaderText="Nº Cuenta">
                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Contrato" HeaderText="Nº Contrato">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" /> 
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Numero" HeaderText="Nº Docto.">
                                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                    <ItemStyle Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_mto" HeaderText="Monto">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_des" HeaderText="Descripción">
                                                                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla"  />
                                                        </asp:GridView>
                                                         </asp:Panel>
                                                   
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                            <td align="center">
                                              <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                              <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="lb_id_doc" Visible="false" runat="server">LinkButton</asp:LinkButton>
                        <asp:ImageButton ID="IB_Buscar" runat="server" AlternateText="Buscar Proveedores" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            OnClick="IB_Buscar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" Style="position: static" />
                        <asp:ImageButton ID="IB_Nuevo" runat="server" AlternateText="Nueva Cuenta" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                            OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" Style="position: static"
                            Enabled="False" />
                        <asp:ImageButton ID="IB_Anular" runat="server" AlternateText="Anular Cuenta" Enabled="False"
                            ImageUrl="~/Imagenes/Botones/boton_anular_out.gif" OnClick="IB_Anular_Click"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_anular_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_anular_in.gif';"
                            Style="position: static" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" AlternateText="Guardar Cuenta" Enabled="False"
                            ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif" OnClick="IB_Guardar_Click"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                            Style="position: static" />
                        <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprime Informe"
                            Enabled="False" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif" OnClick="IB_Imprimir_Click"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            Style="position: static" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpia Pantalla" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            OnClick="IB_Volver_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" Style="position: static" />
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="txt_id_doc" runat="server" />
            <asp:HiddenField ID="HF_Id" runat="server" />
            <asp:HiddenField ID="HF_Pos" runat="server" />
            <asp:HiddenField ID="TipoCuenta" runat="server" />
            <asp:HiddenField ID="NroCuenta" runat="server" />
            <asp:LinkButton ID="lb_deu" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lb_cli" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="hf_id_doc" runat="server" />
            <asp:LinkButton ID="LinkbN_Cuenta" runat="server"></asp:LinkButton>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
            <asp:PostBackTrigger ControlID="lb_id_doc" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guarda" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Link_Anular" runat="server"></asp:LinkButton>
</asp:Content>
