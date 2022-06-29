using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace GamePlay
{
    public class Knife : MonoBehaviour
    {
        public void Setup(InputSystem inputSystem)
        {
            inputSystem.OnClick += RotateKnife;
        }

        private void RotateKnife()
        {
            Debug.Log("Rotate");
        }
    }
}
