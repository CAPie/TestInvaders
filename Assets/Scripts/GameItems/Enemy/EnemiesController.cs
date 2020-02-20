using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private float enemiesWidthSpacing;
    [SerializeField]
    private EnemiesColumn enemiesColumnPrefab;
    [SerializeField]
    private string[] levelsList;

    private Dictionary<int, EnemiesColumn> _columnByIndex;
    private Dictionary<string, LevelData> _allLevelsSetup;

    public LevelData CurrentLevelSetup { get; private set; }
    public float NextShotTimestamp { get; set; }
    public int MovementDirectionRight { get; set; } = 1;

    private void Awake()
    {
        _allLevelsSetup = new Dictionary<string, LevelData>();
        foreach (var level in levelsList)
        {
            var data = ConfigReader.LevelData(level);
            if (data != null && !_allLevelsSetup.ContainsKey(level)) _allLevelsSetup.Add(level, data);
        }
        _columnByIndex = new Dictionary<int, EnemiesColumn>();
    }

    public string InitNew()
    {
        var levelName = _allLevelsSetup.Keys.ToList()[Random.Range(0, _allLevelsSetup.Count)];
        Debug.Log("START LEVEL : " + levelName);
        return Init(levelName);
    }

    public string Init(string levelName)
    {
        var data = _allLevelsSetup[levelName];
        // return random level if provided not found
        CurrentLevelSetup = data != null ? data : _allLevelsSetup.Values.ToList()[Random.Range(0, _allLevelsSetup.Count)];

        InitColumnsAndFillWithEnemies();

        return levelName;
    }

    public EnemiesColumn GetRandomNotEmptyColumn()
    {
        List<EnemiesColumn> availableColumns = new List<EnemiesColumn>();
        foreach(var column in _columnByIndex)
        {
            if (column.Value.HasEmemiesInColumn())
            {
                availableColumns.Add(column.Value);
            }
        }

        if (availableColumns.Count > 0)
        {
            return availableColumns[Random.Range(0, availableColumns.Count)];
        }
        else
        {
            return null;
        }
    }

    public void WorldBorderHit()
    {
        foreach(var item in _columnByIndex)
        {
            item.Value.StepDown(CurrentLevelSetup.landingSpeed);
        }
        MovementDirectionRight *= -1;
    }

    private void InitColumnsAndFillWithEnemies()
    {
        transform.position = transform.position + new Vector3(-CurrentLevelSetup.enemyRows[0].enemyItems.Length * enemiesWidthSpacing / 2, 0, 0);

        for (var rowIndex = 0; rowIndex < CurrentLevelSetup.enemyRows.Length; rowIndex++)
        {
            var row = CurrentLevelSetup.enemyRows[rowIndex];
            for (var columnIndex = 0; columnIndex < row.enemyItems.Length; columnIndex++)
            {
                var columnPosition = new Vector3(
                        transform.position.x + enemiesWidthSpacing * columnIndex,
                        transform.position.y,
                        transform.position.z);

                if (!_columnByIndex.ContainsKey(columnIndex))
                {
                    var column = Instantiate<EnemiesColumn>(enemiesColumnPrefab, transform);
                    column.transform.position = columnPosition;
                    column.Init(this);

                    _columnByIndex.Add(columnIndex, column);
                }

                var enemy = ObjectPooller.Instance.GetFromPool(
                    row.enemyItems[columnIndex],
                    columnPosition
                ) as Enemy;
                if(enemy != null) _columnByIndex[columnIndex].SetEnemyInRow(enemy, rowIndex);
            }
        }
    }

    public void CleanUp()
    {
        var columns = _columnByIndex.ToList();
        foreach (var column in columns)
        {
            column.Value.CleanUp();
            _columnByIndex.Remove(column.Key);
        }

        _columnByIndex = null;
        Destroy(gameObject);
    }
}
