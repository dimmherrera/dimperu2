function ClickOperacion(pTabla, pClass, jClass, sClass) {

    
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].HF_PosNeg.value = '';
            window.document.forms[0].HF_NroOpe.value = '';
            window.document.forms[0].HF_NroNeg.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var rut = event.srcElement.parentElement.cells(1).innerText;
        var ope = event.srcElement.parentElement.cells(0).innerText;
        var neg = event.srcElement.parentElement.cells(9).innerText;

        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_RutCli.value = rut;
        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_PosNeg.value = pos;
        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroOpe.value = ope;
        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroNeg.value = neg;
        
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$Lb_buscar_ccf', '');
        //__doPostBack('ctl00$ContentPlaceHolder1$TabContainer1$TabPanel2$ResumenOperacion1$LB_Refrescar', '');
        return;
    }
}

function VerNegociacion()
{
 
  var Neg = window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroNeg.value;
   if (Neg != '')
   {
       //      var x = window.showModalDialog('../../Carp. Comercial/rigthframe_archivos/PopUpNegociacion.aspx?Pizarra=1&nro=' + Neg, 'PopUpNeg', 'scroll:yes;status:off;dialogWidth:1280px;dialogHeight:1000px;dialogLeft:0px;dialogTop:0px');
       //var x = window.open('../../Carp. Comercial/rigthframe_archivos/PopUpNegociacion.aspx?Pizarra=1&nro=' + Neg, 'PopUpNeg', 'alwaysraised=yes,scrollbars=yes,menubar=no,scrolbar=0,toolbar=0,status=no,width=1280,height=1000,left=0,top=0');
       var x = window.open('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx?Nro=' + Neg + "&Inf=1", 'PopUpNeg', 'alwaysraised=yes,scrollbars=yes,menubar=no,scrolbar=0,toolbar=0,status=no,width=100,height=100,left=450,top=400');
   }
   else
   {
      // alert('Selecione una Operación para ver su Negociación');
       __doPostBack('ctl00$ContentPlaceHolder1$Lb_MJ', '');
   }
   
   return;
    
}

function VerEvaluacion()
{

   var Eva = window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroEva.value;
  
   if (Eva != '')
   {
       //var x = window.open('../../Carp. Comercial/rigthframe_archivos/Reporte_EvaluacionCliDeu.aspx?id=' + Eva, 'PopUpEva', 'scroll:yes;status:off;dialogWidth:1100px;dialogHeight:700px;dialogLeft:30px;dialogTop:30px');
       var x = window.open('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx?Nro=' + Eva + "&Inf=2", 'PopUpEva', 'alwaysraised=yes,scrollbars=yes,menubar=no,scrolbar=0,toolbar=0,status=no,width=100,height=100,left=450,top=400');
       //var x = window.open('VistaEvaluacion.aspx?id=' + Eva, 'PopUpEva', 'alwaysraised=yes,scrollbars=yes,menubar=no,scrolbar=0,toolbar=0,status=no,width=1000,height=700,left=30,top=30');
   }
   else
   {
       //alert('Selecione una Operación para ver su Evaluación');
       __doPostBack('ctl00$ContentPlaceHolder1$Lb_MJ', '');
   }
   
   return;
    
}

function ClickClasificacion(pTabla, pClass, jClass, sClass) {


    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].HF_NroNeg.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var nro = event.srcElement.parentElement.cells(0).innerText;
        //var ccf = event.srcElement.parentElement.cells(3).innerText;

        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroNNC.value = nro;
        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_PosNNC.value = pos;
        //window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroCCF.value = ccf;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$Lb_buscar_frm', '');
        return;
    }
}

function ClickCondicion(pTabla, pClass, jClass, sClass) {

    
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.className == pClass) {

            event.srcElement.parentElement.className = sClass;

            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_HF_NroCon.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Descripcion.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Fecha_Cumplimiento.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_DP_EstadoCondicion.value = 0;
            
//            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_Txt_Descripcion.readOnly = true;
//            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_Txt_Fecha_Cumplimiento.readOnly = true;
//            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_DP_EstadoCondicion.disabled = true;
//            
//            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_Txt_Descripcion.className = 'clsDisabled';
//            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_Txt_Fecha_Cumplimiento.className = 'clsDisabled';
//            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_DP_EstadoCondicion.className = 'clsDisabled';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var Nro = event.srcElement.parentElement.cells(0).innerText;
        var Des = event.srcElement.parentElement.cells(1).innerText;
        var FecCum = event.srcElement.parentElement.cells(2).innerText;
        var Est = event.srcElement.parentElement.cells(5).innerText;
        

        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_HF_NroCon.value = Nro;
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Descripcion.value = Des;
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Fecha_Cumplimiento.value = FecCum;
        
       switch (Est)
       {
        case 'INGRESADA': { window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_DP_EstadoCondicion.value = 1; break }
        case 'CUMPLIDA': { window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_DP_EstadoCondicion.value = 2; break }
       }
      
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Descripcion.readOnly = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Fecha_Cumplimiento.readOnly = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_DP_EstadoCondicion.disabled = false;
            
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Descripcion.className = 'clsMandatorio';
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_Txt_Fecha_Cumplimiento.className = 'clsMandatorio';
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_DP_EstadoCondicion.className = 'clsMandatorio';
            
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        return;
    }
}

function Click_A_Operacion(pTabla, pClass, jClass, sClass) 
{


    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].HF_PosNeg.value = '';
            window.document.forms[0].HF_NroOpe.value = '';
            window.document.forms[0].HF_NroNeg.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var ope = event.srcElement.parentElement.cells(0).innerText;
        var rut = event.srcElement.parentElement.cells(1).innerText;

        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_PosOpe.value = pos;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroOpe.value = ope;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_RutCli.value = rut;
        
        //var rut = event.srcElement.parentElement.cells(1).innerText;
        //var neg = event.srcElement.parentElement.cells(9).innerText;
        //window.document.forms[0].ctl00$ContentPlaceHolder1$HF_RutCli.value = rut;
        //window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroNeg.value = neg;


        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        
       // __doPostBack('ctl00$ContentPlaceHolder1$lb_ope', '');
        //__doPostBack('ctl00$ContentPlaceHolder1$TabContainer1$TabPanel2$ResumenOperacion1$LB_Refrescar', '');
        
        return;
    }
}
