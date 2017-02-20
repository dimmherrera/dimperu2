function CelClick_GV_Verificacion(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;
                

            return;

        }

        for(var i=0;i<pTabla.rows.length;i++)
        {
	        if(pTabla.rows(i).cells(1).className == sClass) 
	        
	        {
	            var Rut = pTabla.children(0).rows(i).cells(0).innerText;
	            var r = Rut.substring(0, Rut.search('-'));
		        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Id_Deudor.value = r;
		        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Nom_Deudor.value = pTabla.children(0).rows(i).cells(1).innerText;
	        }
        }
        
        //window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Id_Deudor.value = event.srcElement.parentElement.rowIndex;
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass)
        //__doPostBack('Busqueda_GV_DEUCLI', '');
        
        return;
        
    }
}

function CelClick_GV_DoctosDvf(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            return;
        }
        
        window.document.forms[0].Txt_PosGV.value = event.srcElement.parentElement.rowIndex;
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass)
       
        return;
    }
}


function CelClick_GV_Doctosprueba(pTabla, pClass, jClass, sClass) {
   var posicion = event.srcElement.parentElement.rowIndex;
    
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;
              
            return;
        }
        
        //window.document.forms[0].Txt_PosGV.value = event.srcElement.parentElement.rowIndex;
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass)
         window.document.forms[0].HF_pos_grilla.value = posicion;
         __doPostBack('lb_id_doc','');
        return;
    }
}