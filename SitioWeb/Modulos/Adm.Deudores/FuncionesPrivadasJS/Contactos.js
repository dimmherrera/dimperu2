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
			__doPostBack('LnkBtnLimpiaCnc','');
        	return;
		}
    NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	
    var Id = IdGrilla(pTabla,'clicktable',0);
    window.document.form1.TxtNro.value = Id;

	__doPostBack('LnkBtnCnc','');
   	return;
   	}
}