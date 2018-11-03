using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{

    public class PlayerRefill : NetworkBehaviour
    {
        void Start()
        {
     
        }

        [SyncVar] string message;

        public static string ObtainedItem;


        [Command]
        public void CmdRefill(int id)
        {
            Player player = PlayerTable.Select(id);
            System.Random rnd = new System.Random();
            int rnum = rnd.Next(0, 100);
            if(rnum >= 0 && rnum <= 33) //healthpack
            {
               
                Character ch = CharacterTable.Select(player.CharacterID);
                player.Health = ch.BHealth + Constants.player_basehealth;
                PlayerTable.Update(player);
                this.message = "Health";
            }
            else if(rnum > 33 && rnum <= 66)//ammo
            {
                Inventory i = InventoryTable.Select(player.InventoryID);
                i.Actual = WeaponTable.Select(i.Weapon_ID).Ammo;
                InventoryTable.Update(i);
                this.message = "Ammo";
            }
            else //Weapon
            {
                Collection<Weapon> weapons = WeaponTable.Select();
                Collection<Inventory> inventories = InventoryTable.Select();
                Collection<Weapon> notowned = new Collection<Weapon>();
                foreach (Weapon w in weapons)
                {
                    bool add = true;
                    foreach (Inventory inv in inventories)
                    {
                        if(w.ID == inv.Weapon_ID && inv.Player_ID == player.ID) //pokud hráč již vlastní zbraň
                        {
                            add = false;
                            break;
                        }
                    }
                    if(add)
                    {
                        notowned.Add(w);
                    }                   
                }
                int WeaponID = rnd.Next(0, notowned.Count - 1);
                Inventory i = new Inventory();
                i.Player_ID = player.ID;
                i.Weapon_ID = notowned[WeaponID].ID;
                i.Actual = notowned[WeaponID].Ammo;
                i.Slot = InventoryTable.Select_Count() + 1;
                InventoryTable.Insert(i);
                this.message = notowned[WeaponID].Name;
            }
            PlayerRefill.ObtainedItem = this.message;  
        }

        public static void RunRefill(int id)
        {
            PlayerSelection.LocalPlayer.GetComponent<PlayerRefill>().CmdRefill(id);
        }
    }
}
