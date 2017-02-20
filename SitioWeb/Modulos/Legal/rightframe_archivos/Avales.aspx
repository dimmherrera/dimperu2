<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    CodeFile="Avales.aspx.vb" Inherits="Avales" Title="Avales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
            
            <table id="Tabla Contenido" width="100%" border="0" cellpadding="0" cellspacing="1" class="Contenido" align="center">
                <tr>
                    <td class = "Cabecera" style="height:31px">
                        <asp:Label ID="Label1" runat="server" Text="Legal - Avales" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="height: 590px;padding:5px" valign="top" align="center" >
                        <table id="Tabla General" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <table id="Sucursales" border="0" cellpadding="0" cellspacing="0" width="150px">
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
                                                                        <asp:CheckBox ID="CB_Suc" runat="server" Text="Todas las Sucursales" Checked="true"
                                                                            AutoPostBack="true" CssClass="Label" />
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
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_Eje" runat="server" CssClass="clsTxt" AutoPostBack="True" Width="200px" />
                                                                     

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
                                                <table id="Tabla Cliente" style="width: 562px" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:CheckBox ID="CB_Cli" runat="server" AutoPostBack="true" Text="Cliente" CssClass="SubTitulos"
                                                                Checked="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsDisabled" 
                                                                            ReadOnly="true" Width="90px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="Txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsDisabled" AutoPostBack="true"
                                                                            ReadOnly="true" Width="15px" MaxLength="1"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" 
                                                                            runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                            TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server"  AlternateText="Ayuda Clientes"
                                                                         ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="25px" Enabled="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" 
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <table style="width: 568px" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label4" runat="server" Text="Tipo Aval" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="CB_TipoAvalTodos" runat="server" Text="Todos" CssClass="Label"
                                                                            AutoPostBack="true" Checked="True" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_TipoAval" runat="server" CssClass="clsTxt" AutoPostBack="True">
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
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label5" runat="server" Text="Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <%-- width="1170px" Se modifica tamaño de panel que contiene gv--%>
                                                                <asp:Panel ID="Panel_Gr_Avales" runat="server" ScrollBars="Horizontal" 
                                                                width="1350px" height="300px">
                                                        <asp:GridView ID="Gr_Avales" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="formatUltcell" ShowHeader="True" Width="2880px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Selección" 
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                                ToolTip='<%# Eval("id_avl") %>' onclick="Img_Ver_Click" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id_avl" HeaderText="Id">
                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Rut_Cli" HeaderText="NIT Cliente">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cliente" HeaderText="Razón Social">
                                                            <ItemStyle HorizontalAlign="Left" Width="350px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Rut_Aval" HeaderText="NIT Aval">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Nombre_Aval" HeaderText="Razón Social">
                                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Ciu_Particular" HeaderText="Ciudad">
                                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Comuna_Particualar" HeaderText="Comuna">
                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Domicilio_Particular" HeaderText="Domicilio">
                                                            <ItemStyle HorizontalAlign="Left" Width="350px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Reg_Matrimonial" HeaderText="Regimen Matrimonial">
                                                            <ItemStyle HorizontalAlign="Left" Width="340px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Tipo_Aval" HeaderText="Tipo Aval">
                                                            <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Est_Aval" HeaderText="Estado Aval">
                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Patrimonio" HeaderText="Patrimonio">
                                                            <ItemStyle HorizontalAlign="Left" Width="280px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Ciu_Comercial" HeaderText="Ciudad Comercial">
                                                            <ItemStyle HorizontalAlign="Left" Width="230px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Comuna_Comercial" HeaderText="Comuna Comercial">
                                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Domicilio_Comercial" 
                                                            HeaderText="Domicilio Comercial">
                                                            <ItemStyle HorizontalAlign="Left" Width="350px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sucursal" HeaderText="Sucursal">
                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo">
                                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Notaria" HeaderText="Notaria">
                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha_est" DataFormatString="{0:dd/MM/yyyy}" 
                                                            HeaderText="Fec. Est. Sit.">
                                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha_Ju" DataFormatString="{0:dd/MM/yyyy}" 
                                                            HeaderText="Fec. Jut. Extr.">
                                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
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
                <tr>
                    <td align="right">
                        <%--********Botonera***************--%>
                        <asp:HiddenField ID="HF_Pos" runat="server" />
                        <asp:HiddenField ID="HF_Id" runat="server" />
                        <asp:HiddenField ID="HF_id_Aval" runat="server" />
                        <asp:LinkButton ID="Link_Gr" runat="server"></asp:LinkButton>
                        
                        <asp:ImageButton ID="IB_Buscar" runat="server" AlternateText="Buscar" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                              <asp:ImageButton ID="IB_Nuevo" runat="server" AlternateText="Nuevo" OnClick="IB_Nuevo_Click"
                            ImageUrl="../../../Imagenes/Botones/Boton_Nuevo_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" />
                       <asp:ImageButton ID="IB_Detalle" runat="server"  AlternateText="Detalle" 
                         ImageUrl="../../../Imagenes/Botones/boton_detalle_out.gif" 
                         onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_out.gif';" 
                         onmouseover="this.src='../../../Imagenes/botones/boton_detalle_in.gif';"/>
                        <asp:ImageButton ID="IB_Imprimir" runat="server" AlternateText="Imprimir" ImageUrl="../../../Imagenes/Botones/boton_imprimir_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" />
                        <asp:ImageButton ID="Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" />
                  
                    </td>
                </tr>
            </table>
            
           <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Nuevo" />
            <asp:PostBackTrigger ControlID="IB_Imprimir" />
           
        </Triggers>
    </asp:UpdatePanel>
  
    <asp:LinkButton ID="LinkB_Elimina" runat="server"></asp:LinkButton>
    </asp:Content>
