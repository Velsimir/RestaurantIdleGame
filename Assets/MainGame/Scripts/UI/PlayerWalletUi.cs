using MainGame.Scripts.Logic.PlayerLogic;
using TMPro;
using UnityEngine;

namespace MainGame.Scripts.UI
{
    public class PlayerWalletUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
    
        private PlayerWallet _wallet;

        public void Initialize(PlayerWallet wallet)
        {
            _wallet = wallet;
        
            _coinsText.text = _wallet.Coins.ToString();
            _wallet.Updated += UpdateUi;
        }

        private void OnDisable()
        {
            _wallet.Updated -= UpdateUi;
        }

        private void UpdateUi()
        {
            _coinsText.text = _wallet.Coins.ToString();
        }
    }
}
