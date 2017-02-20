<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionesAnteriores.aspx.vb" Inherits="Modulos_Prorrogas_rightframe_archivos_Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target=_self /> 
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 501px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <fieldset>
                              <table cellspacing="0">
               <tr>
                   <td class="Cabecera">
                                                <asp:Label ID="Label78" runat="server" Text="Gestiones Anteriores" 
                                                    CssClass="SubTitulos"></asp:Label>
                                            </td>
               </tr>
               <tr>
                   <td class="Contenido">
                                            <table>
                        <tr>
                            <td class="style2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td width="100px">
                                                        <asp:Label ID="Label67" runat="server" CssClass="Label" Text="Fecha Gestión"></asp:Label>
                                                    </td>
                                                    <td width="100px">
                                                        <asp:Label ID="Label69" runat="server" CssClass="Label" Text="Hora Gestión"></asp:Label>
                                                    </td>
                                                    <td width="150px">
                                                        <asp:Label ID="Label72" runat="server" CssClass="Label" Text="Cobrador Telefónico"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="HD_Item" runat="server" />
                                                    </td>
                                                </trt>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="Txt_GesAnteriores" runat="server" Width="400" autocomplete="off"
                                                CssClass="clsTxt" ReadOnly="true" /><br />
                                            <asp:Panel ID="PanelGestiones" runat="server" CssClass="popupControl">
                                                <div style="border: 1px outset white; width: 400px">
                                                    <asp:RadioButtonList ID="RB_GesAnteriores" runat="server" AutoPostBack="true" CssClass="Label">
                                                    </asp:RadioButtonList>
                                                </div>
                                            </asp:Panel>
                                            <cc1:PopupControlExtender ID="PCExt_GesAnteriores" runat="server" TargetControlID="Txt_GesAnteriores"
                                                PopupControlID="PanelGestiones" CommitProperty="value" Position="Bottom" CommitScript="e.value" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                               <%-- <div style="border-style: solid; border-width: thin; overflow: auto; height: 300px;
                                    width: 450px;">--%>
                                    <!-- TABLA DATOS GESTION ANTERIOR-->
                                    <table border="0" cellpadding="0" cellspacing="0" width="420px">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label64" runat="server" Text="Datos de Gestión" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="left" valign="top">
                                            <td class="Contenido">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <!--Datos Fechas-->
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="Label65" runat="server" CssClass="Label" Text="Fecha Pago"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAFechaPago" runat="server" Width="90px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="Label66" runat="server" CssClass="Label" Text="Hora Pago Desde"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAHoraPagodde" runat="server" Width="50px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td width="50px">
                                                                        <asp:Label ID="Label68" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAHoraPagoHta" runat="server" Width="50px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="Label70" runat="server" CssClass="Label" Text="Fec.Prox.Gestión"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAFechaProxGestion" runat="server" Width="90px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td width="50px">
                                                                        <asp:Label ID="Label71" runat="server" CssClass="Label" Text="Hora"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAHoraGestion" runat="server" Width="50px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <!--Codigo Cobranza-->
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="Label75" runat="server" CssClass="Label" Text="Est.Cobranza"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GACodCobranza" runat="server" Width="50px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                        <asp:TextBox ID="txt_GADesCodCobranza" runat="server" Width="210px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <!--Datos Ciudad y Comuna-->
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="Label76" runat="server" CssClass="Label" Text="Ciudad"></asp:Label>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:TextBox ID="txt_GACiudad" runat="server" Width="100px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td width="50px">
                                                                        <asp:Label ID="Label77" runat="server" CssClass="Label" Text="Comuna"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAComuna" runat="server" Width="100px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <!--Datos Zona-->
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="LabelZona" runat="server" CssClass="Label" Text="Zona"></asp:Label>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:TextBox ID="txt_GAZona" runat="server" Width="100px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <!--Datos Direccion-->
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="LabelDirecc" runat="server" CssClass="Label" Text="Dirección"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_GAdireccion" runat="server" Width="280px" CssClass="clsDisabled"
                                                                            ReadOnly="true" /><br />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <!--Datos Documentos y Observación-->
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="Labeldocnec" runat="server" CssClass="Label" Text="Doc.Necesaria Ret.Pago"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GADoctoNec" runat="server" Width="280px" Height="51px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Labelobs" runat="server" CssClass="Label" Text="Observación"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAObservacion" runat="server" Width="280px" Height="51px" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td width="110px">
                                                                        <asp:Label ID="LabelAlaOrden" runat="server" CssClass="Label" Text="A la Orden"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_GAAlaOrden" runat="server" Width="280" CssClass="clsDisabled"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- TABLA FIN DATOS GESTION ANTERIOR-->
                               <%-- </div>--%>
                            </td>
                        </tr>
                    </table>
                   
                      </td>
               </tr>
           </table> 

                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel> 
  

  
    </form>
</body>
</html>
