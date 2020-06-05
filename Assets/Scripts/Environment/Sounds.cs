using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private King _king;
    [SerializeField] private AudioSource _pigDie;
    [SerializeField] private AudioSource _exitRoom;

    private PigVulnerability[] _warriors;

    private void Awake()
    {
        _warriors = FindObjectsOfType<PigVulnerability>();
    }

    private void OnEnable()
    {
        foreach (PigVulnerability pig in _warriors)
        {
            pig.Died += OnPigDied;
        }

        _king.MoveThroughExitDoor += OnMoveThroughExitDoor;
    }

    private void OnPigDied(PigVulnerability pig)
    {
        _pigDie.Play();
        pig.Died -= OnPigDied;

    }

    private void OnDisable()
    {
        _king.MoveThroughExitDoor -= OnMoveThroughExitDoor;
    }

    private void OnMoveThroughExitDoor()
    {
        _exitRoom.Play();
    }
}
