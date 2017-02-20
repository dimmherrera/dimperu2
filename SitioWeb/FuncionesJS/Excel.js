// Archivo JScript
        	function abrirEx()
			{	
				/*
				if (GrillaPagoCuotas.rows.length - 2 <= 0)
				{
					alert('Deben haber elementos a exportar');
					return;
				}*/
				miW=window.open('Excel.aspx', 'PopUp','width=1,height=1,left=600,top=350,alwaysraised=yes,scrollbars=no,menubar=no,scrolbar=0,toolbar=0,status:off');
				var abierto=true;
				
				if (abierto)
				{
					abierto=false;
					timer=setInterval(cerraExp,7000); //7000=7sg
				}
			}
			
			function cerraExp()
			{
				miW.close();
			}

