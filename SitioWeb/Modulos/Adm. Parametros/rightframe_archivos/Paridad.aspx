<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Paridad.aspx.vb" Inherits="Paridad" Title="Paridades" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="position: static" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <tbody>
                    <tr>
                        <td style="text-align:-moz-center" align="center" class="Cabecera">
                            &nbsp;<asp:Label Style="position: static" ID="Label33" runat="server" CssClass="Titulos">Mantenimiento-Administración de Paridades</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px; padding-top: 15px; width:100%" class="Contenido">
                            <table style="position: static; height:100%" cellspacing="0" cellpadding="0" width="100%"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td valign="top" width="200px" height="100%">
                                        <table>
                                        <tr>
                                        <td class="Cabecera">
                                         <asp:Label Style="position: static" ID="Label5" runat="server" CssClass="SubTitulos" Text="Criterios"
                                                                Width="111px"></asp:Label>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="Contenido">
                                           <table style="width: 486px" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 136px" align="right">
                                                            <asp:Label Style="position: static" ID="Label6" runat="server" CssClass="Label" Text="Dgos. y Festivos"
                                                                Width="111px"></asp:Label>
                                                        </td>
                                                        <td style="width: 112px">
                                                            <asp:TextBox Style="position: static" ID="TextBox2" runat="server" CssClass="clsTxt"
                                                                Width="34px" BackColor="DarkRed" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 136px" align="right">
                                                            <asp:Label Style="position: static" ID="Label4" runat="server" CssClass="Label" Text="Sabado"></asp:Label>
                                                        </td>
                                                        <td style="width: 112px">
                                                            <asp:TextBox Style="position: static" ID="TextBox3" runat="server" CssClass="clsTxt"
                                                                Width="34px" BackColor="#FFE0C0" Enabled="false"></asp:TextBox>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 136px" align="right">
                                                            <asp:Label Style="position: static" ID="Label1" runat="server" CssClass="Label" Text="Año"></asp:Label>
                                                        </td>
                                                        <td style="width: 112px">
                                                            <asp:TextBox Style="position: static" ID="Txt_Ano" runat="server" CssClass="clsTxt"
                                                                Width="90px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="Txt_Ano_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_Ano">
                                                            </cc1:FilteredTextBoxExtender>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Btn_menos" runat="server" Height="21px" Width="25px" Text="-" CausesValidation="False">
                                                            </asp:Button>
                                                            <asp:Button ID="Btnmas" runat="server" Height="21px" Width="25px" Text="+"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 136px" align="right">
                                                            <asp:Label Style="position: static" ID="Label2" runat="server" CssClass="Label" Text="Mes"></asp:Label>
                                                        </td>
                                                        <td style="width: 112px">
                                                            <asp:DropDownList Style="position: static" ID="Dr_mes" runat="server" CssClass="clsTxt"
                                                                Width="102px" AutoPostBack="True">
                                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 136px" align="right">
                                                            <asp:Label Style="position: static" ID="Label3" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                        </td>
                                                        <td style="width: 112px">
                                                            <asp:DropDownList Style="position: static" ID="Dr_mon" runat="server" CssClass="clsTxt"
                                                                Width="102px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        </tr>                                        
                                        </table>
                                         
                                        </td>
                                        <td valign="top" width="80%">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="height: 520px">
                                                        <asp:Panel ID="Panel1" runat="server" Height="540px" ScrollBars="Vertical" Width="100%">
                                                            <asp:GridView ID="Gv_paridades" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                PageSize="7" ShowHeader="true" Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Fec_Corta" HeaderText="Dia" ApplyFormatInEditMode="True">
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="par_fec" HeaderText="Fecha" DataFormatString="{0:d}">
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:BoundField>
                                                                    <%--Se declara el campo como templatefield--%>
                                                                    <asp:TemplateField HeaderText="Valor">
                                                                        <ItemTemplate>
                                                                            
                                                                            <%--   En el template se crea un textbox y el valor que debe mostrar (campo del dataset) , se le da en el text , con esta expresion ( Text='<%#Eval("CAMPO")%>')--%>
                                                                            <asp:TextBox ID="Txt_valor" runat="server" Text='<%#Eval("par_val")%>'>></asp:TextBox>
                                                                            <cc1:MaskedEditExtender ID="Txt_valor_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="," CultureThousandsPlaceholder="." CultureTimePlaceholder=""
                                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999.9999" MaskType="Number"
                                                                                TargetControlID="Txt_valor" CultureName="es-ES">
                                                                            </cc1:MaskedEditExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Valor Cobrar">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="Txt_valcob" runat="server" Text='<%#Eval("par_val_cob")%>'></asp:TextBox>
                                                                            <cc1:MaskedEditExtender ID="Txt_valor_MaskedEditExtender_1" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="," CultureThousandsPlaceholder="." CultureTimePlaceholder=""
                                                                                Enabled="True" InputDirection="RightToLeft" Mask="999,999.9999" MaskType="Number"
                                                                                TargetControlID="Txt_valcob" CultureName="es-ES">
                                                                            </cc1:MaskedEditExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <SelectedRowStyle BorderColor="Red"></SelectedRowStyle>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;&nbsp;
                            <table style="position: static" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 29px">
                                            <asp:ImageButton ID="Btn_con1" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_In.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" OnClick="Btn_Buscar_Click"
                                                runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Buscar_out.gif" ToolTip="Buscar">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="height: 29px">
                                            <asp:ImageButton ID="Btn_Guar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="btn_gua1_Click"
                                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" ToolTip="Guardar">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="height: 29px">
                                            <asp:ImageButton ID="btn_lim" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="btn_limp1_Click"
                                                runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" ToolTip="Limpiar">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <cc1:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" Width="20"
                TargetControlID="Txt_Ano" TargetButtonDownID="Btn_menos" TargetButtonUpID="Btnmas">
            </cc1:NumericUpDownExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
