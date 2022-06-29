using System;
using UnityEngine;

namespace GamePlay
{
    public class CameraController : MonoBehaviour
    {
        private Transform target;
        private Vector3 offset;
        
        public void Setup(Transform followTarget)
        {
            target = followTarget;
            offset = transform.position - target.position;
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            var pos = target.position + offset;
            transform.position = pos;
        }
    }
}