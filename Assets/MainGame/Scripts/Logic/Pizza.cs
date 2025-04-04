using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pizza : MonoBehaviour, ISpawnable
{
    private Transform _transform;
    private Collider _collider;

    public event Action<ISpawnable> Dissapear;

    private void Awake()
    {
        _transform = transform;
        _collider = GetComponent<Collider>();
    }

    private void OnDisable()
    {
        Dissapear?.Invoke(this);
    }

    public Bounds GetBounds()
    {
        return _collider.bounds;
    }
}