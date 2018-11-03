using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerCombat : NetworkBehaviour
    {

        public static GameObject LocalPlayer;
        void Start()
        {
            if (isLocalPlayer)
                LocalPlayer = this.gameObject;
        }

        [SyncVar]
        public string Message;

        public static int Dmg, Accuracy, ActionPointLoss, BaseHeal, BaseArmor, BaseActPoint, CurrHeal, CurrArmor, CurrActionPoint;

        private Player Attacker;
        private Character AttackerCharacter;
        private Inventory AttackerInventory;
        private Weapon Weapon;

        private Player Defender;


        [Command]
        public void CmdPlayerCombat(int attacker_id, int defender_id)
        {
            this.Attacker = PlayerTable.Select(attacker_id);
            this.AttackerCharacter = CharacterTable.Select(this.Attacker.CharacterID);
            this.AttackerInventory = InventoryTable.Select(this.Attacker.InventoryID);
            this.Weapon = WeaponTable.Select(this.AttackerInventory.Weapon_ID);

            this.Defender = PlayerTable.Select(defender_id);

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

        public static void DoPlayerCombat(int attacker_id, int defender_id)
        {
            LocalPlayer.GetComponent<PlayerCombat>().CmdPlayerCombat(attacker_id, defender_id);
        }

        private int CountFireDamage(out bool EnoughStamina)
        {
            /*
             *  Výpočet dmg
             *  Odečtení náboje 
             *  odečtení actionpointů
             *  
             *  vrací dmg + out parametr dává najevo, jestli je dost staminy
             */
            PlayerCombat.ActionPointLoss = this.Weapon.Cost; // pro vypis
            PlayerCombat.BaseActPoint = this.AttackerCharacter.Stamina; // pro vypis

            if (this.AttackerCharacter.Stamina >= this.Weapon.Cost)
            {
                EnoughStamina = true;
                int Accuracy = this.Weapon.Accuracy;
                PlayerCombat.Accuracy = Accuracy; // pro vypis
                int RawDamage = this.Weapon.Damage;

                this.AttackerInventory.Actual--;
                InventoryTable.Update(AttackerInventory);

                this.AttackerCharacter.Stamina -= this.Weapon.Cost;
                PlayerCombat.CurrActionPoint = this.AttackerCharacter.Stamina; // pro vypis
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
                    PlayerCombat.Dmg = Damage; // pro vypis
                    return Damage;
                }
            }
            EnoughStamina = false;
            return 0;

        }
 
        private void LifeLoss(int Damage)
        {
            /*
             *  Odečtení armoru, nebo hp, když nestačí armor, odečte se zbytek z hp
             */
            PlayerCombat.BaseArmor = this.Defender.Armor;
            PlayerCombat.BaseHeal = this.Defender.Health;

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

            PlayerCombat.CurrArmor = this.Defender.Armor;
            PlayerCombat.CurrHeal = this.Defender.Health;
        }


    }
}
