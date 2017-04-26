IF EXISTS(SELECT name FROM sys.objects WHERE name = N'fnSplit')  DROP FUNCTION dbo.fnSplit;
GO
Create FUNCTION dbo.fnSplit(
@sInputList VARCHAR(max) -- List of delimited items
 , @sDelimiter VARCHAR(8000) = ',' -- delimiter that separates items
 ) RETURNS @List TABLE (val VARCHAR(max))

BEGIN
 DECLARE @sItem VARCHAR(max)
 WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
  BEGIN
    SELECT
     @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX
      (@sDelimiter,@sInputList,0)-1))),  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX
      (@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))

  IF LEN(@sItem) > 0
    INSERT INTO @List SELECT @sItem
  END

 IF LEN(@sInputList) > 0
     INSERT INTO @List SELECT @sInputList -- Put the last item in
     RETURN
  END
GO