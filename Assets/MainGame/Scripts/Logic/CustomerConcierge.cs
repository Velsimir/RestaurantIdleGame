using System.Collections.Generic;
using MainGame.Scripts.Logic;
using MainGame.Scripts.Logic.Npc;
using MainGame.Scripts.Logic.Tables.ManagerTable;
using UnityEngine;

public class CustomerConcierge : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _fillFactor = 0.4f;
    [SerializeField] private float _fillingRate = 0.1f;
    [SerializeField] private QueuePositionHandler _queuePositionHandler;
    [SerializeField] private Transform _spawnPoint;
    
    private readonly List<Customer> _customers = new List<Customer>();
    
    private float _timeSinceLastInvite;
    private CustomerInviter _customerInviter;
    
    private int CurrentQueueSize => _customers.Count;
    private int MaxFreeSeats => _queuePositionHandler.CountPositions;

    private void Awake()
    {
        _customerInviter = new CustomerInviter(_spawnPoint);
    }

    private void Update()
    {
        _timeSinceLastInvite += Time.deltaTime;
        
        if (ShouldInviteNewGuest())
        {
            InviteGuest();
            _timeSinceLastInvite = 0f;
        }
    }

    private bool ShouldInviteNewGuest()
    {
        float targetQueueSize = _fillFactor * MaxFreeSeats * (1 - Mathf.Exp(-_fillingRate * _timeSinceLastInvite));
        
        return CurrentQueueSize < targetQueueSize;
    }

    private void InviteGuest()
    {
        if (_queuePositionHandler.TryGetQueuePosition(out  QueuePosition queuePosition))
        {
            _customers.Add(_customerInviter.Invite(queuePosition));
        }
    }
}
