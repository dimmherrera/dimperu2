<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    CodeFile="Tipo_Cartera.aspx.vb" Inherits="Tipo_Cartera" Title="Mantenimiento Tipo Cartera" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript">

        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");

        }


    </script>

    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>

    <script src="../FuncionesPrivadasJS/Ciuddad_Comuna.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="1" class="Contenido">
                <tr>
                    <td align="center" style="text-align: -moz-center" class="Cabecera">
                        <asp:Label ID="Label1" runat="server" Text="Mantenimiento-Tipo de Cartera(Riesgo-Cliente)"
                            CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="height: 570px; width:100%; padding: 5px; text-align: -moz-center"
                        align="center" valign="top" width="100%">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera" align="left" >
                                    <asp:Label ID="Label2" runat="server" Text="Datos Tipos de Cartera" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido">
                                    <table>
                                        <tr>
                                            <td align="right" style="width: 152px">
                                                <asp:Label ID="Label3" runat="server" Text="Numero Cartera" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 882px" align="left">
                                                <asp:TextBox ID="txt_NroCartera" runat="server" CssClass="clsDisabled" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 152px">
                                                <asp:Label ID="Label4" runat="server" Text="Descripcion" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 882px" align="left">
                                                <asp:TextBox ID="txt_Des" runat="server" CssClass="clsDisabled" Width="400px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 152px">
                                                <asp:Label ID="Label5" runat="server" Text="Nro de dias Para Cobro" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 882px" align="left">
                                                <asp:TextBox ID="txt_NroDiasCobro" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    MaxLength="10"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txt_NroDiasCobro_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_NroDiasCobro">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 5px" align="center" >
                                                <table border="1" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" >
                                                        <div>
                                                            <table class="cabeceraGrilla" width="519px" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td Width="90px">
                                                                       <asp:Label ID="Label9" runat="server" Text="Selección" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td width="150px">
                                                                        <asp:Label ID="Label6" runat="server" Text="Numero Cartera" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td width="300px">
                                                                        <asp:Label ID="Label7" runat="server" Text="Descripcion" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td width="119px">
                                                                        <asp:Label ID="Label8" runat="server" Text="Numero Días" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                            <div id="GridViewDiv" style="overflow: auto; width: 520px; height: 150px" onscroll="DoScroll()">
                                                                <asp:GridView ID="Gr_Crt" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                    ShowHeader="false" Width="500px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Selección">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="Btn_ver" runat="server" ToolTip='<%# Eval("Nro") %>' 
                                                                                    ImageUrl="~/Images/bt_ver.gif" onclick="Btn_ver_Click" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Nro" HeaderText="Nro Cartera">
                                                                            <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Des" HeaderText="Descripcion">
                                                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="NDias" HeaderText="Nro dias ">
                                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                                                    <RowStyle CssClass="formatUltcell" />
                                                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 152px">
                                                <asp:HiddenField ID="HF_Idcrt" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 152px">
                                                <asp:LinkButton ID="LinkB_Id_crt" runat="server"></asp:LinkButton>
                                                <asp:HiddenField ID="HF_Posicion" runat="server" />
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
                        <asp:ImageButton ID="IB_Nuevo" runat="server" OnClick="IB_Nuevo_Click" ImageUrl="../../../Imagenes/Botones/Boton_Nuevo_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';"
                            AlternateText="Nuevo" />
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Guardar_Out.gif"
                            Enabled="False" onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" AlternateText="Guardar" />
                        <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Eliminar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';"
                            Enabled="False" AlternateText="Eliminar" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            AlternateText="Limpiar" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Link_Eliminar" runat="server"></asp:LinkButton>
</asp:Content>
