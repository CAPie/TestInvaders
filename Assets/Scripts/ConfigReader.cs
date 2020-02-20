using UnityEngine;
using System.Collections.Generic;

public class ConfigReader
{
    private static readonly string PATH_TO_CONFIGS = "Configs/{0}";
    private static readonly Dictionary<string, DefenceItemData> defenceConfig = new Dictionary<string, DefenceItemData>();
    private static readonly Dictionary<string, EnemyData> enemyConfigs = new Dictionary<string, EnemyData>();
    private static readonly Dictionary<string, LevelData> levelsConfigs = new Dictionary<string, LevelData>();

    public static DefenceItemData DefenceItemData(NamedItem item)
    {
        var itemName = item.definedName;
        var fileName = string.Format(PATH_TO_CONFIGS, itemName);
        if (defenceConfig.ContainsKey(itemName))
        {
            return defenceConfig[itemName];
        }
        else
        {
            var fileContent = Resources.Load<TextAsset>(fileName);
            if (fileContent == null) {
                Debug.LogError("NO FILE: " + fileName);
                return null;
            }
            DefenceItemData data = null;
            try {
                data = JsonUtility.FromJson<DefenceItemData>(fileContent.ToString());
            }
            catch(System.ArgumentException e)
            {
                Debug.Log("NULL. " + fileName + ":" + fileContent.ToString() + "\n" + e);
            }

            defenceConfig.Add(itemName, data);
            return data;
        }
    }
    public static EnemyData EnemyData(NamedItem item)
    {
        var itemName = item.definedName;
        var fileName = string.Format(PATH_TO_CONFIGS, itemName);
        if (enemyConfigs.ContainsKey(itemName))
        {
            return enemyConfigs[itemName];
        }
        else
        {
            var fileContent = Resources.Load<TextAsset>(fileName);
            if (fileContent == null) {
                Debug.LogError("NO FILE: " + fileName);
                return null;
            }
            EnemyData data = null;
            try {
                data = JsonUtility.FromJson<EnemyData>(fileContent.ToString());
            }
            catch(System.ArgumentException e)
            {
                Debug.Log("NULL. " + fileName + ":" + fileContent.ToString() + "\n" + e);
            }

            enemyConfigs.Add(itemName, data);
            return data;
        }
    }

    public static LevelData LevelData(string levelItem)
    {
        var fileName = string.Format(PATH_TO_CONFIGS, levelItem);
        if (levelsConfigs.ContainsKey(levelItem))
        {
            return levelsConfigs[levelItem];
        }
        else
        {
            var fileContent = Resources.Load<TextAsset>(fileName);
            if (fileContent == null) {
                Debug.LogError("NO FILE: " + fileName);
                return null;
            }

            var data = JsonUtility.FromJson<LevelData>(fileContent.ToString());

            levelsConfigs.Add(levelItem, data);
            return data;
        }
    }
}
