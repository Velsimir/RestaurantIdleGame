using MainGame.Scripts.Logic;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

public class ManagerObserver : MonoBehaviour
{
    [SerializeField] private TriggerObserver _managerTrigger;
    [SerializeField] private ReceptionTablePizzaTaker _receptionTablePizzaTaker;

    public bool HasManager { get; private set; }

    private void OnEnable()
    {
        _managerTrigger.CollusionEntered += SetManager;
        _managerTrigger.CollusionExited += UnSetManager;
    }

    private void OnDisable()
    {
        _managerTrigger.CollusionEntered -= SetManager;
        _managerTrigger.CollusionExited -= UnSetManager;
    }
    
    private void SetManager(Collider collider)
    {
        if (collider.transform.TryGetComponent(out Player player))
        {
            HasManager = true;
        }
    }

    private void UnSetManager(Collider collider)
    {
        if (collider.transform.TryGetComponent(out Player player))
        {
            HasManager = false;
        }
    }
}