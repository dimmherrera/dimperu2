//function DoScroll_()
// {
//    var _gridView = document.getElementById("Div_Operaciones");
//    var _header = document.getElementById("HeaderDiv_Operaciones");
//    _header.scrollLeft = _gridView.scrollLeft;

//}

function Aprobacion(Posicion) 
{
    
    document.getElementById("ctl00_ContentPlaceHolder1_HF_PosNeg").value = Posicion;
    return;

}

function ClickClasificacion(pTabla, pClass, jClass, sClass) {


    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].HF_NroNeg.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var nro = event.srcElement.parentElement.cells(0).innerText;
        var ccf = event.srcElement.parentElement.cells(3).innerText;

        window.document.forms[0].HF_NroNNC.value = nro;
        window.document.forms[0].HF_PosNNC.value = pos;
        window.document.forms[0].HF_NroCCF.value = ccf;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('Lb_buscar_frm', '');
        return;
    }
}