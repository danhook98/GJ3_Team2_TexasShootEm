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
        [SerializeField] private AudioClipSOEvent playSfx;

        [Header("Audio Clip SO")]
        [SerializeField] private AudioClipSO entityDeathSfx;
        [SerializeField] private AudioClipSO entityShootSfx;

        private Animator _entityAnim;
        private bool _isDead = false;

        private void Awake()
        {
            _entityAnim = GetComponent<Animator>();
        }

        private void Update() // TODO: Remove this, this is here only for testing purposes.
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(EntityShoot());
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(EntityDeath());
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(EntityAim());
            }
        }

        public IEnumerator EntityShoot()
        {
            if (!_isDead) // Condition needed to avoid playing gunshot SFX when dead.
            {
                Debug.Log("Starting shoot coroutine");
                _entityAnim.SetTrigger("Shoot");
                yield return new WaitForSeconds(0.2f);
                playSfx.Invoke(entityShootSfx);
                entityShoot.Invoke(new Empty());
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        public IEnumerator EntityDeath()
        {
            _isDead = true;
            Debug.Log("Starting death coroutine");
            _entityAnim.SetTrigger("Death");
            entityDeath.Invoke(new Empty());
            playSfx.Invoke(entityDeathSfx);
            yield return new WaitForSeconds(0.9f);
            _entityAnim.SetTrigger("StayDead");
        }

        public IEnumerator EntityAim()
        {
            if (_isDead) yield return null;
            
            Debug.Log("Starting aim coroutine");
            _entityAnim.SetTrigger("Aim");
            entityAim.Invoke(new Empty());
            yield return new WaitForSeconds(0.5f);
            _entityAnim.SetTrigger("StayAiming");
        }
    }
}