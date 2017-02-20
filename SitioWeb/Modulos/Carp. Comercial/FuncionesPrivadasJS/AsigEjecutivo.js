

// Archivo JScript
function Check_Eje()
{
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_CB_Eje.checked == true)
    {
    window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos.disabled = false;
    window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos.className = "clsMandatorio";
    }
    else
    {
    window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos.disabled = true;
    window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos.className = "clsDisabled";
    }
    return true;
}

function Check_Cli()
{
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_CB_Cli.checked == true)
    {
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.readOnly = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig.readOnly = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.className = "clsMandatorio";
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig.className = "clsMandatorio";
     }
     else
     {
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.readOnly = true;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig.readOnly = true;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.className = "clsDisabled";
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig.className = "clsDisabled";
     }
    return true;
}

function CelEje(pTabla,pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			return;
		}

	window.document.forms[0].ctl00_ContentPlaceHolder1_TxtCodEje.value = event.srcElement.parentElement.cells(0).innerText;
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	return;
	}
}
