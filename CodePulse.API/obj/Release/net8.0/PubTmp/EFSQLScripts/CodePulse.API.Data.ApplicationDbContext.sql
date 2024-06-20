IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614105328_InitialMigration'
)
BEGIN
    CREATE TABLE [BlogPosts] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [ShortDescription] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [FeaturedImageUrl] nvarchar(max) NOT NULL,
        [UrlHandle] nvarchar(max) NOT NULL,
        [PublishedDate] datetime2 NOT NULL,
        [Author] nvarchar(max) NOT NULL,
        [IsVisible] bit NOT NULL,
        CONSTRAINT [PK_BlogPosts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614105328_InitialMigration'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [UrlHandle] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614105328_InitialMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614105328_InitialMigration', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616053552_Add relationships between tables'
)
BEGIN
    CREATE TABLE [BlogPostCategory] (
        [BlogPostsId] uniqueidentifier NOT NULL,
        [CategoriesId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BlogPostCategory] PRIMARY KEY ([BlogPostsId], [CategoriesId]),
        CONSTRAINT [FK_BlogPostCategory_BlogPosts_BlogPostsId] FOREIGN KEY ([BlogPostsId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_BlogPostCategory_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616053552_Add relationships between tables'
)
BEGIN
    CREATE INDEX [IX_BlogPostCategory_CategoriesId] ON [BlogPostCategory] ([CategoriesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616053552_Add relationships between tables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240616053552_Add relationships between tables', N'8.0.6');
END;
GO

COMMIT;
GO

