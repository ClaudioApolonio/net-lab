CREATE PROCEDURE P_TOTAL_IMPOSTOS_CFOP
AS 
Select Cfop,
	   Sum(BaseIcms) as TotalBaseIcms, 
	   Sum(ValorIcms) as TotalIcms, 
	   Sum(BaseIpi) as TotalBaseIpi, 
	   Sum(ValorIpi) as TotalIpi 
from [dbo].[NotaFiscalItem] 
group by Cfop