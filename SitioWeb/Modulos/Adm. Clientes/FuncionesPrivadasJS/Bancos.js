function NewBanco(url,pWidth,pHeight,pLeft,pTop)
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


function celClickDosBanco(pTabla,pClass,jClass,sClass)

{
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;

            window.document.forms[0].CodBco.value = '';
            window.document.forms[0].HF_Pos.value = '';
            
			return;

		}

		window.document.forms[0].CodBco.value = event.srcElement.parentElement.cells(3).innerText;
		window.document.forms[0].HF_Pos.value = event.srcElement.parentElement.rowIndex;
		
	    NormalClass(pTabla,pClass,jClass);
	    J_RolClass_Tabla(pTabla, pClass);
    	
	    __doPostBack('ActBancos', '');
    	
	    return;
	
	}
}

/*
function celClickBanco(pTabla,pBtn,pClass,jClass,sClass)

{
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			BtnEnable(pBtn,true);

			return;

		}
	
	window.document.forms[0].Txt_Cod_Bco.readOnly= false;	
	window.document.forms[0].DP_Bancos.disabled= false;	
	window.document.forms[0].DP_Sucursales.disabled= false;
	window.document.forms[0].Txt_Cta_Cte.readOnly= false;
	window.document.forms[0].CB_Deposito.disabled= false;
		
	window.document.forms[0].Txt_Cod_Bco.className= "clsTxt";	
	window.document.forms[0].DP_Bancos.className= "clsTxt";	
	window.document.forms[0].DP_Sucursales.className= "clsTxt";	
	window.document.forms[0].Txt_Cta_Cte.className= "clsTxt";	
	
	
	var Dep = event.srcElement.parentElement.cells(0).innerText;
	
	
	if (Dep == 'S') 
	    {window.document.forms[0].CB_Deposito.checked =  true;}
	else
	    {window.document.forms[0].CB_Deposito.checked =  false;}
	
	
	window.document.forms[0].Txt_Cod_Bco.value = event.srcElement.parentElement.cells(1).innerText;
	window.document.forms[0].DP_Bancos.value = event.srcElement.parentElement.cells(1).innerText;
	window.document.forms[0].Txt_Cta_Cte.value = event.srcElement.parentElement.cells(4).innerText;
	
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass(pClass);
	BtnEnable(pBtn,false);
	__doPostBack('DP_Bancos_SelectedIndexChanged','');	
	return;
	}
}
*/

function openseleccionDosBanco(grilla,url,pWidth,pHeight,pLeft,pTop)
{
var id;
var pClass = 'clicktable';
var rut = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.value;



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
