<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="IngDeudor.aspx.vb" Inherits="Deudores_IngDeudor"
    Title="Ingreso y Modicación de Deudores" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<%--   <asp:UpdatePanel ID="UpdatePanelDeudores" runat="server">
    
        <ContentTemplate>
--%>        
            <table cellspacing="1" cellpadding="0" width="100%" class="Contenido" border="0">
                <tr>
                    <td style="height: 37px" valign="middle" class = "Cabecera">
                        <asp:Label ID="Label30" TabIndex="25" runat="server" CssClass="Titulos" Text="Administración de Pagadores" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="Contenido">
                        <div style="overflow: auto; height: 550px">
                            <table style="width: 850px; height: 54px" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label31" TabIndex="25" runat="server" CssClass="SubTitulos" Text="Pagador"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="left">
                                            <table style="width: 800px" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    
                                                    <asp:Label ID="Label94" TabIndex="27" runat="server" CssClass="Label"
                                                        Text="Tipo Identificación"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:DropDownList ID="DropTipoIdentificacion" TabIndex="1" runat="server" CssClass="clsMandatorio"
                                                        AutoPostBack="True" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    
                                                    <asp:Label ID="Label2" TabIndex="27" runat="server" CssClass="Label"
                                                        Text="Tipo Pagador"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="DropTipoDeudor" TabIndex="4" runat="server" CssClass="clsMandatorio"
                                                        AutoPostBack="True" Width="300px">
                                                    </asp:DropDownList>
                                                </td>
                                                </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label1" TabIndex="26" runat="server" CssClass="Label"
                                                        Text="Número Identificación Pagador"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txt_Rut_Deu" TabIndex="2" runat="server" CssClass="clsMandatorio"
                                                        Width="90px" AutoPostBack="True"></asp:TextBox>

                                                    <asp:TextBox ID="Txt_Dig_Deu" TabIndex="2" runat="server" CssClass="clsMandatorio"
                                                        Width="15px" MaxLength="1" AutoPostBack="true"></asp:TextBox>

                                                    <cc1:FilteredTextBoxExtender ID="Txt_Dig_Deu_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Deu" ValidChars="K,k">
                                                    </cc1:FilteredTextBoxExtender>

                                                    <cc1:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" ClearMaskOnLostFocus ="true"
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label50" runat="server" CssClass="Label" Text="Nro Cliente">
                                                    </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txt_Nro_Cli" runat="server" CssClass="clsMandatorio"  AutoPostBack="True" MaxLength="8" TabIndex="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label92" runat="server" CssClass="Label" Text="CORASU"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="DP_Corasu" runat="server" AutoPostBack="true" CssClass="clsMandatorio"
                                                        Width="200px" TabIndex="3">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    &nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table style="width: 850px" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Lbl_Nat_Jur" TabIndex="28" runat="server" CssClass="SubTitulos" Text="Natural"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="left">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" TabIndex="29" runat="server" Text="Nombre / Razón Social Pagador" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="Txt_Rso_Deu" TabIndex="6" runat="server" CssClass="clsMandatorio"
                                                                Width="610px" MaxLength="150"></asp:TextBox>
                                                           
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label4" TabIndex="30" runat="server" Width="90px" Text="Apellido Pat."
                                                                CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtApePat" TabIndex="7" runat="server" CssClass="clsMandatorio"
                                                                Width="229px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        <td align="right"">
                                                            <asp:Label ID="Label5" runat="server" Width="86px" Text="Apellido Mat" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtApeMat" TabIndex="8" runat="server" CssClass="clsMandatorio"
                                                                Width="230px" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <!--*******************************************Antecedentes Generales*************************************-->
                            <table id="tableAntGrales" border="0" cellpadding="0" cellspacing="0" width="850px">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" Width="100%">
                                    <table style="width: 100%" class="Cabecera" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label ID="Label32" runat="server" CssClass="SubTitulos">Antecedentes Generales</asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label35" runat="server" CssClass="SubTitulos">(Ver Detalles...)</asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Iconos/collapse_blue.jpg"
                                                        Style="height: 13px"></asp:Image>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" align="left">
                                <asp:Panel ID="PnlAntGen" runat="server" Width="100%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label91" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropSucursal" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                    TabIndex="9" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <%--<td align="right">
                                                <asp:Label ID="Label1456" runat="server" __designer:wfdid="w51" CssClass="Label"
                                                    Text="Zona"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Zona" runat="server" __designer:wfdid="w44" CssClass="clsDisabled"
                                                    MaxLength="25" ReadOnly="True" Width="300px"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1457" runat="server" __designer:wfdid="w51" CssClass="Label"
                                                    Text="Banca"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Banca" runat="server" __designer:wfdid="w44" CssClass="clsDisabled"
                                                    MaxLength="25" ReadOnly="True" Width="300px"></asp:TextBox>
                                            </td>
                                            <%--<td align="right">
                                                <asp:Label ID="Label1458" runat="server" __designer:wfdid="w51" CssClass="Label"
                                                    Text="Territorial"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Territorial" runat="server" __designer:wfdid="w44" CssClass="clsDisabled"
                                                    MaxLength="25" ReadOnly="True" Width="300px"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label51" runat="server" CssClass="Label" Text="Departamento">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDepto" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                    Width="300px" TabIndex="11">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Municipio"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropCiudadDeu" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                    TabIndex="10" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Localidad/Barrio"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropComunaDeu" runat="server" CssClass="clsTxt" TabIndex="12"
                                                    Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" Text="Dirección" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtDirDeudor" TabIndex="8" runat="server" CssClass="clsTxt" Width="300px"
                                                    MaxLength="32"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="CIIU"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropGiroDeu" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                    TabIndex="13" Width="300px">
                                                </asp:DropDownList>
                                                <%--<cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="DropGiroDeu"
                                                    PromptCssClass="LabelListSearch" QueryPattern="Contains" PromptPosition="Top" IsSorted="true">
                                                </cc1:ListSearchExtender>--%>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Act. Econom."></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropActEcoDeu" runat="server" CssClass="clsDisabled" Enabled="False"
                                                    TabIndex="13" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label8" runat="server" Text="Abr.Raz.Soc." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropAbrRazSoc" TabIndex="14" runat="server" CssClass="clsMandatorio"
                                                    Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Segmento" Width="61px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropSeg" runat="server" CssClass="clsTxt" TabIndex="16" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Estado Documento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropEstadoDeu" runat="server" CssClass="clsMandatorio" TabIndex="15"
                                                    Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Grte/Eje. Factoring"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_Eje_Fac" runat="server" CssClass="clsMandatorio" TabIndex="17"
                                                    Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                    <%--        <td align="right">
                                                <asp:Label ID="Label21" runat="server" __designer:wfdid="w67" CssClass="Label" Text="Grte/Eje. Oficina"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_Gestor" runat="server" __designer:wfdid="w68" CssClass="clsMandatorio"
                                                    AutoPostBack="true" Width="300px">
                                                </asp:DropDownList>
                                            </td>--%>
                           <%--                 <td align="right">
                                                <asp:Label ID="Label1459" runat="server" __designer:wfdid="w101" CssClass="Label"
                                                    Text="Grte/Eje. Código"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Cod_Ges" runat="server" __designer:wfdid="w102" CssClass="clsDisabled" Width="300px"
                                                    ReadOnly="true" MaxLength="10"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <%--<td align="right">
                                                <asp:Label ID="Label44" runat="server" __designer:wfdid="w67" CssClass="Label" Text="Grte/Eje. del Negocio"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DP_GestorNeg" runat="server" __designer:wfdid="w68" CssClass="clsMandatorio"
                                                    AutoPostBack="true" Width="300px">
                                                </asp:DropDownList>
                                            </td>--%>
                                            <td align="right">
                                                <asp:Label ID="Label45" runat="server" __designer:wfdid="w101" CssClass="Label" Text="Mail Grte/Eje."></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Ema_Ges" runat="server" __designer:wfdid="w102" CssClass="clsDisabled"
                                                    ReadOnly="true" MaxLength="50" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--<td align="right">
                                                <asp:Label ID="Label36" runat="server" __designer:wfdid="w89" CssClass="Label" Text="Canal"></asp:Label>
                                            </td>--%>
                                            <%--<td align="left">
                                                <asp:DropDownList ID="DP_Canal" runat="server" __designer:wfdid="w60" AutoPostBack="true"
                                                    CssClass="clsMandatorio" Width="300px">
                                                </asp:DropDownList>
                                            </td>--%>
                                            <%--<td align="right">
                                                <asp:Label ID="Label43" runat="server" __designer:wfdid="w89" CssClass="Label" Text="SubCanal"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DP_SubCanal" runat="server" __designer:wfdid="w60" CssClass="clsMandatorio"
                                                    Width="300px">
                                                </asp:DropDownList>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderAntGen" runat="server"
                                TargetControlID="PnlAntGen" ExpandControlID="Panel3" Collapsed="False" TextLabelID="Label35"
                                ExpandedText="(Esconder Detalles...)" CollapsedText="(Ver Detalles...)" ImageControlID="Image3"
                                ExpandedImage="~/Imagenes/Iconos/expand_blue.jpg" CollapsedImage="~/Imagenes/Iconos/collapse_blue.jpg"
                                SuppressPostBack="true" CollapseControlID="Panel3">
                            </cc1:CollapsiblePanelExtender>
                            <!--*******************************************Fin Antecedentes Generales*************************************-->
                            <br />
                            <table id="Tab_Linea_Finan" border="0" cellpadding="0" cellspacing="0" width="850px" runat="server" visible="False">
                             <tr>
                              <td>
                                <asp:Panel ID="Panel4" runat="server" Width="100%">
                                            <table style="width: 100%" class="Cabecera" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label14" runat="server" CssClass="SubTitulos">Linea de Financiamiento Global</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label15" runat="server" CssClass="SubTitulos">(Ver Detalles...)</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Iconos/collapse_blue.jpg">
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">&nbsp;</td>
                                                        <td align="right">&nbsp;</td>
                                                        <td align="center">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top">&nbsp;</td>
                                                        <td align="right">&nbsp;</td>
                                                        <td align="center">&nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                </asp:Panel>
                              </td> 
                             </tr>
                             <tr>
                              <td class="Contenido" align="left">
                                <asp:Panel ID="Panel5" runat="server" Width="850px" ScrollBars="Auto">
                                 <table>
                                  <tr>
                                    <td>
                                      <table width="100%">
                                          <tr>
                                              <td align="left">
                                                  <asp:Label ID="Label55" runat="server" CssClass="Label" Text="Moneda">
                                                   </asp:Label>
                                                  <asp:GridView ID="GV_MON_DEU" runat="server" AllowSorting="True" 
                                                      AutoGenerateColumns="False" CssClass="formatUltcell" EnableTheming="True">
                                                      <Columns>
                                                          <asp:BoundField DataField="descripcion" HeaderText="Moneda" 
                                                              ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                              <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                          </asp:BoundField>
                                                          <asp:BoundField DataField="monto" HeaderText="Cupo Aprobado" 
                                                              ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                                                              <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                          </asp:BoundField>
                                                          <asp:BoundField DataField="deu_mon_dis" HeaderText="Cupo Disponible" 
                                                              ItemStyle-HorizontalAlign="Right" ItemStyle-Width="110px">
                                                              <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                          </asp:BoundField>
                                                          <asp:TemplateField HeaderText="Cupo Utilizado" 
                                                              ItemStyle-HorizontalAlign="Right" ItemStyle-Width="110px">
                                                              <ItemTemplate>
                                                                  <asp:LinkButton ID="LB_Mto_Ocu" runat="server" OnClick="LB_Mto_Ocu_Click" 
                                                                      Text='<%# Eval("deu_mon_ocu")%>' ToolTip='<%# Eval("codigo")%>' />
                                                              </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Modalidad CR" ItemStyle-HorizontalAlign="Right" 
                                                              ItemStyle-Width="110px">
                                                              <ItemTemplate>
                                                                  <asp:LinkButton ID="LB_Mto_CR" runat="server" OnClick="LB_Mto_CR_Click" 
                                                                      Text='<%# Eval("conrecurso")%>' ToolTip='<%# Eval("codigo")%>' />
                                                              </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Modalidad SR" ItemStyle-HorizontalAlign="Right" 
                                                              ItemStyle-Width="110px">
                                                              <ItemTemplate>
                                                                  <asp:LinkButton ID="LB_Mto_SR" runat="server" OnClick="LB_Mto_SR_Click" 
                                                                      Text='<%# Eval("sinrecurso")%>' ToolTip='<%# Eval("codigo")%>' />
                                                              </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                          </asp:TemplateField>
                                                          <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo" 
                                                              ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                              <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                          </asp:BoundField>
                                                          <asp:BoundField DataField="Estado" HeaderText="Estado" 
                                                              ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                              <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                          </asp:BoundField>
                                                          <asp:BoundField DataField="fecha_vcto" DataFormatString="{0:dd/MM/yyyy}" 
                                                              HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Center" 
                                                              ItemStyle-Width="80px">
                                                              <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                          </asp:BoundField>
                                                         <%-- <asp:BoundField DataField="Observacion" HeaderText="Observación" 
                                                              ItemStyle-HorizontalAlign="left" ItemStyle-Width="150px">
                                                              <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                          </asp:BoundField>--%>
                                                      </Columns>
                                                      <HeaderStyle CssClass="cabeceraGrilla" />
                                                      <RowStyle CssClass="formatUltcell" />
                                                      <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                  </asp:GridView>
                                              </td>    
                                          </tr>
                                          <tr>
                                          <td>
                                            <table>
                                            <tr>
                                             <td>     
                                                  <asp:DropDownList ID="DP_MON" runat="server" CssClass="clsMandatorio" 
                                                      Width="200px" AutoPostBack="True">
                                                  </asp:DropDownList>
                                              </td>
                                              <td>
                                                  <asp:Label ID="Label93" runat="server" Text="Fecha Vcto." CssClass="Label"></asp:Label>
                                              </td>
                                              <td>
                                                  <asp:TextBox ID="Txt_Fec_Vto" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                      Width="60px"></asp:TextBox>
                                                  <cc1:CalendarExtender ID="Txt_Fec_Vto_CalendarExtender" runat="server" CssClass="radcalendar"
                                                      Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Vto">
                                                  </cc1:CalendarExtender>
                                                  <cc1:MaskedEditExtender ID="Txt_Fec_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                      CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                      CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                      Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Vto">
                                                  </cc1:MaskedEditExtender>
                                              </td>
                                              <td>
                                                <asp:Label ID="Label56" runat="server" Text="Monto Aprobado" CssClass="Label"></asp:Label>
                                              </td>
                                              <td>                                                
                                                <asp:TextBox ID="Txt_Mto_Apr" runat="server" CssClass="clsDisabled" Width="150px" ReadOnly="true">
                                                </asp:TextBox>
                                                  <cc1:MaskedEditExtender ID="MaskedEditExtender_Txt_Mto_Apr" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Mto_Apr">
                                                  </cc1:MaskedEditExtender>
                                              </td>
                                              <td>
                                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Observación"></asp:Label>
                                              </td>
                                              <td>
                                                 <asp:TextBox ID="Txt_Obs_Deu" runat="server" CssClass="clsDisabled" 
                                                      Width="200px" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                              </td>
                                            </tr>
                                            </table>
                                          </td>
                                          </tr>
                                       </table>
                                    </td>
                                  </tr>
                                   <tr>
                                    <td>
                                    <asp:Panel ID="Panel_grid" runat="server" Height="100px" ScrollBars="Auto">
                                    </asp:Panel>
                                    </td>
                                    </tr>
                                    <tr>
                                      <td align="left">
                                        <asp:Button ID="btn_nuevo" runat="server" Text="Nuevo" Width="70px" 
                                              CssClass="button" />
                                        <asp:Button ID="Btn_Con" runat="server" Text="Guardar" Width="70px" 
                                              CssClass="button" />
                                        <asp:Button ID="Btn_Limp" runat="server" Text="Limpiar" Width="70px" 
                                              CssClass="button" />
                                      </td>
                                    </tr>
                                  </table>
                                </asp:Panel>
                              </td>
                             </tr>
                            </table>
                           
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderPanel5" runat="server"
                                TargetControlID="Panel5" ExpandControlID="Panel4" Collapsed="true" TextLabelID="Label15"
                                ExpandedText="(Esconder Detalles...)" CollapsedText="(Ver Detalles...)" ImageControlID="Image4"
                                ExpandedImage="~/Imagenes/Iconos/expand_blue.jpg" CollapsedImage="~/Imagenes/Iconos/collapse_blue.jpg"
                                SuppressPostBack="true" CollapseControlID="Panel4"><%--PRUEBA FY--%> 
                            </cc1:CollapsiblePanelExtender>
                            <!--*************************Clientes Asociados al Deudor*********************************************************************-->
                            <br />
                            <table id="table1" border="0" cellpadding="0" cellspacing="0" width="850px">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" CssClass="cabecera" Width="850px">
                                            <table style="width: 100%" class="Cabecera" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label33" runat="server" CssClass="SubTitulos">Clientes Asociados al Pagador</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label34" runat="server" CssClass="SubTitulos">(Ver Detalles...)</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Iconos/collapse_blue.jpg">
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="left">
                                        <asp:Panel ID="PnlCliAsoDue" runat="server" Width="850px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table scellspacing="0" cellpadding="0" border="0">
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:ImageButton ID="IB_AgrCli" runat="server" Enabled="false" ImageUrl="~/Imagenes/btn_workspace/Clientes_out.gif"
                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/Clientes_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/Clientes_out.gif';" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <asp:RadioButton ID="RBtn_Todos" runat="server" AutoPostBack="True" CssClass="Label"
                                                                        GroupName="GrupoCliente" Text="Todos" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <asp:RadioButton ID="RBtn_Cli" runat="server" AutoPostBack="True" CssClass="Label"
                                                                        GroupName="GrupoCliente" Text="Solo Activos" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%--<div align="center" style="overflow: auto; width: 600px; height: 110px">--%>
                                                        <asp:Panel ID="Panel_GrClientes" runat="server" Width="600px" Height="110px">
                                                            <asp:GridView ID="GrClientes" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                CssClass="formatUltcell" EnableTheming="True" Font-Underline="False" PageSize="3"
                                                                Width="571px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="cli_idc" HeaderText="NIT Cliente" ItemStyle-HorizontalAlign="Right"
                                                                        ItemStyle-Width="100" />
                                                                    <asp:BoundField DataField="cli_rso" HeaderText="Razón Social Cliente" ItemStyle-HorizontalAlign="left"
                                                                        HeaderStyle-HorizontalAlign="Left" />
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
                                                        <table>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" AlternateText="Anterior"/>
                                                                    <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" AlternateText="Siguiente" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>                               
                                            
                                               <%-- </div>--%>
                                         </asp:Panel>
                                    </td>
                                </tr>                               
                            </table>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderCliAsoDeu" runat="server"
                                TargetControlID="PnlCliAsoDue" ExpandControlID="Panel1" Collapsed="True" TextLabelID="Label34"
                                ExpandedText="(Esconder Detalles...)" CollapsedText="(Ver Detalles...)" ImageControlID="Image2"
                                ExpandedImage="~/Imagenes/Iconos/expand_blue.jpg" CollapsedImage="~/Imagenes/Iconos/collapse_blue.jpg"
                                SuppressPostBack="true" CollapseControlID="Panel1">
                            </cc1:CollapsiblePanelExtender>
                            <!--*************************Fin Clientes Asociados al Deudor*********************************************************************-->
                            <!--*************************Antecedentes de Cobranza*********************************************************************-->
                            <br />
                            <table id="table2" border="0" cellpadding="0" cellspacing="0" width="850px">
                                <%--<tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" CssClass="cabecera" Width="850px">
                                            <table class="Cabecera" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                 <tbody>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label21" runat="server" CssClass="SubTitulos">Antecedentes de cobranza</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label22" runat="server" CssClass="SubTitulos">(Ver Detalles...)</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Imagenes/Iconos/collapse_blue.jpg">
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" CssClass="cabecera" Width="850px">
                                            <table style="width: 100%" class="Cabecera" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label21" runat="server" CssClass="SubTitulos">Antecedentes de cobranza</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label22" runat="server" CssClass="SubTitulos">(Ver Detalles...)</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Iconos/collapse_blue.jpg">
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="left">
                                    <asp:Panel ID="PnlTabContRecYCob" runat="server" Width="850px">
                                             <table border="0" cellpadding="0" cellspacing="0" id="TblAntCobranza">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label90" runat="server" CssClass="Label" Text="Cob. Telefónico"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DP_Ejecutivo" runat="server" Width="300px" CssClass="clsTxt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td>
                                                                     <asp:TextBox ID="TxtVtasDeu" runat="server" CssClass="clsTxt" MaxLength="6" 
                                                                        Width="0px" Visible="False"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="TxtVtasDeu_MaskedEditExtender" runat="server" 
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                        InputDirection="RightToLeft" Mask="999,99" MaskType="Number" 
                                                                        TargetControlID="TxtVtasDeu">
                                                                    </cc1:MaskedEditExtender></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label88" runat="server" CssClass="Label" Text="Obs. Gestión"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Obs_Gsn" runat="server" CssClass="clsTxt" MaxLength="50" Width="295px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label89" runat="server" CssClass="Label" Text="Fecha Operación"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_Fec_Obs" runat="server" CssClass="clsDisabled" 
                                                                        ReadOnly="True" Width="70px">__/__/____</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label19" runat="server" Text="Contactos por Defecto" CssClass="Label"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txt_ConDef" runat="server" CssClass="clsDisabled" Width="295px"
                                                                        ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:ImageButton ID="IB_Contactos" runat="server" ImageUrl="~/Imagenes/btn_workspace/Contactos_out.gif"
                                                                         onmouseover="this.src='../../../Imagenes/btn_workspace/Contactos_in.gif';"
                                                                         onmouseout="this.src='../../../Imagenes/btn_workspace/Contactos_out.gif';" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label86" runat="server" Text="Atributo Cartas" CssClass="Label"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="RB_Carta_Si" runat="server" Text="Si" GroupName="GrupoCarta"
                                                                        CssClass="Label" Checked="True" />
                                                                    <asp:RadioButton ID="RB_Carta_No" runat="server" GroupName="GrupoCarta" CssClass="Label"
                                                                        Text="No" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label87" runat="server" CssClass="Label" Text="Notificación"></asp:Label>
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:RadioButton ID="RB_Not_Si" runat="server" Text="Si" GroupName="GrupoNotificacion"
                                                                        CssClass="Label" Checked="True" />
                                                                    <asp:RadioButton ID="RB_Not_No" runat="server" Text="No" GroupName="GrupoNotificacion"
                                                                        CssClass="Label" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    
                                                                </td>
                                                                <td>
                                                                   
                                                                    <asp:TextBox ID="Txt_ATR_CAR" runat="server" CssClass="clsTxt" 
                                                                        Width="200px" MaxLength="30" ></asp:TextBox>
                                                                </td>
                                                                <td style="height: 22px">
                                                                    
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                               <td align="right">
                                                                   <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Rad. Fact. Original" ToolTip="Radicar Facturas"></asp:Label>
                                                               </td>
                                                               <td>
                                                                  <asp:RadioButton ID="RB_Rad_Si" runat="server" Text="Si" GroupName="GrupoRadicar"
                                                                        CssClass="Label" Checked="True" />
                                                                    <asp:RadioButton ID="RB_Rad_No" runat="server" Text="No" GroupName="GrupoRadicar"
                                                                        CssClass="Label" />
                                                               </td>
                                                               <td style="height: 22px">
                                                                   <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Cant. Dias a Rad." ToolTip="Cantidad de dias a radicar factura"></asp:Label>   
                                                               </td>
                                                               <td>
                                                                  <asp:TextBox ID="txt_dias_rad" runat="server" CssClass="clsTxt" Width="50px" MaxLength="3"></asp:TextBox>
                                                                   <cc1:FilteredTextBoxExtender ID="FTBE_dias_rad" runat="server" FilterType="Numbers" TargetControlID="txt_dias_rad">
                                                                   </cc1:FilteredTextBoxExtender>
                                                               </td> 
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderAntCob" runat="server"
                                TargetControlID="PnlTabContRecYCob" ExpandControlID="Panel2" Collapsed="False"
                                TextLabelID="Label84" ExpandedText="(Esconder Detalles...)" CollapsedText="(Ver Detalles...)"
                                ImageControlID="Image1" ExpandedImage="~/Imagenes/Iconos/expand_blue.jpg" CollapsedImage="~/Imagenes/Iconos/collapse_blue.jpg"
                                SuppressPostBack="true" CollapseControlID="Panel2">
                            </cc1:CollapsiblePanelExtender>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 70px" valign="middle">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Sw" runat="server" __designer:wfdid="w38" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" OnClick="IB_Guardar_Click"
                                        ValidationGroup="Vacios" ToolTip="Guardar" TabIndex="18"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Pago" onmouseover="this.src='../../../Imagenes/Botones/Boton_Dias_Pago_In.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Dias_Pago_Out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_Dias_Pago_Out.gif" 
                                        CausesValidation="False" ToolTip="Calendario de Pago" TabIndex="19">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_out.gif';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif" 
                                        CausesValidation="False" ToolTip="Volver" TabIndex="20">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:LinkButton ID="AgregaCli" TabIndex="34" runat="server"></asp:LinkButton>
            
            <asp:HiddenField ID="TxtNro" runat="server" />
            <asp:HiddenField ID="TxtRut" runat="server" />

            <asp:HiddenField ID="TxtNroCli" runat="server" />
            <asp:HiddenField ID="HF_ACCION_LIN" runat="server" />
            <asp:HiddenField  ID="HF_Cont" runat="server" />
            
    <asp:Panel ID="Panel_CupoPagador" runat="server" Width="900px" Height="700px" Style="display: none;
        text-align: center" BackColor="White">
        <table>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" ExportContentDisposition="AlwaysAttachment"
                        Width="850px" Height="580px" Font-Names="Arial">
                    </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Btn_Volver" runat="server" Text="Volver" CssClass="button" />
                </td>
            </tr>
        </table>
    </asp:Panel>
            
     <%-- <div align="center" style="overflow: auto; width: 600px; height: 110px">--%>
    <asp:HiddenField ID="Txt_Rut_Cli" runat="server" />
    <asp:HiddenField ID="Txt_Dig_Cli" runat="server" />
    <asp:HiddenField ID="Txt_Raz_Soc" runat="server" />
    <asp:TextBox ID="TxtGiradoA" runat="server" CssClass="clsTxt" MaxLength="40" Width="300px" Visible="false"></asp:TextBox>
    <asp:LinkButton ID="LnkBtnTraeDDR" TabIndex="35" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
    <asp:LinkButton ID="LB_CargaCupo" runat="server"></asp:LinkButton>
    
    <cc1:ModalPopupExtender ID="Modal_CupoPagador" runat="server" TargetControlID="LB_CargaCupo"
                EnableViewState="False" PopupControlID="Panel_CupoPagador" BackgroundCssClass="modalBackground" CancelControlID="Btn_Volver">
    </cc1:ModalPopupExtender> 


    <%--</table>
    


    </table>
    </div>
    </table>--%>
</asp:Content>
