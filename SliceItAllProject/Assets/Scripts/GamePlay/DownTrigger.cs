using UnityEngine;

namespace GamePlay
{
    public class DownTrigger : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            other.gameObject.SetActive(false);
        }
    }
}
