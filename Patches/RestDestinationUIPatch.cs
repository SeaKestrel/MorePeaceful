using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MorePeaceful.Patches
{
    [HarmonyPatch(typeof(RestDestinationUI), nameof(RestDestinationUI.ShowMainUI))]
    internal class RestDestinationUIPatch
    {
        public static void Postfix(ref RestDestinationUI __instance)
        {
            if(GameManager.Instance.Time.Time > (6f / 24f) && GameManager.Instance.Time.Time < (20f / 24f))
            {
                float time = GameManager.Instance.Time.Time;
                float num2 = 20f / 24f;
                float num = ((!(time >= num2)) ? (num2 - time) : (num2 + (1f - time)));
                float hours = num / (1f / 24f);
                GameObject.Find("GameCanvases/TimePassCanvas/Container/FeedbackText").gameObject.GetComponent<TextMeshProUGUI>().text = "moredredge.sleepuntil.night";
                GameManager.Instance.Time.ForcefullyPassTime(hours, "feedback.pass-time-rest", TimePassageMode.SLEEP);
            } else
            {
                GameObject.Find("GameCanvases/TimePassCanvas/Container/FeedbackText").gameObject.GetComponent<TextMeshProUGUI>().text = "moredredge.sleepuntil.morning";
            }
        }
    }
}
