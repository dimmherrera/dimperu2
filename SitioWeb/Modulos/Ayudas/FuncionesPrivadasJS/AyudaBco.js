// Archivo JScript

function AceptaBanco(grilla)
{
    //var rut = event.srcElement.parentElement.children(0).innerText; 
    //var rso = event.srcElement.parentElement.children(1).innerText; 
    
    var cod;
    var des;
    var pClass = 'clicktable';
    
    for(var i=0;i<grilla.rows.length;i++)
    {
	    if(grilla.rows(i).cells(1).className==pClass) 
	    {
		    cod = grilla.children(0).rows(i).cells(0).innerText;
		    des = grilla.children(0).rows(i).cells(1).innerText;
		    
		
	
	        window.dialogArguments.document.forms[0].Txt_Banco.value = cod;
	        window.dialogArguments.document.forms[0].Txt_BancoDes.value =des ;
	        window.close();
        	return;
	        
		}
    }
    
 }