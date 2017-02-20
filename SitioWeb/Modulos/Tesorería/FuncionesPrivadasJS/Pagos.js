function SeleccionaPago(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	var pos = event.srcElement.parentElement.rowIndex;
	var id = event.srcElement.parentElement.cells(0).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Ing.value = '';
			return;

		}


    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = pos;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Ing.value = id;
	
	ctl00_ContentPlaceHolder1_LB_BuscarDetallePago.click();
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	
	}
	
}

function SeleccionaDetalle(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	var pos = event.srcElement.parentElement.rowIndex;
	var id = event.srcElement.parentElement.cells(0).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Doc_CxC.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Tipo.value = '';
			return;

		}


    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Doc_CxC.value = pos;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Tipo.value = id;
	
	ctl00_ContentPlaceHolder1_LB_BuscaDocCxC.click();
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	
	}
	
}


function SeleccionaPagoAnula(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	var est = event.srcElement.parentElement.cells(13).innerText;	   
    
    //----------------------------------------------------------------------------------------------------------------------
    //si el dpo esta ingresado, a depositar, a depositar en custodia o en cobranza judicial no lo puede anular o protestar
//    if(est == 1 || est == 2 || est== 3 || est== 9)
//    {
//        return 
//    }
    //----------------------------------------------------------------------------------------------------------------------
    
    var pos = event.srcElement.parentElement.rowIndex;
	var id = event.srcElement.parentElement.cells(3).innerText;
	var rut = event.srcElement.parentElement.cells(0).innerText;
    
	    if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Dpo.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Estado.value = '';
	        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_RutCliente.value = '';
			return;

		}


    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = pos;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Dpo.value = id;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Estado.value = est;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_RutCliente.value = rut;
	
	
	//ctl00_ContentPlaceHolder1_LB_BuscarDetallePago.click();
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	__doPostBack('ctl00$ContentPlaceHolder1$Lb_gri','');
	return  ;
	
	}
	
}


function SeleccionaPagoDetalle(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	var pos = event.srcElement.parentElement.rowIndex;
	var id = event.srcElement.parentElement.cells(0).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].HF_Pos_Doc_CxC.value = '';
            window.document.forms[0].HF_Tipo.value = '';
			return;

		}


    window.document.forms[0].HF_Pos_Doc_CxC.value = pos;
	window.document.forms[0].HF_Tipo.value = id;
	
	LB_BuscaDocCxC.click();
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	
	}
	
}



function SeleccionaPagoNomina(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	var pos = event.srcElement.parentElement.rowIndex;
	var id = event.srcElement.parentElement.cells(1).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Ing.value = '';
			return;

		}


    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = pos;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Ing.value = id;
	
	//ctl00_ContentPlaceHolder1_LB_BuscarDetallePago.click();
	ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_LB_BuscarDetallePago.click();
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	
	}

}

function SeleccionaAplicacionAnula(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {

        var est = event.srcElement.parentElement.cells(13).innerText;

        //----------------------------------------------------------------------------------------------------------------------
        //si el dpo esta ingresado, a depositar, a depositar en custodia o en cobranza judicial no lo puede anular o protestar
        //    if(est == 1 || est == 2 || est== 3 || est== 9)
        //    {
        //        return 
        //    }
        //----------------------------------------------------------------------------------------------------------------------

        var pos = event.srcElement.parentElement.rowIndex;
        var id = event.srcElement.parentElement.cells(3).innerText;
        var rut = event.srcElement.parentElement.cells(0).innerText;

        if (pTabla.rows(pos).className == pClass) {
            pTabla.rows(pos).className = sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Dpo.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Estado.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_RutCliente.value = '';
            return;

        }


        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_Ing.value = pos;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Id_Dpo.value = id;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Estado.value = est;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_RutCliente.value = rut;


        //ctl00_ContentPlaceHolder1_LB_BuscarDetallePago.click();

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$Lb_gri_Apl', '');
       // ctl00_ContentPlaceHolder1_Lb_gri_Apl.click()
        return;

    }

}