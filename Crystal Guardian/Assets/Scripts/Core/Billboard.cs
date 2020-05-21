﻿using UnityEngine;

namespace CG.Core
{
    public class Billboard : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}
