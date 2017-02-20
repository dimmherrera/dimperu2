function DetalleDeuComercial(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			
			window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.value = '';
	        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.value = '';
	        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rso_Deu.value = '';
        	
	        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Eva.value = '';
	        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.value = '';

	        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos.value = '';
	        
	        //window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Eva.className = 'clsDisabled';
	        //window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Eva.readOnly = true;
        	
	        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.readOnly= true;
	        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.className = 'clsDisabled';
			return;

		}
	
	var rut = event.srcElement.parentElement.cells(0).innerText;
	var nom = event.srcElement.parentElement.cells(1).innerText;
	
	var eva = event.srcElement.parentElement.cells(4).innerText;
	var doc = event.srcElement.parentElement.cells(5).innerText;
	
	var r = rut.substring(0, rut.search('-'));
	var dig = rut.substring(rut.search('-') + 1, rut.length);

	var pos = event.srcElement.parentElement.rowIndex;
	
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.value = r;
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.value = dig;
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rso_Deu.value = nom;
	
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Eva.value = eva;
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.value = doc;

	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos.value = pos;
	
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.className = 'clsMandatorio';
	window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.readOnly = false;
	
	//window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Por_Ant.className = 'clsMandatorio';
	//window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Por_Ant.readOnly = false;

	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	return;
	}
}


function MontoEvaluado()
{
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Por_Ant.value == 0 || window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Por_Ant.value == '')
    {return;}
    
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.value == 0 || window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.value == '')
    {return;}
    
    var por = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Por_Ant.value;
    var mon = window.document.forms[0].ctl00_ContentPlaceHolder1_DP_TipoMoneda.value;
    var mto = parseFloat(truncaNUM(window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Doc.value));
    var eva;
    
    por = por.replace(',','.');
    eva = (mto * (por / 100));
    
       switch (mon)
       {
        //case '1': { eva = round(eva); break }
        case '1': { eva = Math.round(eva); break }
        case '2': { eva = round(eva, 4); break }
        case '2': { eva = round(eva, 2); break }
        case '3': { eva = round(eva, 2); break }
       }
       
    
    eva = new String(eva);
    eva = eva.replace('.', ',');

    window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Mto_Eva.value = FormatMil(eva);
    //le da el foco al boton agregar
    document.getElementById('ctl00_ContentPlaceHolder1_IB_AgregarDeu').focus();
       
    return;
}

function ClickEvaluacion(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Eva.value = '';
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion.value = '';
            return;

        }

        var nro = event.srcElement.parentElement.cells(0).innerText;
        var pos = event.srcElement.parentElement.rowIndex;

        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Eva.value = nro;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion.value = pos;
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        return;
    }
}

function ClickEvaluacion2(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].HF_Nro_Eva.value = '';
            window.document.forms[0].HF_Posicion.value = '';
            return;

        }

        var nro = event.srcElement.parentElement.cells(0).innerText;
        var pos = event.srcElement.parentElement.rowIndex;

        window.document.forms[0].HF_Nro_Eva.value = nro;
        window.document.forms[0].HF_Posicion.value = pos;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        return;
    }
}

