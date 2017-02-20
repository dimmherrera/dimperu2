// Archivo JScript

function AceptaCliente(grilla)
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
		    rut = grilla.children(0).rows(i).cells(0).innerText;
		    rso = grilla.children(0).rows(i).cells(1).innerText;

		    var r;
		    r = rut.replace('.', '');
		    r = r.replace('.', '');
		    r = r.substring(0, r.search('-'));
		    //window.dialogArguments.document.forms[0]
		    window.document.forms[0].Txt_Rut.value = FormatMil(r);
	        window.document.forms[0].Txt_Dig.value = dv(r);
	        window.document.forms[0].Txt_Rzo.value = rso;
	        //__doPostBack('ctl00_ContentPlaceHolder1_WC_AyudaCliente1_IB_Buscar', '');
	        return;
	        
	        //window.close();
        	//return false;
	        
		}
    }
    
    alert('Seleccione un Cliente');
	//return false;

}



