using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seeding
{
    public static class SeedingData
    {
        public static void SeedingDummyData(AppDbContext appDbContext)
        {
            if (!appDbContext.Branches.Any())
            {
                appDbContext.Database.ExecuteSqlRaw(@"-- Insert into Branches
SET IDENTITY_INSERT Branches ON;

INSERT INTO Branches (Id, Name, Address, City, Country, Phone, CreatedOn, IsDeleted)
VALUES 
(1, 'Main Branch', '123 Main St', 'New York', 'USA', '123-456-7890', CURRENT_TIMESTAMP, 0),
(2, 'Second Branch', '456 Elm St', 'Los Angeles', 'USA', '987-654-3210', CURRENT_TIMESTAMP, 0),
(3, 'Third Branch', '789 Pine St', 'Chicago', 'USA', '555-123-4567', CURRENT_TIMESTAMP, 0);

SET IDENTITY_INSERT Branches OFF;

-- Insert into Services
SET IDENTITY_INSERT Services ON;

INSERT INTO Services (Id, Name, Description, Price, Duration, CreatedOn, IsDeleted)
VALUES 
(1, 'Haircut', 'Basic haircut service', 25.00, 30, CURRENT_TIMESTAMP, 0),
(2, 'Manicure', 'Nail care service', 30.00, 45, CURRENT_TIMESTAMP, 0),
(3, 'Massage', 'Relaxing full-body massage', 50.00, 60, CURRENT_TIMESTAMP, 0),
(4, 'Facial', 'Skin care facial', 40.00, 50, CURRENT_TIMESTAMP, 0),
(5, 'Pedicure', 'Foot care service', 35.00, 50, CURRENT_TIMESTAMP, 0);

SET IDENTITY_INSERT Services OFF;

-- Insert into Clients
SET IDENTITY_INSERT Clients ON;

INSERT INTO Clients (Id, FirstName, LastName, Gender, Email, Phone, Address, City, Country, Birthdate, CreatedOn, IsDeleted)
VALUES 
(1, 'John', 'Doe', 'Male', 'john.doe@example.com', '555-123-4567', '101 Maple St', 'New York', 'USA', '1990-05-15', CURRENT_TIMESTAMP, 0),
(2, 'Jane', 'Smith', 'Female', 'jane.smith@example.com', '555-987-6543', '202 Oak St', 'Los Angeles', 'USA', '1985-09-25', CURRENT_TIMESTAMP, 0),
(3, 'Alice', 'Brown', 'Female', 'alice.brown@example.com', '555-654-3210', '303 Pine St', 'Chicago', 'USA', '1993-12-10', CURRENT_TIMESTAMP, 0),
(4, 'Bob', 'Johnson', 'Male', 'bob.johnson@example.com', '555-333-4444', '404 Oakwood Ave', 'Houston', 'USA', '1980-08-22', CURRENT_TIMESTAMP, 0),
(5, 'Emma', 'Williams', 'Female', 'emma.williams@example.com', '555-666-7777', '505 Cedar Blvd', 'San Francisco', 'USA', '1995-07-19', CURRENT_TIMESTAMP, 0);

SET IDENTITY_INSERT Clients OFF;

-- Insert into Bookings
SET IDENTITY_INSERT Bookings ON;

INSERT INTO Bookings (Id, ClientId, BranchId, BookingDate, BookingTime, Status, CreatedOn, IsDeleted)
VALUES 
(1, 1, 1, '2024-12-20', '10:00:00', 'Completed', CURRENT_TIMESTAMP, 0),
(2, 2, 2, '2024-12-21', '14:00:00', 'Pending', CURRENT_TIMESTAMP, 0),
(3, 3, 3, '2024-12-22', '16:30:00', 'Cancelled', CURRENT_TIMESTAMP, 0),
(4, 4, 1, '2024-12-23', '11:00:00', 'Completed', CURRENT_TIMESTAMP, 0),
(5, 5, 2, '2024-12-24', '15:00:00', 'Pending', CURRENT_TIMESTAMP, 0),
(6, 1, 3, '2024-12-25', '09:00:00', 'Completed', CURRENT_TIMESTAMP, 0),
(7, 2, 1, '2024-12-26', '13:00:00', 'Pending', CURRENT_TIMESTAMP, 0),
(8, 3, 2, '2024-12-27', '17:00:00', 'Cancelled', CURRENT_TIMESTAMP, 0),
(9, 4, 3, '2024-12-28', '08:00:00', 'Completed', CURRENT_TIMESTAMP, 0),
(10, 5, 1, '2024-12-29', '14:30:00', 'Pending', CURRENT_TIMESTAMP, 0);

SET IDENTITY_INSERT Bookings OFF;

-- Insert into BookingServices
SET IDENTITY_INSERT BookingServices ON;

INSERT INTO BookingServices (Id, BookingId, ServiceId, Price, CreatedOn, IsDeleted)
VALUES 
(1, 1, 1, 25.00, CURRENT_TIMESTAMP, 0), 
(2, 2, 2, 30.00, CURRENT_TIMESTAMP, 0), 
(3, 3, 3, 50.00, CURRENT_TIMESTAMP, 0), 
(4, 4, 4, 40.00, CURRENT_TIMESTAMP, 0),
(5, 5, 5, 35.00, CURRENT_TIMESTAMP, 0),
(6, 6, 1, 25.00, CURRENT_TIMESTAMP, 0),
(7, 7, 2, 30.00, CURRENT_TIMESTAMP, 0),
(8, 8, 3, 50.00, CURRENT_TIMESTAMP, 0),
(9, 9, 4, 40.00, CURRENT_TIMESTAMP, 0),
(10, 10, 5, 35.00, CURRENT_TIMESTAMP, 0);

SET IDENTITY_INSERT BookingServices OFF;

-- Insert into Transactions
SET IDENTITY_INSERT Transactions ON;

INSERT INTO Transactions (Id, BookingId, Amount, PaymentMethod, PaymentDate, CreatedOn, IsDeleted)
VALUES 
(1, 1, 25.00, 'Credit Card', '2024-12-20', CURRENT_TIMESTAMP, 0),
(2, 4, 40.00, 'Cash', '2024-12-23', CURRENT_TIMESTAMP, 0),
(3, 6, 25.00, 'Credit Card', '2024-12-25', CURRENT_TIMESTAMP, 0),
(4, 9, 40.00, 'Cash', '2024-12-28', CURRENT_TIMESTAMP, 0),
(5, 2, 30.00, 'Credit Card', '2024-12-21', CURRENT_TIMESTAMP, 0),
(6, 5, 35.00, 'Cash', '2024-12-24', CURRENT_TIMESTAMP, 0),
(7, 7, 30.00, 'Credit Card', '2024-12-26', CURRENT_TIMESTAMP, 0),
(8, 8, 50.00, 'Cash', '2024-12-27', CURRENT_TIMESTAMP, 0),
(9, 3, 50.00, 'Credit Card', '2024-12-22', CURRENT_TIMESTAMP, 0),
(10, 10, 35.00, 'Cash', '2024-12-29', CURRENT_TIMESTAMP, 0);

SET IDENTITY_INSERT Transactions OFF;
");
            }
        }
    }
}
