﻿using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace YarBar.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //CreateDb();
        }

        private void CreateDb()
        {
            Database.EnsureCreated();
            var bar = new PlaceType { Name = "Бар" };
            var restautant = new PlaceType { Name = "Ресторан" };
            var cafe = new PlaceType { Name = "Кафе" };

            var place1 = new Place { Name = "Брюгге", PlaceType = restautant, Description = "Бельгийский ресторан Брюгге приглашает своих посетителей окунуться в атмосферу любимых вкусов. Здесь можно сытно перекусить как одному, так и в компании друзей или семьи. Для вас приготовили щедрое меню с богатым выбором пива и всевозможными закусками, вкусно дополняющими пенный напиток. Здесь можно выпить с друзьями под ненавязчивую музыку и в целом провести приятный вечер. А истинные фанаты спорта оценят трансляции матчей. Помимо вкусной еды здесь можно насладиться винами из разных уголков мира. Искусство сомелье недаром высоко ценят в современном обществе. Умение правильно дегустировать этот напиток часто позволяет полностью раскрыть его вкусовой букет. А дополнить вкус вина помогут наши закуски. Если вам захотелось перекусить на свежем воздухе, особенно в теплый денек, то здесь открыта удобная терраса. Для летнего периода в заведении имеется специальное меню, которое прекрасно освежает в жаркую погоду. Летние кафе отличаются быстрым приготовлением блюд, что сокращает время ожидания заказа. Тут же вы можете заказать блюда с доставкой на дом, оставить ребенка в детской комнате. Всех гостей ждём по адресу: Собинова, 43а. Для гостей заведение работает Пн-чт, вс: 12:00 - 24:00; пт-сб: 12:00 - 02:00." };
            var place2 = new Place { Name = "Хмель и гриль", PlaceType = bar, Description = "Пивной бар Хмель & гриль приглашает своих посетителей окунуться в атмосферу любимых вкусов. В истинных традициях бара для вас приготовили щедрое меню с богатым выбором пива и всевозможными закусками, вкусно дополняющими пенный напиток. Здесь можно выпить с друзьями под ненавязчивую музыку и в целом провести приятный вечер. Здесь же вы сможете отведать сэндвичи и другие американские радости, приготовленные на свежевыпеченном хлебе. Благодаря возможности самому выбирать подливы и начинки, вы с легкостью можете приготовить бургер, который подходит вам. Для вас и вашей компании повара приготовят блюда американской, европейской кухни. Тут же вы можете заказать блюда с доставкой на дом. Всех гостей ждём по адресу: Суркова, 18. Для гостей заведение работает Пн-чт, вс: 12:00 - 24:00; пт-сб: 12:00 - 02:00." };
            var place3 = new Place { Name = "Манеки", PlaceType = cafe, Description = "Вок-кафе «Манеки» — настоящий оазис и уголок паназиатской кухни прямо в центре города! Мы отобрали самые лучшие рецепты и технологии приготовления , чтобы радовать яркими вкусами и необычными блюдами. Меню кафе сочетает в себе самые интересные блюда паназиатской кухни, приготовленные из свежих продуктов с использованием разнообразных экзотических специй и соусов — суши, роллы, закуски, салаты, супы, блюда на воке и десерты в авторском исполнении. Наш персонал с радостью поможет Вам определиться с выбором блюд и отправиться в удивительное гастрономическое путешествие! В барной карте кафе представлены разнообразные алкогольные и безалкогольные напитки, согревающие коктейли, пунши и домашние лимонады. Уютный интерьер, мягкие древесные оттенки и большое количество зелени перенесут гостей в мир спокойствия и гармонии. Ждём Вас по адресу ул. Свободы, д. 27 каждый день с 12:00 до 23:00." };
            var place4 = new Place { Name = "Хинкали хаус", PlaceType = restautant, Description = " Гостям «Хинкали Хаус» предлагают попробовать блюда грузинской кухни. Например, можно заказать 5 видов хинкали: со свининой и говядиной, с ягненком, с сыром сулугуни, с белым мясом цыпленка и с картошкой и грибами. Также в меню представлены такие традиционные блюда Грузии, как шашлык и люля, хашлама, долма, лобио, аджабсандал, кучмачи, чахохбили и хачапури. Из десертов имеются морковно-апельсиновый торт, пахлава, чурчхела и мацони, а из напитков — широкий выбор вина, пива, крепкого алкоголя и безалкогольных напитков. В «Хинкали Хаус» днем можно провести бизнес-ланч, а также заказать блюда из меню с доставкой на дом или в офис. Ресторан расположен по адресу Чкалова, 2 и рад принимать гостей Пн-Чт 11:00 – 23:00 Пт-Сб 11:00 – 02:00 Вс 11:00 – 00:00." };
            var place5 = new Place { Name = "Багет, паштет и жёлтый плед", PlaceType = cafe, Description = "Уютное семейное кафе со знаменитыми мидиями и винтажным колоритом. Мягкие желтые пледы для прохладных вечеров. У нас есть летняя веранда, где в солнечный день можно скрыться от палящего солнца и провести время на свежем воздухе. Мы расположены по адресу ул. Собинова, 41Б и работаем Пн — Чт с 9:00 - 23:00 Пт — Сб с 9:00 - 1:00 Вс с 9:00 - 23:00." };
            var place6 = new Place { Name = "Plan B", PlaceType = bar, Description = "В нашем меню представлены блюда европейской, японской, паназиатской кухни от классики до оригинальных авторских блюд. Многообразие быстрых закусок удивит завсегдатая баров и пабов, а основные блюда приятно удивят самых взыскательных гостей. В нашей барной карте представлено более 50 классических и авторских коктейлей на любой вкус. От легендарных Лонг-Айленд Айс ти и Мохито до авторских шотов и экстримов — все это Бар Коктейль. Расположен по адресу ул. Кирова, 5/23, работает круглосуточно." };
            var place7 = new Place { Name = "Союз писателей", PlaceType = cafe, Description = "Уютное бистро в центре Ярославля, которое расположилось в здании с многолетней историей, некогда служившем местом сбора российских писателей. Современный интерьер, сочетающийся с первозданными деталями, которые вобрали в себя дух этого места, делает Союз писателей удивительным. В наших винных шкафах прячутся всемирно известные вина и редкие локальные автохтоны. Французские Пино нуары соседствуют со словенскими оранжами, Шампань - с пет-натами из Ламбруско. Мы найдём, чем вас удивить! Большую часть блюд мы готовим в особой помпейской печи. Она собиралась вручную. Глина, натуральный камень и температура около 300 градусов помогают готовить очень быстро и сохранять естественный вкус продуктов. За еду отвечают молодые и амбициозные шеф Николай Мазин и су-шеф Михаил Ларин. В их меню знакомые с детства продукты легко соседствуют необычными вкусами и сочетаниями. Особое внимание стоит уделить сезонному меню. Ждём гостей по адрсу Терешковой, 5 Вс - Чт 8:30 - 00:00 Пт, Сб 8:30 - 02:00." };

            PlaceTypes.AddRange(new[] { bar, restautant, cafe });
            Places.AddRange(new[] { place1, place2, place3, place4, place5, place6, place7 });
            SaveChanges();
        }
    }
}
