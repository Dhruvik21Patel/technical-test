1. Given a table of Orders and a table of Customers, write a query to retrieve the top 5
customers with the highest total order value.
=> select c.id, c.name, SUM(o.total_amount) as "TotalOrderValue" from Customers c
    join Orders o on c.id = o.customer_id
    Group by c.id, c.name
    Order by TotalOrderValue desc
    LIMIT 5;

2. You have a table with Orders (OrderId, CustomerId, OrderDate, TotalAmount). Write
a query to get the total amount spent by each customer in the current month,
ordered by the highest spenders first.
=> SELECT CustomerId, SUM(TotalAmount) AS TotalSpent
    FROM Orders
    WHERE DATE_TRUNC('month', "OrderDate") = DATE_TRUNC('month', CURRENT_DATE)
    GROUP BY CustomerId
    ORDER BY TotalSpent DESC;

3. You have a table with Orders (OrderId, CustomerId, OrderDate, TotalAmount). Write
a query to get the latest orders from each customer. The query should return all the
columns (OrderId, CustomerId, OrderDate, TotalAmount) from the Orders table for
this list of latest orders.
=> SELECT DISTINCT ON (CustomerId) 
    OrderId,
    CustomerId,
    OrderDate,
    TotalAmount FROM Orders
    ORDER BY CustomerId, OrderDate DESC;

4. Optimization Query: Can you identify potential issues and suggest improvements
for the query below?
SELECT *
FROM Orders
WHERE CustomerId IN (SELECT CustomerId FROM Customers WHERE Country =
'USA');
=> SELECT o.* FROM Orders o
    JOIN Customers c
    ON o.CustomerId = c.id
    WHERE c.Country = 'USA';