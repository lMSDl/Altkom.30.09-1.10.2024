CREATE OR ALTER PROCEDURE OrderSummary
@id int
AS
BEGIN

	SELECT o.Id, o.[DateTime], COUNT(p.Id) AS COUNT
	FROM [Orders] as o
	JOIN [OrderProducts] as p ON o.Id = p.OrderId
	WHERE @id = o.Id
	GROUP BY o.Id, o.[DateTime]

END