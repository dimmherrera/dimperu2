<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Int_masiva_ddr.aspx.vb" Inherits="Int_masiva_ddr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carga Masiva</title>
    <base target="_self" />
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
    </style>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="500">
    </asp:ScriptManager>

    
    <table width="1000" cellspacing="0">
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label12" runat="server" Text="Carga Masiva de Documentos y Deudores"
                    CssClass="SubTitulos"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" height="500px" valign="top">
                <table>
                    <tr>
                        <td>
                            <table class="style1">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Moneda"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="Dr_moneda" runat="server" CssClass="clsMandatorio">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="%Anticipo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="clsMandatorio" Width="50px"></asp:TextBox>
                                        
                                        
                                        <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" 
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            Mask="99,99" MaskType="Number" TargetControlID="TextBox1">
                                        </cc1:MaskedEditExtender>
                                        
                                        
                                    </td>
                                    <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </td>
                                </tr>
                                </table>
                            
                        </td>
                    </tr>
                    </table>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                         <div id="HeaderDiv" style="overflow: hidden; width: 1150px;">
    <table id="cabecera_grilla" runat="server" class="cabeceraGrilla" width="1430px">
        <tr>
            <td>
                <asp:Label ID="Label0" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_0" runat="server" Width="130px" 
                    AutoPostBack="True" CssClass="clsMandatorio">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label1" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_1" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label2" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_2" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label3" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_3" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label4" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_4" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label5" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_5" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label6" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_6" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label7" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_7" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label8" runat="server" Text="" Visible=True CssClass="LabelCabeceraGrilla" Width="130"></asp:Label>
                <asp:DropDownList ID="Dr_campo_8" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label9" runat="server" Text="" Visible=false 
                    CssClass="LabelCabeceraGrilla"></asp:Label>
                <asp:DropDownList ID="Dr_campo_9" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="Label10" runat="server" Visible=False CssClass="LabelCabeceraGrilla" 
                    Width="130px"></asp:Label>
                <asp:DropDownList ID="Dr_campo_10" runat="server" Width="130px" Enabled=false CssClass = "clsDisabled"  AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </div>                                      
                    <div id="GridViewDiv" style="overflow: scroll; width: 1150px; height: 350px" onscroll="DoScroll()">
                        <asp:GridView ID="GridView1" runat="server" ShowHeader="False" CssClass="formatUltcell"
                            AllowPaging="True" PageSize="200">
                            <RowStyle Width="130px" />
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
                            <asp:Label ID="lb_archivo1" runat="server" CssClass="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
    <asp:HiddenField ID="Hf_tipo_docto" runat="server" />
   
   
   
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
    
                <asp:ImageButton ID="ib_cargar" runat="server" 
                    ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif" ToolTip="Cargar" />
                <asp:ImageButton ID="btn_apb" runat="server"
                    ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" 
                    ToolTip="Aprobar Configuración" />
                    <asp:ImageButton ID="btn_guardar" runat="server" 
                        ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" 
                        ToolTip="Guardar Documentos" Enabled="False" />
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" 
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
                
    <asp:HiddenField ID="HiddenField10" runat="server" />
    <asp:HiddenField ID="HiddenField11" runat="server" />
    <asp:HiddenField ID="HiddenField12" runat="server" />
   
   
   
                              <asp:updateprogress associatedupdatepanelid="updatepanel1" id="updateProgress" runat="server">

    <progresstemplate>

    <div id="progressBackgroundFilter"  style="
  position:absolute; 
  top:0px; 
  bottom:0px; 
  left:0px; 
  right:0px; 
  overflow:hidden; 
  padding:0; 
  margin:0; 
 /* background-color:#000;  
  filter:alpha(opacity=50); 
  opacity:0.5; 
  z-index:1000; */"></div>
        <div id="processMessage" runat="server" style="position: absolute; top: 40%; left: 45%;
            height: 120px; width: 120px; text-align: center; background-color: #FFFFFF;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label_docs" runat="server" Text="Procesando" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <img alt="Loading" src="../../../Imagenes/Iconos/Procesar.gif" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
                                                            
    </progresstemplate>

</asp:updateprogress> 
 
            <uc1:Mensaje ID="Mensaje1" runat="server" />
 
    </ContentTemplate>
                  
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ib_cargar" />
                    </Triggers>
    </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
