using AgendaTelefonica.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTelefonica.Repositorio
{
    public class ContactoContext:DbContext
    {
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }

        public ContactoContext(): base("ContactoContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>().HasMany(e => e.Telefonos).WithRequired(t => t.Contacto)
                .Map(t => t.MapKey("IdContacto"));
        }
    }
}
