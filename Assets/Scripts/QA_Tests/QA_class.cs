using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class QA_class
    {

        public static void Create(out string TestResult)
        {
            string comment = "Přidáno: \n";

            try
            {
                comment += Test_CreateWapon();
            }
            catch
            {
                comment += "Zbraně se nevytvořily";
            }
            try
            {
                comment += Test_CreateCharacters();
            }
            catch
            {
                comment += "Charactery se nevytvořily";
            }
            comment += "\n ==================================== \n Vytvoření 2 hráčů \n ==================================== \n";
            try
            {
                comment += Test_CreatePlayers();
            }
            catch
            {
                comment += "Hráči se nevytvořili";
            }
            comment += "\n ==================================== \n Test combat systemu \n ==================================== \n";
            try
            {
                comment += Test_PlayerCombat();
            }
            catch
            {
                comment += "Nedošlo k souboji";
            }
            comment += "\n ===================================== \n";
            TestResult = comment;

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
            PlayerSelection.CharacterSelection(name2, 2);
            string comment = "Hráč1: "
                           + name1
                           + "\n Hráč2: " 
                           + name2 + "\n ==================================== \n";
            return comment;
        }
        static string Test_PlayerCombat()
        {
            // CurrHeal, CurrArmor, CurrActionPoint;
            PlayerCombat.DoPlayerCombat(1,2);
            return
               
                "DMG: " + PlayerCombat.Dmg + "\n"
                + "Accuracy: " + PlayerCombat.Accuracy + "\n"
                + "ActionPointLoss: " + PlayerCombat.ActionPointLoss + "\n"
                + "BaseHealth: " + PlayerCombat.BaseHeal + "\n"
                + "BaseArmor: " + PlayerCombat.BaseArmor + "\n"
                + "BaseActionPoint: " + PlayerCombat.BaseActPoint + "\n"
                + "CurrHealth :" + PlayerCombat.BaseHeal + "\n"
                + "CurrArmor: " + PlayerCombat.CurrArmor + "\n"
                + "CurrActionPoint: " + PlayerCombat.CurrActionPoint + "\n"
               
                ;
        }
    }
}
