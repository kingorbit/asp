-- Tworzenie tabeli "Products"
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(255),
    Description VARCHAR(255),
    Price DECIMAL(10, 2)
);

-- Tworzenie tabeli "ProductionOrders"
CREATE TABLE ProductionOrders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT,
    StartDate DATE,
    EndDate DATE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Tworzenie tabeli "Machines"
CREATE TABLE Machines (
    MachineID INT IDENTITY(1,1) PRIMARY KEY,
    MachineName VARCHAR(255),
    Description VARCHAR(255)
);

-- Tworzenie tabeli "Employees"
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Position VARCHAR(255)
);
CREATE TABLE EmployeePositions (
    EmployeePositionID IDENTITY(1,1) INT PRIMARY KEY,
    Position VARCHAR(255),
    EmployeeID INT,
    AssignmentOrdersID INT,
    PositionID INT,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    FOREIGN KEY (AssignmentOrdersID) REFERENCES AssignmentOrders(AssignmentOrdersID),
    FOREIGN KEY (PositionID) REFERENCES Positions(PositionID)
);



-- Tworzenie tabeli "AssignmentOrders"
CREATE TABLE AssignmentOrders (
    AssignmentOrdersID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT,
    MachineID INT,
    EmployeeID INT,
    AssignmentDate DATE,
    FOREIGN KEY (OrderID) REFERENCES ProductionOrders(OrderID),
    FOREIGN KEY (MachineID) REFERENCES Machines(MachineID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);
