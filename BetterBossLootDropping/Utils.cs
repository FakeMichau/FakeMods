using System.Collections;
using RoR2;
using UnityEngine;

namespace BetterBossLootDropping
{
    internal class Utils
    {
        public static IEnumerator DelayedDrop(PickupIndex pickupIndex, Vector3 position, Vector3 vector, float delayInSecs)
        {
            yield return new WaitForSeconds(delayInSecs);
            PickupDropletController.CreatePickupDroplet(pickupIndex, position, vector);
        }
    }
}
