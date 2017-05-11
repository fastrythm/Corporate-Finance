SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
IF OBJECT_ID('GetBankReconciliation') IS NOT NULL
    DROP PROCEDURE GetBankReconciliation;
GO
CREATE PROCEDURE GetBankReconciliation @FromDate DATETIME,
                                    @ToDate   DATETIME
AS
     BEGIN
         SET NOCOUNT ON;
         DECLARE @Status_Cleared INT= 1;
         DECLARE @OpeningBalance DECIMAL(18, 4)= 0;

         SELECT cb.CompanyBankID,
                bak.Name Bank,
                cb.AccountNumber,
                cbt.CategoryType,
                cbt.ToCompanyBankID,
                cbt.CategoryReferenceID,
                cbt.TransactionDate,
                cbt.TransactionType,
                cbt.Amount,
			 cbt.Description,
			 (CASE WHEN cbt.TransactionStatus = 2 THEN 'QB' WHEN cbt.TransactionStatus = 3 THEN 'BANK' ELSE '' END) TransactionStatus,
                (CASE
                     WHEN cbt.CategoryType = 'Inter'
                     THEN
                (
                    SELECT tempc.ShortName+' - '+tempb.Name+' - '+tempcb.AccountNumber
                    FROM dbo.CompanyBank tempcb
                         INNER JOIN dbo.Bank tempb ON tempcb.BankID = tempb.BankID
                         INNER JOIN dbo.Company tempc ON tempcb.CompanyID = tempc.CompanyID
                    WHERE tempcb.CompanyBankID = cbt.ToCompanyBankID
                          AND tempcb.IsActive = 1
                )
                     WHEN cbt.CategoryType = 'Client'
                     THEN
                (
                    SELECT c2.Name
                    FROM dbo.Company c2
                    WHERE c2.CompanyID = cbt.CategoryReferenceID
                          AND c2.CompanyType = 'Client'
                )
                     WHEN cbt.CategoryType = 'Vendor'
                     THEN
                (
                    SELECT ven2.Name
                    FROM dbo.Company ven2
                    WHERE ven2.CompanyID = cbt.CategoryReferenceID
                          AND ven2.CompanyType = 'Vendor'
                )
                     WHEN cbt.CategoryType = 'Consultant'
                     THEN
                (
                    SELECT isnull(con.FirstName, '')+isnull(con.LastName, '')
                    FROM dbo.Consultant con
                    WHERE con.ConsultantID = cbt.CategoryReferenceID
                )
                     WHEN cbt.CategoryType = 'Auto-Debit'
                     THEN 'Auto-Debit'
                     ELSE 'Other'
                 END) CategoryReference,
         (
             SELECT ClosingBalance
             FROM dbo.fnGetBankClosingBalance(cb.CompanyBankID, @FromDate, @ToDate)
         ) QBClosingBalance,
	      (
             SELECT ClosingBalance
             FROM dbo.fnGetCompanyClosingBalance(cb.CompanyBankID, @FromDate, @ToDate)
         ) BankClosingBalance
         FROM dbo.CompanyBank cb
              INNER JOIN dbo.Bank bak ON cb.BankID = bak.BankID
              LEFT OUTER JOIN dbo.CompanyBankTransaction cbt ON cb.CompanyBankID = cbt.CompanyBankID
                                                              --  AND cbt.TransactionDate >= @FromDate
                                                             --   AND cbt.TransactionDate <= @ToDate
                                                                AND cbt.TransactionStatus != @Status_Cleared
																AND  cbt.IsDeleted = 0 
         ORDER BY cbt.TransactionStatus  DESC;
     END;
GO