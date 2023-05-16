using Microsoft.EntityFrameworkCore;
using Api.Model;

namespace Api;

public partial class BrowserGameContext : DbContext
{
    public BrowserGameContext()
    {
    }

    public BrowserGameContext(DbContextOptions<BrowserGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Biome> Biomes { get; set; }

    public virtual DbSet<Plot> Plots { get; set; }

    public virtual DbSet<PrismaMigration> PrismaMigrations { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileServerDatum> ProfileServerData { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Settlement> Settlements { get; set; }

    public virtual DbSet<SettlementStructure> SettlementStructures { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<Tile> Tiles { get; set; }

    public virtual DbSet<World> Worlds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("AccountRole", new[] { "MEMBER", "SUPPORT", "ADMINISTRATOR" })
            .HasPostgresEnum("ServerStatus", new[] { "OFFLINE", "MAINTENANCE", "ONLINE" })
            .HasPostgresEnum("TileType", new[] { "OCEAN", "LAND" });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "Account_email_key").IsUnique();

            entity.HasIndex(e => e.UserAuthToken, "Account_userAuthToken_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserAuthToken).HasColumnName("userAuthToken");
        });

        modelBuilder.Entity<Biome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Biome_pkey");

            entity.ToTable("Biome");

            entity.HasIndex(e => e.Name, "Biome_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FoodModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("foodModifier");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OreModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("oreModifier");
            entity.Property(e => e.PlotAreaMax)
                .HasDefaultValueSql("50")
                .HasColumnName("plotAreaMax");
            entity.Property(e => e.PlotAreaMin)
                .HasDefaultValueSql("30")
                .HasColumnName("plotAreaMin");
            entity.Property(e => e.PlotsMax)
                .HasDefaultValueSql("10")
                .HasColumnName("plotsMax");
            entity.Property(e => e.PlotsMin)
                .HasDefaultValueSql("1")
                .HasColumnName("plotsMin");
            entity.Property(e => e.PrecipitationMax).HasColumnName("precipitationMax");
            entity.Property(e => e.PrecipitationMin).HasColumnName("precipitationMin");
            entity.Property(e => e.SolarModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("solarModifier");
            entity.Property(e => e.StoneModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("stoneModifier");
            entity.Property(e => e.TemperatureMax).HasColumnName("temperatureMax");
            entity.Property(e => e.TemperatureMin).HasColumnName("temperatureMin");
            entity.Property(e => e.WaterModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("waterModifier");
            entity.Property(e => e.WindModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("windModifier");
            entity.Property(e => e.WoodModifier)
                .HasDefaultValueSql("1")
                .HasColumnName("woodModifier");
        });

        modelBuilder.Entity<Plot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Plot_pkey");

            entity.ToTable("Plot");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Area)
                .HasDefaultValueSql("30")
                .HasColumnName("area");
            entity.Property(e => e.Food)
                .HasDefaultValueSql("1")
                .HasColumnName("food");
            entity.Property(e => e.Ore)
                .HasDefaultValueSql("1")
                .HasColumnName("ore");
            entity.Property(e => e.Solar)
                .HasDefaultValueSql("1")
                .HasColumnName("solar");
            entity.Property(e => e.Stone)
                .HasDefaultValueSql("1")
                .HasColumnName("stone");
            entity.Property(e => e.TileId).HasColumnName("tileId");
            entity.Property(e => e.Water)
                .HasDefaultValueSql("1")
                .HasColumnName("water");
            entity.Property(e => e.Wind)
                .HasDefaultValueSql("1")
                .HasColumnName("wind");
            entity.Property(e => e.Wood)
                .HasDefaultValueSql("1")
                .HasColumnName("wood");

            entity.HasOne(d => d.Tile).WithMany(p => p.Plots)
                .HasForeignKey(d => d.TileId)
                .HasConstraintName("Plot_tileId_fkey");
        });

        modelBuilder.Entity<PrismaMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("_prisma_migrations_pkey");

            entity.ToTable("_prisma_migrations");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.AppliedStepsCount).HasColumnName("applied_steps_count");
            entity.Property(e => e.Checksum)
                .HasMaxLength(64)
                .HasColumnName("checksum");
            entity.Property(e => e.FinishedAt).HasColumnName("finished_at");
            entity.Property(e => e.Logs).HasColumnName("logs");
            entity.Property(e => e.MigrationName)
                .HasMaxLength(255)
                .HasColumnName("migration_name");
            entity.Property(e => e.RolledBackAt).HasColumnName("rolled_back_at");
            entity.Property(e => e.StartedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("started_at");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Profile_pkey");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.AccountId, "Profile_accountId_key").IsUnique();

            entity.HasIndex(e => e.Username, "Profile_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Account).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.AccountId)
                .HasConstraintName("Profile_accountId_fkey");
        });

        modelBuilder.Entity<ProfileServerDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProfileServerData_pkey");

            entity.HasIndex(e => e.ProfileId, "ProfileServerData_profileId_key").IsUnique();

            entity.HasIndex(e => new { e.ProfileId, e.ServerId }, "ProfileServerData_profileId_serverId_key").IsUnique();

            entity.HasIndex(e => e.ServerId, "ProfileServerData_serverId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProfileId).HasColumnName("profileId");
            entity.Property(e => e.ServerId).HasColumnName("serverId");

            entity.HasOne(d => d.Profile).WithOne(p => p.ProfileServerDatum)
                .HasForeignKey<ProfileServerDatum>(d => d.ProfileId)
                .HasConstraintName("ProfileServerData_profileId_fkey");

            entity.HasOne(d => d.Server).WithOne(p => p.ProfileServerDatum)
                .HasForeignKey<ProfileServerDatum>(d => d.ServerId)
                .HasConstraintName("ProfileServerData_serverId_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Region_pkey");

            entity.ToTable("Region");

            entity.HasIndex(e => new { e.Name, e.WorldId }, "Region_name_worldId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ElevationMap)
                .HasColumnType("jsonb")
                .HasColumnName("elevationMap");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PrecipitationMap)
                .HasColumnType("jsonb")
                .HasColumnName("precipitationMap");
            entity.Property(e => e.TemperatureMap)
                .HasColumnType("jsonb")
                .HasColumnName("temperatureMap");
            entity.Property(e => e.WorldId).HasColumnName("worldId");
            entity.Property(e => e.XCoord)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("xCoord");
            entity.Property(e => e.YCoord)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("yCoord");

            entity.HasOne(d => d.World).WithMany(p => p.Regions)
                .HasForeignKey(d => d.WorldId)
                .HasConstraintName("Region_worldId_fkey");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Server_pkey");

            entity.ToTable("Server");

            entity.HasIndex(e => new { e.Hostname, e.Port }, "Server_hostname_port_key").IsUnique();

            entity.HasIndex(e => e.Name, "Server_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Hostname)
                .HasDefaultValueSql("'localhost'::text")
                .HasColumnName("hostname");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Port)
                .HasDefaultValueSql("5000")
                .HasColumnName("port");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Settlement_pkey");

            entity.ToTable("Settlement");

            entity.HasIndex(e => e.PlotId, "Settlement_plotId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("'Home Settlement'::text")
                .HasColumnName("name");
            entity.Property(e => e.PlayerProfileId).HasColumnName("playerProfileId");
            entity.Property(e => e.PlotId).HasColumnName("plotId");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.PlayerProfile).WithMany(p => p.Settlements)
                .HasPrincipalKey(p => p.ProfileId)
                .HasForeignKey(d => d.PlayerProfileId)
                .HasConstraintName("Settlement_playerProfileId_fkey");

            entity.HasOne(d => d.Plot).WithOne(p => p.Settlement)
                .HasForeignKey<Settlement>(d => d.PlotId)
                .HasConstraintName("Settlement_plotId_fkey");
        });

        modelBuilder.Entity<SettlementStructure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SettlementStructure_pkey");

            entity.ToTable("SettlementStructure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.SettlementId).HasColumnName("settlementId");
            entity.Property(e => e.StructureId).HasColumnName("structureId");

            entity.HasOne(d => d.Settlement).WithMany(p => p.SettlementStructures)
                .HasForeignKey(d => d.SettlementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("SettlementStructure_settlementId_fkey");

            entity.HasOne(d => d.Structure).WithMany(p => p.SettlementStructures)
                .HasForeignKey(d => d.StructureId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("SettlementStructure_structureId_fkey");
        });

        modelBuilder.Entity<Structure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Structure_pkey");

            entity.ToTable("Structure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Tile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tile_pkey");

            entity.ToTable("Tile");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BiomeId).HasColumnName("biomeId");
            entity.Property(e => e.Elevation).HasColumnName("elevation");
            entity.Property(e => e.Precipitation).HasColumnName("precipitation");
            entity.Property(e => e.RegionId).HasColumnName("regionId");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

            entity.HasOne(d => d.Biome).WithMany(p => p.Tiles)
                .HasForeignKey(d => d.BiomeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Tile_biomeId_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Tiles)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("Tile_regionId_fkey");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("World_pkey");

            entity.ToTable("World");

            entity.HasIndex(e => new { e.Name, e.ServerId }, "World_name_serverId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ElevationSettings)
                .HasColumnType("jsonb")
                .HasColumnName("elevationSettings");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PrecipitationSettings)
                .HasColumnType("jsonb")
                .HasColumnName("precipitationSettings");
            entity.Property(e => e.ServerId).HasColumnName("serverId");
            entity.Property(e => e.TemperatureSettings)
                .HasColumnType("jsonb")
                .HasColumnName("temperatureSettings");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Server).WithMany(p => p.Worlds)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("World_serverId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
