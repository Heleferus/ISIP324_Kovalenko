using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    class Game
    {
        Player player = Player.GetInstance();
    }

    public class Player
    {
        static Random rnd = new Random();
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
                decimal blockChance = (decimal)rnd.Next(70, 101) / 100m;
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
                if (rnd.NextDouble() < (double)avoidChance)
                {
                    Console.WriteLine("Вы успешно уклонились от атаки врага!");
                    return;
                }
            }
        }
        public void EquipWeapon(Weapon newWeapon)
        {
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

        public void EquipEquipment(Equipment newEquipment)
        {
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
            Random rnd = new Random();
            decimal actualDamage = Damage;
            if (rnd.NextDouble() < (double)CritChance)
            {
                actualDamage *= 2;
                Console.WriteLine($"{Name} наносит критический удар!");
            }
            if (IgnoreDefense)
            {
                Console.WriteLine($"{Name} игнорирует вашу защиту!");
                player.TakeDamage(actualDamage);
            }
            else
            {
                player.TakeDamage(actualDamage);
            }
        }
    }
    public class Goblin : Enemy
    {
        public Goblin() : base(50, 10, 5, 0.1m, 0, "Гоблин") {}
    }
    public class Skeleton : Enemy
    {
        public Skeleton() : base(40, 8, 3, 0, 0, "Скелет", true) { }
    }
    public class Mage : Enemy
    {
        public Mage() : base(30, 12, 2, 0, 0.1m, "Маг") { }
    }
    public class Gorlanov : Goblin
    {
        public Gorlanov() : base()
        {
            Health = (int)(Health * 2.0);
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
            Damage = Damage * 1.8m;
            Defense = Defense * 0.6m;
            FreezeChance += 0.15m;
            Name = "Пестов С--";
        }
    }
    internal class Program
    {
//        Сделайте текстовую игру для консоли.Игра пошаговая: на каждом ходу случается одно из двух событий — игрок находит сундук или сталкивается со случайным врагом.

// Сущности
 

// Игрок

// Характеристики: здоровье (HP).


// Экипировка: одно оружие и одни доспехи одновременно.


// Враги

// Общие характеристики: здоровье, атака, защита.

// Особенности типов:


// Гоблин — имеет шанс нанести критический урон.

// Скелет — игнорирует защиту игрока.

// Маг — имеет шанс наложить «заморозку» (игрок пропускает следующий ход).

//Боссы

//ВВГ(раса Гоблин)Сохраняет: шанс критического удара.Особые характеристики:Здоровье ×2.0 от базового гоблина.Атака ×1.5.Защита ×1.2.Шанс крита +10%. к значению обычного гоблина.

//Ковальский(раса Скелет)Сохраняет: полностью игнорирует защиту игрока.Особые характеристики:Здоровье ×2.5.Атака ×1.3.Защита ×1.4.

//Архимаг C++ (раса Маг) Сохраняет: шанс наложить заморозку (пропуск хода).Особые характеристики:Здоровье ×1.8.Атака ×1.6.Защита ×1.1.Шанс заморозки +10%. к значению обычного мага.

//Пестов С-- (раса Скелет) Сохраняет: полностью игнорирует защиту игрока.Особые характеристики:Здоровье ×1.3.Атака ×1.8.Защита ×0.6.Шанс заморозки +15%. к значению обычного мага.

//Ход игры

//В начале каждого хода случайно определяется событие: сундук или враг (тип врага выбирается случайно из перечисленных) с шансом 50 на 50.

//Если выпал враг, начинается бой.

//Если выпал сундук, игрок получает случайный предмет.

//Каждые 10 ходов игроку попадается случайный босс.

//Бой

//Игрок всегда ходит первым.

//Ход игрока: выбрать Атаку или Защиту.

//Защита даёт 40% шанс полностью уклониться от следующей атаки врага.Если уклониться не удалось, срабатывает блок: уменьшение получаемого урона на 70–100% от характеристики защиты.

//После хода игрока враг всегда совершает атаку по игроку, применяя свои особенности (крит.шанс, игнор брони, заморозка).

//Сундук и предметы

//Из сундука может выпасть лечебное зелье, оружие или доспех (случайно).

//Лечебное зелье мгновенно полностью лечит игрока.

//При выпадении оружия или доспеха нужно:

//Показать характеристики нового предмета и текущей экипировки.

//Дать выбор: взять новый предмет (заменив текущий) или выбросить его.

//Сделайте так, чтобы все шансы и случайные величины (встреча сундука/врага, тип врага, крит.шанс/заморозка, величина блока 70–100%) определялись генератором случайных чисел.
            static void Main(string[] args)
            {
               
            }
    }
}
