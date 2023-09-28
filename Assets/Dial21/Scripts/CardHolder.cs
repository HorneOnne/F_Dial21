using System.Collections.Generic;
using UnityEngine;

namespace Dial21
{
    public class CardHolder : MonoBehaviour
    {
        public static event System.Action OnCardAdded;
        [SerializeField] private List<CardObject> _cards = new List<CardObject>();


        public int GetCardValues()
        {
            if (_cards == null || _cards.Count == 0)
                return 0;

            int total = 0;
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].CardData.CardValue == 11) continue;
                total += _cards[i].CardData.CardValue;
            }
            total += CalculateAceCardValue();
            return total;
        }
        public int CalculateAceCardValue()
        {
            int numOfAce = 0;
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].CardData.CardValue == 11)
                {
                    numOfAce++;
                }
            }

            if (numOfAce == 1)
                return 11;
            else if (numOfAce > 1)
            {
                if (_cards.Count == 2)
                {
                    Debug.Log("WIN");
                }
            }
            return numOfAce;
        }


        public void AddCard(CardObject card)
        {
            _cards.Add(card);
            SortCards();
            OnCardAdded?.Invoke();
        }


        public void ShowCard()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Show();
            }
        }
        public void HideCard()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Hide();
            }
        }

        private void SortCards()
        {
            List<Vector2> cardPositions = GeneratePoints(transform.position, _cards.Count);
            // Sort the list based on x position
            cardPositions.Sort((a, b) => a.x.CompareTo(b.x));

            for (int i = 0; i < _cards.Count; ++i)
            {
                _cards[i].SpriteRenderer.sortingOrder = i;
                _cards[i].transform.position = cardPositions[i];
            }
        }

        private List<Vector2> GeneratePoints(Vector2 centerPoint, int numberOfPoints, float spacing = 0.5f)
        {
            Vector2 dirVector = Vector2.left;
            List<Vector2> points = new List<Vector2>();
            if (numberOfPoints % 2 == 0)
            {
                // Generate an even number of points
                for (int i = 0; i < numberOfPoints; i++)
                {
                    float offset = (i - numberOfPoints / 2 + 0.5f) * spacing;
                    Vector2 newPoint = centerPoint + dirVector * offset;
                    points.Add(newPoint);
                }
            }
            else
            {
                // Generate an odd number of points
                int middleIndex = numberOfPoints / 2;
                points.Add(centerPoint);

                for (int i = 1; i <= middleIndex; i++)
                {
                    float offset = i * spacing;
                    Vector2 leftPoint = centerPoint - dirVector * offset;
                    Vector2 rightPoint = centerPoint + dirVector * offset;

                    points.Add(leftPoint);
                    points.Add(rightPoint);
                }
            }
            return points;
        }
    }
}