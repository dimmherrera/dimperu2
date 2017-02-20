// Archivo JScript

function celClickContacto(pTabla,pClass,jClass,sClass)
{    
    var rut;
    var rso;
    var pClass = 'clicktable';
    
    if(event.srcElement.parentElement.tagName=='TR')

	{
	    if(event.srcElement.parentElement.cells(0).className==pClass) 
	    {
	        for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			window.document.form1.TxtNro.value = '';
			//__doPostBack('LnkBtnLimpiaCnc','');
        	return;
     }
		
    NormalClass(pTabla,pClass,jClass);
    J_RolClass_Tabla(pTabla, pClass);

	//var Id = IdGrilla(pTabla, 'clicktable', 0);
	var Id = event.srcElement.parentElement.cells(0).innerText;
	var Po = event.srcElement.parentElement.rowIndex;
		
    window.document.form1.NroContacto.value = Id;
    window.document.form1.Posicion.value = Po;
    __doPostBack('DetalleCnc', '');
   	return;
   	}
}

function NewContacto(url, pWidth, pHeight, pLeft, pTop) {

    var rut = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.value;

    rut = String(rut);
    rut = rut.replace('.', '');

    var x = window.showModalDialog(url + '?rut=' + rut, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
    return;
}

function celClickDosContacto(pTabla, pBtn1, pBtn2, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            BtnEnable(pBtn1, true);
            BtnEnable(pBtn2, true);

            return;

        }

        window.document.forms[0].NroCnc.value = event.srcElement.parentElement.cells(0).innerText;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass(pClass);
        BtnEnable(pBtn1, false);
        BtnEnable(pBtn2, false);
        return;
    }
}
             
function openseleccionDosContacto(grilla, url, pWidth, pHeight, pLeft, pTop) {
    var id;
    var pClass = 'clicktable';
    var rut = window.document.forms[0].Txt_Rut.value;
    rut = String(rut);
    rut = rut.replace('.', '');

    for (var i = 0; i < grilla.rows.length; i++) {
        if (grilla.rows(i).cells(0).className == pClass) {
            id = IdGrilla(grilla, 'clicktable', 0);
            var x = window.showModalDialog(url + '?rut=' + rut + "&id=" + id, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
            return;
        }
    }

}

function celClickDosContacto(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;


            return;

        }

        window.document.forms[0].NroCnc.value = event.srcElement.parentElement.cells(0).innerText;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass(pClass);
        return;
    }
}
