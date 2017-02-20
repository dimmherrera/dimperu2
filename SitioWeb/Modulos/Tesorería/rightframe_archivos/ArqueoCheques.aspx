<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="ArqueoCheques.aspx.vb" Inherits="ArqueoCheques" Title="Listado de Cheques" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        
        <script language="javascript" src="../FuncionesPrivadasJS/Colilla.js"></script>
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
            <%--******Tabla General******--%>
            <table border="0" cellpadding="1" cellspacing="0" width="100%" class="Contenido" style="text-align:-moz-center">
                <tr>
                    <td class = "Cabecera" style="height: 31" align="center" valign="middle">
                        <asp:Label ID="Label1" runat="server" Text="Tesorería-Listado de Cheques" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 1200px; height: 580px;padding: 5px" class="Contenido" valign="top" align="center">
                        <%--*********Tabla Contenedora*******--%>
                        <table style="width: 100%" border=0 cellpadding=0 cellspacing=0>
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label2" runat="server" Text="Criterio de Busqueda" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" style="padding: 5px" align="center">
                                    <table border="0" cellpadding="2" cellspacing="5">
                                        <tr>
                                            <td>
                                                <%--*********tabla Fecha********--%>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" style="height: 30px">
                                                            <asp:Label ID="Label4" runat="server" Text="Fecha de Vencimiento" CssClass="SubTitulos"
                                                                Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text="Fecha Desde" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_FDesde" runat="server" CssClass="clsTxt" MaxLength="10" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_FDesde_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FDesde">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_FDesde_CalendarExtender" runat="server" Enabled="True"
                                                                            FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_FDesde" CssClass="radcalendar">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text="Fecha Hasta" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFHasta" runat="server" CssClass="clsTxt" MaxLength="10" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txtFHasta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFHasta">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txtFHasta_CalendarExtender" runat="server" Enabled="True"
                                                                            FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txtFHasta" CssClass="radcalendar">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <%--******Tabla Cliente********--%>
                                                <table border="0" cellpadding="0" cellspacing="0" style="position: static;">
                                                    <tr>
                                                        <td class="Cabecera" style="height: 30px">
                                                            <asp:Label ID="Label14" runat="server" Text="Datos Cliente" CssClass="SubTitulos" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="85px">
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsTxt" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" Width="20px" CssClass="clsTxt" AutoPostBack="True"
                                                                            MaxLength="1"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <%--  <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes"
                                                             ImageUrl="../../../Imagenes/Iconos/155.ICO" Height="20px" />--%>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Height="20px" AlternateText="Ayuda Clientes" />
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
                                            <tr>
                                                <td>
                                                    <%--*********Tabla Custodia******--%>
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        width="100%">
                                                        <tr>
                                                            <td class="Cabecera" style="height: 30px">
                                                                <asp:Label ID="Label8" runat="server" Text="Custodia" CssClass="SubTitulos" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="Dp_Custodia" runat="server" CssClass="clsTxt" Width="200">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--*****************Tabla Estado*****************************--%>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="Cabecera">
                                                                            <asp:Label ID="Label16" runat="server" Text="Estado" CssClass="SubTitulos"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="Contenido">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="Drop_Estado" runat="server" CssClass="clsTxt" Width="280px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="Cabecera">
                                                                            <asp:Label ID="Label17" runat="server" Text="Tipo de Cheque" CssClass="SubTitulos"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td class="Contenido">
                                                                    <table>
                                                                    <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="RB_TipChr" runat="server" Height="24px" 
                                                                            RepeatDirection="Horizontal" CssClass="Label">
                                                                            <asp:ListItem Value="3" Selected="True">Todos </asp:ListItem>
                                                                            <asp:ListItem Value="2">Respaldo</asp:ListItem>
                                                                            <asp:ListItem Value="1">Flujo</asp:ListItem>
                                                                        </asp:RadioButtonList>
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
                                        </tr>
                                    </table>
                                  
                                </td>
                            </tr>
                         </table>
                        <%--******tabla Grilla *******--%>
                   <br />
                      
                      
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label15" runat="server" Text="Resultado de la Búsqueda" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <%--<div id="HeaderDiv" style="overflow: hidden; width: 995px">--%>
                                        <%--********Cabecera Grilla********--%>
                                        <%--<table width="1200px" class="cabeceraGrilla">
                                            <tr>
                                                <td width="100px">
                                                    <asp:Label ID="Label18" runat="server" Text="Tipo Chq." 
                                                        CssClass="LabelCabeceraGrilla"></asp:Label>
                                                </td>
                                                <td width="100px">
                                                    <asp:Label ID="Label19" runat="server" Text="Estado" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                </td>
                                                <td Width="90px">
                                                    <asp:Label ID="Label10" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Cheque"></asp:Label>
                                                </td>
                                                <td width="180px">
                                                    <asp:Label ID="Label12" runat="server" CssClass="LabelCabeceraGrilla" Text="A Nombre De"></asp:Label>
                                                </td>
                                                <td width="180px">
                                                    <asp:Label ID="Label6" runat="server" CssClass="LabelCabeceraGrilla" Text="Banco"></asp:Label>
                                                </td>
                                                <td width="140px">
                                                    <asp:Label ID="Label7" runat="server" CssClass="LabelCabeceraGrilla" Text="Plaza"></asp:Label>
                                                </td>
                                                <td width="120px">
                                                    <asp:Label ID="Label9" runat="server" CssClass="LabelCabeceraGrilla" Text="Cta. Cte."></asp:Label>
                                                </td>
                                                <td width="100px">
                                                    <asp:Label ID="Label13" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha Vcto."></asp:Label>
                                                </td>
                                                <td width="100px">
                                                    <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Monto"></asp:Label>
                                                </td>
                                                 <td width="100px">
                                                    <asp:Label ID="Label20" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    <%--</div>--%>
                                    <%--<div id="GridViewDiv" style="overflow: scroll: ; width: 995px; height: 350px" onscroll="DoScroll()">--%>
                                    <asp:Panel ID="Panel_GR_Busqueda" runat="server" width="1200px" height="350px" ScrollBars="Horizontal">
                                    
                                        <asp:GridView ID="GR_Busqueda" runat="server" AutoGenerateColumns="False" Width="1200px"
                                            CssClass="formatUltcell" ShowHeader="true">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tipo Chq." DataField="TipoCheque" >
                                                <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                
                                                 <asp:BoundField HeaderText="Estado" DataField="Estado">
                                                <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField HeaderText="N de cheque" DataField="chr_num">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="A nombre de" DataField="Nombre">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="bco_des" HeaderText="Banco">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pal_des" HeaderText="Plaza">
                                                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cta_cte" HeaderText="Cta. Cte.">
                                                    <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="chr_fev_rea" HeaderText="Fecha Vcto." DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Monto" DataField="Monto_cheque">
                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                 <asp:BoundField HeaderText="Moneda" DataField="Moneda">
                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                        </asp:GridView>
                                        </asp:Panel>
                                    <%--</div>--%>
                                </td>
                            </tr>
                            <tr>
                            <td align="center">
                                <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" AlternateText="Anterior" />
                                <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" AlternateText="Siguiente" />
                            </td>
                            </tr>
                        </table>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkB_Gv" runat="server">LinkButton</asp:LinkButton>
                        <%--<asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="txt_FDesde" ErrorMessage="Fecha Erronea " 
                            MaximumValue="31/12/2999" MinimumValue="01/01/1900" Display="None"></asp:RangeValidator>--%>
                        <%--<asp:RangeValidator ID="RangeValidator2" runat="server" 
                            ControlToValidate="txtFHasta" ErrorMessage="Fecha Erronea" 
                            MaximumValue="31/12/2999" MinimumValue="01/01/1900" Display="None"></asp:RangeValidator>--%>
                        <asp:HiddenField ID="HF_Pos" runat="server" />
                        <asp:HiddenField ID="HF_Id" runat="server" />
                    </td>
                    <tr>
                        <td align="right">
                            <br />
                            <%--  *******Botones************--%>
                            <asp:ImageButton ID="IB_Buscar" runat="server" 
                                ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" 
                                AlternateText="Buscar" />
                            <asp:ImageButton ID="IB_Imprimir" runat="server" 
                                ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" 
                                AlternateText="Informe" />
                            <asp:ImageButton ID="IB_Limpiar" runat="server" OnClick="IB_Limpiar_Click"
                                ImageUrl="../../../Imagenes/Botones/boton_limpiar_out.gif" 
                                onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" 
                                AlternateText="Limpia Todos los Datos" />
                        </td>
                    </tr>
                </tr>
            </table>
            <%--*******Mensaje******--%>
              
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="IB_Imprimir" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lb_cheque" runat="server">LinkButton</asp:LinkButton>
</asp:Content>
