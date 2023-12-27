using GorillaNetworking;
using HarmonyLib;

namespace GorillaOS.Patchers
{
    [HarmonyPatch(typeof(GorillaComputer), "ProcessSupportState", MethodType.Normal)]
    public class SupportPatch
    {
        public static int focusedModual = 1;

        static bool Prefix(GorillaKeyboardButton buttonPressed)
        {
            focusedModual = 1;
            if (int.TryParse(buttonPressed.characterString, out int result))
            {
                if (result != 0)
                {
                    focusedModual = result;
                }
            }

            if (buttonPressed.characterString== "enter")
            {
                Main.Moduals.ToArray()[focusedModual - 1].enabled = !Main.Moduals.ToArray()[focusedModual - 1].enabled;
            }
            Main.instance.Refresh();
            return true;
        }
    }
}
