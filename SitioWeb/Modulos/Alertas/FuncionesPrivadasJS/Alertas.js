function DoScroll_Lineas()
 {
    var _gridView = document.getElementById("Div_Lineas");
    var _header = document.getElementById("HeaderDiv_Lineas");
    _header.scrollLeft = _gridView.scrollLeft;
 }

function DoScroll_Exc()
 {
    var _gridView = document.getElementById("GridView_Exc");
    var _header = document.getElementById("HeaderDiv_Exc");
    _header.scrollLeft = _gridView.scrollLeft;
 }

 function DoScroll_Vencidos()
 {
    var _gridView = document.getElementById("GridView_Ven");
    var _header = document.getElementById("HeaderDiv_Ven");
    _header.scrollLeft = _gridView.scrollLeft;
 }
 
 function DoScroll_Mora()
 {
    var _gridView = document.getElementById("GridView_Mora");
    var _header = document.getElementById("HeaderDiv_Mora");
    _header.scrollLeft = _gridView.scrollLeft;
 }
 
 function Click_GV_VB(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			
			window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroAplicacion.value = '';
			window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Aprobar.disabled= false;
	        window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Rechazar.disabled= false;
	        return;

		}
	
	var nro = event.srcElement.parentElement.cells(13).innerText;
	
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroAplicacion.value = nro;
	//window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Aprobar.disabled= false;
	//window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Rechazar.disabled= false;
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	}
}
