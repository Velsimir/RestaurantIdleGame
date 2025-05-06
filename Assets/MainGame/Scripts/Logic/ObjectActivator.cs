using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainGame.Scripts.Logic
{
    public class ObjectActivator : MonoBehaviour
    {
        [InterfaceField(typeof(IActivatable))]
        [SerializeField] private Object _activatedObject;
        [SerializeField] private TriggerObserver _triggerObserver;
        
        private IActivatable ActivatableObject => _activatedObject as IActivatable;

        private void Awake()
        {
            _triggerObserver.CollusionEntered += Activate;
        }

        private void Activate(Collider obj)
        {
            if (obj.TryGetComponent(out Player player))
            {
                ActivatableObject.Activate();
            }
        }
    }
}