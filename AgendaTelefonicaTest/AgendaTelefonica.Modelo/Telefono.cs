using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonica.Modelo
{
    public class Telefono
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTelefono { get; set; }
        public virtual Contacto Contacto { get; set; }
        public string Numero { get; set; }
        public string Extension { get; set; }
        public byte TipoTelefono { get; set; }
    }
}
