using System.Collections.Generic;
using UnityEngine;

namespace Dial21
{
    public class DeckOfCards : MonoBehaviour
    {
        public static DeckOfCards Instance { get; private set; }
        [SerializeField] private List<CardData> _cards;
        private Queue<CardData> _cardsQueue = new Queue<CardData>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ShuffleDeckOfCards();
        }

        public CardData GetCardData()
        {
            return _cardsQueue.Dequeue();
        }

        private void ShuffleDeckOfCards()
        {
            Shuffle(_cards);
            foreach (var card in _cards)
            {
                _cardsQueue.Enqueue(card);
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}