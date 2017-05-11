 
	IF EXISTS(SELECT name FROM sys.objects WHERE name = N'fnGetCompanyOpeningBalance')  DROP FUNCTION dbo.fnGetCompanyOpeningBalance;
	GO
	CREATE FUNCTION dbo.fnGetCompanyOpeningBalance (@CompanyBankId bigint,@FromDate datetime)
     RETURNS @ItemTable TABLE (
	  OpeningBalance decimal(18,4)
    ) 
    AS
    BEGIN

	  DECLARE @BANK_PENDING int = 3

	  INSERT INTO @ItemTable (OpeningBalance)
	  SELECT        (
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Receipt') AND cbt.TransactionDate < @FromDate AND cbt.TransactionStatus != @BANK_PENDING AND  cbt.IsDeleted = 0 ) -
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Payment') AND cbt.TransactionDate < @FromDate AND cbt.TransactionStatus != @BANK_PENDING AND  cbt.IsDeleted = 0 )
						 )  AS OpeningBalance
        
	  RETURN;
    END;