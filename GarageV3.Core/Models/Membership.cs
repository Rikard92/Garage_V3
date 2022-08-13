using System.ComponentModel;

namespace GarageV3.Core.Models
{
    public class Membership
    {
        public int Id { get; set; }

        [DisplayName("Medlemskategori")]
        public string? MembershipCategory { get; set; }
        public Owner? Owner { get; set; }
        public int OwnerId { get; set; }
    }
}