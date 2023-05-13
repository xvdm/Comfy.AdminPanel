using AdminPanel.Helpers;
using AdminPanel.MediatorHandlers.Products.Categories;
using AdminPanel.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AdminPanel.MediatorHandlers.Products;
using AdminPanel.MediatorHandlers.Products.Brands;
using AdminPanel.MediatorHandlers.Products.Models;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

// ReSharper disable StringLiteralTypo

namespace AdminPanel.Models.SeedInitializers
{
    public class DatabaseSeedInitializer
    {
        public async Task Seed(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
            var mediator = scopeServiceProvider.GetService<IMediator>();

            if (userManager is null) return;

            #region Users seed

                var user = new ApplicationUser { UserName = "owner" };
                var userResult = await userManager.CreateAsync(user, "owner");
                if (userResult is not null && userResult.Succeeded) await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.Owner));

                user = new ApplicationUser { UserName = "senior" };
                userResult = await userManager.CreateAsync(user, "senior");
                if (userResult is not null && userResult.Succeeded) await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.SeniorAdministrator));

                user = new ApplicationUser { UserName = "admin" };
                userResult = await userManager.CreateAsync(user, "admin");
                if (userResult is not null && userResult.Succeeded) await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.Administrator));

            #endregion


            if (mediator is null) return;

            #region Brands seed

            //smartphones 
            var brand_apple = new Brand { Name = "Apple" };
            await mediator.Send(new CreateBrandCommand(brand_apple));

            var brand_samsung = new Brand { Name = "Samsung" };
            await mediator.Send(new CreateBrandCommand(brand_samsung));

            var brand_xiaomi = new Brand { Name = "Xiaomi" };
            await mediator.Send(new CreateBrandCommand(brand_xiaomi));
            #endregion

            #region Models seed
            //smartphones
            var model_iphone14 = new Model { Name = "iPhone 14" };
            var model_iphone13 = new Model { Name = "iPhone 13" };
            var model_iphone12 = new Model { Name = "iPhone 12" };
            var model_galaxyM14 = new Model { Name = "Galaxy M14" };
            var model_galaxyS22 = new Model { Name = "Galaxy S22" };
            var model_galaxyA54 = new Model { Name = "Galaxy А54" };
            var model_redmi12С = new Model { Name = "Redmi 12С" };
            var model_redmiNote12 = new Model { Name = "Redmi Note 12" };
            var model_redmiNote10Pro = new Model { Name = "Redmi Note 10 Pro" };

            await mediator.Send(new CreateModelCommand(model_iphone14));
            await mediator.Send(new CreateModelCommand(model_iphone13));
            await mediator.Send(new CreateModelCommand(model_iphone12));
            await mediator.Send(new CreateModelCommand(model_galaxyM14));
            await mediator.Send(new CreateModelCommand(model_galaxyS22));
            await mediator.Send(new CreateModelCommand(model_galaxyA54));
            await mediator.Send(new CreateModelCommand(model_redmi12С));
            await mediator.Send(new CreateModelCommand(model_redmiNote12));
            await mediator.Send(new CreateModelCommand(model_redmiNote10Pro));
            #endregion


            #region Categories seed

            #region Смартфони та телефони
            var mainCategory = new MainCategory { Name = "Смартфони та телефони" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            var subcategory = new Subcategory { Name = "Смартфони", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #region Товари (смартфони)
            // Apple
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_apple.Id,
                ModelId = model_iphone14.Id,
                SubcategoryId = subcategory.Id,
                Price = 38999,
                Name = "Смартфон Apple iPhone 14 128Gb Midnight",
                Description = "Більше ніж вражає."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_apple.Id,
                ModelId = model_iphone14.Id,
                SubcategoryId = subcategory.Id,
                Price = 43999,
                Name = "Смартфон Apple iPhone 14 256Gb Midnight",
                Description = "Більше ніж вражає."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_apple.Id,
                ModelId = model_iphone13.Id,
                SubcategoryId = subcategory.Id,
                Price = 33499,
                Name = "Смартфон Apple iPhone 13 128Gb Starlight",
                Description = "Більше ніж вражає."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_apple.Id,
                ModelId = model_iphone13.Id,
                SubcategoryId = subcategory.Id,
                Price = 36499,
                Name = "Смартфон Apple iPhone 13 256Gb Midnight",
                Description = "Більше ніж вражає."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_apple.Id,
                ModelId = model_iphone12.Id,
                SubcategoryId = subcategory.Id,
                Price = 29999,
                Name = "Смартфон Apple iPhone 12 128Gb Purple",
                Description = "Більше ніж вражає."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_apple.Id,
                ModelId = model_iphone12.Id,
                SubcategoryId = subcategory.Id,
                Price = 29999,
                Name = "Смартфон Apple iPhone 12 128Gb Black",
                Description = "Більше ніж вражає."
            });

            // Samsung
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_samsung.Id,
                ModelId = model_galaxyA54.Id,
                SubcategoryId = subcategory.Id,
                Price = 19999,
                Name = "Смартфон Samsung Galaxy A54 5G 6/128Gb Awesome Graphite",
                Description = "Вишуканий дизайн з яскравими відтінками Galaxy A54"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_samsung.Id,
                ModelId = model_galaxyA54.Id,
                SubcategoryId = subcategory.Id,
                Price = 21999,
                Name = "Смартфон Samsung Galaxy A54 5G 8/256Gb Light Violet",
                Description = "Вишуканий дизайн з яскравими відтінками Galaxy A54 5G"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_samsung.Id,
                ModelId = model_galaxyM14.Id,
                SubcategoryId = subcategory.Id,
                Price = 7899,
                Name = "Смартфон Samsung Galaxy M14 5G 4/64Gb Blue",
                Description = "Пориньте в яскраві деталі з Galaxy M14"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_samsung.Id,
                ModelId = model_galaxyM14.Id,
                SubcategoryId = subcategory.Id,
                Price = 8499,
                Name = "Смартфон Samsung Galaxy M14 5G 4/128Gb Dark Blue",
                Description = "Пориньте в яскраві деталі з Galaxy M14"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_samsung.Id,
                ModelId = model_galaxyS22.Id,
                SubcategoryId = subcategory.Id,
                Price = 30999,
                Name = "Смартфон Samsung Galaxy S22 8/128Gb Green",
                Description = "Смартфон, з яким кожен день незабутній."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_samsung.Id,
                ModelId = model_galaxyS22.Id,
                SubcategoryId = subcategory.Id,
                Price = 32999,
                Name = "Смартфон Samsung Galaxy S22 8/256Gb Phantom Black",
                Description = "Смартфон, з яким кожен день незабутній."
            });

            // Xiaomi
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_xiaomi.Id,
                ModelId = model_redmi12С.Id,
                SubcategoryId = subcategory.Id,
                Price = 6499,
                Name = "Смартфон Xiaomi Redmi 12C 4/128Gb Graphite Gray",
                Description = "Ультрачітка подвійна камера 50 МП і тривалий автономної роботи акумулятора гарантують, що ви ніколи не пропустите найцікавіше."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_xiaomi.Id,
                ModelId = model_redmi12С.Id,
                SubcategoryId = subcategory.Id,
                Price = 5499,
                Name = "Смартфон Xiaomi Redmi 12C 3/64Gb Graphite Gray",
                Description = "Ультрачітка подвійна камера 50 МП і тривалий автономної роботи акумулятора гарантують, що ви ніколи не пропустите найцікавіше."
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_xiaomi.Id,
                ModelId = model_redmiNote10Pro.Id,
                SubcategoryId = subcategory.Id,
                Price = 9999,
                Name = "Смартфон Xiaomi Redmi Note 10 Pro 6/128Gb Onyx Gray",
                Description = "Фотографії на вищому рівні з Xiaomi Redmi Note 10 Pro"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_xiaomi.Id,
                ModelId = model_redmiNote10Pro.Id,
                SubcategoryId = subcategory.Id,
                Price = 9999,
                Name = "Смартфон Xiaomi Redmi Note 10 Pro 6/128Gb Gradient Bronze",
                Description = "Фотографії на вищому рівні з Xiaomi Redmi Note 10 Pro"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_xiaomi.Id,
                ModelId = model_redmiNote12.Id,
                SubcategoryId = subcategory.Id,
                Price = 7999,
                Name = "Смартфон Xiaomi Redmi Note 12 4/128Gb Ice Blue",
                Description = "Смартфон для найкращих емоцій"
            });
            await mediator.Send(new CreateProductCommand
            {
                BrandId = brand_xiaomi.Id,
                ModelId = model_redmiNote12.Id,
                SubcategoryId = subcategory.Id,
                Price = 7999,
                Name = "Смартфон Xiaomi Redmi Note 12 4/128Gb Onyx Gray",
                Description = "Смартфон для найкращих емоцій"
            });
            #endregion

            subcategory = new Subcategory { Name = "Телефони", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Аксесуари для смартфонів", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Стартові пакети", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion


            #region Ноутбуки, планшети та комп'ютерна техніка
            mainCategory = new MainCategory { Name = "Ноутбуки, планшети та комп'ютерна техніка" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            subcategory = new Subcategory { Name = "Ноутбуки, комп'ютери", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Планшети", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Комплектуючі для ПК", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Електронні книги", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion


            #region Телевізори та мультимедіа
            mainCategory = new MainCategory { Name = "Телевізори та мультимедіа" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            subcategory = new Subcategory { Name = "Телевізори", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Мультимедіа та звук", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Проектори та екрани", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Аксесуари для ТВ", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion


            #region Смарт-годинники та гаджети
            mainCategory = new MainCategory { Name = "Смарт-годинники та гаджети" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            subcategory = new Subcategory { Name = "Смарт-годинники", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Фітнес-браслети", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Віртуальна реальність", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Роботи", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion


            #region Аудіо
            mainCategory = new MainCategory { Name = "Аудіо" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            subcategory = new Subcategory { Name = "Навушники", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Медіаплеєри", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "MP3-плеєри", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Диктофони", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion


            #region Ігрові консолі та геймінг
            mainCategory = new MainCategory { Name = "Ігрові консолі та геймінг" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            subcategory = new Subcategory { Name = "Ігрові консолі", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Ігри", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Аксесуари для ігрових ПК та ноутбуків", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Геймпади, джойстики, ігрові керма", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion  


            #region Фото та відео
            mainCategory = new MainCategory { Name = "Фото та відео" };
            await mediator.Send(new CreateMainCategoryCommand(mainCategory));


            subcategory = new Subcategory { Name = "Фотокамери", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Відеокамери", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Екшн-камери та стедіками", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));

            subcategory = new Subcategory { Name = "Квадрокоптери", MainCategoryId = mainCategory.Id };
            await mediator.Send(new CreateSubcategoryCommand(subcategory));
            #endregion


            //#region Техніка для кухні
            //mainCategory = new MainCategory { Name = "Техніка для кухні" };
            //createMainCategoryCommand = new CreateMainCategoryCommand(mainCategory);
            //await mediator.Send(createMainCategoryCommand);
            //#endregion


            //#region Техніка для дому
            //mainCategory = new MainCategory { Name = "Техніка для дому" };
            //createMainCategoryCommand = new CreateMainCategoryCommand(mainCategory);
            //await mediator.Send(createMainCategoryCommand);
            //#endregion


            //#region Краса і здоров'я
            //mainCategory = new MainCategory { Name = "Краса і здоров'я" };
            //createMainCategoryCommand = new CreateMainCategoryCommand(mainCategory);
            //await mediator.Send(createMainCategoryCommand);
            //#endregion


            //#region Посуд
            //mainCategory = new MainCategory { Name = "Посуд" };
            //createMainCategoryCommand = new CreateMainCategoryCommand(mainCategory);
            //await mediator.Send(createMainCategoryCommand);
            //#endregion


            //#region Дім та відпочинок
            //mainCategory = new MainCategory { Name = "Дім та відпочинок" };
            //createMainCategoryCommand = new CreateMainCategoryCommand(mainCategory);
            //await mediator.Send(createMainCategoryCommand);
            //#endregion


            //#region Інструменти і автотовари
            //mainCategory = new MainCategory { Name = "Інструменти і автотовари" };
            //createMainCategoryCommand = new CreateMainCategoryCommand(mainCategory);
            //await mediator.Send(createMainCategoryCommand);
            //#endregion

            #endregion
        }
    }
}
