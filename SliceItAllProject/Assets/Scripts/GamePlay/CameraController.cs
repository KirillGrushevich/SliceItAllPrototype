using UnityEngine;

namespace GamePlay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float upMaxBound;
        [SerializeField] private float downMaxBound;
        [SerializeField] private float deadZone;
        [SerializeField] private float verticalSmooth;
        [SerializeField] private float maxYBoundForSmooth;
        [SerializeField] private AnimationCurve verticalSmoothCurve;
        
        private Transform target;
        private Vector3 offset;

        public void Setup(Transform followTarget)
        {
            target = followTarget;
            offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            if (target == null)
            {
                return;
            }

            var diffY = target.position.y - transform.position.y;
            var diffYAbs = Mathf.Abs(diffY);

            var pos = target.position + offset;
            var smoothFactor = verticalSmoothCurve.Evaluate(diffYAbs / maxYBoundForSmooth);
            pos.y = Mathf.Lerp(transform.position.y, pos.y, smoothFactor * Time.fixedDeltaTime * verticalSmooth);

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

            transform.position = pos;
        }
    }
}