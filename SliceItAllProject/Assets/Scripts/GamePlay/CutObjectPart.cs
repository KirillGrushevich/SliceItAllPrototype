using System;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    public class CutObjectPart : MonoBehaviour
    {
        [SerializeField] private Rigidbody objectRigidbody;
        [SerializeField] private Collider objectCollider;

        public void Activate(Vector3 force)
        {
            gameObject.SetActive(true);
            objectCollider.enabled = true;
            objectRigidbody.isKinematic = false;
            objectRigidbody.AddForce(force, ForceMode.Impulse);
        }
        
        public void Deactivate()
        {
            gameObject.SetActive(false);
            objectCollider.enabled = false;
            objectRigidbody.isKinematic = true;
        }

        private void Reset()
        {
            objectRigidbody = GetComponent<Rigidbody>();
            objectCollider = GetComponent<BoxCollider>();
        }
    }
}
