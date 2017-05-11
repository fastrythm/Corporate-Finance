 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('GetCompanyReconciliation') IS NOT NULL DROP PROCEDURE GetCompanyReconciliation
GO
 
CREATE PROCEDURE GetCompanyReconciliation
 @FromDate datetime,
 @ToDate datetime 
AS
BEGIN
	 
	SET NOCOUNT ON;

	  DECLARE @BANK_PENDING int = 3
	  SELECT c.ShortName, cbt.TransactionType,  IsNull(cbt.Amount,0) Amount, ISNULL((SELECT tempc.ShortName  from dbo.CompanyBank tempcb 
										       INNER JOIN dbo.Company tempc ON tempcb.CompanyID = tempc.CompanyID
											 WHERE tempcb.CompanyBankID = cbt.ToCompanyBankID AND tempcb.IsActive = 1 ),c.ShortName) ToComapnyShortName
	   FROM dbo.Company c
	   INNER JOIN dbo.CompanyBank cb ON c.CompanyID = cb.CompanyID
	   LEFT OUTER JOIN dbo.CompanyBankTransaction cbt ON  cb.CompanyBankID = cbt.CompanyBankID
 
	   AND cbt.CategoryType = 'Inter' AND cbt.TransactionStatus != @BANK_PENDING AND  cbt.TransactionDate >= @FromDate AND  cbt.TransactionDate <= @ToDate
	   AND cbt.IsDeleted = 0
        ORDER BY c.ShortName


 
END
GO
