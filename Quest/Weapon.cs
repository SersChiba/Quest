using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest
{
    abstract class Weapon : Mover
    {
        public bool PickedUp { get; private set; }
        public abstract string Name { get; }

        //public Point Location;

        public Weapon(Game game, Point location) : base(game, location)
        {
            PickedUp = false;
        }

        public void PickUpWeapon()
        {
            PickedUp = true;
        }
        public bool DamageEnemy(Direction direction, int radius, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < radius; distance++)
            {
                foreach (var enemy in game.Enemies)
                {
                    if (Nearby(enemy.Location, target, distance))
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }
                }
                target = Move(direction, target, game.Boundaries);
            }
            return false;
        }

        private Point Move(Direction direction, Point target, Rectangle boundaries)
        {
            throw new NotImplementedException();
        }

        private bool Nearby(Point location, Point target, int distance)
        {
            throw new NotImplementedException();
        }

        public abstract void Attack(Direction attackDirection, Random random);
    }
    class Sword : Weapon
    {
        public Sword(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Sword";
            }
        }

        public override void Attack(Direction attackDirection, Random random)
        {
            DamageEnemy(attackDirection, 10, 3, random);
            switch (attackDirection)
            {
                case Direction.Up:
                    DamageEnemy(Direction.Right, 10, 3, random);
                    DamageEnemy(Direction.Left, 10, 3, random);
                    break;
                case Direction.Down:
                    DamageEnemy(Direction.Left, 10, 3, random);
                    DamageEnemy(Direction.Right, 10, 3, random);
                    break;
                case Direction.Left:
                    DamageEnemy(Direction.Up, 10, 3, random);
                    DamageEnemy(Direction.Down, 10, 3, random);
                    break;
                case Direction.Right:
                    DamageEnemy(Direction.Down, 10, 3, random);
                    DamageEnemy(Direction.Up, 10, 3, random);
                    break;                
            }
        }
    }
    class Bow : Weapon
    {
        public Bow(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Bow";
            }
        }

        public override void Attack(Direction attackDirection, Random random)
        {
            DamageEnemy(attackDirection, 30, 1, random);
        }
    }
    class Mace : Weapon
    {
        public Mace(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Mace";
            }
        }

        public override void Attack(Direction attackDirection, Random random)
        {
            for (int i = 0; i < 4; i++)
            {
                DamageEnemy((Direction)i, 20, 6, random);
            }            
        }
    }
    class BluePotion : Weapon, IPotion
    {
        public BluePotion(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Blue Potion";
            }
        }

        public bool Used
        {
            get
            {
                foreach (var weapon in game.PlayerWeapons)
                {
                    if (weapon.Contains(Name))
                    {
                        return false;
                    }                    
                }
                return true;                
            }
        }

        public override void Attack(Direction attackDirection, Random random)
        {
            game.IncreasePlayerHealth(5, random);                
        }
    }
    class RedPotion : Weapon, IPotion
    {
        public RedPotion(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Red Potion";
            }
        }

        public bool Used
        {
            get
            {
                foreach (var weapon in game.PlayerWeapons)
                {
                    if (weapon.Contains(Name))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public override void Attack(Direction attackDirection, Random random)
        {
            game.IncreasePlayerHealth(10, random);
        }
    }
}
