<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AyudaNom.aspx.vb" Inherits="Modulos_Ayudas_AyudaNom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ayuda Nomina</title>
    <base target="_self"></base>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../FuncionesJS/Ventanas.js"></script>
    <script src="FuncionesPrivadasJS/AyudaNomina.js" type="text/javascript"></script>

    
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
            width: 40px;
        }
        .style4
        {
            width: 208px;
        }
        #Table2
        {
            width: 398px;
        }
    </style>

    
</head>
<body leftmargin="15" topmargin="20">
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table2" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="15" class="Cabecera">
                        <asp:Label ID="Label3" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="Contenido">
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0"  style="width: 100%;">
                            <tr >
                                <td align="right" style="width:100px">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Fecha nomina"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_FDesde" runat="server" CssClass="clsTxt" Width="90px" 
                                        MaxLength="10"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="txt_FDesde_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" MaskType="Date" 
                                        TargetControlID="txt_FDesde">
                                    </cc1:MaskedEditExtender>
                                    <cc1:CalendarExtender ID="txt_FDesde_CalendarExtender" runat="server" 
                                        CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" 
                                        Format="dd-MM-yyyy" TargetControlID="txt_FDesde">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <br />  
                        <div style="overflow: auto; height: 190px; position: static;" align="center" 
                            class="Contenido">
                            <asp:GridView ID="GV_Nomina" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                            EnableSortingAndPagingCallbacks="True" EnableTheming="True" Width="95%" 
                                PageSize="8" Height="99px">
                            <Columns>
                                <asp:BoundField DataField="id_nma" HeaderText="NUMERO">
                                    <ItemStyle Width="90px"  HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nma_mto" DataFormatString="{0:###,###,##0}" HeaderText="MONTO" 
                                    ConvertEmptyStringToNull="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" 
                                            ToolTip='<%# Eval("id_nma") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                        </asp:GridView>
                        </div>
                        
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="IB_Prev" runat="server" 
                            ImageUrl="~/Imagenes/btn_workspace/flecha_izq_out.gif" 
                            onmouseout="this.src='../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                            onmouseover="this.src='../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                        <asp:ImageButton ID="IB_Next" runat="server" 
                            ImageUrl="~/Imagenes/btn_workspace/flecha_der_out.gif" 
                            onmouseout="this.src='../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                            onmouseover="this.src='../../Imagenes/btn_workspace/flecha_der_in.gif';"/>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="50" valign="bottom">
                        <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif"
                            OnClick="IB_Buscar_Click" onmouseout="this.src='../../Imagenes/Botones/Boton_buscar_out.gif';"
                            onmouseover="this.src='../../Imagenes/Botones/Boton_Buscar_in.gif';" ToolTip="Buscar Clientes" />
                        <a href="javascript:window.close();">
                            <img src="../../Imagenes/Botones/Boton_Volver_Out.gif" onmouseout="this.src='../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                onmouseover="this.src='../../Imagenes/Botones/Boton_Volver_In.gif';" 
                            border="0" alt="Volver" />
                        </a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
    
</body>
</html>
