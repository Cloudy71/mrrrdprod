using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerStatReport : NetworkBehaviour
    {

        public static GameObject LocalPlayer;
        void Start()
        {
            if (isLocalPlayer)
                LocalPlayer = this.gameObject;
        }

        [Command]
        public void CmdPlayerStatRep(int player_id, out int HP, out int Stamina, out string Weapon)
        {
            Player p = PlayerTable.Select(player_id);
            Character c = CharacterTable.Select(p.CharacterID);
            Inventory i = InventoryTable.Select(p.InventoryID);
            Weapon w = WeaponTable.Select(i.Weapon_ID);
            
            HP = p.Health;
            Stamina = c.Stamina;
            Weapon = w.Name;
        }
        public static void DoPlayerStatRep(int Aplayer_id, out int HP, out int Stamina, out string Weapon)
        {
            LocalPlayer.GetComponent<PlayerStatReport>().CmdPlayerStatRep(Aplayer_id, out HP, out Stamina, out Weapon);
        }
    }

}