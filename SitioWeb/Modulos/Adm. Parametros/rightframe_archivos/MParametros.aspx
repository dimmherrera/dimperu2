<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="MParametros.aspx.vb" Inherits="MParametro" Title="Mantenimiento Parámetros" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server" ID="UP_PARAMETROS">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="1" width="100%" border="0" class="Contenido" >
                <tr>
                    <td style="width: 1029px" align="center" class="Cabecera" height="31px">
                        <asp:Label ID="Label33" runat="server" CssClass="Titulos">Mantenimiento-Administración De Parámetros</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center" style="padding: 10px; height: 500px; text-align: -moz-center; width:100%"
                        class="Contenido">
                        <asp:Panel ID="Panel_Contenido" runat="server" Width="100%" ScrollBars="Vertical"
                            Height="550px">
                            <table style="width: 95%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td valign="top" align="center" style="width: -moz-center">
                                        <table cellspacing="0" cellpadding="10" border="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="500">
                                                        <tr>
                                                            <td class="Cabecera" style="text-align: -moz-left" align="left">
                                                                <asp:Label ID="Label38" runat="server" CssClass="SubTitulos">Tablas</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido" align="left">
                                                                <table border="0" cellpadding="0" cellspacing="3">
                                                                    <tr>
                                                                        <td align="right" style="width: 100px; text-align: -moz-right">
                                                                            <asp:RadioButton ID="Rb_num" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                Font-Bold="False" Text="Numérico" TextAlign="Left" GroupName="PAR" Checked="True"
                                                                                Width="103px" />
                                                                        </td>
                                                                        <td align="right" style="width: 200px">
                                                                            <asp:DropDownList ID="Dd_Tablas" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                                Width="228px" Font-Bold="False">
                                                                            </asp:DropDownList>
                                                                            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="Dd_Tablas"
                                                                                PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                                                PromptPosition="Bottom" IsSorted="true">
                                                                            </cc1:ListSearchExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 100px; text-align: -moz-right">
                                                                            <asp:RadioButton ID="Rb_alfa" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                Font-Bold="False" Text="Alfanumérico" BorderStyle="None" TextAlign="Left" GroupName="PAR"
                                                                                Width="103px" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="Drop_TablaAlfa" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                                Width="200px">
                                                                            </asp:DropDownList>
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
                                                    <br />
                                                    <table border="0" cellpadding="0" cellspacing="0" width="500">
                                                        <tr>
                                                            <td align="left" class="Cabecera" style="text-align: -moz-left">
                                                                <asp:Label ID="Label34" runat="server" CssClass="SubTitulos">Parámetros</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="right" style="width: 100px">
                                                                            <asp:Label ID="Label31" runat="server" CssClass="Label">Parámetro</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 396px">
                                                                            <asp:TextBox ID="Txt_codigo" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                                Width="100px"></asp:TextBox>
                                                                            <asp:DropDownList ID="Dt_par" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                                Width="200px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 100px">
                                                                            <asp:Label ID="Label30" runat="server" CssClass="Label">Descripción</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 396px">
                                                                            <asp:TextBox ID="txt_Des" runat="server" CssClass="clsMandatorio" Width="228px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td align="right" style="width: 100px">
                                                                           <asp:Label ID="Label46" runat="server" CssClass="Label" Visible="false">Cod. Interno</asp:Label>
                                                                       </td>
                                                                       <td align="left" style="width: 396px">
                                                                           <asp:TextBox ID="txt_cod_int" runat="server" CssClass="clsMandatorio" MaxLength="12" Width="228px" Visible="false"></asp:TextBox>
                                                                       </td> 
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 100px">
                                                                            <asp:Label ID="Label29" runat="server" CssClass="Label">Estado</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 396px">
                                                                            <asp:DropDownList ID="Dt_est" runat="server" AutoPostBack="True" CssClass="clsTxt"
                                                                                Width="228px">
                                                                                <asp:ListItem Value="X">Seleccionar</asp:ListItem>
                                                                                <asp:ListItem Value="A">Activo</asp:ListItem>
                                                                                <asp:ListItem Value="I">Inactivo</asp:ListItem>
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
                                    <td style="border-style: none; text-align: -moz-center" valign="top" align="center">
                                        <br />
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="View1" runat="server" __designer:wfdid="w35">
                                                <div>
                                                    <table width="500px">
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                <asp:Label ID="label41" runat="server" CssClass="SubTitulos" Text="Detalle"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="3" class="Contenido" width="500px">
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right">
                                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" __designer:wfdid="w36">Forma de Creación</asp:Label>
                                                                        </td>
                                                                        <td align="left" valign="top" style="text-align: -moz-left; width: 90px">
                                                                            <asp:RadioButton ID="Rb_auto" runat="server" Text="Automatico " GroupName="FORMA"
                                                                                __designer:wfdid="w37" CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                        <td align="left" valign="top" style="text-align: -moz-left; width: 200px">
                                                                            <asp:RadioButton ID="Rb_manual" runat="server" Text="Manual" GroupName="FORMA" __designer:wfdid="w38"
                                                                                CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top" style="text-align: -moz-right">
                                                                            <asp:Label ID="Label16" runat="server" CssClass="Label" __designer:wfdid="w39">Cobra Interés</asp:Label>
                                                                        </td>
                                                                        <td align="left" valign="top" style="text-align: -moz-left; width: 90px">
                                                                            <asp:RadioButton ID="Rb_cinteresi" runat="server" Text="Si" GroupName="INTERES" __designer:wfdid="w40"
                                                                                CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                        <td align="left" valign="top" style="text-align: -moz-left; width: 200px">
                                                                            <asp:RadioButton ID="Rb_cinteresNO" runat="server" Text="No" GroupName="INTERES"
                                                                                __designer:wfdid="w41" CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top" style="text-align: -moz-right">
                                                                            <asp:Label ID="Label17" runat="server" CssClass="Label" __designer:wfdid="w42">Cobra - Paga</asp:Label>
                                                                        </td>
                                                                        <td align="left" valign="top" style="text-align: -moz-left; width: 90px">
                                                                            <asp:CheckBox ID="Ch_cobra" runat="server" Text="Cobra" __designer:wfdid="w43" CssClass="Label">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td align="left" valign="top" style="text-align: -moz-left; width: 200px">
                                                                            <asp:CheckBox ID="Ch_paga" runat="server" Text="Paga" __designer:wfdid="w44" CssClass="Label">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <br />
                                            </asp:View>
                                            <asp:View ID="View2" runat="server" __designer:wfdid="w45">
                                                <div>
                                                    <table width="500px">
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                <asp:Label ID="Label40" runat="server" CssClass="SubTitulos" Text="Detalle"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table class="Contenido" width="500px">
                                                                    <tr>
                                                                        <td align="right" style="width: 180px">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" __designer:wfdid="w46">Aplicar dias de retención</asp:Label>&nbsp;
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:RadioButton ID="Rb_tdcto" runat="server" CssClass="Label" __designer:wfdid="w49"
                                                                                Text="Tipo Docto." GroupName="dret" AutoPostBack="True"></asp:RadioButton>&nbsp;&nbsp;<asp:RadioButton
                                                                                    ID="Rb_pza" runat="server" CssClass="Label" __designer:wfdid="w50" Text="Plaza"
                                                                                    GroupName="dret" AutoPostBack="True"></asp:RadioButton>
                                                                        </td>
                                                                        <td style="text-align: -moz-right" align="right">
                                                                            <asp:Label ID="Label14" runat="server" CssClass="Label" __designer:wfdid="w47">Nº dias </asp:Label>
                                                                        </td>
                                                                        <td style="text-align: -moz-left" align="left">
                                                                            <asp:TextBox ID="Txt_dias" runat="server" CssClass="clsMandatorio" Width="40px" __designer:wfdid="w48"
                                                                                MaxLength="2"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="Txt_dias_FilteredTextBoxExtender" runat="server"
                                                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_dias">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right">
                                                                            <asp:Label ID="Label2" runat="server" CssClass="Label" __designer:wfdid="w51">Sigla</asp:Label>
                                                                        </td>
                                                                        <td style="text-align: -moz-left" align="left">
                                                                            <asp:TextBox ID="Txt_sigla" runat="server" CssClass="clsMandatorio" __designer:wfdid="w52"
                                                                                MaxLength="2" Width="20px"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="Txt_sigla_FilteredTextBoxExtender" runat="server"
                                                                                Enabled="True" FilterType="UppercaseLetters" TargetControlID="Txt_sigla" ValidChars="LowercaseLetters">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:Button ID="B_CategoriaRiesgo" runat="server" Text="Categoria de Riesgo" Visible="False" />
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right">
                                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" __designer:wfdid="w53">Nº DIAS ANTES DE COBRAR</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:TextBox ID="Txt_diancob" runat="server" CssClass="clsMandatorio" __designer:wfdid="w54"
                                                                                Width="70px" MaxLength="3"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="Txt_diancob_FilteredTextBoxExtender" runat="server"
                                                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_diancob">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label4" runat="server" CssClass="Label" __designer:wfdid="w55">Dias habil</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:CheckBox ID="Ch_busdh" runat="server" CssClass="Label" __designer:wfdid="w56"
                                                                                Text="Busca dia Hábil"></asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label5" runat="server" CssClass="Label" __designer:wfdid="w57">Dev. dias de retención</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:CheckBox ID="Ch_diasret" runat="server" CssClass="Label" __designer:wfdid="w58"
                                                                                Text="Dev.Días de Retención"></asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" align="left" style="text-align: -moz-left">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                                                                <tr>
                                                                                    <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                                        <asp:Label ID="Label39" runat="server" Text="Comision por Documento" CssClass="SubTitulos"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="Contenido">
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td align="right" style="width: 100px">
                                                                                                    <asp:Label ID="Label6" runat="server" CssClass="Label" __designer:wfdid="w59">Tipo Moneda</asp:Label>
                                                                                                </td>
                                                                                                <td style="width: 100px; text-align: -moz-left" align="left">
                                                                                                    <asp:DropDownList ID="Dp_mon" runat="server" CssClass="clsMandatorio" __designer:wfdid="w60"
                                                                                                        AutoPostBack="True">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td align="right" style="width: 100px">
                                                                                                    <asp:Label ID="Label7" runat="server" CssClass="Label" __designer:wfdid="w61">%Comisión</asp:Label>
                                                                                                </td>
                                                                                                <td style="width: 100px; text-align: -moz-left" align="left">
                                                                                                    <asp:TextBox ID="Txt_comi" runat="server" CssClass="clsMandatorio" __designer:wfdid="w62"
                                                                                                        MaxLength="3" Width="70px"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_comi_FilteredTextBoxExtender" runat="server"
                                                                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_comi">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="right" style="width: 100px">
                                                                                                    <asp:Label ID="Label8" runat="server" CssClass="Label" __designer:wfdid="w63">MIN</asp:Label>
                                                                                                </td>
                                                                                                <td style="width: 100px; text-align: -moz-left" align="left">
                                                                                                    <asp:TextBox ID="Txt_min" runat="server" CssClass="clsMandatorio" __designer:wfdid="w64"
                                                                                                        MaxLength="15" AutoPostBack="true" Width="90px"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_min_FilteredTextBoxExtender" runat="server"
                                                                                                        Enabled="True" FilterType="Numbers" TargetControlID="Txt_min">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td align="right" style="width: 100px">
                                                                                                    <asp:Label ID="Label9" runat="server" CssClass="Label" __designer:wfdid="w65">Max</asp:Label>
                                                                                                </td>
                                                                                                <td style="width: 100px; text-align: -moz-left" align="left">
                                                                                                    <asp:TextBox ID="Txt_max" runat="server" CssClass="clsMandatorio" __designer:wfdid="w66"
                                                                                                        MaxLength="15" AutoPostBack="true" Width="90px"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="Txt_max_FilteredTextBoxExtender" runat="server"
                                                                                                        Enabled="True" FilterType="Numbers" TargetControlID="Txt_max">
                                                                                                    </cc1:FilteredTextBoxExtender>
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
                                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" __designer:wfdid="w67">Tipo de documento a gestionar</asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:CheckBox ID="Ch_tdctogest" runat="server" CssClass="Label"></asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label11" runat="server" CssClass="Label" __designer:wfdid="w69">Pago tipo Dolar </asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:CheckBox ID="Ch_ptd" runat="server" CssClass="Label" ></asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View3" runat="server" __designer:wfdid="w73">
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td align="right" style="text-align: -moz-right">
                                                                <asp:Label ID="Label13" runat="server" Text="Sigla" __designer:wfdid="w74" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td align="left" style="text-align: -moz-left">
                                                                <asp:TextBox ID="Txt_sig" runat="server" CssClass="clsMandatorio" __designer:wfdid="w75"
                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    FilterType="UppercaseLetters" TargetControlID="Txt_sig" ValidChars="LowercaseLetters">
                                                                </cc1:FilteredTextBoxExtender>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View4" runat="server" __designer:wfdid="w76">
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td align="right" style="text-align: -moz-right">
                                                                <asp:Label ID="Label15" runat="server" CssClass="Label" __designer:wfdid="w77">Provisión</asp:Label>
                                                            </td>
                                                            <td align="left" style="text-align: -moz-left">
                                                                <asp:TextBox ID="Txt_prov" runat="server" CssClass="clsMandatorio" __designer:wfdid="w78"
                                                                    Width="50px" MaxLength="3"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_prov"
                                                                    Mask="999" MaskType="Number">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View5" runat="server" __designer:wfdid="w79">
                                                <div>
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td align="right" style="text-align: -moz-right">
                                                                    <asp:Label ID="Label18" runat="server" CssClass="Label" __designer:wfdid="w80">Abrev. Razón Social</asp:Label>
                                                                </td>
                                                                <td align="left" style="text-align: -moz-left">
                                                                    <asp:TextBox ID="txt_razsoc" runat="server" CssClass="clsMandatorio" __designer:wfdid="w81"
                                                                        MaxLength="20" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View6" runat="server" __designer:wfdid="w82">
                                                <div>
                                                    <table width="500px">
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                <asp:Label ID="Label42" CssClass="SubTitulos" runat="server" Text="Detalle"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table class="Contenido" width="500px">
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 100px">
                                                                            <asp:Label ID="Label21" runat="server" CssClass="Label" __designer:wfdid="w85" Text="Código Interno"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:TextBox ID="Txt_ci" runat="server" CssClass="clsMandatorio" __designer:wfdid="w86"
                                                                                MaxLength="3" Width="50px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 100px">
                                                                            <asp:Label ID="Label23" runat="server" CssClass="Label" __designer:wfdid="w87" Text="Código D24"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:TextBox ID="Txt_cod24" runat="server" __designer:wfdid="w90" CssClass="clsMandatorio"
                                                                                MaxLength="3"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="Txt_cod24_FilteredTextBoxExtender" runat="server"
                                                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_cod24">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 100px">
                                                                            &nbsp;<asp:Label ID="Label22" runat="server" CssClass="Label" __designer:wfdid="w89"
                                                                                Text="Código Fogape"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:TextBox ID="Txt_cfogap" runat="server" __designer:wfdid="w88" CssClass="clsMandatorio"
                                                                                MaxLength="3"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="Txt_cfogap_FilteredTextBoxExtender" runat="server"
                                                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_cfogap">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View7" runat="server" __designer:wfdid="w93">
                                                <div>
                                                    <table width="500px">
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                <asp:Label ID="Label43" CssClass="SubTitulos" runat="server" Text="Detalle"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table class="Contenido" width="500px">
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 150px">
                                                                            <asp:Label ID="Label25" runat="server" __designer:wfdid="w94" Text="Nómina Egreso"
                                                                                CssClass="Label"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:RadioButton ID="Rb_dep" runat="server" __designer:wfdid="w95" Text="A deposito "
                                                                                GroupName="Egreso" CssClass="Label"></asp:RadioButton>&nbsp;&nbsp;<asp:RadioButton
                                                                                    ID="Rb_sdep" runat="server" __designer:wfdid="w96" Text="Sin deposito" GroupName="Egreso"
                                                                                    CssClass="Label"></asp:RadioButton>
                                                                            <asp:RadioButton ID="rb_transelec" runat="server" __designer:wfdid="w97" Text="Transferencia Electiva"
                                                                                GroupName="Egreso" CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 150px">
                                                                            <asp:Label ID="Label26" runat="server" __designer:wfdid="w98" Text="Valida Cta. Cte"
                                                                                CssClass="Label"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:RadioButton ID="Rb_valsi" runat="server" __designer:wfdid="w99" Text="Si" GroupName="cta"
                                                                                CssClass="Label"></asp:RadioButton>
                                                                            <asp:RadioButton ID="Rb_valno" runat="server" __designer:wfdid="w100" Text="No" GroupName="cta"
                                                                                CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 150px">
                                                                            <asp:Label ID="Label27" runat="server" __designer:wfdid="w101" Text="Ingresa Documento Pago"
                                                                                CssClass="Label"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:RadioButton ID="Rb_indcpag" runat="server" __designer:wfdid="w102" Text="Si"
                                                                                GroupName="Docpgo" CssClass="Label"></asp:RadioButton>
                                                                            <asp:RadioButton ID="Rb_noingdcpag" runat="server" __designer:wfdid="w103" Text="No"
                                                                                GroupName="Docpgo" CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="text-align: -moz-right; width: 150px">
                                                                            <asp:Label ID="Label28" runat="server" __designer:wfdid="w104" Text="Carga/Abono"
                                                                                CssClass="Label"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="text-align: -moz-left">
                                                                            <asp:RadioButton ID="Rb_cargosi" runat="server" __designer:wfdid="w105" Text="Si"
                                                                                GroupName="carabo" CssClass="Label"></asp:RadioButton>
                                                                            <asp:RadioButton ID="Rb_cargono" runat="server" __designer:wfdid="w106" Text="No"
                                                                                GroupName="carabo" CssClass="Label"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td align="right" style="text-align: -moz-right; width: 150px">
                                                                           <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Sistema"></asp:Label>
                                                                       </td> 
                                                                       <td align="left" style="text-align: -moz-left">
                                                                           <asp:RadioButton ID="Rb_SisA" runat="server" Text="Ambos" GroupName="sis" CssClass="Label"></asp:RadioButton>
                                                                           <asp:RadioButton ID="Rb_SisB" runat="server" Text="BackOffice" GroupName="sis" CssClass="Label"></asp:RadioButton>
                                                                           <asp:RadioButton ID="Rb_SisW" runat="server" Text="Web" GroupName="sis" CssClass="Label"></asp:RadioButton>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td align="right" style="text-align: -moz-right; width: 150px">
                                                                           <asp:Label ID="Label45" runat="server" CssClass="Label" Text="Aplica GMF"></asp:Label>
                                                                       </td>
                                                                       <td align="left" style="text-align: -moz-left">
                                                                           <asp:RadioButton ID="Rb_GMF_S" runat="server" Text="Si" GroupName="GrupoGMF" CssClass="Label"></asp:RadioButton>
                                                                           <asp:RadioButton ID="Rb_GMF_N" runat="server" Text="No" GroupName="GrupoGMF" CssClass="Label"></asp:RadioButton>
                                                                       </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <%--*******tipo de ingreso*****--%>
                                            <asp:View ID="View8" runat="server">
                                                <div>
                                                    <table width="600px">
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                <asp:Label ID="Label44" CssClass="SubTitulos" runat="server" Text="Detalle"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table class="Contenido" width="600px">
                                                                    <body>
                                                                        <tr>
                                                                            <td align="right" style="text-align: -moz-right; width: 160px">
                                                                                <asp:Label ID="Label24" runat="server" Text="Dias Liberacion Excedentes 
                                                                        y Facturas no Cedidas" CssClass="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="text-align: -moz-left">
                                                                                <asp:TextBox ID="txt_DLE" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="text-align: -moz-right; width: 160px">
                                                                                <asp:Label ID="Label32" runat="server" Text="Aplicar Dias de Retencion X" CssClass="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="text-align: -moz-left">
                                                                                <asp:RadioButton ID="Rb_TiIngreso" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                    Text="Tipo de Ingreso" />
                                                                                <asp:RadioButton ID="Rb_Plaza" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                    Text="Plaza" />
                                                                            </td>
                                                                            <td align="right" style="text-align: -moz-right; width: 100px">
                                                                                <asp:Label ID="Label35" runat="server" CssClass="Label" Text="Nº Dias de Retencion"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="text-align: -moz-left">
                                                                                <asp:TextBox ID="txt_NDR" runat="server" CssClass="clsMandatorio"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="text-align: -moz-right; width: 160px">
                                                                                <asp:Label ID="Label36" runat="server" Text="Ingresa Documento de Pago" CssClass="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="text-align: -moz-left">
                                                                                <asp:RadioButton ID="RB_Si" runat="server" Text="Si" CssClass="Label" AutoPostBack="true" />
                                                                                <asp:RadioButton ID="RB_No" runat="server" CssClass="Label" Text="No" AutoPostBack="true" />
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="text-align: -moz-right; width: 160px">
                                                                                <asp:Label ID="Label37" runat="server" Text="Nómina Depósito" CssClass="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="text-align: -moz-left">
                                                                                <asp:RadioButton ID="Rb_ND_Si" runat="server" Text="Si" CssClass="Label" AutoPostBack="true" />
                                                                                <asp:RadioButton ID="Rb_ND_No" runat="server" Text="No" CssClass="Label" AutoPostBack="true" />
                                                                            </td>
                                                                        </tr>
                                                                    </body>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View9_Plazas" runat="server">
                                                <div>
                                                    <table>
                                                        <body>
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="right" style="text-align: -moz-right">
                                                                                <asp:Label ID="Label20" runat="server" Text="Regiòn de la Plaza" CssClass="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="text-align: -moz-left">
                                                                                <asp:TextBox ID="txt_RegPla" runat="server" CssClass="clsMandatorio" MaxLength="2"
                                                                                    Width="20px"></asp:TextBox>
                                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_RegPla"
                                                                                    Mask="99" MaskType="Number">
                                                                                </cc1:MaskedEditExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </body>
                                                    </table>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </td>
                                </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="btn_cons" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.GIF';" runat="server"
                                        ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" __designer:wfdid="w15" ToolTip="Buscar Parametro">
                                    </asp:ImageButton>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btn_nue" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.GIF';" OnClick="btn_nue1_Click"
                                        runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" __designer:wfdid="w14"
                                        ToolTip="Nuevo"></asp:ImageButton>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btn_eli" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.GIF';" OnClick="btn_eli1_Click"
                                        runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif" __designer:wfdid="w13"
                                        ToolTip="Eliminar"></asp:ImageButton>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btn_gua" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="btn_gua1_Click"
                                        runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" __designer:wfdid="w12"
                                        ToolTip="Guardar Parametro"></asp:ImageButton>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btn_lim" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.GIF';" OnClick="btn_lim1_Click"
                                        runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif" __designer:wfdid="w11">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_Estado" runat="server" />
            <asp:HiddenField ID="HF_Gi_ult" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Link_Eliminar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="acceso" runat="server"></asp:LinkButton>
    
</asp:Content>
