 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('GetBankTransaction') IS NOT NULL DROP PROCEDURE GetBankTransaction
GO
 
CREATE PROCEDURE GetBankTransaction
 @FromDate datetime,
 @ToDate datetime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	SET NOCOUNT ON;
	DECLARE @BANK_PENDING int = 3

	DECLARE @OpeningBalance decimal(18,4) = 0
		  SELECT cb.CompanyBankID, c.ShortName Company ,cb.AccountNumber,cbt.CategoryType,cbt.ToCompanyBankID,cbt.CategoryReferenceID, cbt.TransactionDate,
	   cbt.TransactionType,  cbt.Amount, 
	   (
		CASE WHEN cbt.CategoryType = 'Inter' THEN (SELECT tempc.ShortName + ' - ' + tempb.Name + ' - '+ tempcb.AccountNumber from dbo.CompanyBank tempcb 
										    INNER JOIN dbo.Bank tempb ON tempcb.BankID = tempb.BankID  
										    INNER JOIN dbo.Company tempc ON tempcb.CompanyID = tempc.CompanyID
											 WHERE tempcb.CompanyBankID = cbt.ToCompanyBankID AND tempcb.IsActive = 1 )
											  
		    WHEN cbt.CategoryType = 'Client' THEN (SELECT c2.Name FROM dbo.Company c2 WHERE c2.CompanyID = cbt.CategoryReferenceID AND c2.CompanyType = 'Client')
		    WHEN cbt.CategoryType = 'Vendor' THEN (SELECT ven2.Name FROM dbo.Company ven2 WHERE ven2.CompanyID = cbt.CategoryReferenceID AND ven2.CompanyType = 'Vendor')
		    WHEN cbt.CategoryType = 'Consultant' THEN (SELECT isnull(con.FirstName,'') + isnull(con.LastName,'') FROM dbo.Consultant con WHERE con.ConsultantID = cbt.CategoryReferenceID)
		    WHEN cbt.CategoryType = 'Auto-Debit' THEN 'Auto-Debit'
		    ELSE 'Other' END
	   ) CategoryReference,
		     ( SELECT * from dbo.fnGetCompanyOpeningBalance(cb.CompanyBankID,@FromDate) ) OpeningBalance,
			  ( SELECT * from dbo.fnGetCompanyClosingBalance(cb.CompanyBankID,@FromDate, @ToDate) ) ClosingBalance
	   FROM dbo.CompanyBank cb
	   INNER JOIN dbo.Company c ON cb.CompanyID = c.CompanyID
	   LEFT OUTER JOIN dbo.CompanyBankTransaction cbt ON cb.CompanyBankID = cbt.CompanyBankID
	   AND  cbt.TransactionDate >= @FromDate AND  cbt.TransactionDate <= @ToDate AND cbt.TransactionStatus != @BANK_PENDING
	   ORDER BY (case when cbt.CategoryType ='Inter' then 1 
	   when cbt.CategoryType ='Vendor' then 2 
	   when cbt.CategoryType ='Client' then 3 when cbt.CategoryType ='Consultant' then 4 when cbt.CategoryType ='Auto-Debit' then 5 else 6 end)  Desc
END
GO
