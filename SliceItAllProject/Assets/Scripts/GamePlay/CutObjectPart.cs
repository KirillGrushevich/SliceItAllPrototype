using System;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]
    public class CutObjectPart : MonoBehaviour
    {
        [SerializeField] private Rigidbody objectRigidbody;
        [SerializeField] private BoxCollider boxCollider;

        public void Activate(Vector3 force)
        {
            boxCollider.enabled = true;
            objectRigidbody.isKinematic = false;
            objectRigidbody.AddForce(force, ForceMode.Impulse);
        }
        
        public void Deactivate()
        {
            boxCollider.enabled = false;
            objectRigidbody.isKinematic = true;
        }

        private void Reset()
        {
            objectRigidbody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
        }
    }
}
