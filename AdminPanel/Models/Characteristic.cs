using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models
{
    public class Characteristic
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CharacteristicsNameId { get; set; }
        public CharacteristicName CharacteristicsName { get; set; } = null!;
        
        public int CharacteristicsValueId { get; set; }
        public CharacteristicValue CharacteristicsValue { get; set; } = null!;
    }
}
