// Archivo JScript

function AceptaCliente(rut, digito) {

    var r;
    
    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Cli.value = FormatMil(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Cli.value = digito;

    //ctl00_ContentPlaceHolder1_ctl02_Txt_Rut_Cli
    
    window.close();

	return;

}

function AceptaClienteHoja(rut) {

    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Rut_Deu.value = FormatMil(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Dig_Deu.value = dv(r);

    //ctl00_ContentPlaceHolder1_ctl02_Txt_Rut_Cli

    window.close();

    return;

}

function AceptaClientePago(rut) {

    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));
                                              
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Rut_Cli.value = FormatMil(r);
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_ctl02_Txt_Dig_Cli.value = dv(r);

    //ctl00_ContentPlaceHolder1_ctl02_Txt_Rut_Cli

    window.close();

    return;

}
function AceptaClienteAnulacionPago(rut, tipo) {

    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    if (tipo == 1) {
        window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_Txt_Rut_Cli.value = FormatMil(r);
        window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_Txt_Dig_Cli.value = dv(r);
    }
    else {
        window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_Txt_Rut_Cli_Apl.value = FormatMil(r);
        window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_Txt_Dig_Cli_Apl.value = dv(r);
    }
    
    window.close();

    return;

}



function AceptaClienteCartola(rut) {

    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_Cliente_Txt_Rut_Cli.value = r;
    window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_Cliente_Txt_Dig_Cli.value = dv(r);
    
    window.close();

    return;

}


function AceptaCli(rut) {

    var r;

    r = rut.replace('.', '');
    r = r.replace('.', '');
    r = r.replace('.', '');
    r = r.substring(0, r.search('-'));

    window.dialogArguments.document.forms[0].Txt_Rut_Cli.value = FormatMil(r);
    window.dialogArguments.document.forms[0].Txt_Dig_Cli.value = dv(r);


    window.close();

    return;

}

