using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Personas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public long Id { get; set; }

        public string Nombre { get; set; } = "";

        public string Documento { get; set; } = "";

        // Propiedad de navegación para relacionar con Vehiculos
        public ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
