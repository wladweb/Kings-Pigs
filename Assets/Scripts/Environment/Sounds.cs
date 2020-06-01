using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private King _king;
    [SerializeField] private AudioSource _pigDie;
    [SerializeField] private AudioSource _exitRoom;

    private Pig[] _warriors;

    private void Awake()
    {
        _warriors = FindObjectsOfType<Pig>();
    }

    private void OnEnable()
    {
        foreach (Pig pig in _warriors)
        {
            pig.Died += OnPigDied;
        }

        _king.MoveThroughExitDoor += OnMoveThroughExitDoor;
    }

    private void OnPigDied(Pig pig)
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
