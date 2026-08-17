using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWishList.Shared.Enums;

namespace MyWishList.Shared.Models
{
    public class Wish
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public int UserId { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }

        public WishStatus Status { get; set; } = WishStatus.NotStarted;

        public string? Impression { get; set; }

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
