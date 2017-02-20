function ClickNegociacionl(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value = '';
            return;

        }

        var nro = event.srcElement.parentElement.cells(0).innerText;
     

        window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value = nro;

        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);

        return;
    }
}

function Negociación(url, pWidth, pHeight, pLeft, pTop) {

    var nro = window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value;
    var rut = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Cli.value;

    if (rut == '') 
    {
        //alert('Debe Ingresar un Cliente');
        return;
    }
    else {

        if (nro == '') {
            
        }
        else {
            //var x = window.showModalDialog(url + '?nro=' + nro, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
        }
        
        var x = window.showModalDialog(url, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
        
        return;
    }
}

function DetalleNegociacion(url, pWidth, pHeight, pLeft, pTop) {

    var nro = window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value;
    var rut = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Cli.value;

    if (rut == '') {
        //alert('Debe Ingresar un Cliente');
        return;
    }
    else {

        if (nro == '') {
            //var x = window.showModalDialog(url, window, 'scroll:no;status:off;');
            //var x = window.showModalDialog(url, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
            //alert('Debe Seleccionar una Negociación');
        }
        else {
            var x = window.showModalDialog(url + '?nro=' + nro, window, 'scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px');
        }

        return;
    }
}

function MontoEvaluado() {

    var mto = window.document.forms[0].ctl00_ContentPlaceHolder1_Txt_MtoEva.value;



}


function CheckNotificacion(Tipo) {

    if (Tipo == 1) {
        window.document.forms[0].CB_NotSiPost.checked = true;
        window.document.forms[0].CB_NotPers.checked = false;
    }

    if (Tipo == 2) {
        window.document.forms[0].CB_NotSiPost.checked = false;
        window.document.forms[0].CB_NotPers.checked = true;
    }

    return;

}

function ClickEvaluacion(pTabla, pClass, jClass, sClass) {
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;

            window.document.forms[0].ctl09_HF_NroEva.value = '';
            return;

        }

        var pos = event.srcElement.parentElement.rowIndex;
        var nro = event.srcElement.parentElement.cells(0).innerText;
        var por = event.srcElement.parentElement.cells(1).innerText;
        var mon = event.srcElement.parentElement.cells(2).innerText;
        var mto = event.srcElement.parentElement.cells(3).innerText;
        var deu = event.srcElement.parentElement.cells(4).innerText;
        

        window.document.forms[0].ctl09_HF_PosEva.value = pos;
        window.document.forms[0].ctl09_HF_NroEva.value = nro;
        window.document.forms[0].Txt_PorAnt.value = por;
        window.document.forms[0].Txt_MtoEva.value = mto;
        window.document.forms[0].Txt_CantDeu.value = deu;
        
        switch (mon)
       {
        case 'PESO': { window.document.forms[0].DP_Moneda.value = 1; break }
        case 'UF - UF': { window.document.forms[0].DP_Moneda.value = 2; break }
        case 'US$ - DOLAR': { window.document.forms[0].DP_Moneda.value = 3; break }
        case 'EURO': { window.document.forms[0].DP_Moneda.value = 4; break }
       }

        window.document.forms[0].Txt_PorAnt.className = 'clsDisabled';
        window.document.forms[0].Txt_MtoEva.className = 'clsDisabled';
        window.document.forms[0].Txt_CantDeu.className = 'clsDisabled';
        window.document.forms[0].DP_Moneda.className = 'clsDisabled';
        
        window.document.forms[0].Txt_PorAnt.readOnly= true;
        window.document.forms[0].Txt_MtoEva.readOnly= true;
        window.document.forms[0].Txt_CantDeu.readOnly= true;
        window.document.forms[0].DP_Moneda.disabled = true;
        
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        
        __doPostBack('LB_CargaEvaluacion', '');

        return;
    }
}
