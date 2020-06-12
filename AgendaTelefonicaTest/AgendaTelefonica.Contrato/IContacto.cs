using AgendaTelefonica.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonica.Contrato
{
    public interface IContacto
    {
        Task<int> AddContacto(Contacto contacto);
        Task UpdateContacto(Contacto contacto);
        Task DeleteContacto(Contacto contacto);

        Task<IEnumerable<Contacto>> GetContactos();
    }
}
