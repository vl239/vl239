-- Virginia Lu, Assignment 2 SQL

-- Write queries for following scenarios
-- Joins: (AdventureWorks)
use AdventureWorks2019
GO

-- 1. Write a query that lists the country and province names from person.CountryRegion and person.StateProvince tables. Join them and produce a result set 
-- similar to the following.
--     Country                        Province
SELECT c.[Name], p.[Name]
FROM Person.CountryRegion AS c INNER JOIN Person.StateProvince AS p ON c.CountryRegionCode = p.CountryRegionCode;

-- 2. Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables and list the countries filter them by Germany and Canada.
-- Join them and produce a result set similar to the following.
--     Country                        Province
SELECT c.[Name], p.[Name]
FROM Person.CountryRegion AS c INNER JOIN Person.StateProvince AS p ON c.CountryRegionCode = p.CountryRegionCode
WHERE c.[Name] IN ('Germany', 'Canada');


--  Using Northwind Database: (Use aliases for all the Joins)
use Northwind
GO

-- 3. List all Products that has been sold at least once in last 25 years.
SELECT p.ProductName, COUNT(o.OrderID) AS NumOfOrders
FROM Orders AS o JOIN [Order Details] AS od ON o.OrderID = od.OrderID JOIN Products AS p ON p.ProductID = od.ProductID
WHERE DATEDIFF(year, o.OrderDate, GETDATE()) <= 25
GROUP BY p.ProductName
ORDER BY NumOfOrders DESC;

-- 4. List top 5 locations (Zip Code) where the products sold most in last 25 years.
SELECT TOP 5 o.ShipPostalCode, COUNT(o.OrderID) AS NumOfOrders
FROM Orders AS o JOIN [Order Details] AS od ON o.OrderID = od.OrderID JOIN Products AS p ON p.ProductID = od.ProductID
WHERE DATEDIFF(year, o.OrderDate, GETDATE()) <= 25
GROUP BY o.ShipPostalCode
ORDER BY NumOfOrders DESC;

-- 5. List all city names and number of customers in that city.     
SELECT City, COUNT(*) AS NumOfCustomers
FROM Customers
GROUP BY City
ORDER BY NumOfCustomers DESC;

-- 6. List city names which have more than 2 customers, and number of customers in that city
SELECT City, COUNT(*) AS NumOfCustomers
FROM Customers
GROUP BY City
HAVING COUNT(*) > 2
ORDER BY NumOfCustomers DESC;

-- 7. Display the names of all customers  along with the  count of products they bought
-- (Count of products overall):
SELECT c.ContactName, COUNT(p.ProductID) AS NumOfProducts
FROM Products AS p 
JOIN [Order Details] AS od ON p.ProductID = od.ProductID
JOIN Orders AS o ON od.OrderID = o.OrderID
JOIN Customers AS c ON o.CustomerID = c.CustomerID
GROUP BY c.ContactName
ORDER BY NumOfProducts DESC;
-- (Count of unique products):
SELECT c.ContactName, COUNT(DISTINCT(p.ProductID)) AS NumOfProducts
FROM Products AS p 
JOIN [Order Details] AS od ON p.ProductID = od.ProductID
JOIN Orders AS o ON od.OrderID = o.OrderID
JOIN Customers AS c ON o.CustomerID = c.CustomerID
GROUP BY c.ContactName
ORDER BY NumOfProducts DESC;
-- (NOTE: I wasn't sure which it was asking for, but for the next problem I assume count of products overall,
-- since the highest count of unique products for each customer is only 56.)

-- 8. Display the customer ids who bought more than 100 Products with count of products.
SELECT c.CustomerID, COUNT(p.ProductID) AS NumOfProducts
FROM Products AS p 
JOIN [Order Details] AS od ON p.ProductID = od.ProductID
JOIN Orders AS o ON od.OrderID = o.OrderID
JOIN Customers AS c ON o.CustomerID = c.CustomerID
GROUP BY c.CustomerID
HAVING COUNT(p.ProductID) > 100
ORDER BY NumOfProducts DESC;

-- 9. List all of the possible ways that suppliers can ship their products. Display the results as below
--     Supplier Company Name                Shipping Company Name
SELECT supp.CompanyName AS 'Supplier Company Name', ship.CompanyName AS 'Shipping Company Name'
FROM Suppliers AS supp CROSS JOIN Shippers AS ship
ORDER BY 1;

-- 10. Display the products order each day. Show Order date and Product Name.
SELECT o.OrderDate, p.ProductName
FROM Orders AS o JOIN [Order Details] AS od ON o.OrderID = od.OrderID JOIN Products AS p ON p.ProductID = od.ProductID
ORDER BY 1;

-- 11. Displays pairs of employees who have the same job title.
SELECT e1.Title, e1.EmployeeID , e1.LastName , e2.EmployeeID, e2.LastName
FROM Employees e1 
INNER JOIN Employees e2 ON e2.Title = e1.Title 
WHERE e2.EmployeeID != e1.EmployeeID

-- 12. Display all the Managers who have more than 2 employees reporting to them.
SELECT m.FirstName AS 'Manager', COUNT(m.EmployeeID) AS 'Number of Direct Reports'
FROM Employees e INNER JOIN Employees m ON e.ReportsTo = m.EmployeeID
GROUP BY m.FirstName
HAVING COUNT(m.EmployeeID) > 2;

-- 13. Display the customers and suppliers by city. The results should have the following columns
-- City
-- Name
-- Contact Name,
-- Type (Customer or Supplier)
-- All scenarios are based on Database NORTHWIND.
SELECT City, CompanyName, ContactName, 'Supplier' AS [Type]
FROM Suppliers
UNION
SELECT City, CompanyName, ContactName, 'Customer' AS [Type]
FROM Customers;

-- 14. List all cities that have both Employees and Customers.
SELECT DISTINCT c.City FROM Customers c INNER JOIN Employees e ON c.City = e.City;

-- 15. List all cities that have Customers but no Employee.
-- a. Use sub-query
SELECT DISTINCT City
FROM Customers 
WHERE City NOT IN 
(SELECT City
FROM Employees);
-- b. Do not use sub-query
SELECT DISTINCT c.City 
FROM Customers c, Employees e 
WHERE c.City <> e.City

-- 16. List all products and their total order quantities throughout all orders.
SELECT p.ProductName, SUM(od.Quantity) AS 'Total Order Quantities'
FROM Products p JOIN [Order Details] od ON p.ProductID = od.ProductID
GROUP BY p.ProductName;

-- 17. List all Customer Cities that have at least two customers.
-- a. Use union
SELECT City FROM Customers
GROUP BY City
HAVING COUNT(*) = 2
UNION
SELECT City FROM Customers
GROUP BY City
HAVING COUNT(*) > 2;
-- b. Use sub-query and no union
SELECT DISTINCT City FROM Customers
WHERE City IN (
    SELECT City FROM Customers
    GROUP BY City
    HAVING COUNT(*) >= 2);

-- 18. List all Customer Cities that have ordered at least two different kinds of products.
SELECT c.City, COUNT(DISTINCT(p.ProductID)) AS NumOfProducts
FROM Products AS p 
JOIN [Order Details] AS od ON p.ProductID = od.ProductID
JOIN Orders AS o ON od.OrderID = o.OrderID
JOIN Customers AS c ON o.CustomerID = c.CustomerID
GROUP BY c.City
HAVING COUNT(DISTINCT(p.ProductID)) >= 2
ORDER BY NumOfProducts DESC;

-- 19. List 5 most popular products, their average price, and the customer city that ordered most quantity of it.
SELECT TOP 5 p.ProductName, AVG(p.UnitPrice) AS AvgPrice, c.City, SUM(od.Quantity) AS Quantity
FROM Products AS p 
JOIN [Order Details] AS od ON p.ProductID = od.ProductID
JOIN Orders AS o ON od.OrderID = o.OrderID
JOIN Customers AS c ON o.CustomerID = c.CustomerID
GROUP BY p.ProductName, c.City
ORDER BY SUM(od.Quantity) DESC;
 
-- 20. List one city, if exists, that is the city from where the employee sold most orders (not the product quantity) is, 
-- and also the city of most total quantity of products ordered from. (tip: join  sub-query)
SELECT DISTINCT c.City
FROM Orders o INNER JOIN Customers c ON o.CustomerID = c.CustomerID
WHERE c.City IN (
    SELECT TOP 1 c.City FROM Products p 
    JOIN [Order Details] od ON od.ProductID = p.ProductID
    JOIN Orders o ON o.OrderID = od.OrderID
    JOIN Customers c on c.CustomerID = o.CustomerID
    GROUP BY c.City
    ORDER BY COUNT(o.OrderID) DESC
)
AND c.City IN (
    SELECT TOP 1 c.City FROM Products p 
    JOIN [Order Details] od ON od.ProductID = p.ProductID
    JOIN Orders o ON o.OrderID = od.OrderID
    JOIN Customers c on c.CustomerID = o.CustomerID
    GROUP BY c.City
    ORDER BY SUM(od.Quantity) DESC    
)

-- 21. How do you remove the duplicates record of a table?
-- You can use a common table expression (CTE) to delete duplicate records. 
-- In general, you first find the duplicate rows using GROUP BY or the ROW_NUMBER() function. 
-- Then you can use DELETE to remove all but one occurrence of each record.
-- A general template may look like:
WITH cte AS (
    SELECT recordID, field1, field2, field3, ROW_NUMBER()
    OVER (
        PARTITION BY field1, field2, field3
        ORDER BY field1, field2, field3
    ) rowNumber
    FROM tableName
)
DELETE FROM cte
WHERE rowNumber > 1;