using System.Collections;
using System.Collections.Generic;

namespace Hackathon
{
    public class QA_class
    {
        static void Create()
        {

        }

        static void Test_CreateWapon(out string comment)
        {
            WeaponsFiller.Create();
            comment = "Zbraně do DBS";
        }
        static void Test_CreateCharacter(out string comment)
        {
            CharactersFiller.create();
            comment = "Charactery do DBS";
        }
        static void Test_CreatePlayers(out string comment)
        {
            PlayerSelection PS = new PlayerSelection();
            PS.CmdCharacterSelection("prvni", 1);
            PS.CmdCharacterSelection("druhy", 2);
            comment = "VYtvoření hráčů";
        }
        private void Test_PlayerCombat(out string comment)
        {
            // CurrHeal, CurrArmor, CurrActionPoint;
            PlayerCombat PC = new PlayerCombat();
            PC.CmdPlayerCombat(1,2);
            comment =
                "Test combat systemu"
                + "DMG: " + PC.Dmg + "\n"
                + "Accuracy: " + PC.Accuracy + "\n"
                + "ActionPointLoss: " + PC.ActionPointLoss + "\n"
                + "BaseHealth: " + PC.BaseHeal + "\n"
                + "BaseArmor: " + PC.BaseArmor + "\n"
                + "BaseActionPoint: " + PC.BaseActPoint + "\n"
                + "CurrHealth :" + PC.BaseHeal + "\n"
                + "CurrArmor: " + PC.CurrArmor + "\n"
                + "CurrActionPoint" + PC.CurrActionPoint + "\n"
                ;
        }
    }
}
