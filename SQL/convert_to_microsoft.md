## Contact

```
  INSERT INTO VoltoRepoBigData.dbo.ContactsLOL (
   CompanyId, Value, Type, Checked, Date
  )
  SELECT  comp_id, facebook, 3, checked, date

  FROM Volto_big_data.dbo.facebook
```

## Companies with ID

```
SET IDENTITY_INSERT [VoltoRepoBigData].[dbo].[Companiezz] ON;


INSERT INTO [VoltoRepoBigData].[dbo].[Companiezz] (
    Id, CompanyName, Country, City, FullAddress, Website, CategoryGoogle, RatingGoogle,
    RatedCount, GoogleMapsUrl, BigFishScore, Classification
)

SELECT TOP (9) [id]
      ,[company_name]
      ,[country]
      ,[city]
      ,[full_address]
      ,[website]
      ,[category_google]
      ,[rating_google]
      ,[rated_count]
      ,[google_maps_URL]
      ,[big_fish_score]
      ,[class]
  FROM [Volto_big_data].[dbo].[companies]

  SET IDENTITY_INSERT [VoltoRepoBigData].[dbo].[Companiezz] OFF;


```
