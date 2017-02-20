<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Sucursal_Plaza.aspx.vb" Inherits="SucursalPlaza" Title="Mantenimiento Sucursal Plaza" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Sucursal_Plaza.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="Updatepanel_BancoPlaza" runat="server">
        <ContentTemplate>
            <table width="100%" style="height: 500px; margin-right: 39px;" border="0" cellpadding="0" cellspacing="1" class="Contenido">
                <tr>
                    <td style="text-align: -moz-center" align="center" width="100%" class="Cabecera">
                        <asp:Label ID="Label25" runat="server" Text="Mantenimiento-Sucursal/Plaza" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="width: 100%; height: 500px; text-align: -moz-center"
                        align="center">
                        <table>
                            <tr>
                                <td style="width: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" style="text-align: -moz-left" align="left">
                                                            <asp:RadioButton ID="RB_Sucursal" runat="server" Text="Sucursal" AutoPostBack="true"
                                                                GroupName="Plaz_Ciu" OnCheckedChanged="RB_Sucursal_CheckedChanged" CssClass="SubTitulos">
                                                            </asp:RadioButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%; height: 200px" class="Contenido">
                                                            <asp:Panel ID="Panel_Sucursal" runat="server" Width="100%">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="width: 171px" align="right">
                                                                                        <asp:Label ID="Label16" runat="server" Text="Correlativo" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 834px; text-align: -moz-left" align="left">
                                                                                        <asp:TextBox ID="Txt_CodSuc" runat="server" Width="32px" CssClass="clsDisabled" MaxLength="6"></asp:TextBox>
                                                                                        &nbsp;<asp:Label ID="Label10" runat="server" CssClass="Label" Text="Codigo Interno"></asp:Label>
                                                                                        <asp:TextBox ID="Txt_CodInt" runat="server" AutoPostBack="True" 
                                                                                            CssClass="clsDisabled" MaxLength="4" ReadOnly="True" Width="40px"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="Filtered_Txt_CodInt" runat="server" 
                                                                                            Enabled="true" FilterType="Numbers" TargetControlID="Txt_CodInt">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 171px" align="right">
                                                                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 834px; text-align: -moz-left" align="left">
                                                                                        <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsDisabled" Width="544px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="250: ;" align="right">
                                                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Descripción corta" 
                                                                                            Width="90px"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 834px; text-align: -moz-left" align="left">
                                                                                        <asp:TextBox ID="Txt_Descripcion_corta" runat="server" CssClass="clsDisabled" Width="120px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                             
                                                                                <tr>
                                                                                    <td style="width: 171px" align="right">
                                                                                        <asp:Label ID="Label2" runat="server" Text="Departamento" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 834px; text-align: -moz-left" align="left">
                                                                                        <asp:DropDownList ID="Dp_Cod_Region" runat="server" Width="300px" CssClass="clsDisabled"
                                                                                            Enabled="False">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 171px" align="right">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Plaza Banco Origen" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 834px; text-align: -moz-left" align="left">
                                                                                        <asp:DropDownList ID="Dp_PlazaBanco" runat="server" Width="300px" CssClass="clsDisabled"
                                                                                            Enabled="False">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 171px" align="right">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Teléfono" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td valign="top" style="text-align: -moz-left" align="left">
                                                                                        <asp:TextBox ID="Txt_telefono" runat="server" Width="90px" CssClass="clsDisabled"
                                                                                            __designer:wfdid="w6"></asp:TextBox>
                                                                                        <asp:Label ID="Label7" runat="server" Text="Fax" CssClass="Label"></asp:Label>
                                                                                        <asp:TextBox ID="Txt_fax" runat="server" Width="90px" CssClass="clsDisabled" 
                                                                                            __designer:wfdid="w6"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 171px" align="right">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Dirección" CssClass="Label"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 834px; text-align: -moz-left" align="left">
                                                                                        <asp:TextBox ID="Txt_Direccion" runat="server" Width="544px" CssClass="clsDisabled"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                               
                                                                                <tr>
                                                                                    <td colspan="2" style="padding: 5px" align="center">
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Panel ID="Panel_GR_Sucursales" runat="server" Width="660px" Height="130px">
                                                                                                        <asp:GridView ID="GR_Sucursales" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                                                            ShowHeader="true">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="Selección">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="Btn_ver" runat="server" ToolTip='<%# Eval("suc_Cod") %>' ImageUrl="~/Images/bt_ver.gif"
                                                                                                                            OnClick="Btn_ver_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:BoundField DataField="codigo" HeaderText="Codigo">
                                                                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:BoundField DataField="suc_nom" HeaderText="Descripción">
                                                                                                                    <ItemStyle Width="200px" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:BoundField DataField="suc_des_cra" HeaderText="Desc. Corta">
                                                                                                                    <ItemStyle Width="100px" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:BoundField DataField="pal_pla_doc" HeaderText="Cod. Plaza">
                                                                                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:BoundField DataField="Des_plaza" HeaderText="Plaza Banco Origen">
                                                                                                                    <ItemStyle Width="200px" />
                                                                                                                </asp:BoundField>
                                                                                                            </Columns>
                                                                                                            <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                                                            <RowStyle CssClass="formatUltcell" />
                                                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                                        </asp:GridView>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                            <td>&nbsp;</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" style="text-align: -moz-center">
                                                                                                    <asp:ImageButton ID="IB_Prev_Suc" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                                                        AlternateText="Anterior" />
                                                                                                    <asp:ImageButton ID="IB_Next_Suc" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                                                        AlternateText="Siguiente" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <asp:LinkButton ID="LinkB_GrSuc" runat="server"></asp:LinkButton>
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
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <%--<br />--%>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 100%; text-align: -moz-left" align="left" class="Cabecera">
                                                            <asp:RadioButton ID="RB_Plaza" runat="server" Text="Plaza" AutoPostBack="true" GroupName="Plaz_Ciu"
                                                                OnCheckedChanged="RB_Plaza_CheckedChanged" Enabled="False" CssClass="SubTitulos">
                                                            </asp:RadioButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 100%">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right" style="width: 60px">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Plaza Suc." Width="120px" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 700px; text-align: -moz-left" align="left">
                                                                                    <asp:DropDownList ID="Dp_Plaza_Suc" runat="server" Width="300px" CssClass="clsDisabled">
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="Label9" runat="server" Text="Dias de retención" Width="120px" CssClass="Label"></asp:Label>
                                                                                    <asp:TextBox ID="Txt_Dias_Reten" runat="server" Width="32px" CssClass="clsDisabled"
                                                                                        MaxLength="3"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_Dias_Reten_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Numbers" TargetControlID="Txt_Dias_Reten">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                    <asp:LinkButton ID="LinkB_CodPla" runat="server"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding: 5px" align="center">
                                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Panel ID="Panel_Gr_Plaza" runat="server" Width="380px" Height="130px">
                                                                                                    <asp:GridView ID="Gr_Plaza" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                                                        ShowHeader="true">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="Selección">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:ImageButton ID="Btn_ver2" runat="server" ToolTip='<%# Eval("pal_Cod") %>' 
                                                                                                                        ImageUrl="~/Images/bt_ver.gif" onclick="Btn_ver2_Click" />
                                                                                                                </ItemTemplate>
                                                                                                                
                                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="pal_Cod" HeaderText="Cod. Plaza">
                                                                                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="pal_des" HeaderText="Descripción">
                                                                                                                <ItemStyle Width="200px" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="pds_ret" HeaderText="Días  Ret.">
                                                                                                                <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                                                            </asp:BoundField>
                                                                                                        </Columns>
                                                                                                        <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                                                            <RowStyle CssClass="formatUltcell" />
                                                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                                    </asp:GridView>
                                                                                                    <asp:HiddenField ID="HF_PoPL" runat="server" />
                                                                                                    <asp:HiddenField ID="HF_IdPL" runat="server" />
                                                                                                </asp:Panel>
                                                                                                <%--</div>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" style="text-align: -moz-center">
                                                                                                <asp:ImageButton ID="IB_Prev_Pla" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                                                    AlternateText="Anterior" />
                                                                                                <asp:ImageButton ID="IB_Next_Pla" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
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
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 650px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <br />
                        <asp:TextBox ID="Txt_IndSuc" runat="server" Width="0px" BorderWidth="0px"></asp:TextBox>
                        <asp:TextBox ID="Txt_Sucursal_Oculta" runat="server" Width="0px" BorderWidth="0px"></asp:TextBox>
                        <asp:TextBox ID="Txt_Cod_Zona_Oculto" runat="server" Width="0px" BorderWidth="0px"></asp:TextBox>
                        <asp:TextBox ID="Txt_CodSuc_Oculto" runat="server" Width="0px" BorderWidth="0px"></asp:TextBox>
                        <asp:TextBox ID="Txt_CodInt_Oculto" runat="server" Width="0px" BorderWidth="0px"></asp:TextBox>
                        <asp:HiddenField ID="HF_Po" runat="server" />
                        <asp:HiddenField ID="HF_Codigo" runat="server" />
                        <asp:HiddenField ID="HF_CodSuc" runat="server" />
                        <asp:ImageButton ID="IB_Zona" onmouseover="this.src='../../../Imagenes/Botones/Boton_zona_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_zona_out.gif';" runat="server"
                            ImageUrl="../../../Imagenes/Botones/Boton_zona_out.gif" Enabled="False" AlternateText="Zonas">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Nuevo" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" OnClick="IB_Nuevo_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" AlternateText="Nuevo">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" OnClick="IB_Guardar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif" AlternateText="Guardar">
                        </asp:ImageButton>
                        <asp:ImageButton ID="IB_Eliminar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif" Enabled="False" Visible="False"
                            AlternateText="Eliminar"></asp:ImageButton>
                        <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" OnClick="IB_Limpiar_Click"
                            runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif" AlternateText="Limpiar">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Zona" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
</asp:Content>
