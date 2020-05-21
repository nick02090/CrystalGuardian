using UnityEngine;

namespace CG.Core
{
    public class Crystal : MonoBehaviour
    {
        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
        }
    }
}