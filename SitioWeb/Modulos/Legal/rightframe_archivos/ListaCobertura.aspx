<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Modulos/Master/MasterPage.master" CodeFile="ListaCobertura.aspx.vb" Inherits="ListaCobertura"  Title="Lista Cobertura"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <script language="javascript">
function DoScroll()
 {
    var _gridView = document.getElementById("GridViewDiv");
//    var _header = document.getElementById("HeaderDiv");
//     _header.scrollLeft = _gridView.scrollLeft;
 }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="tabla General" border="0" cellpadding="0" cellspacing="1" width="100%"
                class="Contenido" align="center">
                <tr>
                    <td class="Cabecera" height="31">
                        <asp:Label ID="Label1" runat="server" Text="Legal - Lista Cobertura" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td class="Contenido" style="height: 590px; padding: 5px" valign="top" align="center">
                        <table id="Tabla Contenedora" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td valign="top" >
                                                <table id="Fecha" border="0" cellpadding="0" cellspacing="0" style="width: 130px">
                                                    <tr>
                                                        <td class="Cabecera" style="width: 148px">
                                                            <asp:Label ID="Label2" runat="server" Text="Fecha Cobertura" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" style="width: 148px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Fecha_Cob" runat="server" ReadOnly="true" CssClass="clsDisabled"
                                                                            Width="90px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="middle" class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CB_Suc" runat="server" Text="Todas las Sucursales" CssClass="Label" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <table id="tabla Cli" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" style="width: 162px">
                                                            <asp:Label ID="Label3" runat="server" Text="Estado Clientes" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" style="width: 162px">
                                                            <asp:RadioButtonList ID="RB_Clientes" runat="server" RepeatDirection="Horizontal"
                                                                CssClass="Label">
                                                                <asp:ListItem Value="1" Selected="True">Todos</asp:ListItem>
                                                                <asp:ListItem Value="2">Activos</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 930px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label15" runat="server" Text="Clientes" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 187px">
                                                            <asp:RadioButton ID="RB_Todos_CLi" runat="server" AutoPostBack="true" Text="Todos los Clientes"
                                                                CssClass="Label" />
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:RadioButton ID="RB_Cli_Con_Ope" runat="server" Text="Clientes con Operaciones Sim/Apro. por legal"
                                                                CssClass="Label" AutoPostBack="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 187px">
                                                            <asp:RadioButton ID="RB_PorCliente" runat="server" Text="Por Cliente" CssClass="Label"
                                                                AutoPostBack="true" />
                                                        </td>
                                                        <td style="width: 42px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                            Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_Rut_Cli_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                            Width="17px" AutoPostBack="true" MaxLength="1"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCLi" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="25px" Enabled="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                            Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                <td align="center">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 930px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label4" runat="server" Text="Cobertura Pagaré" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table id="Tabla Cabecera">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel_Gr_ListaCobertura" runat="server" ScrollBars="None" Width="915px"
                                                                Height="320px">
                                                                <asp:GridView ID="Gr_ListaCobertura" runat="server" Width="895px" CssClass="formatUltcell"
                                                                    AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="cli_idc" HeaderText="NIT Cliente">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cli_rso" HeaderText="Razón Social">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Mto_ocu" HeaderText="Monto Ocupado">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Monto" HeaderText="Monto Pagaré">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="pgr_mdt" HeaderText="Mandato">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="95px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                            AlternateText="Anterior" />
                                                                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                            AlternateText="Siguiente" />
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
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Total Generales" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="$" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_totG1" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="$" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_totG2" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Diferencia" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="$" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Dif" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%--*************Botonera***********--%>
                        <asp:HiddenField ID="HF_Pos" runat="server" />
                        <asp:HiddenField ID="HF_Id" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel_Buscar" runat="server">
                            <ContentTemplate>
                                <asp:ImageButton ID="IB_Buscar" runat="server" AlternateText="Buscar" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                                <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprimir" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                                    Enabled="false" onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                                    onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" />
                                <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel_Buscar">
    <ProgressTemplate>
        <uc1:Cargando ID="Cargando1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
