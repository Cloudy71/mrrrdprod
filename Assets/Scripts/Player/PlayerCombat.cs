using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerCombat : NetworkBehaviour
    {
        [SyncVar]
        private
            Player Attacker;
            Character AttackerCharacter;
            Inventory AttackerInventory;
            Weapon Weapon;

            Player Defender;
            Character DefenderCharacter;
        
        [Command]
        public string CmdPlayerCombat(int attacker_id, int defender_id)
        {
            this.Attacker = PlayerTable.Select(attacker_id);
            this.AttackerCharacter = CharacterTable.Select(Attacker.CharacterID);        
            this.AttackerInventory = InventoryTable.Select(Attacker.InventoryID);         
            this.Weapon = WeaponTable.Select(AttackerInventory.Weapon_ID);

            this.Defender = PlayerTable.Select(defender_id);
            this.Defender = PlayerTable.Select(Defender.CharacterID);


            int Damage = CountFireDamage();
            if (Damage > 0)
            {
                LifeLoss(Damage);
                return System.Convert.ToString(Damage);
            }
            else
                return "miss";
            

        }
        /*
         *  Výpočet dmg
         *  Odečtení náboje 
         */
        private int CountFireDamage()
        {
            int Accuracy = this.Weapon.Accuracy;
            int RawDamage = this.Weapon.Damage;

            this.AttackerInventory.Current--;
            InventoryTable.Update(AttackerInventory);

            System.Random r = new System.Random();
            int RandAcc = r.Next(0,100);

            if (RandAcc > Accuracy)
            {
                return 0;
            }
            else
            {
                System.Random DamageOnRange = new System.Random();
                int Damage = DamageOnRange.Next(System.Convert.ToInt32(RawDamage * 0.85), RawDamage);
                return Damage;
            }
        }

        /*
         *  Odečtení armoru, nebo hp, když nestačí armor, odečte se zbytek z hp
         */
        private void LifeLoss(int Damage)
        {
            int DamageOverLimit = 0;
            if (this.Defender.Armor > 0)
            {
                this.Defender.Armor -= Damage;
                if (this.Defender.Armor < 0)
                {
                    DamageOverLimit = (-this.Defender.Armor);
                    this.Defender.Armor = 0;
                }
            }
            else
                this.Defender.Health -= Damage;

            this.Defender.Health -= DamageOverLimit;
            PlayerTable.Update(this.Defender);
        }

    
    }
}
