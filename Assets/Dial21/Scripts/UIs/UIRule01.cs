using UnityEngine;
using UnityEngine.UI;

namespace Dial21
{
    public class UIRule01 : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _nextBtn;

        private void Start()
        {
            _nextBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayRule02Menu(true);
            });
        }

        private void OnDestroy()
        {
            _nextBtn.onClick.RemoveAllListeners();
        }
    }
}

