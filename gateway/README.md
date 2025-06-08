Proto file used to generate classes

gateway.csproj

```C#
  <ItemGroup>
    <Protobuf Include="../big_data/Protos/bigdata.proto" GrpcServices="Client" />
```

---

---

---

# gRPC server to client to REST API

## gRPC server

if not found in DB, throws `RpcException`

```csharp
if (entity == null) throw new RpcException(new Status(StatusCode.NotFound, "Company not found"));
```

## gRPC client

Calls the above method on the server
It returns `Task<CompanyDto?>` nullable

```csharp
try {}
catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
  {
      return null;
  }
```

## REST controller

Checks if null and returns 404

```csharp
  var companyDto = await _bigDataClient.GetCompany(id);
  if (companyDto == null)
  {
      return NotFound(); // 404
  }
  return Ok(companyDto); // 200 with the DTO
```
