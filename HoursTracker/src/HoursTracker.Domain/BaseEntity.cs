using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoursTracker.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool Disabled { get; set; }
    }
}