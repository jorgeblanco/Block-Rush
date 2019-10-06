using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class ParticleSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip bornSound;
    [SerializeField] private AudioClip deathSound;

    private ParticleSystem _particle;
    private AudioSource _audioSource;
    private int _currentNumberOfParticles;

    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var particleCount = _particle.particleCount;
        if (particleCount < _currentNumberOfParticles)
        {
            if (deathSound)
            {
                _audioSource.PlayOneShot(deathSound);
            }
        }
        if (particleCount > _currentNumberOfParticles)
        {
            if (bornSound)
            {
                _audioSource.PlayOneShot(bornSound);
            }
        }

        _currentNumberOfParticles = particleCount;
    }
}
