USE [SalesTransactions]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](50) NOT NULL,
	[CustomerAddress] [varchar](50) NOT NULL,
	[ContactNo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[CustomerIdFK] [int] NOT NULL,
	[InvoiceTotal] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[ProductRate] [money] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesTransaction]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesTransaction](
	[SalesTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerIdFK] [int] NOT NULL,
	[ProductIdFK] [int] NOT NULL,
	[Rate] [varchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [varchar](max) NOT NULL,
	[InvoiceIdFK] [int] NULL,
 CONSTRAINT [PK_SalesTransaction] PRIMARY KEY CLUSTERED 
(
	[SalesTransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY([CustomerIdFK])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Customer]
GO
ALTER TABLE [dbo].[SalesTransaction]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransaction_Customer] FOREIGN KEY([CustomerIdFK])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[SalesTransaction] CHECK CONSTRAINT [FK_SalesTransaction_Customer]
GO
ALTER TABLE [dbo].[SalesTransaction]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransaction_Product] FOREIGN KEY([ProductIdFK])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[SalesTransaction] CHECK CONSTRAINT [FK_SalesTransaction_Product]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllCustomers]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_GetAllCustomers] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT c.CustomerId, c.CustomerName, c.CustomerAddress, c.ContactNo From Customer as c
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProducts]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllProducts] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.ProductId, p.ProductName, p.ProductRate From Product as p
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInvoice]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetInvoice] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select * from Invoice
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInvoiceById]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetInvoiceById] 
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select i.InvoiceId as Id,i.InvoiceDate as InvoiceDate,i.InvoiceTotal as Total, st.Quantity as Quantity, st.Rate as Rate,st.Total as IndividualTotal,c.CustomerName as CustomerName,p.ProductName as ProductName	 from Invoice i
	left join SalesTransaction st
	on i.InvoiceId = st.InvoiceIdFK
	left join Customer c
	on i.CustomerIdFK = c.CustomerId
	left join Product p 
	on st.ProductIdFK = p.ProductId
	where i.InvoiceId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSalesTransaction]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_GetSalesTransaction] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select * from SalesTransaction
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSalesTransactionById]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_GetSalesTransactionById] 
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select * from SalesTransaction where SalesTransactionId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertCustomers]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_InsertCustomers] 
	@CustomerName varchar(MAX),
	@CustomerAddress varchar(MAX),
	@ContactNo varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 Insert Into Customer (CustomerName,CustomerAddress,ContactNo) values (@CustomerName,@CustomerAddress,@ContactNo)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertInvoice]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertInvoice] 
	@InvoiceDate datetime = null,
	@CustomerId int =null,
	@SalesTransactionIds nvarchar(MAX) =null
AS
BEGIN
	DECLARE @invoiceTotal decimal;
	set @invoiceTotal = (select sum(Convert(decimal,Total)) from SalesTransaction where SalesTransactionId in (SELECT convert(int, value) FROM string_split(@SalesTransactionIds,',')));
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	

 Insert Into Invoice(InvoiceDate,CustomerIdFK,InvoiceTotal) values (@InvoiceDate,@CustomerId,Convert(nvarchar(max),@invoiceTotal));

 update SalesTransaction set InvoiceIdFK = SCOPE_IDENTITY() where SalesTransactionId in (SELECT convert(int, value) FROM string_split(@SalesTransactionIds,','))
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertProducts]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertProducts] 
	@ProductName varchar(MAX),
	@ProductRate varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 Insert Into Product (ProductName,ProductRate) values (@ProductName,@ProductRate)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertSalesTransaction]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertSalesTransaction] 
	@CustomerId int  =null,
	@ProductId int = null,
	@Rate nvarchar(MAX) = null,
	@Quantity int =null,
	@Total nvarchar(MAX) =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 Insert Into SalesTransaction(CustomerIdFK,ProductIdFK,Rate,Quantity,Total) values (@CustomerId,@ProductId,@Rate,@Quantity,@Total)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomers]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_UpdateCustomers]
	@CustomerId int,
	@CustomerName varchar(MAX),
	@CustomerAddress varchar(MAX),
	@ContactNo varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Customer
	set CustomerName=@CustomerName,
	CustomerAddress=@CustomerAddress,
	ContactNo=@ContactNo
	where CustomerId=@CustomerId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProducts]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateProducts]
	@ProductId int,
	@ProductName varchar(MAX),
	@ProductRate varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Product
	set ProductName=@ProductName,
	ProductRate=@ProductRate
	where ProductId=@ProductId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSalesTransaction]    Script Date: 5/1/2022 10:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateSalesTransaction] 
	@Id int = null,
	@CustomerId int = null,
	@ProductId int = null,
	@Rate nvarchar(MAX) = null,
	@Quantity int = null,
	@Total nvarchar(MAX) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Update SalesTransaction
	set Rate = @Rate,Quantity = @Quantity, Total = @Total
	where SalesTransactionId = @Id and CustomerIdFK = @CustomerId and ProductIdFK = @ProductId
END
GO
