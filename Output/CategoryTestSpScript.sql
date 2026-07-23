-- =============================================
-- TEST SCRIPT FOR Category
-- =============================================

PRINT '=============================================';
PRINT 'Testing Category Stored Procedures';
PRINT '=============================================';

GO
-- =============================================
-- INSERT TEST DATA
-- =============================================

PRINT 'Insert Test Records';


PRINT 'Insert Record 1';

EXEC [dbo].[sp_Category_Insert]

    @Code = 'Test Code 1',
    @Name = 'Test Name 1',
    @Description = 'Test Description 1',
    @IsActive = 1

GO


PRINT 'Insert Record 2';

EXEC [dbo].[sp_Category_Insert]

    @Code = 'Test Code 2',
    @Name = 'Test Name 2',
    @Description = 'Test Description 2',
    @IsActive = 1

GO


PRINT 'Insert Record 3';

EXEC [dbo].[sp_Category_Insert]

    @Code = 'Test Code 3',
    @Name = 'Test Name 3',
    @Description = 'Test Description 3',
    @IsActive = 1

GO


PRINT 'Insert Record 4';

EXEC [dbo].[sp_Category_Insert]

    @Code = 'Test Code 4',
    @Name = 'Test Name 4',
    @Description = 'Test Description 4',
    @IsActive = 1

GO


PRINT 'Insert Record 5';

EXEC [dbo].[sp_Category_Insert]

    @Code = 'Test Code 5',
    @Name = 'Test Name 5',
    @Description = 'Test Description 5',
    @IsActive = 1

GO



-- =============================================
-- GET ALL
-- =============================================

PRINT 'Get All';

EXEC [dbo].[sp_Category_GetAll];

GO


-- =============================================
-- GET BY ID
-- =============================================

PRINT 'Get By Id';

EXEC [dbo].[sp_Category_GetById]
    @Id = 1;

GO


-- =============================================
-- UPDATE
-- =============================================

PRINT 'Update Record';

EXEC [dbo].[sp_Category_Update]

@Id = 1,
@Code = 'Test Code',
@Name = 'Test Name',
@Description = 'Test Description',
@IsActive = 1

GO


SELECT *
FROM [dbo].[Category];

GO


-- =============================================
-- GET ALL PAGED
-- =============================================

PRINT 'Get All Paged';

EXEC [dbo].[sp_Category_GetAllPaged]

    @Code = NULL,
    @Name = NULL,
    @Description = NULL,

    @PageNumber = 1,
    @PageSize = 10;

GO


-- =============================================
-- DELETE
-- =============================================

PRINT 'Delete Record';

EXEC [dbo].[sp_Category_Delete]
    @Id = 1;

GO


SELECT *
FROM [dbo].[Category];

GO


PRINT '=============================================';
PRINT 'Category Stored Procedure Testing Completed';
PRINT '=============================================';

GO