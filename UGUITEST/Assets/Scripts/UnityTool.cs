using System.IO;
using LitJson;

public class UnityTool
{
    private static UnityTool instance;
    public static UnityTool Instance
    {
        get { return instance ?? (instance = new UnityTool()); }
    }

    private UnityTool()
    {
        
    }

    public void SaveData(string path,object data)
    {
        string jsonStr = JsonMapper.ToJson(data);
        File.WriteAllText(path, jsonStr);
    }
    public T LoadData<T>(string path)
    {
        if (!File.Exists(path))
        {
            if (default(T) == null)
            {
                return System.Activator.CreateInstance<T>();
            }
            return default(T);
        }
        string jsonStr = File.ReadAllText(path);
        T t= JsonMapper.ToObject<T>(jsonStr);
        if (t == null)
        {
            return System.Activator.CreateInstance<T>();
        }
        else
        {
            return t;
        }
        
    }
}
