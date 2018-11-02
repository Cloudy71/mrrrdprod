using System.Collections;
using System.Collections.Generic;

namespace Hackathon
{
    public class QA_class
    {

        public static void Create(out string TestResult)
        {
            string comment = "Přidáno:";
            try
            {
                comment += Test_CreateWapon();
            }
            catch (System.Exception e)
            {
                comment += "Zbraňe se nevytvořily";
                throw e;
            }

            try
            {
                comment += Test_CreateCharacters();
            }
            catch (System.Exception e)
            {
                comment += "Charactery se nevytvořily";
                throw e;
            }

            try
            {
                comment += Test_CreatePlayers();
            }
            catch (System.Exception e)
            {
                comment += "Hráči se nevytvořili";
                throw e;
            }

            try
            {
                comment += Test_PlayerCombat();
            }
            catch (System.Exception e)
            {
                comment += "Nedošlo k souboji";
                throw e;
            }
            TestResult = comment;
        }

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
            PlayerSelection PS = new PlayerSelection();
            PS.CmdCharacterSelection("prvni", 1);
            PS.CmdCharacterSelection("druhy", 2);
            return  "Vytvoření 2 hráčů \n";
        }
        static string Test_PlayerCombat()
        {
            // CurrHeal, CurrArmor, CurrActionPoint;
            PlayerCombat PC = new PlayerCombat();
            PC.CmdPlayerCombat(1,2);
            return 
                "Test combat systemu \n"
                + "DMG: " + PC.Dmg + "\n"
                + "Accuracy: " + PC.Accuracy + "\n"
                + "ActionPointLoss: " + PC.ActionPointLoss + "\n"
                + "BaseHealth: " + PC.BaseHeal + "\n"
                + "BaseArmor: " + PC.BaseArmor + "\n"
                + "BaseActionPoint: " + PC.BaseActPoint + "\n"
                + "CurrHealth :" + PC.BaseHeal + "\n"
                + "CurrArmor: " + PC.CurrArmor + "\n"
                + "CurrActionPoint: " + PC.CurrActionPoint + "\n"
                ;
        }
    }
}
