using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace dim.xml
{
    [XmlRootAttribute("DTE", Namespace = "http://www.sii.cl/SiiDte", IsNullable = false)]
    public class DTEDocument
    {
        #region Campos (nombre, apellidos)
        
        

        #endregion
        
        #region Propiedades (Nombre, Apellidos)

        

        #endregion
    
     #region Constructores

        public DTEDocument()
        {
            
        }

        public DTEDocument(IdDoc iddoc, Emisor emisor)
        {
            
        }

        #endregion
    }

    [XmlRootAttribute("IdDoc", Namespace = "http://www.sii.cl/SiiDte", IsNullable = false)]
    public class IdDoc
    {
        #region Campos
            private string tipo;
            private string folio;
            private string fecha;
        #endregion

        #region Propiedades
            [XmlElementAttribute("tipo")]
            public string Tipo
            {
                get { return tipo; }
                set { tipo = value; }
            }

            [XmlElementAttribute("folio")]
            public string Folio
            {
                get { return folio; }
                set { folio = value; }
            }

            [XmlElementAttribute("fecha")]
            public string Fecha
            {
                get { return fecha; }
                set { fecha = value; }
            }
        #endregion

        #region Constructores
            public IdDoc()
            {
                this.tipo = "";
                this.folio = "";
                this.fecha = "";
            }

            public IdDoc(string tipo, string folio, string fecha)
            {
                this.tipo = "";
                this.folio = "";
                this.fecha = "";
            }
        #endregion
    }

    [XmlRootAttribute("Emisor", Namespace = "http://www.sii.cl/SiiDte", IsNullable = false)]
    public class Emisor
    {
        private string rut;
        private string razon;
        private string giro;
        private string actividad;
        private string sucsii;
        private string direccion;
        private string comuna;
        private string ciudad; 

        [XmlElementAttribute("rut")]
        public string Rut
        {
            get { return rut; }
            set { rut = value; }
        }

        [XmlElementAttribute("razon")]
        public string Razon
        {
            get { return razon; }
            set { razon = value; }
        }

        [XmlElementAttribute("giro")]
        public string Giro
        {
            get { return giro; }
            set { giro = value; }
        }

        [XmlElementAttribute("actividad")]
        public string Actividad
        {
            get { return actividad; }
            set { actividad = value; }
        }

        [XmlElementAttribute("sucsii")]
        public string Sucsii
        {
            get { return sucsii; }
            set { sucsii = value; }
        }

        [XmlElementAttribute("direccion")]
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        [XmlElementAttribute("comuna")]
        public string Comuna
        {
            get { return comuna; }
            set { comuna = value; }
        }

        [XmlElementAttribute("ciudad")]
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }

        public Emisor()
        {
            this.rut = "";
            this.razon = "";
            this.giro = "";
            this.actividad = "";
            this.sucsii = "";
            this.direccion = "";
            this.comuna = "";
            this.ciudad = "";
        }

        public Emisor(string rut, string razon, string giro, string actividad, string sucsii, string direccion, string comuna, string ciudad)
        {
            this.rut = rut;
            this.razon = razon;
            this.giro = giro;
            this.actividad = actividad;
            this.sucsii = sucsii;
            this.direccion = direccion;
            this.comuna = comuna;
            this.ciudad = ciudad;
        }

    }

}
