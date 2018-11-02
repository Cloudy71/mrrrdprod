using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerSelection : NetworkBehaviour {

        Constants constants = new Constants();
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        //[Command]
        public void CmdCharacterSelection(string name, int ID)
        {
            Player player = new Player();
            Character c = CharacterTable.Select(ID);
            Inventory i = new Inventory();
            Weapon w = WeaponTable.Select(c.Weapon);
            player.ID = PlayerTable.Select_Count()+1;
            player.Name = name;
            player.Health = constants.player_basehealth + c.BHealth;
            player.Score = 0;
            player.Armor = 0;
            player.CharacterID = ID;
            player.InventoryID = InventoryTable.Select_Count() + 1;
            PlayerTable.Insert(player);

            
            i.Current = w.Ammo;
            i.Player_ID = player.ID;
            i.Weapon_ID = c.Weapon;
            i.Slot = 1;
            InventoryTable.Insert(i);




        }
    }
}
