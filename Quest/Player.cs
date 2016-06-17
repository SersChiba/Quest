using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest
{
    class Player : Mover
    {
        //private Game game;
        private Weapon equippedWeapon;
        private List<Weapon> inventory = new List<Weapon>();
        public int HitPoints { get; private set; }
        internal IEnumerable<string> Weapons
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Weapon weapon in inventory)
                {
                    names.Add(weapon.Name);
                }
                return names;
            }
        }

        public Player(Game game, Point location) : base(game, location)
        {
            HitPoints = 10;
        }

        internal void Move(Direction moveDirection)
        {
            location = Move(moveDirection, game.Boundaries);
            if (!game.WeaponInRoom.PickedUp)
                if (Nearby(location, 1))
                {
                    game.WeaponInRoom.PickUpWeapon();
                    inventory.Add(game.WeaponInRoom);
                    if (inventory.Count == 1)
                        Equip(game.WeaponInRoom.Name);
                }
        }

        internal void Equip(string weaponName)
        {
            foreach (Weapon weapon in inventory)
            {
                if (weapon.Name == weaponName)
                    equippedWeapon = weapon;
            }
        }

        internal void Hit(int maxDamage, Random random)
        {
            HitPoints -= random.Next(1, maxDamage + 1);
        }

        internal void IncreaseHealth(int health, Random random)
        {
            HitPoints += random.Next(1, health + 1);
        }

        internal void Attack(Direction attackDirection, Random random)
        {
            if (equippedWeapon.Name != "")
            {                
                if (equippedWeapon is IPotion)
                {
                    inventory.Remove(equippedWeapon);
                }
                equippedWeapon.Attack(attackDirection, random);
            }
        }
    }
}
