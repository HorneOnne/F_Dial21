using UnityEngine;

namespace Dial21
{
    public class Dealer : MonoBehaviour
    {
        public static Dealer Instance { get; private set; }
        [SerializeField] private CardHolder _computer;
        [SerializeField] private CardHolder _player;


        [SerializeField] private CardObject _cardPrefab;

        #region Properties
        public CardHolder Player { get => _player; }
        public CardHolder NPC { get => _computer; }
        #endregion

        private bool _isPlayerTurn = true;
        public bool AutoPlay = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            DistrubuteCardForPlayer();
        }

        private void Update()
        {
            if (GameplayManager.Instance.CurrentState != GameplayManager.GameState.PLAYING) return;

            if (_isPlayerTurn == false)
            {
                _isPlayerTurn = true;
                if(_computer.GetCardValues() < 15)
                    DistrubuteCardForNPC();
            }

            if(AutoPlay)
            {
                if (_player.GetCardValues() > 21)
                {
                    if (_computer.GetCardValues() < 15)
                    {
                        DistrubuteCardForNPC();
                    }
                    else
                    {
                        CheckWinPositive();
                    }                       
                }
                else
                {
                    if(NPC.GetCardValues() < _player.GetCardValues())
                    {
                        DistrubuteCardForNPC();
                    }
                    else
                    {
                        GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                    }
                }
            }
        }



        public void DistrubuteCardForPlayer()
        {
            CardData cardData = DeckOfCards.Instance.GetCardData();
            CardObject card = Instantiate(_cardPrefab, _player.transform);
            card.SetData(cardData);
            card.Show();
            _player.AddCard(card);

            _isPlayerTurn = false;

            CheckWinNegative();
        }


        public void DistrubuteCardForNPC()
        {
            CardData cardData = DeckOfCards.Instance.GetCardData();
            CardObject card = Instantiate(_cardPrefab, _computer.transform);
            card.SetData(cardData);
            card.Hide();
            _computer.AddCard(card);

            CheckWinNegative();
        }


        public void CheckWinNegative()
        {
            if (Player.GetCardValues() > 21)
            {
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
            }

            if (NPC.GetCardValues() > 21)
            {
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WIN);
            }

        }

        public void CheckWinPositive()
        {
          
            if (_player.GetCardValues() <= 21)
            {
                if (_player.GetCardValues() >= _computer.GetCardValues())
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WIN);
                }
                else
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                }
            }
            else
            {
                if (_player.GetCardValues() >= _computer.GetCardValues())
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                }
                else
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WIN);
                }
            }
        }


        public void ShowNPCCards()
        {
            _computer.ShowCard();                
        }


    }
}