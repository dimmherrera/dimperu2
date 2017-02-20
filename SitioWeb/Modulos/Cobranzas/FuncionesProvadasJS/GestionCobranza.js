function CelClick_GV_DEUCLI(Pos) {
    window.document.forms[0].ID_GV_DEUDOR.value = Pos;
    window.document.forms[0].ID_GV_CLIENTE.value = '';
    window.document.forms[0].ID_GV_Doctos.value = '';
    return;
}

function CelClick_GV_DEUDOR(Pos, Rut) {

    window.document.forms[0].ID_GV_DEUDOR.value = Pos;
    window.document.forms[0].ID_GV_CLIENTE.value = '';
    window.document.forms[0].ID_GV_Doctos.value = '';
    var x = window.showModalDialog('../../Contactos/Contactos.aspx?Rut=' + Rut + '&RefrescarModal=1', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:630px;dialogLeft:400px;dialogTop:200px');
    return;
}

function CelClick_GV_CLIENTE(Pos) {
    window.document.forms[0].ID_GV_CLIENTE.value = Pos;
    return;
}

function CelClick_GV_Contactos(pos, fila, id) {

   window.document.forms[0].ID_GV_DEUDOR.value = pos;
   window.document.forms[0].ID_GV_Contactos.value = fila;
   window.document.forms[0].ID_Contacto.value = id;
   __doPostBack('Busqueda_GV_Contactos', '');
   
   return;
}


function CelClick_GV_Doctos(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;


            return;

        }

        window.document.forms[0].ID_GV_Doctos.value = event.srcElement.parentElement.rowIndex - 1;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_TablaCob(pTabla, pClass)
        //Busqueda_GV_Doctos.click();
        //__doPostBack('Busqueda_GV_Doctos', '');

        return;

    }
}

function J_RolClass_TablaCob(pTabla, pClass) {

    var id = event.srcElement.parentElement.rowIndex;

    if (pTabla.rows(id).className == 'clicktableCob') return;

    pTabla.rows(id).className = pClass;

    return;

}