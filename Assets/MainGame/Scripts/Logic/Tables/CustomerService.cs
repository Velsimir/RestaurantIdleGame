using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ManagerObserver))]
[RequireComponent(typeof(CustomerObserver))]
[RequireComponent(typeof(ReceptionTablePizzaTaker))]
public class CustomerService : MonoBehaviour
{
    [SerializeField] private ManagerObserver _managerObserver;
    [SerializeField] private CustomerObserver _customerObserver;
    [SerializeField] private ReceptionTablePizzaTaker _receptionTablePizzaTaker;
    
    private bool _isWorking = true;
    private WaitUntil _waitHasUnServCustomer;
    private WaitForSeconds _wait;
    private Coroutine _servCoroutine;
    private float _delay = 0.1f;
    
    private void Awake()
    {
        _isWorking = true;
        _waitHasUnServCustomer = new WaitUntil(() => _customerObserver.Customer != null && _customerObserver.HasUnServCustomer);
        _wait = new WaitForSeconds(_delay);

        StopCurrentCoroutine();

        _servCoroutine = StartCoroutine(ServCustomers());
    }

    private IEnumerator ServCustomers()
    {
        while (_isWorking)
        {
            Debug.Log("начало");
            yield return _waitHasUnServCustomer;
            Debug.Log($"появился покупатель необслуженный {_customerObserver.Customer} {_waitHasUnServCustomer}");
            if (_managerObserver.HasManager && _receptionTablePizzaTaker.HasPizzas)
            {
                Debug.Log("Пробуем отдать пиицу");
                _customerObserver.Customer.TakePizza(_receptionTablePizzaTaker.GetPizza());
            }

            Debug.Log("ждем");
            yield return _wait;
        }
    }

    private void StopCurrentCoroutine()
    {
        if (_servCoroutine != null)
        {
            StopCoroutine(_servCoroutine);
            _servCoroutine = null;
        }
    }
}