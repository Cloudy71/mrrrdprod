﻿using System.Collections;
using System.Collections.Generic;


namespace Hackathon
{
    class WeaponsFiller
    {
        public void Create()
        {                 // NAME      range    DMG    cost    ammo    accuracy
            CreateWeapon("LMG",         4,      70,     8,      200,    70);
            CreateWeapon("Sniperka",    6,      100,    5,      5,      70);
            CreateWeapon("CZ vz. 75",   3,      70,     2,      7,      90);
            CreateWeapon("Brokovnice",  1,      120,    5,      7,      90);
            CreateWeapon("AK-74U",      4,      90,     4,      35,     100);     
        }

        public void CreateWeapon(string name, int range, int damage, int cost, int ammo, int accuracy)
        {
            Weapon w = new Weapon();
            w.ID = WeaponTable.Select_Count() + 1;
            w.Name = name;
            w.Range = 1 + range;
            w.Damage = damage;
            w.Cost = cost;
            w.Ammo = ammo;
            w.Accuracy = accuracy;
            WeaponTable.Insert(w);
        }
    }
}
