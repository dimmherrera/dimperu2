function ClickVBSolicitud(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value = '';
            return;

        }

        var nro = event.srcElement.parentElement.rowIndex;


        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value = nro;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        __doPostBack('ctl00$ContentPlaceHolder1$Busqueda_GV_SOLICITUD', '');
        
        return;
    }
}