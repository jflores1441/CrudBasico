using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //Patron desarrollado que permite que el modelo nos de mayor informacion, todo lo contenido aqui se convierte en JSON y se le imprime al cliente
    //Y poder accesar y manipular  todas estas propiedades desde Javascript y cuando usamos AJAX

    public class ResponseModel
    {
        public dynamic result { get; set; }
        public bool response { get; set; }
        public string message { get; set; }
        public string href { get; set; }
        public string function { get; set; }

        public ResponseModel()
        {
            this.response = false; //Por defecto dejamos que el Response Model tenga el valor de que ocurrio un error    
            this.message = "Ocurrio un error inesperado en el constructor";
        }

        public void SetResponse(bool r, string m = "")
        {
            this.response = r;
            this.message = m;

            if (!r && m == "")
                this.message = "Ocurrio un error inesperado en el SetResponse";
        }
    }
}
