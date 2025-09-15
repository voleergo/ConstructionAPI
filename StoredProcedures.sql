-- ====================================================================================
-- STORED PROCEDURES FOR CONSTRUCTION API
-- All repository methods have been converted to use stored procedures
-- ====================================================================================

-- ====================================================================================
-- CUSTOMER STORED PROCEDURES
-- ====================================================================================

-- Get All Customers
CREATE PROCEDURE usp_Customer_GetAll
AS
BEGIN
    SELECT ID_Customer, CustomerCode, CustomerName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Customers 
    ORDER BY CustomerName;
END
GO

-- Get Customer By ID
CREATE PROCEDURE usp_Customer_GetById
    @ID_Customer BIGINT
AS
BEGIN
    SELECT ID_Customer, CustomerCode, CustomerName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Customers 
    WHERE ID_Customer = @ID_Customer;
END
GO

-- Insert Customer
CREATE PROCEDURE usp_Customer_Insert
    @CustomerCode VARCHAR(30),
    @CustomerName VARCHAR(100),
    @CreatedOn DATETIME,
    @CreatedBy BIGINT = NULL,
    @ModifiedOn DATETIME = NULL,
    @ModifiedBy BIGINT = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO Customers (CustomerCode, CustomerName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
    VALUES (@CustomerCode, @CustomerName, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Customer
CREATE PROCEDURE usp_Customer_Update
    @ID_Customer BIGINT,
    @CustomerCode VARCHAR(30),
    @CustomerName VARCHAR(100),
    @ModifiedOn DATETIME,
    @ModifiedBy BIGINT = NULL
AS
BEGIN
    UPDATE Customers 
    SET CustomerCode = @CustomerCode, 
        CustomerName = @CustomerName, 
        ModifiedOn = @ModifiedOn, 
        ModifiedBy = @ModifiedBy
    WHERE ID_Customer = @ID_Customer;
END
GO

-- Delete Customer
CREATE PROCEDURE usp_Customer_Delete
    @ID_Customer BIGINT
AS
BEGIN
    DELETE FROM Customers WHERE ID_Customer = @ID_Customer;
END
GO

-- Get Customer By Code
CREATE PROCEDURE usp_Customer_GetByCode
    @CustomerCode VARCHAR(30)
AS
BEGIN
    SELECT ID_Customer, CustomerCode, CustomerName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Customers 
    WHERE CustomerCode = @CustomerCode;
END
GO

-- Search Customers By Name
CREATE PROCEDURE usp_Customer_SearchByName
    @CustomerName VARCHAR(100)
AS
BEGIN
    SELECT ID_Customer, CustomerCode, CustomerName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Customers 
    WHERE CustomerName LIKE '%' + @CustomerName + '%'
    ORDER BY CustomerName;
END
GO

-- ====================================================================================
-- USER ROLE STORED PROCEDURES
-- ====================================================================================

-- Get All User Roles
CREATE PROCEDURE usp_UserRole_GetAll
AS
BEGIN
    SELECT ID_UserRole, RoleName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM UserRoles 
    ORDER BY RoleName;
END
GO

-- Get User Role By ID
CREATE PROCEDURE usp_UserRole_GetById
    @ID_UserRole BIGINT
AS
BEGIN
    SELECT ID_UserRole, RoleName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM UserRoles 
    WHERE ID_UserRole = @ID_UserRole;
END
GO

-- Insert User Role
CREATE PROCEDURE usp_UserRole_Insert
    @RoleName VARCHAR(100),
    @CreatedOn DATETIME,
    @CreatedBy BIGINT = NULL,
    @ModifiedOn DATETIME = NULL,
    @ModifiedBy BIGINT = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO UserRoles (RoleName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
    VALUES (@RoleName, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update User Role
CREATE PROCEDURE usp_UserRole_Update
    @ID_UserRole BIGINT,
    @RoleName VARCHAR(100),
    @ModifiedOn DATETIME,
    @ModifiedBy BIGINT = NULL
AS
BEGIN
    UPDATE UserRoles 
    SET RoleName = @RoleName, 
        ModifiedOn = @ModifiedOn, 
        ModifiedBy = @ModifiedBy
    WHERE ID_UserRole = @ID_UserRole;
END
GO

-- Delete User Role
CREATE PROCEDURE usp_UserRole_Delete
    @ID_UserRole BIGINT
AS
BEGIN
    DELETE FROM UserRoles WHERE ID_UserRole = @ID_UserRole;
END
GO

-- Get User Role By Name
CREATE PROCEDURE usp_UserRole_GetByName
    @RoleName VARCHAR(100)
AS
BEGIN
    SELECT ID_UserRole, RoleName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM UserRoles 
    WHERE RoleName = @RoleName;
END
GO

-- ====================================================================================
-- USER STORED PROCEDURES
-- ====================================================================================

-- Get All Users
CREATE PROCEDURE usp_User_GetAll
AS
BEGIN
    SELECT ID_Users, UserName, UserPassword, MobileNumber, Email, FK_UserRoles, UserStatus, 
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Users 
    ORDER BY UserName;
END
GO

-- Get User By ID
CREATE PROCEDURE usp_User_GetById
    @ID_Users BIGINT
AS
BEGIN
    SELECT ID_Users, UserName, UserPassword, MobileNumber, Email, FK_UserRoles, UserStatus, 
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Users 
    WHERE ID_Users = @ID_Users;
END
GO

-- Insert User
CREATE PROCEDURE usp_User_Insert
    @UserName VARCHAR(100),
    @UserPassword VARCHAR(255),
    @MobileNumber VARCHAR(20) = NULL,
    @Email VARCHAR(255) = NULL,
    @FK_UserRoles BIGINT = NULL,
    @UserStatus VARCHAR(50) = NULL,
    @CreatedOn DATETIME,
    @CreatedBy BIGINT = NULL,
    @ModifiedOn DATETIME = NULL,
    @ModifiedBy BIGINT = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO Users (UserName, UserPassword, MobileNumber, Email, FK_UserRoles, UserStatus, 
                       CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
    VALUES (@UserName, @UserPassword, @MobileNumber, @Email, @FK_UserRoles, @UserStatus, 
            @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update User
CREATE PROCEDURE usp_User_Update
    @ID_Users BIGINT,
    @UserName VARCHAR(100),
    @UserPassword VARCHAR(255),
    @MobileNumber VARCHAR(20) = NULL,
    @Email VARCHAR(255) = NULL,
    @FK_UserRoles BIGINT = NULL,
    @UserStatus VARCHAR(50) = NULL,
    @ModifiedOn DATETIME,
    @ModifiedBy BIGINT = NULL
AS
BEGIN
    UPDATE Users 
    SET UserName = @UserName, 
        UserPassword = @UserPassword, 
        MobileNumber = @MobileNumber, 
        Email = @Email, 
        FK_UserRoles = @FK_UserRoles, 
        UserStatus = @UserStatus, 
        ModifiedOn = @ModifiedOn, 
        ModifiedBy = @ModifiedBy
    WHERE ID_Users = @ID_Users;
END
GO

-- Delete User
CREATE PROCEDURE usp_User_Delete
    @ID_Users BIGINT
AS
BEGIN
    DELETE FROM Users WHERE ID_Users = @ID_Users;
END
GO

-- Get User By UserName
CREATE PROCEDURE usp_User_GetByUserName
    @UserName VARCHAR(100)
AS
BEGIN
    SELECT ID_Users, UserName, UserPassword, MobileNumber, Email, FK_UserRoles, UserStatus, 
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Users 
    WHERE UserName = @UserName;
END
GO

-- Validate User
CREATE PROCEDURE usp_User_ValidateUser
    @UserName VARCHAR(100),
    @Password VARCHAR(255)
AS
BEGIN
    SELECT ID_Users, UserName, UserPassword, MobileNumber, Email, FK_UserRoles, UserStatus, 
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Users 
    WHERE UserName = @UserName AND UserPassword = @Password;
END
GO

-- Get Users By Role
CREATE PROCEDURE usp_User_GetByRole
    @FK_UserRoles BIGINT
AS
BEGIN
    SELECT ID_Users, UserName, UserPassword, MobileNumber, Email, FK_UserRoles, UserStatus, 
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Users 
    WHERE FK_UserRoles = @FK_UserRoles
    ORDER BY UserName;
END
GO

-- ====================================================================================
-- SUPPLIER STORED PROCEDURES
-- ====================================================================================

-- Get All Suppliers
CREATE PROCEDURE usp_Supplier_GetAll
AS
BEGIN
    SELECT ID_Supplier, SupplierCode, SupplierName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Suppliers 
    ORDER BY SupplierName;
END
GO

-- Get Supplier By ID
CREATE PROCEDURE usp_Supplier_GetById
    @ID_Supplier BIGINT
AS
BEGIN
    SELECT ID_Supplier, SupplierCode, SupplierName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Suppliers 
    WHERE ID_Supplier = @ID_Supplier;
END
GO

-- Insert Supplier
CREATE PROCEDURE usp_Supplier_Insert
    @SupplierCode VARCHAR(30),
    @SupplierName VARCHAR(100),
    @CreatedOn DATETIME,
    @CreatedBy BIGINT = NULL,
    @ModifiedOn DATETIME = NULL,
    @ModifiedBy BIGINT = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO Suppliers (SupplierCode, SupplierName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
    VALUES (@SupplierCode, @SupplierName, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Supplier
CREATE PROCEDURE usp_Supplier_Update
    @ID_Supplier BIGINT,
    @SupplierCode VARCHAR(30),
    @SupplierName VARCHAR(100),
    @ModifiedOn DATETIME,
    @ModifiedBy BIGINT = NULL
AS
BEGIN
    UPDATE Suppliers 
    SET SupplierCode = @SupplierCode, 
        SupplierName = @SupplierName, 
        ModifiedOn = @ModifiedOn, 
        ModifiedBy = @ModifiedBy
    WHERE ID_Supplier = @ID_Supplier;
END
GO

-- Delete Supplier
CREATE PROCEDURE usp_Supplier_Delete
    @ID_Supplier BIGINT
AS
BEGIN
    DELETE FROM Suppliers WHERE ID_Supplier = @ID_Supplier;
END
GO

-- Get Supplier By Code
CREATE PROCEDURE usp_Supplier_GetByCode
    @SupplierCode VARCHAR(30)
AS
BEGIN
    SELECT ID_Supplier, SupplierCode, SupplierName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Suppliers 
    WHERE SupplierCode = @SupplierCode;
END
GO

-- Search Suppliers By Name
CREATE PROCEDURE usp_Supplier_SearchByName
    @SupplierName VARCHAR(100)
AS
BEGIN
    SELECT ID_Supplier, SupplierCode, SupplierName, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM Suppliers 
    WHERE SupplierName LIKE '%' + @SupplierName + '%'
    ORDER BY SupplierName;
END
GO

-- ====================================================================================
-- PROJECT STORED PROCEDURES
-- ====================================================================================

-- Get All Projects
CREATE PROCEDURE usp_Project_GetAll
AS
BEGIN
    SELECT ID_Project, ProjectCode, ProjectName, FK_Customer, EstimateAmt, StartDate, EndDate, 
           ProjectType, ProjectStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Projects 
    ORDER BY ProjectName;
END
GO

-- Get Project By ID
CREATE PROCEDURE usp_Project_GetById
    @ID_Project BIGINT
AS
BEGIN
    SELECT ID_Project, ProjectCode, ProjectName, FK_Customer, EstimateAmt, StartDate, EndDate, 
           ProjectType, ProjectStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Projects 
    WHERE ID_Project = @ID_Project;
END
GO

-- Insert Project
CREATE PROCEDURE usp_Project_Insert
    @ProjectCode VARCHAR(30),
    @ProjectName VARCHAR(100),
    @FK_Customer BIGINT = NULL,
    @EstimateAmt DECIMAL(18,2) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @ProjectType VARCHAR(100) = NULL,
    @ProjectStatus VARCHAR(100) = NULL,
    @CreatedBy BIGINT = NULL,
    @CreatedOn DATETIME,
    @UpdatedBy BIGINT = NULL,
    @UpdatedOn DATETIME = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO Projects (ProjectCode, ProjectName, FK_Customer, EstimateAmt, StartDate, EndDate, 
                          ProjectType, ProjectStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
    VALUES (@ProjectCode, @ProjectName, @FK_Customer, @EstimateAmt, @StartDate, @EndDate, 
            @ProjectType, @ProjectStatus, @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Project
CREATE PROCEDURE usp_Project_Update
    @ID_Project BIGINT,
    @ProjectCode VARCHAR(30),
    @ProjectName VARCHAR(100),
    @FK_Customer BIGINT = NULL,
    @EstimateAmt DECIMAL(18,2) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @ProjectType VARCHAR(100) = NULL,
    @ProjectStatus VARCHAR(100) = NULL,
    @UpdatedBy BIGINT = NULL,
    @UpdatedOn DATETIME = NULL
AS
BEGIN
    UPDATE Projects 
    SET ProjectCode = @ProjectCode, 
        ProjectName = @ProjectName, 
        FK_Customer = @FK_Customer, 
        EstimateAmt = @EstimateAmt, 
        StartDate = @StartDate, 
        EndDate = @EndDate, 
        ProjectType = @ProjectType, 
        ProjectStatus = @ProjectStatus, 
        UpdatedBy = @UpdatedBy, 
        UpdatedOn = @UpdatedOn
    WHERE ID_Project = @ID_Project;
END
GO

-- Delete Project
CREATE PROCEDURE usp_Project_Delete
    @ID_Project BIGINT
AS
BEGIN
    DELETE FROM Projects WHERE ID_Project = @ID_Project;
END
GO

-- Get Project By Code
CREATE PROCEDURE usp_Project_GetByCode
    @ProjectCode VARCHAR(30)
AS
BEGIN
    SELECT ID_Project, ProjectCode, ProjectName, FK_Customer, EstimateAmt, StartDate, EndDate, 
           ProjectType, ProjectStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Projects 
    WHERE ProjectCode = @ProjectCode;
END
GO

-- Get Projects By Customer
CREATE PROCEDURE usp_Project_GetByCustomer
    @FK_Customer BIGINT
AS
BEGIN
    SELECT ID_Project, ProjectCode, ProjectName, FK_Customer, EstimateAmt, StartDate, EndDate, 
           ProjectType, ProjectStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Projects 
    WHERE FK_Customer = @FK_Customer
    ORDER BY ProjectName;
END
GO

-- Get Projects By Status
CREATE PROCEDURE usp_Project_GetByStatus
    @ProjectStatus VARCHAR(100)
AS
BEGIN
    SELECT ID_Project, ProjectCode, ProjectName, FK_Customer, EstimateAmt, StartDate, EndDate, 
           ProjectType, ProjectStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Projects 
    WHERE ProjectStatus = @ProjectStatus
    ORDER BY ProjectName;
END
GO

-- ====================================================================================
-- LEVEL STORED PROCEDURES
-- ====================================================================================

-- Get All Levels
CREATE PROCEDURE usp_Level_GetAll
AS
BEGIN
    SELECT ID_Level, LevelCode, LevelName, LevelStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Levels 
    ORDER BY LevelName;
END
GO

-- Get Level By ID
CREATE PROCEDURE usp_Level_GetById
    @ID_Level BIGINT
AS
BEGIN
    SELECT ID_Level, LevelCode, LevelName, LevelStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Levels 
    WHERE ID_Level = @ID_Level;
END
GO

-- Insert Level
CREATE PROCEDURE usp_Level_Insert
    @LevelCode VARCHAR(30),
    @LevelName VARCHAR(100),
    @LevelStatus VARCHAR(50) = NULL,
    @CreatedBy BIGINT = NULL,
    @CreatedOn DATETIME,
    @UpdatedBy BIGINT = NULL,
    @UpdatedOn DATETIME = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO Levels (LevelCode, LevelName, LevelStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
    VALUES (@LevelCode, @LevelName, @LevelStatus, @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Level
CREATE PROCEDURE usp_Level_Update
    @ID_Level BIGINT,
    @LevelCode VARCHAR(30),
    @LevelName VARCHAR(100),
    @LevelStatus VARCHAR(50) = NULL,
    @UpdatedBy BIGINT = NULL,
    @UpdatedOn DATETIME = NULL
AS
BEGIN
    UPDATE Levels 
    SET LevelCode = @LevelCode, 
        LevelName = @LevelName, 
        LevelStatus = @LevelStatus, 
        UpdatedBy = @UpdatedBy, 
        UpdatedOn = @UpdatedOn
    WHERE ID_Level = @ID_Level;
END
GO

-- Delete Level
CREATE PROCEDURE usp_Level_Delete
    @ID_Level BIGINT
AS
BEGIN
    DELETE FROM Levels WHERE ID_Level = @ID_Level;
END
GO

-- Get Level By Code
CREATE PROCEDURE usp_Level_GetByCode
    @LevelCode VARCHAR(30)
AS
BEGIN
    SELECT ID_Level, LevelCode, LevelName, LevelStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Levels 
    WHERE LevelCode = @LevelCode;
END
GO

-- Get Levels By Status
CREATE PROCEDURE usp_Level_GetByStatus
    @LevelStatus VARCHAR(50)
AS
BEGIN
    SELECT ID_Level, LevelCode, LevelName, LevelStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Levels 
    WHERE LevelStatus = @LevelStatus
    ORDER BY LevelName;
END
GO

-- ====================================================================================
-- ITEM STORED PROCEDURES
-- ====================================================================================

-- Get All Items
CREATE PROCEDURE usp_Item_GetAll
AS
BEGIN
    SELECT ID_Item, ItemCode, ItemName, ItemType, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Items 
    ORDER BY ItemName;
END
GO

-- Get Item By ID
CREATE PROCEDURE usp_Item_GetById
    @ID_Item BIGINT
AS
BEGIN
    SELECT ID_Item, ItemCode, ItemName, ItemType, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Items 
    WHERE ID_Item = @ID_Item;
END
GO

-- Insert Item
CREATE PROCEDURE usp_Item_Insert
    @ItemCode VARCHAR(30),
    @ItemName VARCHAR(100),
    @ItemType VARCHAR(100) = NULL,
    @CreatedBy BIGINT = NULL,
    @CreatedOn DATETIME,
    @UpdatedBy BIGINT = NULL,
    @UpdatedOn DATETIME = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO Items (ItemCode, ItemName, ItemType, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
    VALUES (@ItemCode, @ItemName, @ItemType, @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Item
CREATE PROCEDURE usp_Item_Update
    @ID_Item BIGINT,
    @ItemCode VARCHAR(30),
    @ItemName VARCHAR(100),
    @ItemType VARCHAR(100) = NULL,
    @UpdatedBy BIGINT = NULL,
    @UpdatedOn DATETIME = NULL
AS
BEGIN
    UPDATE Items 
    SET ItemCode = @ItemCode, 
        ItemName = @ItemName, 
        ItemType = @ItemType, 
        UpdatedBy = @UpdatedBy, 
        UpdatedOn = @UpdatedOn
    WHERE ID_Item = @ID_Item;
END
GO

-- Delete Item
CREATE PROCEDURE usp_Item_Delete
    @ID_Item BIGINT
AS
BEGIN
    DELETE FROM Items WHERE ID_Item = @ID_Item;
END
GO

-- Get Item By Code
CREATE PROCEDURE usp_Item_GetByCode
    @ItemCode VARCHAR(30)
AS
BEGIN
    SELECT ID_Item, ItemCode, ItemName, ItemType, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Items 
    WHERE ItemCode = @ItemCode;
END
GO

-- Get Items By Type
CREATE PROCEDURE usp_Item_GetByType
    @ItemType VARCHAR(100)
AS
BEGIN
    SELECT ID_Item, ItemCode, ItemName, ItemType, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
    FROM Items 
    WHERE ItemType = @ItemType
    ORDER BY ItemName;
END
GO

-- ====================================================================================
-- PROJECT LEVEL STORED PROCEDURES
-- ====================================================================================

-- Get All Project Levels
CREATE PROCEDURE usp_ProjectLevel_GetAll
AS
BEGIN
    SELECT ID_ProjectLevel, FK_Project, FK_Level 
    FROM ProjectLevels 
    ORDER BY FK_Project, FK_Level;
END
GO

-- Get Project Level By ID
CREATE PROCEDURE usp_ProjectLevel_GetById
    @ID_ProjectLevel BIGINT
AS
BEGIN
    SELECT ID_ProjectLevel, FK_Project, FK_Level 
    FROM ProjectLevels 
    WHERE ID_ProjectLevel = @ID_ProjectLevel;
END
GO

-- Insert Project Level
CREATE PROCEDURE usp_ProjectLevel_Insert
    @FK_Project BIGINT,
    @FK_Level BIGINT,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO ProjectLevels (FK_Project, FK_Level)
    VALUES (@FK_Project, @FK_Level);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Project Level
CREATE PROCEDURE usp_ProjectLevel_Update
    @ID_ProjectLevel BIGINT,
    @FK_Project BIGINT,
    @FK_Level BIGINT
AS
BEGIN
    UPDATE ProjectLevels 
    SET FK_Project = @FK_Project, 
        FK_Level = @FK_Level
    WHERE ID_ProjectLevel = @ID_ProjectLevel;
END
GO

-- Delete Project Level
CREATE PROCEDURE usp_ProjectLevel_Delete
    @ID_ProjectLevel BIGINT
AS
BEGIN
    DELETE FROM ProjectLevels WHERE ID_ProjectLevel = @ID_ProjectLevel;
END
GO

-- Get Project Levels By Project
CREATE PROCEDURE usp_ProjectLevel_GetByProject
    @FK_Project BIGINT
AS
BEGIN
    SELECT ID_ProjectLevel, FK_Project, FK_Level 
    FROM ProjectLevels 
    WHERE FK_Project = @FK_Project
    ORDER BY FK_Level;
END
GO

-- Get Project Levels By Level
CREATE PROCEDURE usp_ProjectLevel_GetByLevel
    @FK_Level BIGINT
AS
BEGIN
    SELECT ID_ProjectLevel, FK_Project, FK_Level 
    FROM ProjectLevels 
    WHERE FK_Level = @FK_Level
    ORDER BY FK_Project;
END
GO

-- ====================================================================================
-- PROJECT TRANSACTION STORED PROCEDURES
-- ====================================================================================

-- Get All Project Transactions
CREATE PROCEDURE usp_ProjectTrans_GetAll
AS
BEGIN
    SELECT ID_ProjectTrans, FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM ProjectTrans 
    ORDER BY CreatedOn DESC;
END
GO

-- Get Project Transaction By ID
CREATE PROCEDURE usp_ProjectTrans_GetById
    @ID_ProjectTrans BIGINT
AS
BEGIN
    SELECT ID_ProjectTrans, FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM ProjectTrans 
    WHERE ID_ProjectTrans = @ID_ProjectTrans;
END
GO

-- Insert Project Transaction
CREATE PROCEDURE usp_ProjectTrans_Insert
    @FK_Project BIGINT,
    @FK_Level BIGINT,
    @FK_Item BIGINT,
    @AccountType VARCHAR(100) = NULL,
    @Amount DECIMAL(18,2) = NULL,
    @Qty DECIMAL(18,2) = NULL,
    @Description TEXT = NULL,
    @CreatedOn DATETIME,
    @CreatedBy BIGINT = NULL,
    @ModifiedOn DATETIME = NULL,
    @ModifiedBy BIGINT = NULL,
    @ID BIGINT OUTPUT
AS
BEGIN
    INSERT INTO ProjectTrans (FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
                              CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
    VALUES (@FK_Project, @FK_Level, @FK_Item, @AccountType, @Amount, @Qty, @Description,
            @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy);
    
    SET @ID = SCOPE_IDENTITY();
END
GO

-- Update Project Transaction
CREATE PROCEDURE usp_ProjectTrans_Update
    @ID_ProjectTrans BIGINT,
    @FK_Project BIGINT,
    @FK_Level BIGINT,
    @FK_Item BIGINT,
    @AccountType VARCHAR(100) = NULL,
    @Amount DECIMAL(18,2) = NULL,
    @Qty DECIMAL(18,2) = NULL,
    @Description TEXT = NULL,
    @ModifiedOn DATETIME = NULL,
    @ModifiedBy BIGINT = NULL
AS
BEGIN
    UPDATE ProjectTrans 
    SET FK_Project = @FK_Project, 
        FK_Level = @FK_Level, 
        FK_Item = @FK_Item, 
        AccountType = @AccountType, 
        Amount = @Amount, 
        Qty = @Qty, 
        Description = @Description, 
        ModifiedOn = @ModifiedOn, 
        ModifiedBy = @ModifiedBy
    WHERE ID_ProjectTrans = @ID_ProjectTrans;
END
GO

-- Delete Project Transaction
CREATE PROCEDURE usp_ProjectTrans_Delete
    @ID_ProjectTrans BIGINT
AS
BEGIN
    DELETE FROM ProjectTrans WHERE ID_ProjectTrans = @ID_ProjectTrans;
END
GO

-- Get Project Transactions By Project
CREATE PROCEDURE usp_ProjectTrans_GetByProject
    @FK_Project BIGINT
AS
BEGIN
    SELECT ID_ProjectTrans, FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM ProjectTrans 
    WHERE FK_Project = @FK_Project
    ORDER BY CreatedOn DESC;
END
GO

-- Get Project Transactions By Level
CREATE PROCEDURE usp_ProjectTrans_GetByLevel
    @FK_Level BIGINT
AS
BEGIN
    SELECT ID_ProjectTrans, FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM ProjectTrans 
    WHERE FK_Level = @FK_Level
    ORDER BY CreatedOn DESC;
END
GO

-- Get Project Transactions By Item
CREATE PROCEDURE usp_ProjectTrans_GetByItem
    @FK_Item BIGINT
AS
BEGIN
    SELECT ID_ProjectTrans, FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM ProjectTrans 
    WHERE FK_Item = @FK_Item
    ORDER BY CreatedOn DESC;
END
GO

-- Get Project Transactions By Account Type
CREATE PROCEDURE usp_ProjectTrans_GetByAccountType
    @AccountType VARCHAR(100)
AS
BEGIN
    SELECT ID_ProjectTrans, FK_Project, FK_Level, FK_Item, AccountType, Amount, Qty, Description,
           CreatedOn, CreatedBy, ModifiedOn, ModifiedBy 
    FROM ProjectTrans 
    WHERE AccountType = @AccountType
    ORDER BY CreatedOn DESC;
END
GO
