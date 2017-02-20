

function SeleccionaGrSucursal(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_CodSuc.value = '';
			return;
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.value = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion_corta.value = event.srcElement.parentElement.cells(2).innerText;
    document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_PlazaBanco.value  = event.srcElement.parentElement.cells(3).innerText;
   
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_CodSuc.value = codigo;
    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$LinkB_GrSuc','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
	return;
	}
}



function SeleccionaGrPlaza(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PoPL.value = posicion;
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdPL.value = codigo;
            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = '';
            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = '';
			return;
		}

   var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
     // document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = event.srcElement.parentElement.cells(0).innerText;

      //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = event.srcElement.parentElement.cells(2).innerText;

   
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PoPL.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdPL.value = codigo;
        
   __doPostBack('ctl00$ContentPlaceHolder1$LinkB_CodPla','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
	return;
	}
}

function SeleccionaGrCiudad(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PoPL.value = posicion;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdPL.value = codigo;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = '';
            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = '';
			return;
		}

//   var posicion = event.srcElement.parentElement.rowIndex;
//    var codigo = event.srcElement.parentElement.cells(0).innerText;
     document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Ciudad.value = event.srcElement.parentElement.cells(0).innerText;
     document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Ciu.value = event.srcElement.parentElement.cells(1).innerText;

   
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PoPL.value = posicion;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdPL.value = codigo;
//        
//   __doPostBack('ctl00$ContentPlaceHolder1$LinkB_CodPla','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
	return;
	}
}

//function SeleccionaGrSucursal(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//	    var i = event.srcElement.parentElement.rowIndex;
//	    //if(event.srcElement.parentElement.cells(0).className==pClass)
//		
//	    if(pTabla.rows(i).className==pClass)
//		{
//		    pTabla.rows(i).className = sClass;
//			//for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
////			//event.srcElement.parentElement.cells(i).className=sClass;
////			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = '';
////			document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroCartera.value = '';
////            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.value = '';
////            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.value = '';
//            
////            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.readOnly = true;
////            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = true;
//    
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.className = 'clsDisabled';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsDisabled';
//            
////            document.getElementById('ctl00_ContentPlaceHolder1_IB_Guardar').disabled = true;
////            document.getElementById('ctl00_ContentPlaceHolder1_IB_Eliminar').disabled = true;
//			return;
//		}
//	

//    var codigo = event.srcElement.parentElement.cells(0).innerText;
//    var posicion = event.srcElement.parentElement.rowIndex;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroCartera.value = event.srcElement.parentElement.cells(0).innerText;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.value = event.srcElement.parentElement.cells(1).innerText;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.value = event.srcElement.parentElement.cells(2).innerText;
//    
////    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.readOnly = false;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = false;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
//    
////     document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
////    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
////    document.getElementById('ctl00_ContentPlaceHolder1_IB_Guardar').disabled = false;
////    document.getElementById('ctl00_ContentPlaceHolder1_IB_Eliminar').disabled = false;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_CodSuc.value = codigo;
//	__doPostBack('ctl00$ContentPlaceHolder1$LinkB_GrSuc','');        
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass_Tabla(pTabla, pClass);
//	
//	return;
//	}
//}




//function SeleccionaGrZona(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PoPL.value = posicion;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdPL.value = codigo;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = '';
//            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = '';
//			return;
//		}

////   var posicion = event.srcElement.parentElement.rowIndex;
////    var codigo = event.srcElement.parentElement.cells(0).innerText;
//  //   document.forms.txt_Cod_Zon.value = event.srcElement.parentElement.cells(0).innerText;
//  //   document.aspnetForm.txt_Des.Text.value = event.srcElement.parentElement.cells(1).innerText;
//  window.document.forms[0].txt_Des.Text.value = event.srcElement.parentElement.cells(1).innerText;
//   
////    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PoPL.value = posicion;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdPL.value = codigo;
// //  window.document.form1.txt_Des.value = event.srcElement.parentElement.cells(2).innerText;
//     window.document.form1.txt_Des.value = event.srcElement.parentElement.cells(2).innerText;
//  //__doPostBack('ctl00$ContentPlaceHolder1$Linkb_Zon','');        
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	return;
//	}
//}



//function ClickGrCtr(pTabla, pClass, jClass, sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			
//			//return false;
//            return
//		}


//		var posicion = event.srcElement.parentElement.rowIndex;
//		var codigo = event.srcElement.parentElement.cells(0).innerText;
//		// document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = event.srcElement.parentElement.cells(0).innerText;

//		//document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = event.srcElement.parentElement.cells(2).innerText;


//		document.aspnetForm.HF_Po.value = posicion;
//		document.aspnetForm..value = codigo;
//       
//	 
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass_Tabla(pTabla, pClass);
//	return false;
//	}
//}



function SeleccionaGrZona(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
	    var i = event.srcElement.parentElement.rowIndex;
	    //if(event.srcElement.parentElement.cells(0).className==pClass)
		
	    if(pTabla.rows(i).className==pClass)
		{
		    pTabla.rows(i).className = sClass;
			//for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			//event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = '';
		   
            document.form1.txt_Des.readOnly = true;
               
            document.form1.txt_Des.className = 'clsDisabled';
       //     document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsDisabled';
            
            document.getElementById('txt_Des').disabled = true;
            document.getElementById('Ib_Guardar').disabled = true;
			return;
		}
	

    var codigo = event.srcElement.parentElement.cells(0).innerText;
 window.document.form1.txt_Cod_Zon.value = event.srcElement.parentElement.cells(0).innerText;
   window.document.form1.txt_Des.value = event.srcElement.parentElement.cells(2).innerText;
    
    
    document.form1.txt_Des.readOnly = false;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = false;
    
    document.form1.txt_Des.className = 'clsMandatorio';
    //document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsMandatorio';
    
    
    document.getElementById('txt_Des').disabled = false;
    document.getElementById('Ib_Guardar').disabled = false;
    
 //   document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = codigo;
    
	//__doPostBack('ctl00$ContentPlaceHolder1$LinkB_Id_crt','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	}
}



//function GrCtr(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//	    var i = event.srcElement.parentElement.rowIndex;
//	 	
//	    if(pTabla.rows(i).className==pClass)
//		{
//		    pTabla.rows(i).className = sClass;
//	           
//			return;
//		}
//	
//   window.document.form1.txt_TpRLetra.value = event.srcElement.parentElement.cells(0).innerText;
//   window.document.form1.txt_TPRnumero.value = event.srcElement.parentElement.cells(1).innerText;
//    
//  
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass_Tabla(pTabla, pClass);
//	
//	return;
//	}
//}



function ClickGrCtr(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.form1.HF_Po.value = posicion;
            document.form1.HF_Id.value = codigo;
        
			return;
		}

   var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
 
   
    window.document.form1.HF_Po.value = posicion;
    window.document.form1.HF_Id.value = codigo;
        
   __doPostBack('LinkB','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pTabla, pClass);
	
	return;
	}
}





//function ClickAyudaCliente(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			window.document.form1.HF_Pos.value = '';
//            window.document.form1.HF_Id.value = '';
//			return; 
//		}

//	var posicion = event.srcElement.parentElement.rowIndex;
//    var codigo = event.srcElement.parentElement.cells(0).innerText;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

//    window.document.form1.HF_Pos.value = posicion;
//    window.document.form1.HF_Id.value = codigo;
//    
//    
////    window.document.forms.txt_Des.value = event.srcElement.parentElement.cells(2).innerText;

//    __doPostBack('Linkb_GR','');        
//	NormalClass(pTabla,pClass,jClass);
//   //J_RolClass(pClass);
//		
//	return;
//	}

//}

