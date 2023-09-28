using UnityEngine;


namespace Dial21
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;
        public static event System.Action OnPlaying;
        public static event System.Action OnWin;
        public static event System.Action OnGameOver;

        public enum GameState
        {
            WAITING,
            PLAYING,
            WIN,
            GAMEOVER,
            PAUSE,
            UNPAUSE,
            EXIT,
        }


        [Header("Properties")]
        [SerializeField] private GameState _currentState;
        private GameState _gameStateWhenPause;


        #region Properties
        public GameState CurrentState { get => _currentState; }
        #endregion


        #region Init & Events
        private void Awake()
        {
            Instance = this;

        }

        private void OnEnable()
        {
            OnStateChanged += SwitchState;
        }

        private void OnDisable()
        {
            OnStateChanged -= SwitchState;
        }

        private void Start()
        {
            ChangeGameState(GameState.WAITING);
            GameManager.Instance.PlayTime++;
        }
        #endregion



        public void ChangeGameState(GameState state)
        {
            if(state == GameState.PAUSE)
            {
                CacheGameStateWhenPause(_currentState);
            }
 
            _currentState = state;
            OnStateChanged?.Invoke();
        }

        public void CacheGameStateWhenPause(GameState state)
        {
            _gameStateWhenPause = state;
        }

        private void SwitchState()
        {
            switch (_currentState)
            {
                default: break;
                case GameState.WAITING:
                    StartCoroutine(Utilities.WaitAfter(0.1f, () =>
                    {
                        ChangeGameState(GameState.PLAYING);
                    }));


                    break;
                case GameState.PLAYING:

                    OnPlaying?.Invoke();
                    break;              
                case GameState.WIN:
                    GameManager.Instance.WinCount++;
                    GameManager.Instance.WinStreak++;
                    Dealer.Instance.ShowNPCCards();

                    OnWin?.Invoke();
                    SoundManager.Instance.PlaySound(SoundType.Win, false);
                    StartCoroutine(Utilities.WaitAfter(2.0f, () =>
                    {
                        Loader.Load(Loader.Scene.MenuScene);
                    }));

                    break;
                case GameState.GAMEOVER:
                    GameManager.Instance.LoseCount++;
                    GameManager.Instance.WinStreak = 0;
                    Dealer.Instance.ShowNPCCards();
                    OnGameOver?.Invoke();
                    SoundManager.Instance.PlaySound(SoundType.GameOver, false);
                    StartCoroutine(Utilities.WaitAfter(2.0f, () =>
                    {
                        Loader.Load(Loader.Scene.MenuScene);
                    }));

                    break;
                case GameState.PAUSE:
                    Time.timeScale = 0.0f;
                    break;
                case GameState.UNPAUSE:
                    Time.timeScale = 1.0f;
                    _currentState = _gameStateWhenPause;
                    break;
                case GameState.EXIT:
                    Time.timeScale = 1.0f;
                    break;
            }
        }
    }
}

