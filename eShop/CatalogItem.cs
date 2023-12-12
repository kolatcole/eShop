using eShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace eShop
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int CatalogTypeId { get; set; }
        //public CatalogType CatalogType { get; set; }
        public int CatalogBrandId { get; set; }
        //public CatalogBrand CatalogBrand { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }
        public CatalogItem() { }

        // Additional code ...
    }


public static class CatalogItemEndpoints
{
	public static void MapCatalogItemEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/CatalogItem").WithTags(nameof(CatalogItem));

        group.MapGet("/", async (CatalogContext db) =>
        {
            return await db.CatalogItems.ToListAsync();
        })
        .WithName("GetAllCatalogItems")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<CatalogItem>, NotFound>> (int id, CatalogContext db) =>
        {
            return await db.CatalogItems.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is CatalogItem model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCatalogItemById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, CatalogItem catalogItem, CatalogContext db) =>
        {
            var affected = await db.CatalogItems
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, catalogItem.Id)
                  .SetProperty(m => m.Name, catalogItem.Name)
                  .SetProperty(m => m.Description, catalogItem.Description)
                  .SetProperty(m => m.Price, catalogItem.Price)
                  .SetProperty(m => m.PictureFileName, catalogItem.PictureFileName)
                  .SetProperty(m => m.PictureUri, catalogItem.PictureUri)
                  .SetProperty(m => m.CatalogTypeId, catalogItem.CatalogTypeId)
                  .SetProperty(m => m.CatalogBrandId, catalogItem.CatalogBrandId)
                  .SetProperty(m => m.AvailableStock, catalogItem.AvailableStock)
                  .SetProperty(m => m.RestockThreshold, catalogItem.RestockThreshold)
                  .SetProperty(m => m.MaxStockThreshold, catalogItem.MaxStockThreshold)
                  .SetProperty(m => m.OnReorder, catalogItem.OnReorder)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCatalogItem")
        .WithOpenApi();

        group.MapPost("/", async (CatalogItem catalogItem, CatalogContext db) =>
        {
            db.CatalogItems.Add(catalogItem);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/CatalogItem/{catalogItem.Id}",catalogItem);
        })
        .WithName("CreateCatalogItem")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, CatalogContext db) =>
        {
            var affected = await db.CatalogItems
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCatalogItem")
        .WithOpenApi();
    }
}}
