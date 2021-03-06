create database [QuanLyQuanCafe]
go
USE [QuanLyQuanCafe]
GO


CREATE TABLE [dbo].[STAFF](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[sdt] [varchar](15) NULL,
	[address] [nvarchar](20) NULL,
	[sex] [nvarchar](20) NULL,
	[salary] [int] NULL,
	[status] [int] NULL,
)
GO
CREATE TABLE [dbo].[ACCOUNT](
	[username] [nvarchar](20) primary key NOT NULL,
	[DisplayName] [nvarchar](20) NULL,
	[password] [varchar](20) NULL,
	[type] [int] NULL,
	[id_staff] [int] REFERENCES STAFF (id), 
)
GO

CREATE TABLE [dbo].[TABLE_FOOD](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[status] [int] NULL,
)
GO

CREATE TABLE [dbo].[BILL](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[dayCheckIn] [datetime] NULL,
	[dayCheckOut] [datetime] NULL,
	[discount] [int] NULL,
	[id_staff] [int] NULL ,
	[id_table] [int] NULL,
	[status] [int] NULL,
	[totalPrice] [int] NULL,
	foreign key(id_staff) references STAFF (id),
	foreign key(id_table) references TABLE_FOOD(id)
)
GO

CREATE TABLE [dbo].[FOOD_CATEGORY](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
)
GO
CREATE TABLE [dbo].[FOOD](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[price] [int] NULL,
	[name] [nvarchar](50) NULL,
	[id_category] [int] NULL,
	foreign key(id_category) references FOOD_CATEGORY(id)
)
GO
CREATE TABLE [dbo].[BILL_INF](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[id_bill] [int] NULL,
	[id_food] [int] NULL,
	[amount] [int] NULL,
	foreign key(id_bill) references BILL (id),
	foreign key(id_food) references FOOD(id)
)
GO

INSERT [dbo].[STAFF] ( [name], [sdt], [address], [sex], [salary], [status]) VALUES ( N'Huỳnh Thị Hương Ly', N'771111111', N'Quảng Nam', N'nữ', 7000000, 1)
INSERT [dbo].[STAFF] ( [name], [sdt], [address], [sex], [salary], [status]) VALUES ( N'Phan Thành Trung', N'771111112', N'Quảng Bình', N'nữ', 7000000, 1)
INSERT [dbo].[STAFF] ( [name], [sdt], [address], [sex], [salary], [status]) VALUES (N'Lê Đỗ Trà My', N'771111113', N'Bình Dương', N'nữ', 7000000, 1)
INSERT [dbo].[STAFF] ([name], [sdt], [address], [sex], [salary], [status]) VALUES ( N'Võ Thị Thanh Ngân', N'771111114', N'Đồng Tháp', N'nữ', 7000000, 1)
INSERT [dbo].[STAFF] ([name], [sdt], [address], [sex], [salary], [status]) VALUES ( N'Trần Gia Nguyên', N'771111115', N'Gia Lai', N'nữ', 7000000, 1)
INSERT [dbo].[STAFF] ( [name], [sdt], [address], [sex], [salary], [status]) VALUES ( N'Admin', N'771111110', N'Tp.HCM', N'Nữ', 0, 1)
INSERT [dbo].[STAFF] ([name], [sdt], [address], [sex], [salary], [status]) VALUES ( N'La Lương Linh', N'771111005', N'Gia Lai', N'nữ', 7000000, 0)
GO
INSERT [dbo].[ACCOUNT] ([username], [DisplayName], [password], [type],[id_staff]) VALUES (N'admin', N'admin', N'a1',1,6)
INSERT [dbo].[ACCOUNT] ([username], [DisplayName], [password], [type],[id_staff]) VALUES (N'trung', N'Trung', N'123', 0,2)
INSERT [dbo].[ACCOUNT] ([username], [DisplayName], [password], [type],[id_staff]) VALUES (N'myldt', NULL, N'123', 0,3)
INSERT [dbo].[ACCOUNT] ([username], [DisplayName], [password], [type],[id_staff]) VALUES (N'nganvtt', NULL, N'123', 0,4)
INSERT [dbo].[ACCOUNT] ([username], [DisplayName], [password], [type],[id_staff]) VALUES (N'nguyentg', NULL, N'123', 0,5)
INSERT [dbo].[ACCOUNT] ([username], [DisplayName], [password], [type],[id_staff]) VALUES (N'huongly', N'Hương Ly', N'1', 0,1)
GO


INSERT [dbo].[FOOD_CATEGORY] ( [name]) VALUES ( N'Cà phê')
INSERT [dbo].[FOOD_CATEGORY] ( [name]) VALUES ( N'Trà sữa')
INSERT [dbo].[FOOD_CATEGORY] ( [name]) VALUES (N'Trà')
INSERT [dbo].[FOOD_CATEGORY] ( [name]) VALUES ( N'Bánh')
GO
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES (29000, N'Phin sữa đá', 1)
INSERT [dbo].[FOOD] ( [price], [name], [id_category]) VALUES ( 29000, N'Phin đen đá', 1)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES (29000, N'Bạc Xỉu', 1)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES ( 29000, N'Phin sữa nóng', 1)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES ( 29000, N'Phin đen nóng', 1)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES (29000, N'Ca cao đá', 1)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES (33000, N'Đường nâu cacao sữa', 2)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES ( 33000, N'Đường nâu kem cheese', 2)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES (30000, N'Trà xanh đậu đỏ', 3)
INSERT [dbo].[FOOD] ([price], [name], [id_category]) VALUES ( 30000, N'Trà sen vàng', 3)
INSERT [dbo].[FOOD] ( [price], [name], [id_category]) VALUES ( 29000, N'Bánh Tiramisu', 4)
INSERT [dbo].[FOOD] ( [price], [name], [id_category]) VALUES ( 29000, N'Bánh mousse Đào', 4)
INSERT [dbo].[FOOD] ( [price], [name], [id_category]) VALUES (29000, N'Bánh phô mai cà phê', 4)
INSERT [dbo].[FOOD] ( [price], [name], [id_category]) VALUES ( 9000, N'Cookies hạt bí', 4)
GO


INSERT [dbo].[TABLE_FOOD] ( [name], [status]) VALUES ( N'bàn 1', 0)
INSERT [dbo].[TABLE_FOOD] ( [name], [status]) VALUES ( N'bàn 2', 0)
INSERT [dbo].[TABLE_FOOD] ( [name], [status]) VALUES ( N'bàn 3', 0)
INSERT [dbo].[TABLE_FOOD] ([name], [status]) VALUES ( N'bàn 4', 0)
INSERT [dbo].[TABLE_FOOD] ( [name], [status]) VALUES ( N'bàn 5', 0)
INSERT [dbo].[TABLE_FOOD] ( [name], [status]) VALUES ( N'bàn 6', 0)
GO

SET IDENTITY_INSERT [dbo].[BILL] ON 
go
INSERT [dbo].[BILL] ([id], [dayCheckIn], [dayCheckOut], [discount], [id_staff], [id_table], [status], [totalPrice]) VALUES (14, CAST(N'2020-12-17T22:24:17.103' AS DateTime), NULL, 0, 6, 3, 0, NULL)
SET IDENTITY_INSERT [dbo].[BILL] OFF
GO
SET IDENTITY_INSERT [dbo].[BILL_INF] ON 

INSERT [dbo].[BILL_INF] ([id], [id_bill], [id_food], [amount]) VALUES (24, 14, 1, 1)
INSERT [dbo].[BILL_INF] ([id], [id_bill], [id_food], [amount]) VALUES (25, 14, 6, 1)
SET IDENTITY_INSERT [dbo].[BILL_INF] OFF
GO

create PROC [dbo].[USP_GetListBillByDate]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT b.id, t.name AS [Tên bàn], b.totalPrice AS [Tổng tiền], 
		format(dayCheckIn, 'dd/MM/yyyy hh:mm:ss') AS [Ngày vào], format(dayCheckOut,'dd/MM/yyyy hh:mm:ss') AS [Ngày ra], discount AS [Giảm giá]
	FROM dbo.Bill AS b,dbo.TABLE_FOOD AS t
	WHERE dayCheckIn >= @checkIn AND dayCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.id_table
END
go
CREATE PROC [dbo].[USP_GetTableList]
AS SELECT * FROM dbo.Table_Food
GO



create PROC [dbo].[USP_InsertBill]
(@idTable INT, @id_staff int)
AS
BEGIN
	update table_food
	set status=1
	where id=@idTable
	INSERT dbo.Bill 
	        ( 
				dayCheckIn ,
	          DayCheckOut ,
			  discount,
			  id_staff,
	          id_Table ,
	          status	          
	        )
	VALUES  ( 
			GETDATE() , -- DateCheckIn - date
	          NULL , -- DateCheckOut - date
			  0,
			  @id_staff,
	          @idTable , -- idTable - int
	          0  -- chưa tính tiền	          
	        );
	
END
GO

create PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idFood INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1

	SELECT @isExitsBillInfo = id, @foodCount = b.amount
	FROM Bill_Inf AS b 
	WHERE id_Bill = @idBill AND id_Food = @idFood

	IF (@isExitsBillInfo > 0)-- nếu món đã có trong bill thì cộng dồn
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.Bill_Inf	
			SET amount = @foodCount + @count 
			WHERE id_Food = @idFood
		ELSE
			DELETE dbo.Bill_Inf WHERE id_Bill = @idBill AND id_Food = @idFood
	END
	ELSE
	BEGIN
		INSERT	dbo.Bill_Inf( id_Bill, id_Food, amount )
		VALUES  (@idBill,  @idFood, @count  )
	END
END
GO


create PROC [dbo].[USP_Login]
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * 
	FROM dbo.Account a, staff s
	WHERE a.id_staff = s.id and
		UserName = @userName AND PassWord = @passWord and status=1
END
GO


create PROC [dbo].[USP_SwitchTabelEmpty]
@idTable1 INT, @idTable2 int AS 
BEGIN
	update bill
	set id_table=@idTable2
	where status=0 and id_table=@idTable1
	update TABLE_FOOD
	set status=0
	where id=@idTable1
	update TABLE_FOOD
	set status=1
	where id=@idTable2
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[USP_SwitchTabelNotEmpty]
@idTable1 INT, @idTable2 int AS 
BEGIN
	declare @id1 int
	declare @id2 int
	--lấy id bill bàn bị chuyển
	select @id1=id
	from BILL 
	where id_table=@idTable1 and status=0--0:chưa tính tiền
	--lấy id bill bàn sắp chuyển
	select @id2=id
	from BILL 
	where id_table=@idTable2 and status=0
	--lấy food bàn cũ vào bảng tạm
	SELECT id_bill,id_food,amount
	INTO #lstFoodOldTable --- temporary table
	FROM BILL_INF
	WHERE id_bill=@id1

	update #lstFoodOldTable
	set id_bill=@id2
	--them ds food từ bàn cũ sang mới
	INSERT INTO bill_inf(id_bill,id_food,amount)
	SELECT * FROM #lstFoodOldTable	 
	--thay đổi status bàn bị chuyển
	update table_food
	set status=0 
	where id=@idTable1
	--xóa ds food bàn cũ
	delete BILL_INF where id_bill=@id1
	--xóa bill bàn bị chuyển
	delete bill where id_table=@idTable1 and status=0
END
GO

CREATE PROC [dbo].[USP_UpdateAccount]
@userName NVARCHAR(20), @displayName NVARCHAR(20), @password VARCHAR(20), @type int, @id_staff int
AS
BEGIN
	UPDATE dbo.Account 
	SET displayName = @displayName , id_staff=@id_staff , Account.password=@password  , type=@type
	WHERE UserName = @userName
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UpdateBill]
@id int ,@discount int ,@totalPrice int, @id_table int
AS 
BEGIN
	UPDATE Bill 
	SET dayCheckOut = GETDATE(), status = 1,  discount =  @discount, totalPrice = @totalPrice
	WHERE id = @id
	update table_food
	set status=0
	where id=@id_table
END
GO

CREATE PROC [dbo].[USP_GetListBillByDateForReport]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT t.name, b.totalPrice, dayCheckIn, dayCheckOut, discount
	FROM dbo.Bill AS b,dbo.TABLE_FOOD AS t
	WHERE dayCheckIn >= @checkIn AND dayCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.id_table
END
GO

CREATE PROC [dbo].[USP_GetNumBillByDate]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT COUNT(*)
	FROM dbo.Bill AS b,dbo.TABLE_FOOD AS t
	WHERE dayCheckIn >= @checkIn AND dayCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.id_table
END
GO

CREATE PROC [dbo].[USP_GetListBillByDateAndPage]
@checkIn date, @checkOut date, @page int
AS 
BEGIN
	DECLARE @pageRows INT = 10
	DECLARE @selectRows INT = @pageRows
	DECLARE @exceptRows INT = (@page - 1) * @pageRows
	
	;WITH BillShow AS( SELECT b.ID, t.name AS [Tên bàn], b.totalPrice AS [Tổng tiền], dayCheckIn AS [Ngày vào], dayCheckOut AS [Ngày ra], discount AS [Giảm giá]
	FROM dbo.Bill AS b,dbo.TABLE_FOOD AS t
	WHERE dayCheckIn >= @checkIn AND dayCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.id_table)
	
	SELECT TOP (@selectRows) * FROM BillShow WHERE id NOT IN (SELECT TOP (@exceptRows) id FROM BillShow)
END
GO
CREATE PROC [dbo].[USP_GetListBillInfByIDBill]
@id_bill int
AS 
BEGIN
	SELECT f.name as [Tên món], bf.amount as [Số lượng], f.price as [Giá bán], bf.amount*f.price as [Thành tiền]
	FROM BILL_INF bf, FOOD f
	WHERE bf.id_food=f.id
	and id_bill=@id_bill
END
go
create function tongtien1
	(
			@checkIn				datetime,
			@checkOut				datetime
	)
	returns int
	as
	Begin
	declare @tien int = 0
		select @tien=sum(totalPrice)
		from BILL
		where dayCheckOut>=@checkIn and dayCheckOut<=@checkOut
		return (@tien);
	End
Go
