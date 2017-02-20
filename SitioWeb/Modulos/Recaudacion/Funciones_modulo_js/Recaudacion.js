function TraspasoCollCampos(pTabla, caso, pClass, jClass, sClass)
{

	if(event.srcElement.parentElement.tagName=='TR')
	{
		if(event.srcElement.parentElement.cells(0).className==pClass)
		{
		return;
		}

	
	//Ejecuta Link Retorna Documentos según operación seleccionada
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
    //var id = IdGrilla(pTabla, 'clicktable', 0);
	var id = event.srcElement.parentElement.rowIndex;
	
	if (caso == 1) 
	{
	    window.document.forms[0].pos_deu.value = id;
	}
	if (caso == 2)
	
	 {
	    window.document.forms[0].pos_rec.value = id;
	}
	if (caso == 3)
	 {
	     window.document.forms[0].ctl00_ContentPlaceHolder1_pos_am.value = id;
	     window.document.forms[0].ctl00_ContentPlaceHolder1_pos_pm.value = "";
	     __doPostBack('ctl00$ContentPlaceHolder1$Desmarque', '');
	}

	if (caso == 4) {
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_am.value = "";
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_pm.value = id;
	    __doPostBack('ctl00$ContentPlaceHolder1$Desmarque', '');
	}
	if (caso == 5) {
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_anu.value = id;
	}
//	__doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

	return;
	}
}



function SelecionaPago(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {

        var pos = event.srcElement.parentElement.rowIndex;
        var id = event.srcElement.parentElement.cells(0).innerText

        if (pTabla.rows(pos).className == pClass) {
            pTabla.rows(pos).className = sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_tabpanel2_HF_Pos_DPO.value = '';
            return;

        }


        var pos = event.srcElement.parentElement.rowIndex;
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_tabpanel2_HF_Pos_DPO.value = pos;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        return;
    }
}



function SeleccionaDoctos(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {

        var pos = event.srcElement.parentElement.rowIndex;
        var id = event.srcElement.parentElement.cells(0).innerText

        if (pTabla.rows(pos).className == pClass) {
            pTabla.rows(pos).className = sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_HF_Pos_doc.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_HF_Pos_doc.value = pos;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        return;
    }
}

function CargaDetalle(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') 
    {

        var pos = event.srcElement.parentElement.rowIndex;
        var id = event.srcElement.parentElement.cells(0).innerText

        if (pTabla.rows(pos).className == pClass)
        
         {
            pTabla.rows(pos).className = sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_doc.value = '';
            __doPostBack('ctl00$ContentPlaceHolder1$Marca', '')
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_doc.value = pos;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');
        
        return;
    }
}

function marcagasto(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {

        var pos = event.srcElement.parentElement.rowIndex;
        var id = event.srcElement.parentElement.cells(0).innerText

        if (pTabla.rows(pos).className == pClass) {
            pTabla.rows(pos).className = sClass;
            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_doc.value = '';
         
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos_doc.value = pos;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        __doPostBack('ctl00$ContentPlaceHolder1$marcagrilla', '');

        return;
    }
}


function AceptaPlaza(grilla)
{
    //var rut = event.srcElement.parentElement.children(0).innerText; 
    //var rso = event.srcElement.parentElement.children(1).innerText; 
    
    var rut;
    var rso;
    var pClass = 'clicktable';
    
    for(var i=0;i<grilla.rows.length;i++)
    {
	    if(grilla.rows(i).className==pClass) 
	    {
		    dias = grilla.children(0).rows(i).cells(2).innerText;
		    
		    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Pza.value = dias;
		    
		    window.close();
		    
		    return;
	}
   }
    
	return;

}


function celClickSinBtnPza(pTabla, pClass, jClass, sClass) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            return false;

        }

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        //AceptaDeudor(pTabla);
        AceptaPlaza(pTabla);
        return false;

    }

}