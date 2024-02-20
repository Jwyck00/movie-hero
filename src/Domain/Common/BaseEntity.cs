using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Interfaces;

namespace Domain.Common;

public abstract class BaseEntity<TKey> : Entity where TKey : IComparable
{
    public TKey Id { get; set; } = default!;
}
