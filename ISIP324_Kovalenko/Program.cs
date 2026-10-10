using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    class Game
    {
        static Random rnd = new Random();
        Player player = Player.GetInstance();
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

    public class EnemyFabric { }
    public abstract class Enemy
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public decimal Damage { get; set; }
        public decimal Defense { get; set; }
        public decimal CritChance { get; set; }
        public decimal FreezeChance { get; set; }
        public string Name { get; set; }
        public bool HasCrit = false;
        public Enemy(int health, decimal damage, decimal defense, decimal critChance, decimal freezeChance, string name)
        {
            Health = health;
            MaxHealth = health;
            Damage = damage;
            Defense = defense;
            CritChance = critChance;
            FreezeChance = freezeChance;
            Name = name;
        }
    }
    public class Goblin : Enemy
    {
        public Goblin() : base(50, 10, 5, 0.1m, 0, "Гоблин") {}
    }
    public class Skeleton : Enemy
    {
        public Skeleton() : base(40, 8, 3, 0, 0, "Скелет") { }
    }
    public class Mage : Enemy
    {
        public Mage() : base(30, 12, 2, 0, 0.1m, "Маг") { }
    }
    public class Gorlanov : Goblin { }
    public class Kovalskii : Skeleton { }
    public class Archmage : Mage { }
    public class Pestov : Skeleton { }
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
