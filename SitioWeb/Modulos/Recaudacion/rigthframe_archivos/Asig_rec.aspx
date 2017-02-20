<%@ Page Title="Asignación Recaudador" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Asig_rec.aspx.vb" Inherits="Modulos_Recaudacion_rigthframe_archivos_Asig_rec" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
              <%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table style="width: 100%; height: 570px; text-align:-moz-center" class="Contenido">
        <tr>
            <td class = "Cabecera" align="center" 
                style="width: 100%; text-align:-moz-center;height:31px">
                <asp:Label ID="Label10" runat="server" CssClass="Titulos" 
                    Text="Recaudacion-Asignación Recaudador"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" height="590" valign="top" style="padding: 5px; width:100%; text-align:-moz-center" align="center" >
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="Cabecera" style="width: 900px;" align="left" >
                            <asp:Label ID="Label17" runat="server" CssClass="SubTitulos" 
                                Text="Datos de Recaudación"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" style="width: 900px" align="left" >
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Sucursales"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ch_suc" runat="server" CssClass="Label" Text="Todas" 
                                            Width="200px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="Label" 
                                            Text="Fecha de Generación"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fec" runat="server" AutoPostBack="True" 
                                            CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txt_fec_CalendarExtender" runat="server" 
                                            CssClass="radcalendar" Enabled="True" Format="dd-MM-yyyy" 
                                            TargetControlID="txt_fec" FirstDayOfWeek="Monday">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="txt_fec_MaskedEditExtender" runat="server" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fec">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Horario"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RB_HORA" runat="server" CssClass="Label" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="A">AM</asp:ListItem>
                                            <asp:ListItem Value="P">PM</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Zonas"></asp:Label>
                                    </td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="dr_zona" runat="server" AutoPostBack="True" 
                                                        CssClass="clsMandatorio" Width="220px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rb_zona" runat="server" AutoPostBack="True" Checked="True" 
                                                        CssClass="Label" Text="Todas" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Recaudador" CssClass="Label"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="Drop_Recaudador" runat="server" CssClass="clsMandatorio" Width="280px" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" style="width: 900px">
                    <tr>
                        <td class="Cabecera" align="left" >
                            <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Recaudadores"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" class="Contenido">
                            <br />
                            <table  cellpadding="0" cellspacing="0">
                                 <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" Height="350px" ScrollBars="None" >
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                ShowHeader="true"  CssClass="formatUltcell">
                                                <Columns>
                                                    <asp:BoundField DataField="suc_des_cra" HeaderText="Sucursal">
                                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="id_eje" HeaderText="Código">
                                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="eje_nom" HeaderText="Recaudadores">
                                                    <ItemStyle Width="250px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="deudores" HeaderText="Cant.Pagadores">
                                                    <ItemStyle Width="120px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="documentos" HeaderText="Cant.Documentos">
                                                    <ItemStyle Width="120px" HorizontalAlign="Right" />
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
                                        <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                            AlternateText="Anterior" />
                                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                            AlternateText="Siguiente" />
                                    </td>
                                </tr>
                            </table>
                            
                            <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>
                            
                        </td>
                    </tr>
                </table>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btn_buscar" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_buscar_in.gif';" 
                                        ToolTip="Buscar" ValidationGroup="ingreso" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_gest_hoy" runat="server" 
                                         ImageUrl="~/Imagenes/Botones/btn_ges_out.png" 
                                        onmouseout="this.src='../../../Imagenes/Botones/btn_ges_out.png';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/btn_ges_in.png';" 
                                        ToolTip="Gestión de Hoy" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_asig_esp" runat="server" 
                                          ImageUrl="~/Imagenes/Botones/asig_esp_out.png" 
                                        onmouseout="this.src='../../../Imagenes/Botones/asig_esp_out.png';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/asig_esp_in.png';"
                                        ToolTip="Asignación Especial" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="btn_limpiar" runat="server" 
                                        ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" 
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </td>
        </tr>
    </table>
    
  
    
       <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updatepanel1">

<ProgressTemplate>

       
</ProgressTemplate>

</asp:UpdateProgress>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_asig_esp" />
            <asp:PostBackTrigger ControlID="btn_gest_hoy" />
        </Triggers>
        
         
        
    </asp:UpdatePanel>
</asp:Content>

