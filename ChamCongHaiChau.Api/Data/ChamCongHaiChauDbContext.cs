// ChamCongHaiChau.Api/Data/ChamCongHaiChauDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChamCongHaiChau.Shared.Models;

namespace ChamCongHaiChau.Api.Data;

public class ChamCongHaiChauDbContext : IdentityDbContext<ApplicationUser>
{
    public ChamCongHaiChauDbContext(DbContextOptions<ChamCongHaiChauDbContext> options) : base(options)
    {
    }

    // DbSet<...> ở đây khi cần thêm
}
