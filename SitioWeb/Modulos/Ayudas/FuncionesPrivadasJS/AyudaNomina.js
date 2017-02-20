function AceptaNomina(grilla)
{

    var Num;
    var Mon;
    var pClass = 'clicktable';
    
    for(var i=0;i<grilla.rows.length;i++)
    {
	    if(grilla.rows(i).cells(1).className==pClass) 
	    {
		    Num = grilla.children(0).rows(i).cells(0).innerText;
		    Mon = grilla.children(0).rows(i).cells(1).innerText;
		
	        window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Nro_Nomina.value = Num;

	        window.close();
        	return;
	        
		}
    }

}

function Nomina(Num) {
   window.dialogArguments.document.forms[0].ctl00_ContentPlaceHolder1_Txt_Nro_Nomina.value = Num
   window.close();
   return;
}
