namespace Movi.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cartoon",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 255),
                        AnotherName = c.String(maxLength: 255),
                        Area = c.String(maxLength: 32),
                        Cover = c.String(maxLength: 128),
                        LocalCover = c.String(maxLength: 128),
                        Mins = c.Int(nullable: false),
                        Language = c.String(maxLength: 32),
                        Director = c.String(maxLength: 32),
                        Year = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                        Summary = c.String(maxLength: 2048),
                        Rank = c.Double(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        OuterLink = c.String(maxLength: 128),
                        CategoriesSerialized = c.String(maxLength: 255),
                        ActorsSerialized = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartoonCaption",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cartoon", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.CartoonSource",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.String(nullable: false, maxLength: 128),
                        Size = c.String(maxLength: 32),
                        Address = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 255),
                        Quality = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cartoon", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 255),
                        AnotherName = c.String(maxLength: 255),
                        Area = c.String(maxLength: 32),
                        Cover = c.String(maxLength: 128),
                        LocalCover = c.String(maxLength: 128),
                        Mins = c.Int(nullable: false),
                        Language = c.String(maxLength: 32),
                        Director = c.String(maxLength: 32),
                        Year = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                        Summary = c.String(maxLength: 2048),
                        Rank = c.Double(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        OuterLink = c.String(maxLength: 128),
                        CategoriesSerialized = c.String(maxLength: 255),
                        ActorsSerialized = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCaption",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.MovieSource",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.String(nullable: false, maxLength: 128),
                        Size = c.String(maxLength: 32),
                        Address = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 255),
                        Quality = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Teleplay",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 255),
                        AnotherName = c.String(maxLength: 255),
                        Area = c.String(maxLength: 32),
                        Cover = c.String(maxLength: 128),
                        LocalCover = c.String(maxLength: 128),
                        Mins = c.Int(nullable: false),
                        Language = c.String(maxLength: 32),
                        Director = c.String(maxLength: 32),
                        Year = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                        Summary = c.String(maxLength: 2048),
                        Rank = c.Double(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        OuterLink = c.String(maxLength: 128),
                        CategoriesSerialized = c.String(maxLength: 255),
                        ActorsSerialized = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeleplayCaption",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teleplay", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.TeleplaySource",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.String(nullable: false, maxLength: 128),
                        Size = c.String(maxLength: 32),
                        Address = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 255),
                        Quality = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teleplay", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeleplaySource", "EntityId", "dbo.Teleplay");
            DropForeignKey("dbo.TeleplayCaption", "EntityId", "dbo.Teleplay");
            DropForeignKey("dbo.MovieSource", "EntityId", "dbo.Movie");
            DropForeignKey("dbo.MovieCaption", "EntityId", "dbo.Movie");
            DropForeignKey("dbo.CartoonSource", "EntityId", "dbo.Cartoon");
            DropForeignKey("dbo.CartoonCaption", "EntityId", "dbo.Cartoon");
            DropIndex("dbo.TeleplaySource", new[] { "EntityId" });
            DropIndex("dbo.TeleplayCaption", new[] { "EntityId" });
            DropIndex("dbo.MovieSource", new[] { "EntityId" });
            DropIndex("dbo.MovieCaption", new[] { "EntityId" });
            DropIndex("dbo.CartoonSource", new[] { "EntityId" });
            DropIndex("dbo.CartoonCaption", new[] { "EntityId" });
            DropTable("dbo.TeleplaySource");
            DropTable("dbo.TeleplayCaption");
            DropTable("dbo.Teleplay");
            DropTable("dbo.MovieSource");
            DropTable("dbo.MovieCaption");
            DropTable("dbo.Movie");
            DropTable("dbo.CartoonSource");
            DropTable("dbo.CartoonCaption");
            DropTable("dbo.Cartoon");
        }
    }
}
