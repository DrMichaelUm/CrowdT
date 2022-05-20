using System.Collections.Generic;
using KetchappTools.PoolParty;
using KetchappTools.SimpleFeedbacks;
using Unity.Mathematics;
using UnityEngine;

namespace CrowdT.Level
{
    public class Barrier : MonoBehaviour, IObservable
    {
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private float destroyParticlesOffset;

        private List<IObserver> _observers = new List<IObserver>();

        private BarrierGroup _group;
        private BarrierController _barrierController;

        private void Awake()
        {
            RegisterObserver(FindObjectOfType<PlayerInput>());

            _group = GetComponentInParent<BarrierGroup>();
            _barrierController = FindObjectOfType<BarrierController>();
        }

        public void RemoveWithNotification()
        {
            NotifyObservers();

            if (_group)
                RemoveGroup();
            else
                Remove();
        }

        private void RemoveGroup()
        {
            _group.RemoveTheGroupCompletely();            
        }

        public void Remove()
        {
            SpawnParticles();

            _barrierController.Remove(this);

            Destroy(gameObject);
        }

        private void SpawnParticles()
        {
            if (destroyParticlesOffset == 0)
            {
                Debug.LogError("DestroyParticlesOffset is 0!");

                return;
            }

            var barrierLength = transform.localScale.z;

            var particlesAmount = (int) (barrierLength / destroyParticlesOffset);

            var spawnPos = transform.position;

            spawnPos -= transform.forward * (barrierLength / 2 - destroyParticlesOffset / 2);

            for (var i = 0; i != particlesAmount; ++i)
            {
                var particle = PoolParty.Instantiate(destroyParticle, spawnPos, quaternion.identity);

                spawnPos += transform.forward * destroyParticlesOffset;
                
                PoolParty.Destroy(particle, destroyParticle.main.startLifetimeMultiplier);
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            for (var i = 0; i != _observers.Count; ++i)
            {
                _observers[i].UpdateObserver();
            }
        }
    }
}