<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="Aplicaciones.aspx.vb" Inherits="Modulos_Carp_Aplicaciones" Title="Aplicaciones" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../../../WebControles/Cargando.ascx" TagName="Cargando" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
    <script src="../FuncionesPrivadasJS/Aplicaciones.js" type="text/javascript"></script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_Principal" >
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <%--<script type="text/javascript">
        //Reference of the GridView. 
        var TargetBaseControl = null;
        //Total no of checkboxes in a particular column inside the GridView.
        var CheckBoxes;
        //Total no of checked checkboxes in a particular column inside the GridView.
        var CheckedCheckBoxes;
        //Array of selected item's Ids.
        var SelectedItems;
        //Hidden field that will contain string of selected item's Ids separated by '|'.
        var SelectedValues;

        window.onload = function() {
            //Get reference of the GridView. 
            try {
                TargetBaseControl = document.getElementById('<%= GV_CxP.ClientID %>');
            }
            catch (err) {
                TargetBaseControl = null;
            }

            //Get total no of checkboxes in a particular column inside the GridView.
            try {
                CheckBoxes = parseInt('<%= GV_CxP.Rows.Count %>');
            }
            catch (err) {
                CheckBoxes = 0;
            }

            //Get total no of checked checkboxes in a particular column inside the GridView.
            CheckedCheckBoxes = 0;

            //Get hidden field that will contain string of selected item's Ids separated by '|'.
            SelectedValues = document.getElementById('<%= hdnFldSelectedValues.ClientID %>');

            //Get an array of selected item's Ids.
            if (SelectedValues.value == '')
                SelectedItems = new Array();
            else
                SelectedItems = SelectedValues.value.split('|');

            //Restore selected CheckBoxes' states.
            if (TargetBaseControl != null)
                RestoreState();
        }

        function HeaderClick(CheckBox) {
            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName('input');

            //Checked/Unchecked all the checkBoxes in side the GridView & 
            //modify selected items array.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf('chkBxSelect', 0) >= 0) {
                Inputs[n].checked = CheckBox.checked;
                if (CheckBox.checked)
                    SelectedItems.push(document.getElementById(Inputs[n].id.replace
			('chkBxSelect', 'hdnFldId')).value);
                else
                    DeleteItem(document.getElementById(Inputs[n].id.replace
			('chkBxSelect', 'hdnFldId')).value);
            }

            //Update Selected Values. 
            SelectedValues.value = SelectedItems.join('|');

            //Reset Counter
            CheckedCheckBoxes = CheckBox.checked ? CheckBoxes : 0;
        }

        function ChildClick(CheckBox, HCheckBox, Id) {
            //Modify Counter;            
            if (CheckBox.checked && CheckedCheckBoxes < CheckBoxes)
                CheckedCheckBoxes++;
            else if (CheckedCheckBoxes > 0)
                CheckedCheckBoxes--;

            //Change state of the header CheckBox.
            if (CheckedCheckBoxes < CheckBoxes)
                HCheckBox.checked = false;
            else if (CheckedCheckBoxes == CheckBoxes)
                HCheckBox.checked = true;

            //Modify selected items array.
            if (CheckBox.checked)
                SelectedItems.push(Id);
            else
                DeleteItem(Id);

            //Update Selected Values. 
            SelectedValues.value = SelectedItems.join('|');
        }

        function RestoreState() {
            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName('input');

            //Header CheckBox
            var HCheckBox = null;

            //Restore previous state of the all checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf('chkBxSelect', 0) >= 0)
                if (IsItemExists(document.getElementById(Inputs[n].id.replace
		('chkBxSelect', 'hdnFldId')).value) > -1) {
                Inputs[n].checked = true;
                CheckedCheckBoxes++;
            }
            else
                Inputs[n].checked = false;
            else if (Inputs[n].type == 'checkbox' &&
		Inputs[n].id.indexOf('chkBxHeader', 0) >= 0)
                HCheckBox = Inputs[n];

            //Change state of the header CheckBox.
            if (CheckedCheckBoxes < CheckBoxes)
                HCheckBox.checked = false;
            else if (CheckedCheckBoxes == CheckBoxes)
                HCheckBox.checked = true;
        }

        function DeleteItem(Text) {
            var n = IsItemExists(Text);
            if (n > -1)
                SelectedItems.splice(n, 1);
        }


        function IsItemExists(Text) {
            for (var n = 0; n < SelectedItems.length; ++n)
                if (SelectedItems[n] == Text)
                return n;

            return -1;
        }    </script>--%>
 
    <%--<script type="text/javascript">
        $("[id*=chkHeader_CxP]").live("click", function() {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function() {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
            SumaTotalCxP();
        });
        $("[id*=chkRow_CxP]").live("click", function() {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader_CxP]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow_CxP]", grid).length == $("[id*=chkRow_CxP]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
            SumaTotalCxP();
        });
        $("[id*=chkHeader_Exc]").live("click", function() {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function() {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
            SumaTotalExc();
        });
        $("[id*=chkRow_Exc]").live("click", function() {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader_Exc]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow_Exc]", grid).length == $("[id*=chkRow_Exc]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
            SumaTotalExc();
        });

        function SumaTotalExc() {
            var total = 0.0;

            $("#<%=GV_Excedentes.ClientID%> input[id*='chkRow_Exc']:checkbox").each(function(index) {
                if ($(this).is(':checked')) {
                    var tr = $(this).parent().parent();
                    var valor = $("td:eq(8)", tr).html();
                    
                    valor = truncaNUM(valor);
                    total += parseFloat(valor);
                }
            });
            $('#<%=Txt_Pag_Exc.ClientID%>').val(FormatMil(total.toString()));
            SumaTotal();
        }
//
        function SumaTotalCxP() {

            var total = 0.0;

            $("#<%=GV_CxP.ClientID%> input[id*='chkRow_CxP']:checkbox").each(function(index) {
                if ($(this).is(':checked')) {
                    var tr = $(this).parent().parent();
                    var valor = $("td:eq(5)", tr).html();
                    valor = truncaNUM(valor);
                    total += parseFloat(valor);
                }
            });
            //alert(total);
            //alert(FormatMil(total.toString()));
            $('#<%=Txt_Pag_Cxp.ClientID%>').val(FormatMil(total.toString()));
            SumaTotal();
        }

        function SumaTotal() {
            var totalCxP = 0;
            var totalExc = 0;
            var totalDNC = 0;
            var totalCxC = 0;
            var totalDoc = 0;
            var total = 0;
            var deuda = 0;
            var saldo = 0;

            totalCxP = parseFloat(truncaNUM($('#<%=Txt_Pag_Cxp.ClientID%>').val()));
            totalExc = parseFloat(truncaNUM($('#<%=Txt_Pag_Exc.ClientID%>').val()));
            totalDNC = parseFloat(truncaNUM($('#<%=Txt_Pag_Dnc.ClientID%>').val()));
            
            totalCxP = isNaN(totalCxP) ? 0 : parseFloat(totalCxP);
            totalExc = isNaN(totalExc) ? 0 : parseFloat(totalExc);
            totalDNC = isNaN(totalDNC) ? 0 : parseFloat(totalDNC);
            
            total = totalCxP + totalExc + totalDNC;
                        
            totalCxC = parseFloat(truncaNUM($('#<%=Txt_Pag_CxC.ClientID%>').val()));
            totalDoc = parseFloat(truncaNUM($('#<%=txt_total_doc.ClientID%>').val()));

            totalCxC = isNaN(totalCxC) ? 0 : parseFloat(totalCxC);
            totalDoc = isNaN(totalDoc) ? 0 : parseFloat(totalDoc);
            
            deuda = totalCxC + totalDoc;

            saldo = deuda - total;
            
//            alert(deuda);
//            alert(total);
            
            $('#<%=Txt_Total_A_Pagar.ClientID%>').val(FormatMil(total.toString()));
            $('#<%=Txt_Deuda_Total.ClientID%>').val(FormatMil(deuda.toString()));
            $('#<%=Txt_Saldo.ClientID%>').val(FormatMil(saldo.toString()));
            
            return;
	        
        }
    </script>--%>
	
	<asp:LinkButton ID="Lb_Banco" runat="server"></asp:LinkButton>
           	
    <asp:UpdatePanel ID="UP_Principal" runat="server">
        <ContentTemplate>
        
            <table cellspacing="1" cellpadding="0" width="100%" class="Contenido">
                <tr>
                    <td style="height: 31px" class="Cabecera">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Comercial - Aplicaciones"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" align="left" height="590" valign="top" style="padding: 3px">
                        <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td class="Cabecera" align="left">
                                    <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Cliente"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top" style="height: 40px; padding: 3px" align="left">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                        CssClass="clsMandatorio" Width="90px" __designer:wfdid="w286"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                        Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                    </cc2:MaskedEditExtender>
                                                    <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                        AutoPostBack="true" CssClass="clsMandatorio" Width="15px" __designer:wfdid="w286"
                                                        MaxLength="1"></asp:TextBox>
                                                    <cc2:FilteredTextBoxExtender ID="Txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                    </cc2:FilteredTextBoxExtender>
                                                    <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                        Width="20px" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                        Text="Razón Soc." Width="70px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                        ReadOnly="True" Style="position: static" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="1">
                                                    <asp:Label Style="position: static" ID="Label15" runat="server" CssClass="Label"
                                                        Text="Sucursal" __designer:wfdid="w290"></asp:Label>
                                                </td>
                                                <td align="left" colspan="1">
                                                    <asp:TextBox ID="Txt_Sucursal" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label Style="position: static" ID="Label14" runat="server" CssClass="Label"
                                                        Text="Ejecutivo" __designer:wfdid="w292"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txt_Ejecutivo" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <%--<asp:UpdatePanel ID="UP_Aplicacion" runat="server" UpdateMode="Always">
                            <ContentTemplate>--%>
                                <table id="Table1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Aplicación de Cuentas y Reserva"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" valign="top" style="height: 30px; padding: 3px" align="left">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:RadioButtonList ID="RB_Excedentes" runat="server" CellPadding="0" CellSpacing="0"
                                                                CssClass="Label" Width="220px">
                                                                <asp:ListItem Value="0">Reserva Liberados y Por Liberar</asp:ListItem>
                                                                <asp:ListItem Value="1" Selected="True">Reserva Liberados</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label41" runat="server" CssClass="Label" Font-Bold="False" Text="% Tasa Cliente"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 55px">
                                                            <asp:TextBox ID="Txt_Tasa_Cliente" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                                Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" style="width: 97px">
                                                            <asp:Label ID="Label42" runat="server" CssClass="Label" Font-Bold="False" Text="% Tasa Aplicar"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Tasa_Aplicar" runat="server" CssClass="clsTxt" Width="50px"
                                                                MaxLength="5"></asp:TextBox>
                                                            <cc2:FilteredTextBoxExtender ID="Txt_Tasa_Aplicar_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="Txt_Tasa_Aplicar"
                                                                ValidChars=",">
                                                            </cc2:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align="left">
                                                            <asp:ImageButton ID="IB_AplicarTasa" runat="server" ImageUrl="~/Imagenes/btn_workspace/Aplicar_out.gif"
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/Aplicar_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/Aplicar_out.gif';"
                                                                ToolTip="Ejecutar Tasa a Aplicar" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label50" runat="server" CssClass="Label" Font-Bold="False" Text="Fecha Aplicación"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Txt_Fecha_Aplicacion" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                            <cc2:MaskedEditExtender ID="Txt_Fecha_Aplicacion_MaskedEditExtender" runat="server"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fecha_Aplicacion"
                                                                Century="2000">
                                                            </cc2:MaskedEditExtender>
                                                            <cc2:CalendarExtender ID="Txt_Fecha_Aplicacion_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fecha_Aplicacion">
                                                            </cc2:CalendarExtender>
                                                        </td>
                                                        <td align="left">
                                                            <asp:ImageButton ID="IB_AplicarFecha" runat="server" ImageUrl="~/Imagenes/btn_workspace/Aplicar_out.gif"
                                                                onmouseover="this.src='../../../Imagenes/btn_workspace/Aplicar_in.gif';" onmouseout="this.src='../../../Imagenes/btn_workspace/Aplicar_out.gif';"
                                                                ToolTip="Ejecutar Fecha de Aplicación" />
                                                        </td>
                                                    </tr>
                                            </table>
                                            <%-- <asp:LinkButton ID="LinkButton1" runat="server" __designer:wfdid="w372" OnClick="Lb_buscar_Click"
                                                Style="position: static" TabIndex="54" ValidationGroup="Cliente"></asp:LinkButton>--%>
                                        </td>
                                    </tr>
                                </table>
                        <%--    </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <br />
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td valign="top">
                                    <cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                                        Height="300px" Width="1100px">
                                        <cc2:TabPanel runat="server" ID="TabPanel1" HeaderText="Totales">
                                           <ContentTemplate>
                                              <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                   <ContentTemplate>--%>
                                                       <table border="0" cellpadding="0" cellspacing="0">
                                                           <tr>
                                                               <td align="left">
                                                                   <asp:Label ID="Label43" runat="server" CssClass="Label" Font-Bold="True" Text="RESERVA"></asp:Label>
                                                               </td>
                                                               <td width="170">
                                                               </td>
                                                               <td align="left">
                                                                   <asp:Label ID="Label44" runat="server" CssClass="Label" Font-Bold="True" Text="CTAS. X COBRAR"></asp:Label>
                                                               </td>
                                                               <td>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Tot_Exc" runat="server" CssClass="clsTxt" Font-Bold="True" ReadOnly="True" Width="155px"></asp:TextBox>
                                                               </td>
                                                               <td align="left">
                                                                   <asp:TextBox ID="Txt_Pag_Exc" runat="server" CssClass="clsDisabled" Font-Bold="True" Width="155px" Enabled="false">0</asp:TextBox>
                                                               </td>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Tot_CxC" runat="server" CssClass="clsTxt" Font-Bold="True" ReadOnly="True" Width="155px"></asp:TextBox>
                                                               </td>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Pag_CxC" runat="server" CssClass="clsDisabled" Font-Bold="True" Enabled="false" Width="155px">0</asp:TextBox>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td align="left">
                                                                   <asp:Label ID="Label3" runat="server" CssClass="Label" Font-Bold="True" Text="CTAS. X PAGAR"></asp:Label>
                                                               </td>
                                                               <td width="150">
                                                               </td>
                                                               <td align="left" colspan="1">
                                                                   <asp:Label ID="Label4" runat="server" CssClass="Label" Font-Bold="True" Text="TODOS LOS DOCTOS."></asp:Label>
                                                               </td>
                                                               <td>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Tot_Cxp" runat="server" CssClass="clsTxt" Font-Bold="True" ReadOnly="True" Width="155px"></asp:TextBox>
                                                               </td>
                                                               <td align="left">
                                                                   <asp:TextBox ID="Txt_Pag_Cxp" runat="server" CssClass="clsDisabled" Font-Bold="True" Width="155px" Enabled="false">0</asp:TextBox>
                                                               </td>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Tot_Doc" runat="server" CssClass="clsTxt" Font-Bold="True" ReadOnly="True" Width="155px"></asp:TextBox>
                                                               </td>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Pag_Doc" runat="server" CssClass="clsDisabled" Font-Bold="True" Enabled="false" Width="155px">0</asp:TextBox>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td align="left">
                                                                   <asp:Label ID="Label5" runat="server" CssClass="Label" Font-Bold="True" Text="DOC. NO CEDIDOS"></asp:Label>
                                                               </td>
                                                               <td align="left" width="150">
                                                                   &nbsp;&nbsp;
                                                               </td>
                                                               <td align="left" colspan="2">
                                                                   &nbsp;&nbsp;
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td>
                                                                   <asp:TextBox ID="Txt_Tot_Dnc" runat="server" CssClass="clsTxt" Font-Bold="True" ReadOnly="True" Width="155px"></asp:TextBox>
                                                               </td>
                                                               <td align="left">
                                                                   <asp:TextBox ID="Txt_Pag_Dnc" runat="server" CssClass="clsDisabled" Font-Bold="True" Enabled="false" Width="155px">0</asp:TextBox>
                                                               </td>
                                                               <td>
                                                               </td>
                                                               <td>
                                                               </td>
                                                           </tr>
                                                       </table>
                                                       <br />
                                                       <table>
                                                           <tr>
                                                               <td valign="top">
                                                                   <table border="0" cellpadding="0" cellspacing="0" width="400px">
                                                                       <tr>
                                                                           <td class="Cabecera">
                                                                               <asp:Label ID="Label54" runat="server" CssClass="SubTitulos" Font-Bold="True" Text="Desembolso"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                       <tr>
                                                                           <td class="Contenido">
                                                                               <table border="0" cellpadding="0" cellspacing="0">
                                                                                   <tr>
                                                                                       <td>
                                                                                       </td>
                                                                                       <td>
                                                                                           <asp:CheckBox ID="CB_Sin_Devolucion" runat="server" AutoPostBack="True" CssClass="Label"
                                                                                               Enabled="False" Text="Sin Devolución" />
                                                                                           <asp:CheckBox ID="CB_Antes14" runat="server" CssClass="Label" Enabled="False" Text="Antes 14 Hrs. " />
                                                                                       </td>
                                                                                   </tr>
                                                                                   <tr>
                                                                                       <td align="right">
                                                                                           <asp:Label ID="Label46" runat="server" CssClass="Label" Font-Bold="False" Text="Tipo Egreso"></asp:Label>
                                                                                       </td>
                                                                                       <td>
                                                                                           <asp:DropDownList ID="DP_TipoEgreso" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                                               Enabled="False" Width="300px">
                                                                                           </asp:DropDownList>
                                                                                       </td>
                                                                                   </tr>
                                                                                   <tr>
                                                                                       <td align="right">
                                                                                           <asp:Label ID="Label47" runat="server" CssClass="Label" Font-Bold="False" Text="Banco"></asp:Label>
                                                                                       </td>
                                                                                       <td>
                                                                                           <asp:DropDownList ID="DP_Banco" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                                                                               Enabled="False" Width="300px">
                                                                                           </asp:DropDownList>
                                                                                       </td>
                                                                                       <td>
                                                                                       </td>
                                                                                   </tr>
                                                                                   <tr>
                                                                                       <td align="right">
                                                                                           <asp:Label ID="Label48" runat="server" CssClass="Label" Font-Bold="False" Text="Cuenta Corriente"></asp:Label>
                                                                                       </td>
                                                                                       <td>
                                                                                           <asp:TextBox ID="Txt_Cta_Cte" runat="server" CssClass="clsDisabled" MaxLength="35"
                                                                                               ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                       </td>
                                                                                       <td>
                                                                                       </td>
                                                                                   </tr>
                                                                               </table>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </td>
                                                               <td valign="top">
                                                                   <table border="0" cellpadding="0" cellspacing="0">
                                                                       <tr>
                                                                           <td class="Cabecera">
                                                                               <asp:Label ID="Label10" runat="server" CssClass="SubTitulos" Font-Bold="True" Text="Observación Aplicación"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                       <tr>
                                                                           <td class="Contenido">
                                                                               <table border="0" cellpadding="0" cellspacing="0">
                                                                                   <tr>
                                                                                       <td rowspan="3" valign="top">
                                                                                           <asp:TextBox ID="Txt_Observacion" runat="server" CssClass="clsDisabled" Height="69px"
                                                                                               ReadOnly="True" TextMode="MultiLine" Width="228px"></asp:TextBox>
                                                                                       </td>
                                                                                   </tr>
                                                                                   <tr>
                                                                                       <td>
                                                                                           &nbsp;&nbsp;
                                                                                       </td>
                                                                                   </tr>
                                                                                   <tr>
                                                                                       <td>
                                                                                           &nbsp;&nbsp;
                                                                                       </td>
                                                                                   </tr>
                                                                               </table>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </td>
                                                           </tr>
                                                       </table>
                                                 <%--  </ContentTemplate>
                                               </asp:UpdatePanel> --%>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="RSV">
                                            <ContentTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel_GV_Excedentes" runat="server" Width="1050px" Height="250px"
                                                                        ScrollBars="Auto">
                                                                        <asp:GridView ID="GV_Excedentes" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                            PageSize="1" AllowSorting="True">
                                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="IB_ExcTodos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif" OnClick="IB_ExcTodos_Click" />
                                                                                        <%--<asp:CheckBox ID="chkHeader_Exc" runat="server" toolTip="" />--%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="CB_Seleccionar_Exc" runat="server" AutoPostBack="True" OnCheckedChanged="CB_Seleccionar_Exc_CheckedChanged" />
                                                                                        <%--<asp:CheckBox ID="chkRow_Exc" runat="server" ToolTip=""  />--%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Deudor" HeaderText="Pagador">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="opo_otg" HeaderText="N° Otorg.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TD" HeaderText="TD">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_num" HeaderText="N° Docto.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="N° Cuota">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="excedente" HeaderText="Mto. A Pagar">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="excedente" HeaderText="Excedente" Visible="False">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_id_doc" runat="server" Text='<%#Eval("id_doc")%>'></asp:Label>
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
                                                                <td align="right">
                                                                    <asp:Label ID="Label59" runat="server" CssClass="Label" Text="Total RSV"></asp:Label><asp:TextBox
                                                                        ID="txt_total_exc" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &#160;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table width="350" cellpadding="0" cellspacing="0" border="1">
                                                                        <tr>
                                                                            <td class="Label" bgcolor="#CCFFCC" align="center">
                                                                                Rsv. Por Liberar
                                                                            </td>
                                                                            <td class="Label" bgcolor="#CC99FF" align="center">
                                                                                Aplic. Ctas. y Rsv.
                                                                            </td>
                                                                            <td class="Label" bgcolor="#FFCC99" align="center">
                                                                                Pago sin Procesar
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="CxP">
                                            <ContentTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel_GV_CxP" runat="server" Width="1050px" Height="250px" ScrollBars="Auto">
                                                                        <asp:GridView ID="GV_CxP" runat="server" CssClass="formatUltcell" PagerSettings-Position="Top" 
                                                                            AutoGenerateColumns="False" AllowPaging="True"
                                                                            PageSize="500" AllowSorting="True">
                                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%--<asp:CheckBox ID="chkHeader_CxP" runat="server" ToolTip="" />--%>
                                                                                        <asp:ImageButton ID="IB_Seleccion_CxP" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif" OnClick="IB_Seleccion_CxP_Click" />
                                                                                        <%--<asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />--%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%--<asp:CheckBox ID="chkRow_CxP" runat="server" ToolTip=""  CssClass="chkSelect" />--%>
                                                                                        <asp:CheckBox ID="CB_Seleccionar_CxP" runat="server" AutoPostBack="True" OnCheckedChanged="CB_Seleccionar_CxP_CheckedChanged" ToolTip="" />
                                                                                      <%--  <asp:CheckBox ID="chkBxSelect" runat="server" />
                                                                                        <asp:HiddenField ID="hdnFldId" runat="server" Value='<%# Eval("id_cxp") %>' />--%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cta.">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="id_cxp" HeaderText="N° Cta.">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxp_des" HeaderText="Descripción">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="270px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxp_mto" HeaderText="Monto">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxp_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Gen.">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_num" HeaderText="Nº Docto.">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                            <RowStyle CssClass="formatUltcell" />
                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                    <asp:HiddenField ID="hdnFldSelectedValues" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label58" runat="server" CssClass="Label" Text="Total  CxP"></asp:Label><asp:TextBox
                                                                        ID="txt_total_cxp" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &#160;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table width="250" cellpadding="0" cellspacing="0" border="1">
                                                                        <tr>
                                                                            <td class="Label" bgcolor="#CC99FF" align="center">
                                                                                Aplic. Ctas. y Rsv.
                                                                            </td>
                                                                            <td class="Label" bgcolor="#FFCC99" align="center">
                                                                                Pago sin Procesar
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel4" runat="server" HeaderText="DNC">
                                            <ContentTemplate>
                                               <%-- <asp:UpdatePanel ID="UP_DNC" runat="server">
                                                    <ContentTemplate>--%>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel_GV_DNC" runat="server" Width="1050px" Height="250px" ScrollBars="Auto">
                                                                        <asp:GridView ID="GV_DNC" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                            PageSize="1" AllowSorting="True">
                                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="IB_Seleccion_Dnc" runat="server" OnClick="IB_Seleccion_Dnc_Click"
                                                                                            ImageUrl="~/Imagenes/Iconos/check.gif" /></HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="CB_Seleccionar_DNC" runat="server" OnCheckedChanged="CB_Seleccionar_DNC_CheckedChanged"
                                                                                            AutoPostBack="true" /></ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="30" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Deudor" HeaderText="Pagador">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TD" HeaderText="TD">
                                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="nce_num_doc" HeaderText="N° Docto.">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Monto" HeaderText="Monto Docto.">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lb_id_nce" runat="server" Visible="false" Text='<%#Eval("id_nce")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="0px" />
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
                                                                <td align="right">
                                                                    <asp:Label ID="Label57" runat="server" CssClass="Label" Text="Total DNC"></asp:Label><asp:TextBox
                                                                        ID="txt_total_dnc" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &#160;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table width="250" cellpadding="0" cellspacing="0" border="1">
                                                                        <tr>
                                                                            <td class="Label" bgcolor="#CC99FF" align="center">
                                                                                Aplic. Ctas. y Rsv.
                                                                            </td>
                                                                            <td class="Label" bgcolor="#FFCC99" align="center">
                                                                                Pago sin Procesar
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel5" runat="server" HeaderText="CxC">
                                            <ContentTemplate>
                                                <%--<asp:UpdatePanel ID="UP_CxC" runat="server">
                                                    <ContentTemplate>--%>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel_GV_CxC" runat="server" Width="1050px" Height="250px" ScrollBars="Auto">
                                                                        <asp:GridView ID="GV_CxC" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                            PageSize="8" AllowSorting="True" ShowHeader="true" Width="900" AllowPaging="True">
                                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="id_fct" HeaderText="N° Factura">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cta.">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="id_cxc" HeaderText="N° Cta.">
                                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Descrip_Cta" HeaderText="Descripción">
                                                                                    <ItemStyle HorizontalAlign="center" Width="150" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                    <ItemStyle HorizontalAlign="center" Width="100" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxc_sal" HeaderText="Saldo">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="interes" HeaderText="Interés">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="" HeaderText="Monto Pagar">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cxc_ful_pgo" HeaderText="Fec.Ult.Pag." DataFormatString="{0:dd/MM/yyyy}"
                                                                                    NullDisplayText="01/01/1900">
                                                                                    <ItemStyle HorizontalAlign="center" Width="80" />
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
                                                                <td align="right">
                                                                    <asp:Label ID="Label56" runat="server" CssClass="Label" Text="Total CxC"></asp:Label><asp:TextBox
                                                                        ID="txt_total_cxc" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &#160;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table width="250" cellpadding="0" cellspacing="0" border="1">
                                                                        <tr>
                                                                            <td class="Label" bgcolor="#CC99FF" align="center">
                                                                                Aplic. Ctas. y Rsv.
                                                                            </td>
                                                                            <td class="Label" bgcolor="#FFCC99" align="center">
                                                                                Pago sin Procesar
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                  <%--  </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel6" runat="server" HeaderText="Documentos">
                                            <ContentTemplate>
                                      <%--          <asp:UpdatePanel ID="UP_DOC" runat="server">
                                                    <ContentTemplate>--%>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="Btn_Criterio" runat="server" Text="Documentos" CssClass="boton" Enabled="False" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label26" runat="server" Text="Ordenar Por:" CssClass="Label" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DP_Orden" runat="server" CssClass="clsMandatorio" AutoPostBack="True"
                                                                                    Visible="false">
                                                                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                                                    <asp:ListItem Value="1">NIT Pagador</asp:ListItem>
                                                                                    <asp:ListItem Value="2">N° Otorg.</asp:ListItem>
                                                                                    <asp:ListItem Value="3">N° Docto.</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Fecha Vcto.</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Saldo</asp:ListItem>
                                                                                    <asp:ListItem Value="6">Estado Docto.</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label27" runat="server" Text="Total a Pagar" CssClass="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel_Gr_Documentos" runat="server" Width="1050px" Height="200px"
                                                                        ScrollBars="Auto">
                                                                        <asp:GridView ID="Gr_Documentos" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                                                                            PageSize="8" AllowSorting="True" Width="1770px" ShowHeader="true" AllowPaging="True">
                                                                            <FooterStyle CssClass="cabeceraGrilla" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="deudor" HeaderText="Pagador">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TipoDoctoCorta" HeaderText="T.D.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="id_opn" HeaderText="N° Ope.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_num" HeaderText="N° Doc.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuo">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="doc_fev_rea" HeaderText="Fecha Vcto" DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="doc_sdo_cli" HeaderText="Saldo Cli.">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="doc_sdo_ddr" HeaderText="Saldo Deu.">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Interes" HeaderText="Interés">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="MontoPagar" HeaderText="Monto a Pagar">
                                                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="doc_ful_pgo" HeaderText="Fec.Ult.Pag." DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cco_num" HeaderText="Est. Cob.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cco_des" HeaderText="Descrip.Est.Cob.">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="EstadoDocto" HeaderText="Estado">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
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
                                                                <td>
                                                                    &#160;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label55" runat="server" CssClass="Label" Text="Total Documentos"></asp:Label><asp:TextBox
                                                                        ID="txt_total_doc" runat="server" CssClass="clsDisabled" Width="90px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table width="250" cellpadding="0" cellspacing="0" border="1">
                                                                        <tr>
                                                                            <td class="Label" bgcolor="#CC99FF" align="center">
                                                                                Aplic. Ctas. y Rsv.
                                                                            </td>
                                                                            <td class="Label" bgcolor="#FFCC99" align="center">
                                                                                Pago sin Procesar
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="Center">
                                                                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/Iconos/prev_out.png"
                                                                        onmouseover="this.src='../../../Imagenes/Iconos/prev_in.png';" onmouseout="this.src='../../../Imagenes/Iconos/prev_out.png';" /><asp:ImageButton
                                                                            ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/Iconos/next_out.png"
                                                                            onmouseover="this.src='../../../Imagenes/Iconos/next_in.png';" onmouseout="this.src='../../../Imagenes/Iconos/next_out.png';" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                  <%--  </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                    </cc2:TabContainer>
                                </td>
                                <td valign="middle" align="Center">
                                  <%-- <asp:UpdatePanel ID="UP_Totales" runat="server">
                                        <ContentTemplate>--%>
                                            <table border="0" cellpadding="0" cellspacing="3" class="Contenido" width="120px">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label7" runat="server" Text="TOTALES" CssClass="SubTitulos"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label8" runat="server" Text="Total a Pagar" CssClass="Label" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="Txt_Total_A_Pagar" runat="server" CssClass="clsDisabled" Enabled="false">0</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label9" runat="server" Text="Int. Devolver" CssClass="Label" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="Txt_Int_Devolver" runat="server" CssClass="clsDisabled" Enabled="false">0</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label51" runat="server" Text="Interés" CssClass="Label" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="Txt_Interes" runat="server" CssClass="clsDisabled" Enabled="false">0</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label52" runat="server" Text="Deuda Total" CssClass="Label" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="Txt_Deuda_Total" runat="server" CssClass="clsDisabled" Enabled="false">0</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label53" runat="server" Text="Saldo" CssClass="Label" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="Txt_Saldo" runat="server" CssClass="clsDisabled" Enabled="false">0</asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                  <%--      </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    <%--
                        <asp:UpdatePanel ID="UP_Botones" runat="server">
                            <ContentTemplate>--%>
                            <asp:ImageButton ID="IB_Buscar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/Boton_Buscar_out.gif" ToolTip="Buscar"></asp:ImageButton>
                            <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" runat="server"
                                ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" ToolTip="Guardar Aplicación"
                                Enabled="False"></asp:ImageButton>
                            <asp:ImageButton ID="IB_Aplicaciones" runat="server" ImageUrl="~/Imagenes/Botones/boton_ver_aplicaciones_out.gif"
                                onmouseover="this.src='../../../Imagenes/Botones/boton_ver_aplicaciones_in.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/boton_ver_aplicaciones_out.gif';"
                                ToolTip="Ver Aplicaciones" Enabled="False"></asp:ImageButton>
                            <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';"
                                ToolTip="Limpiar Pantalla de Aplicación"></asp:ImageButton>
                             <cc2:ConfirmButtonExtender ID="IB_Guardar_ConfirmButtonExtender" runat="server" ConfirmText="¿ Desea realizar aplicación ?" Enabled="True" TargetControlID="IB_Guardar">
                             </cc2:ConfirmButtonExtender>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="HF_Posicion" runat="server" />
            <asp:HiddenField ID="HF_Posicion_CxC" runat="server" />
            <asp:HiddenField ID="HF_Posicion_Exc" runat="server" />
            <asp:HiddenField ID="HF_Posicion_CxP" runat="server" />
            <asp:HiddenField ID="HF_Posicion_DNC" runat="server" />
            <asp:HiddenField ID="HF_Interes_CxC" runat="server" />
            <asp:HiddenField ID="HF_Interes_Doc" runat="server" />
            <asp:HiddenField ID="HF_NroAplicacion" runat="server" />
            
            <%--Criterio de Busqueda--%>
            <asp:LinkButton ID="LinkCriterio" runat="server"></asp:LinkButton>
            <cc2:ModalPopupExtender ID="MP_Criterio" runat="server" TargetControlID="LinkCriterio"
                EnableViewState="False" PopupControlID="Panel_Criterio" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="Panel_Criterio">
            </cc2:ModalPopupExtender>
            
            <asp:Panel ID="Panel_Criterio" runat="server" Width="800px" Height="200px" Style="display: none">
                <table class="Contenido" border="0" cellpadding="0" cellspacing="0" width="100%"
                    style="padding: 10px">
                    <tbody>
                        <tr>
                            <td align="center" class="Cabecera">
                                <asp:Label ID="Label6" runat="server" Text="Criterio de Busqueda" CssClass="Titulos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido">
                                <table>
                                    <tr>
                                        <td>
                                            <%--Deudor--%>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="CB_Deudor" runat="server" Text="Pagador+" CssClass="Label" AutoPostBack="true" />
                                                        <asp:TextBox ID="Txt_Rut_Deu" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                            ReadOnly="true" Style="position: static" TabIndex="1" Width="90px"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Deu_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Deu">
                                                        </cc2:MaskedEditExtender>
                                                        <asp:TextBox ID="Txt_Dig_Deu" runat="server" __designer:wfdid="w286" CssClass="clsDisabled"
                                                            ReadOnly="true" MaxLength="1" Style="position: static" TabIndex="1" Width="15px"
                                                            AutoPostBack="true">
                                                        </asp:TextBox>
                                                        <asp:ImageButton ID="IB_AyudaDeu" runat="server" ImageUrl="~/Imagenes/Iconos/155.ICO"
                                                            Width="20" Enabled="false" />
                                                        <asp:TextBox ID="Txt_Rso_Deu" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                            ReadOnly="True" Style="position: static" Width="280px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <%--Estado de Documentos--%>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Estados" CssClass="Label" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DP_Estados" runat="server" CssClass="clsTxt" Width="150">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label16" runat="server" Text="Tipo Docto." CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DP_TipoDocto" runat="server" CssClass="clsTxt" Width="100">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label17" runat="server" Text="N° Otorg." CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Nro_Oto" runat="server" CssClass="clsTxt" Width="90"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="Txt_Nro_Oto"
                                                            Mask="999,999,999,999" MaskType="Number" InputDirection="RightToLeft">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label18" runat="server" Text="N° Docto." CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Nro_Doc" runat="server" CssClass="clsTxt" Width="90"></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="999,999,999,999"
                                                            MaskType="Number" TargetControlID="Txt_Nro_Doc" InputDirection="RightToLeft">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label19" runat="server" Text="Est. Cob." CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                       <asp:DropDownList ID="DP_CodCobranza" runat="server" Width="300px" CssClass="clsTxt" />
                                                        <cc2:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="DP_CodCobranza"
                                                            PromptCssClass="Label" QueryPattern="Contains" PromptText="Escriba Para Buscar"
                                                            PromptPosition="Bottom" IsSorted="true">
                                                        </cc2:ListSearchExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="padding: 0 0px 0 5px">
                                            <%--Monto Doctos--%>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label20" runat="server" Text="Monto Doctos." CssClass="Label" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label21" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Mto_Dsd" runat="server" CssClass="clsTxt" Width="100"></asp:TextBox><cc2:MaskedEditExtender
                                                            ID="Txt_Mto_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                            TargetControlID="Txt_Mto_Dsd">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label22" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Mto_Hst" runat="server" CssClass="clsTxt" Width="100"></asp:TextBox><cc2:MaskedEditExtender
                                                            ID="Txt_Mto_Hst_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                            TargetControlID="Txt_Mto_Hst">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="padding: 0 0px 0 5px">
                                            <%--Fecha Vcto--%>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #000000;">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label23" runat="server" Text="Fecha Vcto." CssClass="Label" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label24" runat="server" Text="Desde" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fec_Dsd" runat="server" CssClass="clsTxt" Width="70"></asp:TextBox><cc2:CalendarExtender
                                                            ID="Txt_Fec_Dsd_CalendarExtender" runat="server" CssClass="radcalendar" Enabled="True"
                                                            FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Dsd">
                                                        </cc2:CalendarExtender>
                                                        <cc2:MaskedEditExtender ID="Txt_Fec_Dsd_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99-99-9999" MaskType="Date" TargetControlID="Txt_Fec_Dsd">
                                                        </cc2:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label25" runat="server" Text="Hasta" CssClass="Label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Fec_Hst" runat="server" CssClass="clsTxt" Width="70"></asp:TextBox><cc2:MaskedEditExtender
                                                            ID="Txt_Fec_Hst_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="Txt_Fec_Hst">
                                                        </cc2:MaskedEditExtender>
                                                        <cc2:CalendarExtender ID="Txt_Fec_Hst_CalendarExtender" runat="server" CssClass="radcalendar"
                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="Txt_Fec_Hst">
                                                        </cc2:CalendarExtender>
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
                                <asp:ImageButton ID="btn_acep" OnClick="btn_acep_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_Out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Aceptar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Aceptar_in.gif';">
                                </asp:ImageButton>
                                <asp:ImageButton ID="btn_canc" OnClick="btn_canc_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Cancelar_Out.gif"
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Cancelar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Cancelar_in.gif';">
                                </asp:ImageButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            
      </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="UpdatePanel_Modales" runat="server">
        <ContentTemplate>
            <%--Grilla con pagos sin procesar--%>
            <asp:LinkButton ID="LinkPagos" runat="server"></asp:LinkButton>
            <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkPagos"
                EnableViewState="False" PopupControlID="Panel_PagosAnt" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="Panel_PagosAnt">
            </cc2:ModalPopupExtender>
            <asp:Panel ID="Panel_PagosAnt" runat="server" Style="display: none">
                <table id="nn" class="Contenido">
                    <tr>
                        <td>
                            <asp:GridView ID="GridPagos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="formatUltcell" PageSize="1" ShowHeader="true">
                                <FooterStyle CssClass="cabeceraGrilla" />
                                <Columns>
                                    <asp:BoundField DataField="id_doc" HeaderText="N° Docto.">
                                        <ItemStyle HorizontalAlign="Right" Width="80" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_cxc" HeaderText="N° cxc">
                                        <ItemStyle HorizontalAlign="Right" Width="80" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Estado" HeaderText="Est. Ingreso">
                                        <ItemStyle HorizontalAlign="center" Width="80" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ing_fec" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec. Ingr">
                                        <ItemStyle HorizontalAlign="center" Width="80" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ing_mto_tot" DataFormatString="{0:###,###,###}" HeaderText="Monto">
                                        <ItemStyle HorizontalAlign="Right" Width="150" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Modulo" HeaderText="Modulo">
                                        <ItemStyle HorizontalAlign="center" Width="100" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ejecutivo" HeaderText="Recaudor">
                                        <ItemStyle HorizontalAlign="center" Width="300" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="cabeceraGrilla" />
                                <RowStyle CssClass="formatUltcell" />
                                <AlternatingRowStyle CssClass="formatUltcellAlt" />
                            </asp:GridView>
                            <asp:ImageButton ID="ImageButton1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="IB_Imprir_Pagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Imprimir_Out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Imprimir_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Imprimir_in.gif';" />
                            <asp:ImageButton ID="IB_Cerrar_Pagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
    
</asp:Content>
