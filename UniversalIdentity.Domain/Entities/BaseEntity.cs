using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversalIdentity.Domain.Entities
{
    /// <summary>
    /// Base para entidades
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador único
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
