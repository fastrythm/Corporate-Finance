 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('GetBankTransactionByPaymentTypes') IS NOT NULL DROP PROCEDURE GetBankTransactionByPaymentTypes
GO
 
CREATE PROCEDURE GetBankTransactionByPaymentTypes
 @FromDate datetime,
 @ToDate datetime ,
 @PaymentTypes VARCHAR(MAX)
AS
BEGIN
	 
	SET NOCOUNT ON;
  DECLARE @BANK_PENDING int = 3

SELECT cbt.TransactionDate,b.Name BankName,cb.AccountNumber,cbt.TransactionType,cbt.ReceiptNumber,cbt.PaymentType, cbt.Description, cbt.Amount FROM dbo.CompanyBankTransaction cbt
INNER JOIN  dbo.CompanyBank cb ON cbt.CompanyBankID = cb.CompanyBankID
INNER JOIN dbo.Bank b ON cb.BankID = b.BankID
WHERE cbt.PaymentType IN (SELECT val FROM   dbo.fnSplit(@PaymentTypes, ',')) AND ( cbt.TransactionDate >= @FromDate AND  cbt.TransactionDate <= @ToDate) AND cbt.TransactionStatus != @BANK_PENDING
 AND cbt.IsDeleted = 0

 
END
GO
