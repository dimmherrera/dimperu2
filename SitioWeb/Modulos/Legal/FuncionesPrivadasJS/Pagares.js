
function ClickPagare(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
		
		 
		//	for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
		event.srcElement.parentElement.cells(i).className=sClass;
//			document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = posicion;
//			
			return;
		}

    var posicion = event.srcElement.parentElement.rowIndex;
    var codigo = event.srcElement.parentElement.cells(0).innerText;
//     document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = event.srcElement.parentElement.cells(0).innerText;
//     document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.Value = event.srcElement.parentElement.cells(13).innerText;

   
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
    document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Id.value = codigo;
//        
   __doPostBack('ctl00$ContentPlaceHolder1$Link_Gr','');        
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
	return;
	}
}