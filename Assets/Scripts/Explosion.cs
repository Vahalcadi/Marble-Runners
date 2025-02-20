using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void StartExplosion()
    {
        foreach (var particle in particles)
            particle.Play();

        AudioManager.Instance.PlaySFXDirectly(17, null);
    }
}
