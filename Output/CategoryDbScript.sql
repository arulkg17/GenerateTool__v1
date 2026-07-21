-- =============================================
-- Create Table : Category
-- =============================================

CREATE TABLE [dbo].[Category]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Code] NVARCHAR(50) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(500) NULL,
    [IsActive] BIT NOT NULL,
    [CreatedBy] NVARCHAR(500) NOT NULL,
    [CreatedDate] DATETIME NULL,
    [UpdatedBy] NVARCHAR(500) NOT NULL,
    [UpdatedDate] DATETIME NULL,

    CONSTRAINT PK_Category
        PRIMARY KEY (
            Id

        )
);

GO


-- =============================================
-- INSERT
-- =============================================

CREATE PROCEDURE [dbo].[sp_Category_Insert]

(
    @Code NVARCHAR(50),
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @IsActive BIT,
    @CreatedBy NVARCHAR(500),
    @CreatedDate DATETIME,
    @UpdatedBy NVARCHAR(500),
    @UpdatedDate DATETIME,

)

AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [dbo].[Category]
    (
        [Code],
        [Name],
        [Description],
        [IsActive],
        [CreatedBy],
        [CreatedDate],
        [UpdatedBy],
        [UpdatedDate],

    )

    VALUES
    (
        @Code,
        @Name,
        @Description,
        @IsActive,
        @CreatedBy,
        @CreatedDate,
        @UpdatedBy,
        @UpdatedDate,

    );

    SELECT SCOPE_IDENTITY() AS Id;

END

GO


-- =============================================
-- UPDATE
-- =============================================

CREATE PROCEDURE [dbo].[sp_Category_Update]

(
    @Id INT,
    @Code NVARCHAR(50),
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @IsActive BIT,
    @CreatedBy NVARCHAR(500),
    @CreatedDate DATETIME,
    @UpdatedBy NVARCHAR(500),
    @UpdatedDate DATETIME,

)

AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [dbo].[Category]

    SET

        [Code] = @Code,
        [Name] = @Name,
        [Description] = @Description,
        [IsActive] = @IsActive,
        [CreatedBy] = @CreatedBy,
        [CreatedDate] = @CreatedDate,
        [UpdatedBy] = @UpdatedBy,
        [UpdatedDate] = @UpdatedDate,


    WHERE Id=@Id;

    SELECT @@ROWCOUNT AS UpdatedRows;

END

GO


-- =============================================
-- DELETE
-- =============================================

CREATE PROCEDURE [dbo].[sp_Category_Delete]

(
    @Id INT
)

AS
BEGIN

    SET NOCOUNT ON;

    DELETE FROM [dbo].[Category]

    WHERE Id=@Id;

    SELECT @@ROWCOUNT AS DeletedRows;

END

GO


-- =============================================
-- GET BY ID
-- =============================================

CREATE PROCEDURE [dbo].[sp_Category_GetById]

(
    @Id INT
)

AS
BEGIN

    SET NOCOUNT ON;

    SELECT *

    FROM [dbo].[Category]

    WHERE Id=@Id;

END

GO


-- =============================================
-- GET ALL
-- =============================================

CREATE PROCEDURE [dbo].[sp_Category_GetAll]

AS
BEGIN

    SET NOCOUNT ON;

    SELECT *

    FROM [dbo].[Category]

    ORDER BY Id DESC;

END

GO


-- =============================================
-- GET ALL PAGED
-- =============================================

CREATE PROCEDURE [dbo].[sp_Category_GetAllPaged]

(
    @PageNumber INT = 1,
    @PageSize INT = 10
)

AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @Offset INT;

    SET @Offset = (@PageNumber-1) * @PageSize;

    SELECT *

    FROM [dbo].[Category]

    ORDER BY Id DESC

    OFFSET @Offset ROWS

    FETCH NEXT @PageSize ROWS ONLY;

    SELECT COUNT(*) AS TotalRecords

    FROM [dbo].[Category];

END

GO