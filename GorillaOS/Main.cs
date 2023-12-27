using UnityEngine;
using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using System.Collections.Generic;
using GorillaOS.Patchers;
using System.Linq;
using System.Collections;
using GorillaNetworking;

namespace GorillaOS
{
    [BepInPlugin("com.Kman.GorillaOS", "Gorilla OS", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static Main instance
        {
            get
            {
                return _instance;
            }
        }

        static Main _instance;

        private ManualLogSource logger;
        private float refresh;
        public static List<BaseUnityPlugin> Moduals = new List<BaseUnityPlugin>();
        public static string list;

        private void Awake()
        {
            _instance = this;
            new Harmony("com.Kman.GorillaOS").PatchAll();
        }

        private void KeepAlive()
        {
            if (!GameObject.Find("GorillaOS"))
            {
                GameObject go = new GameObject("GorillaOS");
                go.AddComponent<Main>();
            }
        }

        public void UpadateTheme(int theme)
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/motdscreen").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/screen").GetComponent<MeshRenderer>().enabled = true;
            GorillaComputer.instance.computerScreenRenderer.enabled = true;
            GorillaComputer.instance.wallScreenRenderer.enabled = true;
            GorillaComputer.instance.scoreboardRenderer.enabled = true;
            for (int i = 0; i < GorillaComputer.instance.levelScreens.Length; i++)
            {
                GorillaComputer.instance.levelScreens[i].GetComponent<MeshRenderer>().enabled = true;
            }
            Material mat = new Material(Shader.Find("GorillaTag/UberShader"));
            switch (theme)
            {
                case 1:
                    mat.color = Color.gray * 0.4f;
                    break;

                case 2:
                    mat.color = Color.magenta * 0.2f;
                    break;

                case 3:
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/motdscreen").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/screen").GetComponent<MeshRenderer>().enabled = false;
                    GorillaComputer.instance.computerScreenRenderer.enabled = false;
                    GorillaComputer.instance.wallScreenRenderer.enabled = false;
                    GorillaComputer.instance.scoreboardRenderer.enabled = false;
                    for (int i = 0; i < GorillaComputer.instance.levelScreens.Length; i++)
                    {
                        GorillaComputer.instance.levelScreens[i].GetComponent<MeshRenderer>().enabled = false;
                    }
                    return;
                    break;

                case 4:

                    break;
            }
            GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/motdscreen").GetComponent<MeshRenderer>().material = mat;
            GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/screen").GetComponent<MeshRenderer>().material = mat;
            GorillaComputer.instance.computerScreenRenderer.material=mat;
            GorillaComputer.instance.wallScreenRenderer.material = mat;
            GorillaComputer.instance.scoreboardRenderer.material = mat;
            for (int i = 0; i < GorillaComputer.instance.levelScreens.Length; i++)
            {
                GorillaComputer.instance.levelScreens[i].badMaterial = mat;
                GorillaComputer.instance.levelScreens[i].goodMaterial = mat;
                GorillaComputer.instance.levelScreens[i].UpdateText(GorillaComputer.instance.levelScreens[i].myText.text, true);
            }
        }

        public void Reload()
        {
            StartCoroutine(ReloadPage());
        }

        public IEnumerator ReloadPage()
        {
            yield return new WaitForSeconds(1);
            GorillaComputer.instance.UpdateScreen();
            yield break;
        }

        public void Refresh()
        {
            list = "";
            BaseUnityPlugin[] arr = GameObject.Find("BepInEx_Manager").GetComponents<BaseUnityPlugin>();
            Moduals = arr.ToList();
            foreach (BaseUnityPlugin comp in Moduals)
            {
                if (comp == Moduals.ToArray()[SupportPatch.focusedModual - 1])
                {
                    list += comp.enabled ? $"> <color=green>[+]</color> : {comp.GetType().Name.ToUpper()}\n" : $"> <color=red>[-]</color> : {comp.GetType().Name.ToUpper()}\n";
                }
                else
                {
                    list += comp.enabled ? $"<color=green>[+]</color> : {comp.GetType().Name.ToUpper()}\n" : $"<color=red>[-]</color> : {comp.GetType().Name.ToUpper()}\n";
                }
            }
        }

        private void Update()
        {
            KeepAlive();
            if (Time.time > refresh + 5f)
            {
                list = "";
                BaseUnityPlugin[] arr = GameObject.Find("BepInEx_Manager").GetComponents<BaseUnityPlugin>();
                Moduals = arr.ToList();
                foreach (BaseUnityPlugin comp in Moduals)
                {
                    if (comp == Moduals.ToArray()[SupportPatch.focusedModual-1])
                    {
                        list += comp.enabled ? $"> <color=green>[+]</color> : {comp.GetType().Name.ToUpper()}\n" : $"> <color=red>[-]</color> : {comp.GetType().Name.ToUpper()}\n";
                    }
                    else
                    {
                        list += comp.enabled ? $"<color=green>[+]</color> : {comp.GetType().Name.ToUpper()}\n" : $"<color=red>[-]</color> : {comp.GetType().Name.ToUpper()}\n";
                    }
                }
            }
        }
    }
}