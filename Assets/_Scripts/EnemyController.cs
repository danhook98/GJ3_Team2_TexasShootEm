using System.Collections;
using UnityEngine;
using TexasShootEm.EventSystem;
using UnityEngine.Serialization;

namespace TexasShootEm
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private VoidEvent shoot;
        [SerializeField] private VoidEvent death;

        [Header("Animators")] 
        [SerializeField] private Animator sheriffAnim;
        [SerializeField] private Animator evilAnim;
        
        //private Animator evilTubboAnim;
        private bool _isDead = false;

        private void Start()
        {
            evilAnim = GetComponent<Animator>();
        }

        private void Update() // TODO: Remove this, this is here only for testing purposes.
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
            if (!_isDead)
            {
                Debug.Log("Starting shoot coroutine");
                evilAnim.SetTrigger("Shoot");
                yield return new WaitForSeconds(0.5f);
                shoot.Invoke(new Empty());
                yield return new WaitForSeconds(0.5f);
            }
        }

        public IEnumerator Death()
        {
            _isDead = true;
            Debug.Log("Starting death coroutine");
            evilAnim.SetTrigger("Death");
            death.Invoke(new Empty());
            yield return new WaitForSeconds(0.7f);
            evilAnim.SetTrigger("StayDead");
        }
    }
}

// Trigger names for Sheriff Tubbo: Aim, Shoot, Death, StayDead
