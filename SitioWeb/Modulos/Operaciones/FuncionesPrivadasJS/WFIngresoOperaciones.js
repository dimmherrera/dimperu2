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

	id = id;
	
	if (caso == 1) 
	{
	    window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemOPE.value = id;
	}
	if (caso == 2)
	
	 {
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_sim.value = id;
	}
	if (caso == 3)
	 {
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_otg.value = id;
	}

	if (caso == 4) {
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_pag.value = id;
	}
	if (caso == 5) {
	    window.document.forms[0].ctl00_ContentPlaceHolder1_pos_anu.value = id;
	}
	__doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

	return;
	}
}

//function TraspasoCollCamposSimu(pTabla, pClass, jClass, sClass) {

//    if (event.srcElement.parentElement.tagName == 'TR') {
//        if (event.srcElement.parentElement.cells(0).className == pClass) {
//            return;
//        }


//        //Ejecuta Link Retorna Documentos según operación seleccionada
//        NormalClass(pTabla, pClass, jClass);
//        J_RolClass_Tabla(pTabla, pClass);
//        //var id = IdGrilla(pTabla, 'clicktable', 0);
//        var id = event.srcElement.parentElement.rowIndex;
//        window.document.forms[0].ctl00_ContentPlaceHolder1_pos_sim.value = id;

//        __doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

//        return;
//    }
//}
//function TraspasoCollCamposOtg(pTabla, pClass, jClass, sClass) {

//    if (event.srcElement.parentElement.tagName == 'TR') {
//        if (event.srcElement.parentElement.cells(0).className == pClass) {
//            return;
//        }


//        //Ejecuta Link Retorna Documentos según operación seleccionada
//        NormalClass(pTabla, pClass, jClass);
//        J_RolClass_Tabla(pTabla, pClass);
//        //var id = IdGrilla(pTabla, 'clicktable', 0);
//        var id = event.srcElement.parentElement.rowIndex;
//        window.document.forms[0].ctl00_ContentPlaceHolder1_pos_otg.value = id;

//        __doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

//        return;
//    }
//}
//function TraspasoCollCamposPag(pTabla, pClass, jClass, sClass) {

//    if (event.srcElement.parentElement.tagName == 'TR') {
//        if (event.srcElement.parentElement.cells(0).className == pClass) {
//            return;
//        }


//        //Ejecuta Link Retorna Documentos según operación seleccionada
//        NormalClass(pTabla, pClass, jClass);
//        J_RolClass_Tabla(pTabla, pClass);
//        //var id = IdGrilla(pTabla, 'clicktable', 0);
//        var id = event.srcElement.parentElement.rowIndex;
//        window.document.forms[0].ctl00_ContentPlaceHolder1_pos_pag.value = id;

//        __doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

//        return;
//    }
//}





function TraspasoCollDSICampos(pTabla,pClass,jClass,sClass)
{
    function TraspasoCollCampos(pTabla, pClass, jClass, sClass) {

        if (event.srcElement.parentElement.tagName == 'TR') {
            if (event.srcElement.parentElement.cells(0).className == pClass) {
                return;
            }


            //Ejecuta Link Retorna Documentos según operación seleccionada
            NormalClass(pTabla, pClass, jClass);
            J_RolClass_Tabla(pTabla, pClass);
            //var id = IdGrilla(pTabla, 'clicktable', 0);
            var id = event.srcElement.parentElement.rowIndex;
            window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemOPE.value = id;

            __doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

            return;
        }
    }
	if(event.srcElement.parentElement.tagName=='TR')
	{
	    function TraspasoCollCampos(pTabla, pClass, jClass, sClass) {

	        if (event.srcElement.parentElement.tagName == 'TR') {
	            if (event.srcElement.parentElement.cells(0).className == pClass) {
	                return;
	            }


	            //Ejecuta Link Retorna Documentos según operación seleccionada
	            NormalClass(pTabla, pClass, jClass);
	            J_RolClass_Tabla(pTabla, pClass);
	            //var id = IdGrilla(pTabla, 'clicktable', 0);
	            var id = event.srcElement.parentElement.rowIndex;
	            window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemOPE.value = id;

	            __doPostBack('ctl00$ContentPlaceHolder1$RetornaDoctos', '');

	            return;
	        }
	    } if (event.srcElement.parentElement.cells(0).className == pClass)
		{
			window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemDSI.value = "";
			return;
		}

    
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);

    //var id = IdGrilla(pTabla, 'clicktable', 0);
    var id = event.srcElement.parentElement.rowIndex;
    window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemDSI.value = id;
//    window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Grid.value=event.srcElement.parentElement.cells(0).innerText;
//    window.document.forms[0].ctl00_ContentPlaceHolder1_Btn_ModDoc.disabled = false;
//    window.document.forms[0].ctl00_ContentPlaceHolder1_Btn_EliDoc.disabled = false;


	return;
	}
}

function OpenPopupModDoctos(url,pWidth,pHeight,pLeft,pTop)
{
    var vItemDSI = document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_ItemDSI.value;
    vItemDSI = truncaNUM(vItemDSI) + 1;
	var vItemOPE = document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_ItemOPE.value ;
	vItemOPE = truncaNUM(vItemOPE) + 1;
	if (vItemDSI != 0)
	{
	    /*Asigna Valor para que al cargar pantalla refresque collection DSI*/
	    document.aspnetForm.ctl00_ContentPlaceHolder1_NoSeleccion.value = "";
	    document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_RefrescaCollDSI.value = 1
    	
	    var x=window.showModalDialog(url + '?itemDSI=' + vItemDSI + '&itemOPE=' + vItemOPE , window, 'scroll:no;status:off;dialogWidth:'+pWidth+'px;dialogHeight:'+pHeight+'px;dialogLeft:'+pLeft+'px;dialogTop:'+pTop+'px'); 
	 
	 }
	 else
	 {
	 document.aspnetForm.ctl00_ContentPlaceHolder1_NoSeleccion.value = 1;
	 //__doPostBack('ctl00_ContentPlaceHolder1_Lb_mod','');
	 }
	  
	 document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_ItemDSI.value = "";
	 
     return false;
	 
}

function OpenPopupIngDoctos(url,pWidth,pHeight,pLeft,pTop)
{	
	var vItemDSI = 0;
	var vItemOPE=document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_ItemOPE.value ;
	vItemOPE = truncaNUM(vItemOPE) + 1
	/*Asigna Valor para que al cargar pantalla refresque collection DSI*/
	document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_RefrescaCollDSI.value = 1

	//var x = window.showModalDialog(url + '?itemDSI=' + vItemDSI + '&itemOPE=' + vItemOPE, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
	var x = window.showModalDialog(url + '?itemDSI=' + vItemDSI + '&itemOPE=' + vItemOPE, window, 'scroll:yes;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px'); 
	
	//return;
}

function OpenPopupModOpe(url, pWidth, pHeight, pLeft, pTop) {
    var vItemDSI = 0;
    var vItemOPE = document.aspnetForm.ctl00_ContentPlaceHolder1_pos_otg.value;



    var x = window.showModalDialog(url + '?itemDSI=' + vItemDSI + '&itemOPE=' + vItemOPE, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
    return;
}


function OpenPopupGastos(url,pWidth,pHeight,pLeft,pTop)
{	
	/*Asigna Valor para que al cargar pantalla refresque collection DSI*/
	var x=window.showModalDialog(url , window, 'scroll:no;status:off;dialogWidth:'+pWidth+'px;dialogHeight:'+pHeight+'px;dialogLeft:'+pLeft+'px;dialogTop:'+pTop+'px'); 
	return;
}

function OpenPopupDeudorConProblemas(url,pWidth,pHeight,pLeft,pTop)
{	
	if (document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_ItemDeudorProblemas.value == 1)	
    { 
	 var x=window.showModalDialog(url, window, 'scroll:no;status:off;dialogWidth:'+pWidth+'px;dialogHeight:'+pHeight+'px;dialogLeft:'+pLeft+'px;dialogTop:'+pTop+'px'); 
	}
	return;
}


function TraspasoCollCamposSim(pTabla, pClass, jClass, sClass) {

    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {
            //for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
            //event.srcElement.parentElement.cells(i).className=sClass;

            return;
        }
        //document.aspnetForm.ctl00_ContentPlaceHolder1



        //Ejecuta Link Retorna Documentos según operación seleccionada
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        //var id = IdGrilla(pTabla, 'clicktable', 0);
        var id = event.srcElement.parentElement.rowIndex;

//        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemOPE.value = id + 1;
        window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_ItemOPE.value = id;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_N_Ope.value = event.srcElement.parentElement.cells(0).innerText;
        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Pos.values = id
        __doPostBack('ctl00$ContentPlaceHolder1$retorna', '');
	
	/*	
	//TextBox
	window.document.forms[0].Txt_FecIngreso.value = ope_fec;
	//window.document.forms[0].Txt_FecIngreso.className = 'clstxt';
	//window.document.forms[0].Txt_FecIngreso.readOnly = false;
	
	//window.document.forms[0].Txt_CanDocumentos.value = ope_can_doc;
	window.document.forms[0].Txt_CanDocumentos.className = 'clstxt';	
	window.document.forms[0].Txt_CanDocumentos.readOnly = false;
	window.document.forms[0].Txt_MontoFinanciar.value = ope_mto_doc;
	window.document.forms[0].Txt_MontoFinanciar.className = 'clstxt';	
	window.document.forms[0].Txt_MontoFinanciar.readOnly = false;
	
	//DropList
	window.document.forms[0].DP_TipoOperacion.value = pnu_tip_ope;
	window.document.forms[0].DP_TipoOperacion.disabled = false;
	window.document.forms[0].DP_TipoOperacion.className = 'clstxt';
	window.document.forms[0].DP_TipoDocumento.value = pnu_tip_doc;
	window.document.forms[0].DP_TipoDocumento.disabled = false;
	window.document.forms[0].DP_TipoDocumento.className = 'clstxt';
	window.document.forms[0].DP_Moneda.value = pnu_mon;
	window.document.forms[0].DP_Moneda.disabled = false;
	window.document.forms[0].DP_Moneda.className = 'clstxt';
	
	//RadioButton            RB_ConCuotas1
	window.document.forms[0].RBConCuotas1.disabled = false;
	window.document.forms[0].RBConCuotas2.disabled = false;
	if (ope_cuo=='S')
	   {window.document.forms[0].RBConCuotas1.checked = true}
	else
	   {window.document.forms[0].RBConCuotas2.checked = true}   
	
	window.document.forms[0].RB_OpePuntual1.disabled = false;
	window.document.forms[0].RB_OpePuntual2.disabled = false;
	if (ope_lnl=='S')
	   {window.document.forms[0].RB_OpePuntual1.checked = true}
	else   
	   {window.document.forms[0].RB_OpePuntual2.checked = true}

	window.document.forms[0].RB_Responsabilidad1.disabled = false;
	window.document.forms[0].RB_Responsabilidad2.disabled = false;
	if (ope_res_son==1)   
	   {window.document.forms[0].RB_Responsabilidad1.checked = true}
	else   
	   {window.document.forms[0].RB_Responsabilidad2.checked = true}

	window.document.forms[0].RB_ModoOpera1.disabled = false;
	window.document.forms[0].RB_ModoOpera2.disabled = false;
	if (ope_res_son=='S')      
	   {window.document.forms[0].RB_ModoOpera1.checked = true}
	else   
	   {window.document.forms[0].RB_ModoOpera2.checked = true}
*/
	
	return;
	}
}

function ValidacionesBusqueda()
{
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut.value == '')
    {alert('Ingrese un Rut a buscar'); return}
    
    if (window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig.value =='' )

       {alert("Debe Ingresar Digito Verificador");return}

    __doPostBack('ctl00$ContentPlaceHolder1$Btn_Buscar','');
    return;
     
      
    //return true;
    
}