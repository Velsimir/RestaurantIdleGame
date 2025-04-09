using System;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

public class Table : MonoBehaviour
{
    [field: SerializeField]public Transform _customerPoint { get; private set; }
    
    private Customer _currentCustomer;
    
    public event Action<Table> Released;


    private void TakeCustomer(Customer customer)
    {
        _currentCustomer = customer;
        _currentCustomer.EatEnded += SetFreeStatus;
    }

    private void SetFreeStatus()
    {
        _currentCustomer.EatEnded -= SetFreeStatus;
        _currentCustomer = null;
        Released?.Invoke(this);
    }
}
