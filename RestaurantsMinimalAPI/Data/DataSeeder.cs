using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestaurantsMinimalApi.Data;
using RestaurantsMinimalApi.Models;

public class DataSeeder
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

    }

    private static void SeedData(AppDbContext? context)
    {
        if (context?.Restaurants != null && context?.MenuItems != null && context?.Orders != null)
        {
            // Seed restaurants
            if (!context.Restaurants.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Restaurants.AddRange(
                    new Restaurant()
                    {
                        Id = 1,
                        Name = "PizzaHat",
                        Schedule = "08:00-16:00",
                        MinimumOrderPrice = 10M,
                        StdDeliveryPrice = 5M,
                        ExtraDeliveryFeePerKm = 1.2M,
                        MaximumDistanceStdDelivery = 100,
                    },
                    new Restaurant()
                    {
                        Id = 2,
                        Name = "PizzaRomero",
                        Schedule = "09:00-19:00",
                        MinimumOrderPrice = 10M,
                        StdDeliveryPrice = 6.5M,
                        ExtraDeliveryFeePerKm = 1.4M,
                        MaximumDistanceStdDelivery = 100,
                    },
                    new Restaurant()
                    {
                        Id = 3,
                        Name = "Pizza24",
                        Schedule = "00:00-00:00",
                        MinimumOrderPrice = 10M,
                        StdDeliveryPrice = 10M,
                        ExtraDeliveryFeePerKm = 2M,
                        MaximumDistanceStdDelivery = 100,
                    },
                    new Restaurant()
                    {
                        Id = 4,
                        Name = "AllAroungPizza",
                        Schedule = "10:00-21:00",
                        MinimumOrderPrice = 10M,
                        StdDeliveryPrice = 6M,
                        ExtraDeliveryFeePerKm = 1.6M,
                        MaximumDistanceStdDelivery = 100,
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have restaurant data");
            }

            // Seed menu items
            if (!context.MenuItems.Any())
            {
                Console.WriteLine("--> Seeding menu items Data...");

                context.MenuItems.AddRange(
                    new MenuItem()
                    {
                        Id = 1,
                        Name = "Pizza Margerita",
                        Description = "Vivamus a varius nisl. Ut a bibendum risus, in porta urna. Vivamus consequat tempus augue, vel pretium nunc lobortis in. Pellentesque aliquet, risus ut sollicitudin faucibus, nisl dui pharetra ante, id imperdiet diam diam ut dui. Etiam efficitur condimentum ante, id semper dui pretium in. Vestibulum nec risus in urna tempor elementum. Cras tristique dui nec aliquam faucibus. Nullam blandit mauris mauris, sed tincidunt eros porttitor vel. Donec hendrerit velit ligula, ac dapibus ligula blandit quis. Sed eu ante at est tristique accumsan at id ligula. Phasellus et leo sed eros cursus pellentesque ac vel leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam vel sem rhoncus, varius lorem eu, pharetra massa. Nulla tincidunt diam a nisi consequat, vel tincidunt est blandit. Quisque laoreet sem nibh, id consectetur erat vulputate ac.",
                        Price = 12,
                        RestaurantId = 1
                    },
                    new MenuItem()
                    {
                        Id = 2,
                        Name = "Pizza Diavola",
                        Description = "Vivamus a varius nisl.Ut a bibendum risus, in porta urna.Vivamus consequat tempus augue, vel pretium nunc lobortis in. Pellentesque aliquet, risus ut sollicitudin faucibus, nisl dui pharetra ante, id imperdiet diam diam ut dui.Etiam efficitur condimentum ante, id semper dui pretium in. Vestibulum nec risus in urna tempor elementum.Cras tristique dui nec aliquam faucibus.Nullam blandit mauris mauris, sed tincidunt eros porttitor vel.Donec hendrerit velit ligula, ac dapibus ligula blandit quis.Sed eu ante at est tristique accumsan at id ligula.Phasellus et leo sed eros cursus pellentesque ac vel leo.Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam vel sem rhoncus, varius lorem eu, pharetra massa.Nulla tincidunt diam a nisi consequat, vel tincidunt est blandit.Quisque laoreet sem nibh, id consectetur erat vulputate ac.",
                        Price = 12,
                        RestaurantId = 2
                    },
                    new MenuItem()
                    {
                        Id = 3,
                        Name = "Pizza Canibale",
                        Description = "Vivamus a varius nisl. Ut a bibendum risus, in porta urna. Vivamus consequat tempus augue, vel pretium nunc lobortis in. Pellentesque aliquet, risus ut sollicitudin faucibus, nisl dui pharetra ante, id imperdiet diam diam ut dui. Etiam efficitur condimentum ante, id semper dui pretium in. Vestibulum nec risus in urna tempor elementum. Cras tristique dui nec aliquam faucibus. Nullam blandit mauris mauris, sed tincidunt eros porttitor vel. Donec hendrerit velit ligula, ac dapibus ligula blandit quis. Sed eu ante at est tristique accumsan at id ligula. Phasellus et leo sed eros cursus pellentesque ac vel leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam vel sem rhoncus, varius lorem eu, pharetra massa. Nulla tincidunt diam a nisi consequat, vel tincidunt est blandit. Quisque laoreet sem nibh, id consectetur erat vulputate ac.",
                        Price = 14,
                        RestaurantId = 3
                    },
                    new MenuItem()
                    {
                        Id = 4,
                        Name = "Boiled eggs",
                        Description = "Vivamus a varius nisl. Ut a bibendum risus, in porta urna. Vivamus consequat tempus augue, vel pretium nunc lobortis in. Pellentesque aliquet, risus ut sollicitudin faucibus, nisl dui pharetra ante, id imperdiet diam diam ut dui. Etiam efficitur condimentum ante, id semper dui pretium in. Vestibulum nec risus in urna tempor elementum. Cras tristique dui nec aliquam faucibus. Nullam blandit mauris mauris, sed tincidunt eros porttitor vel. Donec hendrerit velit ligula, ac dapibus ligula blandit quis. Sed eu ante at est tristique accumsan at id ligula. Phasellus et leo sed eros cursus pellentesque ac vel leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam vel sem rhoncus, varius lorem eu, pharetra massa. Nulla tincidunt diam a nisi consequat, vel tincidunt est blandit. Quisque laoreet sem nibh, id consectetur erat vulputate ac.",
                        Price = 4,
                        RestaurantId = 1
                    },
                    new MenuItem()
                    {
                        Id = 5,
                        Name = "Pizza Prosciuto e Fungi",
                        Description = "Vivamus a varius nisl. Ut a bibendum risus, in porta urna. Vivamus consequat tempus augue, vel pretium nunc lobortis in. Pellentesque aliquet, risus ut sollicitudin faucibus, nisl dui pharetra ante, id imperdiet diam diam ut dui. Etiam efficitur condimentum ante, id semper dui pretium in. Vestibulum nec risus in urna tempor elementum. Cras tristique dui nec aliquam faucibus. Nullam blandit mauris mauris, sed tincidunt eros porttitor vel. Donec hendrerit velit ligula, ac dapibus ligula blandit quis. Sed eu ante at est tristique accumsan at id ligula. Phasellus et leo sed eros cursus pellentesque ac vel leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam vel sem rhoncus, varius lorem eu, pharetra massa. Nulla tincidunt diam a nisi consequat, vel tincidunt est blandit. Quisque laoreet sem nibh, id consectetur erat vulputate ac.",
                        Price = 4,
                        RestaurantId = 4
                    },
                    new MenuItem()
                    {
                        Id = 6,
                        Name = "Pizza QuatroFromagi",
                        Description = "Vivamus a varius nisl. Ut a bibendum risus, in porta urna. Vivamus consequat tempus augue, vel pretium nunc lobortis in. Pellentesque aliquet, risus ut sollicitudin faucibus, nisl dui pharetra ante, id imperdiet diam diam ut dui. Etiam efficitur condimentum ante, id semper dui pretium in. Vestibulum nec risus in urna tempor elementum. Cras tristique dui nec aliquam faucibus. Nullam blandit mauris mauris, sed tincidunt eros porttitor vel. Donec hendrerit velit ligula, ac dapibus ligula blandit quis. Sed eu ante at est tristique accumsan at id ligula. Phasellus et leo sed eros cursus pellentesque ac vel leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam vel sem rhoncus, varius lorem eu, pharetra massa. Nulla tincidunt diam a nisi consequat, vel tincidunt est blandit. Quisque laoreet sem nibh, id consectetur erat vulputate ac.",
                        Price = 4,
                        RestaurantId = 4
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have menu item data");
            }

        }

    }

}