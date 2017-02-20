
function SelecionaDocto(pTabla,pClass,jClass,sClass,Posicion)
{
      
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			
			return false;

		}
		
    window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_HF_Posicion.value = Posicion;
    NormalClass(pTabla, pClass, jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
    return;
}

function CB_CriterioDeudor()
{

    var cb = window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_CB_Deudor.checked;
    
    if (cb)
    {
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Rut_Deu.className = 'clsMandatorio';
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Dig_Deu.className = 'clsMandatorio';
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Rut_Deu.readOnly = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Dig_Deu.readOnly = false;
      
    }
    else
    {
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Rut_Deu.className = 'clsDisabled';
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Dig_Deu.className = 'clsDisabled';
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Rut_Deu.readOnly = true;
        window.document.forms[0].ctl00_ContentPlaceHolder1_WC_QuePaga1_Txt_Dig_Deu.readOnly = true;
    }
    
    return;
    
 }