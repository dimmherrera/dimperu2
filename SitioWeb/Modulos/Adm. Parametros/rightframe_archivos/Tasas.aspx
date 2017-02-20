<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Tasas.aspx.vb" Inherits="ClsTasas" Title="Mantenimiento Tasas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Ciuddad_Comuna.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script src="../FuncionesPrivadasJS/Grila.js" type="text/javascript"></script>

    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>

    <asp:UpdatePanel runat="server" ID="Updatepanel_Tasas">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" border="0" style="height: 660px"
                class="Contenido">
                <tbody>
                    <tr>
                        <td style="width: 100%; text-align: -moz-center" class="Cabecera" align="center">
                            <asp:Label ID="Label25" runat="server" Text="Mantenimiento-Tasas" CssClass="Titulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" valign="top" align="center" style="height: 580px; text-align: -moz-center;
                            width: 100%">
                            <%--**********Tabla Rb***********--%>
                            <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                <tr>
                                    <td class="Cabecera">
                                        <asp:Label ID="Label2" runat="server" Text="Tipos de Tasas" CssClass="SubTitulos"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="Rb_Tas_Max" runat="server" Text="Tasa Máxima Legal" Width="192px"
                                                        CssClass="Label" AutoPostBack="True" GroupName="Tasa"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="Rb_Tas_bas" runat="server" Text="Tasa Contratación" Width="150px"
                                                        CssClass="Label" AutoPostBack="True" GroupName="Tasa"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="Rb_Tas_Impto" runat="server" Text="Tasa Impuesto" Width="157px"
                                                        CssClass="Label" AutoPostBack="True" GroupName="Tasa"></asp:RadioButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="border-top-style: none; border-right-style: none; border-left-style: none;
                                border-bottom-style: none; text-align: -moz-center" class="Contenido">
                                <tbody>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Lb_TMC" runat="server" Text="Tasa Maxima Convencional" Width="100%"
                                                CssClass="Cabecera" Visible="False"></asp:Label>
                                            <asp:Label ID="Lb_TB" runat="server" Text="Tasa Base" Width="100%" CssClass="Cabecera"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="Lb_TI" runat="server" Text="Tasa Impuesto" Width="100%" CssClass="Cabecera"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="text-align: -moz_center">
                                            <asp:Panel ID="Panel_Tasa_Max_Conv" runat="server" Width="1000px">
                                                <table id="Tasa Max Conv" border="0" cellpadding="0" cellspacing="0" style="width: 500px">
                                                    <tr>
                                                        <td class="Cabecera" style="width: 531px">
                                                            <asp:Label ID="Label6" runat="server" Text="Tasa Maxima Legal" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido" style="width: 531px">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 306px">
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="400px">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label5" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButton ID="RBTMC_Activo" runat="server" Text="Activo" CssClass="Label"
                                                                                                    AutoPostBack="true" Checked="True" Enabled="False" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButton ID="RBTMC_Inactivo" runat="server" Text="Inactivo" CssClass="Label"
                                                                                                    AutoPostBack="true" Enabled="False" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 63px">
                                                                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Fecha Tasa" Width="60px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TMC_Fecha" runat="server" CssClass="clsDisabled" MaxLength="10"
                                                                                        TabIndex="1" Width="72px" ReadOnly="True"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="Txt_TMC_Fecha_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                                        Enabled="False" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="Txt_TMC_Fecha">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text=" % Tasa" Width="34px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TMC_Porc_Tasa" runat="server" CssClass="clsDisabled" MaxLength="5"
                                                                                        ReadOnly="True" Width="70px"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_TMC_Porc_Tasa_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_TMC_Porc_Tasa"
                                                                                        ValidChars=",">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label14" runat="server" CssClass="Label" Text="% Tasa Mora" Width="90px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TML_Mor_Porc" runat="server" CssClass="clsDisabled" MaxLength="5"
                                                                                        ReadOnly="True" Width="70px"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_TML_Mor_Porc_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_TML_Mor_Porc"
                                                                                        ValidChars=",">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <tr>
                                                    <td align="center" style="text-align: -moz-center">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="625px">
                                                            <tr>
                                                                <td class="Cabecera">
                                                                    <asp:Label ID="Label38" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Contenido">
                                                                    <asp:Panel ID="Panel_Gr_Tasas_Max_Con" runat="server" Width="619px" Height="400px"
                                                                        ScrollBars="Horizontal">
                                                                        <asp:GridView ID="Gr_Tasas_Max_Con" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                            ShowHeader="true" Width="600px">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Selección">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("Cod") %>' ImageUrl="~/Images/bt_ver.gif"
                                                                                            OnClick="Button1_Click" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Cod" HeaderText="Codigo">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="tmc_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="tmc_val" HeaderText="Valor Tasa" DataFormatString="{0:F2}" >
                                                                                    <ItemStyle HorizontalAlign="Right" Width="200px" /><%--DataFormatString="{0:###,###,###.00}"--%>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="tmc_mor" HeaderText="Valor Mora" DataFormatString="{0:F2}">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="tmc_est" HeaderText="Estado">
                                                                                    <ItemStyle Width="200px" />
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
                                                                    <asp:ImageButton ID="IB_Prev_TMC" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                        AlternateText="Anterior" />
                                                                    <asp:ImageButton ID="IB_Next_TMC" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                        AlternateText="Siguiente" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_Tasa_Base" runat="server" Width="1000px">
                                                <table id="Tasa Base" border="0" cellpadding="0" cellspacing="0" style="width: 700px">
                                                    <tr>
                                                        <td class="Cabecera" style="height: 20px">
                                                            <asp:Label ID="Label23" runat="server" Text="Tasa Contratación" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label8" runat="server" Text="Fecha Ultima Modificacion" Width="168px"
                                                                                        CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TB_Fecha" runat="server" Width="72px" CssClass="clsDisabled"
                                                                                        MaxLength="10" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                                <td colspan="2" valign="top">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="300px">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label9" runat="server" Text="Estado" Width="40px" CssClass="Label"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButton ID="RBTB_Activo" runat="server" Text="Activo" CssClass="Label" AutoPostBack="True"
                                                                                                    Checked="True" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButton ID="RBTB_Inactivo" runat="server" Text="Inactivo" CssClass="Label"
                                                                                                    AutoPostBack="True" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label7" runat="server" Text="Tipo Moneda" Width="96px" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DP_TB_TipoMoneda" runat="server" Width="167px" CssClass="clsDisabled"
                                                                                        Enabled="False">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label13" runat="server" Text="Descripcion" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TB_Descrip" TabIndex="1" runat="server" Width="223px" CssClass="clsDisabled"
                                                                                        Height="19px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label10" runat="server" Text="Días Desde" Width="72px" CssClass="Label"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_TB_Desde" runat="server" Width="72px" CssClass="clsDisabled"
                                                                                                    MaxLength="3"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="Txt_TB_Desde_FilteredTextBoxExtender" runat="server"
                                                                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_TB_Desde">
                                                                                                </cc1:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label12" runat="server" Text="Días Hasta" Width="90px" CssClass="Label"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_TB_Hasta" runat="server" Width="72px" CssClass="clsDisabled"
                                                                                                    MaxLength="3"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="Txt_TB_Hasta_FilteredTextBoxExtender" runat="server"
                                                                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_TB_Hasta">
                                                                                                </cc1:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td valign="top">
                                                                                    <asp:Label ID="Label11" runat="server" Text=" % Tasa" Width="72px" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td valign="top" align="left">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_TB_Porc_Tasa" runat="server" Width="72px" CssClass="clsDisabled"
                                                                                                    MaxLength="5"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="Txt_TB_Porc_Tasa_FilteredTextBoxExtender" runat="server"
                                                                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_TB_Porc_Tasa"
                                                                                                    ValidChars=",">
                                                                                                </cc1:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                            <td align="right" >
                                                                                                <asp:Label ID="Label20" runat="server" Text="% Spread" Width="60px" CssClass="Label"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="Txt_TB_Spr" runat="server" Width="72px" CssClass="clsDisabled" MaxLength="5"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="Txt_TB_Spr_FilteredTextBoxExtender" runat="server"
                                                                                                    Enabled="true" FilterType="Custom, Numbers"   TargetControlID="Txt_TB_Spr" ValidChars=",">
                                                                                                </cc1:FilteredTextBoxExtender>
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
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 977px">
                                                    <%--********Tabla Grilla********--%>
                                                    <tr>
                                                        <td align="center" style="text-align: -moz-center">
                                                            <table id="Criterio de busqueda" style="width: 400px" border="0" cellpadding="0"
                                                                cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label15" runat="server" Text="Criterios de Busqueda" CssClass="SubTitulos"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButton ID="Rb_Todos" runat="server" Text="Todos" CssClass="Label" AutoPostBack="True"
                                                                                        GroupName="EST" Checked="True"></asp:RadioButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="Rb_Act" runat="server" Text="Activo" CssClass="Label" AutoPostBack="True"
                                                                                        GroupName="EST"></asp:RadioButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="Rb_Inac" runat="server" Text="Inactivo" CssClass="Label" AutoPostBack="True"
                                                                                        GroupName="EST"></asp:RadioButton>
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
                                                            <table border="0" cellpadding="0" cellspacing="0" width="950px">
                                                                <tr>
                                                                    <td class="Cabecera">
                                                                        <asp:Label ID="Label39" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido">
                                                                        <asp:Panel ID="Panel_Gr_TB" runat="server" Width="960px" Height="300px">
                                                                            <asp:GridView ID="Gr_TB" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Selección">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="Button1" runat="server" CssClass="button" ToolTip='<%# Eval("typ_Cod") %>'
                                                                                                ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click1" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="typ_Cod" HeaderText="Código">
                                                                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_desc" HeaderText="Moneda" DataFormatString="{0:&quot;###,###,###.00&quot;}">
                                                                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                                                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_mto" DataFormatString="{0:F2}" HeaderText="Tasa">
                                                                                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="spread" DataFormatString="{0:F2}" HeaderText="Spread">
                                                                                        <ItemStyle Width="150px" HorizontalAlign="Right" /> 
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_dde" HeaderText="Días Desde">
                                                                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_hta" HeaderText="Días Hasta">
                                                                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_des" HeaderText="Descripción">
                                                                                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="typ_est" HeaderText="Estado">
                                                                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Lb_moneda" runat="server" Text='<%#Eval("id_p_0023")%>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
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
                                                                        <asp:ImageButton ID="IB_Prev_TB" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                            AlternateText="Anterior" />
                                                                        <asp:ImageButton ID="IB_Next_TB" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                            AlternateText="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_Tasa_Impuesto" runat="server" Width="1000px">
                                                <table id="Tasa_Impuesto" border="0" cellpadding="0" cellspacing="0" width="500px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label1" runat="server" Text="Tasa Impuesto" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 539px">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label19" runat="server" Text="Estado" Width="40px" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RBTI_Activo" runat="server" Text="Activo" CssClass="Label" Checked="True"
                                                                                        AutoPostBack="True" />
                                                                                </td>
                                                                                <td style="width: 79px">
                                                                                    <asp:RadioButton ID="RBTI_Inactivo" runat="server" Text="Inactivo" CssClass="Label"
                                                                                        AutoPostBack="True" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label16" runat="server" Text=" % Plazo" Width="56px" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TI_Porc_Plazo" TabIndex="1" runat="server" Width="72px" CssClass="clsDisabled"
                                                                                        ReadOnly="true" MaxLength="5" CausesValidation="True" ValidationGroup="ing"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_TI_Porc_Plazo_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_TI_Porc_Plazo"
                                                                                        ValidChars=",">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Fecha" Width="56px"> </asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TI_Fecha" runat="server" CssClass="clsDisabled" MaxLength="10"
                                                                                        ReadOnly="true" TabIndex="1" Width="70px"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 79px">
                                                                                    <asp:Label ID="Label17" runat="server" CssClass="Label" Text=" % Vista" Width="56px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="Txt_TI_Porc_Vista" runat="server" CssClass="clsDisabled" MaxLength="5"
                                                                                        ReadOnly="true" TabIndex="1" Width="72px"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_TI_Porc_Vista_FilteredTextBoxExtender" runat="server"
                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_TI_Porc_Vista"
                                                                                        ValidChars=".,">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table border="0" cellpadding="0" cellspacing="0" width="635px">
                                                    <tr>
                                                        <td class="Cabecera">
                                                            <asp:Label ID="Label40" runat="server" Text="Resultado de Búsqueda" CssClass="SubTitulos"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <asp:Panel ID="Panel_Gr_Ti" runat="server" Width="627px" Height="400px" ScrollBars="Horizontal">
                                                                <asp:GridView ID="Gr_Ti" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                    ShowHeader="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Selección">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="Button1" runat="server" CssClass="button" ToolTip='<%# Eval("tim_cod") %>'
                                                                                    ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click2" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="tim_Cod" HeaderText="Codigo">
                                                                            <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="tim_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha">
                                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="tim_val" HeaderText="Valor Plazo" DataFormatString="{0:F2}">
                                                                            <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="tim_TVista" HeaderText="Tasa Vista" DataFormatString="{0:F2}">
                                                                            <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="tim_est" HeaderText="Estado">
                                                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
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
                                                            <asp:ImageButton ID="IB_Prev_Ti" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                AlternateText="Anterior" />
                                                            <asp:ImageButton ID="IB_Next_Ti" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                AlternateText="Siguiente" />
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
                        <td colspan="1">
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Fecha Erronea"
                                ControlToValidate="Txt_TMC_Fecha" Display="None" MaximumValue="31/12/2999" MinimumValue="01/01/1900"></asp:RangeValidator>
                            <cc1:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server"
                                Enabled="True" TargetControlID="RangeValidator1">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </tbody>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                            OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" Enabled="False"
                            AlternateText="Nuevo" ToolTip="Nuevo" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif"
                            OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" Style="margin-top: 1px"
                            AlternateText="Guardar" ToolTip="Guardar" ValidationGroup="ing" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                            OnClick="IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" AlternateText="Limpiar"
                            ToolTip="Limpiar" />
                        <asp:TextBox ID="Txt_TB_Cod" runat="server" Width="0px" __designer:wfdid="w15" BorderWidth="0px"></asp:TextBox>
                        <asp:HiddenField ID="HF_Tim" runat="server" />
                        <asp:LinkButton ID="Link_TMC" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="Link_TB" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="Link_TI" runat="server"></asp:LinkButton>
                        <asp:HiddenField ID="HF_ID_Tmc" runat="server" />
                        <asp:HiddenField ID="HF_Po" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="IB_Guardar" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
</asp:Content>
