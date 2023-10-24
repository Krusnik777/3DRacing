using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(CarCameraController))]
    public abstract class CarCameraComponent : MonoBehaviour
    {
        protected Car m_car;
        protected Camera m_camera;

        public virtual void SetPropertioes(Car car, Camera camera)
        {
            m_car = car;
            m_camera = camera;
        }
    }
}
