using UnityEngine;
using UnityEngine.UI;

namespace CG.Core
{
    public class FireBullets : MonoBehaviour
    {
        public Transform player;
        public Text ammoText;
        public int maxAmmo = 10;

        Inventory inventory;
        PoolManager poolManager;
        int ammo;

        private void Start()
        {
            inventory = Inventory.instance;
            poolManager = PoolManager.instance;
            ammo = maxAmmo;
        }

        private void Update()
        {
            ammoText.text = $"{ammo}/{maxAmmo}";
            if (inventory.isOpened)
                return;
            Fire();
        }

        public int GetBulletsNumber()
        {
            return ammo;
        }

        public void AddBulletsToStack(int amount)
        {
            ammo += amount;
        }

        private void Fire()
        {
            if (Input.GetMouseButtonDown(0) && ammo > 0)
            {
                for (int i = 0; i < poolManager.bulletList.Count; i++)
                {
                    if (!poolManager.bulletList[i].activeInHierarchy)
                    {
                        poolManager.bulletList[i].SetActive(true);
                        Physics.IgnoreCollision(poolManager.bulletList[i].GetComponent<Collider>(), player.GetComponent<Collider>());
                        poolManager.bulletList[i].transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                        poolManager.bulletList[i].transform.forward = Camera.main.transform.forward;
                        break;
                    }
                    else
                    {
                        // last bullet existing
                        if (i == poolManager.bulletList.Count - 1)
                        {
                            GameObject newBullet = Instantiate(poolManager.bullet);
                            newBullet.transform.parent = poolManager.transform;
                            newBullet.SetActive(false);
                            poolManager.bulletList.Add(newBullet);
                        }
                    }
                }
                ammo--;
            }
        }
    }
}
