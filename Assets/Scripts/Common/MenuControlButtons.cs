using UnityEngine;

namespace Racing
{
    public class MenuControlButtons : MonoBehaviour
    {
        public static bool IsLeft, IsRight, IsUp, IsDown;
        private float lastX, lastY;

        private void Update()
        {
            float x = Input.GetAxis("MenuHorizontal");
            float y = Input.GetAxis("MenuVertical");

            IsLeft = false;
            IsRight = false;
            IsUp = false;
            IsDown = false;

            if (lastX != x)
            {
                if (x == -1)
                    IsLeft = true;
                else if (x == 1)
                    IsRight = true;
            }

            if (lastY != y)
            {
                if (y == -1)
                    IsDown = true;
                else if (y == 1)
                    IsUp = true;
            }

            lastX = x;
            lastY = y;
        }
    }
}
