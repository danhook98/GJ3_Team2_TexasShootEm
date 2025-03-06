using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexasShootEm
{
    public class EnemyController : MonoBehaviour
    {
        private GameObject _evilTubbo;
        
        private void Start()
        {
            _evilTubbo = GameObject.FindWithTag("Enemy");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Shoot());
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(Death());
            }
        }
        
        public IEnumerator Shoot()
        {
            Debug.Log("Starting shoot coroutine");
            yield return null;
        }

        public IEnumerator Death()
        {
            Debug.Log("Starting death coroutine");
            yield return null;
        }
        /*
         *  Death Coroutine
         * 1. Set isDead to true through the evil Tubbo animator
         * 2. Play relevant SFX & Music using the Audio Manager
         * 3. Wait until animation finishes
         *
         * Shoot Coroutine
         * 1. Trigger Shoot using the animation controller, set isNotShooting to false
         * 2. Play relevant SFX & Music using the Audio Manager
         * 3. Wait until animation finishes
         * 4. Set isNotShooting to true so EvilTubbo goes back to his default state
         *
         */
    }
}
