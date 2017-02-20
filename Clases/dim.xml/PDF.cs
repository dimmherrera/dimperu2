using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Collections;
using System.Text;
using System.Xml;

namespace dim.xml
{
    #region OBJETOS

    public class Documento
    {
        public string nrofactura { get; set; }
        public string Moneda { get; set; }
        public string monto { get; set; }
        public string FechaVcto { get; set; }

    }

    public class Notaria
    {
        public string nombrenotaria { get; set; }
        public string nombrenotario { get; set; }
        public string nronotaria { get; set; }
        public string direccionnotaria { get; set; }
        public string comunanotaria { get; set; }
        public string ciudadnotaria { get; set; }
    }

    public class Deudor
    {
        public string replegal { get; set; }
        public string cargocontacto { get; set; }
        public string nombrecontacto { get; set; }
        public string nombredeudor { get; set; }
        public string direcciondeudor { get; set; }
        public string comunadeudor { get; set; }
    }

    public class Factoring
    {
        public string nombrecliente { get; set; }
        public string factoring { get; set; }
        public string rutfactoring { get; set; }
        public string direccionfactoring { get; set; }
        public string comunafactoring { get; set; }
        public string ciudadfactoring { get; set; }
        public string fonofactoring { get; set; }
        public string pagoenfactoring { get; set; }
        public string bancopagoenfactoring { get; set; }
        public string emailfactoring { get; set; }
    }

    public class PagareBlanco
    {
        public string rut_deudor { get; set; }
        public string rsoc_deudor { get; set; }
        public string tip_ident { get; set; }
    }

    public class Reglamento
    {
        public string rut_cliente { get; set; }
        public string razon_cliente { get; set; }
        public string representante { get; set; }
    }

    #endregion

    public class clsPDF
    {       
        public void GeneraCartaPDF(Notaria notaria, Deudor deudor, Factoring factoring, 
                                   String fechaoperacion, Documento[] facturas, String ruta)
         {


             String outputPath = ruta;

             
             //XmlDocument xdoc = new XmlDocument();

             //creamos el documento
             //...ahora configuramos para que el tamaño de hoja sea LETTER (tamaño carta)
             Document document = new Document(iTextSharp.text.PageSize.LETTER);

             try
             {

                 //document.PageSize.BackgroundColor = new iTextSharp.text.color(255, 255, 255);
                 document.PageSize.Rotate();

                 //...definimos el autor del documento.
                 document.AddAuthor("Dimension S.A.");

                 //...el creador, que será el mismo eh!
                 document.AddCreator("Dimension S.A.");

                 //hacemos que se inserte la fecha de creación para el documento
                 document.AddCreationDate();

                 //...título
                 document.AddTitle("CARTA NOTARIAL");

                 //creamos un instancia del objeto escritor de documento
                 PdfWriter writer = PdfWriter.GetInstance(document, 
                                                          new System.IO.FileStream(outputPath, 
                                                                                   System.IO.FileMode.Create));

                 //definimos la manera de inicialización de abierto del documento.
                 //esto, hará que veamos al inicio, todas la páginas del documento
                 //en la parte izquierda
                 //writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;

                 //con esto conseguiremos que el documento sea presentada de dos en dos 
                 //writer.ViewerPreferences = PdfWriter.PageLayoutTwoColumnLeft;

                 //abrimos el documento para agregarle contenido
                 document.Open();

                 //----------------------------------------------------------------------------------------------------             
                 //creamos las fuente que utilizaremos
              
                 iTextSharp.text.Font fontnegrita = new iTextSharp.text.Font(
                 FontFactory.GetFont(FontFactory.COURIER, 10, iTextSharp.text.Font.BOLD));

                 iTextSharp.text.Font font = new iTextSharp.text.Font(
                 FontFactory.GetFont(FontFactory.COURIER, 10, iTextSharp.text.Font.NORMAL));


                 //creamos parrafos con los datos del emisor
                Paragraph saltolinea = new Paragraph("\n", font);

                 ////////Paragraph parrafo_notaria = new Paragraph(notaria.nombrenotario + "\n" +
                 ////////                                          "NOTARIO PUBLICO" + "\n" +
                 ////////                                          notaria.nronotaria + "\n" +
                 ////////                                          notaria.direccionnotaria + "\n" +
                 ////////                                          notaria.comunanotaria + ", " + notaria.ciudadnotaria + "\n",
                 ////////                                          fontnegrita);
                 ////////parrafo_notaria.Alignment = 0;
                 ////////document.Add(parrafo_notaria);
                 ////////document.Add(saltolinea);

                 //----------------------------------------------------------------------------------------------------
                 String hoy = "";
                 hoy = DateTime.Now.Day + " de " + String.Format("{0:MMMM}", DateTime.Now).ToString() + " de " + DateTime.Now.Year;

                 //Paragraph fecha = new Paragraph(factoring.ciudadfactoring + ", " + hoy, font);
                 Paragraph fecha = new Paragraph(hoy, font);

                 fecha.Alignment =  0;
                 document.Add(fecha);

                 document.Add(saltolinea);
                 
                 Paragraph parrafo_deudor = new Paragraph("Señor(es)" + "\n" +
                                                          deudor.replegal + "\n" +
                                                          deudor.nombredeudor + "\n" +
                                                          deudor.direcciondeudor + "\n" +
                                                          deudor.comunadeudor + "\n", 
                                                          font);

                 Paragraph parrafo_contacto = new Paragraph("AT.: " + deudor.nombrecontacto + "-" + deudor.cargocontacto, fontnegrita);
                 parrafo_deudor.Alignment = Element.ALIGN_LEFT;
                 parrafo_contacto.Alignment = Element.ALIGN_LEFT;

                 document.Add(parrafo_deudor);
                 document.Add(parrafo_contacto);

                 document.Add(saltolinea); 
                 document.Add(saltolinea);

                 //////////////////Paragraph parrafo_uno = new Paragraph("Por medio de la presente notifico a ustedes que los créditos " + 
                 //////////////////                                      "que emanan de la(s) factura(s) que se identifica(n) a continuación, " +
                 //////////////////                                      "y cuya(s) copia(s) certificada(s) por el suscrito se acompaña(n), " +
                 //////////////////                                      "ha(n) sido cedida(s) por " + factoring.nombrecliente + " a " + factoring.factoring + ", " +
                 //////////////////                                      "R.U.T. N° " + factoring.rutfactoring + ", domiciliado en " + factoring.direccionfactoring + ", " +
                 //////////////////                                      "comuna de " + factoring.comunafactoring + ", " + factoring.ciudadfactoring + ", según contrato de cesión " +
                 //////////////////                                      "de créditos de fecha " + fechaoperacion + ".", 
                 //////////////////                                      font);

                 //////////////////Paragraph parrafo_uno = new Paragraph("Para los efectos legales correspondientes con esta fecha el Notario que suscribe esta " +
                 //////////////////                                      "carta certificada, Comunica a ustedes que por escritura pública de fecha " + fechaoperacion + " " + 
                 //////////////////                                      "otorgada en la notaría de " + notaria.nombrenotario + " el(los) crédito(s) de " +
                 //////////////////                                      "propiedad de " + factoring.nombrecliente + ", " +
                 //////////////////                                      "que emanan de la(s) factura(s) que se singulariza(n):", 
                 //////////////////                                      font);

                 Paragraph parrafo_uno = new Paragraph("De mi consideración:" + "\n" +
                                                       "Para los efectos de lo dispuesto en los artículos 1902 y siguientes del Código Civil y 162 y " + 
                                                       "siguientes del Código de Comercio, y conforme lo establecido en el artículo 7º de la ley Nº" +
                                                       "19.983, por la presente pongo en conocimiento de Uds. que por contrato de fecha " + fechaoperacion + ", " +
                                                       "cuyas firmas se autorizaron notarialmente, " + factoring.nombrecliente + " cedío a " + factoring.factoring + "," +
                                                       "RUT: " + factoring.rutfactoring + ", los créditos emanados de la(s) factura(s) emitidas a " +
                                                       "vuestra empresa cuya(s) fotocopia(s) autorizada(s) adjunto y que detallo a continuación.", font);

                 parrafo_uno.Alignment = Element.ALIGN_JUSTIFIED;
                 document.Add(parrafo_uno);
                 document.Add(saltolinea);

                 //----------------------------------------------------------------------------------------------------
                 //DETALLE DE LA FACTURA
                 //----------------------------------------------------------------------------------------------------
                 int filas = facturas.Count();
                 int i = 0;

                 PdfPTable tabledetalle = new PdfPTable(3);
                 tabledetalle.WidthPercentage = 30;
                 tabledetalle.HorizontalAlignment = Element.ALIGN_LEFT;  
                 

                 int[] widths = {70, 10,70};
                 tabledetalle.SetWidths(widths);
                 

                 //CREAMOS LA CABECERA DE LA TABLA DETALLE
                 PdfPCell cab1 = new PdfPCell(new Phrase("Número", fontnegrita));
                 PdfPCell cab2 = new PdfPCell(new Phrase("", fontnegrita));
                 PdfPCell cab3 = new PdfPCell(new Phrase("Monto", fontnegrita));
                 //PdfPCell cab4 = new PdfPCell(new Phrase("Fecha Vcto.", fontnegrita));
                 
                 cab1.HorizontalAlignment = 1;
                 cab2.HorizontalAlignment = 1;
                 cab3.HorizontalAlignment = 1;
                 //cab4.HorizontalAlignment = 1;

                 cab1.Border = 0;
                 cab2.Border = 0;
                 cab3.Border = 0;
                 //cab4.Border = 0;

                 tabledetalle.AddCell(cab1);
                 tabledetalle.AddCell(cab2);
                 tabledetalle.AddCell(cab3);
                 //tabledetalle.AddCell(cab4);
                 
                 PdfPCell det1;
                 PdfPCell det2;
                 PdfPCell det3;
                 //PdfPCell det4;

                 
                 //CREAMOS EL DETALLE
                 foreach (Documento d in facturas)
                 {
                         det1 = new PdfPCell(new Phrase(d.nrofactura, font));
                         det2 = new PdfPCell(new Phrase(d.Moneda, font));
                         det3 = new PdfPCell(new Phrase(d.monto, font));
                         //det4 = new PdfPCell(new Phrase(d.FechaVcto, font));

                         det1.Border = 0;
                         det2.Border = 0;
                         det3.Border = 0;
                         //det4.Border = 0;

                         ////det1.BorderWidthBottom = 0;
                         ////det2.BorderWidthBottom = 0;
                         ////det3.BorderWidthBottom = 0;
                         ////det4.BorderWidthBottom = 0;

                         ////det1.BorderWidthTop = 0;
                         ////det2.BorderWidthTop = 0;
                         ////det3.BorderWidthTop = 0;
                         ////det4.BorderWidthTop = 0;

                         ////det4.BorderWidthLeft = 0;

                         det1.HorizontalAlignment = 1;
                         det2.HorizontalAlignment = 2;
                         //det4.HorizontalAlignment = 2;
                         det3.HorizontalAlignment = 2;

                         tabledetalle.AddCell(det1);
                         tabledetalle.AddCell(det2);
                         tabledetalle.AddCell(det3);
                         //tabledetalle.AddCell(det4);
                     
                 }

                 //terminamos de dibujar tabla hasta 50 filas
                 for (int x = 0; x <= (filas - i); x++)
                 {
                     det1 = new PdfPCell();
                     det2 = new PdfPCell();
                     det3 = new PdfPCell();
                     //det4 = new PdfPCell();

                     det1.Border = 0;
                     det2.Border = 0;
                     det3.Border = 0;
                     //det4.Border = 0; 

                     ////if (x != (filas - i))
                     ////{
                     ////    det1.BorderWidthBottom = 0;
                     ////    det2.BorderWidthBottom = 0;
                     ////    det3.BorderWidthBottom = 0;
                     ////    det4.BorderWidthBottom = 0;
                     ////}

                     ////det1.BorderWidthTop = 0;
                     ////det2.BorderWidthTop = 0;
                     ////det3.BorderWidthTop = 0;
                     ////det4.BorderWidthTop = 0;
                   
                     ////det4.BorderWidthLeft = 0;
                   
                     tabledetalle.AddCell(det1);
                     tabledetalle.AddCell(det2);
                     tabledetalle.AddCell(det3);
                     //tabledetalle.AddCell(det4);
                 }

                 //Paragraph parrafo_tabla = new Paragraph("", font);  
                 document.Add(tabledetalle);
                 document.Add(saltolinea);

                 //////////////Paragraph parrafo_dos = new Paragraph("Conforme lo antes señalado, el pago de la(s) factura(s) precedentemente " +
                 //////////////                                      "individualizada(s) deberá efectuarse exclusivamente a " + factoring.factoring + ", en su " +
                 //////////////                                      "domicilio señalado en el párrafo primero de la presente, quién es el nuevo " +
                 //////////////                                      "acreedor para todos los efectos legales.",
                 //////////////                                      font);


                 //////////////Paragraph parrafo_dos = new Paragraph("ha(n) sido cedida(s) por su acreedor a " + factoring.factoring + ", RUT: " + factoring.rutfactoring + ", " +
                 //////////////                                      "domiciliado en " + factoring.direccionfactoring + ", " + factoring.comunafactoring  + ", " +
                 //////////////                                      "fono " + factoring.fonofactoring + " al cual se debe efectuar el pago en " + factoring.pagoenfactoring + ", " + 
                 //////////////                                      "para que esta(s) se entienda(n) cancelada(s), e informa que esta carta ha sido despachada por medio de esta notaría a petición del acreedor " + factoring.factoring,
                 //////////////                                      font);


                 Paragraph parrafo_dos = new Paragraph("Conforme lo expresado, para que se entienda(n) cancelada(s) la(s) señalada(s) factura(s), su pago deberáser hecho exclusivamente a " + factoring.factoring + "\n" +
                                                       "Por instrucciones de " + factoring.factoring + ", me permito informar lo siguiente:" + "\n" +
                                                       "1) Los pagos deberán ser hechos mediante cheque nominativo y cruzado, el cual será retirado por los cobradores de " + factoring.factoring +
                                                       " o bien podrá ser enviado a " + factoring.direccionfactoring + ", " + factoring.comunafactoring + "." + "\n" +
                                                       "2) Los pagos o depósitos en moneda extranjera, plaza fuera de Chile, NO serán recibidos, para mayor información, puede " +
                                                       "llamarse a los ejecutivos de cobranza de " + factoring.factoring + " a los telefonós " + factoring.fonofactoring + "." + "\n" +
                                                       "3) Se puede efectuar el pago depositando en nuestra cuenta corriente e informándonos vía e-mail." + "\n" +
                                                       "Cuenta Corriente: " + factoring.pagoenfactoring + "\n" +
                                                       "Banco: " + factoring.bancopagoenfactoring + "\n" +
                                                       "Razón Social: " + factoring.factoring + "\n" + 
                                                       "Rut: " + factoring.rutfactoring + "\n" + 
                                                       "Email: " + factoring.emailfactoring + "\n",                 
                                                       font);
                 parrafo_dos.Alignment = Element.ALIGN_JUSTIFIED;
                 document.Add(parrafo_dos);
                 document.Add(saltolinea);


                 //////////Paragraph parrafo_tres = new Paragraph("Para canalizar sus consultas, agradeceré tomar contacto con el Departamento " +
                 //////////                                       "de Cobranzas de " + factoring.factoring + " al teléfono " + factoring.fonofactoring + ", " + 
                 //////////                                       "o bien concurrir directamente a sus oficinas.",
                 //////////                                       font);

                 //////////parrafo_tres.Alignment = Element.ALIGN_JUSTIFIED;
                 //////////document.Add(parrafo_tres);
                 //////////document.Add(saltolinea);


                 //////////Paragraph parrafo_cuarto = new Paragraph("En cumplimiento a lo dispuesto en el articulo 7º de la ley N° 19.983, " +
                 //////////                                         "adjunto a la presente copia debidamente autorizada ante notario público competente de la(s)" + 
                 //////////                                         "factura(s) que contiene(n) el(los) crédito(s) cedidos, ya singularizada(s)",
                 //////////                                         font);

                 //////////parrafo_cuarto.Alignment = Element.ALIGN_JUSTIFIED;
                 //////////document.Add(parrafo_cuarto);

                 document.Add(saltolinea);
                 ////////////document.Add(saltolinea);
                 ////////////document.Add(saltolinea);
                 ////////////document.Add(saltolinea);


                 //Paragraph parrafo_final = new Paragraph(notaria.nombrenotario,
                 //                                        fontnegrita);
                 Paragraph parrafo_final1 = new Paragraph("Saluda atte. A Uds.",
                                                         font);

                 //parrafo_final.Alignment = Element.ALIGN_RIGHT;
                 parrafo_final1.Alignment = Element.ALIGN_LEFT ;

                 //document.Add(parrafo_final);
                 document.Add(parrafo_final1);

                 //esto es importante, pues si no cerramos el document entonces no se creara el pdf.
                 document.Close();

                 FileInfo file = new FileInfo(outputPath);
                 WriteToPdf(file, "");



             }
             catch (Exception e)
             {
                 document.Close();
             }

         }

        public void GeneraPagare(PagareBlanco pagare, String ruta, String rutaimagen)
        {

            String outputPath = ruta;
            Document document = new Document(iTextSharp.text.PageSize.LETTER);

            try
            {

                document.PageSize.Rotate();
                document.AddAuthor("BBVA Colombia");
                document.AddCreator("BBVA Colombia");
                document.AddCreationDate();
                document.AddTitle("PAGARE CREDITO DE TESORERIA");

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(outputPath, System.IO.FileMode.Create));

                document.Open();

                iTextSharp.text.Font fontnegrita = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD));
                iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL));

                Paragraph saltolinea = new Paragraph("\n");
                Paragraph saltopagina = new Paragraph("\f");

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------//
    
                #region parrafo hoja 1, 2, 3 (carta instrucciones)

                String punto1_hoja1 = "1. En el espacio reservado en el literal a). del pagaré arriba citado para colocar una suma de dinero, se consignará la cuantía a la que asciendan las obligaciones insolutas que por cualquier concepto o naturaleza, tenga(mos) contraída(s) o llegue(mos) a contraer, directa o indirectamente, individual o conjuntamente, en unión de varios de los abajo firmantes o de otras personas naturales o jurídicas y a favor o a la orden del <b>BBVA COLOMBIA</b>, incluídas sus prórrogas, renovaciones, reestructuraciones o refinanciaciones, denominadas en moneda legal colombiana o en moneda extranjera, ya sea producto de préstamos, créditos de corto plazo o de tesorería, aperturas de crédito, sobregiros, cartas de crédito sobre el exterior y el interior, avales o garantías, aceptaciones de letras, descuentos, diferencias de cambio, utilización y/o avances de tarjetas de crédito propia(s) o amparada(s) , compra de deudas o títulos con pacto de retroventa, financiación del precio de ventas a plazo, créditos adquiridos por el <b>BBVA COLOMBIA</b> a cargo de cualquiera del(de los) suscrito(s) por endoso o cesión de terceras personas o que provengan de cualquier otra operación activa de crédito o servicio financiero prestado por el <b>BBVA COLOMBIA</b> o que simplemente aparezcan registradas en los libros o consten en los archivos del <b>BBVA COLOMBIA</b> a cargo de todos, varios o uno cualquiera de los firmantes de este instructivo. Si alguna de las mencionadas obligaciones estuviere denominada en moneda extranjera el <b>BBVA COLOMBIA</b>  podrá expresar su valor en la divisa estipulada o en pesos colombianos liquidados a la tasa representativa del mercado del día en que decida llenar el pagaré o al tipo de cambio vigente para tales divisas, así como también podrá diligenciar los documentos que exijan las autoridades cambiarias para tal fin.";
                String punto2_hoja1 = "2. En el espacio reservado en el literal b). del pagaré arriba citado para colocar una suma de dinero, se colocará la cantidad que corresponda a la sumatoria de intereses remuneratorios y moratorios causados y no pagados sobre las sumas  de que trata el punto anterior, así como  comisiones, impuesto de timbre causado por el otorgamiento, diligenciamiento y utilización de dicho instrumento, lo mismo que la comisión por concepto de estudio de crédito y demás comisiones, honorarios de estudios de títulos, costos de avalúos y sus actualizaciones, gastos de cobranza si hubiere lugar a ella, honorarios del abogado que para el cobro";
                String punto2_hoja2 = "judicial o extrajudicial tenga establecidos el <b>BBVA COLOMBIA</b> de manera general y a los cuales me(nos) acojo(gemos), agencias en derecho, primas de seguros de vida de deudores y de daños, incendio y terremoto sobre los bienes dados en garantía, costas, diferencias de cambio, portes.";
                String punto3_hoja2 = "3. Como fecha de vencimiento de dicho pagaré, el <b>BBVA COLOMBIA</b> deberá colocarle la del día en que lo llene o diligencie. El lugar de cumplimiento del mismo será la ciudad donde se encuentre localizada la oficina del <b>BBVA COLOMBIA</b> donde deba pagarse la obligación a la que alude el punto primero de este instructivo.";
                String punto4_hoja2 = "4. El <b>BBVA COLOMBIA</b>  podrá diligenciar el mencionado pagaré en cualquier tiempo, sin que para el efecto sea necesario aviso o requerimiento judicial o extrajudicial ni formalidad previa alguna, en cualquiera de los siguientes casos: a). Mora o incumplimiento en el pago de cualquiera de las cuotas de capital, intereses, comisiones o demás accesorios de cualquiera de las obligaciones que el otorgante tenga con el Banco; b). Si los bienes de uno cualquiera de los otorgantes son embargados o perseguidos por cualquier persona o por el mismo <b>BBVA COLOMBIA</b> en ejercicio de cualquier acción; c). Si los bienes dados en garantía se gravan o enajenan en todo o en parte sin previo permiso escrito del <b>BBVA COLOMBIA</b> o se deprecian, demeritan o dejan de ser garantía suficiente; d). Si se llegare a solicitar, declarar o admitir a uno cualquiera de los otorgantes a algún proceso concursal  de los contemplados en la ley 1116 de 2007, concordato, liquidación obligatoria, liquidación forzosa, toma de posesión de sus negocios, reorganización empresarial o entrare en cualquier otro trámite de ejecución universal. e). Si falleciere alguno de los otorgantes persona natural, evento en el cual habrá derecho a exigir la totalidad de las sumas insolutas de que trata este instructivo, a uno cualquiera de los firmantes o a los herederos, sin necesidad de demandarlos a todos; f). La simple mora en el pago de cualquier obligación que el (los) otorgante (s) tenga(n) con el Banco o sus filiales, dará derecho a declarar de plazo vencido todas las demás obligaciones que se tengan con el Banco o sus filiales y a exigir la devolución de la totalidad de las obligaciones, las cuales se podrán incorporar en el pagaré.  g) En los demás casos de aceleración de los plazos previstos en la Ley.";
                String punto5_hoja3 = "5. Acepto(amos) incondicionalmente todo traspaso, endoso o cesión que el <b>BBVA COLOMBIA</b> haga del presente instructivo junto con el pagaré al cual corresponde y de la garantía que lo ampara, sin que para su efectividad sean necesarias, nuevas autorizaciones o aceptaciones.";
                
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                String Cabecera_h1 = "<div style='text-align:center; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                        "<b>CARTA DE INSTRUCCIONES ABIERTA ANEXA AL PAGARÉ No. _________</b>" +
                                     "</div><br>";

                String hoja1_1 = "<div style='text-align:left; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                     "Señores<br>" +
                                     "<b>BANCO BILBAO VISCAYA ARGENTARIA COLOMBIA S.A.</b><br>" +
                                     "<b>BBVA COLOMBIA</b><br>" +
                                     "Ciudad" +
                                 "</div><br>";

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                String hoja1_2 = "<div style='text-align: justify; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                 "El(los)suscrito(s) <u>" + pagare.rsoc_deudor + "</u>, " +
                                 "en los términos del Artículo 622 del Código de Comercio, permanente, expresa e irrevocablemente, faculto(amos) al " +
                                 "<b>BANCO BILBAO VISCAYA ARGENTARIA COLOMBIA S.A.,</b> en adelante <b>BBVA COLOMBIA</b> o a quien en el futuro ostente la calidad de " +
                                 "acreedor o tenedor legítimo del pagaré identificado con el número ___________________, para llenar en cualquier tiempo y " +
                                 "sin necesidad de previo aviso a mí(nosotros), todos los espacios en blanco de dicho instrumento, de conformidad con las " +
                                 "siguientes instrucciones: <br><br>" +
                                                          "<p style='text-align: justify; margin-left: 5px'>" + punto1_hoja1 + "</p><br>" +
                                                          "<p style='text-align: justify; margin-left: 5px'>" + punto2_hoja1 + "</p><br>" +
                                 "</div>";
                
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                String Cabecera_h2 = "<div style='text-align:center; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                        "<b>HOJA DOS (2) DE LA CARTA DE INSTRUCCIONES ABIERTA</b>" +
                                     "</div><br>";

                String hoja1_3 = "<div style='text-align: justify; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                     "<p style='text-align: justify'>" + punto2_hoja2 + "</p><br>" +
                                     "<p style='text-align: justify'>" + punto3_hoja2 + "</p><br>" +
                                     "<p style='text-align: justify'>" + punto4_hoja2 + "</p><br>" +
                                 "</div>";
                
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                String Cabecera_h3 = "<div style='text-align:center; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                        "<b>HOJA TRES (3) DE LA CARTA DE INSTRUCCIONES ABIERTA</b>" +
                                     "</div><br>";

                String hoja1_4 = "<div style='text-align: justify; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                    "<p style='text-align: justify'>" + punto5_hoja3 + "</p><br>" +
                                    "Así mismo autorizo(amos) al <b>BBVA COLOMBIA</b> o a quien en el futuro tenga la calidad de acreedor <br>" +
                                    "o tenedor legítimo del pagaré antes identificado, para diligenciar tanto en esta carta de instrucciones <br>" +
                                    "como en el título respectivo, los espacios relativos al número del pagaré, nuestros nombres, domicilios <br>" +
                                    "y la calidad en la que actuamos de conformidad con lo aquí indicado." +
                                 "</div><br>";

                String firmas_1 = "<div style='text-align:left; font-family:Times New Roman, Times, serif; font-size:10'>" +
                                  "Se firma esta carta en la ciudad de _________ a los ____ días del mes de _____________ del año _____.<br>" +
                                  "NOMBRE_________________________________&nbsp;&nbsp;&nbsp;&nbsp; NOMBRE__________________________________<br>" +
                                  "C.C. o NIT No._____________________________&nbsp;&nbsp;&nbsp; C.C. o NIT No.________________________________<br>" +
                                  "DIRECCION_______________________________&nbsp;&nbsp;&nbsp;&nbsp;DIRECCION_______________________________<br>" +
                                  "TELEFONO_______________________________&nbsp;&nbsp;&nbsp; TELEFONO________________________________<br><br>" +
                                  "_________________________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;________________________________________<br>" +
                                  "FIRMA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FIRMA" +
                                  "<br>" +
                                  "</div>";

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

                String hoja1 = Cabecera_h1 + hoja1_1 + hoja1_2;
                String hoja2 = Cabecera_h2 + hoja1_3;
                String hoja3 = Cabecera_h3 + hoja1_4 + firmas_1;
                
                List<IElement> htmlarraylist0 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(hoja1), null);
                Paragraph parrafo_hoja1 = new Paragraph("");
                parrafo_hoja1.InsertRange(0, htmlarraylist0);

                List<IElement> htmlarraylist1 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(hoja2), null);
                Paragraph parrafo_hoja2 = new Paragraph("");
                parrafo_hoja2.InsertRange(0, htmlarraylist1);

                List<IElement> htmlarraylist2 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(hoja3), null);
                Paragraph parrafo_hoja3 = new Paragraph("");
                parrafo_hoja3.InsertRange(0, htmlarraylist2);

                #endregion

                #region parrafo hoja 4 y 5 (PAGARE No.)
                
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------//
                
                String Cabecera_h4 = "<div style='text-align:center; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                  "<b>PAGARE No. ___________________________</b></div><br>";

                String Hoja4_1 = "<div style='text-align: justify; font-family: Times New Roman, Times, serif; font-size:11'> "
                                 + "Yo(Nosotros), <u>" + pagare.rsoc_deudor + "</u> " +
                                 "pagaré(mos) incondicional e indivisiblemente a la orden del <b>BANCO BILBAO VIZCAYA " +
                                 "ARGENTARIA COLOMBIA S.A.,</b> en adelante <b>BBVA COLOMBIA</b>, en su oficina " +
                                 "____________________ de la ciudad de ____________________, el dia _____ del mes de " +
                                 "___________________ del año __________, las siguientes cantidades de dinero que reconozco(emos)" +
                                 "solidariamente adeudarle: <b>a).</b> La suma de _______________________________________________________________;" +
                                 " y, <b>b).</b> La suma de _______________________________________________________________" +
                                 "A partir de la fecha de vencimiento anotada en este título valor, reconoceré(mos) y pagaré(mos) intereses moratorios sobre la suma consignada en el literal a). de este pagaré, liquidados a las tasas que estuvieren vigentes como límite máximo a cobrar de acuerdo con la ley o el reglamento, para cada período en que persista la mora. Además, a  partir de la fecha en que el <b>BBVA COLOMBIA</b>  instaure demanda judicial de cobro del presente pagaré, reconoceré(mos) y pagaré(mos) intereses moratorios sobre la suma consignada en el literal b). de este pagaré, si llevare más de un (1) año de mora, liquidados a la tasa máxima legal permitida. Autorizo(amos) expresa e irrevocablemente al <b>BBVA COLOMBIA</b>  para debitar, sin necesidad de aviso previo, de la(s) cuenta(s) corriente(s) y de ahorros que poseo(amos) conjunta o separadamente, en esa Institución o en sus filiales o subsidiarias, bien se trate de cuenta(s) conjunta(s) o alternativa(s), así como de los depósitos de cualquier naturaleza que en él mantuviese(mos), el valor insoluto de este pagaré y sus intereses a su vencimiento o en el momento de hacerse exigible por cualquiera de las causas de aceleración del plazo convenidas, así como el valor de las cuotas de amortización. Autorizo(amos) que el pago total o parcial, tanto de los intereses como del capital de este título, se hagan constar en registros sistematizados o manuales establecidos de manera general por el <b>BBVA COLOMBIA</b>  para contabilizar abonos de cartera y me(nos) acojo(gemos) expresamente al sistema de amortización que el <b>BBVA COLOMBIA</b> tiene establecido para el abono de los pagos del presente pagaré. Se hace constar que la responsabilidad solidaria y las garantías reales constituídas para respaldar el pago de este título, subsisten toda vez que el <b>BBVA COLOMBIA</b>  hace expresa reserva a la solidaridad prevista en el artículo 1.573 del Código Civil, entre otros eventos similares, en los siguientes casos: a). Prórroga o cualquier modificación a lo aquí estipulado, así éstas se pacten con uno solo de los firmantes, por cuanto desde ahora accedemos a ellas expresamente; b). Si se llegare a aprobar acuerdo concordatario respecto de alguno de los otorgantes; c). Si alguno de los otorgantes solicitare o es admitido o convocado a concordato; o, d). Si se llegare a recibir o a cobrar todo o parte del importe de este título a alguno(s) de los suscriptores. Acepto(amos) incondicionalmente todo endoso o cesión que el <b>BBVA COLOMBIA</b>  haga del presente pagaré, así como de la garantía que lo ampara, sin que para su efectividad sean necesarias, nuevas autorizaciones o aceptaciones. Queda entendido que toda garantía real o personal constituida conjunta o separadamente por el(los) suscriptor(es) de este título valor a favor del <b>BBVA COLOMBIA</b> o que el <b>BBVA COLOMBIA</b> llegare a adquirir por endoso o cesión de otras personas, amparará las obligaciones contenidas en este título así como sus prórrogas y demás modificaciones." + 
                                 "</div>";

                                
                String Cabecera_h5 = "<div style='text-align:center; font-family: Times New Roman, Times, serif; font-size:11'>" +
                                     "<b>HOJA DOS (2) DEL PAGARE No.___________________________</b></div>";

               
                String Hoja5_1 = "<div style='text-align:left; font-family:Times New Roman, Times, serif; font-size:10'>" +
                                  "Se firma esta carta en la ciudad de _________ a los ____ días del mes de _____________ del año _____.<br>" +
                                  "NOMBRE_________________________________&nbsp;&nbsp;&nbsp;&nbsp; NOMBRE__________________________________<br>" +
                                  "C.C. o NIT No._____________________________&nbsp;&nbsp;&nbsp; C.C. o NIT No.________________________________<br>" +
                                  "DIRECCION_______________________________&nbsp;&nbsp;&nbsp;&nbsp;DIRECCION_______________________________<br>" +
                                  "TELEFONO_______________________________&nbsp;&nbsp;&nbsp; TELEFONO________________________________<br><br>" +
                                  "_________________________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;________________________________________<br>" +
                                  "FIRMA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FIRMA" +
                                  "<br>" +
                                  "</div>";

                String hoja4 = Cabecera_h4 + Hoja4_1;
                String hoja5 = Cabecera_h5 + Hoja5_1;

                List<IElement> htmlarraylist3 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(hoja4), null);
                Paragraph Parrafo_hoja4 = new Paragraph("");
                Parrafo_hoja4.InsertRange(0, htmlarraylist3);

                List<IElement> htmlarraylist4 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(hoja5), null);
                Paragraph Parrafo_hoja5 = new Paragraph("");
                Parrafo_hoja5.InsertRange(0, htmlarraylist4);
                
                #endregion
                
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------//

                document.Add(parrafo_hoja1);
                document.Add(parrafo_hoja2);
                document.NewPage();
                document.Add(parrafo_hoja3);
                document.NewPage();
                document.Add(Parrafo_hoja4);
                document.NewPage();
                document.Add(Parrafo_hoja5);

                document.Close();

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------//
                
            }
            catch (Exception e)
            {
                document.Close();
            }

        }

        public void GeneraReglamento(Reglamento reglamento, String outputPath)
        {

            Document document = new Document(iTextSharp.text.PageSize.LETTER);

            try
            {

                document.PageSize.Rotate();
                document.AddAuthor("BBVA Colombia");
                document.AddCreator("BBVA Colombia");
                document.AddCreationDate();
                document.AddTitle("REGLAMENTO GENERAL DE TÉRMINOS Y CONDICIONES");

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(outputPath, System.IO.FileMode.Create));

                document.Open();

                iTextSharp.text.Font fontnegrita = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD));
                iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL));

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------//

                #region parrafo hoja 1, 2, 3 
                               
                
                String titulo = "<div style='text-align:center; font-family: Bookman Old Style; font-size:8'>" +
                                        "<b>REGLAMENTO GENERAL DE TÉRMINOS Y CONDICIONES</b><br>" +
                                        "<b>PARA EL PRODUCTO DE FACTORING OFRECIDO POR BBVA COLOMBIA</b><br>" +
                                     "</div><br>";

                String hoja1 = "<div style='text-align: justify; font-family: Bookman Old Style; font-size:8;line-height: 1em;'>" +
                                    "El presente reglamento fija los términos y condiciones del producto de Factoring ofrecido por BANCO BILBAO VIZCAYA ARGENTARIA COLOMBIA S.A., en adelante EL BANCO y regula las relaciones que surgen entre EL BANCO y EL CLIENTE cuando EL BANCO adquiera a descuento al CLIENTE títulos valores que este endosará en propiedad y/o se cederán los derechos económicos derivados del documento de crédito, SEGÚN CORRESPONDA, a favor de EL BANCO a cambio del pago pactado, de conformidad con lo establecido en la cláusula relativa al precio. En lo no dispuesto en éste reglamento, las partes se sujetarán a lo dispuesto en las normas vigentes.<br><br>" +
                                    "<b>    1.	CONDICIONES GENERALES A LOS PRODUCTOS DE FACTORING</b><br><br>" +
                                    "<b>1.1. Definiciones:</b> (a) Cliente: Es la persona natural o jurídica con la que el BANCO establece relaciones de origen legal o contractual a través de operaciones de Factoring mediante la compra con descuento de títulos valores o documentos contentivos de créditos; (b) Modalidades de Factoring: Las operaciones de Factoring se realizarán bajo las modalidades de FACTORING CON RECURSO y FACTORING SIN RECURSO; (c) Servicios: Son aquellas actividades conexas al desarrollo de las operaciones de Factoring, tales como canales o mecanismos físicos y electrónicos que se suministran a los CLIENTES en oficinas, bbva.net, terminales financieras y todos aquellos que disponga EL BANCO para que EL CLIENTE pueda realizar sus operaciones de Factoring; (d) Reglamento: Son los términos y condiciones convenidas entre EL BANCO y EL CLIENTE para  las operaciones de Factoring; (e) Petición: Será cualquier solicitud que no contenga inconformidad respecto de los productos, servicios o canales: (f) Queja o reclamo: Es la manifestación de inconformidad expresada por un CLIENTE respecto de un producto, servicio o canal, puesta en conocimiento del BANCO, el Defensor del Consumidor Financiero, la Superintendencia Financiera de Colombia o demás instituciones competentes; (g) Canales: son mecanismos de distribución de servicios financieros: oficinas, , bbva.net, terminales financieras y demás sistemas de acceso remoto para clientes.<br><br>" +
                                    "<b>1.2. Derechos del CLIENTE:</b> EL CLIENTE tendrá los siguientes derechos: (a) Recibir productos y servicios con estándares de seguridad y calidad de acuerdo con las condiciones ofrecidas y las obligaciones asumidas por EL BANCO; (b) Tener a su disposición información transparente, clara, veraz, oportuna y verificable, sobre las características propias de cada uno de los productos y servicios ofrecidos o suministrados por EL BANCO, que le permita su comprensión y comparación frente a los productos ofrecidos en el mercado; (c) Exigir la debida diligencia en la prestación del servicio por parte del BANCO; (d) Recibir una adecuada educación y orientación respecto de las diferentes formas de instrumentar los productos de Factoring y servicios ofrecidos, sus derechos y obligaciones, los mercados y tipo de actividad que desarrolla EL BANCO y los mecanismos de protección establecidos para la defensa de sus derechos; (e) Conocer gastos y tarifas que generan cada uno de los productos de Factoring y sus servicios asociados así como la utilización de los canales; (f) Presentar de manera respetuosa consultas, peticiones, quejas o reclamos ante EL BANCO, el Defensor del Consumidor Financiero, la Superintendencia Financiera de Colombia, los organismos de autorregulación; (g) Los demás derechos que se establezcan en la ley o en instrucciones de la Superintendencia Financiera de Colombia.<br><br>" +
                                    "<b>1.3. Obligaciones y prácticas de protección propias del CLIENTE:</b> EL CLIENTE, al solicitar y aceptar la oferta de productos de Factoring ofrecidos por el BANCO se compromete a realizar, en todo momento, las siguientes buenas prácticas de protección propia: (a) Informarse sobre los productos o servicios que adquiere o emplea, enterándose sobre las condiciones generales de la operación, derechos, obligaciones, costos y tarifas, exclusiones y restricciones aplicables y solicitar las explicaciones que considere necesarias para adoptar decisiones informadas; (b) Observar las medidas de seguridad y las recomendaciones e instrucciones que imparta EL BANCO para el manejo adecuado y seguro de los productos de Factoring, servicios y canales; (c) Revisar, leer y comprender los términos y condiciones del presente reglamento que para todos los efectos legales constituye el contrato y conservar la copia que le suministre EL BANCO; (d) informarse sobre los órganos y medios de que dispone EL BANCO para presentar peticiones, solicitudes, quejas o reclamos y el procedimiento previsto para obtener respuestas oportunas; (e) Acceder oportunamente a los canales que el BANCO tiene dispuestos para obtener información sobre sus cuentas o productos, de tal manera que cumpla a tiempo con sus obligaciones, compromisos y deberes; f) Las demás previstas en las normas aplicables, en este reglamento o en las condiciones especiales de cada producto o servicio.<br><br>" +
                                    "<b>1.4. Obligaciones especiales del BANCO:</b> EL BANCO tiene las siguientes obligaciones especiales: (a) Entregar los productos de Factoring y prestar los servicios en las condiciones informadas, ofrecidas o pactadas y emplear adecuados estándares de seguridad y calidad; (b) Suministrar información comprensible, cierta, suficiente y publicidad transparente, clara, veraz, oportuna y actualizada acerca de sus productos y servicios; (c) Contar con un Sistema de Atención al Consumidor Financiero “SAC”; (d) Abstenerse de incurrir en conductas que conlleven abusos contractuales; (e) Abstenerse de hacer cobros no pactados o no informados previamente al CLIENTE; (f) Dar constancia del estado y/o las condiciones específicas de los productos a una fecha determinada, en la forma acordada con EL CLIENTE, de conformidad con el procedimiento establecido o cuando EL BANCO se encuentre obligado; (g) Atender y dar respuesta oportuna a las peticiones, quejas o reclamos formulados por EL CLIENTE, siguiendo los procedimientos aplicables; (h) Dar a conocer por el respectivo canal y en forma previa a la realización de la operación, las tarifas y demás gastos asociados al producto o servicio, brindando la posibilidad de efectuarla o declinarla; (i) Disponer de los medios electrónicos y controles idóneos para brindar seguridad eficiente a las transacciones, a la información confidencial de EL CLIENTE y a las redes que la contengan; (n) Entregar copia de este reglamento y sus anexos a EL CLIENTE; (j) Conservar, por los plazos previstos en la ley, los comprobantes o soportes de las transacciones u operaciones realizadas; (k) Colaborar oportuna y diligentemente con las autoridades competentes, el organismo de autorregulación y el Defensor del Consumidor Financiero, en las actuaciones e investigaciones, en los casos que se requieran; (l) Las demás previstas en la ley, las que se deriven de la naturaleza del contrato o del servicio prestado y las que se deriven de las instrucciones de la Superintendencia Financiera de Colombia y los organismos de autorregulación.<br><br>" +
                                    "<b>1.5. Suministro, manejo y actualización de información:</b> (a) La información suministrada por EL CLIENTE y la que repose en los archivos del BANCO, sea comercial, profesional, técnica, administrativa o financiera, así como aquella a la que tenga acceso EL BANCO con ocasión o en desarrollo de las operaciones derivadas del presente reglamento, es confidencial y se encuentra amparada por la reserva bancaria, sin perjuicio del suministro de dicha información a las autoridades competentes o personas autorizadas; (b) EL CLIENTE entregará información veraz y verificable y actualizará su información personal, comercial y financiera por lo menos una (1) vez al año o cada vez que así lo solicite EL BANCO, o cuando se presenten variaciones; (c) EL CLIENTE imparte de manera expresa, permanente e irrevocable las siguientes autorizaciones: (i) al BANCO y a sus filiales, subsidiarias y/o subordinadas de su matriz, que existan o que se constituyan en el futuro, o a quien represente sus derechos, para almacenar, consultar, procesar, reportar, obtener, actualizar, compilar, tratar, intercambiar, enviar, modificar, emplear, eliminar, ofrecer, suministrar y divulgar a los operadores, centrales o bases de información y/o buró de crédito y/o cualquier otra entidad nacional o extranjera que tenga los mismos fines, la información de carácter personal, incluida la de carácter financiero, así como aquella que se derive de la relación y/o operaciones que llegue a celebrar con EL BANCO o con alguna de las entidades antes mencionadas, con fines de análisis de riesgos, estadísticos, de control, supervisión, encuestas, muestreos, pruebas de mercadeo y de actualización y verificación de información. La permanencia de la información positiva y negativa se regirá por lo dispuesto en la ley; (ii) a los operadores de información, bases de datos y/o buró de crédito, para tratar y/o administrar, dentro de los límites que disponen sus estatutos, reglamentos y la ley, la información personal suministrada en virtud de la presente autorización; (iii)  al BANCO, sus filiales, subsidiarias y/o subordinadas, así como a su matriz y las filiales, subsidiarias y vinculadas, en Colombia o en el exterior, que existan o que se constituyan en el futuro, para que utilicen, traten, intercambien, obtengan, actualicen, compilen, procesen, envíen, modifiquen y se suministren entre sí y/o con contratistas y/o terceras personas con las cuales se establezca relaciones comerciales, legales o contractuales, la información de carácter personal, incluida la de carácter financiero, así como aquella que se derive de la relación y/o operaciones o que llegaren a conocer,  siempre que a tales compañías, contratistas y/o terceros se les exija guardar la confidencialidad de la información de acuerdo con la ley y las políticas internas del banco. Esta información podrá ser suministrada para el desarrollo y prestación de los servicios principales, accesorios y conexos de las entidades, para el análisis de riesgo, para el cumplimiento de obligaciones establecidas en la ley o para fines de mercadeo, estadísticos, de control, comercialización de productos, supervisión, encuestas, muestreos y de actualización y verificación de información. En todo caso, EL CLIENTE se reserva el derecho de solicitar la no utilización de la información con fines de mercadeo y/o promoción de productos o servicios, mediante los medios de contacto establecidos en la página web: www.bbva.com.co. EL CLIENTE contará con los derechos que le otorgan las leyes de hábeas data y de protección de datos personales.<br><br>" +
                                    "<b>1.6. Obligaciones Especiales de EL CLIENTE:</b> EL CLIENTE, al celebrar operaciones de factoring y aceptar el presente reglamento se compromete a abstenerse de transferir mediante endoso a favor de EL BANCO facturas que: (i) no correspondan a una entrega real y material de bienes o a una efectiva prestación de servicios prestados en virtud de un contrato real o escrito; (ii) facturas que no hayan sido aceptadas o que se encuentren expresamente rechazadas, se encuentren en disputa comercial o que no hayan cumplido los requisitos, condiciones, términos y procedimientos de aceptación tácita previstos en los artículos 773 del Código de Comercio modificado por el articulo 20 de la ley 1231 de 2008 y lo previsto en el artículo 5  del Decreto 3327 de 2009. El incumplimiento de estas obligaciones especiales dará derecho inmediato a EL BANCO a terminar anticipadamente el presente contrato sin perjuicio de las acciones legales a que hubiere lugar.<br><br>" +
                                    "<b>1.7. Requisitos de los títulos valores y/o documentos contentivos de obligaciones crediticias:</b> Bajo la modalidad de “FACTORING” previo acuerdo con EL BANCO, EL CLIENTE podrá negociar y factorizar facturas de venta, títulos valores, actas de obra pública, sentencias judiciales, laudos arbitrales, actas de conciliación y cualquier documento en el que se incorporen obligaciones de contenido crediticio. Los títulos valores o documentos que incorporen obligaciones de contenido crediticio a factorizar deberán contener los elementos y cumplir los requisitos exigidos por la ley para cada uno de ellos. Las actas de obra pública deberán estar debidamente aceptadas por el interventor y la entidad contratante y contar con la apropiación presupuestal correspondiente por parte de la entidad pública contratante. Las sentencias judiciales y laudos arbitrales debidamente ejecutoriados y en firme. Las actas de conciliación aprobadas judicialmente en caso de tratarse de entidades públicas.  Los demás documentos en los que se incorporen obligaciones de contenido crediticio, deberán cumplir con los requisitos necesarios para que presten merito ejecutivo en los términos del artículo 488 del Código de Procedimiento Civil o las normas que lo modifiquen o adicionen.<br><br>" +
                                    "<b>1.8. Trasferencia de los títulos valores o documentos de crédito:</b> Cuando se trate del descuento de títulos valores la transferencia de la propiedad se realizará mediante endoso en propiedad, entrega real y material que efectuará EL CLIENTE a EL BANCO, de forma autónoma e independiente a la relación causal que les dio origen a los mismos. En el caso de factorizar documentos que incorporen obligaciones de contenido crediticio, tales como, actas de obra pública, sentencias judiciales y laudos arbitrales, así como cualquier documento en el que se incorporen obligaciones de crédito la transferencia de la propiedad se realizará mediante el contrato de cesión respectivo, la notificación al contratante cedido y la aceptación expresa de éste último.<br><br>" +
                                    "<b>1.9. Titularidad y representantes:</b> Podrá ser CLIENTE del BANCO toda persona natural o jurídica que descuente títulos valores o documentos de crédito con el BANCO. Si EL CLIENTE es persona natural actuará directamente o a través de su apoderado debidamente constituido. Si EL CLIENTE es persona jurídica actuará a través del  representante legal debidamente inscrito en la cámara de comercio o de las personas designadas por este. EL CLIENTE  y sus representantes o apoderados son los únicos facultados para operar los productos y servicios de Factoring. Si EL CLIENTE o una autoridad revoca o modifica las facultades de los representantes o apoderados, o los remplaza, deberá comunicarlo oportunamente al BANCO, adjuntando la documentación necesaria. Las condiciones de operación del producto de Factoring serán las que figuran en los registros internos del BANCO y no podrán ser modificadas unilateralmente. Cuando existan personas autorizadas para la realización de operaciones de Factoring, las condiciones y autorizaciones permanecerán vigentes hasta que se notifiquen por el CLIENTE y se acepten por el BANCO las modificaciones respectivas.<br><br>" +
                                    "<b>1.10. Comunicaciones:</b> Se conviene que el BANCO podrá enviar comunicaciones e información a los CLIENTES mediante cartas, circulares, avisos colocados en las instalaciones del BANCO abiertas al público, publicaciones en diarios de circulación nacional o local, volantes que acompañen los estados de cuenta, correos electrónicos, fax, información verbal, telefónica, publicada en la página WEB o cualquier otro medio, sobre las condiciones de prestación de los productos e Factoring o servicios asociados, comisiones, horarios de  funcionamiento, tasas de interés, costos y gastos asociados al producto o servicio, promociones, y cualquier otra información, actualización, aclaración o  modificación del presente reglamento, la cual se entenderá aceptada por el CLIENTE en los términos aquí previstos.<br><br>" +
                                    "<b>1.11. Estados de cuenta:</b> EL BANCO pondrá a disposición de LOS CLIENTES el estado de cuenta de las operaciones de Factoring a la última dirección física o electrónica registrada por EL CLIENTE o en la página Web. El estado de cuenta informará las operaciones realizadas, tasa de descuento acordada para cada título valor o documento de crédito factorizado y fecha de vencimiento. EL BANCO llevará un registro de todos los títulos valores y documentos de crédito factorizados. La periodicidad y la fecha para el envío de los estados de cuenta podrá ser modificada por EL BANCO, comunicando oportunamente al CLIENTE, de conformidad con lo previsto en las leyes vigentes. En caso de pérdida o extravío de los estados de cuenta, EL CLIENTE deberá solicitar en la oficina donde tenga radicadas sus operaciones de Factoring un duplicado o información sobre el estado de las mismas, de acuerdo con las tarifas que tenga vigentes EL BANCO, o consultar electrónicamente en los canales que EL BANCO tenga habilitados para el efecto. Estas circunstancias o cualquier otra no alteran los plazos pactados, ni los montos o fechas de pago previamente acordadas con exactitud.<br><br>" +
                                    "<b>1.12. Imputación de pagos y autorización de débitos a cuentas:</b> Todo pago efectuado por EL CLIENTE y o el deudor del Titulo valor y/o documento de contenido crediticio se imputará en el siguiente orden: tarifas, comisiones, impuestos, primas de seguro, gastos de cobranza administrativa, extrajudicial y judicial si hubiere lugar a ellos, honorarios del abogado y costas, intereses de mora, si se hubieren causado, intereses remuneratorios causados y vencidos, amortización a capital de las obligaciones vencidas, en orden de antigüedad de los respectivos vencimientos. Si quedaren saldos se aplicaran a obligaciones no vencidas, de conformidad con lo acordado con EL CLIENTE. En el evento de existir varias obligaciones vencidas, en virtud de lo dispuesto en los artículos 1564 y 1565 del Código Civil, EL DEDUOR faculta al BANCO para elegir a cuál de ellas se imputa un abono determinado, a menos que EL DEUDOR le indique otra cosa por escrito el mismo día en que haga el pago, siempre que se prefiera el pago de una obligación vencida. EL CLIENTE autoriza al BANCO para debitar de las cuentas y/o productos abiertos en EL BANCO, sin necesidad de previa comunicación, las sumas de dinero por concepto de títulos valores descontados en desarrollo de  las relaciones comerciales derivadas del presente reglamento y no pagados oportunamente por el comprador o beneficiario del servicio facturado.<br><br>" +
                                    "<b>1.13. Otras Disposiciones:</b> (a) EL CLIENTE impartirá todas las autorizaciones que sean necesarias para que el presente reglamento pueda ser ejecutado por EL BANCO; (b) Se reconocerán como prueba de las operaciones realizadas en desarrollo del presente reglamento, los comprobantes, registros magnéticos, archivos y/o los documentos en los que quede evidencia que las operaciones de Factoring fueron emitidas o provienen del CLIENTE o de una persona por él autorizada; (c) EL BANCO podrá suspender el presente reglamento y la operativa del contrato de Factoring absteniéndose de factorizar títulos valores o documentos de crédito cuando se presenten situaciones que puedan generar riesgo para EL CLIENTE o EL BANCO, cuando resulte impaga alguna de las facturas, títulos valores y/o documentos de contenido crediticio factorizados, cuando los deudores de las de las facturas sean objeto de medidas de embargo que afecten materialmente su situación financiera o cuando se presenten situaciones de fraude o posible fraude. Estas medidas podrán adoptarse hasta que desaparezcan las causas que las motivaron; (d) El incumplimiento por parte del CLIENTE en el pago oportuno de las obligaciones crediticias derivadas de tarjetas de crédito, sobregiros, crédito rotativo, comercial y en general cualquier obligación dineraria contraída con EL BANCO y/o derivada del descuento de títulos valores y/o documentos que contengan obligaciones de crédito descontadas en desarrollo del producto de Factoring o el incumplimiento de cualquiera de las condiciones previstas en el presente reglamento, generará la suspensión de los cupos de crédito, sin perjuicio de las acciones legales a que hubiere lugar, quedando EL BANCO facultado para declarar el vencimiento anticipado de todos los plazos y exigir la cancelación total de todas las sumas a cargo del CLIENTE; (e) EL BANCO está facultado para determinar los Canales a través de los cuales EL CLIENTE podrá efectuar las operaciones de Factoring. Así mismo, podrá determinar los  montos, número de operaciones permitidas y horarios de atención. EL BANCO podrá modificar la disponibilidad de los referidos Canales, horarios de funcionamiento u otras condiciones por razones de seguridad, utilizando para el efecto los mecanismos de comunicación establecidos en  el presente reglamento; (f) Serán a cargo del CLIENTE  los gastos de cobranza en que incurra EL BANCO en caso de acción judicial o  extrajudicial iniciada para lograr el pago de las obligaciones a cargo del CLIENTE; (g) EL CLIENTE suscribirá los pagarés de contragarantía o con espacios en blanco que sean necesarios, que podrán ser diligenciados por el Banco de acuerdo con las instrucciones respectivas.<br><br>" +
                                    "<b>1.14. Terminación de la relación contractual:</b> EL CLIENTE podrá en cualquier momento disponer la terminación del contrato de FACTORING CON RECURSO a través de cualquiera de los canales habilitados por EL BANCO. Correlativamente EL BANCO podrá disponer la terminación del contrato de FACTORING CON RECURSO en cualquiera de los siguientes casos: (i) Si a criterio del BANCO existen dudas o conflictos respecto a la legitimidad, legalidad, vigencia o alcance de las facultades de los representantes del CLIENTE o legalidad de la procedencia de los títulos valores o documentos de crédito factorizados; (ii) Si la información proporcionada por EL CLIENTE resulta falsa, inexacta o incompleta en forma total o parcial;  (iii) si EL CLIENTE incurre en cualquiera de las conductas previstas en los numerales 1.6 y 2.10 del presente Reglamento; (iv)  Si EL CLIENTE utiliza los cupos de crédito aprobados por montos superiores a los aprobados por EL BANCO; (v) Si el nombre del CLIENTE aparece relacionado en una lista pública por supuesta vinculación con delitos narcotráfico, lavado de activos, terrorismo, secuestro, extorsión o cualquiera de los relacionados en el artículo 323 del Código penal colombiano o normas que lo modifiquen o sustituyan; (iv) cuando existan otras razones objetivas. En estos casos EL BANCO podrá disponer la terminación y cierre de las cuentas, productos y servicios accesorios a los mismos, comunicando su decisión al CLIENTE vía correo electrónico o a la última dirección registrada en EL BANCO, con una anticipación no menor a quince (15) días calendario, salvo las causales señaladas en los incisos (ii), (iii), (iv) y (v), en cuyo caso la decisión será informada dentro de los tres (3) días siguientes a la terminación de la relación contractual a través de cualquier otro canal habilitado para el efecto.<br><br>" +
                                    "<b>1.15. Aceptación del presente reglamento:</b> Este Reglamento se entenderá aceptado por EL CLIENTE con la suscripción de la solicitud de vinculación y contratación de productos y/o con la suscripción de la tarjeta de firmas en la que se establecen las condiciones de manejo de las operaciones de Factoring y/o con el descuento de un título valor o documento de crédito. Por utilización se entiende el hecho de beneficiarse EL CLIENTE del servicio o la realización operaciones de FACTORING CON RECURSO y/o de los servicios anexos al mismo. EL CLIENTE tendrá la libertad de adherir o no a los términos y condiciones del presente reglamento. No habrá lugar a aceptaciones condicionales o parciales. El reglamento y cada una de sus actualizaciones sustituyen los reglamentos o contratos precedentes, conservando vigencia y efectos jurídicos el último ejemplar entregado o comunicado. EL BANCO se reserva el derecho de realizar actualizaciones o modificaciones cuando las circunstancias de mercado o regulatorias así lo ameriten, las cuales serán comunicadas con 45 días de antelación a su entrada en vigencia.<br><br><br>" +
                                    "<b>    2.	CONDICIONES ESPECIALES PARA FACTORING CON RECURSO.</b><br><br>" +
                                    "A las operaciones de FACTORING CON RECURSO además de las disposiciones de condiciones generales establecidas en el Capitulo 1 del presente reglamento, aplicaran las siguientes condiciones:<br><br>" +
                                    "<b>2.1.</b> A través de la modalidad de FACTORING CON RECURSO se podrán factorizar títulos valores y/o documentos que incorporen obligaciones de contenido crediticio tales como, actas de obra pública aceptadas y con certificado de disponibilidad presupuestal para su pago, sentencias judiciales y/o laudos arbitrales con su constancia de ejecutoria, actas de conciliación y, en general, cualquier documento en el que se incorporen obligaciones de crédito.<br><br>" +
                                    "<b>2.2.</b> EL CLIENTE se compromete a negociar títulos valores o documentos que incorporen obligaciones de contendido crediticio, vigentes, expresas, claras y exigibles, que correspondan a la entrega material y real de bienes o a la prestación de servicios debidamente aceptados por el deudor de los mismos y que no se encuentren afectados por notas crédito, devoluciones de mercancía, disputas comerciales o contractuales, ni otros eventos o discusiones sobre el nacimiento o exigibilidad de las mismas. En el evento de presentarse alguna disputa comercial o discusión que afecte la existencia, validez, del titulo valor y/o documento de contenido crediticio factorizado,  EL CLIENTE se compromete expresamente a informar inmediatamente y por escrito al BANCO sobre tal hecho conocido y se obliga a más tardar dentro de los tres (3) días comunes siguientes a remplazar por otros títulos valores y/o documentos de contenido crediticio, libres de disputas comerciales o discusiones.<br><br>" +
                                    "<b>2.3.</b> Los títulos valores que EL BANCO factorizará deberán ser endosados en propiedad y con responsabilidad cambiaria a cargo de EL CLIENTE y ser entregados real y materialmente al BANCO.<br><br>" +
                                    "<b>2.4.</b> Cuando se trate del descuento de documentos que contengan obligaciones de contenido crediticio la transferencia de la propiedad se realizará mediante la suscripción del contrato de cesión, la notificación al contratante cedido y la aceptación expresa de éste último. EL CLIENTE no podrá sin autorización previa y escrita del BANCO introducir cualquier modificación, otrosí, prórroga y/o terminación al documento, convenio y/o o contrato, contentivo de la obligación dineraria de contenido crediticio y factorizado en desarrollo el presente reglamento.<br><br>" +
                                    "<b>2.5.</b> La celebración de toda operación de FACTORING CON RECURSO incluidas aquellas en las que se descuenten títulos valores, deberá estar precedida de: (i) Aceptación de la factura y/o acta de obra y/o sentencia judicial con constancia de ejecutoria, por parte del deudor cedido; (ii) Notificación de El CLIENTE a su deudor cedido del endoso y/o cesión de derechos económicos a favor del BANCO en la que además se indique que los pagos únicamente deberán realizarse a favor del BANCO; y, (iii) Aceptación expresa y por escrito del deudor cedido de pagar directamente al BANCO. Las notas de cesión y aceptación según el caso, deberán ser elaboradas en los términos establecidos por el BANCO y entregadas al BANCO. Las notas de endoso deberán estar suscritas por el representante legal de EL CLIENTE o por un apoderado facultado para el efecto. Todo, sin perjuicio de lo previsto en los procedimientos aplicables a las operaciones electrónicas que celebren las partes, si fuere el caso.<br><br>" +
                                    "<b>2.6.</b> En las operaciones que se realicen bajo la modalidad de FACTORING CON RECURSO EL CLIENTE es solidariamente responsable del pago efectivo del crédito junto con el deudor cedido, con los respectivos intereses de mora, en los términos previstos en este reglamento.<br><br>" +
                                    "<b>2.7.</b> EL BANCO se reserva el derecho de cobrar judicial o extrajudicialmente el título factorizado a EL CLIENTE y/o deudor cedido y/o obligado al pago. EL BANCO podrá perseguir el pago de los créditos factorizados contra cualquier persona obligada al pago, incluido el CLIENTE para lo cual podrá diligenciar el pagaré de contragarantía otorgado por EL CLIENTE a la suscripción del presente Reglamento.<br><br>" +
                                    "<b>2.8.</b> EL BANCO solo estará obligado a devolver a EL CLIENTE los títulos valores y/o documentos que contengan obligaciones crediticias que fueron objeto FACTORING CON RECURSO, cuando EL CLIENTE haya cancelado el valor total de los títulos factorizados con sus respectivos intereses al BANCO y/o los títulos sean remplazados por aquellos sobre los cuales exista disputa comercial o discusión, caso en el cual procederá a realizar la nota de endoso o cesión, según sea el caso, sin asumir responsabilidad cambiaria de ninguna naturaleza.<br><br>" +
                                    "<b>2.9.</b> En el evento que el deudor del título valor y o documento de contendido crediticio objeto de una operación de FACTORING CON RECURSO cancele el valor del mismo a EL CLIENTE, éste deberá informar del hecho al BANCO de inmediato, y transferirle la totalidad de lo recibido dentro de los dos (2) días hábiles siguientes a la fecha en que hubiere recibido el pago, asumiendo todos los costos, cargas y gravámenes de la transferencia. En caso de mora, se causarán intereses moratorios a cargo de EL CLIENTE y a favor del BANCO, conforme a lo dispuesto en la cláusula décimo segunda de este reglamento.<br><br>" +
                                    "<b>2.10.</b> Las operaciones de FACTORING CON RECURSO se harán con base en las condiciones financieras relativas a plazos, tasas de interés y tasas de descuento previamente acordadas entre EL BANCO y EL CLIENTE. Las condiciones financieras podrán: (a) Ser acordadas en documento separado y suscrito por las partes, el cual hará parte integral del presente reglamento; y/o, (b) Constar en comunicaciones cruzadas entre las partes, a través de fax, cartas o correos electrónicos en la que conste la aceptación expresa del CLIENTE; documentos que harán parte integral del presente reglamento. A pesar de lo previsto en la regla anterior, EL BANCO podrá establecer unilateralmente, en forma temporal o indefinida, nuevas condiciones financieras más favorables para EL CLIENTE, sin que pierdan vigencia las condiciones inicialmente pactadas, las cuales podrán aplicarse nuevamente en cualquier momento, aun por decisión unilateral del BANCO.<br><br>" +
                                    "<b>2.11.</b> Si EL DEUDOR del titulo valor y/o documento de contenido crediticio factorizado y/o el CLIENTE no efectúan el pago total y oportuno de las obligaciones a su cargo, en la fecha límite de pago: (i) Se devengarán intereses de mora a la tasa de interés moratorio permitida por la ley, (ii) EL BANCO podrá suspender el descuento de más títulos valores y/o documentos de contenido crediticio; y, (iii) si la mora persiste por más de 30 días calendario, EL BANCO podrá dar por terminados los servicios de operaciones de FACTORING CON RECURSO ejecutados bajo el presente reglamento y exigir el pago inmediato y total de las sumas insolutas por concepto de capital, intereses y demás gastos asociados a las operaciones de FACTORING CON RECURSO, sin perjuicio de las acciones legales pertinentes.<br><br>" +
                                    "<b>2.12.</b> EL BANCO, el deudor del título valor o documento de contenido crediticio factorizado y EL CLIENTE podrán establecer, cuando lo estimen pertinente, los procedimientos específicos que habrán de observarse para celebrar cualquiera de las operaciones de FACTORING CON RECURSO, tanto para la presentación de los títulos valores y/o documentos de contenido crediticio factorizados, como para el pago de los mismos. Estos procedimientos harán parte integral del presente reglamento.<br><br>" +
                                    "<b>2.13.</b> EL CLIENTE tendrá la posibilidad de utilizar los medios y canales electrónicos que EL BANCO disponga para celebrar operaciones de FACTORING CON RECURSO previstas en este reglamento. Para el efecto, EL CLIENTE suscribirá con EL BANCO en documento anexo, las condiciones de operación y procedimientos que se aplicaran a estas operaciones, documento que hará parte integral del presente reglamento.<br><br>" +
                                    "<b>2.14.</b> EL BANCO podrá declarar de plazo vencido todas o algunas de las obligaciones anotadas en la cuenta de EL CLIENTE y exigir el pago de todas ellas más los intereses causados, accesorios, costas, gastos y honorarios de cobranzas judicial o extrajudicial si a ello hubiere lugar, en los siguientes casos: (i) mora en el pago de cualquiera de las obligaciones a cargo del deudor de los títulos valores o documentos de contendido crediticio factorizados y/o del CLIENTE, sus codeudores o avalistas; (ii) embargo de uno o más bienes pertenecientes al deudor de los títulos valores o documentos de contendido crediticio factorizados y/o del CLIENTE, sus codeudores o avalistas, que afecten materialmente y/o deterioren la situación financiera de cualquiera de ellos, que permita a la entidad temer el incumplimiento total o parcial de las obligaciones contraídas a su favor; (iii) haberse solicitado al deudor de los títulos valores o documentos de contendido crediticio factorizados y/o del CLIENTE, sus codeudores o avalistas sean admitidos en un trámite de reorganización empresarial, liquidación voluntaria o judicial o un proceso concursal equivalente o semejante a éstos; (iv) Por haberse demeritado las garantías reales o personales constituidas para asegurar el cumplimiento de las obligaciones  a cargo de EL CLIENTE; (v) cuando el deudor de los títulos valores o documentos de contendido crediticio factorizados y/o el CLIENTE, sus codeudores, avalistas o cualquiera de sus socios llegaren a ser vinculados por parte de las autoridades competentes a cualquier tipo de investigación por delitos de narcotráfico, terrorismo, secuestro, lavado de activos, financiación de terrorismo y administración de recursos relacionados con actividades terroristas u otros delitos relacionados con el lavado de activos y financiación de terrorismo y/o sean incluidos en listas para el control de Lavado de Activos y financiación del terrorismo, administradas por cualquier autoridad nacional o extranjera, tales como la Oficina de Control de Lavado de Activos en el Exterior – OFAC, emitida por la Oficina del Tesoro de los Estados Unidos de Norteamérica, la Lista de la Organización de las Naciones Unidas y otras listas públicas relacionadas con el tema de lavado de activos y financiación del terrorismo y/o condenados por parte de las autoridades competentes en cualquier tipo de proceso judicial relacionado con la comisión de los anteriores delitos. (vi) la terminación de la relación contractual por alguna de las causas contempladas en el numeral 1.14 de este reglamento.<br><br>" +
                                    "<b>2.15.</b> EL CLIENTE  a través de su representante legal, cuando a ello hubiere lugar y debidamente facultado para el efecto, suscribe y entrega a EL BANCO un pagaré de contragarantía con espacios en blanco el cual será diligenciado por EL BANCO de acuerdo con la respectiva carta de instrucciones.<br><br>" +
                                    "<b>2.16.</b> EL CLIENTE se compromete a entregar  información completa, veraz y verificable del deudor de los títulos valores o documentos de contendido crediticio factorizados, sus pagadores, procedimientos de pago, fechas establecidas para el pago de los títulos valores  y/o documentos de contenido crediticio factorizados. EL BANCO queda facultado para  suministrar pagadores de las entidades deudoras de los títulos valores y/o documentos e contenido crediticio factorizados,  o a las personas señaladas por EL CLIENTE y/o a las Compañías de Seguros de crédito, información sobre el estado de las obligaciones derivadas del descuento de los títulos valores y o documentos de contenido crediticio factorizados, de la relación comercial y el valor máximo que tiene disponible EL CLIENTE aprobado en EL BANCO para operaciones de FACTORING CON RECURSOS.<br><br>" +
                                    "<b>2.17.</b> EL CLIENTE se compromete a atender las solicitudes de información que requiera el BANCO  para el adecuado desarrollo y ejecución de las operaciones de FACTORING CON RECURSO en desarrollo del contrato de Factoring regulado en éste documento y a prestar tota la colaboración necesaria para obtener el pago oportuno de los títulos valores y/o documentos de contenido crediticio factorizados.<br><br>" +
                                    "<b>2.18.</b> EL CLIENTE declara que los títulos valores y/o documentos de contenido crediticio factorizados en desarrollo del contrato de Factoring, se encuentran libres de embargos y gravámenes, de deducciones de cualquier índole y no han sido transferidos con anterioridad mediante endoso y/o cesión. Así mismo, EL CLIENTE se compromete a no endosarlos y/o cederlos ni transferirlos a otra persona.<br><br>" +
                                    "<b>2.19.</b> EL CLIENTE se compromete  a prestar tota la colaboración y a adelantar todas las gestiones necesarias para obtener el pago al Banco de los títulos valores y/o documentos de contenido crediticio factorizados en desarrollo del contrato de Factoring que se regula por el presente reglamento, sus prórrogas, renovaciones, y/o modificaciones, y a presentar los soportes que se requieran para que el deudor de los títulos valores y/o documentos de contenido crediticio realice los pagos correspondientes y, en general, a cumplir todos los requisitos contractuales y administrativos para el pago de las cuentas y/o facturas y/o títulos valores factorizados.<br><br>" +
                                    "<b>2.20.</b> EL BANCO tendrá la facultad de transferir total o parcialmente los créditos que surjan a cargo de EL CLIENTE en desarrollo y ejecución del contrato de FACTORING CON RECURSO regulado en el presente reglamento. La persona natural o Jurídica que adquiera los créditos se encuentran facultados para diligenciar el pagaré o pagarés previstos en el numeral 2.15  y exigir su cobro judicial o extrajudicialmente. Para tal efecto, EL CLIENTE autoriza al BANCO para que suministre a los adquirentes de los créditos a los que hace referencia el presente numeral, copia del presente reglamento, del formulario de vinculación vigente suscrito por el CLIENTE, soportes de cada una de las operaciones de FACTORING CON RECURSO, pagarés, facturas, títulos valores y/o documentos de contenido crediticio factorizados y/o copia de los mismos, anotaciones contables.<br><br>" +
                                    "<b>2.21.</b> En cumplimiento de las normas y procedimientos relacionados con el Sistema de Administración del Riesgo de Lavado de Activos y de Financiación del Terrorismo (SARLAFT), EL CLIENTE garantiza que los títulos valores y/o documentos de contenido crediticio, provienen de negocios celebrados y ejecutados válidamente y de buena fe, de la venta de bienes y/o prestación de servicios en desarrollo de su actividad comercial y de actividades lícitas, que no son producto de actividades relacionadas con lavado de activos y/o financiación del terrorismo.<br><br>" +
                                    "<b>2.22.</b> Este reglamento es de duración indefinida. Sin embargo, cualquiera de las partes podrá darlo por terminado mediante aviso dado a la otra, por escrito con la antelación determinada en las leyes vigentes para el momento en que decida hacer afectiva la terminación. La terminación ocurrirá desde el momento de la recepción de la comunicación y en consecuencia a no se factorizar más títulos valores y/o documentos de contenido crediticio. No obstante lo anterior, los títulos valores y/o documentos de contendido crediticio que se encuentren factorizados hasta la fecha de la terminación y pendientes de pago por sus deudores y/o EL CLIENTE  seguirán sometidas al presente reglamento y a los términos y condiciones especiales acorados para cada uno de ellos.<br><br>" +
                                    "<b>2.23.</b> Todo impuesto, carga, gravamen o retención de cualquier naturaleza que eventualmente llegue a causarse por razón de cualquiera de las operaciones de FACTORING CON RECURSO, estará a cargo de EL CLIENTE.<br><br>" +
                                    "<b>2.24.</b> EL BANCO no garantiza, ni avala al CLIENTE en sus obligaciones contractuales, no atenderá ni tramitará glosas o reclamos derivados de los títulos valores endosados y/o los documentos de contenido crediticio cedidos, ni asumirá ningún tipo de penas o multas por el incumplimiento de los mismos, ni asume riesgos por demora en los pagos que deba hacer el deudor de los documentos factorizados. El BANCO no está obligado a posponer los vencimientos, ni a reestructurar las obligaciones contenidos en los títulos valores y/o documentos de contenido crediticio factorizados.<br><br>" +
                                    "<b>    3.	CONDICIONES ESPECIALES PARA FACTORING SIN RECURSO.</b><br><br>" +
                                    "A las operaciones de FACTORING SIN RECURSO además de las disposiciones de condiciones generales establecidas en el Capitulo 1 del presente reglamento, aplicaran las siguientes condiciones:<br><br>" +
                                    "<b>3.1.</b> A través de la modalidad de FACTORING SIN RECURSO se podrán factorizar únicamente títulos valores.<br><br>" +
                                    "<b>3.2.</b> EL CLIENTE se compromete a negociar títulos valores que incorporen obligaciones de contendido crediticio, vigentes, expresas, claras y exigibles, que correspondan a la entrega material y real de bienes o a la prestación de servicios debidamente aceptados por el deudor de los mismos y que no se encuentren afectados por notas crédito, devoluciones de mercancía, disputas comerciales o contractuales, ni otros eventos o discusiones sobre el nacimiento o exigibilidad de las mismas. En el evento de presentarse alguna disputa comercial o discusión que afecte la existencia, validez, del titulo valor y/o documento de contenido crediticio factorizado,  EL CLIENTE se compromete expresamente a informar inmediatamente y por escrito al BANCO sobre tal hecho conocido y se obliga a más tardar dentro de los tres (3) días comunes siguientes a remplazar por otros títulos valores y/o documentos de contenido crediticio, libres de disputas comerciales o discusiones.<br><br>" +
                                    "<b>3.3.</b> Los títulos valores que EL BANCO factorizará deberán ser endosados en propiedad y ser entregados real y materialmente al BANCO.<br><br>" +
                                    "<b>3.4.</b> La celebración de toda operación de FACTORING SIN RECURSO deberá estar precedida de: (i) Aceptación de la factura por parte del deudor cedido; (ii) Notificación de El CLIENTE a su deudor cedido del endoso a favor del BANCO en la que además se indique que los pagos únicamente deberán realizarse a favor del BANCO; y, (iii) Aceptación expresa y por escrito del deudor cedido de pagar directamente al BANCO. Las notas de cesión y aceptación según el caso, deberán ser elaboradas en los términos establecidos por el BANCO y entregadas al BANCO. Las notas de endoso deberán estar suscritas por el representante legal de EL CLIENTE o por un apoderado facultado para el efecto. Todo, sin perjuicio de lo previsto en los procedimientos aplicables a las operaciones electrónicas que celebren las partes, si fuere el caso.<br><br>" +
                                    "<b>3.5.</b> EL BANCO solo estará obligado a devolver a EL CLIENTE los títulos valores y/o documentos que contengan obligaciones crediticias que fueron objeto FACTORING SIN RECURSO, cuando EL CLIENTE los reemplace por existir sobre ellos disputa comercial o discusión, caso en el cual procederá a realizar la nota de endoso o cesión, según sea el caso, sin asumir responsabilidad cambiaria de ninguna naturaleza.<br><br>" +
                                    "<b>3.6.</b> En el evento que el deudor del título valor y o documento de contendido crediticio objeto de una operación de FACTORING SIN RECURSO cancele el valor del mismo a EL CLIENTE, éste deberá informar del hecho al BANCO de inmediato, y transferirle la totalidad de lo recibido dentro de los dos (2) días hábiles siguientes a la fecha en que hubiere recibido el pago, asumiendo todos los costos, cargas y gravámenes de la transferencia. En caso de mora, se causarán intereses moratorios a cargo de EL CLIENTE y a favor del BANCO, conforme a lo dispuesto en la cláusula décimo segunda de este reglamento.<br><br>" +
                                    "<b>3.7.</b> Las operaciones de FACTORING SIN RECURSO se harán con base en las condiciones financieras relativas a plazos, tasas de interés y tasas de descuento previamente acordadas entre EL BANCO y EL CLIENTE. Las condiciones financieras podrán: (a) Ser acordadas en documento separado y suscrito por las partes, el cual hará parte integral del presente reglamento; y/o, (b) Constar en comunicaciones cruzadas entre las partes, a través de fax, cartas o correos electrónicos en la que conste la aceptación expresa del CLIENTE; documentos que harán parte integral del presente reglamento. A pesar de lo previsto en la regla anterior, EL BANCO podrá establecer unilateralmente, en forma temporal o indefinida, nuevas condiciones financieras más favorables para EL CLIENTE, sin que pierdan vigencia las condiciones inicialmente pactadas, las cuales podrán aplicarse nuevamente en cualquier momento, aun por decisión unilateral del BANCO.<br><br>" +
                                    "<b>3.8.</b> Si EL DEUDOR del titulo valor factorizado no efectúa el pago total y oportuno de las obligaciones a su cargo, en la fecha límite de pago: (i) Se devengarán intereses de mora a la tasa de interés moratorio permitida por la ley, (ii) EL BANCO podrá suspender el descuento de más títulos valores; y, (iii) si la mora persiste por más de 30 días calendario, EL BANCO podrá dar por terminados los servicios de operaciones de FACTORING SIN RECURSO ejecutados bajo el presente reglamento, sin perjuicio de las acciones legales pertinentes.<br><br>" +
                                    "<b>3.9.</b> EL BANCO, el deudor del título valor o documento de contenido crediticio factorizado y EL CLIENTE podrán establecer, cuando lo estimen pertinente, los procedimientos específicos que habrán de observarse para celebrar cualquiera de las operaciones de FACTORING SIN RECURSO, tanto para la presentación de los títulos valores factorizados, como para el pago de los mismos. Estos procedimientos harán parte integral del presente reglamento.<br>" +
                                    "<b>3.10.</b> EL CLIENTE tendrá la posibilidad de utilizar los medios y canales electrónicos que EL BANCO disponga para celebrar operaciones de FACTORING SIN RECURSO previstas en este reglamento. Para el efecto, EL CLIENTE suscribirá con EL BANCO en documento anexo, las condiciones de operación y procedimientos que se aplicaran a estas operaciones, documento que hará parte integral del presente reglamento.<br><br>" +
                                    "<b>3.11.</b> EL BANCO podrá declarar de plazo vencido todas o algunas de las obligaciones anotadas en la cuenta de EL CLIENTE y exigir el pago de todas ellas más los intereses causados, accesorios, costas, gastos y honorarios de cobranzas judicial o extrajudicial si a ello hubiere lugar, en los siguientes casos: (i) mora en el pago de cualquiera de las obligaciones a cargo del deudor de los títulos valores factorizados; (ii) embargo de uno o más bienes pertenecientes al deudor de los títulos valores factorizados, que afecten materialmente y/o deterioren la situación financiera de cualquiera de ellos, que permita a la entidad temer el incumplimiento total o parcial de las obligaciones contraídas a su favor; (iii) haberse solicitado al deudor de los títulos valores factorizados sea admitido en un trámite de reorganización empresarial, liquidación voluntaria o judicial o un proceso concursal equivalente o semejante a éstos; (iv) Por haberse demeritado las garantías reales o personales constituidas para asegurar el cumplimiento de las obligaciones; (v) cuando el deudor de los títulos valores factorizados llegare a ser vinculado por parte de las autoridades competentes a cualquier tipo de investigación por delitos de narcotráfico, terrorismo, secuestro, lavado de activos, financiación de terrorismo y administración de recursos relacionados con actividades terroristas u otros delitos relacionados con el lavado de activos y financiación de terrorismo y/o sean incluidos en listas para el control de Lavado de Activos y financiación del terrorismo, administradas por cualquier autoridad nacional o extranjera, tales como la Oficina de Control de Lavado de Activos en el Exterior – OFAC, emitida por la Oficina del Tesoro de los Estados Unidos de Norteamérica, la Lista de la Organización de las Naciones Unidas y otras listas públicas relacionadas con el tema de lavado de activos y financiación del terrorismo y/o condenados por parte de las autoridades competentes en cualquier tipo de proceso judicial relacionado con la comisión de los anteriores delitos. (vi) la terminación de la relación contractual por alguna de las causas contempladas en el numeral 1.14 de este reglamento.<br><br>" +
                                    "<b>3.12.</b> EL CLIENTE se compromete a entregar  información completa, veraz y verificable del deudor de los títulos valores o documentos de contendido crediticio factorizados, sus pagadores, procedimientos de pago, fechas establecidas para el pago de los títulos valores  y/o documentos de contenido crediticio factorizados. EL BANCO queda facultado para  suministrar pagadores de las entidades deudoras de los títulos valores factorizados,  o a las personas señaladas por EL CLIENTE y/o a las Compañías de Seguros de crédito, información sobre el estado de las obligaciones derivadas del descuento de los títulos valores y o documentos de contenido crediticio factorizados, de la relación comercial y el valor máximo que tiene disponible EL CLIENTE aprobado en EL BANCO para operaciones de FACTORING SIN RECURSO.<br><br>" +
                                    "<b>3.13.</b> EL CLIENTE se compromete a atender las solicitudes de información que requiera el BANCO  para el adecuado desarrollo y ejecución de las operaciones de FACTORING SIN RECURSO en desarrollo del contrato de Factoring regulado en éste documento y a prestar tota la colaboración necesaria para obtener el pago oportuno de los títulos valores factorizados.<br><br>" +
                                    "<b>3.14.</b> EL CLIENTE declara que los títulos valores factorizados en desarrollo del contrato de Factoring, se encuentran libres de embargos y gravámenes, de deducciones de cualquier índole y no han sido transferidos con anterioridad mediante endoso y/o cesión. Así mismo, EL CLIENTE se compromete a no endosarlos y/o cederlos ni transferirlos a otra persona.<br><br>" +
                                    "<b>3.15.</b> EL CLIENTE se compromete  a prestar tota la colaboración y a adelantar todas las gestiones necesarias para obtener el pago al Banco de los títulos valores factorizados en desarrollo del contrato de Factoring que se regula por el presente reglamento, sus prórrogas, renovaciones y/o modificaciones, y a presentar los soportes que se requieran para que el deudor de los títulos valores y/o documentos de contenido crediticio realice los pagos correspondientes y, en general, a cumplir todos los requisitos contractuales y administrativos para el pago de las cuentas y/o facturas y/o títulos valores factorizados.<br><br>" +
                                    "<b>3.16.</b> EL BANCO tendrá la facultad de transferir total o parcialmente los créditos que surjan a cargo de EL CLIENTE en desarrollo y ejecución del contrato de FACTORING SIN RECURSO regulado en el presente reglamento. La persona natural o Jurídica que adquiera los créditos se encuentran facultados para diligenciar el pagaré o pagarés previstos en el numeral 2.15  y exigir su cobro judicial o extrajudicialmente. Para tal efecto, EL CLIENTE autoriza al BANCO para que suministre a los adquirentes de los créditos a los que hace referencia el presente numeral, copia del presente reglamento, del formulario de vinculación vigente suscrito por el CLIENTE, soportes de cada una de las operaciones de FACTORING SIN RECURSO, facturas, títulos valores y anotaciones contables.<br><br>" +
                                    "<b>3.17.</b> En cumplimiento de las normas y procedimientos relacionados con el Sistema de Administración del Riesgo de Lavado de Activos y de Financiación del Terrorismo (SARLAFT), EL CLIENTE garantiza que los títulos valores provienen de negocios celebrados y ejecutados válidamente y de buena fe, de la venta de bienes y/o prestación de servicios en desarrollo de su actividad comercial y de actividades lícitas, que no son producto de actividades relacionadas con lavado de activos y/o financiación del terrorismo.<br><br>" +
                                    "<b>3.18.</b> Este reglamento es de duración indefinida. Sin embargo, cualquiera de las partes podrá darlo por terminado mediante aviso dado a la otra, por escrito con la antelación determinada en las leyes vigentes para el momento en que decida hacer afectiva la terminación. La terminación ocurrirá desde el momento de la recepción de la comunicación y en consecuencia no se factorizarán más títulos valores. No obstante lo anterior, los títulos valores que se encuentren factorizados hasta la fecha de la terminación y pendientes de pago por sus deudores seguirán sometidas al presente reglamento y a los términos y condiciones especiales acorados para cada uno de ellos.<br><br>" +
                                    "<b>3.19.</b> Todo impuesto, carga, gravamen o retención de cualquier naturaleza que eventualmente llegue a causarse por razón de cualquiera de las operaciones de FACTORING SIN RECURSO, estará a cargo de EL CLIENTE.<br><br>" +
                                    "<b>3.20.</b> EL BANCO no garantiza, ni avala al CLIENTE en sus obligaciones contractuales, no atenderá ni tramitará glosas o reclamos derivados de los títulos valores endosados, ni asumirá ningún tipo de penas o multas por el incumplimiento de los mismos, ni asume riesgos por demora en los pagos que deba hacer el deudor de los documentos factorizados. El BANCO no está obligado a posponer los vencimientos, ni a reestructurar las obligaciones contenidos en los títulos valores factorizados.<br><br><br>" +
                                    "Para constancia de lo anterior, se firma en dos (2) ejemplares del mismo tenor en la ciudad de Bogotá,  a los _________ (" + DateTime.Now.Day + ") días del mes de " + DateTime.Now.Month + " de dos mil ___ (" + DateTime.Now.Year + ").<br><br>" +
                                    "<b>EL CLIENTE:</b><br><br><br><br>" +
                                    "_________________________<br>" +
                                    "<b>" + reglamento.razon_cliente + "</b><br>" +
                                    "<b>NIT " + reglamento.rut_cliente + "</b><br>" +
                                    "<b>C.C. No.</b> ____________ <br>" +
                                    "<b>Representante Legal</b>" +
                                "</div>";

                String hoja2 = "<div style='text-align: justify; font-family: Bookman Old Style; font-size:8;line-height: 1em;'>" +
                                    "<b>EL BANCO: </b><br><br><br><br>" +
                                    "_________________________<br>" +
                                    "<b>BANCO BILBAO VIZCAYA ARGENTARIA COLOMBIA S.A. – BBVA COLOMBIA</b><br>" +
                                    "<b>NIT 860.003.020-1</b>br>" +
                                "</div>";

                String pagina1 = titulo + hoja1;
                
                List<IElement> htmlarraylist0 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(pagina1), null);
                Paragraph parrafo_hoja1 = new Paragraph("");
                parrafo_hoja1.InsertRange(0, htmlarraylist0);

                List<IElement> htmlarraylist1 = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(hoja2), null);
                Paragraph parrafo_hoja2 = new Paragraph("");
                parrafo_hoja2.InsertRange(0, htmlarraylist1);
                                                                
                #endregion

                document.Add(parrafo_hoja1);
                document.NewPage();
                document.Add(parrafo_hoja2);
                                
                document.Close();

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------//

            }
            catch (Exception e)
            {
                document.Close();
            }

        }

        private void WriteToPdf(FileInfo sourceFile, string stringToWriteToPdf)
         {
            PdfReader reader = new PdfReader(sourceFile.FullName);
 
             using (MemoryStream memoryStream = new MemoryStream())
            {
               //
                 // PDFStamper is the class we use from iTextSharp to alter an existing PDF.
                 //
                 PdfStamper pdfStamper = new PdfStamper(reader, memoryStream);
 
                 for (int i = 1; i <= reader.NumberOfPages; i++) // Must start at 1 because 0 is not an actual page.
                 {
                     //
                     // If you ask for the page size with the method getPageSize(), you always get a
                    // Rectangle object without rotation (rot. 0 degrees)—in other words, the paper size
                     // without orientation. That’s fine if that’s what you’re expecting; but if you reuse
                     // the page, you need to know its orientation. You can ask for it separately with
                     // getPageRotation(), or you can use getPageSizeWithRotation(). - (Manning Java iText Book)
                     //   
                     //
                     Rectangle pageSize = reader.GetPageSizeWithRotation(i);
 
                     //
                     // Gets the content ABOVE the PDF, Another option is GetUnderContent(...)  
                     // which will place the text below the PDF content. 
                     //
                     PdfContentByte pdfPageContents = pdfStamper.GetUnderContent(i);
                     pdfPageContents.BeginText(); // Start working with text.
 
                  //
                     // Create a font to work with 
                     //
                     BaseFont baseFont = BaseFont.CreateFont(FontFactory.COURIER, Encoding.ASCII.EncodingName, false);
                     pdfPageContents.SetFontAndSize(baseFont, 40); // 40 point font
                     //pdfPageContents.SetRGBColorFill(255, 0, 0); // Sets the color of the font, RED in this instance
                     pdfPageContents.SetRGBColorFill(0, 0, 255); // Sets the color of the font, RED in this instance

                     //
                     // Angle of the text. This will give us the angle so we can angle the text diagonally 
                     // from the bottom left corner to the top right corner through the use of simple trigonometry. 
                     //
                     float textAngle =
                         (float) GetHypotenuseAngleInDegreesFrom(pageSize.Height, pageSize.Width);
 
                     //
                     // Note: The x,y of the Pdf Matrix is from bottom left corner. 
                     // This command tells iTextSharp to write the text at a certain location with a certain angle.
                    // Again, this will angle the text from bottom left corner to top right corner and it will 
                     // place the text in the middle of the page. 
                    //
                    pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, stringToWriteToPdf,
                                                    pageSize.Width/2,
                                                    pageSize.Height/2,
                                                    textAngle);
 
                     pdfPageContents.EndText(); // Done working with text
                 }
                 
                 pdfStamper.FormFlattening = true; // enable this if you want the PDF flattened. 
                 pdfStamper.Close(); // Always close the stamper or you'll have a 0 byte stream. 

                 byte[] Bin = memoryStream.ToArray();

                 //System.IO.MemoryStream rptStream = new System.IO.MemoryStream(Bin);

                 FileStream oFileStream;
                 
                 if (File.Exists(sourceFile.FullName)) 
                     File.Delete(sourceFile.FullName);

                 oFileStream = new FileStream(sourceFile.FullName, FileMode.CreateNew);
                 oFileStream.Write(Bin, 0, Bin.Length);
                 oFileStream.Close();
                 oFileStream = null;

                 //AR1.LoadFile(pathTemporal);
                 //If File.Exists(pathTemporal) Then File.Delete(pathTemporal)

                 
                 //return memoryStream.ToArray();
             }
        }

        public static double GetHypotenuseAngleInDegreesFrom(double opposite, double adjacent)
         {
 
             double radians = Math.Atan2(opposite, adjacent); // Get Radians for Atan2
             double angle = radians*(180/Math.PI); // Change back to degrees
             return angle;
         }
            
    }

}

