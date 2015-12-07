using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Utility
{
    public static class TeamLogic
    {
        public const string TeamA = "TeamA";
        public const string TeamB = "TeamB";
        public const string Neutral = "Neutral";

        public static string oppositeTeam(string thisPlayer)
        {
            if (thisPlayer == TeamA) return TeamB;
            else return TeamA;
        }

        public static bool areEnemies(GameObject a, GameObject b)
        {
            if (a == null || b == null) return false;
            // Is b on the opposite team of a?
            return a.tag != b.tag;//a.CompareTag(Utility.TeamLogic.oppositeTeam(b.tag));
        }

        public static bool areAllies(GameObject a, GameObject b)
        {
            return !areEnemies(a, b);
        }

        public static List<GameObject> ObjsInRange(GameObject unit, float radius)
        {
            return Physics.OverlapSphere(unit.transform.position, radius)
                          .Select(x => x.gameObject)
                          .Where(x => x.name != "AI_Collider")
                          .Where(x => x.tag == TeamA || x.tag == TeamB || x.tag == Neutral)
                          .ToList();
        }

        public static List<GameObject> allyObjsInRange(GameObject unit, float radius)
        {
            return ObjsInRange(unit, radius)
						.Where(x => TeamLogic.areAllies(x.gameObject, unit) && x.name != "AI_Collider")
                        .ToList();
        }

        public static List<Combat> allyCombatsInRange(GameObject unit, float radius)
        {
           return allyObjsInRange(unit, radius)
                  .Select(x => x.GetComponent<Combat>())
                  .Where(x => x != null)
                  .ToList();
        }

        public static List<GameObject> enemyObjsInRange(GameObject unit,float radius)
        {
            return ObjsInRange(unit, radius)
                    .Where(x => TeamLogic.areEnemies(x.gameObject, unit) && x.name != "AI_Collider")
                    .ToList();
        }

        public static List<Combat> enemyCombatsInRange(GameObject unit, float radius)
        {
            return enemyObjsInRange(unit, radius)
                        .Select(x => x.GetComponent<Combat>())
                        .Where(x => x != null)
                        .ToList();
        }

    }
}