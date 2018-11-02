using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class CharactersFiller
    {
        public static void create()
        {
                            //name  stamina  attack   health   weapon
            createCharacter("Heavy",    10,    10,      10,      1);
            createCharacter("Sniper",   10,    10,      10,      2);
            createCharacter("Scout",    10,    10,      10,      3);
            createCharacter("Coldsteel",10,    10,      10,      4);
            createCharacter("Redneck",  10,    10,      10,      5);
            createCharacter("Pepe",     10,    10,      10,      6);

        }

        static void createCharacter(string name, int stamina, int attack, int health, int weapon)
        {
            Character c = new Character();
            c.ID = CharacterTable.Select_Count() + 1;
            c.Name = name;
            c.Stamina = stamina;
            c.BAttack = attack;
            c.BHealth = health;
            c.Weapon = weapon;
            CharacterTable.Insert(c);
        }

    }
}

