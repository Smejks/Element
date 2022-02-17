using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using SaveData;

public static class SaveManager
{
    public async static Task<T> LoadObject<T>(string path)
    {
        string jsonData = await FBDatabase.db.RootReference.Child(path).GetValueAsync().ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return null;
            }
            return task.Result.GetRawJsonValue();

        });
        try {
            return JsonUtility.FromJson<T>(jsonData);
        }
        catch (Exception e) {
            Debug.LogError(e.ToString());
            return default(T);
        }
    }

    public async static Task<List<T>> LoadMultipleObjects<T>(string path)
    {
        List<string> jsonData = await FBDatabase.db.RootReference.Child(path).GetValueAsync().ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return null;
            }

            List<string> jsonData = new List<string>();
            foreach (var item in task.Result.Children) {
                jsonData.Add(item.GetRawJsonValue());
            }
            return jsonData;
        });

        if (jsonData == null)
            return null;

        List<T> objectList = new List<T>();

        foreach (var item in jsonData) {
            objectList.Add(JsonUtility.FromJson<T>(item));
        }
        return objectList;
    }

    public async static Task<bool> SaveObject<T>(string path, T data)
    {
        Debug.Log("Saving Data...");
        string jsonData = JsonUtility.ToJson(data);

        return await FBDatabase.db.RootReference.Child(path).SetRawJsonValueAsync(jsonData).ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return false;
            }
            Debug.Log("Save Successful.");
            return true;
        });
    }

    public async static Task<bool> RemoveNode<T>(string path)
    {
        Debug.Log("Removing data...");

        return await FBDatabase.db.RootReference.Child(path).RemoveValueAsync().ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return false;
            }
            return true;
        });
    }

}
