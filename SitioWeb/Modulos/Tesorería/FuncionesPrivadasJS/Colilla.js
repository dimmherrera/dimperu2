

function ClickVBSolicitud(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = codigo;
    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$LinbCheque','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}




function ClickColillaAnterior(posicion, codigo)
{

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = codigo;
    
    __doPostBack('ctl00$ContentPlaceHolder1$LinkB_CAnteriores','');        
   	
	return;
}

function ClickDetalleSolicitud(pTabla,pClass,jClass,sClass)
{
	 if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(1).innerText;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Nro_Neg.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = codigo;
    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$Busqueda_GV_SOLICITUD','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}

function Click_GV_VB(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;


           // window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroAplicacion.value = '';
            //window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Aprobar.disabled = false;
            //window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Rechazar.disabled = false;
            return;

        }

    //    var nro = event.srcElement.parentElement.cells(13).innerText;
      //  var rut = event.srcElement.parentElement.cells(0).innerText
        //window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroAplicacion.value = nro;
        //window.document.forms[0].ctl00_ContentPlaceHolder1_HF_CLI.value = rut;
        //window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Aprobar.disabled= false;
        //window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Rechazar.disabled= false;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$lb_cheque', '');
        //__DoPostback('', 'window.document.forms[0].ctl00_ContentPlaceHolder1$Lb$informe.value')
        return;
    }
}