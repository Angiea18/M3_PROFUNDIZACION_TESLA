using System;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data;
public class NikolaContext :DbContext
{
     public NikolaContext(DbContextOptions<NikolaContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Album> Albums {get;set;}
    public DbSet<Artist> Artists{get;set;}
    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if(builder == null)
        {
            return;
        }

        builder.Entity<Album>().ToTable("Album").HasKey(k => k.Id);
        builder.Entity<Artist>().ToTable("Artist").HasKey(k => k.Id);
         builder.Entity<Song>().ToTable("songs").HasKey(k => k.Id);

    }
}