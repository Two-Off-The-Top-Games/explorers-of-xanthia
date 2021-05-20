using System;
using UnityEngine;

namespace Items.Equipment.Weapons
{
    public class Longsword : Weapon
    {
        public override int Damage => 5;

        public Longsword()
        {
            EquippableGameObject = Resources.Load<GameObject>("Longsword");
        }
    }
}
