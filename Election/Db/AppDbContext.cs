using Election.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Election.Db;

public class AppDbContext:IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
    {
    }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Ballot> Ballot { get; set; }
    public DbSet<ElectionCandidate> ElectionCandidates { get; set; }
    public DbSet<Vote> Vote { get; set; }
    public DbSet<ElectionModel> Elections { get;  set; }
}
