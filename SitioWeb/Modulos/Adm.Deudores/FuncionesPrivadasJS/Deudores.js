//function celClickDeudor(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//	    if(event.srcElement.parentElement.cells(0).className==pClass) 
//	    {
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_TxtNro.value = '';
//			//BtnEnable(pBtn,true);

//			return;

//		}
//		
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	//BtnEnable(pBtn,false);
//	//var Id = event.srcElement.parentElement.cells(0).innerText;
//	//Id = Id.substring(0, Id.search('-'));
//	var Id = IdGrilla(pTabla,'clicktable',0);
//	document.aspnetForm.ctl00_ContentPlaceHolder1_TxtNro.value = Id;
//	
//	return;
//	}
//}

function celClickDeudor(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            return;

        }

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        var id = IdGrilla(pTabla, 'clicktable', 0);
        window.document.forms[0].ctl00_ContentPlaceHolder1_TxtNro.value = id;

        return;
    }
}


function celClickDDR(pTabla,pMltView,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
	
	
		if(event.srcElement.parentElement.cells(0).className==pClass)
    	{		
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_TxtGiradoA.value = '';
//          document.aspnetForm.ctl00_ContentPlaceHolder1_TxtObsGes.value = '';
//          document.aspnetForm.ctl00_ContentPlaceHolder1_TxtFecObs.value = '';
//          document.aspnetForm.ctl00_ContentPlaceHolder1_TxtDocNec.value = '';
//          document.aspnetForm.ctl00_ContentPlaceHolder1_TxtDirCob.value = '';
//          document.aspnetForm.ctl00_ContentPlaceHolder1_TxtDocNec.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_TxtRut.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_TxtNro.value = '';
			BtnEnable(pMltView,true);
			return;
		}
	var rut = event.srcElement.parentElement.cells(0).innerText;
	var r = rut.substring(0, rut.search('-'));
	rut = String(r);
	rut = rut.replace('.','');
	document.aspnetForm.ctl00_ContentPlaceHolder1_TxtRut.value =  rut;
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	var Id = IdGrilla(pTabla,'clicktable',0);
	document.aspnetForm.ctl00_ContentPlaceHolder1_TxtNro.value = Id;
    BtnEnable(pMltView,false);
	__doPostBack('ctl00$ContentPlaceHolder1$LnkBtnTraeDDR','');	
	
	return;
	}
}


function AyudaDeu()
{
    window.showModalDialog('Deudores/Ayudas/AyudaDeu.aspx', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:380px;dialogLeft:400px;dialogTop:200px');
    return; 
}