using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.EFCore;

public abstract class EntityBase
{
    public int Id { get; set; }
}
