using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BowAnimEventHandler : MonoBehaviour
    {
        public ProjectileSpawner _projectileSpawner;

        private void SpawnProjectile ()
        {
            _projectileSpawner.SpawnArrow();
        }

        private void SpawnFixArrow()
        {
            _projectileSpawner._arrow.gameObject.SetActive(true);
        }

    }
