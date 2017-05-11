 
	IF EXISTS(SELECT name FROM sys.objects WHERE name = N'fnGetCompanyClosingBalance')  DROP FUNCTION dbo.fnGetCompanyClosingBalance;
	GO
	CREATE FUNCTION dbo.fnGetCompanyClosingBalance (@CompanyBankId bigint,@FromDate datetime,@ToDate datetime)
     RETURNS @ItemTable TABLE (
	  ClosingBalance decimal(18,4)
    ) 
    AS
    BEGIN
	  
	  DECLARE @BANK_PENDING int = 3

	  INSERT INTO @ItemTable (ClosingBalance)
	  SELECT     ( (
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Receipt') AND cbt.TransactionDate < @FromDate AND cbt.TransactionStatus != @BANK_PENDING AND cbt.IsDeleted = 0 ) -
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Payment') AND cbt.TransactionDate < @FromDate AND cbt.TransactionStatus !=  @BANK_PENDING AND cbt.IsDeleted = 0 )
				 ) 
					
			 	+	
				 (
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Receipt') AND ( cbt.TransactionDate >= @FromDate AND cbt.TransactionDate <= @ToDate ) AND cbt.TransactionStatus != @BANK_PENDING AND cbt.IsDeleted = 0 ) -
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Payment') AND ( cbt.TransactionDate >= @FromDate AND cbt.TransactionDate <= @ToDate ) AND cbt.TransactionStatus !=  @BANK_PENDING AND cbt.IsDeleted = 0 )
				 ) )		 
						 
						   AS ClosingBalance
       
	  RETURN;
    END;