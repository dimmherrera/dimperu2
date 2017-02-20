
function Check_Reemplazo() {
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos_Reemplazo.length == 1) {
        alert('No Existen Cobradores para Reemplazo');
        window.document.forms[0].ctl00_ContentPlaceHolder1_CB_Reemplazo.checked = false ;
    }
    else
    {
    
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_CB_Reemplazo.checked == true) {
        window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos_Reemplazo.disabled = false;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos_Reemplazo.className = "clsMandatorio";
    }
    else {
        window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos_Reemplazo.disabled = true;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Dp_Ejecutivos_Reemplazo.className = "clsDisabled";
    }
}
    return true;
}