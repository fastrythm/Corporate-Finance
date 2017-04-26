 
	IF EXISTS(SELECT name FROM sys.objects WHERE name = N'fnGetBankClosingBalance')  DROP FUNCTION dbo.fnGetBankClosingBalance;
	GO
	CREATE FUNCTION dbo.fnGetBankClosingBalance (@CompanyBankId bigint,@FromDate datetime,@ToDate datetime)
     RETURNS @ItemTable TABLE (
	  ClosingBalance decimal(18,4)
    ) 
    AS
    BEGIN
	  
	  DECLARE @QB_PENDING int = 2

	  INSERT INTO @ItemTable (ClosingBalance)
	  SELECT     ( (
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Receipt') AND cbt.TransactionDate < @FromDate AND cbt.TransactionStatus != @QB_PENDING ) -
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Payment') AND cbt.TransactionDate < @FromDate AND cbt.TransactionStatus !=  @QB_PENDING )
				 ) 
					
			 	+	
				 (
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Receipt') AND ( cbt.TransactionDate >= @FromDate AND cbt.TransactionDate <= @ToDate ) AND cbt.TransactionStatus != @QB_PENDING ) -
                             (SELECT        ISNULL(SUM(Amount), 0) AS Expr1
                               FROM            dbo.CompanyBankTransaction AS cbt
                               WHERE        (CompanyBankID = @CompanyBankId) AND (TransactionType = 'Payment') AND ( cbt.TransactionDate >= @FromDate AND cbt.TransactionDate <= @ToDate ) AND cbt.TransactionStatus !=  @QB_PENDING )
				 ) )		 
						 
						   AS ClosingBalance
       
	  RETURN;
    END;