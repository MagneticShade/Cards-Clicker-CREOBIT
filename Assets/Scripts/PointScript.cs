using System.Collections.Generic;

using UnityEngine;

public class PointScript : MonoBehaviour
{
    private ParticleSystem particle;

    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), colliders);
        if (colliders.Count > 0)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }

        }
    }


    public void SetParticleDuration(float lifeTime, float duration)
    {
        particle.Stop();
        particle.Clear();
        var main = particle.main;
        main.startLifetime = lifeTime;
        main.duration = duration;

    }
}
