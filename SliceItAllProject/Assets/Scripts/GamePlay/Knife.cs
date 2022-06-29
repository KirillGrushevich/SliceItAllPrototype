using System.Collections;
using Core;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knife : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private Rigidbody knifeRigidbody;
        
        [Header("Movement")]
        [SerializeField] private float horizontalVelocity = 1.5f;
        [SerializeField] private float moveUpForce = 5f;
        
        [Header("Rotation")]
        [SerializeField] private float rotationUpTorque = 20f;
        [SerializeField] private float rotationDownMaxAngularDrag = 6f;
        [SerializeField] private float rotationDownMaxAngle = 30f;
        [SerializeField] private AnimationCurve rotationDownAngularDragCurve;
        
        [Header("AutoRotation")]
        [SerializeField] private float autoRotationMinHeight = 3f;
        [SerializeField] private float freeFallingMaxTime = 0.5f;
        
        
        private InputSystem input;
        private Coroutine rotationCoroutine;

        private Vector3 basicPosition;
        private Quaternion basicRotation;
        private float basicAngularDrag;

        private Collider hitCollider;
        
        public void Setup(InputSystem inputSystem)
        {
            input = inputSystem;
        }

        public void Activate()
        {
            input.OnClick += RotateKnife;
            RotateKnife();
        }

        public void ResetPosition()
        {
            Stop();
            
            transform.position = basicPosition;
            transform.rotation = basicRotation;

            hitCollider = null;
            
            Release();
        }

        public void Release()
        {
            input.OnClick -= RotateKnife;
        }

        private void Start()
        {
            basicPosition = transform.position;
            basicRotation = transform.rotation;
            basicAngularDrag = knifeRigidbody.angularDrag;
        }

        private void FixedUpdate()
        {
            if (knifeRigidbody.isKinematic)
            {
                return;
            }

            transform.Translate(Time.deltaTime * horizontalVelocity * Vector3.forward, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hitCollider != null)
            {
                return;
            }
            
            Stop();
            hitCollider = other;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!knifeRigidbody.isKinematic)
            {
                return;
            }

            if (hitCollider == other)
            {
                hitCollider = null;
            }
        }

        private void Stop()
        {
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }

            knifeRigidbody.isKinematic = true;
            knifeRigidbody.angularDrag = basicAngularDrag;
        }

        private void RotateKnife()
        {
            RotateKnife(true);
        }

        private void RotateKnife(bool isJump)
        {
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }

            rotationCoroutine = StartCoroutine(RotateKnifeProcess(isJump));
        }

        private IEnumerator RotateKnifeProcess(bool isJump)
        {
            knifeRigidbody.isKinematic = false;
            
            if (isJump)
            {
                knifeRigidbody.velocity = Vector3.zero;
                knifeRigidbody.AddForce(new Vector3(0f, moveUpForce, 0f), ForceMode.Impulse);
            }

            knifeRigidbody.angularDrag = basicAngularDrag;
            knifeRigidbody.AddTorque(new Vector3(rotationUpTorque, 0f, 0f), ForceMode.Impulse);

            while (Vector3.SignedAngle(transform.forward, Vector3.up, Vector3.up) > 10f)
            {
                yield return new WaitForFixedUpdate();
            }

            hitCollider = null;

            var angle = Vector3.SignedAngle(transform.forward, Vector3.down, Vector3.up);
            var t = 0f;
            while (angle > rotationDownMaxAngle)
            {
                yield return new WaitForFixedUpdate();
                t += Time.fixedDeltaTime;
                angle = Vector3.SignedAngle(transform.forward, Vector3.down, Vector3.up);

                var angularDragFactor = rotationDownAngularDragCurve.Evaluate(t);
                knifeRigidbody.angularDrag = Mathf.Lerp(basicAngularDrag, rotationDownMaxAngularDrag, angularDragFactor);
            }
            
            knifeRigidbody.angularDrag = rotationDownMaxAngularDrag;
            yield return new WaitForSeconds(freeFallingMaxTime);

            rotationCoroutine = null;
            
            var ray = new Ray(transform.position, Vector3.down);
            if (!Physics.Raycast(ray, autoRotationMinHeight))
            {
                RotateKnife(false);
            }

        }

        private void Reset()
        {
            knifeRigidbody = GetComponent<Rigidbody>();
        }
    }
}
