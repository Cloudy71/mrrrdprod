using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerSelection : NetworkBehaviour {

        public static GameObject LocalPlayer;
        public static string UsedWeapon;

        private PlayerData _playerData;


        // Use this for initialization
        void Start() {
            _playerData = GetComponent<PlayerData>();
            
            if (isLocalPlayer)
            {
                LocalPlayer = this.gameObject;

                string comm;
                Hackathon.QA_class.Create(out comm);
                Debug.Log(comm);
            }
        }

        // Update is called once per frame
        void Update() {

        }

        public static void CharacterSelection(string name, int ID)
        {
            LocalPlayer.GetComponent<PlayerSelection>().CmdCharacterSelection(name, ID);
        }

        [Command]
        public void CmdCharacterSelection(string name, int ID)
        {
            Player player = new Player();
            Character c = CharacterTable.Select(ID);
            Inventory i = new Inventory();
            Weapon w = WeaponTable.Select(c.Weapon);
            player.ID = PlayerTable.Select_Count() + 1;
            player.Name = name;
            player.Health = Constants.player_basehealth + c.BHealth;
            player.Score = 0;
            player.Armor = 0;
            player.CharacterID = ID;
            player.InventoryID = InventoryTable.Select_Count() + 1;
            PlayerTable.Insert(player);

            i.Actual = w.Ammo;
            i.Player_ID = player.ID;
            i.Weapon_ID = c.Weapon;
            i.Slot = InventoryTable.Select_Count() + 1;
            InventoryTable.Insert(i);

            PlayerSelection.UsedWeapon = w.Name;

            Debug.Log("Srv exec." + name + "," + ID);

            _playerData.Name = player.Name;
            _playerData.Armor = player.Armor;
            _playerData.Health = player.Health;
            _playerData.Score = player.Score;
            _playerData.Inventory_ID = player.InventoryID;
            _playerData.Character_ID = player.CharacterID;
            _playerData.IsMoving = false;
            // TODO: Generate position for player.
        }
    }
}
