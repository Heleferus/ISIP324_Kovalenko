using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    public static class RandomHelper
    {
        public static Random rand = new Random();
    }

    public class Game
    {
        List<Weapon> weapons = new List<Weapon> 
        { 
           new Weapon(10, 0.1m, "Обычный меч"),
           new Weapon(15, 0.15m, "РУСкий топор"),
           new Weapon(20, 0.2m, "Палочка выручалочка"),
           new Weapon(25, 0.25m, "Мороженое 48 копеек"),
           new Weapon (30, 0.3m, "RTX 5090"),
           new Weapon (35, 0.35m, "Святая мать"),
           new Weapon (40, 0.4m, "Тьмослик"),
           new Weapon (45, 0.45m, "Хомяк"),
           new Weapon (50, 0.5m, "Пенсил"),
           new Weapon (55, 0.55m, "Топор Рыкарей"),
           new Weapon (100, 0.99m, "Пёся"),
        };

        List<Equipment> equipments = new List<Equipment>
        {
            new Equipment(5, "Обычная броня"),
            new Equipment(10, "Коcоворотка"),
            new Equipment(15, "Железная дева"),
            new Equipment(20, "Стальные яйца"),
            new Equipment(25, "Мифриловая броня"),
            new Equipment(30, "Драконья броня"),
            new Equipment(35, "Броня из Вальхейма"),
            new Equipment(40, "Броня Гаврюшы"),
            new Equipment(45, "Броня из пёси"),
            new Equipment(-10, "БДСМ-Костюм"),
            new Equipment(-100, "Фурсьют"),
        };
        public static Enemy GenerateEnemy()
        {
            return EnemyFabric.CreateEnemy();
        }

       public static void OpenChest()
        {
            Player player = Player.GetInstance();
            int itemType = RandomHelper.rand.Next(0, 3);
            switch (itemType)
            {
                case 0:
                    player.Heal();
                    break;
                case 1:
                    player.EquipWeapon(new Game().weapons);
                    break;
                case 2:
                    player.EquipEquipment(new Game().equipments);
                    break;
            }
        }

        public static void Battle(Enemy enemy)
        {
            Player player = Player.GetInstance();
            Console.WriteLine($"Вы столкнулись с {enemy.Name}!");
            while (player.IsAlive() && enemy.IsAlive())
            {
                if (player.IsFrozen)
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    player.IsFrozen = false;
                }
                else
                {
                    Console.WriteLine("Ваш ход! Выберите действие: 1 - Атака, 2 - Защита");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        decimal actualDamage = player.Weapon.Damage;
                        if (RandomHelper.rand.NextDouble() < (double)player.Weapon.CritChance)
                        {
                            actualDamage *= 2;
                            Console.WriteLine("Вы наносите критический удар!");
                        }
                        enemy.Health -= (int)actualDamage;
                        Console.WriteLine($"Вы нанесли {actualDamage} урона {enemy.Name}. Текущее здоровье врага: {enemy.Health}/{enemy.MaxHealth}");
                    }
                    else if (choice == "2")
                    {
                        player.IsDefending = true;
                        Console.WriteLine("Вы выбрали защиту! Уклонение от следующей атаки врага увеличено.");
                    }
                }
                if (enemy.IsAlive())
                {
                    enemy.AttackPlayer(player);
                }
                player.IsDefending = false; 
            }
            if (!player.IsAlive())
            {
                Console.WriteLine("Вы проиграли бой");
            }
            else
            {
                Console.WriteLine($"Вы победили {enemy.Name}!");
            }
        }

        public static void StartGame()
        {
            Player player = Player.GetInstance();
            int turnCount = 0;
            Console.WriteLine("Добро пожаловать в игру!");
            while (player.IsAlive())
            {
                turnCount++;
                Console.WriteLine($"\nХод {turnCount}:");
                if (turnCount % 10 == 0)
                {
                    Enemy boss = EnemyFabric.CreateEnemy();
                    Console.WriteLine($"Вы встретили босса: {boss.Name}!");
                    Battle(boss);
                }
                else
                {
                    if (RandomHelper.rand.Next(0, 2) == 0)
                    {
                        OpenChest();
                    }
                    else
                    {
                        Enemy enemy = GenerateEnemy();
                        Battle(enemy);
                    }
                }
            }
            Console.WriteLine("Игра окончена. Вы проиграли.");
        }

    }

    public class Player
    {
        private Player()
        {
            MaxHealth = 100;
            Health = MaxHealth;
            Damage = 10;
            Defense = 5;
            IsFrozen = false;
            IsDefending = false;
            Weapon = new Weapon(10, 0.1m, "Обычный меч");
            Equipment = new Equipment(5, "Обычная броня");
        }
        private static Player _instance = new Player();
        public static Player GetInstance() { return _instance; }

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public Weapon Weapon { get; set; }
        public Equipment Equipment { get; set; }
        public decimal Defense { get; set; }
        public decimal Damage { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsDefending { get; set; }

        public void Heal()
        {
            Health = MaxHealth;
            Console.WriteLine("Вы использовали лечебное зелье и полностью восстановили здоровье!");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void TakeDamage(decimal damage)
        {
            if (IsDefending)
            {
                decimal blockChance = (decimal)RandomHelper.rand.Next(70, 101) / 100m;
                damage *= (1 - blockChance);
                Console.WriteLine($"Вы защищаетесь! Урон уменьшен на {blockChance * 100}%.");
            }
            damage -= Defense;
            if (damage < 0) damage = 0;
            Health -= (int)damage;
            Console.WriteLine($"Вы получили {damage} урона. Текущее здоровье: {Health}/{MaxHealth}");
        }

        public void TryAvoid()
        {
            if (IsDefending)
            {
                decimal avoidChance = 0.4m;
                if (RandomHelper.rand.NextDouble() < (double)avoidChance)
                {
                    Console.WriteLine("Вы успешно уклонились от атаки врага!");
                    return;
                }
            }
        }
        public void EquipWeapon(List<Weapon> weapons)
        {
            Weapon newWeapon = weapons[RandomHelper.rand.Next(0, weapons.Count)];
            Console.WriteLine($"Вы нашли новое оружие: {newWeapon.Name} (Урон: {newWeapon.Damage}, Шанс крита: {newWeapon.CritChance * 100}%)");
            Console.WriteLine($"Текущее оружие: {Weapon.Name} (Урон: {Weapon.Damage}, Шанс крита: {Weapon.CritChance * 100}%)");
            Console.WriteLine("Хотите заменить текущее оружие на новое? (да/нет)");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "да")
            {
                Weapon = newWeapon;
                Console.WriteLine($"Вы экипировали новое оружие: {Weapon.Name}");
            }
            else
            {
                Console.WriteLine("Вы оставили текущее оружие.");
            }
        }

        public void EquipEquipment(List<Equipment> equipments)
        {
            Equipment newEquipment = equipments[RandomHelper.rand.Next(0, equipments.Count)];
            Console.WriteLine($"Вы нашли новое снаряжение: {newEquipment.Name} (Защита: {newEquipment.Defense})");
            Console.WriteLine($"Текущее снаряжение: {Equipment.Name} (Защита: {Equipment.Defense})");
            Console.WriteLine("Хотите заменить текущее снаряжение на новое? (да/нет)");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "да")
            {
                Equipment = newEquipment;
                Defense = newEquipment.Defense;
                Console.WriteLine($"Вы экипировали новое снаряжение: {Equipment.Name}");
            }
            else
            {
                Console.WriteLine("Вы оставили текущее снаряжение.");
            }
        }
    }
    public class Weapon
    {
        public int Damage { get; set; }
        public decimal CritChance { get; set; }
        public string Name { get; set; }
        public Weapon( int dmg, decimal critChance, string name)
        {
            Damage = dmg;
            CritChance = critChance;
            Name = name;
        }
    }
    public class Equipment
    {
        public int Defense { get; set; }
        public string Name { get; set; }
        public Equipment(int def, string name)
        {
            Defense = def;
            Name = name;
        }
    }

    public class EnemyFabric
    {
        public static Enemy CreateEnemy()
        {
            int enemyType = new Random().Next(0, 7);
            switch (enemyType)
            {
                case 0:
                    return new Goblin();
                case 1:
                    return new Skeleton();
                case 2:
                    return new Mage();
                case 3:
                    return new Gorlanov();
                case 4:
                    return new Kovalskii();
                case 5:
                    return new Archmage();
                case 6:
                    return new Pestov();
                default:
                    throw new Exception("Неверный тип врага");
            }
        }
    }
    public abstract class Enemy
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public decimal Damage { get; set; }
        public decimal Defense { get; set; }
        public decimal CritChance { get; set; }
        public decimal FreezeChance { get; set; }
        public string Name { get; set; }
        public bool IgnoreDefense { get; set; }
        public Enemy(int health, decimal damage, decimal defense, decimal critChance, decimal freezeChance, string name, bool ignoreDefense = false)
        {
            Health = health;
            MaxHealth = health;
            Damage = damage;
            Defense = defense;
            CritChance = critChance;
            FreezeChance = freezeChance;
            Name = name;
            IgnoreDefense = ignoreDefense;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void AttackPlayer(Player player)
        {
            decimal actualDamage = Damage;
            if (RandomHelper.rand.NextDouble() < (double)CritChance)
            {
                actualDamage *= 2;
                Console.WriteLine($"{Name} наносит критический удар!");
            }
            if (IgnoreDefense)
            {
                Console.WriteLine($"{Name} игнорирует вашу защиту!");
                player.TakeDamage(actualDamage);
            }
            if (RandomHelper.rand.NextDouble() < (double)FreezeChance)
            {
                player.IsFrozen = true;
                Console.WriteLine($"{Name} наложил заморозку! Вы пропускаете следующий ход.");
            }   
            else
            {
                player.TakeDamage(actualDamage);
            }
        }
    }
    public class Goblin : Enemy
    {
        public Goblin() : base(RandomHelper.rand.Next(40, 61), 10, 5, 0.1m, 0, "Гоблин") {}
    }
    public class Skeleton : Enemy
    {
        public Skeleton() : base(RandomHelper.rand.Next(30, 51), RandomHelper.rand.Next(5, 16), RandomHelper.rand.Next(1, 11), 0, 0, "Скелет", true) { }
    }
    public class Mage : Enemy
    {
        public Mage() : base(RandomHelper.rand.Next(20, 41), RandomHelper.rand.Next(10, 21), RandomHelper.rand.Next(0, 3), 0, 0.1m, "Маг") { }
    }
    public class Gorlanov : Goblin
    {
        public Gorlanov() : base()
        {
            Health = (int)(Health * 2.0);
            MaxHealth = Health;
            Damage = Damage * 1.5m;
            Defense = Defense * 1.2m;
            CritChance += 0.1m;
            Name = "ВВГ";
        }
    }
    public class Kovalskii : Skeleton
    {
        public Kovalskii() : base()
        {
            Health = (int)(Health * 2.5);
            MaxHealth = Health;
            Damage = Damage * 1.3m;
            Defense = Defense * 1.4m;
            Name = "Ковальский";
        }
    }
    public class Archmage : Mage
    {
        public Archmage() : base()
        {
            Health = (int)(Health * 1.8);
            MaxHealth = Health;
            Damage = Damage * 1.6m;
            Defense = Defense * 1.1m;
            FreezeChance += 0.1m;
            Name = "Архимаг C++";
        }
    }
    public class Pestov : Skeleton
    {
        public Pestov() : base()
        {
            Health = (int)(Health * 1.3);
            MaxHealth = Health;
            Damage = Damage * 1.8m;
            Defense = Defense * 0.6m;
            FreezeChance += 0.15m;
            Name = "Пестов С--";
        }
    }
    internal class Program
    {
            static void Main(string[] args)
            {
             Game.StartGame();

            }
    }
}
