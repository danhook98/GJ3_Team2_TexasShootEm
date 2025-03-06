using System.Collections;
using UnityEngine;
using TexasShootEm.EventSystem;

namespace TexasShootEm
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private VoidEvent shoot;
        [SerializeField] private VoidEvent death;

        private Animator _evilTubboAnim;
        private bool _isDead = false;

        private void Start()
        {
            var evilTubbo = GameObject.FindWithTag("Enemy");
            _evilTubboAnim = evilTubbo.GetComponent<Animator>();
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
                _evilTubboAnim.SetTrigger("Shoot");
                yield return new WaitForSeconds(0.5f);
                shoot.Invoke(new Empty());
                yield return new WaitForSeconds(0.5f);
            }
        }

        public IEnumerator Death()
        {
            _isDead = true;
            Debug.Log("Starting death coroutine");
            _evilTubboAnim.SetTrigger("Death");
            death.Invoke(new Empty());
            yield return new WaitForSeconds(0.7f);
            _evilTubboAnim.SetTrigger("StayDead");
        }
    }
}
