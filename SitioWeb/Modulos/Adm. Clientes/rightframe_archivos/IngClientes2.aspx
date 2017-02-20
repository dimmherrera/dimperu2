<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="IngClientes2.aspx.vb" Inherits="IngCliente" Title="Mantención de Clientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc5" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Bancos.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Validar() {
            var error = 0;

            if (error == 0) {
                if (confirm("Esta seguro que desea grabar?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }
    </script>
    

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel runat="server" ID="Updatepanel_mclientes">
        <ContentTemplate>
            <table cellspacing="1" class="Contenido" cellpadding="5" width="100%" border="0">
                
                    <tr>
                        <td style="height: 37px" valign="middle" width="100%" class="Cabecera">
                            <asp:Label ID="Label40" runat="server" CssClass="Titulos" __designer:wfdid="w1" Text="Administración de Proveedor"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="center" width="100%" height="550px" valign="top">
                            <br />
                            <table cellspacing="0" cellpadding="5" border="0" width="900">
                               
                                    <%--Antecedentes Personales--%>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="Panel7" runat="server" CssClass="Cabecera" __designer:wfdid="w2" Width="100%">
                                                <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Antecedentes Personales"
                                                    __designer:wfdid="w3"></asp:Label>
                                            </asp:Panel>
                                            <div style="width:100%" class="Contenido">
                                                <table id="Table2" cellspacing="1" cellpadding="1" border="0">
                                                  
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label1455" runat="server" __designer:wfdid="w8" CssClass="Label" 
                                                                    Text="Tipo Identificación Proveedor"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DP_TipoIdentificacion" runat="server" 
                                                                    __designer:wfdid="w9" AutoPostBack="True" CssClass="clsMandatorio" TabIndex="1"
                                                                    Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" width="100">
                                                                <asp:Label ID="Label3" runat="server" __designer:wfdid="w8" CssClass="Label" 
                                                                    Text="Tipo Proveedor"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_TipoCliente" runat="server" __designer:wfdid="w9" 
                                                                    CssClass="clsMandatorio" Width="300px" AutoPostBack="True" TabIndex="4">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:ImageButton ID="IB_Contacto" runat="server" 
                                                                    ImageUrl="~/Imagenes/btn_workspace/Contactos_out.gif" 
                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/Contactos_out.gif';" 
                                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/Contactos_in.gif';" TabIndex="6" />
                                                            </td>
                                                            <td align="left">
                                                                <asp:ImageButton ID="IB_Empresa" runat="server" 
                                                                    ImageUrl="~/Imagenes/btn_workspace/Empresas_out.gif" 
                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/Empresas_out.gif';" 
                                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/Empresas_in.gif';" TabIndex="7" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Lbl_NIT" runat="server" __designer:wfdid="w5" CssClass="Label" 
                                                                    Text="Nro. Ident. PROV."></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Rut" runat="server" __designer:wfdid="w6" 
                                                                    CssClass="clsMandatorio" Width="90px" AutoPostBack="True" TabIndex="1"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                                                    AcceptNegative="None" AutoComplete="False" ClearMaskOnLostFocus="True" 
                                                                    ClearTextOnInvalid="True" CultureAMPMPlaceholder="" 
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                    TargetControlID="Txt_Rut">
                                                                </cc1:MaskedEditExtender>

                                                                <asp:TextBox ID="Txt_Dig" runat="server" __designer:wfdid="w7" 
                                                                    AutoPostBack="True" CssClass="clsDisabled" ReadOnly="true" MaxLength="1" Width="16px"></asp:TextBox>
                                                            </td>
                                                            <td align="right" width="100">
                                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Nro. de Proveedor"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_nro_cli" runat="server" AutoPostBack="True" MaxLength="8"
                                                                    CssClass="clsMandatorio" Width="100px" TabIndex="5"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_nro_cli">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td align="right" rowspan="2" valign="top">
                                                                <asp:ImageButton ID="IB_Bancos" runat="server" 
                                                                    ImageUrl="~/Imagenes/btn_workspace/Bancos_out.gif" 
                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/Bancos_out.gif';" 
                                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/Bancos_in.gif';" TabIndex="8" />
                                                            </td>
                                                            <td align="left" rowspan="2" valign="top">
                                                                <asp:ImageButton ID="IB_Organigrama" runat="server" 
                                                                    ImageUrl="~/Imagenes/btn_workspace/Organigrama_out.gif" 
                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/Organigrama_out.gif';" 
                                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/Organigrama_in.gif';" 
                                                                    Width="51px" TabIndex="9" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label33" runat="server" CssClass="Label" Text="CORASU"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DP_Corasu" runat="server" AutoPostBack="true" 
                                                                    CssClass="clsMandatorio" Width="200px" TabIndex="2">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="right">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                              
                                                </table>
                                            </div>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" __designer:wfdid="w12"
                                                ValidChars="K,k" TargetControlID="Txt_Dig" FilterType="Custom, Numbers">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <%--Natural  -  Juridico--%>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="Panel8" runat="server" CssClass="Cabecera" __designer:wfdid="w15"
                                                Width="100%" BackImageUrl="~/Imagenes/patronSubtitulo.gif">
                                                <asp:Label ID="Titulo_MV" runat="server" CssClass="SubTitulos" Text="PERSONA NATURAL"
                                                    __designer:wfdid="w16"></asp:Label>
                                            </asp:Panel>
                                            <div style="width: 100%" class="Contenido">
                                                <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                                                    <asp:View ID="uno" runat="server">
                                                        <table id="TablaNatural" cellspacing="0" cellpadding="0">
                                                   
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label5" runat="server" CssClass="Label" __designer:wfdid="w19" Text="Nombre / Razón Social Proveedor"></asp:Label>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="Txt_Nom_Bre" runat="server" CssClass="clsMandatorio" __designer:wfdid="w20"
                                                                            Width="800px" MaxLength="150" TabIndex="10"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label6" runat="server" CssClass="Label" __designer:wfdid="w23" Text="1er apellido"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Ape_Pat" runat="server" CssClass="clsMandatorio" __designer:wfdid="w24"
                                                                            Width="350px" MaxLength="50" TabIndex="11"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label9" runat="server" CssClass="Label" __designer:wfdid="w25" Text="F. Nacimiento"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fec_Nac" runat="server" CssClass="clsTxt" __designer:wfdid="w26"
                                                                            Width="76px" MaxLength="10" TabIndex="13"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="Txt_Fec_Nac"
                                                                            MaskType="Date" Mask="99/99/9999">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="Txt_Fec_Nac_CalendarExtender" runat="server" Enabled="True"
                                                                            TargetControlID="Txt_Fec_Nac" CssClass="radcalendar" PopupPosition="BottomRight"
                                                                            FirstDayOfWeek="Monday" Format="dd-MM-yyyy">
                                                                        </cc1:CalendarExtender>
  
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label7" runat="server" CssClass="Label" __designer:wfdid="w28" Text="2do apellido"></asp:Label>
                                                                    </td>
                                                                    <td style="height: 28px">
                                                                        <asp:TextBox ID="Txt_Ape_Mat" runat="server" CssClass="clsMandatorio" __designer:wfdid="w29"
                                                                            Width="350px" MaxLength="50" TabIndex="12"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label8" runat="server" CssClass="Label" __designer:wfdid="w21" Text="Sexo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Dp_Sexo" runat="server" CssClass="clsMandatorio" __designer:wfdid="w22"
                                                                            Width="151px" TabIndex="14">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                        
                                                        </table>
                                                    </asp:View>
                                                    <asp:View ID="dos" runat="server">
                                                        <table id="TablaJuridico" cellspacing="0" cellpadding="0">
                                                   
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label11" runat="server" CssClass="Label" __designer:wfdid="w32" Text="Razón Social"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsMandatorio" __designer:wfdid="w33"
                                                                            Width="800px" MaxLength="150" TabIndex="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" __designer:wfdid="w34" Text="Fecha Const."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Fec_Con" runat="server" CssClass="clsTxt" Width="76px" TabIndex="15"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="Txt_Fec_Con"
                                                                            CssClass="radcalendar" PopupPosition="BottomRight" FirstDayOfWeek="Monday" Format="dd-MM-yyyy">
                                                                        </cc1:CalendarExtender>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="Txt_Fec_Con"
                                                                            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" UserDateFormat="DayMonthYear" />
                                                                    </td>
                                                                </tr>
                                                       
                                                        </table>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--Antecedentes Generales--%>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="PanelCabAntGral" runat="server" CssClass="Cabecera" Width="100%" class="Contenido">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                            
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label39" runat="server" CssClass="SubTitulos" __designer:wfdid="w39">Antecedentes Generales</asp:Label>
                                                            </td>
                                                        </tr>
                                         
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server" class="Contenido" Width="100%">
                                                <table id="TablaGenerales" cellspacing="0" cellpadding="0" border="0">
                                        
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="CIIU" __designer:wfdid="w45"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Giro" runat="server" __designer:wfdid="w46" CssClass="clsMandatorio"
                                                                    Width="250px" AutoPostBack="True" TabIndex="16">
                                                                </asp:DropDownList>
                                                                
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Act. Económica" __designer:wfdid="w57"></asp:Label>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:DropDownList ID="DP_ActEco" runat="server" __designer:wfdid="w58" CssClass="clsDisabled"
                                                                    Width="460px" Enabled="false" TabIndex="19">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Oficina" __designer:wfdid="w47"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Sucursal" runat="server" CssClass="clsMandatorio" __designer:wfdid="w48"
                                                                    Width="250px" AutoPostBack="True" TabIndex="17">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label10" runat="server" __designer:wfdid="w53" CssClass="Label" Text="Departamento"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Depto" runat="server" CssClass="clsTxt" __designer:wfdid="w54"
                                                                    Width="200px" AutoPostBack="true" TabIndex="20">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label19" runat="server" __designer:wfdid="w49" CssClass="Label" Text="Municipio"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Ciudad" runat="server" __designer:wfdid="w50" AutoPostBack="True"
                                                                    CssClass="clsTxt" Width="200px" TabIndex="21">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <!--
                                                            <td align="right">
                                                                <asp:Label ID="Label1456" runat="server" __designer:wfdid="w51" CssClass="Label"
                                                                    Text="Zona"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Zona" runat="server" __designer:wfdid="w44" 
                                                                    CssClass="clsDisabled" MaxLength="25" ReadOnly="True" Width="250px"></asp:TextBox>
                                                            </td>
                                                            -->
                                                            <td align="right">
                                                                <asp:Label ID="Label1457" runat="server" __designer:wfdid="w51" CssClass="Label"
                                                                    Text="Banca"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Banca" runat="server" __designer:wfdid="w44" 
                                                                    CssClass="clsDisabled" MaxLength="25" ReadOnly="True" Width="300px"></asp:TextBox>
                                                            </td>
                                                            <!--
                                                            <td align="right">
                                                                <asp:Label ID="Label1458" runat="server" __designer:wfdid="w51" CssClass="Label"
                                                                    Text="Territorial"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Terrotorial" runat="server" __designer:wfdid="w44" 
                                                                    CssClass="clsDisabled" MaxLength="25" ReadOnly="True" Width="200px"></asp:TextBox>
                                                            </td>
                                                            -->
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label15" runat="server" __designer:wfdid="w51" CssClass="Label" Text="Email"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Mai" runat="server" __designer:wfdid="w52" CssClass="clsTxt"
                                                                    MaxLength="50" Width="250px" TabIndex="18"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Localidad/Barrio" __designer:wfdid="w53"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Comuna" runat="server" CssClass="clsTxt" __designer:wfdid="w54"
                                                                    Width="200px" AutoPostBack="True" TabIndex="22">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label13" runat="server" __designer:wfdid="w43" CssClass="Label" Text="Direccion"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Dom_Par" runat="server" __designer:wfdid="w44" CssClass="clsTxt"
                                                                    MaxLength="25" ReadOnly="True" Width="200px" TabIndex="23"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                               
                                                            <td align="right">
                                                                <asp:Label ID="Label1454" runat="server" __designer:wfdid="w89" CssClass="Label"
                                                                    Text="Clasificación" Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Clacificacion" runat="server" __designer:wfdid="w74" CssClass="clsTxt"
                                                                    Width="250px" Visible="false">
                                                                </asp:DropDownList>
                                                            </td>
      
                                                            <td align="right">
                                                                <asp:Label ID="Label17" runat="server" __designer:wfdid="w59" CssClass="Label" Text="Segmento"></asp:Label>
                                                            </td>
                                      
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Segmento" runat="server" __designer:wfdid="w60" CssClass="clsTxt"
                                                                    Width="200px" TabIndex="24">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label16" runat="server" __designer:wfdid="w55" CssClass="Label" Text="Cod. Postal"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Cod_Pos" runat="server" CssClass="clsTxt" MaxLength="10" Visible="false" TabIndex="25"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <!--
                                                            <td align="right">
                                                                <asp:Label ID="Label36" runat="server" __designer:wfdid="w89" CssClass="Label"
                                                                    Text="Canal"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_Canal" runat="server" __designer:wfdid="w60" AutoPostBack="true"
                                                                    CssClass="clsMandatorio" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            -->
                                                            <!--
                                                            <td align="right">
                                                                <asp:Label ID="Label43" runat="server" __designer:wfdid="w89" CssClass="Label" Text="SubCanal"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_SubCanal" runat="server" __designer:wfdid="w60" CssClass="clsMandatorio"
                                                                    Width="200px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            -->
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                        
                                                </table>
                                            </asp:Panel>
                                            <%--<cc1:CollapsiblePanelExtender ID="cpeGeneral" runat="Server" TargetControlID="Panel1"
                                                CollapseControlID="PanelCabAntGral" SuppressPostBack="true" CollapsedImage="../../../Imagenes/Iconos/expand_blue.jpg"
                                                ExpandedImage="../../../Imagenes/Iconos/collapse_blue.jpg" ImageControlID="Image1"
                                                CollapsedText="(Ver Detalles...)" ExpandedText="(Esconder Detalles...)" TextLabelID="Label34"
                                                Collapsed="false" ExpandControlID="PanelCabAntGral">
                                            </cc1:CollapsiblePanelExtender>--%>
                                        </td>
                                    </tr>
                                    <%--Antecedentes Factoring--%>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel3" runat="server" CssClass="Cabecera" Width="100%" >
                                                <table style="cellspacing="0" cellpadding="0" border="0">

                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <asp:Label ID="Label41" runat="server" __designer:wfdid="w63" CssClass="SubTitulos">Antecedentes Factoring</asp:Label>
                                                            </td>
                                                            <td align="right">
                                                            </td>
                                                            <td align="center">
                                                            </td>
                                                        </tr>

                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server" Width="100%" class="Contenido">
                                                <table id="Table3" border="0" cellpadding="0" cellspacing="0" >
                                              
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label22" runat="server" __designer:wfdid="w67" CssClass="Label" Text="Grte/Eje. Factoring"></asp:Label>
                                                            </td>
                                                            <td style="width: 205px">
                                                                <asp:DropDownList ID="DP_Ejecutivo" runat="server" __designer:wfdid="w68" CssClass="clsMandatorio"
                                                                    Width="208px" TabIndex="26">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" style="width: 93px">
                                                                <asp:Label ID="Label32" runat="server" __designer:wfdid="w69" CssClass="Label" Text="Estado"></asp:Label>
                                                            </td>
                                                            <td style="width: 222px">
                                                                <asp:DropDownList ID="DP_Estado" runat="server" __designer:wfdid="w70" CssClass="clsMandatorio"
                                                                    Width="221px" TabIndex="30">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label27" runat="server" __designer:wfdid="w87" CssClass="Label" Text="F. Creación"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Fec_Cre" runat="server" __designer:wfdid="w88" CssClass="clsDisabled"
                                                                    ReadOnly="True" Width="76px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label23" runat="server" __designer:wfdid="w71" CssClass="Label" Text="Modo Ope."></asp:Label>
                                                            </td>
                                                            <td style="width: 205px">
                                                                <asp:DropDownList ID="DP_ModoOpe" runat="server" __designer:wfdid="w72" CssClass="clsMandatorio"
                                                                    Width="208px" TabIndex="27">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label28" runat="server" __designer:wfdid="w89" CssClass="Label" Text="Fecha Inicio Operación"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Fec_Ope" runat="server" __designer:wfdid="w90" CssClass="clsDisabled"
                                                                    ReadOnly="True" Width="76px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label31" runat="server" __designer:wfdid="w73" CssClass="Label" Text="Categoria Riesgo"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_CateRiesgo" runat="server" __designer:wfdid="w74" CssClass="clsMandatorio"
                                                                    Visible="false" TabIndex="33">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label24" runat="server" __designer:wfdid="w75" CssClass="Label" Text="Estado Cart."></asp:Label>
                                                            </td>
                                                            <td style="width: 205px">
                                                                <asp:DropDownList ID="DP_EstCartera" runat="server" __designer:wfdid="w76" CssClass="clsMandatorio"
                                                                    Width="208px" TabIndex="28">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" style="width: 93px">
                                                                <asp:Label ID="Label30" runat="server" __designer:wfdid="w77" CssClass="Label" Text="Forma Envió"></asp:Label>
                                                            </td>
                                                            <td style="width: 222px">
                                                                <asp:DropDownList ID="DP_FormaEnvio" runat="server" __designer:wfdid="w78" CssClass="clsTxt"
                                                                    Width="221px" TabIndex="31">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;</td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="Label25" runat="server" __designer:wfdid="w79" CssClass="Label" Text="Tipo Inf."></asp:Label>
                                                            </td>
                                                            <td style="width: 205px" valign="top">
                                                                <asp:DropDownList ID="DP_EstadoInf" runat="server" __designer:wfdid="w80" CssClass="clsTxt"
                                                                    Width="208px" TabIndex="29">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" style="width: 93px">
                                                                <asp:Label ID="Label26" runat="server" __designer:wfdid="w83" CssClass="Label" 
                                                                    Text="Bienes / Servicios" Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 222px" align="left">
                                                                <asp:TextBox ID="Txt_Bie_Ser" runat="server" __designer:wfdid="w84" 
                                                                    CssClass="clsTxt" Height="48px" MaxLength="250" TextMode="MultiLine" 
                                                                    Width="221px" Visible="false" TabIndex="32"></asp:TextBox>
                                                            </td>
                                                            <td align="right" colspan="2" valign="top">
                                                                <asp:CheckBox ID="CB_CobranzaAnt" runat="server" __designer:wfdid="w86" 
                                                                    CssClass="Label" Text="Proveedor con Cobranza Anticipada" TabIndex="34" />
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                &nbsp;</td>
                                                            <td style="width: 205px">
                                                                &nbsp;</td>
                                                            <td align="right" valign="middle">
                                                                &nbsp;</td>
                                                            <td align="left" valign="middle">
                                                                
                                                                &nbsp;</td>
                                                            <td colspan="2" valign="middle">
                                                                &nbsp;</td>
                                                            <td>
                                                            </td>
                                                        </tr>
              
                                                </table>
                                            </asp:Panel>
                                            
                                        </td>
                                    </tr>
                                    <%--Antecedentes Banco--%>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel5" runat="server" CssClass="Cabecera" Width="100%" class="Contenido">
                                                <table style="cellspacing="0" cellpadding="0" border="0">
                                       
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <asp:Label ID="Label42" runat="server" CssClass="SubTitulos" __designer:wfdid="w93">Antecedentes Con Banco</asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <%--<asp:Label ID="Label43" runat="server" CssClass="SubTitulos" __designer:wfdid="w94">(Ver Detalles...)</asp:Label>--%>
                                                            </td>
                                                            <td align="center">
                                                                <%--<asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Iconos/expand_blue.jpg"
                                                                    __designer:wfdid="w95"></asp:Image>--%>
                                                            </td>
                                                        </tr>
                                         
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel6" runat="server" Width="100%" class="Contenido">
                                                <table id="Table4" cellspacing="0" cellpadding="0" border="0" >
                                          
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label1452" runat="server" CssClass="Label" Text="Sucursal Banco" 
                                                                    __designer:wfdid="w97"></asp:Label>
                                                            </td>
                                                            <td align="right">

                                                                <%--<asp:TextBox ID="Txt_Suc_Bco" runat="server" CssClass="clsTxt" __designer:wfdid="w98"
                                                                    Width="400px" MaxLength="70"></asp:TextBox>--%>

                                                                <asp:DropDownList ID="DP_Suc_Bco" runat="server" CssClass="clsMandatorio" _designer:wfdid="w98"
                                                                    Width="400px" MaxLength="70" AutoPostBack="True" TabIndex="35">

                                                                </asp:DropDownList>
                                                                
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label37" runat="server" __designer:wfdid="w101" CssClass="Label" 
                                                                    Text="Anexo"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Anx_Bco" runat="server" __designer:wfdid="w102" 
                                                                    CssClass="clsTxt" MaxLength="5" TabIndex="38"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="Txt_Anx_Bco_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Numbers" 
                                                                    TargetControlID="Txt_Anx_Bco">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td align="right">
                                                                <asp:Label ID="Label35" runat="server" __designer:wfdid="w67" CssClass="Label" Text="Grte/Eje. Oficina" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DP_Gestor" runat="server" __designer:wfdid="w68" CssClass="clsMandatorio" AutoPostBack="true"
                                                                    Width="208px" Visible="false" TabIndex="36">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label1459" runat="server" __designer:wfdid="w101" 
                                                                    CssClass="Label" Text="Grte/Eje. Código" Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Cod_Ges" runat="server" __designer:wfdid="w102" 
                                                                    CssClass="clsDisabled" ReadOnly="true" MaxLength="10" Visible="false" TabIndex="39"></asp:TextBox>
                                                                
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label44" runat="server" __designer:wfdid="w67" CssClass="Label" Text="Grte/Eje. del Negocio" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DP_GestorNeg" runat="server" __designer:wfdid="w68" CssClass="clsMandatorio" AutoPostBack="true"
                                                                    Width="208px" Visible="false" TabIndex="37">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label45" runat="server" __designer:wfdid="w101" 
                                                                    CssClass="Label" Text="Mail Grte/Eje." Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Ema_Ges" runat="server" __designer:wfdid="w102" 
                                                                    CssClass="clsDisabled" ReadOnly="true" MaxLength="50" Width="200px" Visible="false" TabIndex="40"></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                               
                                                </table>
                                            </asp:Panel>
                                          
                                        </td>
                                    </tr>
                                    <%--Condiciones Financieras--%>
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="Panel_Condiciones" runat="server" CssClass="Cabecera" Width="100%" class="Contenido">
                                                <table style="cellspacing="0" cellpadding="0" border="0">
                                           
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" __designer:wfdid="w93">Condiciones Financieras</asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <%--<asp:Label ID="Label43" runat="server" CssClass="SubTitulos" __designer:wfdid="w94">(Ver Detalles...)</asp:Label>--%>
                                                            </td>
                                                            <td align="center">
                                                                <%--<asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Iconos/expand_blue.jpg"
                                                                    __designer:wfdid="w95"></asp:Image>--%>
                                                            </td>
                                                        </tr>
                                                 
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_" runat="server" Width="100%" class="Contenido">
                                                <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                
                                                        <tr>
                                                            <td style="width: 93px" align="right">
                                                                <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Tipo Tasa"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="DP_TipoTasa" runat="server" CssClass="clsTxt" 
                                                                    Width="80px" AutoPostBack="True" TabIndex="41">
                                                                <asp:ListItem Text="Fija" Value="F" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Variable DTF" Value="V" ></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 93px" align="right">
                                                                <asp:Label ID="Lbl_TipoTasa" runat="server" CssClass="Label" Text="Tasa Fija E.A."></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_Anx_Bco">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:TextBox ID="Txt_Spr_Col" runat="server" __designer:wfdid="w82" 
                                                                    CssClass="clsTxt" Width="40px" TabIndex="42"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Spr_Col_MaskedEditExtender" runat="server" 
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    InputDirection="RightToLeft" Mask="999,99" MaskType="Number" 
                                                                    TargetControlID="Txt_Spr_Col">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 93px" align="right">
                                                                <asp:Label ID="Label29" runat="server" __designer:wfdid="w81" CssClass="Label" 
                                                                    Text="% Spread Mora"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Txt_Spr_Mor" runat="server" __designer:wfdid="w82" 
                                                                    CssClass="clsTxt" Width="40px" TabIndex="43"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="Txt_Spr_Mor_MaskedEditExtender" runat="server" 
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                    InputDirection="RightToLeft" Mask="999,99" MaskType="Number" 
                                                                    TargetControlID="Txt_Spr_Mor">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 93px">
                                                                <asp:Label ID="Label34" runat="server" __designer:wfdid="w77" CssClass="Label" 
                                                                    Text="Base de Días"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="RB_BaseDias" runat="server" CssClass="Label" 
                                                                    RepeatDirection="Horizontal" TabIndex="44">
                                                                    <asp:ListItem Selected="True" Text="Comercial (360)" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="Real (365)" Value="R"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                             
                                                </table>
                                            </asp:Panel>
                                          
                                        </td>
                                    </tr>
                         
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="50">
                            <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                                ValidationGroup="Cliente" ToolTip="Guardar Datos del Proveedor" TabIndex="45" />
                            <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';"
                                ToolTip="Volver a Listado de Clientes" TabIndex="46" />
                        </td>
                    </tr>
               
            </table>
            <asp:HiddenField ID="SW" runat="server" />
            <asp:CheckBox ID="CB_Sinacofi" runat="server" __designer:wfdid="w85" 
                Checked="true" CssClass="Label" Text="No informar SINACOFI" Visible="false" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Empresa" />
            <asp:PostBackTrigger ControlID="IB_Contacto" />
            <asp:PostBackTrigger ControlID="IB_Bancos" />
            <asp:PostBackTrigger ControlID="IB_Organigrama" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    </asp:UpdatePanel>
    <asp:LinkButton ID="LK_Rut" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lb_mj" runat="server"></asp:LinkButton>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel_mclientes">
        <ProgressTemplate>
            <uc5:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
</asp:Content>
