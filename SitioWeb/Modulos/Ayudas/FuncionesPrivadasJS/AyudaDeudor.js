// Archivo JScript
function AceptaDeudorCartola(rut, rso) {
    
    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_Cliente_Txt_Rut_Deu.value = r;
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_Cliente_Txt_Dig_Deu.value = dv(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_Cliente_Txt_Rso_Deu.value = rso;
    
    window.close();

    return;

}

function AceptaDeudorPago(rut, rso) {

    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Rut_Deu.value = r;
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Dig_Deu.value = dv(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Rso_Deu.value = rso;

    window.close();

    return;

}


function AceptaDeudor(rut, rso) {

    var r = rut.substring(0, rut.search('-'));

    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    //window.opener.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.value = r;
    //window.opener.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.value = dv(r);
    //window.opener.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rso_Deu.value = rso;

    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.value = r;
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.value = dv(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rso_Deu.value = rso;

    window.close();

    return;

}

function AceptaDeudorWebControl(rut, rso) {

    var r = rut.substring(0, rut.search('-'));


    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    
    window.dialogArguments.document.forms[0].WC_QuePaga1_Txt_Rut_Deu.value = r;
    window.dialogArguments.document.forms[0].WC_QuePaga1_Txt_Dig_Deu.value = dv(r);
    window.dialogArguments.document.forms[0].WC_QuePaga1_Txt_Rso_Deu.value = rso;

    window.close();

    return;


}

function AceptaDeudorNormal(rut, rso) {

    var r = rut.substring(0, rut.search('-'));

    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    
    window.dialogArguments.document.forms[0].Txt_Rut_Deu.value = r;
    window.dialogArguments.document.forms[0].Txt_Dig_Deu.value = dv(r);
    window.dialogArguments.document.forms[0].Txt_Rso_Deu.value = rso;

    window.close();

    return;

}

function AceptaDeudorEsp(rut, rso) {

    var r = rut.substring(0, rut.search('-'));

    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    
    WC_QuePaga1_Txt_Rut_Deu
    
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Rut_Deu.value = r;
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Dig_Deu.value = dv(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Rso_Deu.value = rso;

    window.close();

    return;

}
