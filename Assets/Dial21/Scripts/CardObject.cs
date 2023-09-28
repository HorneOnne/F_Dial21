using UnityEngine;

namespace Dial21
{
    public class CardObject : MonoBehaviour
    {
        private SpriteRenderer _sr;
        [SerializeField] private Sprite _hideCardSprite;
        private CardData _cardData;


        #region Properties
        public CardData CardData { get => _cardData; }
        public SpriteRenderer SpriteRenderer { get => _sr; }
        #endregion

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        public void SetData(CardData cardData)
        {
            _cardData = cardData;
        }


        public void Hide()
        {
            _sr.sprite = _hideCardSprite;
        }

        public void Show()
        {
            _sr.sprite = _cardData.CardSprite;
        }
    }
}