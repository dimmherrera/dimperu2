function round(number,X) {
X = (!X ? 2 : X);
return Math.round(number*Math.pow(10,X))/Math.pow(10,X);
}

function fnTrapKD(btn)
{
				
				if (document.all)
				{
					if(event.keyCode==13)
					{
						event.returnValue=false;
						event.cancel=true;
						btn.click();
					}
				}
				else if(document.getElementById)
				{
					if(event.which==13)
					{
						event.returnValue=false;
						event.cancel=true;
						btn.click();
					}
				}
				else if(document.layers)
				{
					if(event.which==13)
					{
						event.returnValue=false;
						event.cancel=true;
						btn.click();
					}
				}
}



    function Funcion(event)

{
    var obj = event.srcElement.alt
    var seltreeNode = obj;
    window.document.forms[0].ctl00$Menu_Left1$hf_nodo.value = event.srcElement.alt;
   // alert(event.srcElement.alt);
    __doPostBack('ctl00$Menu_Left1$LinkButton1', '');
    return false;
}

function fnBtnClickOnEnter(mto) 
{

    if (document.all) 
    {
      
           
        if (event.keyCode >= 13)
         {
             //__doPostBack('ctl00_ContentPlaceHolder1_lb_calcular', '');
             //window.__doPostBack('ctl00_ContentPlaceHolder1_lb_calcular', '');
             
             //var mto = window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txt_mto_rec.value;
            
             mto =  mto.replace('.', '');
             mto = mto.replace('.', '');
             mto = mto.replace('.', '');
             mto = mto.replace('.', '');
             mto = mto.replace(',', '.');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');
             mto = mto.replace('_', '');

//             if (mto == "") 
//            {

//                return;

//            } 
//             
             var fac = window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txt_tip_cam.value;
             var tot=truncaNUM(mto) * truncaNUM(fac) ;
             window.document.forms[0].ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txt_mto.value = tot;
             __doPostBack('ctl00_ContentPlaceHolder1_lb_calcular', '');
             return;

         }
        
    }
    else if (document.getElementById) 
    {
        if (event.which == 13) 
        {
            event.returnValue = false;
            event.cancel = true;
            btn.click();
        }
    }
    else if (document.layers) 
    {
        if (event.which == 13) 
        {
            event.returnValue = false;
            event.cancel = true;
            btn.click();
        }
    }
}

function RollOver(control, pClass)
{
	 control.className=pClass;	
}

function BtnEnable(pBtn,est)
{
 pBtn.disabled = est;
 return;
}

function truncaNUM(pnum)
{
	pnum = String(pnum);
	pnum = pnum.replace('.','');
	pnum = pnum.replace('.','');
	pnum = pnum.replace('.','');
	pnum = pnum.replace('.','');
	pnum = pnum.replace(',','.');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = pnum.replace('_','');
	pnum = parseFloat(pnum);
	return pnum;
}

function FormatMil(monto)
{
  var lugar,paso,negativo,lugarIII,miles,aux,cont,largo,formato;
  lugar = monto.search('-');
  
    if(lugar == 0)
    {
        paso = monto.slice(lugar+1);
        negativo = '-';
    }
    else
    {
        paso = monto.replace('-','');
        negativo = '';
    }
    
    lugarIII = paso.search(',');
    if(lugarIII > 0)
    {
        miles = paso.slice(0,lugarIII);
        decimales = ',' + paso.slice(lugarIII+1);
    }
    else
    {
        miles = paso.replace(',','');
        decimales = '';
    }
        
    aux = '';
    largo = miles.length;
    cont = 0;
    for(var i=largo-1;i>=0;i--)
    {
        if(cont==3)
        {
			cont = 0;
			aux = '.' + aux;
        }
        aux = miles.substr(i,1) + aux;
        cont++;
     }
        formato = negativo.concat(aux,decimales);
 return formato;
}


function Decimal(Cadena)
{
   var auxDeci,c,desDeci;
   
   Cadena = new String(Cadena);
   c = Cadena.search(',');

   if(c > 0)
   {
		desDeci = Cadena.slice(c+1);
		switch(desDeci.length)
		{       
	         case 1:
			    auxDeci = Cadena.slice(0,c+1).concat(Cadena.slice(c+1),'0');
				break
			
			 case 2:	
				auxDeci	= Cadena;
                break
             case 4:
                auxDeci = Cadena.slice(0,c+1).concat(Cadena.slice(c+1),'0');
				break
        }
            
   }     
   else
        auxDeci = Cadena.concat(',00');

  return auxDeci;

}

function MsjConfirm(Strs,Key){
if(confirm(Strs)) 
	{		
		__doPostBack(Key,'');
		
	}
	else
	{
		__doPostBack('Cancel','');
		
	}
	
}


function MsjConfirm(Strs,Key){
if(confirm(Strs)) 
	{		
		__doPostBack(Key,'');
		
	}
	else
	{
		__doPostBack('Cancel','');
		
	}
	
}


function ArgumentsDoPostBack(key)
{
__doPostBack(key,'');
closeIt();
}


function LenRut(prut)
{
if (prut.length <= 15)
{
alert('Ingrese un NIT valido');
return false;
}
else
{
return true;
}
}



function dv(txtN) {

if (isNaN(txtN)) return "";
txtN = txtN.toUpperCase();
if (txtN == "CF" || txtN == "C/F" || txtN == "") return "";
var nit = txtN;

var pos = nit.length;


var Correlativo = pos; //nit.substr(0, pos);
var Factor = pos;  //Correlativo.length + 1;
var Suma = 0;
var Valor = 0;

for (x = 0; x <= (pos - 1); x++) {
    
    Valor = eval(nit.substr((nit.length - 1) - x, 1));
    
    if (x == 0) Factor = 3;
    if (x == 1) Factor = 7;
    if (x == 2) Factor = 13;
    if (x == 3) Factor = 17;
    if (x == 4) Factor = 19;
    if (x == 5) Factor = 23;
    if (x == 6) Factor = 29;
    if (x == 7) Factor = 37;
    if (x == 8) Factor = 41;
    if (x == 9) Factor = 43;
    if (x == 10) Factor = 47;
    if (x == 11) Factor = 53;
    if (x == 12) Factor = 59;
    if (x == 13) Factor = 67;
    if (x == 14) Factor = 71;
    
    var Multiplicacion = eval(Valor * Factor);
    Suma = eval(Suma + Multiplicacion);
    
}

var xMOd11 = 0;
xMOd11 = (Suma % 11);
var s = xMOd11;

if (s <= 1)
    return s;
else
    return (11 - s);
       
}


function solonumeros(e) {
tecla=(document.all) ? e.keyCode : e.which;
if(tecla<48 || tecla>57)
return false;
}

function Caracter(valor)
{
var lst = window.document.Formulario1.Busq.value;
if (lst == "1")
{
return solonumeros(valor);
}
else
{
return sololetras(valor);
}

}

      
function sololetras(e) {
tecla=(document.all) ? e.keyCode : e.which;
var opcion
if(tecla<65 || tecla>122){
opcion = 1;
}
if(tecla==209 || tecla==241){
opcion = 2;
}

if(tecla>91 && tecla<96){
opcion = 3;
}
if(tecla == 32)
{
opcion = 4;
}

switch(opcion){ 

case 1: 

return false;
break;

case 2:

return true;
break;


case 3:

return false;
break;

case 4:

return true;
break;

default: return true;

}

}

/*
function SetFocus(ctrl)
{
	document.getElementById(' + ctrl.ID + ').focus();
	if ( document.getElementById(' + ctrl.ID + ').select != null ){document.getElementById(' + ctrl.ID + ').select();
}
*/