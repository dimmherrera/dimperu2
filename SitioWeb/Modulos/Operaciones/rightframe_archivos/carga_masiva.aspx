<%@ Page Language="VB" AutoEventWireup="false" CodeFile="carga_masiva.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_carga_masiva" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"></base>
    <title>Carga masiva de Documentos</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
        <script language=javascript src="../../../../FuncionesJS/Funciones.js"></script>
     <script language=javascript src="../../../../FuncionesJS/Grilla.js"></script>
     <script language=javascript  src="../../../FuncionesJS/Ventanas.js"></script>
     <script language=javascript src="../../../../FuncionesJS/Ajax.js"></script>
     <script language=javascript src="../../../FuncionesJS/PopCalendar.js"></script>
     <script language=javascript src="../../../../FuncionesJS/Excel.js"></script>
     <script language=javascript src="../FuncionesPrivadasJS/WFIngresoOperaciones.js"></script>
     <script language=javascript src="../FuncionesPrivadasJS/WFIngModDoctos.js"></script>

    <script language="javascript">
               
        function SelecionaDocto(Posicion) {
            window.document.forms[0].hf_posicion.value = Posicion;
            return;
        }

        function DoScroll() {
            var _gridView = document.getElementById("GridViewDiv");
            var _header = document.getElementById("HeaderDiv");
            _header.scrollLeft = _gridView.scrollLeft;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="500">
    </asp:ScriptManager>
    
    <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="6">
    
    </asp:Timer>
    <table width="1000" cellspacing="0">
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label12" runat="server" Text="Carga Masiva de Documentos" Visible="true"
                    CssClass="SubTitulos"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" height="500px" valign="top">
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ib_cargar" runat="server" ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_aceptar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_aceptar_in.gif';"
                                ToolTip="Cargar" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="HeaderDiv" style="overflow: hidden; width: 1150px;">
                            <table id="cabecera_grilla" runat="server" class="cabeceraGrilla">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label0" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_0" runat="server" Width="120px" AutoPostBack="True"
                                            CssClass="clsMandatorio">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_1" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_2" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_3" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_4" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_5" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_6" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_7" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="" Visible="false" CssClass="LabelCabeceraGrilla"
                                            Width="130"></asp:Label>
                                        <asp:DropDownList ID="Dr_campo_8" runat="server" Width="120px" Enabled="false" CssClass="clsDisabled"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="GridViewDiv" style="overflow: scroll; width: 1150px; height: 350px">
                            <asp:GridView ID="GridView1" runat="server" ShowHeader="False" CssClass="formatUltcell" PageSize="10000">
                                <HeaderStyle  CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" Width="140px" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="lb_archivo" runat="server" CssClass="Label"></asp:Label>
                <table class="style1">
                    <tr>
                        <td>
                            <asp:Label ID="lb_archivo0" runat="server" CssClass="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lb_archivo1" runat="server" CssClass="LabelError"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ImageButton ID="btn_apb" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';"
                            ToolTip="Aprobar Configuración" />
                        <asp:ImageButton ID="btn_guardar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                            ToolTip="Guardar Documentos" Enabled="False" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                            ToolTip="Limpiar Configuración" />
                        <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            ToolTip="Limpiar Configuración" />
                            
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <asp:HiddenField ID="HiddenField3" runat="server" />
                        <asp:HiddenField ID="HiddenField4" runat="server" />
                        <asp:HiddenField ID="HiddenField5" runat="server" />
                        <asp:HiddenField ID="HiddenField6" runat="server" />
                        <asp:HiddenField ID="HiddenField7" runat="server" />
                        <asp:HiddenField ID="HiddenField8" runat="server" />
                        <asp:HiddenField ID="HiddenField9" runat="server" />
                        <%--
                        <asp:HiddenField ID="HiddenField10" runat="server" />
                        <asp:HiddenField ID="HiddenField11" runat="server" />
                        <asp:HiddenField ID="HiddenField12" runat="server" />
                        <asp:HiddenField ID="HiddenField13" runat="server" />
                        --%>
                        <asp:HiddenField ID="HF_FecVtoReal" runat="server" />
                        <asp:HiddenField ID="HF_FEcVtoCal" runat="server" />
                     
                        <uc1:Mensaje ID="Mensaje1" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ib_cargar" />
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc2:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
      <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <uc2:Cargando ID="Cargando2" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
   
   
    </form>
</body>
</html>
