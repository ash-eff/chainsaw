using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public GameObject splatPrefab;

    private ParticleSystem particleSys;
    private ParticleSystem.Particle[] particles;

    private void Awake()
    {
        particleSys = GetComponent<ParticleSystem>();
        if (particles == null || particles.Length < particleSys.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[particleSys.main.maxParticles];
        }
    }

    private void LateUpdate()
    {
        int numParticlesAlive = particleSys.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            if (particles[i].remainingLifetime < 0.01f && particles[i].remainingLifetime > 0.00f)
            {
                GameObject splat = Instantiate(splatPrefab, particles[i].position, Quaternion.identity) as GameObject;
                splat.GetComponent<Splat>().Initialize();
            }
        }

        //particleSys.SetParticles(particles, numParticlesAlive);
    }
}
