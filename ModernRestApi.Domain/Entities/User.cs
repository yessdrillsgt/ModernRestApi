using ModernRestApi.Domain.Common;

namespace ModernRestApi.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
