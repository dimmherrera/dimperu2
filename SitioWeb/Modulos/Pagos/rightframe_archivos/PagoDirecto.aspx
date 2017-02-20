<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="PagoDirecto.aspx.vb" Inherits="Modulos_Pagos_rightframe_archivos_PagoDirecto" EnableEventValidation="true" 
    Title="Pago Directo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/PDirecto.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
    <ProgressTemplate></ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table width="100%" cellspacing="1" cellpadding="0" border="0" class="Contenido">
                <tr>
                    <td valign="middle" class = "Cabecera" height="31">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Cancelaciòn - Pago Directo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 5px; height: 550px" class="Contenido">
                        <table id="Tb_General" border="0" cellpadding="0" cellspacing="0" width="99%">
                            <tr>
                                <td valign="top" align="left">
                                    
                                    <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="Limit"
                                        RequireOpenedPane="true" SuppressHeaderPostbacks="true" Height="200px" Width="850">
                                         <Panes>
                                            <cc1:AccordionPane ID="AccordionPane2" runat="server">
                                                  <Header>
                                                    <a href="" class="accordionLink">Criterio de Búsqueda: </a>
                                                    <asp:Label ID="LB_Cliente" CssClass="accordionLabel" runat="server" Text="Cliente"></asp:Label>
                                                    <asp:Label ID="Label23" CssClass="accordionLabel" runat="server" Text="  /  "></asp:Label>
                                                    <asp:Label ID="LB_Deudor" CssClass="accordionLabel" runat="server" Text="Pagador"></asp:Label>
                                                </Header>
                                                <Content>
                                                     <%--Criterio--%>
                                                      <table  border="0" cellpadding="1" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <%--Cliente--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="CB_Cliente" runat="server" Text="Cliente" CssClass="Label" Font-Bold="true"
                                                                                AutoPostBack="True" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled"
                                                                                 Style="position: static" TabIndex="1" Width="90px" ></asp:TextBox>
                                                                            <asp:TextBox ID="Txt_Dig_Cli" runat="server"  CssClass="clsDisabled"
                                                                                MaxLength="1" Style="position: static" TabIndex="1" Width="15px" 
                                                                                AutoPostBack="true"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="Txt_Dig_Cli"
                                                                                FilterType="Custom,numbers" ValidChars="k,K" runat="server">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                                Width="20px" Style="margin-top: 0px" Enabled="false" />
                                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli" >
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" MaxLength="50"
                                                                                Style="position: static" Width="270px" ></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top" align="left">
                                                                
                                                                <%--Pagador--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label11" runat="server" Text="Pagador" CssClass="Label" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:RadioButtonList ID="RB_Pag" runat="server" CssClass="Label" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Selected="True" Value="C">Cliente</asp:ListItem>
                                                                                <asp:ListItem Value="D">Pagador</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
                                                            </td>
                                                        </tr>
                                                        <td valign="top">
                                                            <%--Deudor--%>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="CB_Deudor" runat="server" Text="Pagador" CssClass="Label" Font-Bold="true" AutoPostBack="True" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server"  CssClass="clsDisabled"
                                                                             Style="position: static" TabIndex="1" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server"  CssClass="clsDisabled"
                                                                            MaxLength="1" Style="position: static" TabIndex="1" Width="15px" ReadOnly="True"
                                                                            AutoPostBack="true"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="Txt_Dig_Deu"
                                                                            FilterType="Custom,numbers" ValidChars="k,K" runat="server">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <asp:ImageButton ID="IB_AyudaDeu" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" Style="margin-top: 0px" Enabled="false" />
                                                                        <cc1:MaskedEditExtender ID="Maskededitextender1" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" CssClass="clsDisabled" MaxLength="50"
                                                                            Style="position: static" Width="270px" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                                <Header>
                                                    <a href="" class="accordionLink">Parámetros: </a>
                                                    <asp:Label ID="LB_Parametros" CssClass="accordionLabel" runat="server" Text=""></asp:Label>
                                                </Header>
                                                <Content>
                                                    <%--Parametros--%>
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <%--Fecha Pago--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label10" runat="server" Text="Fecha Pago" CssClass="Label" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Fec_Pag" runat="server" CssClass="clsMandatorio" Width="70"
                                                                                AutoPostBack="true"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="Txt_Fec_Pag" runat="server" Format="dd-MM-yyyy" CssClass="radcalendar" FirstDayOfWeek="Monday">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:MaskedEditExtender ID="Txt_Fec_Pag_MaskedEditExtender" runat="server" Mask="99/99/9999"
                                                                                MaskType="Date" TargetControlID="Txt_Fec_Pag">
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <%--Tasas--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label12" runat="server" Text="Tasa" CssClass="Label" Font-Bold="true" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Tasa" runat="server" CssClass="clsDisabled" Width="50" ReadOnly="true" Visible="false"></asp:TextBox>
                                                                         <cc1:MaskedEditExtender ID="Txt_Por_Ant_MaskedEditExtender" runat="server" 
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                                                CultureName="es-ES" CultureThousandsPlaceholder="." CultureTimePlaceholder="" 
                                                                Enabled="True" InputDirection="RightToLeft" Mask="999.99" MaskType="Number" 
                                                                TargetControlID="Txt_Tasa">
                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Iconos/add.png" Width="20px" Enabled="false" OnClick="ImageButton1_Click" Visible="false" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <%--Dias devolver interes--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="Label13" runat="server" Text="Días Dev.Inter." CssClass="Label" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_DiasDevInt" runat="server" CssClass="clsDisabled" Width="50"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="Txt_DiasDevInt"
                                                                                runat="server" FilterType="Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Iconos/add.png" Width="20px" Enabled="false" OnClick="ImageButton2_Click"/>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <%--Dias retencion--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label14" runat="server" Text="Dias Retenc." CssClass="Label" Font-Bold="true" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="DP_Plaza" runat="server" Width="150px" CssClass="clsTxt" AutoPostBack="True" Visible="false" />
                                                                            <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="DP_Plaza"
                                                                                PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                                                PromptPosition="Bottom" IsSorted="true" Enabled="false">
                                                                            </cc1:ListSearchExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Dia_Ret" runat="server" CssClass="clsDisabled" Width="50" ReadOnly="true" Visible="false" Text="0"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="Txt_Dia_Ret"
                                                                                FilterType="Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <%--Factor--%>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <asp:Label ID="Label2" runat="server" Text="Factor de cambio / Pago de Dolares" CssClass="Label"
                                                                                Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label4" runat="server" Text="Cobranza" CssClass="Label"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Dolar_Cob" runat="server" CssClass="clsDisabled" Width="80"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredText_Txt_Dolar_Cob" runat="server" TargetControlID="Txt_Dolar_Cob"
                                                                                FilterType="Custom,Numbers" ValidChars=",.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label5" runat="server" Text="Observado" CssClass="Label"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txt_Dolar_Obs" runat="server" CssClass="clsDisabled" Width="80"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--Observacion Pago--%>
                                                    <table border="0" cellpadding="2" cellspacing="0" style="border: 1px solid #000000;"
                                                        width="99.5">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Observación de Pago" CssClass="Label"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Observacion" runat="server" CssClass="clsDisabled" TextMode="MultiLine"
                                                                    Width="800" Height="35" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                    <br />
                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="300px" Width="1000px" AutoPostBack="false">
                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Forma Pago">
                                            <HeaderTemplate>
                                                Forma Pago
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="IB_Doctos" runat="server" Text="Asociar Doctos." Enabled="false"
                                                            CssClass="boton" />
                                                        <div class="Contenido" style="height:280px">
                                                        <table border="0" cellpadding="2" cellspacing="0" width="850">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:DropDownList ID="DP_Moneda" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                        Enabled="False" Width="100">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Forma de Pago"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DP_FormaPago" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                        Enabled="False" EnableTheming="True" Width="200px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Monto del Pago"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Mto_Rec" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="Txt_Mto_Rec_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                        TargetControlID="Txt_Mto_Rec">
                                                                    </cc1:MaskedEditExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="IB_Agregar" runat="server" ImageUrl="~/Imagenes/btn_workspace/Agregar_out.gif"
                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/Agregar_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Agregar_in.gif';" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <asp:ImageButton ID="IB_Quitar" runat="server" ImageUrl="~/Imagenes/btn_workspace/Quitar_out.gif"
                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/Quitar_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Quitar_in.gif';" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8">
                                                                    <asp:GridView ID="GV_Pagos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                        Width="600px">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_P_0054" HeaderText="Tipo de Pago">
                                                                                <ItemStyle Width="200px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="dpo_num" HeaderText="N° Docto.">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="id_p_0023" HeaderText="Moneda">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="dpo_mto" HeaderText="Mto. Recaudado">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-Width="90px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" OnClick="Button1_Click"
                                                                                        ToolTip='' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                                        <RowStyle CssClass="formatUltcell" />
                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Total a Cancelar"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:TextBox ID="Txt_Total_Cancelar" runat="server" BorderColor="#FF3300" BorderWidth="1px"
                                                                        CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        
                                                        </div>  
                                                        <asp:HiddenField ID="HF_Pos_DPO" runat="server" />  
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="IB_Doctos" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="CXC">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <div id="GridViewDiv2" style="overflow: scroll; width: 990px; height: 295px" onscroll="DoScroll2()">
                                                            <asp:GridView ID="GV_CxC" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                PageSize="8" AllowSorting="True" ShowHeader="true" Width="900" AllowPaging="True">
                                                                <FooterStyle CssClass="cabeceraGrilla" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="id_doc" HeaderText="N° Factura">
                                                                        <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cta.">
                                                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="id_cxc" HeaderText="N° Cta.">
                                                                        <ItemStyle HorizontalAlign="center" Width="80" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Descrip_Cta" HeaderText="Descripción">
                                                                        <ItemStyle HorizontalAlign="center" Width="150" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                        <ItemStyle HorizontalAlign="center" Width="100" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="interes" HeaderText="Interés">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="" HeaderText="Monto Pagar">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cxc_ful_pgo" HeaderText="Fec.Ult.Pag." DataFormatString="{0:dd/MM/yyyy}"
                                                                        NullDisplayText="01/01/1900">
                                                                        <ItemStyle HorizontalAlign="center" Width="80" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Documentos">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="GridViewDiv" style="overflow: scroll; width: 990px; height: 295px" onscroll="DoScroll()">
                                                            <asp:GridView ID="Gr_Documentos" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                PageSize="8" AllowSorting="True" Width="1770px" AllowPaging="True">
                                                                <FooterStyle CssClass="cabeceraGrilla" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="deudor" HeaderText="Razón Social">
                                                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TipoDoctoCorta" HeaderText="T.D.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="id_opn" HeaderText="N° Ope.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="dsi_num" HeaderText="N° Doc.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuo">
                                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fec_ven" HeaderText="Fecha Vcto" DataFormatString="{0:dd/MM/yyyy}">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Saldo" DataFormatString="{0:###,###,##0}" HeaderText="Saldo">
                                                                        <ItemStyle HorizontalAlign="Right" Width="110" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Interes" HeaderText="Interés">
                                                                        <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="MontoPagar" HeaderText="Monto a Pagar">
                                                                        <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="doc_ful_pgo" HeaderText="Fec.Ult.Pag." DataFormatString="{0:dd/MM/yyyy}">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cco_num" HeaderText="Est. Cob.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cco_des" HeaderText="Descrip.Est.Cob.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="EstadoDocto" HeaderText="Estado">
                                                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </td>
                                <td valign="top" rowspan="2" style="border: 1px solid #000000;" align="center">
                                    <%--Totales--%>
                                    <table border="0" cellpadding="1" cellspacing="0" width="140">
                                        <tr>
                                            <td valign="top" align="center">
                                                <table cellspacing="0" style="width: 114px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label25350" runat="server" CssClass="SubTitulos" 
                                                                Text="Datos Diarios"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label25355" runat="server" CssClass="Label" Text="Fecha"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 95px">
                                                                        <asp:TextBox ID="Txt_FecSimulacion" runat="server" AutoPostBack="True" 
                                                                            Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_FecSimulacion_MaskedEditExtender" 
                                                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_FecSimulacion">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label25356" runat="server" CssClass="Label" Text="US$"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 95px">
                                                                        <asp:TextBox ID="vdolar" runat="server" AutoPostBack="True" 
                                                                            CssClass="clsDisabled" ReadOnly="True" Width="90px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 5px">
                                                                        <asp:Label ID="Label25357" runat="server" CssClass="Label" Text="TML"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 95px; height: 5px">
                                                                        <asp:TextBox ID="vtmc" runat="server" AutoPostBack="True" 
                                                                            CssClass="clsDisabled" ReadOnly="True" Width="90px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Total CxC"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Tot_CXC" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:Label ID="Label1" runat="server" Text="Total Doctos." CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Tot_Doc" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" height="20">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:Label ID="Label16" runat="server" Text="TOTALES" CssClass="SubTitulos" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:Label ID="Label15" runat="server" Text="Total Seleccionado" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Tot_Sel" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:Label ID="Label17" runat="server" Text="Total Interés(+)" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Tot_Int" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:Label ID="Label18" runat="server" Text="Total Nota Crédito(-)" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Tot_Not_Cre" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:Label ID="Label19" runat="server" Text="Total Int. a Dev.(-)" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Tot_Int_Dev" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" height="15">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" style="border-style: solid none none none; border-width: 2px;
                                                border-color: #FF0000">
                                                <asp:Label ID="Label20" runat="server" Text="Total a Cobrar." CssClass="SubTitulos"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:TextBox ID="Txt_Total_Cobrar" runat="server" CssClass="clsDisabled" BorderColor="#FF3300"
                                                    BorderWidth="1" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                            ToolTip="Buscar CxC y Doctos. del Cliente" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                            ToolTip="Guardar Pagos" Enabled="False" />
                        <asp:ImageButton ID="Ib_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/BOTON_LIMPIAR_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/BOTON_LIMPIAR_out.gif';" OnClick="IB_limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif" ToolTip="Limpiar">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="HF_SaldoCxC" runat="server" />
            <asp:HiddenField ID="HF_SaldoDoctos" runat="server" />
            <asp:LinkButton ID="LB_BuscaDeudor" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscaCliente" runat="server"></asp:LinkButton>
            <%--****************************************************************************************************--%>
            
            <asp:LinkButton ID="LB_DoctoPago" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="MP_DoctoPago" runat="server" TargetControlID="LB_DoctoPago"
                EnableViewState="False" PopupControlID="Panel_DoctoPago" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="Panel_DoctoPago">
            </cc1:ModalPopupExtender>
            
            <asp:Panel ID="Panel_DoctoPago" runat="server" Width="650px" Height="300px" Style="display: none">
                <%--Style="display: none"--%>
                <table border="0" cellpadding="0" cellspacing="0" class="Contenido">
                    <tr>
                        <td class="Cabecera">
                            <asp:Label ID="Label33" runat="server" Text="Documentos de Pago : Cheque" CssClass="SubTitulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido">
                            <table class="Contenido">
                                <tr>
                                    <td style="height: 100px" valign="top">
                                        <table border="0" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td>
                                                    <%--Banco--%>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" Text="Bancos" CssClass="Label" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="DP_Banco" runat="server" Width="400px" CssClass="clsMandatorio"
                                                                    Enabled="true" />
                                                                <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="DP_Banco"
                                                                    PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba para buscar"
                                                                    PromptPosition="Bottom" IsSorted="true">
                                                                </cc1:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <%--Plaza--%>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" Text="Plaza" CssClass="Label" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="DP_PlazaBanco" runat="server" Width="400px" CssClass="clsMandatorio"
                                                                    Enabled="true" />
                                                                <cc1:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="DP_PlazaBanco"
                                                                    PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                                    PromptPosition="Bottom" IsSorted="true">
                                                                </cc1:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid #000000;">
                                        <%--Datos Docto. de Pago--%>
                                        <asp:Label ID="Label27" runat="server" Text="Datos Docto. de Pago" CssClass="Label"
                                            Font-Bold="true"></asp:Label>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label26" runat="server" Text="N° Docto." CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_NroDocto" runat="server" CssClass="clsMandatorio" ReadOnly="false" Width="100px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="Txt_NroDocto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="999,999,999,999" MaskType="Number" 
                                                        TargetControlID="Txt_NroDocto" InputDirection="RightToLeft">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                                <td>
                                                    
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label28" runat="server" Text="Fecha Emision" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fec_Emi" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                    
                                                    <cc1:calendarextender id="Calendarextender1" runat="server" enabled="True" Format="dd-MM-yyyy"
                                                        targetcontrolid="Txt_Fec_Emi" cssclass="radcalendar" firstdayofweek="Monday">
                                                    </cc1:calendarextender>
                                                    
                                                    <cc1:maskededitextender id="MaskedEditExtender2" runat="server" targetcontrolid="Txt_Fec_Emi"
                                                        mask="99/99/9999" userdateformat="DayMonthYear" masktype="Date">
                                                    </cc1:maskededitextender>
                                                    
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label29" runat="server" Text="Fecha Vcto." CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fec_Vto" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                    
                                                    <cc1:calendarextender id="txt_fec_des_CalendarExtender" runat="server" enabled="True" Format="dd-MM-yyyy"
                                                        targetcontrolid="Txt_Fec_Vto" cssclass="radcalendar" firstdayofweek="Monday">
                                                    </cc1:calendarextender>
                                                    
                                                    <cc1:maskededitextender id="MaskedEditExtender6" runat="server" targetcontrolid="Txt_Fec_Vto"
                                                        mask="99/99/9999" userdateformat="DayMonthYear" masktype="Date">
                                                    </cc1:maskededitextender>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label30" runat="server" Text="Cta. Cte." CssClass="Label"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="Txt_Cta_Cte" runat="server" CssClass="clsMandatorio" Width="100%"
                                                        ReadOnly="false" MaxLength="30"></asp:TextBox>
                                                </td>
                                                <td align="right" width="100px">
                                                    <asp:Label ID="Label32" runat="server" CssClass="Label" Text="  Monto Docto."></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Mto_Dco" runat="server" CssClass="clsMandatorio" 
                                                        ReadOnly="false" Width="90px" MaxLength="12"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="Txt_Mto_Dco_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="999,999,999,999" MaskType="Number" 
                                                        TargetControlID="Txt_Mto_Dco" InputDirection="RightToLeft">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                             
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label31" runat="server" Text="Origen de Fondo" CssClass="Label"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:DropDownList ID="DP_OrigenFondo" runat="server" CssClass="clsMandatorio" Width="100%"
                                                        Enabled="true">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                
                                               
                                                
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label41" runat="server" CssClass="Label" Text="A la Orden"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButton ID="RB_Banco" runat="server" CssClass="Label" GroupName="Orden"
                                                        Enabled="true" Text="Banco" Checked="True" AutoPostBack="true" />
                                                    <asp:RadioButton ID="RB_Cliente" runat="server" CssClass="Label" GroupName="Orden"
                                                        Enabled="true" Text="Cliente" AutoPostBack="true"/>
                                                    <asp:RadioButton ID="RB_Tercero" runat="server" CssClass="Label" GroupName="Orden"
                                                        Enabled="true" Text="Tercero" AutoPostBack="true"/>
                                                </td>
                                                <td align="right">
                                                    
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="Txt_Orden" runat="server" CssClass="clsDisabled" Width="100%" ReadOnly="true" Text="Banco"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    
                                                </td>
                                               
                                               
                                               
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="right">
                            <asp:ImageButton ID="IB_AceptarCheque" runat="server" AlternateText="Aceptar" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';" />
                            <asp:ImageButton ID="IB_CancelarCheque" runat="server" AlternateText="Cencelar" ImageUrl="~/Imagenes/Botones/Boton_Cancelar_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Cancelar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Cancelar_in.gif';" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--****************************************************************************************************--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
</asp:Content>
