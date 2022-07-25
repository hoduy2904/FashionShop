create database fashionshop
go
use fashionshop
go

create table banners
(
	id int identity primary key,
	imgsrc varchar(200)
)

create table formpages
(
	id int identity primary key,
	title nvarchar(200),
	noidung nvarchar(max),
	alias varchar(100)
)

create table accounts
(
	email varchar(100) primary key,
	pwd varchar(200),
	timecreate datetime,
	identityCard varchar(20),
	isVerified bit,
	isAdmin bit,
	hoTen nvarchar(100)
)

go

create table infomationAccounts
(
	id int identity primary key,
	email varchar(100),
	AddressAccount nvarchar(max),
	phone varchar(11),
	constraint FK_INFO_Account foreign key (email) references accounts(email)
)

go

create table categoryFrist
(
	id int identity primary key,
	CategoryName nvarchar(100),
	alias varchar(max),
)

go

create table categorySecond
(
	id int identity primary key,
	CategoryName nvarchar(100),
	CategoryFrist int,
	alias varchar(max),
	constraint FK_CTSC_CTF foreign key (CategoryFrist) references categoryFrist(id)
)
go
create table Products
(
	productID varchar(20) primary key,
	productName nvarchar(150),
	price float,
	priceOld float,
	categorySecond int,
	noidung nvarchar(max),
	size varchar(100) not null,
	views int,
	alias varchar(max),
	constraint FK_Product_CTSC foreign key (categorySecond) references categorySecond(id)
)
go
create table subImages
(
id int identity primary key,
imgsrc varchar(max),
productID varchar(20),
constraint FK_SubI_products foreign key (productID) references Products (productID)
)
go
create table carts
(
	email varchar(100) not null,
	productID varchar(20) not null,
	numberProduct int,
	size varchar(10),
	constraint FK_Carts_Users foreign key (email) references accounts(email),
	constraint FK_Carts_Products foreign key (productID) references Products(productID),
	constraint PK_carts primary key (email, productID,size)
)
go
create table Orders
(
	OrderID int identity primary key,
	email varchar(100),
	isVerified bit,
	addressOrder nvarchar(200),
	phone varchar(11),
	timecreate datetime,
	constraint FK_Orders_Users foreign key (email) references accounts(email)
)

go
create table OrderDetail
(
	id int identity primary key,
	OrderID int,
	productID varchar(20),
	numberProduct int,
	price float,
	size varchar(10),
	constraint FK_OrdersDetail_Order foreign key (OrderID) references Orders(OrderID),
	constraint FK_OrdersDetail_Product foreign key (productID) references Products(productID)
)