using KetchappTools.PoolParty;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrowdT
{
    public class SingleUnitView : MonoBehaviour
    {
        [SerializeField] private Renderer bodyRenderer;
        [SerializeField] private Material deathMaterial;

        [SerializeField] private Renderer weapon;

        [BoxGroup("HitParticles")]
        [SerializeField] private ParticleSystem hitParticles;
        [BoxGroup("HitParticles")]
        [SerializeField] private Transform hitParticlesSpawner;
        
        [BoxGroup("HitImpactParticles")]
        [SerializeField] private ParticleSystem hitImpactParticles;        
        [BoxGroup("HitImpactParticles")]
        [SerializeField] private Transform hitImpactParticlesSpawner;
        
        private Animator _anim;
        
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int DeathVariant = Animator.StringToHash("DeathVariant");
        private static readonly int Dance = Animator.StringToHash("Dance");
        private static readonly int DanceVariant = Animator.StringToHash("DanceVariant");

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void SetIdle(bool value)
        {
            _anim.SetBool(Idle, value);
        }

        public void SetAttack()
        {
            _anim.SetTrigger(Attack);
        }

        public void SetRandomDeath()
        {
            var variant = Random.Range(0, 3);
            _anim.SetInteger(DeathVariant, variant);
            _anim.SetTrigger(Death);
            
            ChangeColorToDeath();
        }

        private void ChangeColorToDeath()
        {
            bodyRenderer.material = deathMaterial;

            weapon.gameObject.SetActive(false);
            
            // for (var i = 0; i != weapon.materials.Length; ++i)
            // {
            //     weapon.materials[i] = deathMaterial;
            //     
            //     // var color = weapon.materials[i].color;
            //     // color.a = 30;
            //     // weapon.materials[i].color = color;
            // }
        }

        public void PlayHit()
        {
            var particle = PoolParty.Instantiate(hitParticles, hitParticlesSpawner.position, quaternion.identity);
            PoolParty.Destroy(particle, hitParticles.main.startLifetimeMultiplier);
        }
        
        public void PlayHitImpact()
        {
            var particle = PoolParty.Instantiate(hitImpactParticles, hitImpactParticlesSpawner.position, quaternion.identity);
            PoolParty.Destroy(particle, hitImpactParticles.main.startLifetimeMultiplier);
        }

        public void SetRandomDance()
        {
            var variant = Random.Range(0, 3);
            _anim.SetInteger(DanceVariant, variant);
            _anim.SetTrigger(Dance);
        }

        public void SetDance(int variant)
        {
            _anim.SetInteger(DanceVariant, variant);
            _anim.SetTrigger(Dance);
        }
        
        public float GetNormalizedTime (int layer = 0)
        {
            return _anim.GetCurrentAnimatorStateInfo (layer).normalizedTime % 1f;
        }
    }
}