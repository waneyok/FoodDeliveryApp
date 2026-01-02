namespace FoodDeliveryApp.Models
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Restaurants.Any() || context.Categories.Any())
            {
                return; // DB has been seeded
            }

            // Add categories
            var categories = new[]
            {
                new Category { Name = "Пицца", Description = "Итальянская пицца" },
                new Category { Name = "Суши", Description = "Японская кухня" },
                new Category { Name = "Бургеры", Description = "Американская кухня" },
                new Category { Name = "Паста", Description = "Итальянская паста" },
                new Category { Name = "Салаты", Description = "Здоровое питание" },
                new Category { Name = "Напитки", Description = "Напитки и соки" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Add restaurants
            var restaurants = new[]
            {
                new Restaurant
                {
                    Name = "Pizza House",
                    Description = "Лучшая пицца в городе",
                    Address = "ул. Пиццерийная, 15",
                    Phone = "+7 (999) 123-45-67",
                    ImageUrl = "/images/pizza.jpg",
                    Rating = 4.7,
                    DeliveryFee = 150,
                    DeliveryTime = 30
                },
                new Restaurant
                {
                    Name = "Sushi Master",
                    Description = "Свежие суши и роллы",
                    Address = "ул. Японская, 22",
                    Phone = "+7 (999) 234-56-78",
                    ImageUrl = "/images/sushi.jpg",
                    Rating = 4.8,
                    DeliveryFee = 200,
                    DeliveryTime = 40
                },
                new Restaurant
                {
                    Name = "Burger King",
                    Description = "Король бургеров",
                    Address = "ул. Американская, 33",
                    Phone = "+7 (999) 345-67-89",
                    ImageUrl = "/images/burger.jpg",
                    Rating = 4.5,
                    DeliveryFee = 100,
                    DeliveryTime = 25
                }
            };
            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();

            // Add menu items
            var menuItems = new[]
            {
                // Pizza House items
                new MenuItem
                {
                    Name = "Маргарита",
                    Description = "Томатный соус, сыр моцарелла, базилик",
                    Price = 550,
                    ImageUrl = "/images/margarita.jpg",
                    RestaurantId = 1,
                    CategoryId = 1
                },
                new MenuItem
                {
                    Name = "Пепперони",
                    Description = "Томатный соус, пепперони, сыр моцарелла",
                    Price = 650,
                    ImageUrl = "/images/pepperoni.jpg",
                    RestaurantId = 1,
                    CategoryId = 1
                },
                new MenuItem
                {
                    Name = "Четыре сыра",
                    Description = "Смесь четырех сыров",
                    Price = 700,
                    ImageUrl = "/images/4cheese.jpg",
                    RestaurantId = 1,
                    CategoryId = 1
                },
                
                // Sushi Master items
                new MenuItem
                {
                    Name = "Филадельфия",
                    Description = "Лосось, сыр, огурец, авокадо",
                    Price = 850,
                    ImageUrl = "/images/philadelphia.jpg",
                    RestaurantId = 2,
                    CategoryId = 2
                },
                new MenuItem
                {
                    Name = "Калифорния",
                    Description = "Краб, авокадо, огурец",
                    Price = 750,
                    ImageUrl = "/images/california.jpg",
                    RestaurantId = 2,
                    CategoryId = 2
                },
                
                // Burger King items
                new MenuItem
                {
                    Name = "Чизбургер",
                    Description = "Говяжья котлета, сыр, овощи",
                    Price = 350,
                    ImageUrl = "/images/cheeseburger.jpg",
                    RestaurantId = 3,
                    CategoryId = 3
                },
                new MenuItem
                {
                    Name = "Биг Кинг",
                    Description = "Две говяжьи котлеты, сыр, соус",
                    Price = 450,
                    ImageUrl = "/images/bigking.jpg",
                    RestaurantId = 3,
                    CategoryId = 3
                }
            };
            context.MenuItems.AddRange(menuItems);
            context.SaveChanges();
        }
    }
}