using System;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class ManagerObserver : IDisposable
    {
        private readonly TriggerObserver _managerTrigger;

        public ManagerObserver(TriggerObserver managerTrigger)
        {
            _managerTrigger = managerTrigger;
            _managerTrigger.CollusionEntered += SetManager;
            _managerTrigger.CollusionExited += UnSetManager;
        }

        public bool HasManager { get; private set; }

        public void Dispose()
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
}