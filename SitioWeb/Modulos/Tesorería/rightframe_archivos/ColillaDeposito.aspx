<%@ Page Title="Colilla de Deposito" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="ColillaDeposito.aspx.vb" Inherits="ColillaDeposito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Colilla.js" type="text/javascript"></script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
                <%--Cabecera--%>
                <tr>
                    <td class = "Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Cancelaciòn - Colilla de Deposito"></asp:Label>
                    </td>
                </tr>
                <%--Contenido--%>
                <tr>
                    <td class="Contenido" style="padding: 10px" align="center">
                        <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <%--Criterios (Selección Custodia)--%>
                                        <table id="Table1" cellspacing="0" cellpadding="0" border="0" width="1000px">
                                                        <tbody>
                                                            <tr>
                                                                <td class="Cabecera" align="left">
                                                                    <asp:Label ID="Label10" runat="server"
                                                                        CssClass="SubTitulos" Text="Criterios de Preparación de Informe" __designer:wfdid="w284"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido" valign="middle"  style="height: 25px">
                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td align="right">
                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Custodia"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="DP_Custodia" runat="server" CssClass="clsMandatorio" Width="246px" AutoPostBack="True">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td align="right">
                                                                                                    <asp:Label ID="Label19" runat="server" Text="Fecha Ultima Planilla Generada" CssClass="Label"
                                                                                                        Visible="False"></asp:Label>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label22" runat="server" Text="Label" CssClass="SubTitulos" Visible="False"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table align="left" width="1000px">
                                            <tbody>
                                                <tr>
                                                    <td valign="top">
                                                        <!--Tabla Criterios (Selección Nuevo/Colillas Anteriores)!-->
                                                        <table id="Table8" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left" class="Cabecera">
                                                                        <asp:Label ID="Label7" runat="server" Text="Criterios" CssClass="SubTitulos"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido" style="height: 150px; width: 200px" valign="top" align="left">
                                                                        <cc2:TabContainer runat="server" ID="Tabs" Height="180px" ActiveTabIndex="0" 
                                                                            Width="236px">
                                                                            <cc2:TabPanel runat="server" ID="Pn_Nuevo" HeaderText="Nueva Colilla">
                                                                                <HeaderTemplate>
                                                                                    Nueva Colilla</HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Fecha de Proceso"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_FechaProceso" runat="server" Width="90px" CssClass="clsMandatorio"
                                                                                                        MaxLength="10"></asp:TextBox>
                                                                                                    <cc2:MaskedEditExtender ID="txt_FechaProceso_MaskedEditExtender" runat="server" 
                                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaProceso">
                                                                                                    </cc2:MaskedEditExtender>
                                                                                                    <cc2:CalendarExtender ID="txt_FechaProceso_CalendarExtender" runat="server" 
                                                                                                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                                                                                        TargetControlID="txt_FechaProceso" CssClass="radcalendar">
                                                                                                    </cc2:CalendarExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="IB_Procesar" 
                                                                                                        ImageUrl="~/Imagenes/btn_workspace/Nuevo_out.gif" 
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/Nuevo_out.gif';"
                                                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/Nuevo_in.gif';"
                                                                                                        runat="server" AlternateText="Procesar Fecha Colilla Deposito"
                                                                                                        Enabled="False"></asp:ImageButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_FechaProceso"
                                                                                                        Display="None" ErrorMessage="&lt;b&gt;Fecha Invalida&lt;/b&gt;&lt;br/&gt;Favor ingrese campo fecha con el siguiente Formato:&lt;br /&gt;dd/mm/yyyy"
                                                                                                        MaximumValue="31/12/3000" MinimumValue="01/01/1900" Type="Date"></asp:RangeValidator>
                                                                                                    <cc2:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server"
                                                                                                        Enabled="True" TargetControlID="RangeValidator1">
                                                                                                    </cc2:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc2:TabPanel>
                                                                            <cc2:TabPanel runat="server" ID="Pn_Anteriores" HeaderText="Colillas Generadas">
                                                                                <HeaderTemplate>
                                                                                    Colillas Generadas</HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Fecha Colilla Desde"></asp:Label>
                                                                                                    <asp:RequiredFieldValidator ID="RangeVal_FechaColillaHasta" runat="server" ControlToValidate="txt_FechaColillaHasta"
                                                                                                        Display="None" ErrorMessage="&lt;b&gt;Debe ingresar campo Fecha Colilla Hasta&lt;/b&gt;&lt;br/&gt;Campo fecha colilla hasta debe ser ingresado obligatoriamente "
                                                                                                        ValidationGroup="Cliente"></asp:RequiredFieldValidator>
                                                                                                    <cc2:ValidatorCalloutExtender ID="PNReqE1" runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight"
                                                                                                        TargetControlID="RangeVal_FechaColillaHasta" Width="350px">
                                                                                                    </cc2:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_FechaColillaDesde" runat="server" Width="90px" CssClass="clsMandatorio"
                                                                                                        MaxLength="10" ReadOnly="True"></asp:TextBox>
                                                                                                    <cc2:MaskedEditExtender ID="txt_FechaColillaDesde_MaskedEditExtender" 
                                                                                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                                        Mask="99/99/9999" TargetControlID="txt_FechaColillaDesde" 
                                                                                                        ClearMaskOnLostFocus="False">
                                                                                                    </cc2:MaskedEditExtender>
                                                                                                    <cc2:CalendarExtender ID="txt_FechaColillaDesde_CalendarExtender" 
                                                                                                        runat="server" CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                                                        Format="dd-MM-yyyy" TargetControlID="txt_FechaColillaDesde">
                                                                                                    </cc2:CalendarExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Fecha Colilla Hasta"></asp:Label>
                                                                                                    <asp:RangeValidator ID="RV_FechaColillaDesde" runat="server" ControlToValidate="txt_FechaColillaDesde"
                                                                                                        CssClass="validatorCalloutHighlight" Display="None" ErrorMessage="&lt;b&gt;Fecha Invalida&lt;/b&gt;&lt;br/&gt;Favor ingrese campo fecha con el siguiente Formato:&lt;br /&gt;dd/mm/yyyy"
                                                                                                        highlightcssclass="validatorCalloutHighlight" MaximumValue="31/12/6000" MinimumValue="01/01/1900"
                                                                                                        Type="Date" ValidationGroup="Cliente"></asp:RangeValidator>
                                                                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                                                                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RV_FechaColillaDesde">
                                                                                                    </cc2:ValidatorCalloutExtender>
                                                                                                    <asp:RangeValidator ID="RV_FechaColillaHasta" runat="server" ControlToValidate="txt_FechaColillaHasta"
                                                                                                        CssClass="validatorCalloutHighlight" Display="None" ErrorMessage="&lt;b&gt;Fecha Invalida&lt;/b&gt;&lt;br/&gt;Favor ingrese campo fecha con el siguiente Formato:&lt;br /&gt;dd/mm/yyyy"
                                                                                                        highlightcssclass="validatorCalloutHighlight" MaximumValue="31/12/6000" MinimumValue="01/01/1900"
                                                                                                        Type="Date" ValidationGroup="Cliente"></asp:RangeValidator>
                                                                                                    <cc2:ValidatorCalloutExtender ID="PNReqEx1" runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight"
                                                                                                        TargetControlID="RV_FechaColillaHasta">
                                                                                                    </cc2:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_FechaColillaHasta" runat="server" Width="90px" 
                                                                                                        CssClass="clsMandatorio"></asp:TextBox>
                                                                                                        <cc2:MaskedEditExtender ID="txt_FechaColillaHasta_MaskedEditExtender" 
                                                                                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                                                                        Mask="99/99/9999" TargetControlID="txt_FechaColillaHasta" 
                                                                                                        ClearMaskOnLostFocus="False">
                                                                                                    </cc2:MaskedEditExtender>
                                                                                                    <cc2:CalendarExtender ID="CalendarExtender1" 
                                                                                                        runat="server" CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                                                                        Format="dd-MM-yyyy" TargetControlID="txt_FechaColillaHasta">
                                                                                                    </cc2:CalendarExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="IB_Nuevo" runat="server" AlternateText="Procesar Fecha Colilla Deposito"
                                                                                                        Enabled="False" 
                                                                                                        ImageUrl="~/Imagenes/btn_workspace/Buscar_out.gif" 
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/Buscar_out.gif';"
                                                                                                        onmouseover="this.src='../../../Imagenes/btn_workspace/Buscar_in.gif';" />
                                                                                                        
                                                                                                    <asp:RequiredFieldValidator ID="RFV_FechaColillaDesde" runat="server" ControlToValidate="txt_FechaColillaDesde"
                                                                                                        Display="None" ErrorMessage="&lt;b&gt;Debe ingresar campo Fecha Colilla Desde&lt;/b&gt;&lt;br/&gt;Campo fecha colilla desde debe ser ingresado obligatoriamente "
                                                                                                        ValidationGroup="Cliente"></asp:RequiredFieldValidator>
                                                                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                                                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_FechaColillaDesde"
                                                                                                        Width="350px">
                                                                                                    </cc2:ValidatorCalloutExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3">
                                                                                                    <table id="Table9" border="0" cellpadding="0" cellspacing="0" width="200">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Panel ID="Panel_GV_ColAnteriores" runat="server" height="90px">
                                                                                                                
                                                                                                                    <asp:GridView ID="GV_ColAnteriores" runat="server" AutoGenerateColumns="False" 
                                                                                                                        CssClass="formatUltcell">
                                                                                                                        <Columns>
                                                                                                                            <asp:BoundField DataField="cdp_fec" HeaderText="Fecha">
                                                                                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField DataField="cdp_mto" HeaderText="monto">
                                                                                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                ItemStyle-Width="90px">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif"/>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                        <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                                                                        <RowStyle CssClass="formatUltcell" />
                                                                                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                                                    </asp:GridView>
                                                                                                             </asp:Panel>
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
                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc2:TabPanel>
                                                                        </cc2:TabContainer>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td valign="top" align="left">
                                                        <!--Tabla Cheques!-->
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="Cabecera">
                                                                    <asp:Label ID="Label20" runat="server" Text="Listado de Cheques" CssClass="SubTitulos"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido">
                                                                    <table id="Table3" border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RB_Todos" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                        Text="Todos" Enabled="False" Visible="False" />
                                                                                    <asp:RadioButton ID="RB_Pendientes" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                        Enabled="False" Text="Pendientes" Visible="False" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="CB_SelecTodo" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                        Enabled="False" Text="Seleccionar Todo" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                   
                                                                                    
                                                                                    <div id="GridViewDiv_Cheque" style="overflow: scroll; width: 750px; height: 200px">
                                                                                        <asp:GridView ID="GV_Cheques" runat="server" AutoGenerateColumns="False" 
                                                                                            CssClass="formatUltcell" Width="1170px">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                                                                                            oncheckedchanged="CheckBox1_CheckedChanged" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="id_chr" HeaderText="Cheque">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="cli_idc" HeaderText="Nit Cliente">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="cliente" HeaderText="Cliente">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="bco_des" HeaderText="Banco">
                                                                                                    <ItemStyle HorizontalAlign="Left" Width="220px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="chr_fev_rea" HeaderText="Fecha Pago">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="chr_num" HeaderText="Nº Docto.">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="pal_des" HeaderText="Region">
                                                                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Monto_cheque" HeaderText="Monto Cheque">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="pnu_des" HeaderText="Estado">
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                                            <RowStyle CssClass="formatUltcell" />
                                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
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
                                    <td>
                                        <!--Tabla Documentos!-->
                                        <br />
                                        <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="1000px">
                                            <tbody>
                                                <tr>
                                                    <td align="left" class="Cabecera">
                                                        <asp:Label ID="Label24" runat="server" Text="Documentos Cubiertos por Cheques Seleccionados"
                                                            CssClass="SubTitulos"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Contenido" valign="top">
                                                       
                                                            <div id="Div3" style="overflow: scroll; width: 1000px;height:190px">
                                                                <asp:GridView ID="GV_Documentos" runat="server" AutoGenerateColumns="False" 
                                                                    CssClass="formatUltcell">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="id_chr" HeaderText="Cheque">
                                                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="deu_ide" HeaderText="Nit Pagador">
                                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                        </asp:BoundField>
                                                                    <asp:BoundField DataField="deudor" HeaderText="Pagador">
                                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="id_ope" HeaderText="Nro Operacion">
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="tipo_doc" HeaderText="Tipo Documento">
                                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="dsi_num" HeaderText="Nº Documento">
                                                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="dsi_fev" HeaderText="Fecha Vto.">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="dsi_mto" HeaderText="Monto Doc">
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="saldo_cli" HeaderText="Saldo Documento">
                                                                            <ItemStyle HorizontalAlign="Right" Width="105px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="saldo_deu" HeaderText="Saldo Pagador">
                                                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Estado" HeaderText="Estado Doc">
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:BoundField>
                                                                       
                                                                    </Columns>
                                                                    <HeaderStyle  CssClass="cabeceraGrilla" />
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                </asp:GridView>
                                                            </div>
                                                       <%--</asp:Panel>--%>
                                                    </td>
                                                </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="99%">
                                            <tr>
                                                <td style="width:140px">
                                                    <asp:Label ID="Label12" runat="server" Text="Cantidad de Cheques" CssClass="Label"></asp:Label>
                                                </td>
                                                <td align="left" width="600px"> 
                                                    <asp:TextBox ID="txt_NroCheques" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                </td>
                                               <td align="left">
                                                    <asp:Label ID="Label5" runat="server" Text="Documentos Guardados" BorderStyle="Inset"
                                                        CssClass="Label" Width="250px" BackColor="Bisque" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label42" runat="server" Text="Total" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_Tot" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                        </table>
                                       
                                    </td>
                                  
                                </tr>
                            </table>
                    </td>
                </tr>
                <%--Botonera--%>
                <tr>
                    <td align="right">
                    <asp:ImageButton ID="IB_INFORME" runat="server" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" 
                            Height="25px" />
                        <asp:ImageButton ID="IB_Eli_Pendiente" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';"
                            Enabled="False" Visible="False" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                            Enabled="False" />
                       
                        <asp:ImageButton ID="IB_Rechazarsolicitud" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" 
                            AlternateText="Rechazar Solicitud de Prorroga" Enabled="False" Visible="False">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                            AlternateText="Limpiar"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            
  
            
            <asp:HiddenField ID="HF_Id_Ing" runat="server" />
            <asp:HiddenField ID="HF_Pos" runat="server" />
            <asp:HiddenField ID="HF_Id" runat="server" />
            <asp:HiddenField ID="HF_Nro_Neg" runat="server" />
            <asp:HiddenField ID="HF_IdMoneda" runat="server" />
            <asp:HiddenField ID="HF_Bco" runat="server" />
            <asp:HiddenField ID="HF_IdReg" runat="server" />
            <asp:HiddenField ID="HF_Cli" runat="server" />
            <asp:HiddenField ID="Id_Chr" runat="server" />
            <asp:HiddenField ID="txt_Rut_Deudor" runat="server" />
            
            <%--<asp:TextBox ID="Id_Chr" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="txt_Rut_Deudor" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" ForeColor="White"></asp:TextBox>--%>
            
            
            <asp:LinkButton ID="Lb_buscar" runat="server" ValidationGroup="Cliente"></asp:LinkButton>
            <asp:HiddenField ID="HF_CtaCte" runat="server" />
            
        </ContentTemplate>
        <Triggers>
   <asp:PostBackTrigger ControlID="IB_Guardar" />
     <asp:PostBackTrigger ControlID="IB_INFORME" />
   
   </Triggers>
    </asp:UpdatePanel>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:LinkButton ID="LinkB_CAnteriores" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
</asp:Content>
