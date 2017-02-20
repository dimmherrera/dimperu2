function NewEmpresa(url,pWidth,pHeight,pLeft,pTop)
{	
	
	var rut = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.value;
	
	rut = String(rut);
	rut = rut.replace('.','');
	rut = rut.replace('.','');
    if (rut == '')
	{
	    alert('Debe Ingresar NIT del Cliente');
	}	
	else
	{
	    var x=window.showModalDialog(url + '?rut=' + rut, window, 'scroll:no;status:off;dialogWidth:'+pWidth+'px;dialogHeight:'+pHeight+'px;dialogLeft:'+pLeft+'px;dialogTop:'+pTop+'px'); 
	}
	return;			
}

function celClickDosEmpresa(pTabla,pClass,jClass,sClass)

{
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;


            window.document.forms[0].RutEmp.value = '';
            window.document.forms[0].sw.value = '';
            
			return;

		}

		var rut = event.srcElement.parentElement.cells(2).innerText;
		var pos = event.srcElement.parentElement.rowIndex;		
		
		var r = rut.substring(0, rut.search('-'));
	
	    rut = r.replace('.','')
	    rut = rut.replace('.','')

	    window.document.forms[0].RutEmp.value = rut;
	    window.document.forms[0].sw.value = 'UPDATE';
	    window.document.forms[0].HF_Pos.value = pos;
	        		
	    NormalClass(pTabla,pClass,jClass);
	    J_RolClass_Tabla(pTabla, pClass);
	    __doPostBack('ActEmpresa', '');
	    
	    return;
	}
}

function openseleccionDosEmpresa(grilla,url,pWidth,pHeight,pLeft,pTop)
{
var id;
var pClass = 'clicktable';
var rut = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.value;
rut = String(rut);
rut = rut.replace('.','');

for(var i=0;i<grilla.rows.length;i++)
{
	if(grilla.rows(i).cells(0).className==pClass) 
	{
		id = i;
	}
}

var x=window.showModalDialog(url + '?rut=' + rut + "&id=" + id, window, 'scroll:no;status:off;dialogWidth:'+pWidth+'px;dialogHeight:'+pHeight+'px;dialogLeft:'+pLeft+'px;dialogTop:'+pTop+'px'); 

return;

}