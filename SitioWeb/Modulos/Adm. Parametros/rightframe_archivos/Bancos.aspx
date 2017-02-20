<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Bancos.aspx.vb" Inherits="Bancos" Title="Mantenimiento Bancos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../FuncionesPrivadasJS/Bancos.js" type="text/javascript"></script>

    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");

        }

        function DoScrollsuc() {
            var _gridView = document.getElementById("GridViewDiv_Suc");

        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Tabla General" cellspacing="1" cellpadding="0" border="0" width="100%" class="Contenido"> 
                <tr>
                    <td style="text-align: moz-center; width: 100%" align="center" class="Cabecera">
                        <asp:Label ID="Label1" runat="server" Text="Mantenimiento-Bancos" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="width: 100%; text-align: -moz-center" align="center">
                        <%--*********Tabla Contenedora*********--%>
                        <table id="Tabla Contenedora" border="0" cellpadding="0" cellspacing="0" width="100%"
                            style="text-align: -moz-center">
                            <tr>
                                <td style="padding: 5px; height: 570px; text-align: -moz-center" valign="top" align="center">
                                    <table border="0" cellpadding="0" cellspacing="00">
                                        <tr>
                                            <td style="width: 100%" align="left">
                                                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                            <asp:RadioButton ID="RB_Bco" runat="server" Text="Banco" CssClass="SubTitulos" AutoPostBack="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <%--****************Tabla Bancos************ --%>
                                                            <table>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label2" runat="server" Text="Banco" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_id_Bco" runat="server" CssClass="clsDisabled" Width="90px" AutoPostBack="True"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_id_Bco_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_id_Bco">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label3" runat="server" Text="Descripción" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_Des_Bco" runat="server" CssClass="clsDisabled" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="padding: 5px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                                                            <tr>
                                                                                <td class="Cabecera">
                                                                                    <table id="cabecera Gr_Bco" border="0" cellpadding="0" cellspacing="0" width="500px"
                                                                                        class="cabeceraGrilla">
                                                                                        <tr>
                                                                                            <td Width="90px">
                                                                                                <asp:Label ID="Label11" runat="server" Text="Selección" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                                            </td>
                                                                                            <td width="100px">
                                                                                                <asp:Label ID="Label7" runat="server" Text="Codigo" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                                            </td>
                                                                                            <td width="400px">
                                                                                                <asp:Label ID="Label8" runat="server" Text="Descripción" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Contenido">
                                                                                    <asp:Panel ID="Panel1" runat="server" Width="500px" Height=" 130px" ScrollBars="Vertical">
                                                                                        <asp:GridView ID="Gr_Bco" runat="server" Width="480px" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                                            ShowHeader="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Selección">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="BtnVerBanco" runat="server" ToolTip='<%# Eval("bco_Cod") %>'
                                                                                                            ImageUrl="~/Images/bt_ver.gif" onclick="BtnVerBanco_Click" 
                                                                                                            style="height: 13px" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="bco_Cod" HeaderText="bco_Cod">
                                                                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="bco_desc" HeaderText="bco_desc">
                                                                                                    <ItemStyle HorizontalAlign="Left" Width="385px" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
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
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <br />
                                                <%--**********Tabla Sucursal*******--%>
                                                <table id="Sucursal" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="Cabecera" align="left" style="text-align: -moz-left">
                                                            <asp:RadioButton ID="RB_Suc" runat="server" Text="Sucursal Banco" CssClass="SubTitulos"
                                                                AutoPostBack="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Contenido">
                                                            <table>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label4" runat="server" Text="Sucursal" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_ID_Suc" runat="server" CssClass="clsDisabled" Width="90px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label5" runat="server" Text="Descripción" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_Des_Suc" runat="server" CssClass="clsDisabled" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label6" runat="server" Text="Plaza" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="Drop_Plaza" runat="server" CssClass="clsDisabled">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="padding: 5px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                                                            <tr>
                                                                                <td class="Cabecera">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="500px" class="cabeceraGrilla">
                                                                                        <tr>
                                                                                            <td Width="90px">
                                                                                                <asp:Label ID="Label12" runat="server" Text="Selección" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                                            </td>
                                                                                            <td width="100px">
                                                                                                <asp:Label ID="Label9" runat="server" Text="Codigo" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                                            </td>
                                                                                            <td width="400px">
                                                                                                <asp:Label ID="Label10" runat="server" Text="Descripción" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Contenido" width="500px">
                                                                                    <asp:Panel ID="Panel2" runat="server" Width="500px" Height="130px" ScrollBars="Vertical">
                                                                                        <asp:GridView ID="Gr_Suc" runat="server" Width="480px" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                                            ShowHeader="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Selección">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="BtnVerSucursal" runat="server" ToolTip='<%# Eval("Cod_Suc") %>'
                                                                                                            ImageUrl="~/Images/bt_ver.gif" onclick="BtnVerSucursal_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="Cod_Suc" HeaderText="Cod_Suc">
                                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Desc_Suc" HeaderText="Desc_Suc">
                                                                                                    <ItemStyle HorizontalAlign="Left" Width="385px" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
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
                <%--*********Botonera***********--%>
                <tr>
                    <td align="right">
                        <asp:HiddenField ID="HF_PosSuc" runat="server" />
                        <asp:HiddenField ID="HF_IdSuc" runat="server" />
                        <asp:HiddenField ID="HF_PosBco" runat="server" />
                        <asp:HiddenField ID="HF_Idbco" runat="server" />
                        <asp:LinkButton ID="Link_Suc" runat="server"></asp:LinkButton><asp:LinkButton ID="Link_Bco"
                            runat="server"></asp:LinkButton><asp:ImageButton ID="IB_Nuevo" runat="server" AlternateText="Nuevo"
                                ImageUrl="../../../Imagenes/Botones/Boton_Nuevo_out.gif" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" Enabled="False" />
                        <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Eliminar_Out.gif"
                            AlternateText="Eliminar" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';" Enabled="False"
                            Visible="False" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" AlternateText="Guardar" ImageUrl="../../../Imagenes/Botones/Boton_Guardar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                            Enabled="False" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <%-- <Triggers>
          <asp:PostBackTrigger ControlID="Link_Bco" />
          
       </Triggers>--%></asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton></asp:Content>
