
function CambioBoton()
{
  document.aspnetForm.codsuc.enabled =true;
    document.aspnetForm.dessuc.enabled =true;
    return;
}




//function DetalleBco(pTabla,pClass,jClass,sClass)
//{
//    
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			
//			
//	//document.aspnetForm.ctl00_ContentPlaceHolder1_NroBco.value = 0;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_id_Bco.value = "";
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Bco.value = "";
//    //document.aspnetForm.ctl00_ContentPlaceHolder1_SW.value = 1;
//    
//			return;

//		}
//		
//	var id = IdGrilla(pTabla,sClass);
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_id_Bco.value = id;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_id_Bco.value = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des_Bco.value = event.srcElement.parentElement.cells(1).innerText;
//    
//    //document.aspnetForm.ctl00_ContentPlaceHolder1_SW.value = 1;
//    
//    __doPostBack('ctl00_ContentPlaceHolder1_Link_Bco','');
//		
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
// 
//	
//	//SelFila(pTabla,jClass,IdGrilla(pTabla,pClass,0))
//	//document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = '';
//   //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Suc.value = '';
//	return;
//	}
//}



//*****Banco******



function DetalleBco(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PosBco.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idbco.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PosBco.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idbco.value = codigo;
   // document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des_Bco.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
   
    __doPostBack('ctl00$ContentPlaceHolder1$Link_Bco','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}

//**********Sucursal*************

function DetalleSuc(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PosSuc.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdSuc.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PosSuc.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdSuc.value = codigo;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des_Suc.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$Link_Suc','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}






//function DetalleSuc(pTabla,pClass,jClass,sClass)
//{
//    
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = "";
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Suc.value = "";
//            document.aspnetForm.ctl00_ContentPlaceHolder1_DL_Plaza.value = 0;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_SW.value = 3;			
//			return;

//		}
//		
//	document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = event.srcElement.parentElement.cells(1).innerText;
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Suc.value = event.srcElement.parentElement.cells(2).innerText;
//   document.aspnetForm.ctl00_ContentPlaceHolder1_DL_Plaza.value = truncaNUM(event.srcElement.parentElement.cells(3).innerText);
//  document.aspnetForm.ctl00_ContentPlaceHolder1_SW.value = 4;
//		
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
// 
//	return;
//	}
//}

function Detallecartera(pTabla,pClass,jClass,sClass)

{
   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_nro.value = event.srcElement.parentElement.cells(0).innerText;
   document.aspnetForm.ctl00_ContentPlaceHolder1_txt_des.value = event.srcElement.parentElement.cells(1).innerText;
   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_dias.value = truncaNUM(event.srcElement.parentElement.cells(2).innerText);	
   NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
 return;
}


function Detallenotario(pTabla,pClass,jClass,sClass)

{
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_corr.value = event.srcElement.parentElement.cells(0).innerText;
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_nom.value = event.srcElement.parentElement.cells(1).innerText;
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_dir.value = event.srcElement.parentElement.cells(4).innerText;	
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_tel.value = event.srcElement.parentElement.cells(6).innerText;	
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_diremp.value = event.srcElement.parentElement.cells(5).innerText;	

//   
//   var ch = event.srcElement.parentElement.cells(7).innerText;
//   
//   if (ch == 'S')
//   {
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Ch_def.checked = true;
//   }
//   
//   if (ch == 'N')
//   {
//   document.aspnetForm.ctl00_ContentPlaceHolder1_Ch_def.checked = false;
//   }       	
   //document.aspnetForm.ctl00_ContentPlaceHolder1_Btn_guar.disabled=false;
    //document.aspnetForm.ctl00_ContentPlaceHolder1_Btn_eli.disabled=false;
    NormalClass(pTabla, pClass, jClass);
    J_RolClass(pClass);
    var id = event.srcElement.parentElement.rowIndex;
    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_gr.value = id;
   

   __doPostBack('ctl00$ContentPlaceHolder1$Detalle', '');
 return;
}