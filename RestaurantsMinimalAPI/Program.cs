using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantsMinimalApi.Data;
using RestaurantsMinimalApi.Models;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();  //set the allowed origin  
        });
});

// Add inmemory database
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// This needs to be disabled because Firefox is more restrictive with cors requests and blocks requests with invalid ssl certificate (the dotnet selfsign certificate isn't considered as valid)
// app.UseHttpsRedirection();
app.UseCors();

// restaurant api endpoints 
// the update and delete endpoints won't be implemented in order to simplify the example
app.MapGet("/restaurant", async (AppDbContext context) => await context.Restaurants!
.ToListAsync() is List<Restaurant> restaurants && (restaurants.Count > 0) ? Results.Ok(restaurants) : Results.NotFound())
.WithName("All Restaurants").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapGet("/restaurant/{id}", async (int id, AppDbContext context) => await context.Restaurants!
.FindAsync(id) is Restaurant restaurant ? Results.Ok(restaurant) : Results.NotFound())
.WithName("Restaurant").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

// menu item api endpoints
// the update and delete endpoints won't be implemented in order to simplify the example
app.MapGet("/menuitem", async (AppDbContext context) => await context.MenuItems!
.ToListAsync() is List<MenuItem> menuItems && (menuItems.Count > 0) ? Results.Ok(menuItems) : Results.NotFound())
.WithName("All Menu Items").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapGet("/menuitem/{id}", async (int id, AppDbContext context) => await context.MenuItems!
.FindAsync(id) is MenuItem menuItem ? Results.Ok(menuItem) : Results.NotFound())
.WithName("Menu Item").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapGet("/menuitem/restaurant/{id}", async (int id, AppDbContext context) => await context.MenuItems!
.Where(m => m.RestaurantId == id).ToListAsync() is List<MenuItem> menuItems && (menuItems.Count > 0) ? Results.Ok(menuItems) : Results.NotFound())
.WithName("All Menu Items for a Restaurant").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

// order api endpoints
app.MapGet("/order", async (AppDbContext context) => await context.Orders!
.ToListAsync() is List<Order> orders && (orders.Count > 0) ? Results.Ok(orders) : Results.NotFound())
.WithName("All Orders").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapGet("/order/{id}", async (int id, AppDbContext context) => await context.Orders!
.FindAsync(id) is Order order ? Results.Ok(order) : Results.NotFound()).WithName("Order")
.Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapPost("/order", async (Order orderInput, AppDbContext context) =>
{
    context.Orders!.Add(orderInput);
    await context.SaveChangesAsync();
    return Results.Created($"/order/{orderInput.Id}", orderInput);
}).WithName("Create Order").Produces(StatusCodes.Status201Created);

app.MapPut("/order/{id}", async (int id, Order orderInput, AppDbContext context) =>
{
    var order = await context.Orders!.FindAsync(id);
    if (order == null) return Results.NotFound();
    order.Name = orderInput.Name;
    order.Address = orderInput.Address;
    order.DistanceInKm = orderInput.DistanceInKm;
    order.OtherMentions = orderInput.OtherMentions;
    order.RestaurantId = orderInput.RestaurantId;
    context.Orders!.Update(order);
    await context.SaveChangesAsync();
    return Results.NoContent();
}).WithName("Update Order").Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);

app.MapDelete("/order/{id}", async (int id, AppDbContext context) =>
{
    var order = await context.Orders!.FindAsync(id);
    if (order == null) return Results.NotFound();
    context.Orders!.Remove(order);
    await context.SaveChangesAsync();
    return Results.NoContent();
}).WithName("Delete Order").Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);

DataSeeder.PrepPopulation(app);

app.Run();

