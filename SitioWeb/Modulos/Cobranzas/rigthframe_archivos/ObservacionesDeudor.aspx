<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ObservacionesDeudor.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_ObservacionesDeudor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Observación Pagador</title>
    <base target="_self"></base>
    
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>

    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
        
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
        <table cellpadding="0" cellspacing="1">
            <tr>
                <td>
                    <asp:label id="Titulo" runat="server" cssclass="Titulos" 
                        text="Observación Pagador"></asp:label>
                    
                </td>
            </tr>
            <tr>
                <td>
                
                    <table id="Table2" border="0" cellpadding="1" cellspacing="1" align="left" 
                        frame="border">
                        <tr>
                            <td align="left">
                               
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                               
                                    <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" 
                                        Text="Fecha Actualización"></asp:Label>
                                            </td>
                                            <td align="center">
                               
                                    <asp:TextBox ID="Txt_FechaAct" runat="server" CssClass="clsDisabled" 
                                        Height="22px" Width="100px" ReadOnly="True"></asp:TextBox>
                                                <cc1:CalendarExtender ID="Txt_FechaAct_CalendarExtender" runat="server" 
                                                    CssClass="radcalendar" Enabled="false" FirstDayOfWeek="Monday" 
                                                    Format="dd-MM-yyyy" TargetControlID="Txt_FechaAct" 
                                                    PopupPosition="BottomRight">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                               
                                    <asp:TextBox ID="Txt_Obs" runat="server" CssClass="clsMandatorio" 
                                        Height="189px" Width="281px" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                                    <br />
                            </td>
                        </tr>
                        </table>
                
                    
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="0" cellspacing="1">
                        <tr>
                            <td align="right" >
                        <asp:imagebutton id="IB_Guardar" runat="server" imageurl="~/Imagenes/Botones/boton_guardar_out.gif"
                            onclick=" IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" 
                                    ToolTip="Guardar" />
                        </td>
                            <td align="right" >
                    
                        <asp:imagebutton id="IB_Volver" runat="server" imageurl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" 
                            ToolTip="Volver" />
                    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
        <uc1:Mensaje ID="Mensaje1" runat="server" />
        
    </form>
    
</body>
</html>
