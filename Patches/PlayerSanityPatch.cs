using System;
using HarmonyLib;
using UnityEngine;

namespace MorePeaceful.Patches
{

    [HarmonyPatch(typeof(PlayerSanity), nameof(PlayerSanity.Update))]
    internal class PlayerSanityPatch
    {
        // Using this patch to make the player sanity null when in peacefull mode
        public static bool Prefix(PlayerSanity __instance)
        {
            if(MorePeaceful.Instance.Passive)
            {
                __instance._sanity = 1;
                return false;
            }
            return true;
        }
    }
}
