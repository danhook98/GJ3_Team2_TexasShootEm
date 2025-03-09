using System.Collections;
using UnityEngine;
using TexasShootEm.EventSystem;

namespace TexasShootEm
{
    public class EntityController : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private VoidEvent evilShoot;
        [SerializeField] private VoidEvent evilDeath;
        [SerializeField] private VoidEvent sheriffShoot;
        [SerializeField] private VoidEvent sheriffDeath;
        [SerializeField] private VoidEvent sheriffAim;

        [Header("Animators")] 
        [SerializeField] private Animator sheriffAnim;
        [SerializeField] private Animator evilAnim;
        
        private bool _isDead = false;

        private void Update() // TODO: Remove this, this is here only for testing purposes.
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               // StartCoroutine(EvilTubboShoot());
                StartCoroutine(SheriffShoot());
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //StartCoroutine(EvilTubboDeath());
                StartCoroutine(SheriffDeath());
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(SheriffAim());
            }
        }

        public IEnumerator EvilTubboShoot()
        {
            if (!_isDead)
            {
                Debug.Log("Starting enemy shoot coroutine");
                evilAnim.SetTrigger("Shoot");
                yield return new WaitForSeconds(0.5f);
                evilShoot.Invoke(new Empty());
                yield return new WaitForSeconds(0.5f);
            }
        }

        public IEnumerator EvilTubboDeath()
        {
            _isDead = true;
            Debug.Log("Starting enemy death coroutine");
            evilAnim.SetTrigger("Death");
            evilDeath.Invoke(new Empty());
            yield return new WaitForSeconds(0.7f);
            evilAnim.SetTrigger("StayDead");
        }

        public IEnumerator SheriffShoot()
        {
            if (!_isDead) // Condition needed to avoid playing gunshot SFX when dead.
            {
                Debug.Log("Starting player shoot coroutine");
                sheriffAnim.SetTrigger("Shoot");
                yield return new WaitForSeconds(0.2f);
                sheriffShoot.Invoke(new Empty());
                yield return new WaitForSeconds(0.5f);
            }
        }
            
        
        public IEnumerator SheriffDeath()
        {
            _isDead = true;
            Debug.Log("Starting player death coroutine");
            sheriffAnim.SetTrigger("Death");
            sheriffDeath.Invoke(new Empty());
            yield return new WaitForSeconds(0.9f);
            sheriffAnim.SetTrigger("StayDead");
        }

        public IEnumerator SheriffAim()
        {
            if (_isDead) yield return null;
            
            Debug.Log("Starting player aim coroutine");
            sheriffAnim.SetTrigger("Aim");
            sheriffAim.Invoke(new Empty());
            yield return new WaitForSeconds(0.5f);
            sheriffAnim.SetTrigger("StayAiming");
        }
    }
}