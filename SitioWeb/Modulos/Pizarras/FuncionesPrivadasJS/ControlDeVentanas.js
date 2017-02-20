function SendToPdf(neg,Inf){
       abrir(neg,Inf)
       timer=setInterval(cerrar,7000) //8000=8sg
                      }

function cerrar(){
      miW.close()
                  }

 function abrir(neg,Inf){
 
 	//var neg = window.document.forms[0].ctl00_ContentPlaceHolder1_HF_Nro_Neg.value;
 
     miW=window.open('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx?Nro=' + neg,'Negociacion','width=100,height=100,left=600,top=400,alwaysraised=yes,scrollbars=no,menubar=no,scrolbar=0,toolbar=0,status=no');
 
       //miW=window.open('OpenPDF.aspx?Nro=' + neg + '&Inf=' + Inf,'MR','width=100,height=100,left=600,top=400,alwaysraised=yes,scrollbars=no,menubar=no,scrolbar=0,toolbar=0,status=no');
 
       abierto=true
           if (abierto){
               abierto=false
               timer=setInterval(cerrar,5000) //7000=7sg
                       }
                  }
                  
     
