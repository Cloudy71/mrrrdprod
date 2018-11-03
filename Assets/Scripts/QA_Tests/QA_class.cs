using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hackathon
{
    public class QA_class
    {

        public static void Create(out string TestResult)
        {
            List<string> Comment = new List<string>();
            Comment.Add("Přidáno: \n");

            CharacterTable.DeleteAll();
            InventoryTable.DeleteAll();
            PlayerTable.DeleteAll();
            WeaponTable.DeleteAll();

            try
            {
                Comment.Add(Test_CreateWapon());
            }
            catch
            {
                Comment.Add("Zbraně se nevytvořily");
            }
            try
            {
                Comment.Add(Test_CreateCharacters());
            }
            catch
            {
                Comment.Add("Charactery se nevytvořily");
            }
            Comment.Add(" ====================================");
            Comment.Add("2 hráči");
            Comment.Add("=====================================");
            try
            {
                Comment.Add(Test_CreatePlayers());
            }
            catch
            {
                Comment.Add("Hráči se nevytvořili");
            }
            Comment.Add(" ====================================");
            Comment.Add("CombatSystem");
            Comment.Add("=====================================");
            try
            {
                Comment.Add(Test_PlayerCombat());
            }
            catch
            {
                Comment.Add("Nedošlo k souboji");
            }
            Comment.Add(" ====================================");
            Comment.Add("LootBox");
            Comment.Add("=====================================");
            try
            {
                for (uint i = 0; i < 100; i++)
                    Comment.Add(Test_refill(1));
            }
            catch
            {
                Comment.Add("Nepadlo nic");
            }

            try
            {
                Test_StatReport(1);
                Test_StatReport(2);
            }
            catch
            {
                Comment.Add("Nepovedlo se načíst z hráče");
            }



            var comment = System.String.Join("", Comment.ToArray());
            TestResult = comment;
            try
            {
                using (StreamWriter writer = new StreamWriter("QA_Tests.txt"))
                {
                    foreach (string s in Comment)
                    {
                        writer.WriteLine(s);
                    }
                }
            }
            catch { }
        }

        //volají se pak
        static string Test_CreateWapon()
        {
            WeaponsFiller.Create();
            return  "Zbraně do DBS \n";
        }
        static string Test_CreateCharacters()
        {
            CharactersFiller.create();
            return  "Charactery do DBS \n";
        }
        static string Test_CreatePlayers()
        {
            string name1, name2;
            name1 = "prvni";
            name2 = "druhy";
            PlayerSelection.CharacterSelection(name1, 1);
            string comment = "Hráč1: "
                           + name1 + "\n Zbraň: "
                           + PlayerSelection.UsedWeapon;
            PlayerSelection.CharacterSelection(name2, 2);
            comment += " Hráč2: " 
                    + name2 + " ";
            return comment;
        }
        static string Test_PlayerCombat()
        {
            // CurrHeal, CurrArmor, CurrActionPoint;
            PlayerCombat.DoPlayerCombat(1,2);
            return
               
                "DMG: " + PlayerCombat.Dmg + " | "
                + "Accuracy: " + PlayerCombat.Accuracy + " | "
                + "ActionPointLoss: " + PlayerCombat.ActionPointLoss + " | "
                + "BaseHealth: " + PlayerCombat.BaseHeal + " | "
                + "BaseArmor: " + PlayerCombat.BaseArmor + " | "
                + "BaseActionPoint: " + PlayerCombat.BaseActPoint + " | "
                + "CurrHealth :" + PlayerCombat.BaseHeal + " | "
                + "CurrArmor: " + PlayerCombat.CurrArmor + " | "
                + "CurrActionPoint: " + PlayerCombat.CurrActionPoint + " | "

                ;
        }
        static string Test_refill(int id)
        {
            PlayerRefill.RunRefill(id);
            return "získaný item: " + PlayerRefill.ObtainedItem;
        }

        static string Test_StatReport(int id)
        {
            int HP, Stamina;
            string Weapon;
            PlayerStatReport.DoPlayerStatRep(id, out HP, out Stamina, out Weapon);
            return "Hrac: " + id + " HP: " + HP + " SP: " + Stamina + " Weapon: " + Weapon;
        }
    }
}
