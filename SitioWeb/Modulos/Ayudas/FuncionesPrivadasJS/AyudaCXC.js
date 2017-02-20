function AceptaCXC(nro, doc) {

    var n;
    var d;

    c = nro.replace('.', '');
    c = c.replace(',', '');
    d = doc.replace('.', '');
    d = d.replace(',', '');
   
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_txt_Contrato.value = c;
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_txt_nro_doc.value = d;
    
    window.close();

    return;

}

function AceptaDNC(nro, doc){
    
    var n;
    var d;

    c = nro.replace('.', '');
    c = c.replace(',', '');
    d = doc.replace('.', '');
    d = d.replace(',', '');
    
    window.dialogArguments.document.forms[0].txt_Contrato.value = c;
    window.dialogArguments.document.forms[0].txt_nro_doc.value = d;
    window.close();

    return;
}

