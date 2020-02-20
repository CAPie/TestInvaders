using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour
{
    [SerializeField]
    private float worldRange;
    [SerializeField]
    private GameObject worldRangeLeftTrigger;
    [SerializeField]
    private GameObject worldRangeRightTrigger;
    [SerializeField]
    private EnemiesController enemiesPrefab;

    public int Score { get; private set; } = 0;
    public bool IsPause { get; private set; } = false;
    public bool IsGameStarted { get; private set; } = false;
    public float WorldRange { get { return worldRange; } }
    private Vector3 _enemiesSpawnPosition = new Vector3(0, 9, 0);
    private string _levelName;
    private EnemiesController _levelSetup;

    private void Awake()
    {
        GameEvents.Instance.OnEnemyKilled += OnEnemyKilled;
        GameEvents.Instance.OnPauseGame += OnPauseGame;
        GameEvents.Instance.OnGameOver += OnGameOver;
        GameEvents.Instance.OnNewGame += OnNewGame;

        GameEvents.Instance.GameStatsUpdate(this);
    }

    private void Start()
    {
        worldRangeLeftTrigger.transform.position = new Vector3(
            worldRangeLeftTrigger.transform.position.x - worldRange,
            worldRangeLeftTrigger.transform.position.y,
            worldRangeLeftTrigger.transform.position.z);
        worldRangeRightTrigger.transform.position = new Vector3(
            worldRangeRightTrigger.transform.position.x + worldRange,
            worldRangeRightTrigger.transform.position.y,
            worldRangeRightTrigger.transform.position.z);

        GameEvents.Instance.NewGame();
    }

    public bool IsColliderWorldRange(Collider2D collider)
    {
        return collider.gameObject == worldRangeLeftTrigger.gameObject
            || collider.gameObject == worldRangeRightTrigger.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if(enemy != null)
        {
            GameEvents.Instance.GameOver(false);
        }
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        Score += enemy.Score;

        GameEvents.Instance.GameStatsUpdate(this);
    }

    private void OnPauseGame()
    {
        if (IsGameStarted)
        {
            IsPause = !IsPause;
            GameEvents.Instance.PauseGameValue(IsPause);
        }
    }

    private void OnGameOver(bool win)
    {
        IsPause = true;
        IsGameStarted = false;

        GameEvents.Instance.PauseGameValue(IsPause);

        if (win)
        {
            GameEvents.Instance.OnNewGame += OnNewGame;
        }
        else
        {
            GameEvents.Instance.OnNewGame += OnNewGame;
        }
    }

    private void OnRestartGame()
    {
        GameEvents.Instance.OnNewGame -= OnRestartGame;
        Score = 0;

        InitLevel().Init(_levelName);
    }

    private void OnNewGame()
    {
        GameEvents.Instance.OnNewGame -= OnNewGame;

        _levelName = InitLevel().InitNew();
    }

    private EnemiesController InitLevel()
    {
        if (_levelSetup != null)
        {
            _levelSetup.CleanUp();
            _levelSetup = null;
        }

        _levelSetup = Instantiate<EnemiesController>(enemiesPrefab, transform);
        _levelSetup.transform.position = _enemiesSpawnPosition;

        IsPause = false;
        IsGameStarted = true;

        return _levelSetup;
    }
}
