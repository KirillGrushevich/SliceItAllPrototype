using System;
using UnityEngine;

namespace GamePlay
{
    public class CameraController : MonoBehaviour
    {
        [Serializable]
        private class ParallaxObject
        {
            public Transform target;
            public float speedFactor;

            private Vector3 startPosition;
            
            public void Setup()
            {
                startPosition = target.localPosition;
            }

            public void Reset()
            {
                target.localPosition = startPosition;
            }

            public void Translate(float diff)
            {
                var pos = target.transform.localPosition;
                pos.x += diff * speedFactor;
                target.localPosition = pos;
            }
        }
        
        [Header("Movement")]
        [SerializeField] private float upMaxBound;
        [SerializeField] private float downMaxBound;
        [SerializeField] private float deadZone;
        [SerializeField] private float verticalSmooth;
        [SerializeField] private float horizontalSmooth;
        [SerializeField] private float maxYBoundForSmooth;
        [SerializeField] private AnimationCurve verticalSmoothCurve;

        [Header("Parallax")] 
        [SerializeField] private ParallaxObject[] parallaxObjects;
        
        private Transform target;
        private Vector3 offset;

        public void Setup(Transform followTarget)
        {
            target = followTarget;
            offset = transform.position - target.position;

            foreach (var parallaxObject in parallaxObjects)
            {
                parallaxObject.Setup();
            }
        }

        public void ResetPosition()
        {
            transform.position = target.position + offset;
            
            foreach (var parallaxObject in parallaxObjects)
            {
                parallaxObject.Reset();
            }
        }

        private void FixedUpdate()
        {
            if (target == null)
            {
                return;
            }

            var previewZ = transform.position.z;
            
            var diffY = target.position.y - transform.position.y;
            var diffYAbs = Mathf.Abs(diffY);

            var pos = target.position + offset;
            var smoothFactor = verticalSmoothCurve.Evaluate(diffYAbs / maxYBoundForSmooth);
            pos.y = Mathf.Lerp(transform.position.y, pos.y, smoothFactor * Time.fixedDeltaTime * verticalSmooth);
            pos.z = Mathf.Lerp(transform.position.z, pos.z, horizontalSmooth * Time.fixedTime);

            if (diffYAbs > deadZone)
            {
                if (diffY > upMaxBound)
                {
                    var offsetY = diffY - upMaxBound;
                    pos.y += offsetY;
                }
                else if (diffY < downMaxBound)
                {
                    var offsetY = downMaxBound - diffY;
                    pos.y -= offsetY;
                }
            }

            if (pos.z < transform.position.z)
            {
                pos.z = transform.position.z;
            }
            
            transform.position = pos;

            var diff = pos.z - previewZ;
            if (diff <= 0f)
            {
                return;
            }

            foreach (var parallaxObject in parallaxObjects)
            {
                parallaxObject.Translate(diff);
            }
        }
    }
}