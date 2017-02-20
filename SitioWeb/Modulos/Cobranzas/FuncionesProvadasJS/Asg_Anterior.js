//Archivo JS
function Check_FechasAsignacion()
{
    if (window.document.forms[0].CB_FechaAsignacion.checked == true)
    {
        window.document.forms[0].TxtFechaAsig1.disabled = false;
        window.document.forms[0].TxtFechaAsig2.disabled = false;
        window.document.forms[0].TxtFechaAsig1.className = "clsMandatorio";
        window.document.forms[0].TxtFechaAsig2.className = "clsMandatorio";
     }
     else
     {
        window.document.forms[0].TxtFechaAsig1.disabled = true;
        window.document.forms[0].TxtFechaAsig2.disabled = true;
        window.document.forms[0].TxtFechaAsig1.className = "clsDisabled";
        window.document.forms[0].TxtFechaAsig2.className = "clsDisabled";
     }
    return true;
}

function Check_Deudor()
{
    if (window.document.forms[0].CB_Deudores.checked == true)
    {
        window.document.forms[0].Txt_Rut.disabled = false;
        window.document.forms[0].Txt_Dig.disabled = false;
        window.document.forms[0].Txt_Rut.className = "clsMandatorio";
        window.document.forms[0].Txt_Dig.className = "clsMandatorio";
     }
     else
     {
        window.document.forms[0].Txt_Rut.disabled = true;
        window.document.forms[0].Txt_Dig.disabled = true;
        window.document.forms[0].Txt_Rut.className = "clsDisabled";
        window.document.forms[0].Txt_Dig.className = "clsDisabled";
     }
    return true;
}