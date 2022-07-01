using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class CutObject : MonoBehaviour
    {
        [SerializeField] private MeshRenderer mainObject;
        [SerializeField] private CutObjectPart[] leftObjects;
        [SerializeField] private CutObjectPart[] rightObjects;
        [SerializeField] private float minHitForce = 1f;
        [SerializeField] private float maxHitForce = 2f;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private int scorePoints;
        public int ScorePoints => scorePoints;

        private void Start()
        {
            PrepareObjects(false, null);
        }

        public void Cut(Material cutMaterial)
        {
            if (mainObject != null)
            {
                mainObject.enabled = false;
            }
            
            boxCollider.enabled = false;
            PrepareObjects(true, cutMaterial);
        }

        private void PrepareObjects(bool isActivate, Material cutMaterial)
        {
            PrepareObjects(ref leftObjects, isActivate, Vector3.left, cutMaterial);
            PrepareObjects(ref rightObjects, isActivate, Vector3.right, cutMaterial);
        }

        private void PrepareObjects(ref CutObjectPart[] parts, bool isActivate, Vector3 hitDirection, Material cutMaterial)
        {
            foreach (var objectPart in parts)
            {
                if (isActivate)
                {
                    objectPart.Activate(hitDirection * Random.Range(minHitForce, maxHitForce), cutMaterial);
                }
                else
                {
                    objectPart.Deactivate();
                }
            }
        }

        private void Reset()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
    }
}
