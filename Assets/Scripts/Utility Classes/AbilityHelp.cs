using UnityEngine;
using System.Collections;


namespace Utility
{

    public static class AbilityHelp
    {
        public static Vector3 getTerrain_UnderMouse()
        {
            RaycastHit hit;

            // Get the point on the terrain where the mouse is
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0F, (1 << 8));

            return hit.point;
        }

        public static GameObject getSelectable_UnderMouse()
        {
            RaycastHit hit;

            // Get the point on the terrain where the mouse is
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0F, (1 << 9));

            return hit.collider != null ? hit.collider.gameObject : null;
        }

    }
}