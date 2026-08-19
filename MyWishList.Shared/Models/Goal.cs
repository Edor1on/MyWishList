using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWishList.Shared.Enums;

namespace MyWishList.Shared.Models
{
    public class Goal
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public required string Name { get; set; }
        public string? Description { get; set; }

        public GoalStatus Status { get; set; } = GoalStatus.NotStarted;
        public double ProgressPercentage { get; set; } = 0;

        public string? Impression { get; set; }

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
