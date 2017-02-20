<%@ Page Title="" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="VerificacionDocumentos.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_VerificacionDocumentos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="../../Pizarras/rigthframe_archivos/Documentacion.ascx" tagname="Documentacion" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel runat="server" ID="UP_Verificacion" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tb_gral" cellpadding="0" cellspacing="1" style="width: 100%" class="Contenido">
                <tr>
                    <td class="Cabecera" style="height: 31px">
                        <asp:Label ID="Titulo" runat="server" CssClass="Titulos" Text="OPERACIONES - Verificación de Documentos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="Contenido" style="padding: 10px;  text-align: -moz-center"
                        valign="top">
                        <table align="center" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" style="text-align: -moz-center">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left">
                                                            <asp:Label ID="Lbl_Cliente" runat="server" CssClass="SubTitulos" Style="left: 8px;
                                                                position: static; top: -14px" Text="Proveedor"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Contenido" style="height:30px">
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Número Identificación Proveedor"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc2:MaskedEditExtender>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" MaxLength="1"
                                                                            Width="15px" AutoPostBack="True" CausesValidation="True"> </asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Proveedor" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="23px" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Lbl_Raz_Soc" runat="server" CssClass="Label" Text="Nombre / Razón Social Proveedor"></asp:Label>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="True" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td align="left" class="Cabecera">
                                                            <asp:Label ID="Label14" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                                top: -14px" Text="Criterio de Búsqueda"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" align="left" style="height:30px">
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Label ID="Label17" runat="server" CssClass="Label" Text="N° Operación"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="Txt_Nro_ope" runat="server" CssClass="clsMandatorio" TabIndex="1"
                                                                                                    Width="95px"></asp:TextBox>
                                                                                                <cc2:MaskedEditExtender ID="Txt_Nro_ope_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                                    Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Nro_ope">
                                                                                                </cc2:MaskedEditExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="text-align: -moz-center">
                                    <br />
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                    top: 14px" Text="Operaciones Digitadas y Simuladas"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" height="170" valign="top" align="center">
                                                <asp:GridView ID="GV_Operaciones" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                    HeaderStyle-CssClass="cabeceraGrilla" PageSize="5">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="90px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("NroNeg") %>'
                                                                    OnClick="Img_Ver_Click" />
                                                                <asp:HiddenField ID="HF_RutCli" runat="server" Value='<%# Eval("rut") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NroNeg" HeaderText="Nro.">
                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="rut" HeaderText="Identificación">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cliente" HeaderText="Nombre / Razón Social Proveedor">
                                                            <ItemStyle HorizontalAlign="left" Width="300px" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipDoc" HeaderText="Tipo Docto.">
                                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaNeg" HeaderText="Fecha Ing.">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PorAnt" HeaderText="% Anticipo">
                                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MtoOpe" HeaderText="Monto Doctos.">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                      <%--  <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Operación">
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                        </asp:BoundField>--%>
                                                    </Columns>
                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                    <RowStyle CssClass="formatUltcell" />
                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                          <tr>
                                              <td align="center">
                                                  <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                      onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                                  <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                      onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                                              </td>
                                          </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                        <tr>
                                            <td align="left" class="Cabecera">
                                                <asp:Label ID="Label25" runat="server" CssClass="SubTitulos" Style="left: 8px; position: static;
                                                    top: 14px" Text="Documentación a verificar"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido" height="200" valign="top" align="center">
                                                <table width="700" cellspacing="10">
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:DropDownList ID="DP_Tipo" runat="server" CssClass="clsMandatorio" Enabled="true"
                                                                Width="250px" AutoPostBack="True">
                                                                <asp:ListItem Value="1">Documentos Legales</asp:ListItem>
                                                                <asp:ListItem Value="2">Documentos Operación</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Panel ID="Panel_Gr_DocCom" runat="server" Height="280px" ScrollBars="Auto">
                                                                <asp:GridView ID="Gr_DocCom" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    ShowHeader="true">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="IB_TodosDoc" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                                                    ToolTip="Seleccionar todos" OnClick="IB_TodosDoc_Click" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CB_DOC" runat="server" CssClass="Label" ToolTip='<%#eval("id") %>'
                                                                                    AutoPostBack="True" OnCheckedChanged="CB_DOC_CheckedChanged" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="des" HeaderText="Descripción">
                                                                            <ItemStyle Width="350px" HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="usuario" HeaderText="usuario">
                                                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="fecha" HeaderText="fecha aprobación">
                                                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="margin-left: 120px">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Buscar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        ToolTip="Buscar" Style="margin-left: 0px" />
                                </td>
                                 <td align="center">
                                    <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Guardar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                        ToolTip="Guardar" Style="margin-left: 0px" />
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_Limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="HF_NroOpe" runat="server" />
                 
            
        </ContentTemplate>
       
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>   
    
    
    
    
</asp:Content>

