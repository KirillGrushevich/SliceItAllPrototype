using System;
using System.Collections;
using Core;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knife : MonoBehaviour
    {
        [SerializeField] private float horizontalVelocity;
        [SerializeField] private float moveUpForce;
        [SerializeField] private float rotationUpTorque;
        [SerializeField] private float rotationDownMaxAngularDrag;
        [SerializeField] private AnimationCurve rotationDownAngularDragCurve;
        [SerializeField] private Rigidbody knifeRigidbody;
        
        private InputSystem input;
        private Coroutine rotationCoroutine;

        private Vector3 basicPosition;
        private Quaternion basicRotation;
        private float basicAngularDrag;
        
        public void Setup(InputSystem inputSystem)
        {
            input = inputSystem;
            input.OnClick += RotateKnife;
        }

        public void ResetPosition()
        {
            Stop();
            
            transform.position = basicPosition;
            transform.rotation = basicRotation;
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
            
            
            
            //TODO First tests only
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetPosition();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //TODO for first tests only
            Stop();
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
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }

            rotationCoroutine = StartCoroutine(RotateKnifeProcess());
        }

        private IEnumerator RotateKnifeProcess()
        {
            knifeRigidbody.isKinematic = false;
            knifeRigidbody.velocity = Vector3.zero;
            
            knifeRigidbody.AddTorque(new Vector3(rotationUpTorque, 0f, 0f), ForceMode.Impulse);
            knifeRigidbody.AddForce(new Vector3(0f, moveUpForce, 0f), ForceMode.Impulse);
            
            while (Vector3.SignedAngle(transform.forward, Vector3.up, Vector3.up) > 10f)
            {
                yield return new WaitForFixedUpdate();
            }

            var angle = Vector3.SignedAngle(transform.forward, Vector3.down, Vector3.up);
            var t = 0f;
            while (angle > 10f)
            {
                yield return new WaitForFixedUpdate();
                t += Time.fixedDeltaTime;
                angle = Vector3.SignedAngle(transform.forward, Vector3.down, Vector3.up);

                var angularDragFactor = rotationDownAngularDragCurve.Evaluate(t);
                knifeRigidbody.angularDrag = Mathf.Lerp(basicAngularDrag, rotationDownMaxAngularDrag, angularDragFactor);
            }
        }

        private void Reset()
        {
            knifeRigidbody = GetComponent<Rigidbody>();
        }
    }
}
