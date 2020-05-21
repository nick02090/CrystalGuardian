using System.Collections.Generic;
using UnityEngine;

namespace CG.Core
{
    public class PoolManager : MonoBehaviour
    {
        #region Singleton
        public static PoolManager instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion

        public GameObject bullet;
        public int spawnCount;

        public List<GameObject> bulletList;

        private void Start()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject bullet = Instantiate(this.bullet);
                bulletList.Add(bullet);
                bullet.transform.parent = transform;
                bullet.SetActive(false);
            }
        }
    }
}
