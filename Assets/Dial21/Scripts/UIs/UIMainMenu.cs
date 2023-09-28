using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dial21
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _ruleBtn;
        [SerializeField] private Button _exitBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _totalGameText;
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private TextMeshProUGUI _loseText;
        [SerializeField] private TextMeshProUGUI _winStrikeText;

        private void Start()
        {
            _totalGameText.text = $"Сыграно партий {GameManager.Instance.PlayTime}";
            _winText.text = $"Всего побед {GameManager.Instance.WinCount}";
            _loseText.text = $"Всего порaжений {GameManager.Instance.LoseCount}";
            _winStrikeText.text = $"Побед подряд {GameManager.Instance.WinStreak}";

            _playBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Loader.Load(Loader.Scene.GameplayScene);
            });

            _ruleBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayRule01Menu(true);
            });

            _exitBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Application.Quit();

                // For the Unity Editor, this will not quit the application. It will stop the editor's play mode.
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _ruleBtn.onClick.RemoveAllListeners();
            _exitBtn.onClick.RemoveAllListeners();
        } 
    }
}

