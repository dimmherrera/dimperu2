// Archivo JScript

//function celClickCXC(pTabla,pBtn,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			BtnEnable(pBtn,true);
//    		return;

//		}
//		
//	window.document.forms[0].NroCuenta.value = event.srcElement.parentElement.children(1).innerText;
//	
//	if (event.srcElement.parentElement.children(8).innerText == 'ANULADA')
//	{BtnEnable(pBtn,true);}
//	else
//	{BtnEnable(pBtn,false);}
//	
//	
//	//window.document.forms[0].TipoCuenta.value = event.srcElement.parentElement.children(0).innerText;
//	window.document.forms[0].Txt_Monto.value = '';
//    window.document.forms[0].Txt_Descripcion.value = '';
//    
//	window.document.forms[0].Txt_Monto.className = 'clsDisabled';
//    window.document.forms[0].Txt_Descripcion.className = 'clsDisabled';
//    
//    window.document.forms[0].Txt_Monto.readOnly = true;
//    window.document.forms[0].Txt_Descripcion.readOnly = true;
//    
//	
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	//BtnEnable(pBtn,false); 
//	return;
//	}
//}
/*
function LimpiarCXC()
{
    window.document.forms[0].Txt_Monto.className = 'clsDisabled';
    window.document.forms[0].Txt_Descripcion.className = 'clsDisabled';
    
    window.document.forms[0].Txt_Monto.readOnly = true;
    window.document.forms[0].Txt_Descripcion.readOnly = true;
    
    window.document.forms[0].TabStrip1.disabled= false;
    window.document.forms[0].MultiPage1.disabled = false;
    
    window.document.forms[0].Txt_Monto.value = '';
    window.document.forms[0].Txt_Descripcion.value = '';
    window.document.forms[0].Txt_Rut_Cli.value = '';
    window.document.forms[0].Txt_Dig_Cli.value = '';
    window.document.forms[0].Txt_Raz_Soc.value = '';
    window.document.forms[0].Txt_Nro_Oto.value = '';
    window.document.forms[0].Txt_Rut_Deu.value = '';
    window.document.forms[0].Txt_Dig_Deu.value = '';
    window.document.forms[0].Txt_Rso_Deu.value = '';
    window.document.forms[0].dp_Tipo_Docto.value = 0;
    window.document.forms[0].Txt_Nro_Doc.value = '';
    window.document.forms[0].Txt_Can_Cuo.value = '';
    
    
    return;
    
}
*/
//function celClickCXC(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			//BtnEnable(pBtn,true);
//    		return;

//		
//    var posicion = event.srcElement.parentElement.rowIndex;
//    var codigo = event.srcElement.parentElement.cells(1).innerText;
//	document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;
//	
//	
//	 document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = codigo;
//    
//	if (event.srcElement.parentElement.children(8).innerText == 'VIGENTE')
//	{BtnEnable(ctl00$ContentPlaceHolder1$IB_Anular,true);}
//	else
//	{BtnEnable(ctl00$ContentPlaceHolder1$IB_Anular,false);}
//	
//	if (event.srcElement.parentElement.children(1).innerText == "PAGADA" )
//		      {
//		        document.aspnetForm.ctl00$ContentPlaceHolder1$IB_Anular.enabled =s false;
//		      }     
//		    else if (event.srcElement.parentElement.children(1).innerText == "VIGENTE")
//		      {
//		        document.aspnetForm.ctl00$ContentPlaceHolder1$IB_Anular.enabled = true;
//		      }		    
//		}
//	
//	NormalClass(pTabla,pClass,jClass);
//    J_RolClass(pClass);
//	
//	  
//	BtnEnable(pBtn,false); 
//	__doPostBack('ctl00$ContentPlaceHolder1_LinkbN_Cuenta','');	
//	return;
//	}
//}




function celClickCXC(pTabla,pClass,jClass,sClass)
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
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = codigo;
    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$LinkbN_Cuenta','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}
//celdocckSinBtn
function  celdocSinBtn(pTabla, pClass, jClass, sClass) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            return false;

        }

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        AceptaDocumento(pTabla);
        return false;

    }

}


function AceptaDocumento(grilla)
{
    //var rut = event.srcElement.parentElement.children(0).innerText; 
    //var rso = event.srcElement.parentElement.children(1).innerText; 
    
    var nro;
    var cuota;
    var monto;
    var id;
    var pClass = 'clicktable';
    
    var posicion = event.srcElement.parentElement.rowIndex;
    
    for(var i=0;i<grilla.rows.length;i++)
    {
	    if(grilla.rows(i).className==pClass) 
	    {
		    nro = grilla.children(0).rows(i).cells(4).innerText;
		    monto = grilla.children(0).rows(i).cells(5).innerText;
		    cuota = grilla.children(0).rows(i).cells(6).innerText;
//	        id = grilla.children(0).rows(i).cells(10).innerText;
		    
	        window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_txt_nro_doc.value = nro;
		    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_txt_mont_doc.value = monto;
		    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_txt_nro_cuota.value = cuota;
		    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;

		    window.dialogArguments.__doPostBack('ctl00$ContentPlaceHolder1$lb_id_doc', '');
		    
            window.close();
	        return ;
		}
    }
    
    alert('Seleccione un Documento');
	//return false;

}

function funcionprueba(pTabla, pClass, jClass, sClass,rut,dv,rs) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            return false;

        }

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        AceptaClientePrueba(pTabla,rut,dv,rs);
        return false;

    }

}

function ClickContenedora(pTabla, pClass, jClass, sClass) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            return false;

        }

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        AceptaClienteContenedora(pTabla);
        return false;

    }

}

function celClickAyudaDoc(pTabla,pClass,jClass,sClass)
{
if(event.srcElement.parentElement.tagName=='TR')
{
if(event.srcElement.parentElement.cells(0).className==pClass)
{
for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
event.srcElement.parentElement.cells(i).className=sClass;
document.aspnetForm.HF_Pos.value = '';
return;
}

var posicion = event.srcElement.parentElement.rowIndex;


document.aspnetForm.HF_Pos.value = posicion;


// document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
// document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';

__doPostBack('LinkbN_Cuenta','');
NormalClass(pTabla,pClass,jClass);
J_RolClass(pClass);

return;
}
}