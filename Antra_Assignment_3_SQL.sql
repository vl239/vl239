-- Virginia Lu, Assignent 3 SQL

-- Write queries for following scenarios
-- Use Northwind database. All questions are based on assumptions described by the Database Diagram sent to you yesterday. 
-- When inserting, make up info if necessary. Write query for each step. Do not use IDE. BE CAREFUL WHEN DELETING DATA OR DROPPING TABLE.
USE Northwind
GO

-- 1. Create a view named “view_product_order_[your_last_name]”, list all products and total ordered quantity for that product.
-- DROP VIEW view_product_order_Lu
CREATE VIEW view_product_order_Lu AS (
    SELECT p.ProductID, p.ProductName, COUNT(od.Quantity) AS TotalOrderedQuantity
    FROM Products p INNER JOIN [Order Details] od ON od.ProductID = p.ProductID
    GROUP BY p.ProductID, p.ProductName
)

-- 2. Create a stored procedure “sp_product_order_quantity_[your_last_name]” that accept product id as an input and total quantities of order as output parameter.
-- DROP PROC sp_product_order_quantity_Lu
CREATE PROC sp_product_order_quantity_Lu
@id INT,
@total INT OUT
AS
BEGIN
SELECT @id = view_product_order_Lu.ProductID, @total = view_product_order_Lu.TotalOrderedQuantity
FROM view_product_order_Lu
WHERE view_product_order_Lu.ProductID = @id
END

-- example execution of procedure
DECLARE @id INT, @total INT
EXEC sp_product_order_quantity_Lu 8, @total out
print @total

-- 3. Create a stored procedure “sp_product_order_city_[your_last_name]” that accept product name as an input and top 5 cities that ordered most that product 
--    combined with the total quantity of that product ordered from that city as output.
-- DROP PROC sp_product_order_city_Lu
CREATE PROC sp_product_order_city_Lu
@name VARCHAR(20),
@order_city VARCHAR(20) OUT
AS
BEGIN
SELECT @name = pn.ProductName FROM (
    SELECT TOP 5 topProd.ProductID, topProd.ProductName FROM (
        SELECT p.ProductID, p.ProductName, SUM(od.Quantity) q FROM Products p
        INNER JOIN [Order Details] od ON p.ProductID = od.ProductID
        GROUP BY p.ProductID, p.ProductName
    ) AS topProd
    ORDER BY topProd.q DESC
) AS pn
LEFT JOIN (
    SELECT * FROM (
        SELECT [1].ProductID, [1].City, RANK() OVER (
            PARTITION BY ProductID
            ORDER BY q DESC
        ) AS [2]
        FROM (
            SELECT p.ProductID, c.City, SUM(od.Quantity) q 
            FROM Customers c JOIN Orders o ON c.CustomerID = o.CustomerID
            LEFT JOIN [Order Details] od ON o.OrderID = od.OrderID
            LEFT JOIN Products p ON od.ProductID = p.ProductID
            GROUP BY p.ProductID, c.City
        ) AS [1]
    ) AS [3]
    WHERE [3].[2] = 1
) AS [4]
ON pn.ProductID = [4].ProductID
WHERE [4].City = @order_city
END

-- example execution of procedure
DECLARE @name nvarchar(max), @order_city nvarchar(max)
EXEC sp_product_order_city_Lu 'Camembert Pierrot', @order_city out
print @order_city

-- 4. Create 2 new tables “people_your_last_name” “city_your_last_name”. City table has two records: {Id:1, City: Seattle}, {Id:2, City: Green Bay}. People has 
--    three records: {id:1, Name: Aaron Rodgers, City: 2}, {id:2, Name: Russell Wilson, City:1}, {Id: 3, Name: Jody Nelson, City:2}. Remove city of Seattle. 
--    If there was anyone from Seattle, put them into a new city “Madison”. Create a view “Packers_your_name” lists all people from Green Bay. 
--    If any error occurred, no changes should be made to DB. (after test) Drop both tables and view.
CREATE TABLE people_Lu(
    id INT,
    fullname VARCHAR(20),
    cityID INT
)
INSERT INTO people_Lu VALUES(1, 'Aaron Rodgers', 2)
INSERT INTO people_Lu VALUES(2, 'Russell Wilson', 1)
INSERT INTO people_Lu VALUES(3, 'Jody Nelson', 2)

CREATE TABLE city_Lu(
    cityID int,
    cityName VARCHAR(20)
)
INSERT INTO city_Lu VALUES(1, 'Seattle')
INSERT INTO city_Lu VALUES(2, 'Green Bay')

UPDATE city_Lu
SET cityName = 'Madison'
WHERE cityName = 'Seattle'

CREATE VIEW Packers_Lu AS (
    SELECT p.fullname
    FROM people_Lu p 
    INNER JOIN city_Lu c ON p.cityID = c.cityID
    WHERE c.cityName = 'Green Bay'
)
BEGIN TRAN
ROLLBACK
DROP TABLE people_Lu
DROP TABLE city_Lu
DROP VIEW Packers_Lu

-- 5. Create a stored procedure “sp_birthday_employees_[you_last_name]” that creates a new table “birthday_employees_your_last_name” 
-- and fill it with all employees that have a birthday on Feb. (Make a screen shot) drop the table. Employee table should not be affected.
-- Fill table with Feb birthday employees as part of procedure:
CREATE PROC sp_birthday_employees_Lu1
AS
BEGIN
    CREATE TABLE birthday_employees_Lu1(
    EmployeeID INT,
    EmployeeName NVARCHAR(20),
    BirthDate DATETIME
    )
    INSERT INTO birthday_employees_Lu1 
        SELECT EmployeeID, FirstName+' '+LastName, BirthDate
        FROM Employees
        WHERE MONTH(BirthDate) = 2
END

EXEC sp_birthday_employees_Lu1

DROP TABLE birthday_employees_Lu1
-- DROP PROC sp_birthday_employees_Lu1

-- Procedure only creates table and it's filled after created
CREATE PROC sp_birthday_employees_Lu2
AS
BEGIN
    CREATE TABLE birthday_employees_Lu2(
    EmployeeID INT,
    EmployeeName NVARCHAR(20),
    BirthDate DATETIME
    )
END

EXEC sp_birthday_employees_Lu2

INSERT INTO birthday_employees_Lu2 
SELECT EmployeeID, FirstName+' '+LastName, BirthDate
FROM Employees
WHERE MONTH(BirthDate) = 2

DROP TABLE birthday_employees_Lu2
-- DROP PROC sp_birthday_employees_Lu2


-- 6. How do you make sure two tables have the same data?
-- Store total data count of both tables, union the tables, check if total data count of unioned table is same as 
-- original total data count of the two tables.
