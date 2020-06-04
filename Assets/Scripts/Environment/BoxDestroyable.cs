using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ParticleSystem))]
public class BoxDestroyable : MonoBehaviour
{
    private ParticleSystem _fog;
    private SpriteRenderer _renderer;
    private AudioSource _sound;

    private void Start()
    {
        _fog = GetComponent<ParticleSystem>();
        _renderer = GetComponent<SpriteRenderer>();
        _sound = GetComponent<AudioSource>();
    }

    public void DestroyBox()
    {
        _renderer.color = new Color(0, 0, 0, 0);
        _sound.Play();
        _fog.Play();
    }
}
