<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Notario.aspx.vb" Inherits="Notario" Title="Notarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../FuncionesPrivadasJS/Bancos.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" border="0" height="600px" class="Contenido">
                      <tr>
                        <td class="Cabecera" align="center" style="text-align: -moz-center">
                            <asp:Label ID="Label10" runat="server" CssClass="Titulos" Text="Mantenimiento-Administración de Notarios"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" height="450px" style="padding-left: 15px; text-align: -moz-center; width:100%" align="center">
                            <table cellspacing="0" width="1000">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="Label7" runat="server" __designer:wfdid="w40" CssClass="SubTitulos"
                                            Font-Bold="True" Style="left: 0px; position: static; top: 448px" Text="Datos Generales"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="left">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" __designer:wfdid="w63" CssClass="Label" Style="position: static"
                                                        Text="Sucursal" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="Dr_suc" runat="server" __designer:wfdid="w60" AutoPostBack="True"
                                                        CssClass="clsMandatorio" onselectedindexchanged1="Dr_suc" Style="position: static"
                                                        Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="Ch_def" runat="server" __designer:wfdid="w61" CssClass="Label"
                                                        Style="position: static" TabIndex="1" Text="Por Defecto" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" width="1000">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="Label6" runat="server" __designer:wfdid="w44" CssClass="SubTitulos"
                                            Font-Bold="True" Style="position: static" Text="Datos Personales"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="left">
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label1" runat="server" __designer:wfdid="w63" CssClass="Label" Style="position: static"
                                                        Text="Corr. Notario" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_corr" runat="server" __designer:wfdid="w64" AutoPostBack="True"
                                                        Enabled="False" Style="position: static" Visible="False" Width="25px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label2" runat="server" __designer:wfdid="w65" CssClass="Label" Style="position: static"
                                                        Text="Nombre"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_nom" runat="server" __designer:wfdid="w66" CssClass="clsMandatorio"
                                                        Style="position: static" TabIndex="2" Width="388px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" __designer:wfdid="w67" CssClass="Label" Style="position: static"
                                                        Text="Dirección"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_dir" runat="server" __designer:wfdid="w68" CssClass="clsMandatorio"
                                                        Style="position: static" TabIndex="3" Width="388px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" __designer:wfdid="w51" CssClass="Label" Style="position: static"
                                                        Text="Teléfono"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_tel" runat="server" __designer:wfdid="w70" CssClass="clsMandatorio"
                                                        Style="position: static" TabIndex="4" Width="100px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="Txt_tel_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="Txt_tel">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" __designer:wfdid="w73" CssClass="Label" Style="position: static"
                                                        Text="Dir. de la Empresa"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_diremp" runat="server" __designer:wfdid="w74" CssClass="clsMandatorio"
                                                        Style="position: static" TabIndex="6" Width="388px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="position: static" cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-left: 20px; padding-top: 15px">
                                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="Cabecera">
                                                        <table class="cabecera" style="width: 100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" width="80">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="LabelCabeceraGrilla" Text="Selección"></asp:Label>
                                                                </td>
                                                                <td align="center" width="80">
                                                                    <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Correlativo"></asp:Label>
                                                                </td>
                                                                <td align="center" width="150">
                                                                    <asp:Label ID="Label12" runat="server" CssClass="LabelCabeceraGrilla" Text="Nombre"></asp:Label>
                                                                </td>
                                                                <td align="center" width="150">
                                                                    <asp:Label ID="Label13" runat="server" CssClass="LabelCabeceraGrilla" Text="Sucursal"></asp:Label>
                                                                </td>
                                                                <td align="center" width="150">
                                                                    <asp:Label ID="Label15" runat="server" CssClass="LabelCabeceraGrilla" Text="Domicilio"></asp:Label>
                                                                </td>
                                                                <td width="150" align="center">
                                                                    <asp:Label ID="Label16" runat="server" CssClass="LabelCabeceraGrilla" Text="Dirección Empresa"></asp:Label>
                                                                </td>
                                                                <td align="center" width="80">
                                                                    <asp:Label ID="Label17" runat="server" CssClass="LabelCabeceraGrilla" Text="Teléfono"></asp:Label>
                                                                </td>
                                                                <td align="center" width="80">
                                                                    <asp:Label ID="Label18" runat="server" CssClass="LabelCabeceraGrilla" Text="Por Def."></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="Contenido">
                                                        <div style="overflow: auto; width: 100%; position: static; height: 300px; text-align: center"
                                                            align="center">
                                                            <asp:GridView Style="position: static" ID="Gv_Notario" runat="server" CssClass="formatUltcell"
                                                                Width="100%" __designer:wfdid="w75" PageSize="7" AutoGenerateColumns="False"
                                                                ShowHeader="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Selección">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Btn_ver_ciu" runat="server" ToolTip='<%# Eval("ID_NTR") %>'
                                                                                ImageUrl="~/Images/bt_ver.gif" onclick="Btn_ver_Click" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ID_NTR" HeaderText="Corr">
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ntr_nom" HeaderText="Nombre" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                        <ItemStyle Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="suc_nom" HeaderText="Sucursal" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                        <ItemStyle Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ntr_dml" HeaderText="Domicilio" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                        <ItemStyle Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ntr_dml_emp" HeaderText="Direcci&#243;n  Empresa" HtmlEncode="False">
                                                                        <ItemStyle Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ntr_tel" HeaderText="Tel&#233;fono" HtmlEncode="False">
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ntr_def" HeaderText="Por Def.">
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <%--<SelectedRowStyle BorderColor="Red"></SelectedRowStyle>--%>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                                <RowStyle CssClass="formatUltcell" />
                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                
                <caption>
                    &nbsp;&nbsp;
                    <tr>
                        <td align="right" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btn_nue" runat="server" __designer:dtid="9570149208162384" __designer:wfdid="w6"
                                            ImageUrl="~/Imagenes/Botones/Boton_nuevo_out.gif" OnClick="ImageButton2_Click"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.GIF';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.GIF';"
                                            ToolTip="Nuevo" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_guar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                            ToolTip="Guardar" onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                                            onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_eli" runat="server" __designer:dtid="9570149208162382" __designer:wfdid="w5"
                                            ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif" OnClick="ImageButton1_Click1"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';"
                                            ToolTip="Eliminar" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_lim" runat="server" __designer:dtid="9570149208162386" __designer:wfdid="w7"
                                            ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" OnClick="btn_limp1_Click"
                                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                            ToolTip="Limpiar" />
                                    </td>
                                </tr>
                            </table>
                            <asp:LinkButton ID="Detalle" runat="server"></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    </tbody>
                </caption>
            </table>
            <asp:HiddenField ID="pos" runat="server" />
            <asp:HiddenField ID="pos_gr" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lb_guarda" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lb_eli" runat="server"></asp:LinkButton>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
