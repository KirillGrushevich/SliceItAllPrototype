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
            PrepareObjects(false);
        }

        public void Cut()
        {
            if (mainObject != null)
            {
                mainObject.enabled = false;
            }
            
            boxCollider.enabled = false;
            PrepareObjects(true);
        }

        private void PrepareObjects(bool isActivate)
        {
            PrepareObjects(ref leftObjects, isActivate, Vector3.left);
            PrepareObjects(ref rightObjects, isActivate, Vector3.right);
        }

        private void PrepareObjects(ref CutObjectPart[] parts, bool isActivate, Vector3 hitDirection)
        {
            foreach (var objectPart in parts)
            {
                if (isActivate)
                {
                    objectPart.Activate(hitDirection * Random.Range(minHitForce, maxHitForce));
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
