using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexasShootEm
{
    public class EnemyController : MonoBehaviour
    {
        private GameObject _evilTubbo;
        
        // Start is called before the first frame update
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
    }
}
