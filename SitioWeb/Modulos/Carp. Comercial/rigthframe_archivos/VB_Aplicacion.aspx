<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="VB_Aplicacion.aspx.vb" Inherits="VB_Aplicacion" title="Visto Bueno de Aplicaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />

    <script src="../FuncionesPrivadasJS/Aplicaciones.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table cellspacing="1" cellpadding="0" width="100%" class="Contenido">
        <tr>
            <td style="height: 31px" class = "Cabecera" align="center" valign="middle">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Control Dual - Visto Bueno de Aplicaciones"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" align="center" height="590" valign="top" style="padding: 5px;text-align:-moz-center">
                <table border="0" cellpadding="0" cellspacing="5">
                    <tr>
                        <td>
                            <table id="tb_Aplicacion" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Fecha Solicitud"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fecha_Desde" runat="server" CssClass="clsMandatorio" Width="90px" AutoPostBack="True"></asp:TextBox>
                                                    
                                                    <cc2:calendarextender id="txt_fec_des_CalendarExtender" runat="server" enabled="True" Format="dd-MM-yyyy"
                                                        targetcontrolid="Txt_Fecha_Desde" cssclass="radcalendar" firstdayofweek="Monday">
                                                    </cc2:calendarextender>
                                                    
                                                    <cc2:maskededitextender id="MaskedEditExtender6" runat="server" targetcontrolid="Txt_Fecha_Desde"
                                                        mask="99/99/9999" userdateformat="DayMonthYear" masktype="Date">
                                                    </cc2:maskededitextender>
                                                    
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Fecha_Hasta" runat="server" CssClass="clsMandatorio" 
                                                        Width="90px" AutoPostBack="True"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_Fecha_Hasta_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Hasta">
                                                    </cc2:MaskedEditExtender>
                                                    <cc2:CalendarExtender ID="Txt_Fecha_Hasta_CalendarExtender" runat="server" Enabled="True"
                                                        FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Hasta" CssClass="radcalendar">
                                                    </cc2:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table id="tb_Ejecutivos" border="0" cellpadding="0" cellspacing="0" 
                    visible="false" runat="server">
                    <tr>
                        <td align="left" class="Cabecera">
                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Ejecutivo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="Contenido" style="height: 40px; padding: 3px" 
                            valign="top">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <asp:CheckBox ID="CB_TodosEje" runat="server" AutoPostBack="True" 
                                            Checked="True" CssClass="Label" Text="Todos" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DP_Ejecutivos" runat="server" CssClass="clsDisabled" 
                                            Enabled="false" Width="250px">
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
                <br />
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel_GV_Aplicaciones" runat="server" width="1200px" height="420px" ScrollBars="Horizontal">
                            
                                <asp:GridView ID="GV_Aplicaciones" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                    AllowSorting="True" Width="1380px">
                                    <FooterStyle CssClass="cabeceraGrilla" />
                                    <Columns>
                                     <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90px" >
                                        <ItemTemplate>
                                          <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" 
                                                OnClick="Button1_Click" ToolTip='<%# Eval("Nro_Apli") %>' Height="16px" />
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle HorizontalAlign="Center" Width="90px" />
                                      </asp:TemplateField>
                                        <asp:BoundField DataField="Rut" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Right"  Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Razón Social Cliente">
                                            <ItemStyle HorizontalAlign="Left" Width="250" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="apl_fec" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="center" Width="80" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ejecutivo" HeaderText="Ejecutivo">
                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_Exc" HeaderText="Monto Rsv.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_DNC" HeaderText="Monto DNC.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_CXP" HeaderText="Monto CXP.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_CXC" HeaderText="Monto CXC.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto_DVG" HeaderText="Monto DOC.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tasa_Cli" HeaderText="Tasa Cli.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tasa_Apli" HeaderText="Tasa Apli.">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Devuelto" HeaderText="Devolver">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Observacion" HeaderText="Observación">
                                            <ItemStyle HorizontalAlign="Right" Width="200" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nro_Apli" HeaderText="Aplicación">
                                            <ItemStyle HorizontalAlign="Right" Width="0" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lb_apb_com" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                             onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                             onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                             onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                    </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table width="350" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td class="Label" bgcolor="#CCFFCC" align="center">
                                        Aprobada
                                    </td>
                                    <td class="Label" bgcolor="#FF9999" align="center">
                                        Rechazada
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
            
                <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                     onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" runat="server"
                     ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" AlternateText="Buscar Clientes"
                     ToolTip="Buscar"></asp:ImageButton>  
                     
                <asp:ImageButton ID="IB_Aprobar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';"
                     onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" runat="server"
                     ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" AlternateText="Buscar Clientes"
                     ToolTip="Aprobar Aplicación" Enabled="False"></asp:ImageButton>
                
                <asp:ImageButton ID="IB_Rechazar" onmouseover="this.src='../../../Imagenes/Botones/boton_rechazar_in.gif';"
                     onmouseout="this.src='../../../Imagenes/Botones/boton_rechazar_out.gif';" runat="server"
                     ImageUrl="~/Imagenes/Botones/boton_rechazar_out.gif" AlternateText="Buscar Clientes"
                     ToolTip="Rechazar Aplicación" Enabled="False"></asp:ImageButton>        
                
                
                <asp:ImageButton ID="IB_Informe" runat="server" AlternateText="Ver informe" 
                    Enabled="False" ImageUrl="~/Imagenes/Botones/boton_imprimir_out.gif" 
                    onmouseout="this.src='../../../Imagenes/Botones/boton_imprimir_out.gif';" 
                    onmouseover="this.src='../../../Imagenes/Botones/boton_imprimir_in.gif';" 
                    ToolTip="Ver informe" />
                
                
                <asp:ImageButton ID="IB_Limpiar" runat="server" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                     onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                      ToolTip="Limpiar"  />
            </td>
            
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    
    
    <asp:HiddenField ID="HF_NroAplicacion" runat="server" />    
    <asp:HiddenField ID="HF_CLI" runat="server" />    
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="IB_Aprobar" />
        <asp:PostBackTrigger ControlID="IB_Rechazar" />
        <asp:PostBackTrigger ControlID="IB_Informe" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Lb_informe" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="LB_Refrescar" runat="server"></asp:LinkButton>
   
</asp:Content>

