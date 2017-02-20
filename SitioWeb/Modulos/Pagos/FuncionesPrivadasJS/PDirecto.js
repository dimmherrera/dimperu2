function SelecionaPago(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{

	    var pos = event.srcElement.parentElement.rowIndex;
	    var id = event.srcElement.parentElement.cells(0).innerText
	      
	      if(pTabla.rows(pos).className==pClass)
        {
            pTabla.rows(pos).className=sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_HF_Pos_DPO.value = '';
			return;

		}


    var pos = event.srcElement.parentElement.rowIndex;
	window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_HF_Pos_DPO.value = pos;
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	}
}

