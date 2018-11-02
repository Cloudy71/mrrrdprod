using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerCombat : NetworkBehaviour
    {
        [SyncVar]
        public string Message;
        public int Dmg, Accuracy, ActionPointLoss, BaseHeal, BaseArmor, BaseActPoint, CurrHeal, CurrArmor, CurrActionPoint;
        private
            Player Attacker;
            Character AttackerCharacter;
            Inventory AttackerInventory;
            Weapon Weapon;

            Player Defender;
            Character DefenderCharacter;


        [Command]
        public void CmdPlayerCombat(int attacker_id, int defender_id)
        {
            this.Attacker = PlayerTable.Select(attacker_id);
            this.AttackerCharacter = CharacterTable.Select(this.Attacker.CharacterID);        
            this.AttackerInventory = InventoryTable.Select(this.Attacker.InventoryID);         
            this.Weapon = WeaponTable.Select(this.AttackerInventory.Weapon_ID);

            this.Defender = PlayerTable.Select(defender_id);
            this.Defender = PlayerTable.Select(this.Defender.CharacterID);

            bool EnoughStamina;
            int Damage = CountFireDamage(out EnoughStamina);
            if (EnoughStamina)
                if (Damage > 0)
                {
                    LifeLoss(Damage);
                    this.Message = System.Convert.ToString(Damage);
                }
                else
                    this.Message = "miss";
            else
                this.Message = "influcient action points";
        }
        /*
         *  Výpočet dmg
         *  Odečtení náboje 
         *  odečtení actionpointů
         *  
         *  vrací dmg + out parametr dává najevo, jestli je dost staminy
         */
        private int CountFireDamage(out bool EnoughStamina)
        {
            this.ActionPointLoss = this.Weapon.Cost; // pro vypis
            this.BaseActPoint = this.AttackerCharacter.Stamina; // pro vypis

            if (this.AttackerCharacter.Stamina >= this.Weapon.Cost)
            {
                EnoughStamina = true;
                int Accuracy = this.Weapon.Accuracy;
                this.Accuracy = Accuracy; // pro vypis
                int RawDamage = this.Weapon.Damage;

                this.AttackerInventory.Current--;
                InventoryTable.Update(AttackerInventory);

                this.AttackerCharacter.Stamina -= this.Weapon.Cost;
                this.CurrActionPoint = this.AttackerCharacter.Stamina; // pro vypis
                System.Random r = new System.Random();
                int RandAcc = r.Next(0, 100);

                if (RandAcc > Accuracy)
                {
                    return 0;
                }
                else
                {
                    System.Random DamageOnRange = new System.Random();
                    int Damage = DamageOnRange.Next(System.Convert.ToInt32(RawDamage * 0.85), RawDamage);
                    this.Dmg = Damage; // pro vypis
                    return Damage;
                }
            }
            EnoughStamina = false;
            return 0;

        }

        /*
         *  Odečtení armoru, nebo hp, když nestačí armor, odečte se zbytek z hp
         */
        private void LifeLoss(int Damage)
        {
            this.BaseArmor = this.Defender.Armor;
            this.BaseHeal = this.Defender.Health;

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

            this.CurrArmor = this.Defender.Armor;
            this.CurrHeal = this.Defender.Health;
        }

    
    }
}
