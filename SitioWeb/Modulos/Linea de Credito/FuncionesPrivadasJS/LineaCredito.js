    function DetalleLineaCredito(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	    var pos = event.srcElement.parentElement.rowIndex;
	    var id = event.srcElement.parentElement.cells(0).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Pos_Lin.value = '';
            window.document.forms[0].ctl00$ContentPlaceHolder1$NroLinea.value = '';
            ctl00_ContentPlaceHolder1_LB_CargaDatosLinea.click();
			return;

		}


		window.document.forms[0].ctl00$ContentPlaceHolder1$Txt_Pos_Lin.value = pos;
		window.document.forms[0].ctl00$ContentPlaceHolder1$NroLinea.value = id;
	
	//NormalClass(pTabla,pClass,jClass);
	//J_RolClass_Tabla(pTabla, pClass);
	//J_RolClass(pTabla);
    ctl00_ContentPlaceHolder1_LB_CargaDatosLinea.click();
	//__doPostBack('ctl00$ContentPlaceHolder1$LB_CargaDatosLinea', '');
	
	return;
	}
}


//SUB-LINEAS
function DetalleGV_SubDDR(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	    //var id = event.srcElement.parentElement.rowIndex;
	    //var id = event.srcElement.parentElement.cells(0).innerText

	    var pos = event.srcElement.parentElement.rowIndex;
	    var id = event.srcElement.parentElement.cells(4).innerText;
	
    if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
	        window.document.forms[0].Txt_Deu_Rut.className = 'clsDisabled';
	        window.document.forms[0].Txt_Deu_Dig.className = 'clsDisabled';
	        window.document.forms[0].Txt_Deu_Rso.className = 'clsDisabled';
	        window.document.forms[0].Txt_Deu_Val.className = 'clsDisabled';
	        
			window.document.forms[0].Txt_Pos_Lin.value = '';
	        window.document.forms[0].Txt_Deu_Rut.value = '';
	        window.document.forms[0].Txt_Deu_Dig.value = '';
	        window.document.forms[0].Txt_Deu_Rso.value = '';
	        window.document.forms[0].Txt_Deu_Val.value = '';
	        window.document.forms[0].Pos_Deu.value = '';
	        window.document.forms[0].Txt_Deu_Rut.readOnly = true;
	        window.document.forms[0].Txt_Deu_Dig.readOnly = true;
	        window.document.forms[0].Txt_Deu_Rso.readOnly = true;
	        window.document.forms[0].Txt_Deu_Val.readOnly = true;
	        
	        window.document.forms[0].RB_Deu_Mto.disabled = true;
	        window.document.forms[0].RB_Deu_Por.disabled = true;
	        
        	
			return;
		}


		window.document.forms[0].Pos_Deu.value = pos;
		window.document.forms[0].sbl_num.value = id;
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);

	Detalle_Deu.click();
	return;
	}
}




function DetalleGV_SubPro(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	    var pos = event.srcElement.parentElement.rowIndex;
	    var id = event.srcElement.parentElement.cells(0).innerText;
	
        if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className = sClass;
            
	        window.document.forms[0].Pos_Pro.value = '';
        	window.document.forms[0].DP_Pro_Tip.value = 0;
        	window.document.forms[0].Txt_Pro_Val.value = '';
        	
        	window.document.forms[0].DP_Pro_Tip.className = 'clsDisabled';
        	window.document.forms[0].Txt_Pro_Val.className = 'clsDisabled';
        	
        	window.document.forms[0].RB_Pro_Mto.disabled = true;
	        window.document.forms[0].RB_Pro_Por.disabled = true;
	        
        	
	        return;
		}

		window.document.forms[0].Pos_Pro.value = pos;
		window.document.forms[0].sbl_num.value = id;
	
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	Detalle_Pro.click();
	return;
	}
}


//PORCENTAJE A ANTICIPAR

function DetalleGV_APC(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	    var pos = event.srcElement.parentElement.rowIndex;
	    var id = event.srcElement.parentElement.cells(0).innerText;
	    
        if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
	        //window.document.forms[0].ctl00_ContentPlaceHolder1_WC_SubLineas1_Posicion.value = '';
        	window.document.forms[0].DP_Pro_Tip.value = 0;
        	window.document.forms[0].Txt_Pro_Val.value = '';
        	
        	window.document.forms[0].DP_Pro_Tip.className = 'clsDisabled';
        	window.document.forms[0].Txt_Pro_Val.className = 'clsDisabled';
        	
        	window.document.forms[0].DP_Pro_Tip.disabled = true;
            window.document.forms[0].Txt_Pro_Val.readOnly = true;
            
        	//verificacion
        	window.document.forms[0].RB_Ver_Si.disabled = true;
	        window.document.forms[0].RB_Ver_No.disabled = true;
	        //notificacion
	        window.document.forms[0].RB_Not_Si.disabled = true;
	        window.document.forms[0].RB_Not_No.disabled = true;
	        //cobranza
	        window.document.forms[0].RB_Cob_Si.disabled = true;
	        window.document.forms[0].RB_Cob_No.disabled = true;
	               	
	        return;
		}


		window.document.forms[0].Posicion.value = pos;
		window.document.forms[0].apc_num.value = id;
    
    LB_DetalleAPC.click();
	return;
	}
}

