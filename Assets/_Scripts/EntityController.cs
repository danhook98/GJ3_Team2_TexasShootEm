using System.Collections;
using UnityEngine;
using TexasShootEm.EventSystem;

namespace TexasShootEm
{
    public class EntityController : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private VoidEvent entityShoot;
        [SerializeField] private VoidEvent entityDeath;
        [SerializeField] private VoidEvent entityAim;

        [Header("Animators")] 
        //[SerializeField] private Animator sheriffAnim;
        [SerializeField] private Animator entityAnim;
        
        private bool _isDead = false;

        private void Start()
        {
            entityAnim = GetComponent<Animator>();
        }

        private void Update() // TODO: Remove this, this is here only for testing purposes.
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               // StartCoroutine(EvilTubboShoot());
                StartCoroutine(EntityShoot());
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //StartCoroutine(EvilTubboDeath());
                StartCoroutine(EntityDeath());
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(EntityAim());
            }
        }

       /* public IEnumerator EvilTubboShoot()
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
        } */

        public IEnumerator EntityShoot()
        {
            if (!_isDead) // Condition needed to avoid playing gunshot SFX when dead.
            {
                Debug.Log("Starting shoot coroutine");
                entityAnim.SetTrigger("Shoot");
                yield return new WaitForSeconds(0.2f);
                entityShoot.Invoke(new Empty());
                yield return new WaitForSeconds(0.5f);
            }
        }
            
        
        public IEnumerator EntityDeath()
        {
            _isDead = true;
            Debug.Log("Starting death coroutine");
            entityAnim.SetTrigger("Death");
            entityDeath.Invoke(new Empty());
            yield return new WaitForSeconds(0.9f);
            entityAnim.SetTrigger("StayDead");
        }

        public IEnumerator EntityAim()
        {
            if (_isDead) yield return null;
            
            Debug.Log("Starting aim coroutine");
            entityAnim.SetTrigger("Aim");
            entityAim.Invoke(new Empty());
            yield return new WaitForSeconds(0.5f);
            entityAnim.SetTrigger("StayAiming");
        }
    }
}