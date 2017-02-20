// Archivo JScript
function FiltroNumeros(NombreControl) 
{    
    var key = window.event.keyCode;    
    var valor = document.all[NombreControl].value;   
    var coma;   
    
    if(valor.length == 1 && valor.slice(0)=='.')   
    {
        document.all[NombreControl].value = '';   
    }   
    
    //valor = new String(valor);
    //coma = valor.find('.');   
    
     for(i=0;i<valor.length;i++)
    {
        if(valor.charAt(i)=='.') 
        {
            coma=1;
            break;
        }
        else
        {
            coma=-1;
        }
    }
    
    if(valor.length == 0 || coma > -1)   
    {       
        if ((key > 47 && key < 58))            
            return;        
        else 
        {            
            window.event.returnValue = null;         
        }    
        
    }   
    else if(valor.length == 9 && coma <= -1)   
        {       
            if (key == 44 || key == 46)        
            {          
                document.all[NombreControl].value = valor + '.';          
                window.event.returnValue = null;       
            }       
            else 
            {            
                window.event.returnValue = null;         
            }    
        }   
        else   
        {       
            if ((key > 47 && key < 58) || (key == 44) || (key == 46))        
            {           
                if(key == 46 || (key == 44))           
                {               
                    document.all[NombreControl].value = valor + '.';               
                    window.event.returnValue = null;           
                 }           
                 return;        
             }       
             else 
             {            
                window.event.returnValue = null;         
             }    
    }
 }    
 
  function FormatoMiles(NombreControl)
        { 
           var monto = document.all[NombreControl].value; 
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
           
           for(i=0;i<monto.length;i++)
            {
                lugarIII=i+1;
            
                if(monto.charAt(i)=='.') 
                {
                    lugarIII=lugarIII-1;
                    break;
                }
//                else
//                {
//                    lugarIII=i;
//                }
            }
           //lugarIII = lugarIII + 1;
           //lugarIII = paso.search('.');
           if(lugarIII > 0)
           {
               miles = paso.slice(0,lugarIII);
               decimales = paso.slice(lugarIII);
               //decimales = '.' + paso.slice(lugarIII-1);
           }
           else
           {
               miles = paso.replace('.','');
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
                   aux = ',' + aux;
              }
             aux = miles.substr(i,1) + aux;
             cont++;
           }
           if (decimales.length > 1)
           {formato = negativo.concat(aux,decimales);}
           else
           {formato = aux;}
           
           document.all[NombreControl].value = formato;
        } 


 function delMiles(NombreControl)
        {  
           var valor = document.all[NombreControl].value;
           var valor_aux;
           valor_aux = valor;
           valor_aux = valor_aux.replace(',','');
           valor_aux = valor_aux.replace(',','');
           valor_aux = valor_aux.replace(',','');
           valor_aux = valor_aux.replace(',','');
           document.all[NombreControl].value = valor_aux;
        }  

 function FiltroSoloNum() 
 { 
   var key = window.event.keyCode; 
   if ((key > 47 && key < 58)) 
        return;
   else 
   {window.event.returnValue = null;} 
 }
 
 //FUNCIONES PARA FECHAS
//function FiltroFecha() 
//{ 
//           var key = window.event.keyCode; 
//           if (key > 44 && key < 58) 
//              return; 
//           else { 
//              window.event.returnValue = null;  
//           } 
//} 

//function FormatoFecha(NombreControl) 
//{
//           var valor = document.all[NombreControl].value; 
//           valor = valor.replace('.', '/'); 
//           valor = valor.replace('.', '/'); 
//           valor = valor.replace('.', '/'); 
//           valor = valor.replace('-', '/'); 
//           valor = valor.replace('-', '/'); 
//           valor = valor.replace('-', '/'); 
//           
//           var dia, mes, anio, posi; 
//           
//           if (valor == '') return; 
//           
//           posi = valor.search('/'); 
//           
//           if(posi>=0) 
//           {
//            dia = valor.slice(0,2); 
//            mes = valor.slice(3,5); 
//            anio = valor.slice(6,10);
//           } 
//           else 
//           {
//           dia = valor.slice(0,2); 
//           mes = valor.slice(2,4); 
//           anio = valor.slice(4,8);
//           } 
//           document.all[NombreControl].value = dia + '/' + mes + '/' + anio ; 
//}
//           
//           
//           
////FUNCIONES DE VALIDACION DE FECHAS
//function ValidarFecha(val) 
//{ 
//        var valor = ValidatorTrim(ValidatorGetValue(val.controltovalidate)); 
//        var pernul = document.all[val.controltovalidate].AllowNull; 
//        
//        if (valor.length == 0)return true;
//        if (valor.length != 10)return false;

//        var sep1 = valor.substr(2,1);
//        var sep2 = valor.substr(5,1);

//        if (sep1!='/' || sep2!='/')return false;
//        if (valor == '') {if (pernul == 'true') {return true;} else {return false;}};

//        var mmm = valor.slice(3,5); 
//        var ddd = valor.slice(0,2); 

//        if (ddd.slice(0,1) == '0') { ddd = ddd.slice(1); } 
//        if (mmm.slice(0,1) == '0') { mmm = mmm.slice(1); } 

//        var AAAA = parseInt(valor.slice(-4)); 
//        var MM = parseInt(mmm); 
//        var DD = parseInt(ddd); 

//        if (isNaN(AAAA) || isNaN(MM) || isNaN(DD)) 
//            {
//                if (valor.length == 10) 
//                {
//                    return false;
//                } 
//                else 
//                {
//                return true;
//                } 
//            } 

//        var selectedDate = new Date(AAAA, MM-1, DD); 
//        if (selectedDate.getMonth() != MM -1 || selectedDate.getDate() != DD) 
//        { 
//           return false;
//            
//        } 
//        else 
//        { 
//           return true; 
//        } 
//} 