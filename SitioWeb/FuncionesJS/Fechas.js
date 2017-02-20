 function cerosIzq(sVal, nPos){
    var sRes = sVal;
    for (var i = sVal.length; i < nPos; i++)
     sRes = "0" + sRes;
    return sRes;
   }

   function armaFecha(nDia, nMes, nAno){
    var sRes = cerosIzq(String(nDia), 2);
    sRes = sRes + "/" + cerosIzq(String(nMes), 2);
    sRes = sRes + "/" + cerosIzq(String(nAno), 4);
    return sRes;
   }

   function sumaMes(nDia, nMes, nAno, nSum){
    if (nSum >= 0){
     for (var i = 0; i < Math.abs(nSum); i++){
      if (nMes == 12){
       nMes = 1;
       nAno += 1;
      } else nMes += 1;
     }
    } else {
     for (var i = 0; i < Math.abs(nSum); i++){
      if (nMes == 1){
       nMes = 12;
       nAno -= 1;
      } else nMes -= 1;
     }
    }
    return armaFecha(nDia, nMes, nAno);
   }

   function esBisiesto(nAno){
    var bRes = true;
    res = bRes && (nAno % 4 == 0);
    res = bRes && (nAno % 100 != 0);
    res = bRes || (nAno % 400 == 0);
    return bRes;
   }

   function finMes(nMes, nAno){
    var nRes = 0;
    switch (nMes){
     case 1: nRes = 31; break;
     case 2: nRes = 28; break;
     case 3: nRes = 31; break;
     case 4: nRes = 30; break;
     case 5: nRes = 31; break;
     case 6: nRes = 30; break;
     case 7: nRes = 31; break;
     case 8: nRes = 31; break;
     case 9: nRes = 30; break;
     case 10: nRes = 31; break;
     case 11: nRes = 30; break;
     case 12: nRes = 31; break;
    }
    return nRes + (((nMes == 2) && esBisiesto(nAno))? 1: 0);
   }

   function diasDelAno(nAno){
    var nRes = 365;
    if (esBisiesto(nAno)) nRes++;
    return nRes;
   }

   function anosEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1){
    var nRes = Math.max(0, nAn1 - nAn0 - 1);
    if (nAn1 != nAn0)
     if ((nMe1 > nMe0) || ((nMe1 == nMe0) && (nDi1 >= nDi0)))
      nRes++;
    return nRes;
   }

   function mesesEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1){
    var nRes;
    if ((nMe1 < nMe0) || ((nMe1 == nMe0) && (nDi1 < nDi0))) nMe1 += 12;
    nRes = Math.max(0, nMe1 - nMe0 - 1);
    if ((nDi1 > nDi0) && (nMe1 != nMe0)) nRes++;
    return nRes;
   }

   function diasEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1){
    var nRes;
    if (nDi1 < nDi0) nDi1 += finMes(nMe0, nAn0);
    nRes = Math.max(0, nDi1 - nDi0);
    return nRes;
   }

   function mayorOIgual(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1){
    var bRes = false;
    bRes = bRes || (nAn1 > nAn0);
    bRes = bRes || ((nAn1 == nAn0) && (nMe1 > nMe0));
    bRes = bRes || ((nAn1 == nAn0) && (nMe1 == nMe0) && (nDi1 >= nDi0));
    return bRes;
   }

   function calcula(sFc0,sFc1)
   {
    //var sFc0 = document.frm.fecha0.value; // Se asume válida
    //var sFc1 = document.frm.fecha1.value; // Se asume válida
    
    var nDi0 = parseInt(sFc0.substr(0, 2), 10);
    var nMe0 = parseInt(sFc0.substr(3, 2), 10);
    var nAn0 = parseInt(sFc0.substr(6, 4), 10);
    var nDi1 = parseInt(sFc1.substr(0, 2), 10);
    var nMe1 = parseInt(sFc1.substr(3, 2), 10);
    var nAn1 = parseInt(sFc1.substr(6, 4), 10);
    
     //var desface = diasEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1);
     //window.document.forms[0].TxtDesface.value = desface;
     
     if (mayorOIgual(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1)){
     var nAno = anosEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1);
     var nMes = mesesEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1);
     var nDia = diasEntre(nDi0, nMe0, nAn0, nDi1, nMe1, nAn1);
     var nTtM = nAno * 12 + nMes;
     var nTtD = nDia;
     for (var i = nAn0; i < nAn0 + nAno; i++) nTtD += diasDelAno(nAno);
     for (var j = nMe0; j < nMe0 + nMes; j++) nTtD += finMes(j, nAn1);
     var nTSS = Math.floor(nTtD / 7);
     var nTSD = nTtD % 7;
     
    //alert(String(nDia) + " dias, " + String(nTtD) + " días");
    
    //window.document.forms[0].TxtDesface.value = String(nTtD);
     
     /* 
     document.frm.difDMA.value = String(nAno) + " años, " + String(nMes) + " meses, " + String(nDia) + " días";
     document.frm.difDM.value = String(nTtM) + " meses, " + String(nDia) + " días";
     document.frm.difD.value = String(nTtD) + " días";
     document.frm.difSD.value = String(nTSS) + " semanas, " + String(nTSD) + " días";
     */
     
    } else alert("Error en rango");
    
   } 
   
function DvFecha()
{
    var mydate=new Date();
	var year=mydate.getYear();
	
	if (year < 1000)
		year+=1900;
		
	var day=mydate.getDay();
	var month=mydate.getMonth()+1;
	
	if (month<10)
		month="0"+month;
		
	var daym=mydate.getDate();
	
	if (daym<10)
		daym="0"+daym;
		
	return "+daym+"/"+month+"/"+year+"	

	//document.write("<small><font color='000000' face='Arial'><b>"+daym+"/"+month+"/"+year+"
}