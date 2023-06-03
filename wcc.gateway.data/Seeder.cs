using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.data
{
    public static class Seeder
    {

        // This is purely for a demo, don't anything like this in a real application!
        public static void Seed(this ApplicationDbContext context)
        {
            if (!context.News.Any())
            {
                var news = new News()
                {
                    Name = "Prayer for Ukraine",
                    ImageUrl = "https://www.cossacks3.com/src/img/UA-FLAG.jpg",
                    Description = "The people of Ukraine continue to defend their Independence against the armed forces of Russian Federation.\r\nThey started to bomb our cities without warning, their soldiers intruded from north, east, and south. None of Ukraine&#39;s citizens wanted war, few were ready for it, but we will stand for our freedom to the last.\r\n\r\nWe are eternally grateful to our fans, friends and colleagues who’ve already joined the common cause of stopping Russian aggression.\r\nWe are happy to be a part of such a united community.\r\n\r\nBut yet the war is not over, and the enemy is not stopping. Right now our families and friends are either trying to find shelter from bombings or are actively assisting those who has already suffered from the occupants.\r\n\r\n\r\n\r\nWe need all the help we can get.\r\n\r\n\r\n\r\nUNITED24\r\n\r\nA charitable fundraising platform launched by the President of Ukraine.\r\n\r\nhttps://u24.gov.ua/\r\n\r\n\r\n\r\nThrough pain, death, war, fear and inhuman cruelty, Ukraine will persevere.\r\nAs it always does!"
                };
                context.News.Add(news);
                context.SaveChanges();
            }
            if (!context.Tournaments.Any())
            {
                var tournament = new Tournament()
                {
                    Name = "Lords of Kingdom",
                    Description = "&lt;p&gt;Турнір розраховано на 16 учасників у основному раунді. Якшо кількість учасників буде перевищувати заплановану кількість, будуть введені кваліфікаційні матчі.&lt;br /&gt;\r\nОсновний раунд передбачає гру 1 на 1 до двох перемог (максимально три гри).&lt;br /&gt;\r\nКваліфікаційний матч передбачає гру 1 на 1 (можливо тільки одна гра).&lt;/p&gt;\r\n\r\n&lt;p&gt;&lt;strong&gt;Країна:&lt;/strong&gt;&amp;nbsp; Анлія&lt;/p&gt;\r\n\r\n&lt;p&gt;&lt;strong&gt;Основні правила:&lt;/strong&gt;&lt;/p&gt;\r\n\r\n&lt;table border=&quot;none&quot; cellpadding=&quot;1&quot; cellspacing=&quot;1&quot; style=&quot;width:auto&quot;&gt;\r\n\t&lt;tbody&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:151px&quot;&gt;Тип мапи&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:335px&quot;&gt;Суходіл&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:151px&quot;&gt;Вид рельєфу&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:335px&quot;&gt;Рівнина&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:151px&quot;&gt;Початкові ресурси&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:335px&quot;&gt;Тисячі&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:151px&quot;&gt;Ресурси&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:335px&quot;&gt;Багато&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:151px&quot;&gt;Розмір карти&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:335px&quot;&gt;Маленький&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t&lt;/tbody&gt;\r\n&lt;/table&gt;\r\n\r\n&lt;p&gt;&lt;strong&gt;Додаткові опції:&lt;/strong&gt;&lt;/p&gt;\r\n\r\n&lt;table border=&quot;none&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot; style=&quot;width:auto&quot;&gt;\r\n\t&lt;tbody&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Стартові опції&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;За замовчуванням&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Опції монгольф&amp;#39;єра&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;За замовчуванням&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Гармати&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;За замовчуванням&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Час миру&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;15&amp;nbsp;хв&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Опції 18 століття&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;За замовчуванням&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Захоплення&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;Без захоплення селян&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Дип. центр і ринок&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;Типові налаштування&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Союзники&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;Поряд&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Ліміт населення&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;Без обмеження&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t\t&lt;tr&gt;\r\n\t\t\t&lt;td style=&quot;width:149px&quot;&gt;Швидкість гри&lt;/td&gt;\r\n\t\t\t&lt;td style=&quot;width:337px&quot;&gt;Дуже швидка&lt;/td&gt;\r\n\t\t&lt;/tr&gt;\r\n\t&lt;/tbody&gt;\r\n&lt;/table&gt;\r\n\r\n&lt;p&gt;....&lt;/p&gt;\r\n\r\n&lt;p&gt;&amp;nbsp;&lt;/p&gt;\r\n",
                    ImageUrl = "https://assets.rockpapershotgun.com/images/16/sep/cos3.jpg",
                    CountPlayers = 16,
                    DateStart = new DateTime(2023, 3, 16),
                    DateCreated = new DateTime(2023, 3, 4)
                };
                context.Tournaments.Add(tournament);
                context.SaveChanges();
            }
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role() { Name = "Admin" });
                context.Roles.Add(new Role() { Name = "Manager" });
                context.Roles.Add(new Role() { Name = "User" });
                context.SaveChanges();
            }
            if (!context.TournamentTypes.Any())
            {
                context.TournamentTypes.Add(new TournamentType() { Name = "Rating" });
                context.TournamentTypes.Add(new TournamentType() { Name = "Olympic's" });
                context.TournamentTypes.Add(new TournamentType() { Name = "Switzerland's" });
                context.SaveChanges();
            }
        }
    }
}
