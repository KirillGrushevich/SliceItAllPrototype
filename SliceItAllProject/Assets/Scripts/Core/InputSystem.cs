using System;
using UnityEngine;

namespace Core
{
    public class InputSystem : MonoBehaviour
    {
        public event Action OnClick;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick?.Invoke();
            }
        }
    }
}