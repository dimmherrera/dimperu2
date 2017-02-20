
function DetalleCliente(pTabla, pClass,jClass,sClass)
{
	if(event.srcElement.parentElement.tagName=='TR')

	{
		if(event.srcElement.parentElement.cells(0).className==pClass)

		{
			
			for(var i=0;i<event.srcElement.parentElement.cells.length;i++)
			event.srcElement.parentElement.cells(i).className=sClass;
			return;

		}
		
	
	NormalClass(pTabla,pClass,jClass);
	J_RolClass_Tabla(pTabla, pClass);
	
	var id = IdGrilla(ctl00_ContentPlaceHolder1_GV_Clientes, 'clicktable', 0);
	
	window.document.forms[0].ctl00_ContentPlaceHolder1_Posicion.value = id;
	//document.getElementById('ctl00_ContentPlaceHolder1_IB_Detalle').disabled = 'false';

	return;
	
	}
}

function NuevoCliente()
{

	try 
	{

	//LIMPIAMOS TODOS LOS CAMPOS PARA INGRESO DE UN NUEVO CLIENTE
	window.document.forms[0].sw.value = "INSERT";
	//DATOS GENERALES
	window.document.forms[0].Txt_Rut.className = 'clstxt';
	window.document.forms[0].Txt_Dig.className = 'clstxt';
	
	window.document.forms[0].Txt_Rut.readOnly = false;
	window.document.forms[0].Txt_Dig.readOnly = false;
	
	window.document.forms[0].Txt_Rut.value = "";
	window.document.forms[0].Txt_Dig.value = "";
	window.document.forms[0].DP_TipoCliente.value = 0;
	window.document.forms[0].Txt_Bas_Num.value = "";
	//NATURAL  
	window.document.forms[0].Txt_Nom_Bre.value = "";
	window.document.forms[0].Txt_Ape_Pat.value = "";
	window.document.forms[0].Txt_Ape_Mat.value = "";
	window.document.forms[0].Dp_Sexo.value = 0;
	window.document.forms[0].Txt_Fec_Nac.value = "";
	//JURIDICO
	window.document.forms[0].Txt_Raz_Soc.value = "";
	window.document.forms[0].Txt_Fec_Con.value = "";
	//ANTECEDENTES GENERALES
	window.document.forms[0].Txt_Dom_Par.value = "";
	window.document.forms[0].DP_Sucursal.value = 0;
	window.document.forms[0].DP_Ciudad.value = 0;
	window.document.forms[0].Txt_Mai.value = "";
	window.document.forms[0].Txt_Cod_Pos.value = "";
	window.document.forms[0].DP_Segmento.value = 0;	
	window.document.forms[0].DP_Giro.value = 0;
	window.document.forms[0].DP_Comuna.value = 0;
	window.document.forms[0].DP_ActEco.value = 0;
	//ANTECEDENTES CON FACTORING
	window.document.forms[0].DP_Ejecutivo.value = 0;
	window.document.forms[0].DP_ModoOpe.value = 0;
	window.document.forms[0].DP_EstCartera.value = 0;
	window.document.forms[0].DP_TipoInf.value = 0;
	window.document.forms[0].Txt_Bie_Ser.value = "";
	window.document.forms[0].DP_Estado.value = 0;
	window.document.forms[0].DP_CateRiesgo.value = 0;
	window.document.forms[0].DP_FormaEnvio.value = 0;
	window.document.forms[0].Txt_Tas_Mor.value = "";
	window.document.forms[0].CB_Sinacofi.checked = false;
	window.document.forms[0].CB_CobranzaAnt.checked = false;
	
	window.document.forms[0].Txt_Fec_Cre.value = DvFecha();
	window.document.forms[0].Txt_Fec_Ope.value = "";
	
	//ANTECEDENTES CON BANCO
	//window.document.forms[0].Txt_Suc_Bco.value = "";
	window.document.forms[0].DP_Suc_Bco.value = 0;
	window.document.forms[0].Txt_Eje_Bco.value = "";
	window.document.forms[0].Txt_Anx_Bco.value = "";
	window.document.forms[0].DP_BancaCli.value = 0;
	
	document.getElementById('Txt_Rut').focus();
	
	__doPostBack('NuevoCli','');	
	
	
	} 
	catch(e){alert(e.message);}

	finally {}
}