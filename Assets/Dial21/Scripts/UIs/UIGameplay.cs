using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dial21
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _passBtn;
        [SerializeField] private Button _moreBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private TextMeshProUGUI _npcScoreText;


        private void OnEnable()
        {
            CardHolder.OnCardAdded += LoadPlayerScore;
            GameplayManager.OnWin += LoadNPCScore;
            GameplayManager.OnGameOver += LoadNPCScore;
        }

        private void OnDisable()
        {
            CardHolder.OnCardAdded -= LoadPlayerScore;
            GameplayManager.OnWin -= LoadNPCScore;
            GameplayManager.OnGameOver -= LoadNPCScore;
        }

        private void Start()
        {
            _passBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Dealer.Instance.AutoPlay = true;
            });

            _moreBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                if (GameplayManager.Instance.CurrentState != GameplayManager.GameState.PLAYING) return;
                Dealer.Instance.DistrubuteCardForPlayer();
            });

        }

        private void OnDestroy()
        {
            _passBtn.onClick.RemoveAllListeners();
            _moreBtn.onClick.RemoveAllListeners();
        }

        private void LoadPlayerScore()
        {
            _playerScoreText.text = Dealer.Instance.Player.GetCardValues().ToString();
        }
        private void LoadNPCScore()
        {
            _npcScoreText.text = Dealer.Instance.NPC.GetCardValues().ToString();
        }
    }
}

