<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Modulos/Master/MasterPage.master"  CodeFile="Pagare.aspx.vb" Inherits="Pagares"  Title="Pagaré"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />

    <script src="../FuncionesPrivadasJS/Pagares.js" type="text/javascript"></script>
  <script language="javascript">
function DoScroll()
 {
    var _gridView = document.getElementById("GridViewDiv");
    var _header = document.getElementById("HeaderDiv");
     _header.scrollLeft = _gridView.scrollLeft;
 }
    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table id="tabla General" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido" align="center">
            <tr>
                <td class = "Cabecera" style="height: 31px">
                    <asp:Label ID="Label1" runat="server" Text="Legal - Pagaré" CssClass="Titulos"></asp:Label>
                </td>
            </tr>
            <tr>
            <td class="Contenido" style="height:590px ; padding:10px" valign="top" align="center">
     <%--**********Tabla Contenedora**********--%>
        <table border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td align="center">
            <table>
                <tr>
                    <td>
                        <%--*******Tabla Sucursales****--%>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label2" runat="server" Text="Sucursales" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CBox_TodasSuc" runat="server" Text="Todas las Sucursales" CssClass="Label"
                                                    Checked="True" />
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
                                    <asp:Label ID="Label3" runat="server" Text="Ejecutivo" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="280px" class="Contenido" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="Drop_Ejecutivo" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 313px">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label8" runat="server" Text="Tipo Pagaré" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="Rb_Todos" runat="server" Text="Todos" CssClass="Label" Checked="true"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Drop_TPPagare" runat="server" CssClass="clsTxt" 
                                                    AutoPostBack="True">
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
            <tr>
                <td colspan="2" align="center">
                    <%--******Tabla Cliente******--%>
                    <table>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="Cabecera">
                                            <asp:CheckBox ID="CB_Cliente" runat="server" Text="Cliente" CssClass="SubTitulos"
                                                AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" 
                                                            Width="90px" ReadOnly="true"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="False" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                            TargetControlID="Txt_Rut_Cli">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" 
                                                            Width="20px" ReadOnly="true"
                                                            AutoPostBack="True" MaxLength="1"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                            runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                            TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                     <td>
                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes"
                                                         ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="25px" Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" Width="450px"
                                                            ReadOnly="true"></asp:TextBox>
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
                                            <asp:Label ID="Label7" runat="server" Text="Fecha Protesto" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:TextBox ID="txt_FechaPro" runat="server" CssClass="clsTxt" Width="90px" 
                                                            AutoPostBack="false"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="txt_FechaPro_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                            TargetControlID="txt_FechaPro" UserDateFormat="DayMonthYear">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:CalendarExtender ID="txt_FechaPro_CalendarExtender" runat="server" CssClass="radcalendar"
                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_FechaPro">
                                                        </cc1:CalendarExtender>
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
                                <%--*******Tabla Fecha******--%>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="Cabecera">
                                            <asp:Label ID="Label4" runat="server" Text="Fecha Vigencia" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_Fdsd" runat="server" CssClass="clsTxt" MaxLength="10" 
                                                            Width="90px" AutoPostBack="false"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="txt_Fdsd_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fdsd">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:CalendarExtender ID="txt_Fdsd_CalendarExtender" runat="server" 
                                                            CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                            Format="dd-MM-yyyy" TargetControlID="txt_Fdsd">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_FHst" runat="server" CssClass="clsTxt" MaxLength="10" Width="90px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="txt_FHst_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FHst">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:CalendarExtender ID="txt_FHst_CalendarExtender" runat="server" 
                                                            CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                                            Format="dd-MM-yyyy" TargetControlID="txt_FHst">
                                                        </cc1:CalendarExtender>
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
                                            <asp:Label ID="Label9" runat="server" Text="Mandato" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table width="300px">
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="RB_MandatoTodos" runat="server" Text="Todos" CssClass="Label"
                                                            Checked="true" AutoPostBack="true" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="Drop_Mandato" runat="server" CssClass="clsTxt" AutoPostBack="true">
                                                            <asp:ListItem Value="NADA">Seleccionar</asp:ListItem>
                                                            <asp:ListItem Value="S">Con Mandato</asp:ListItem>
                                                            <asp:ListItem Value="N">Sin Mandato</asp:ListItem>
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
                                            <asp:CheckBox ID="CBox_Monto" runat="server" Text="Monto" CssClass="SubTitulos" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButton ID="Rb_Mon" runat="server" AutoPostBack="true" Checked="true" 
                                                                        CssClass="Label" Text="Todos" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="Drop_Moneda" runat="server" AutoPostBack="True" 
                                                                        CssClass="clsTxt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_Monto" runat="server" CssClass="clsDisabled" 
                                                                        MaxLength="10" ReadOnly="true"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_Monto_MaskedEditExtender" runat="server" 
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="False" 
                                                                        InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                                        TargetControlID="txt_Monto">
                                                                    </cc1:MaskedEditExtender>
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
       <%--******Tabla Tipo Pagare********--%>
           
            <tr  align="center">
                <td >
                <%--********Tabla Grilla*********--%>
                    <table>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label10" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido">
                                            <table>
                                                <tr>
                                                    <td>
                                                       
                                                            <asp:Panel ID="Panel_Gr_Pagare" runat="server" ScrollBars="Horizontal" 
                                                            width="1890px" height="320px" >
                                                               <asp:GridView ID="Gr_Pagare" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                ShowHeader="True" Width="1890px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                                                ToolTip='<%# Eval("id_pgr") %>' onclick="Img_Ver_Click" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="id_pgr" HeaderText="Id" Visible="false">
                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Rut_Cliente" HeaderText="NIT Cliente">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Razon_Social" HeaderText="Razón Social">
                                                                        <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="NumeroDocto" HeaderText="Nº Pagaré">
                                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Tipo_Pagare" HeaderText="Tipo Pagaré">
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Mandato" HeaderText="Mandato">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Fecha_Sucripcion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Ingreso">
                                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FechaVecto" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vcto.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" HeaderText="Monto Pagaré">
                                                                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Fecha_Protesto" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Protesto">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Antecedentes" HeaderText="Antecedentes">
                                                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Impuesto_Pagare" HeaderText="Impuesto">
                                                                        <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="id_cxc" HeaderText="CXC Impuesto">
                                                                        <ItemStyle Width="140px" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Motivo_Protesto" HeaderText="Motivo Protesto">
                                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle  CssClass="cabeceraGrilla" />
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
                <td align="right">
                    <%--******Botonera*******--%>
                    <asp:LinkButton ID="Link_Gr" runat="server"></asp:LinkButton>
                    <asp:HiddenField ID="HF_Pos" runat="server" />
                    <asp:HiddenField ID="HF_Id" runat="server" />
                    <asp:LinkButton ID="lb_cli" runat="server"></asp:LinkButton>
                    <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                        AlternateText="Buscar" onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                         <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Nuevo_out.gif"
                        AlternateText="Nuevo" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" />
                    <asp:ImageButton ID="IB_Detalle" runat="server" AlternateText="Detalle" ImageUrl="../../../Imagenes/Botones/boton_detalle_out.gif"
                        onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_detalle_in.gif';" />
                    <asp:ImageButton ID="IB_GeneraImpuesto" runat="server" Enabled="false" AlternateText="Genera Impuesto"
                        ImageUrl="../../../Imagenes/Botones/boton_asociar_out.gif" onmouseout="this.src='../../../Imagenes/Botones/boton_asociar_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/boton_asociar_in.gif';" />
                    <asp:ImageButton ID="IB_Imprime" runat="server" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                        AlternateText="Imprimir" Enabled="false" onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" />
                    <asp:ImageButton ID="IB_Limpia" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif" AlternateText="Limpiar"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" />
                   
                </td>
            </tr>
        </table>
         
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="IB_Nuevo" />
    
    <asp:PostBackTrigger ControlID="IB_GeneraImpuesto" />
    <asp:PostBackTrigger ControlID="IB_Imprime" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Actualiza" runat="server"></asp:LinkButton>
</asp:Content>