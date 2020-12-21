using UnityEngine;

namespace custom.controllers
{
    public class CustomController
    {
        public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.up, new Vector3(0, y, 0));
            float distance;
            xy.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        public static Vector3 NonSnapToGrid(Vector3 noGridSnap)
        {
            noGridSnap = new Vector3(
                            GameManager.MyInstance.Grid.WorldToLocal(GameManager.MyInstance.MousePosition).x,
                            0,
                            GameManager.MyInstance.Grid.WorldToLocal(GameManager.MyInstance.MousePosition).y
                            );

            return noGridSnap;
        }
    }
}

