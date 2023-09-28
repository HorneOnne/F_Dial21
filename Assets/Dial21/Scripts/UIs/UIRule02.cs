using UnityEngine;
using UnityEngine.UI;

namespace Dial21
{
    public class UIRule02 : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _homeBtn;

        private void Start()
        {
            _homeBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
            });
        }

        private void OnDestroy()
        {
            _homeBtn.onClick.RemoveAllListeners();
        }
    }
}

