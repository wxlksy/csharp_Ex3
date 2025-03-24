using System;

namespace ShadowMageBattle
{
    /// <summary>
    /// Основной класс программы, который запускает битву с боссом.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем экземпляры игрока и босса
            Player player = new Player(500);
            Boss boss = new Boss(1000, 50);

            // Создаем экземпляр класса для управления битвой
            BattleManager battleManager = new BattleManager(player, boss);
            battleManager.StartBattle();
        }
    }

    /// <summary>
    /// Класс для управления битвой между игроком и боссом.
    /// </summary>
    class BattleManager
    {
        private Player player;
        private Boss boss;

        public BattleManager(Player player, Boss boss)
        {
            this.player = player;
            this.boss = boss;
        }

        /// <summary>
        /// Метод для начала битвы.
        /// </summary>
        public void StartBattle()
        {
            while (player.IsAlive && boss.IsAlive)
            {
                Console.WriteLine($"Ваше здоровье: {player.Health}");
                Console.WriteLine($"Здоровье босса: {boss.Health}");

                // Выбор заклинания
                Console.WriteLine("Выберите заклинание:");
                Console.WriteLine("1. Рашамон (Отнимает 100 хп игроку)");
                Console.WriteLine("2. Хуганзакура (Наносит 100 ед. урона, требует Рашамон)");
                Console.WriteLine("3. Межпространственный разлом (Восстанавливает 250 хп)");
                Console.WriteLine("4. Теневой удар (Наносит 75 ед. урона)");
                Console.WriteLine("5. Теневая защита (Снижает урон босса на 50% на один ход)");

                int choice = int.Parse(Console.ReadLine());

                // Выполнение заклинания
                switch (choice)
                {
                    case 1:
                        player.CastRashamon();
                        break;
                    case 2:
                        player.CastHuganzakura(boss);
                        break;
                    case 3:
                        player.CastDimensionalRift();
                        break;
                    case 4:
                        player.CastShadowStrike(boss);
                        break;
                    case 5:
                        player.CastShadowDefense();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                // Атака босса
                if (player.IsAlive)
                {
                    boss.Attack(player);
                }
            }

            // Вывод результата битвы
            if (player.IsAlive)
            {
                Console.WriteLine("Вы победили босса!");
            }
            else
            {
                Console.WriteLine("Босс победил вас!");
            }
        }
    }

    /// <summary>
    /// Класс, представляющий игрока.
    /// </summary>
    class Player
    {
        public int Health { get; private set; }
        public bool IsAlive => Health > 0;
        private bool shadowSpiritSummoned;
        private bool shadowDefenseActive;

        public Player(int initialHealth)
        {
            Health = initialHealth;
            shadowSpiritSummoned = false;
            shadowDefenseActive = false;
        }

        /// <summary>
        /// Заклинание Рашамон.
        /// </summary>
        public void CastRashamon()
        {
            Health -= 100;
            shadowSpiritSummoned = true;
            Console.WriteLine("Вы призвали теневого духа!");
        }

        /// <summary>
        /// Заклинание Хуганзакура.
        /// </summary>
        public void CastHuganzakura(Boss boss)
        {
            if (shadowSpiritSummoned)
            {
                boss.TakeDamage(100);
                Console.WriteLine("Вы нанесли 100 ед. урона боссу!");
            }
            else
            {
                Console.WriteLine("Сначала призовите теневого духа!");
            }
        }

        /// <summary>
        /// Заклинание Межпространственный разлом.
        /// </summary>
        public void CastDimensionalRift()
        {
            Health += 250;
            Console.WriteLine("Вы восстановили 250 хп!");
        }

        /// <summary>
        /// Заклинание Теневой удар.
        /// </summary>
        public void CastShadowStrike(Boss boss)
        {
            boss.TakeDamage(75);
            Console.WriteLine("Вы нанесли 75 ед. урона боссу!");
        }

        /// <summary>
        /// Заклинание Теневая защита.
        /// </summary>
        public void CastShadowDefense()
        {
            shadowDefenseActive = true;
            Console.WriteLine("Вы активировали теневую защиту!");
        }

        /// <summary>
        /// Метод для получения урона.
        /// </summary>
        public void TakeDamage(int damage)
        {
            if (shadowDefenseActive)
            {
                damage /= 2;
                shadowDefenseActive = false;
            }
            Health -= damage;
        }
    }

    /// <summary>
    /// Класс, представляющий босса.
    /// </summary>
    class Boss
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public bool IsAlive => Health > 0;

        public Boss(int initialHealth, int damage)
        {
            Health = initialHealth;
            Damage = damage;
        }

        /// <summary>
        /// Метод для атаки игрока.
        /// </summary>
        public void Attack(Player player)
        {
            player.TakeDamage(Damage);
            Console.WriteLine($"Босс атакует и наносит вам {Damage} ед. урона!");
        }

        /// <summary>
        /// Метод для получения урона.
        /// </summary>
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}

