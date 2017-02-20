// Archivo JScript
function Criterio()
    
{
       
    if (window.document.forms[0].TabContainer1_Cartera_WebUserControl1_CB_Deudor.checked  == true)
    {
        window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_rut.readOnly = false;
        window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_rut.className = "clsMandatorio";
        document.getElementById('TabContainer1_Cartera_WebUserControl1_Txt_rut').focus();
                window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_Dig.readOnly = false;
        window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_Dig.className = "clsMandatorio";
    }
    else
    {
         window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_rut.readOnly = true;
        window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_rut.className = "clsDisabled";
            window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_Dig.readOnly = false;
        window.document.forms[0].TabContainer1_Cartera_WebUserControl1_Txt_Dig.className = "clsDisabled";
        
    
    }
    return;
}

