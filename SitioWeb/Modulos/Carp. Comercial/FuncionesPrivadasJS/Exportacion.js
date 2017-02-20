function SendToPdf(url,neg,Inf){
       abrir(url, neg, Inf);
       //timer = setInterval(cerrar, 10000);
                      }

function cerrar() {
    miW.close()
                  }

function abrir(url, neg, Inf) {

    miW = window.open(url + '?Nro=' + neg + '&Inf=' + Inf);
    
//    abierto = true
//    if (abierto) {
//        abierto = false
//       
//        timer = setInterval(cerrar, 10000) //7000=7sg
//    }
 }
                  
     
