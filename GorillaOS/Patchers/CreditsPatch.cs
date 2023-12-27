using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using static GorillaNetworking.GorillaComputer;

namespace GorillaOS.Patchers
{
    [HarmonyPatch(typeof(GorillaComputer), "ProcessCreditsState", MethodType.Normal)]
    internal class CreditsPatch
    {
        static bool Prefix(GorillaKeyboardButton buttonPressed)
        {
            if (buttonPressed.characterString == "enter")
            {
                return false;
            }
            if (int.TryParse(buttonPressed.characterString, out var character))
            {
                if (character != 0)
                {
                    Main.instance.UpadateTheme(character);
                }
            }
            return true;
        }
    }
}
