function SeleccionaGrCiudad(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Posicion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Ciudad.value = '';
			return;
		}
	
	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
    
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Posicion.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Ciudad.value = codigo;
        	
	__doPostBack('ctl00$ContentPlaceHolder1$LnkB_GrillaComuna','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	return;
	}
}

//*************************Funcion Grilla***********************************************
//Funcion para rescatar un valor de una pagina que esta en una grilla
//function DetalleVB(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')

//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)

//		{
//			
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			
//			return false;
//            
//		}
//		
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	//document.aspnetForm.
//	document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Cod.valu = event.srcElement.parentElement.cells(0).innerText;
//	document.aspnetForm.ctl00_ContentPlaceHolder1_HF_CodCiu.value = event.srcElement.parentElement.cells(0).innerText;
//	var cod = event.srcElement.parentElement.cells(0).innerText;
//	
//	document.getElementById('ctl00_ContentPlaceHolder1_HF_CodCiu').value = cod;
//		
//	return false;
//	
//	}
//}

//*************************Grilla Comuna************************************************

		
//function SeleccionaGrComuna(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com_Oculto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = 0;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Com_Banco.value = 0;
//                        
//			return;
//		}
//		
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	       
//	     try
//	     {
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = '';
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com_Oculto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = 0
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Com_Banco.value = 0;
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = event.srcElement.parentElement.cells(0).innerText;
//            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com.value = event.srcElement.parentElement.cells(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com_Oculto.value = event.srcElement.parentElement.cells(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = event.srcElement.parentElement.cells(1).innerText;
//            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal.value = event.srcElement.parentElement.cells(2).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = event.srcElement.parentElement.cells(2).innerText;
//            
//           document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = event.srcElement.parentElement.children(2).innerText;
//        
//           document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.disabled = false;
//           document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.disabled = false;
//           document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Com_Banco.disabled = false;
//	       	             	                            
//        }	                                                      	       
//	  catch(e){alert(e.message);}
//	       	       	       	            	            
//	return;
//	}
//}



//function SeleccionaGrComuna(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdComuna.value = '';
//			return;
//		}
//	
//	var posicion = event.srcElement.parentElement.rowIndex;
//    var codigo = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = event.srcElement.parentElement.cells(1).innerText;

//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdComuna.value = codigo;
//    
////	__doPostBack('ctl00$ContentPlaceHolder1$LinkBGrillaCmn','');        
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	return;
//	}
//}

function SeleccionaGrComuna(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Posicion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdComuna.value = '';
			return;
		}
	
	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
    
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdComuna.value = codigo;
        	
	__doPostBack('ctl00$ContentPlaceHolder1$LinkBGrillaCmn','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	return;
	}
}

//*************************************************************************************************************************





function SeleccionaGrCartera(pTabla,pClass,jClass,sClass)
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
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroCartera.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.value = '';
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.readOnly = true;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = true;
    
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.className = 'clsDisabled';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsDisabled';
//            
//            document.getElementById('ctl00_ContentPlaceHolder1_IB_Guardar').disabled = true;
//            document.getElementById('ctl00_ContentPlaceHolder1_IB_Eliminar').disabled = true;
			return;
		}
	

    var codigo = event.srcElement.parentElement.cells(0).innerText;
    var posicion = event.srcElement.parentElement.rowIndex;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroCartera.value = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.value = event.srcElement.parentElement.cells(2).innerText;
    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.readOnly = false;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = false;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsMandatorio';
//    
//    
//    document.getElementById('ctl00_ContentPlaceHolder1_IB_Guardar').disabled = false;
//    document.getElementById('ctl00_ContentPlaceHolder1_IB_Eliminar').disabled = false;
    
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = codigo;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Posicion.value = posicion;
	__doPostBack('ctl00$ContentPlaceHolder1$LinkB_Id_crt','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	}
}




//function SeleccionaGrCartera(pTabla,pClass,jClass,sClass)
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
//			//event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroCartera.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.value = '';
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.readOnly = true;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = true;
//    
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.className = 'clsDisabled';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsDisabled';
//            
//            document.getElementById('ctl00_ContentPlaceHolder1_IB_Guardar').disabled = true;
//            document.getElementById('ctl00_ContentPlaceHolder1_IB_Eliminar').disabled = true;
//			return;
//		}
//	

//    var codigo = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroCartera.value = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.value = event.srcElement.parentElement.cells(2).innerText;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.readOnly = false;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.readOnly = false;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsMandatorio';
//    
//    
//    document.getElementById('ctl00_ContentPlaceHolder1_IB_Guardar').disabled = false;
//    document.getElementById('ctl00_ContentPlaceHolder1_IB_Eliminar').disabled = false;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = codigo;
//    
//	//__doPostBack('ctl00$ContentPlaceHolder1$LinkB_Id_crt','');        
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass_Tabla(pTabla, pClass);
//	
//	return;
//	}
//}
//function SeleccionaGrSucursal(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//		//	document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = '';
//            //document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdComuna.value = '';
//			return;
//		}

////	var posicion = event.srcElement.parentElement.rowIndex;
////    var codigo = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.value = event.srcElement.parentElement.cells(0).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion_corta.value = event.srcElement.parentElement.cells(2).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_PlazaBanco.value  = event.srcElement.parentElement.cells(3).innerText;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
////    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_IdComuna.value = codigo;
//    
//	//__doPostBack('ctl00$ContentPlaceHolder1$LinkBGrillaCmn','');        
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	return;
//	}
//}

