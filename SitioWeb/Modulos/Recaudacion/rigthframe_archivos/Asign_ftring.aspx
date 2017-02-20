<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Asign_ftring.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_Asign_ftring" %>

<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignación de Factoring</title>

    <script language="javascript" src="../Funciones_modulo_js/Recaudacion.js"></script>

    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>

    <base target="_self"></base>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <script language="javascript">

        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");
            var _header = document.getElementById("HeaderDiv");
            _header.scrollLeft = _gridView.scrollLeft;
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label23" runat="server" CssClass="Titulos" Text="Asignación de  Documentos a Factoring"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="500" valign="top" class="Contenido">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Text="Factoring"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <table class="cabeceraGrilla">
                                                    <tr>
                                                        <td Width="90px">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" Text="Seleccion"></asp:Label>
                                                        </td>
                                                        <td width="120">
                                                            <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Codigo Factoring"></asp:Label>
                                                        </td>
                                                        <td align="left" width="470">
                                                            <asp:Label ID="Label12" runat="server" CssClass="LabelCabeceraGrilla" Text="Nombre Factoring"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" CssClass="Contenido" Height="150px" ScrollBars="Vertical">
                                                    <asp:GridView ID="gr_fact" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Btn_ver" runat="server" ToolTip='<%# Eval("codigo") %>' 
                                                                        ImageUrl="~/Images/bt_ver.gif" onclick="Btn_ver_Click" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="codigo">
                                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="descripcion">
                                                                <ItemStyle HorizontalAlign="Left" Width="470px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Documentos no Cedidos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <div id="HeaderDiv" style="overflow: hidden; width: 800px">
                                                    <table class="cabeceraGrilla" width="1560px">
                                                        <tr>
                                                            <td width="20">
                                                                <asp:ImageButton ID="IB_SeleccionDoctos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                                                    Width="20px" />
                                                            </td>
                                                            <td width="160">
                                                                <asp:Label ID="Label2" runat="server" CssClass="LabelCabeceraGrilla" Text="Factoring"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label3" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Docto"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label19" runat="server" CssClass="LabelCabeceraGrilla" Text="Moneda"></asp:Label>
                                                            </td>
                                                            <td valign="middle" width="130">
                                                                <asp:Label ID="Label5" runat="server" CssClass="LabelCabeceraGrilla" Text="Mto.Doc"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label6" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha Vcto."></asp:Label>
                                                            </td>
                                                            <td width="50">
                                                                <asp:Label ID="Label7" runat="server" CssClass="LabelCabeceraGrilla" Text="T.D"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label8" runat="server" CssClass="LabelCabeceraGrilla" Text="Nit Cliente"></asp:Label>
                                                            </td>
                                                            <td width="200px">
                                                                <asp:Label ID="Label9" runat="server" CssClass="LabelCabeceraGrilla" Text="Razón Social"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label20" runat="server" CssClass="LabelCabeceraGrilla" Text="Nit Pagador"></asp:Label>
                                                            </td>
                                                            <td width="200px">
                                                                <asp:Label ID="Label21" runat="server" CssClass="LabelCabeceraGrilla" Text="Razón Social"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label22" runat="server" CssClass="LabelCabeceraGrilla" Text="Fecha Ing."></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label13" runat="server" CssClass="LabelCabeceraGrilla" Text="Obs."></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label15" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Pago"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label17" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Hoja"></asp:Label>
                                                            </td>
                                                            <td width="80">
                                                                <asp:Label ID="Label18" runat="server" CssClass="LabelCabeceraGrilla" Text="Fac.Cambio"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="GridViewDiv" onscroll="DoScroll()" style="overflow: scroll; width: 800px;
                                                    height: 250px">
                                                    <asp:GridView ID="gr_fnc" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                        ShowHeader="False" Width="1560px">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="Ch_sel" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="pal_des">
                                                                <ItemStyle Width="160px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nce_num_doc">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nce_mto">
                                                                <ItemStyle Width="130px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nce_fec_vcto" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="tipo_docto">
                                                                <ItemStyle Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cli_idc">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cliente">
                                                                <ItemStyle Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deu_ide">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deu_rso">
                                                                <ItemStyle Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nce_fec_ing" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nce_obs">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_ing">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="id_hre">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="fac_cam">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <asp:HiddenField ID="pos_rec" runat="server" />
                    <asp:HiddenField ID="pos_deu" runat="server" />
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                                        ToolTip="Guardar" Visible="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_asoc" runat="server" ImageUrl="~/Imagenes/Botones/boton_asociar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_asociar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_asociar_in.gif';"
                                        ToolTip="Asociar" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                        ToolTip="Limpiar" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/boton_volver_out.gif"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_volver_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_volver_in.gif';"
                                        ToolTip="Volver" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
