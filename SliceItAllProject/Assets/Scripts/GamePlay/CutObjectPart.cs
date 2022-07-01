using System;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    public class CutObjectPart : MonoBehaviour
    {
        [SerializeField] private Rigidbody objectRigidbody;
        [SerializeField] private Collider objectCollider;
        [SerializeField] private MeshRenderer cutMeshRenderer;

        public void Activate(Vector3 force, Material cutMaterial)
        {
            gameObject.SetActive(true);
            objectCollider.enabled = true;
            objectRigidbody.isKinematic = false;
            objectRigidbody.AddForce(force, ForceMode.Impulse);

            var materials = cutMeshRenderer.sharedMaterials;
            materials[materials.Length - 1] = cutMaterial;
            cutMeshRenderer.sharedMaterials = materials;
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
