<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Informes.aspx.vb" Inherits="Dnc" title="Informe de Cuentas" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

   <script language="javascript">

function DoScrollCXC()
 {
    var _gridView = document.getElementById("GridViewDiv");
    var _header = document.getElementById("HeaderDiv");
     _header.scrollLeft = _gridView.scrollLeft;
 }

function DoScrollCXP()
 {
    var _gridView = document.getElementById("GridViewDivCXP");
    var _header = document.getElementById("HeaderDivCXP");
     _header.scrollLeft = _gridView.scrollLeft;
 }
 
 
function DoScrollNCE()
 {
    var _gridView = document.getElementById("GridViewDivDNC");
    var _header = document.getElementById("HeaderDivDNC");
     _header.scrollLeft = _gridView.scrollLeft;
 }
    </script>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc7:cargando id="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    
        <ContentTemplate>
            <table border="0" cellpadding="0"  class="Contenido" cellspacing="1" width="100%">
                <tr>
                    <td class = "Cabecera" style="height: 34px">
                        <%--<asp:Label ID="Label1" runat="server" Text="Cuentas/Documentos no Cedidos" CssClass="Titulos"></asp:Label>--%>
                        <asp:Label ID="Label1" runat="server" Text="Informe - Cuentas" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <td class="Contenido" align="center" width="100%" height="580px" valign="top" >
                    
                        <%--******Tabla contenido********--%>
                        <table border="0" cellpadding="0"  cellspacing="0" width="100%">
                        
                            <tr>
                                <td  align="center">
                                    <%--*********criterio cliente*******--%>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 645px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label2" runat="server" Text="Criterio" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="Cbx_Cli" runat="server" Text="Cliente" CssClass="Label" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Rut_Cli" runat="server" Width="90px" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                            </cc1:MaskedEditExtender>
                                                            <asp:TextBox ID="Txt_Dig_Cli" runat="server" Width="20px" ReadOnly="true" 
                                                                CssClass="clsDisabled" MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="IB_AyudaCli" runat="server"
                                                                   ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" Enabled="false"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Raz_Soc" runat="server" Width="405px" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                            <asp:LinkButton ID="LinkCli" runat="server"></asp:LinkButton>
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
                                    <br />
                                    <table id="Contenedora de 3 tablas" border="0" cellpadding="0" cellspacing="0" width="750">
                                        <tr>
                                            <td valign="top">
                                                <%--*******Tabla fecha********--%>
                                                <table id="tabla fecha" border="0" cellpadding="0" cellspacing="0" width="300">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha Creación de Cta./Dnc" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" align="center">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Fdsd" runat="server" Width="90px" MaxLength="10" EnableTheming="True" CssClass="clsTxt"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_Fdsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fdsd">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_Fdsd_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_Fdsd">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Fhst" runat="server" Width="90px" MaxLength="10" CssClass="clsTxt"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_Fhst_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fhst">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_Fhst_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_Fhst">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <%--*********tabla moneda*******--%>
                                                <table id="tabla Moneda" border="0" cellpadding="0" cellspacing="0" width="210">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label6" runat="server" Text="Tipo Moneda" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" align="center">
                                                            <table id="tabla moneda" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="Rb_MonTodas" runat="server" Text="Todas" CssClass="Label" AutoPostBack="True"
                                                                            Checked="True" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_Mon" runat="server" AutoPostBack="True" Width="130px"
                                                                            CssClass="clsTxt">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <%--********tabla estado********--%>
                                                <table id="tabla estado" border="0" cellpadding="0" cellspacing="0" width="210">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label7" runat="server" Text="Estado Ctas." CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" align="center">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="rb_est" runat="server" Text="Todas" CssClass="Label" AutoPostBack="True"
                                                                            Checked="True" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_Est" runat="server" AutoPostBack="True" Width="130px"
                                                                            CssClass="clsTxt">
                                                                        </asp:DropDownList>
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
                            
                            <%--********grilla*****--%>
                            
                            <tr>
                                <td align ="center">
                                    <table id="Grilla">
                                        <tr>
                                            <td>
                                                <cc1:TabContainer ID="TabContainer1" runat="server" Width="1000px" ActiveTabIndex="0"
                                                    Height="378px">
                                                    <cc1:TabPanel ID="Panel_CXC" runat="server" HeaderText="CXC">
                                                        <HeaderTemplate>
                                                            CXC</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel_GrCXC" runat="server" ScrollBars="Horizontal" Width="960px"
                                                                Height="370px">
                                                                <asp:GridView ID="Gr_CXC" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    Width="1620px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="cli_idc" HeaderText="Nit Cliente">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="RSocial" HeaderText="Razón Social">
                                                                            <ItemStyle HorizontalAlign="Left" Width="240px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DesEje" HeaderText="Ejecutivo">
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Contrato" HeaderText="NºContrato">
                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="id_cxc" HeaderText="id_cxc">
                                                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxc_fec" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TipoCuenta" HeaderText="TipoCuenta">
                                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxc_des" HeaderText="Descripción">
                                                                            <ItemStyle HorizontalAlign="Left" Width="280px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SGMoneda" HeaderText="Moneda">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxc_mto" HeaderText="Monto">
                                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxc_ful_pgo" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Estado" HeaderText=" Estado">
                                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla"  />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="Tab_CXP" runat="server" HeaderText="CXP">
                                                        <HeaderTemplate>
                                                            CXP</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel_Gr_CXP" runat="server" ScrollBars="Horizontal" Width="960px"
                                                                Height="370px">
                                                                <asp:GridView ID="Gr_CXP" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    ShowHeader="True" Width="1420px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="cli_idc" HeaderText="Nit Cliente">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="RSocial" HeaderText="Razón Social">
                                                                            <ItemStyle HorizontalAlign="left" Width="240px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DesEje" HeaderText="Ejecutivo">
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Contrato" HeaderText="NºContrato">
                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="id_cxp" HeaderText="id_cxp">
                                                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxp_fec" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TipoCuenta" HeaderText="TipoCuenta">
                                                                            <ItemStyle HorizontalAlign="left" Width="200px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxp_des" HeaderText="Descripción">
                                                                            <ItemStyle HorizontalAlign="Left" Width="280px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SGMoneda" HeaderText="Moneda">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="cxp_mto" HeaderText="Monto">
                                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Estado" HeaderText=" Estado">
                                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla"  />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="Tab_DNC" runat="server" HeaderText="DNC">
                                                        <HeaderTemplate>
                                                            DNC</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="960px" Height="370px">
                                                                <asp:GridView ID="Gr_dnc" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    ShowHeader="True" Width="1330px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="RutCli" HeaderText="Nit Cliente">
                                                                            <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="RSocialCli" HeaderText="Razón Social">
                                                                            <ItemStyle HorizontalAlign="left" Width="240px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DesEje" HeaderText="Eje">
                                                                            <ItemStyle Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="RutDeudor" HeaderText="Nit Pagador">
                                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="RSocialDeu" HeaderText="Razón Social Pagador">
                                                                            <ItemStyle HorizontalAlign="Left" Width="240px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Contrato" HeaderText="NºContrato">
                                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SGMoneda" HeaderText="Moneda">
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="nce_mto" HeaderText="Monto Dto.">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="nce_fec_ing" HeaderText="Fecha ingreso" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="nce_fec_vcto" HeaderText="Fecha Vto." DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="nce_fec_pft" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="formatUltcell" />
                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla"  />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                </cc1:TabContainer>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                            <td align="center">
                            <%-- <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                                              <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />--%>
                           
                           
                                <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"/>
                           
                           
                                <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"/>
                                                                
                                                                
                                                   
                            </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                     <%--   <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Fecha Erronea"
                            ControlToValidate="txt_Fdsd" Display="None" MaximumValue="31/12/2999" MinimumValue="01/01/1900"></asp:RangeValidator>
                        <cc1:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server"
                            Enabled="True" TargetControlID="RangeValidator1">
                        </cc1:ValidatorCalloutExtender>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Fecha Erronea"
                            ControlToValidate="txt_Fhst" Display="None" MaximumValue="31/12/2999" MinimumValue="01/01/1900"></asp:RangeValidator>
                        <cc1:ValidatorCalloutExtender ID="RangeValidator2_ValidatorCalloutExtender" runat="server"
                            Enabled="True" TargetControlID="RangeValidator2">
                        </cc1:ValidatorCalloutExtender>--%>
                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                            OnClick="IB_Buscar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" AlternateText="Buscar" />
                        <asp:ImageButton ID="IB_Imprimir" runat="server" OnClick="IB_Imprimir_CLick" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';"
                            AlternateText="Imprimir Informe" Enabled="False" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            OnClick="IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" AlternateText="Limpiar" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
           <%-- <asp:PostBackTrigger ControlID="IB_Prev" />
            <asp:PostBackTrigger ControlID="IB_Next" />
           --%>
        </Triggers>
        
    </asp:UpdatePanel>
    
</asp:Content>
