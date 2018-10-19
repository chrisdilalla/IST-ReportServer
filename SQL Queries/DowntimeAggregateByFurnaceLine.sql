SELECT ProcessCode, (Select [Description] from ProcessCodes Where Code = ProcessCode) as ProcessCodeName,sum([HoursDown]) as 'HoursDown',sum([MinutesDown]) as 'MinutesDown'     
  FROM InductionDowntime WHERE  
  SmallDateTime > '2018-06-01T00:00:01' and SmallDateTime< '2018-07-01T00:00:01' 
  AND LineNumber = 4
  group by ProcessCode order by ProcessCode asc


  SELECT top(1)* from InductionDowntime where SmallDateTime > '2018-06-01T00:00:01' and SmallDateTime< '2018-07-01T00:00:01' AND LineNumber = 4 order by SmallDateTime asc
  SELECT top(1)* from InductionDowntime where SmallDateTime > '2018-06-01T00:00:01' and SmallDateTime< '2018-07-01T00:00:01' AND LineNumber = 4 order by SmallDateTime desc