CREATE TABLE Customers (
   Id UUID PRIMARY KEY NOT NULL ,
   Name TEXT NOT NULL,
   Email TEXT NOT NULL
);

CREATE TABLE Accounts (
    Id UUID PRIMARY KEY NOT NULL ,
    CustomerId UUID NOT NULL,
    AccountNumber TEXT NOT NULL,
    Balance DECIMAL(18,2) NOT NULL,
    Type INT NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE Transactions (
    Id UUID PRIMARY KEY NOT NULL ,
    AccountId UUID NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Description TEXT NULL,
    TransactionDate TIMESTAMPTZ NOT NULL,
    Type INT NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);