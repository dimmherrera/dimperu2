function SelecionaDocto(Posicion)
{
    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion.value = Posicion;
    return;
}

function SelecionaCxc(Posicion)
{
    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion_CxC.value = Posicion;
    return;
}

function SelecionaExc(Posicion)
{
    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion_Exc.value = Posicion;
    return;
}

function SelecionaCxp(Posicion_CxP) {
    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion_CxP.value = Posicion_CxP;
    return;
}

function SelecionaDnc(Posicion_DNC) {
    window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Posicion_DNC.value = Posicion_DNC;
    return;
}

function CB_CriterioDeudor()
{

    var cb = window.document.forms[0].ctl00_ContentPlaceHolder1_CB_Deudor.checked;
    
    if (cb)
    {
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.className = 'clsMandatorio';
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.className = 'clsMandatorio';
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.readOnly = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.readOnly = false;
      
    }
    else
    {
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.className = 'clsDisabled';
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.className = 'clsDisabled';
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.readOnly = true;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.readOnly = true;
    }
    
    return;
    
 }
 
function DoScroll()
 {
    var _gridView = document.getElementById("GridViewDiv");
    var _header = document.getElementById("HeaderDiv");
    _header.scrollLeft = _gridView.scrollLeft;
 }

function DoScroll_Exc()
 {
    var _gridView = document.getElementById("GridView_Exc");
    var _header = document.getElementById("HeaderDiv_Exc");
    _header.scrollLeft = _gridView.scrollLeft;
 }

 function DoScroll2()
 {
    var _gridView = document.getElementById("GridViewDiv2");
    var _header = document.getElementById("HeaderDiv2");
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
	var rut = event.srcElement.parentElement.cells(0).innerText
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_NroAplicacion.value = nro;
	window.document.forms[0].ctl00_ContentPlaceHolder1_HF_CLI.value = rut;
	//window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Aprobar.disabled= false;
	//window.document.forms[0].ctl00_ContentPlaceHolder1_IB_Rechazar.disabled= false;
		
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	__doPostBack('ctl00$ContentPlaceHolder1$Lb_informe', '');
	//__DoPostback('', 'window.document.forms[0].ctl00_ContentPlaceHolder1$Lb$informe.value')
	return;
	}
}
