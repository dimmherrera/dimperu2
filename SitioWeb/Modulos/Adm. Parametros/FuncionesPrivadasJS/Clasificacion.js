function DetalleCfc(pTabla, pClass, jClass, sClass) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;
            document.aspnetForm.ctl00_ContentPlaceHolder1_txt_pos.value = '';
            return;

        }

        //var id = IdGrilla(pTabla, sClass);
        var id = event.srcElement.parentElement.rowIndex;
        document.aspnetForm.ctl00_ContentPlaceHolder1_txt_pos.value = id;
        __doPostBack('ctl00$ContentPlaceHolder1$LB_CargaDetalle', '');

        NormalClass(pTabla, pClass, jClass);
        J_RolClass(pClass);


  
        
        return;
    }
}


function DetalleCcf(pTabla, pClass, jClass, sClass) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;
            document.aspnetForm.ctl00_ContentPlaceHolder1_pos.value = '';
            return;

        }

        //var id = IdGrilla(pTabla, pClass, 0);

        var id = event.srcElement.parentElement.rowIndex;
        
        document.aspnetForm.ctl00_ContentPlaceHolder1_pos.value = id;
        __doPostBack('ctl00$ContentPlaceHolder1$LB_Marca_Grilla', '');

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        return;
    }
}

function DenegaAcceso() {

    // __doPostBack('ctl00$ContentPlaceHolder1$LB_Marca_Grilla', '');
     //__doPostBack('ctl00_ContentPlaceHolder1_acceso', '');
    __doPostBack('ctl00$ContentPlaceHolder1$acceso', '');
    return;
}
    