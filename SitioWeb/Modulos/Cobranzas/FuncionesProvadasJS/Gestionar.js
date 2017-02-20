//function CelClick_GV_DEUCLI(pTabla, pClass, jClass, sClass) 
//{
//    if (event.srcElement.parentElement.tagName == 'TR') {
//        if (event.srcElement.parentElement.cells(0).className == pClass) {

//            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
//                event.srcElement.parentElement.cells(i).className = sClass;


//            return;

//        }

//        window.document.forms[0].ID_GV_DEUCLI.value = Pos; //event.srcElement.parentElement.rowIndex;
//        
//        NormalClass(pTabla, pClass, jClass);
//        J_RolClass_Tabla(pTabla, pClass)
//        __doPostBack('Busqueda_GV_DEUCLI', '');
//        
//        return;
//        
//    }
//}


function CelClick_GV_DEUCLI(pTabla, pClass, jClass, sClass, Pos) 
{
    window.document.forms[0].ID_GV_DEUCLI.value = Pos; //event.srcElement.parentElement.rowIndex;
    //__doPostBack('Busqueda_GV_DEUCLI', '');

    return;

}
function ClickContacto() {

    var i = event.srcElement.parentElement.rowIndex;
    //alert(pTabla.children(0).rows(i).cells(1).innerText);
    return;
    
}

function CelClick_GV_CLI_DEUASOCIADO(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;


            return;

        }

        window.document.forms[0].ID_GV_CLI_DEUASOCIADO.value = event.srcElement.parentElement.rowIndex;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass)

        __doPostBack('Busqueda_GV_CLI_DEUASOCIADO', '');

        return;

    }
}


function CelClick_GV_Contactos(pTabla, pClass, jClass, sClass) {
    /*
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;


            return;

        }

        window.document.forms[0].ID_GV_Contactos.value = event.srcElement.parentElement.rowIndex - 1 ;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass)
        __doPostBack('Busqueda_GV_Contactos', '');

        return;

    }
    */
}


function CelClick_GV_Doctos(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;


            return;

        }

        window.document.forms[0].ID_GV_Doctos.value = event.srcElement.parentElement.rowIndex;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass)
        __doPostBack('Busqueda_GV_Doctos', '');

        return;

    }
}

