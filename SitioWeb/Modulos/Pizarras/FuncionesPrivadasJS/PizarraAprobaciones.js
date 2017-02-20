function ClickNegociacion(pTabla, pClass, jClass, sClass) {

    
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].HF_NroNeg.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var nro = event.srcElement.parentElement.cells(13).innerText;
        var ope = event.srcElement.parentElement.cells(0).innerText;

        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroNeg.value = nro;
        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_PosNeg.value = pos;
        window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroOpe.value = ope;
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        
        __doPostBack('ctl00$ContentPlaceHolder1$Lb_buscar_ccf', '');
        //__doPostBack('ctl00$ContentPlaceHolder1$Lb_buscar', '');
        
        return;
    }
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
        
       // window.document.forms[0].ctl00$ContentPlaceHolder1$HF_NroCCF.value = ccf;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$Lb_buscar_frm', '');
        return;
    }
}

  function DetalleLineaFinanciamiento(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	    var pos = event.srcElement.parentElement.rowIndex;
	    var id = event.srcElement.parentElement.cells(0).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
//            
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;

            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelLinea_LineaFinanciamiento1_Txt_Pos_Lin.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelLinea_LineaFinanciamiento1_Txt_NroLinea.value = '';
			return;

		}


		window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelLinea_LineaFinanciamiento1_Txt_Pos_Lin.value = pos;
		window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanelLinea_LineaFinanciamiento1_NroLinea.value = id;
	
	//NormalClass(pTabla,pClass,jClass);
	//J_RolClass_Tabla(pTabla, pClass);
	//J_RolClass(pTabla);
    //ctl00_ContentPlaceHolder1_TabContainer1_TabPanel6_LineaFinanciamiento1_Txt_NroLinea
	__doPostBack('ctl00$ContentPlaceHolder1$TabContainer1$TabPanelLinea$LineaFinanciamiento1$LB_CargaDatosLinea', '');
	
	return;
	}
}


