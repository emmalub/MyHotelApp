-- Hämta alla aktiva kunder
SELECT * 
FROM Customers
WHERE IsActive = 1;

-- Hämta bokningar för ett rum
SELECT * 
FROM Bookings
WHERE RoomId = 3
AND IsActive = 1;

-- Tillgängliga rum under en period
SELECT *
FROM Rooms r
WHERE r.Id NOT IN (
	SELECT RoomId
	FROM Bookings
	WHERE CheckInDate < 2025-01-08 AND CheckOutDate > 2025-01-12
	);

-- Lägg till bokning
INSERT INTO Bookings (
	GuestId,
	RoomId,
	CheckInDate,
	CheckInDate,
	TotalPrice,
	IsPaid,
	ExtraBeds, 
	Conditions
	)
VALUES (
	2,
	5,
	2025-01-06,
	2025-01-12,
	7000,
	1,
	0,
	null
);

-- Uppdatera en faktura till betald
UPDATE Invoices
SET IsPaid = 1
WHERE Id = 2;

-- Ta bort bokning där fakturan inte är betald
DELETE FROM Bookings
WHERE Id IN (
	SELECT BookingId
	FROM Invoices
	WHERE DueDate < GETDATE() AND IsPaid = 0
	);

-- Bokningar och bokningens rum
SELECT 
	b.Id AS BookingId,
	r.Id AS RoomId,
	r.RoomType, 
	r.Price,
	b.CheckInDate,
	b.CheckOutDate
FROM
	Bookings b
INNER JOIN
	Rooms r ON b.RoomID = r.ID

-- VIP kunders senaste bokning
SELECT 
	c.FirstName,
	c.LastName,
	MAX(b.CheckInDate) AS LastCheckIn
FROM 
	Customers c
INNER JOIN
	Bookings b ON c.Id = b.GuestId
WHERE 
	c.IsVip = 1
GROUP BY
	c.FirstName, 
	c.LastName;