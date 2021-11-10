using System;
using System.Collections;
using Source.Infrastructure.Services.AssetManagement;
using Source.Views.Balls;
using UnityEngine;

namespace Source.Views
{
    public class BallVFX : MonoBehaviour
    {
        [SerializeField] private BallView _view;
        private ParticleSystem _popVFXPrefab;
        private WaitForSeconds _waitUntilVFXStopPlaying;
        public void Construct(ParticleSystem popVFXPrefab)
        {
            _popVFXPrefab = popVFXPrefab;
            _waitUntilVFXStopPlaying = new WaitForSeconds(_popVFXPrefab.main.duration + 0.1f);
        }

        private void Start() => _view.Clicked += PlayPopVFX;

        private void OnDestroy() => _view.Clicked -= PlayPopVFX;

        private void PlayPopVFX(BallView obj)
        {
            ParticleSystem particles = Instantiate(_popVFXPrefab, transform.position, Quaternion.identity);
            StartCoroutine(DestroyParticle(particles));
        }

        private IEnumerator DestroyParticle(ParticleSystem particles)
        {
            yield return _waitUntilVFXStopPlaying;
            Destroy(particles.gameObject);
        }
    }
}