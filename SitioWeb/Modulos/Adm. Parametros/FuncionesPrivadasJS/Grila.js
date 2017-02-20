//*******************************Grilla Tasa Maxima***************************************

//function SeleccionaFilaTasMaxCon(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Oculta.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.value = 0;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha.disabled = true; 
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.disabled = true;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.disabled = true;
//              
//      	
//			return;
//        }	        	
//    NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha.value = '';
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Oculta.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.value = 0;
//            			    
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha.value = event.srcElement.parentElement.children(1).innerText;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Oculta.value = event.srcElement.parentElement.children(1).innerText;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.value = event.srcElement.parentElement.children(2).innerText;
//		    		    
//		    		    
//		    if (event.srcElement.parentElement.children(3).innerText == "A" )
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.value = "A";
//		      }     
//		    else if (event.srcElement.parentElement.children(3).innerText == "I")
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.value = "I";
//		      }		    
//		    
//	            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha.disabled = true; 
//			    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.disabled = false;
//			    document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.disabled = false;
// 	        
//	return;
//	
//	}
//}
    
    
    
    
//    function SeleccionaFilaTasMaxCon(pTabla,pClass,jClass,sClass)
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
////			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idcrt.value = '';
//		   
//         //   document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.readOnly = true;
//               
//      //      document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.className = 'clsDisabled';
//       //     document.aspnetForm.ctl00_ContentPlaceHolder1_txt_NroDiasCobro.className = 'clsDisabled';
//            
//       //   document.getElementById('Txt_TMC_Porc_Tasa').disabled = true;
//       //   document.getElementById('Ib_Guardar').disabled = true;
//			return;
//		}
//	
//    var codigo = event.srcElement.parentElement.cells(0).innerText;
// //window.document.form1.txt_Cod_Zon.value = event.srcElement.parentElement.cells(0).innerText;
// //  window.document.form1.txt_Des.value = event.srcElement.parentElement.cells(2).innerText;
//    
//     document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha.value = event.srcElement.parentElement.children(1).innerText;
//	 document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Oculta.value = event.srcElement.parentElement.children(1).innerText;
//	 document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.value = event.srcElement.parentElement.children(2).innerText;


//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.readOnly = false;
//    //document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.enabled = true;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa.className = 'clsMandatorio';
//    //document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado.className = 'clsMandatorio';
//    
//    
//  //document.getElementById('ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa').disabled = false;
//    //document.getElementById('ctl00_ContentPlaceHolder1_Dp_TMC_Estado').disabled = false;
// // document.getElementById('Ib_Guardar').disabled = false;
//    
//    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_ID_Tmc.value = codigo;
//    
//	//__doPostBack('ctl00$ContentPlaceHolder1$LinkB_Id_crt','');    
//	
//	
////	
////	 if (event.srcElement.parentElement.children(3).innerText == "A" )
////		      {
////		        document.aspnetForm.ctl00_ContentPlaceHolder1_RBTMC_Activo.chequed = true;
////		      }     
////		    else if (event.srcElement.parentElement.children(3).innerText == "I")
////		      {
////		        document.aspnetForm.ctl00_ContentPlaceHolder1_RBTMC_Inactivo.chequed = true;
////		      }		       
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass_Tabla(pTabla, pClass);
//	
//	return;
//	}
//}

    
    function SeleccionaFilaTasMaxCon(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_ID_Tmc.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_ID_Tmc.value = codigo;
   // document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des_Bco.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$Link_TMC','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}
    
    
    
    
    
    
//***********************************Grilla Tasa Maxima Auxiliar*************************************************

function SeleccionaFilaTasMaxConAux(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
		
		
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Aux.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Aux.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa_Aux.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado_Aux.value = 0;
            return;
        }	        	
        
    NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
			
	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Aux.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa_Aux.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado_Aux.value = 0;
            			    
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Fecha_Aux.value = event.srcElement.parentElement.children(0).innerText;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TMC_Porc_Tasa_Aux.value = event.srcElement.parentElement.children(1).innerText;
		    		    
		    if (event.srcElement.parentElement.children(2).innerText == "ACTIVO" )
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado_Aux.value = 1;
		      }
		    else if (event.srcElement.parentElement.children(2).innerText == "INACTIVO")
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TMC_Estado_Aux.value = 2;
		      }		    
		     	
 		return;
	}
}

    
//***********************************Grilla Tasa Base**************************************    
//function SeleccionaFilaTasBas(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//				event.srcElement.parentElement.cells(i).className = sClass;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Cod.value = '';
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.value = 0;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 0;
//		    //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado.value = 0;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 0;
//		
//		
//		
//		
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha.disabled = true;		    	    
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa.disabled = true;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde.disabled = true;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta.disabled = true;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip.disabled = true;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado.disabled = true;			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.disabled = true;
//		
//		
//		
//		
//		    //document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.disabled = false;
//		    //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha.disabled = false;
//		
//			return;
//		}	
//		
//    NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Cod.value = '';
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.value = 0;
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 0;
//		    //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado.value = 0;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 0;
//		
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Cod.value = event.srcElement.parentElement.children(0).innerText;
//		
//			switch ( event.srcElement.parentElement.children(1).innerText )
//                { 
//	                case "PESOS": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.value = 1;
//	                              document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 1;
//	                        break;
//	                case "UF": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.value = 2;
//	                           document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 2;
//			                break;
//	                case "DOLAR": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.value = 3;
//	                              document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 3;
//			                break;
//	                case "EURO": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.value = 4;
//	                             document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 4;
//			                break;
//	            }
//			
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa.value = event.srcElement.parentElement.children(3).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde.value = event.srcElement.parentElement.children(4).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta.value = event.srcElement.parentElement.children(5).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip.value = event.srcElement.parentElement.children(6).innerText;
//				
//			 if (event.srcElement.parentElement.children(7).innerText == "ACTIVO" )
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado.value = 1;
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 1;
//		      }
//		    else if (event.srcElement.parentElement.children(7).innerText == "INACTIVO")
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado.value = 2;
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 2;
//		      }	
//		      
//		      
//            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha.disabled = false;		    	    
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa.disabled = false;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde.disabled = false;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta.disabled = false;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip.disabled = false;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado.disabled = false;
//			 
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda.disabled = true;
//		    
//		    
//		    //document.aspnetForm.ctl00_ContentPlaceHolder1_Label8.visible = true;
//		    //document.aspnetForm.ctl00_ContentPlaceHolder1_Label1.visible = false;
//		    			 	        
//	return;
//	}
//}


 function SeleccionaFilaTasBas(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Idbco.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_ID_Tmc.value = codigo;
  
    
    __doPostBack('ctl00$ContentPlaceHolder1$Link_TB','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}
    





//***********************************Grilla Tasa Base Auxiliar**************************************  

//function SeleccionaFilaTasBasAux(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//				event.srcElement.parentElement.cells(i).className = sClass;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Cod_Aux.value = '';
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 0;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 0;

//			return;
//		}	
//		
//    NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Cod_Aux.value = '';
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 0;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip_Aux.value = '';
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 0;
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Cod_Aux.value = event.srcElement.parentElement.children(0).innerText;
//			
//			switch ( event.srcElement.parentElement.children(1).innerText )
//                { 
//                case "PESOS": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 1;
//	                    break;
//                case "UF": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 2;
//	                    break;
//                case "DOLAR": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 3;
//	                    break;
//                case "EURO": document.aspnetForm.ctl00_ContentPlaceHolder1_DP_TB_TipoMoneda_Aux.value = 4;
//	                    break;
//                }
//		
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Fecha_Aux.value = event.srcElement.parentElement.children(2).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Porc_Tasa_Aux.value = event.srcElement.parentElement.children(3).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Desde_Aux.value = event.srcElement.parentElement.children(4).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Hasta_Aux.value = event.srcElement.parentElement.children(5).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TB_Descrip_Aux.value = event.srcElement.parentElement.children(6).innerText;
//			
//			 if (event.srcElement.parentElement.children(7).innerText == "ACTIVO" )
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 1;
//		      }
//		    else if (event.srcElement.parentElement.children(7).innerText == "INACTIVO")
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TB_Estado_Aux.value = 2;
//		      }	
//		 	        
//	return;
//	}
//}



//*****************************Grilla Tasa Impuesto********************************************
//function SeleccionaFilaTasImp(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//				event.srcElement.parentElement.cells(i).className = sClass;
//			
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha_Aux.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado.value = 0;
//                        
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha.disabled = true;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista.disabled = true;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo.disabled = true;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado.disabled = true;
//		
//		    return;
//            			    
//		}	
//    NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha.value = '';
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha_Aux.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado.value = 0;
//			
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha.value = event.srcElement.parentElement.cells(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha_Aux.value = event.srcElement.parentElement.cells(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista.value = event.srcElement.parentElement.cells(1).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo.value = event.srcElement.parentElement.cells(2).innerText;
//            
//            if (event.srcElement.parentElement.children(3).innerText == "ACTIVO" )
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado.value = 1;
//		      }
//		    else if (event.srcElement.parentElement.children(3).innerText == "INACTIVO")
//		             
//		      {
//		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado.value = 2;
//		      }	
//		      
//		      document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha.disabled = true;
//		      document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista.disabled = false;
//              document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo.disabled = false;
//              document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado.disabled = false;
// 	
//	return;
//	}
//}    

   function SeleccionaFilaTasImp(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_ID_Tmc.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Po.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_ID_Tmc.value = codigo;
  
    
    __doPostBack('ctl00$ContentPlaceHolder1$Link_TI','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}


//*****************************Grilla Tasa Impuesto Auxiliar********************************************
function SeleccionaFilaTasImpAux(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
				event.srcElement.parentElement.cells(i).className = sClass;
			
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha_Aux.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista_Aux.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo_Aux.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado_Aux.value = 0;
		
            return;
            			    
		}	
    NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha_Aux.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista_Aux.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo_Aux.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado_Aux.value = 0;
			
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Fecha_Aux.value = event.srcElement.parentElement.cells(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Vista_Aux.value = event.srcElement.parentElement.cells(1).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_TI_Porc_Plazo_Aux.value = event.srcElement.parentElement.cells(2).innerText;
            
            if (event.srcElement.parentElement.children(3).innerText == "ACTIVO" )
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado_Aux.value = 1;
		      }
		    else if (event.srcElement.parentElement.children(3).innerText == "INACTIVO")
		             
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_TI_Estado_Aux.value = 2;
		      }	
 	
	return;
	}
}    

//********************************Grilla Gastos*****************************************

//function SeleccionaFilaGrGastos(pTabla,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//				event.srcElement.parentElement.cells(i).className = sClass;
//			
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_HF_Codgto.value = '';
//            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Oculto.value = '';
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Sucursal.value = 0;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = 0;
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Mto_Gasto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Rd_Inactivo.checked = false;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Rd_activo.checked = false;
//		
//			return;
//            			    
//		}	
//    NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//		
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Codgto.value = '';
//	        //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Oculto.value = '';
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Sucursal.value = 0;
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = 0;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Mto_Gasto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
//			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_HF_Codgto.value = event.srcElement.parentElement.cells(0).innerText;
//			//document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Oculto.value = event.srcElement.parentElement.cells(0).innerText;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Sucursal.value = event.srcElement.parentElement.children(1).innerText;           
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = event.srcElement.parentElement.children(2).innerText;           
//// 
////          switch ( event.srcElement.parentElement.children(2).innerText )
////                { 
////	                case "POR OPERACION": document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = 1;
////			                break;
////	                case "POR DEUDOR": document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = 2;
////			                break;
////	                case "POR DOCUMENTO": document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = 3;
////			                break;
////	                case "POR DOCUMENTO ESPECIFICO": document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Gasto.value = 4;
////			                break;
////	            }
////                                  
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Mto_Gasto.value = event.srcElement.parentElement.cells(3).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = event.srcElement.parentElement.cells(4).innerText;
// 
//// 		    
// __doPostBack('ctl00$ContentPlaceHolder1$Link_Gto','');        
//	return;
//	}
//}    


function SeleccionaFilaGrGastos(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PosGto.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Codgto.value = '';
			return; 
		}

	var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
   //document.aspnetForm.ctl00_ContentPlaceHolder1_NroCuenta.value = event.srcElement.parentElement.cells(1).innerText;

    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_PosGto.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Codgto.value = codigo;
   // document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Des_Bco.value = event.srcElement.parentElement.cells(1).innerText;
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.className = 'clsMandatorio';
//    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
    
    __doPostBack('ctl00$ContentPlaceHolder1$Link_Gto','');        
	NormalClass(pTabla,pClass,jClass);
   J_RolClass(pClass);
	
	return;
	}
}


//*************************Grilla Ciudad************************************************

		
function SeleccionaGrCiudad(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			 document.aspnetForm.ctl00_ContentPlaceHolder1_Nro.value = 0;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCiudad.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_TxtDescripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal.value = '';
            
			return;
		}
		
	
	var id = IdGrilla(pTabla,sClass);
    document.aspnetForm.ctl00_ContentPlaceHolder1_Nro.value = id;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCiudad.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_TxtDescripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal.value = '';
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCiudad.value = event.srcElement.parentElement.cells(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_TxtDescripcion.value = event.srcElement.parentElement.cells(1).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal.value = event.srcElement.parentElement.cells(2).innerText;
 
            
            __doPostBack('ctl00$ContentPlaceHolder1$LnkB_GrillaComuna','');        
		NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	return;
	}
}


//*************************Grilla Comuna************************************************

		
function SeleccionaGrComuna(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com_Oculto.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = 0;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Com_Banco.value = 0;
                        
			return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	       
	     try
	     {
	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = '';
	        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com_Oculto.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = 0
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Com_Banco.value = 0;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Comuna.value = event.srcElement.parentElement.cells(0).innerText;
            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com.value = event.srcElement.parentElement.cells(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Com_Oculto.value = event.srcElement.parentElement.cells(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = event.srcElement.parentElement.cells(1).innerText;
            //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal.value = event.srcElement.parentElement.cells(2).innerText;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.value = event.srcElement.parentElement.children(3).innerText;
            
           document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.disabled = false;
           document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Zona_Rec.disabled = false;
           document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Com_Banco.disabled = false;
	       	             	                            
        }	                                                      	       
	  catch(e){alert(e.message);}
	       	       	       	            	            
	return;
	}
}


//*************************Grilla Cobranza************************************************

		
function SeleccionaGrCobranza(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Accion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Acc_Nvo.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCobranza.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCobranzaOculto.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_PlazoDias.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Prioridad.value = '';
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_FechaGest.checked = false;
            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_FechaVen.checked = false;
            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_GestionarSi.checked = false;
            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_GestionarNo.checked = false;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Chk_Generar.checked = false;
            NormalClass(pTabla, pClass, jClass);
            J_RolClass(pClass);
            var id = event.srcElement.parentElement.rowIndex;
            return;
            
		}
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	var id = event.srcElement.parentElement.rowIndex;
	window.document.forms[0].ctl00_ContentPlaceHolder1_pos.value = id;
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Accion.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Acc_Nvo.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCobranza.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCobranzaOculto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_PlazoDias.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Prioridad.value = '';
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_FechaGest.checked = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_FechaVen.checked = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_GestionarSi.checked = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_GestionarNo.checked = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Chk_Generar.checked = false;
//	
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCobranza.value =  event.srcElement.parentElement.children(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodCobranzaOculto.value =  event.srcElement.parentElement.children(0).innerText;
//            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value =  event.srcElement.parentElement.children(1).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Prioridad.value =  event.srcElement.parentElement.children(2).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_PlazoDias.value =  event.srcElement.parentElement.children(3).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Acc_Nvo.value =  event.srcElement.parentElement.children(4).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Accion.value =  event.srcElement.parentElement.children(5).innerText;
//	        
//            
//                                
//            //generar gestion
//            if (event.srcElement.parentElement.children(6).innerText == "N") 
//              {
//                document.aspnetForm.ctl00_ContentPlaceHolder1_Chk_Generar.checked = true;
//              }      
//            else if (event.srcElement.parentElement.children(6).innerText == "S") 
//              {
//                document.aspnetForm.ctl00_ContentPlaceHolder1_Chk_Generar.checked = false;
//              }
//              
//               //gestionar
//            if (event.srcElement.parentElement.children(7).innerText == "S") 
//              {
//                document.aspnetForm.ctl00_ContentPlaceHolder1_RB_GestionarSi.checked = true;
//              }
//            else if (event.srcElement.parentElement.children(7).innerText == "N") 
//              {
//                document.aspnetForm.ctl00_ContentPlaceHolder1_RB_GestionarNo.checked = true;
//              }
//		
//		    //Por Fecha
//		    if (event.srcElement.parentElement.children(8).innerText == "G") 
//              {
//                document.aspnetForm.ctl00_ContentPlaceHolder1_RB_FechaGest.checked = true;
//              }
//            else if (event.srcElement.parentElement.children(8).innerText == "V") 
//              {
//                document.aspnetForm.ctl00_ContentPlaceHolder1_RB_FechaVen.checked = true;
//              }

	__doPostBack('ctl00$ContentPlaceHolder1$Detalle', '');
	return true;
	}
}



//*************************Riesgo Adicional************************************************
	
function SeleccionaFilaRiesgo(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
                                                               
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
	          
	return;
	}
}




//*************************Sucursales************************************************

//selecciona indice de grilla seleccionada para dar estilo

function J_RolClass2(pTabla, pClass)
{ 
//alert(pTabla.rows.length);
   //for(var i=1;i<pTabla.rows.length;i++)
   //{
       var id = event.srcElement.parentElement.rowIndex;
   
       if(pTabla.rows(id).className=='clicktable')return;
       
       //if (event.srcElement.parentElement.children(0).className != pClass)
       pTabla.rows(id).className=pClass;
   //}
       return;
 }
    

function SeleccionaSucursal(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
	
        var idx = event.srcElement.parentElement.rowIndex;	
        
		if(pTabla.rows(idx).className==pClass)
		{
		    pTabla.rows(idx).className=sClass;
			//for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			//event.srcElement.parentElement.cells(i).className=sClass;
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion_corta.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_PlazaBanco.value = 0;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Cod_Region.value = 0;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Direccion.value = '';            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Telefono.value = '';            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Fax.value = '';
                                                               
            return;
		}
		
		
		
	        NormalClass(pTabla,pClass,jClass);
	        J_RolClass(pClass);
	
	        //RESCATO INDICE DE FILA SELECCIONADA
	        var id = IdGrilla(pTabla,pClass,0);   
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_IndSuc.value = id;
	
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.value = '';
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion_corta.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_PlazaBanco.value = 0;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Cod_Region.value = 0;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Direccion.value = '';            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Telefono.value = '';            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Fax.value = '';
            
            
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.disabled = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.disabled = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion_corta.disabled = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_PlazaBanco.disabled = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Cod_Region.disabled = false;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Direccion.disabled = false;          
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Telefono.disabled = false;          
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Fax.disabled = false;
                  
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.value =  event.srcElement.parentElement.children(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc.disabled = true;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_CodSuc_Oculto.value =  event.srcElement.parentElement.children(0).innerText;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value =  event.srcElement.parentElement.children(1).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion_corta.value =  event.srcElement.parentElement.children(2).innerText;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_PlazaBanco.value =  event.srcElement.parentElement.children(3).innerText;
           // document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Cod_Region.value =  event.srcElement.parentElement.children().innerText;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Direccion.value =  event.srcElement.parentElement.children(4).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Telefono.value =  event.srcElement.parentElement.children(5).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Fax.value =  event.srcElement.parentElement.children(6).innerText;
            
            
            __doPostBack('ctl00$ContentPlaceHolder1$LnkB_Llena_Combos','');      
            
	return;
	}
}


//*************************Grilla PLAZA************************************************
	
function SeleccionaPlaza(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Plaza_Suc.value = 0;
                                                                         
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Plaza_Suc.value = 0;
            
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Dias_Reten.value =  event.srcElement.parentElement.children(2).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Plaza_Suc.value =  event.srcElement.parentElement.children(0).innerText;
           
	return;
	}
}


//*************************Grilla CIUDAD************************************************
	
function SeleccionaCiudad(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Ciu.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Ciudad.value = 0;
                                                                         
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Ciu.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Ciudad.value = 0;
                        
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Ciu.value =  event.srcElement.parentElement.children(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Ciudad.value =  event.srcElement.parentElement.children(0).innerText;
           
	return;
	}
}



//*************************Grilla ZONAS************************************************
	
function SeleccionaZona(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal_Oculta.value = '';
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona_Oculto.value = '';
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Zona.value = '';
            
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal_Oculta.value = '';
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona.value = '';
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona_Oculto.value = '';
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Zona.value = '';
            
            
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = event.srcElement.parentElement.children(1).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal_Oculta.value = event.srcElement.parentElement.children(1).innerText;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona.value = event.srcElement.parentElement.children(0).innerText;
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona_Oculto.value = event.srcElement.parentElement.children(0).innerText;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Zona.value = event.srcElement.parentElement.children(2).innerText;
                        
           
	return;
	}
}



//*************************Mantencion cuentas  contables************************************************
	
function SeleccionaMantCtasCont(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Voucher.value = 0;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Voucher.disabled = false;
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Voucher.disabled = false;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Voucher.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Voucher.disabled = true;
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Tipo_Documentos.value = 0;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Doc.disabled = false;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Doc.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Doc.disabled = true;
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Cta_Cont.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Linea.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Val.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Glosa_Doc.value = '';
									
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
            document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Voucher.value = 0;
			document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Tipo_Documentos.value = 0;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Cta_Cont.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Linea.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Val.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Glosa_Doc.value = '';
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Voucher.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Voucher.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Tipo_Documentos.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Doc.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Cta_Cont.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Linea.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Val.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Glosa_Doc.disabled = true;
						
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Voucher.value = event.srcElement.parentElement.children(9).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Dp_Tipo_Voucher.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Voucher.value = event.srcElement.parentElement.children(1).innerText;
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Tipo_Documentos.value = event.srcElement.parentElement.children(10).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Doc.value = event.srcElement.parentElement.children(3).innerText;
						
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Cta_Cont.value = event.srcElement.parentElement.children(4).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Nro_Linea.value = event.srcElement.parentElement.children(5).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Tipo_Val.value = event.srcElement.parentElement.children(6).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Glosa_Doc.value = event.srcElement.parentElement.children(8).innerText;
			
			
            if (event.srcElement.parentElement.children(7).innerText == "DEBE" )
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Debe.checked = true;
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Haber.checked = false;
		      }
		    else if (event.srcElement.parentElement.children(7).innerText == "HABER")
		             
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Debe.checked = false;
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Haber.checked = true;
		      }	

          
	return;
	}
}


//*************************Plan de cuentas  contables************************************************
	
function SeleccionaPlanCtasCont(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			
//			document.aspnetForm.ctl00_ContentPlaceHolder1_Panel1.disabled = true;		    		    
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Panel4.disabled = true;
						
	        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Base_Num.checked = false;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Otro.checked = false;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_General.checked = false;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Individual.checked = false;
		    		    
		 	    
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Base_Num.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Otro.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_General.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Individual.disabled = true;
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Citib.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Desc.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod_Trans.value = '';
			
			            
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
//	        document.aspnetForm.ctl00_ContentPlaceHolder1_Panel1.disabled = true;		    		    
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Panel4.disabled = true;
            
            document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Base_Num.checked = false;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Otro.checked = false;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_General.checked = false;
		    document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Individual.checked = false;
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Citib.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Desc.value = '';
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod_Trans.value = '';
						
		
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Base_Num.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Otro.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_General.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Individual.disabled = true;
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Citib.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Desc.disabled = true;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod_Trans.disabled = true;
			
			
			
				
			
			
			if (event.srcElement.parentElement.children(4).innerText == "OTRO" )
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Base_Num.checked = false;
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Otro.checked = true;
		      }
		    else if (event.srcElement.parentElement.children(4).innerText == "B. NUMBER")
		             
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Base_Num.checked = true;
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Otro.checked = false;
		      }	



            if (event.srcElement.parentElement.children(2).innerText == "GENERAL" )
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_General.checked = false;
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Individual.checked = true;
		      }
		    else if (event.srcElement.parentElement.children(2).innerText == "INDIVIDUAL")
		             
		      {
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_General.checked = true;
		        document.aspnetForm.ctl00_ContentPlaceHolder1_RB_Individual.checked = false;
		      }	
			
			
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod.value = event.srcElement.parentElement.children(0).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Citib.value = event.srcElement.parentElement.children(3).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Desc.value = event.srcElement.parentElement.children(1).innerText;
			document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Plan_Cod_Trans.value = event.srcElement.parentElement.children(5).innerText;
			
			
			
			
           
	return;
	}
}


//*************************Cuentas  contables por cliente************************************************
	
function SeleccionaCtasContXCli(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal_Oculta.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona_Oculto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Zona.value = '';
            
            return;
		}
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
//		    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal_Oculta.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona_Oculto.value = '';
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Zona.value = '';

//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Suc.value = event.srcElement.parentElement.children(1).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Sucursal_Oculta.value = event.srcElement.parentElement.children(1).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona.value = event.srcElement.parentElement.children(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Cod_Zona_Oculto.value = event.srcElement.parentElement.children(0).innerText;
//            document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Des_Zona.value = event.srcElement.parentElement.children(2).innerText;
           
	return;
	}
}



//*************************************************************************
	
//function celClick(pTabla,pBtn,pClass,jClass,sClass)
//{
//	if(event.srcElement.parentElement.tagName=='TR')
//	{
//		if(event.srcElement.parentElement.cells(0).className==pClass)
//		{
//			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
//			event.srcElement.parentElement.cells(i).className=sClass;
//			
//			//BtnEnable(pBtn,true);

//			return;
//		}
//		
//	NormalClass(pTabla,pClass,jClass);
//	J_RolClass(pClass);
//	//BtnEnable(pBtn,false); 
//	return;
//	}
//}

//*************************************************************************


//function NormalClass(pTabla,pClass,jClass)
//{
//	for(var i=0;i<pTabla.rows.length;i++)
//	{
//		if(pTabla.rows(i).cells(0).className==pClass) 
//		{
//			pTabla.rows(i).className = jClass;
//			for(var j=0;j<pTabla.rows(i).cells.length;j++)
//			pTabla.rows(i).cells(j).className = jClass; 
//		}
//	} 
//	return;
//}


//*************************************************************************
//function J_RolClass(pClass)
//{ 
//	if(event.srcElement.parentElement.children(0).className=='clicktable')return;

//	    for(var i=0;i<event.srcElement.parentElement.children.length;i++)
//        event.srcElement.parentElement.children(i).className=pClass;
//	    
//    return;	
//}


//*************************************************************************

//function BtnEnable(pBtn,est)
//{
//	try
//	{
//		pBtn.disabled = est;
//	}
//	catch (er){alert(er);}

//}