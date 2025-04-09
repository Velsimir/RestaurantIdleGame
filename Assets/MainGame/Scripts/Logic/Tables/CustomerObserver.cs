using MainGame.Scripts.Logic;
using MainGame.Scripts.Logic.Npc;
using UnityEngine;

public class CustomerObserver : MonoBehaviour
{
    [SerializeField] private TriggerObserver _customerTrigger;
    
    private bool _hasCustomer;
    
    public Customer Customer { get; private set; }
    public bool HasUnServCustomer => _hasCustomer && Customer.IsServed == false;
    
    private void OnEnable()
    {
        _customerTrigger.CollusionEntered += TakeCustomer;
        _customerTrigger.CollusionExited += UnTakeCustomer;
    }

    private void OnDisable()
    {
        _customerTrigger.CollusionEntered -= TakeCustomer;
        _customerTrigger.CollusionExited -= UnTakeCustomer;
    }

    private void TakeCustomer(Collider collider)
    {
        if (collider.TryGetComponent(out Customer customer))
        {
            Customer = customer;
            _hasCustomer = true;
        }
    }

    private void UnTakeCustomer(Collider collider)
    {
        if (collider.TryGetComponent(out Customer customer))
        {
            Customer = null;
            _hasCustomer = false;
        }
    }
}