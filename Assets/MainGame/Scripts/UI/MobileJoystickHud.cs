using UnityEngine;

namespace MainGame.Scripts.UI
{
    public class MobileJoystickHud : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _floatingJoystick;

        public FloatingJoystick FloatingJoystick => _floatingJoystick;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}