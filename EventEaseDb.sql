USE master;
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'EventEaseDB')
    DROP DATABASE EventEaseDB;
CREATE DATABASE EventEaseDB;

USE EventEaseDB;

-- Venue Table
CREATE TABLE Venue (
    VenueID INT IDENTITY(1,1) PRIMARY KEY,
    VenueName NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL CHECK (Capacity > 0),
    ImageUrl NVARCHAR(1000) NULL
);

-- Event Table
CREATE TABLE Event (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventName NVARCHAR(255) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description NVARCHAR(1000) NULL,
    VenueID INT NULL,
    FOREIGN KEY (VenueID) REFERENCES Venue(VenueID) ON DELETE SET NULL
);

-- Booking Table
CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT NOT NULL,
    VenueID INT NOT NULL,
    BookingDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (EventID) REFERENCES Event(EventID) ON DELETE CASCADE,
    FOREIGN KEY (VenueID) REFERENCES Venue(VenueID) ON DELETE CASCADE,
    CONSTRAINT UQ_Venue_Event UNIQUE (VenueID, EventID) --Prevents double booking 
);

-- Insert data into Venue table
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
VALUES 
('The Wanderers Club', '21 North Street, Illovo, Johannesburg', 500, 'https://www.wanderersgolfclub.com/resources/WGC/uploads/a5093910-ca86-72a9-be2d-6d54d0512a4b.jpg'),
('Montecasino', 'Winnie Mandela Drive, Fourways, Johannesburg', 800, 'https://upload.wikimedia.org/wikipedia/commons/1/1f/Montecasino.jpg'),
('Gold Reef City', 'Northern Parkway, Ormonde, Johannesburg', 600, 'https://zb2g8qspmxpc-u2909.pressidiumcdn.com/wp-content/uploads/2022/08/Gold-Reef-City-Theme-Park-Hotel-Access-to-the-theme-Park.jpg'),
('The Forum at The Campus', '57 Sloane Street, Bryanston, Johannesburg', 350, 'https://theplannerguru.co.za/wp-content/uploads/2016/02/ICT-Foyer.jpg'),
('Emperors Palace', '64 Jones Road, Kempton Park, Johannesburg', 700, 'https://www.peermont.com/wp-content/uploads/Emporium.jpg'),
('Molapo Crossing', 'Soweto Highway, Moletsane, Soweto, Johannesburg', 200, 'https://www.sundaystandard.info/wp-content/uploads/2015/08/molapo-piazza-696x464.jpg');

-- Insert data into Event table
INSERT INTO Event (EventName, EventDate, Description, VenueID)
VALUES 
('Tech Conference 2026', '2026-05-08 08:30:00', 'Annual conference on technology and innovations.', 1),
('Wedding Reception - Johnson', '2026-06-13 17:00:00', 'Celebration of the marriage between Sarah and John Johnson.', 2),
('Business Seminar', '2026-07-22 13:00:00', 'Seminar on business management and strategy.', 3),
('Music Concert', '2026-08-14 20:00:00', 'Live music concert featuring popular bands.', 4),
('Garden Party', '2026-09-19 14:30:00', 'Outdoor garden party with refreshments and entertainment.', 5),
('Charity Gala Dinner', '2026-10-31 19:00:00', 'Annual fundraising gala dinner supporting local community projects.', 6);

-- Insert data into Booking table
INSERT INTO Booking (EventID, VenueID, BookingDate)
VALUES 
(1, 1, '2026-04-10 09:00:00'),
(2, 2, '2026-05-20 11:30:00'),
(3, 3, '2026-06-15 14:00:00'),
(4, 4, '2026-07-28 10:00:00'),
(5, 5, '2026-08-25 08:30:00'),
(6, 6, '2026-09-30 15:00:00');

-- Final Data Check
SELECT * FROM Booking;
SELECT * FROM Venue;
SELECT * FROM Event;

-- Dropping all Tables
DROP TABLE Booking;
DROP TABLE Event;
DROP TABLE Venue;

