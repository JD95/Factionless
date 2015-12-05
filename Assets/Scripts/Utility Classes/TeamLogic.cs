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
            // Is b on the opposite team of a?
            return a.tag != b.tag;//a.CompareTag(Utility.TeamLogic.oppositeTeam(b.tag));
        }

        public static bool areAllies(GameObject a, GameObject b)
        {
            return !areEnemies(a, b);
        }

        public static List<GameObject> ObjsIntRange(GameObject unit, float radius)
        {
            return Physics.OverlapSphere(unit.transform.position, 5.0f)
                          .Select(x => x.gameObject).ToList();
        }

        public static List<GameObject> allyObjsInRange(GameObject unit, float radius)
        {
            return Physics.OverlapSphere(unit.transform.position, 5.0f)
						.Where(x => TeamLogic.areAllies(x.gameObject, unit) && x.name != "AI_Collider")
                        .Select(x => x.gameObject).ToList();
        }

        public static List<Combat> allyCombatsInRange(GameObject unit, float radius)
        {
           return allyObjsInRange(unit, radius)
                  .Select(x => x.GetComponent<Combat>())
                  .ToList();
        }

        public static List<GameObject> enemyObjsInRange(GameObject unit,float radius)
        {
            return Physics.OverlapSphere(unit.transform.position, 5.0f)
                          .Where(x => TeamLogic.areEnemies(x.gameObject, unit) && x.name != "AI_Collider")
                          .Select(x => x.gameObject).ToList();
        }

        public static List<Combat> enemyCombatsInRange(GameObject unit, float radius)
        {
            return enemyObjsInRange(unit, radius)
                        .Select(x => x.GetComponent<Combat>())
                        .ToList();
        }

    }
}