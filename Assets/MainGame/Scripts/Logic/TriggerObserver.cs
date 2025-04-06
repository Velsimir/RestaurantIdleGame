using System;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
   [RequireComponent(typeof(Collider))]
   public class TriggerObserver : MonoBehaviour
   {
      public event Action<Collider> CollusionEntered;
      public event Action<Collider> CollusionExited;
      
      private void OnTriggerEnter(Collider other)
      {
         CollusionEntered?.Invoke(other);
      }

      private void OnTriggerExit(Collider other)
      {
         CollusionExited?.Invoke(other);
      }
   }
}
