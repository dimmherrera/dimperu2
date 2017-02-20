<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="CiudadComuna.aspx.vb" Inherits="CiudadComuna" Title="Mantenimiento Ciudad Comuna" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">

        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");

        }
    </script>

    <script src="../FuncionesPrivadasJS/Ciuddad_Comuna.js" type="text/javascript"></script>

    <asp:UpdatePanel runat="server" ID="Updatepanel_CiudadComuna">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="1" border="0" class="Contenido">
                <tr>
                    <td class="Cabecera" align="center" style="text-align: -moz-center">
                        <asp:Label ID="Label3" runat="server" Text="Mantenimiento-Ciudad/Comuna" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tbody>
                    <tr>
                        <td class="Contenido" style="height: 570px; width: 100%; text-align: -moz-center"
                            valign="top" align="center">
                            <%--*******Tabla Contenido********--%>
                            <table id="Contenido">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="Cabecera" align="left">
                                                    <asp:Label ID="Label5" runat="server" Text="Ciudades" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td align="right" style="text-align: -moz-right">
                                                                <asp:Label ID="Label40" runat="server" CssClass="Label" Text="Sucursal"></asp:Label>
                                                            </td>
                                                            <td align="left" style="text-align: -moz-left">
                                                                <asp:DropDownList ID="Dp_Sucursal" runat="server" CssClass="clsMandatorio" Width="464px"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <table border="1" cellpadding="0" cellspacing="0" width="699px">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Panel ID="Panel_Gr_Ciudad" runat="server" Width="700px" Height="130px" ScrollBars="None">
                                                                                <asp:GridView ID="Gr_Ciudad" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                                    ShowHeader="True" Width="680px">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Selección">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="Btn_ver_ciu" runat="server" 
                                                                                                    ToolTip='<%# Eval("id_ciu") %>' ImageUrl="~/Images/bt_ver.gif" 
                                                                                                    onclick="Btn_ver_ciu_Click" style="height: 13px" />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="id_ciu" HeaderText="Cod. Ciudad">
                                                                                            <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ciu_des" HeaderText="Descripción">
                                                                                            <ItemStyle Width="300px" HorizontalAlign="Left"  />
                                                                                        </asp:BoundField>
                                                                                      </Columns>
                                                                                    <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                    <RowStyle CssClass="formatUltcell" />
                                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:ImageButton ID="IB_Prev_Ciu" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                                AlternateText="Anterior" />
                                                                            <asp:ImageButton ID="IB_Next_Ciu" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                                                AlternateText="Siguiente" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--*******Tabla Comuna********--%>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tr>
                                                            <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                                <asp:Label ID="Label39" runat="server" CssClass="SubTitulos" Text="Comuna"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Contenido">
                                                                <table style="width: 660px">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Cod. Comuna"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Cod_Comuna" runat="server" CssClass="clsDisabled" Enabled="False"
                                                                                Width="40px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Descripcion"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsMandatorio" Width="464px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Zona Recaudación"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="Dp_Zona_Rec" runat="server" CssClass="clsMandatorio" Width="216px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table border="1" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Panel ID="Panel_Gr_Comuna" runat="server" Width="700px" Height="130px">
                                                                                            <asp:GridView ID="Gr_Comuna" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                                                Width="680px" ShowHeader="true">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Selección">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="Btn_ver_Cmn" runat="server" 
                                                                                                                ToolTip='<%# Eval("id_Cmn") %>' ImageUrl="~/Images/bt_ver.gif" 
                                                                                                                onclick="Btn_ver_Cmn_Click" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="id_Cmn" HeaderText="Cod. Comuna">
                                                                                                        <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Nom_Cmn" HeaderText="Descripción">
                                                                                                        <ItemStyle Width="300px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Zona" HeaderText="Zona Comuna">
                                                                                                        <ItemStyle Width="250px" />
                                                                                                    </asp:BoundField>
                                                                                                </Columns>
                                                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                                                                <RowStyle CssClass="formatUltcell" />
                                                                                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                                            </asp:GridView>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:ImageButton ID="IB_Prev_Com" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                                                            AlternateText="Anterior" />
                                                                                        <asp:ImageButton ID="IB_Next_Com" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
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
                        <td align="right">
                            <asp:HiddenField ID="HF_Posicion" runat="server" />
                            <asp:HiddenField ID="HF_Ciudad" runat="server" />
                            <asp:TextBox ID="Txt_Cod_Suc" runat="server" __designer:wfdid="w38" Width="0px" BorderWidth="0px"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="Ib_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" />
                            <asp:ImageButton ID="IB_GuardarCom" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" OnClick=" IB_GuardarCom_Click"
                                runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"></asp:ImageButton>
                            <asp:ImageButton ID="IB_LimpiarCom" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                                OnClick=" IB_LimpiarCom_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" />
                        </td>
                    </tr>
            </table>
            <asp:LinkButton ID="LinkBGrillaCmn" runat="server"></asp:LinkButton>
            <%-- <asp:LinkButton ID="LB_Guardar" OnClick="LB_Guardar_Click" runat="server"></asp:LinkButton>--%>
            <asp:LinkButton ID="LnkB_GrillaComuna" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="HF_Bco" runat="server" />
            <asp:HiddenField ID="HF_Po" runat="server" />
            <asp:HiddenField ID="HF_IdComuna" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
</asp:Content>
