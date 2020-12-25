using UnityEngine;

public class Sinleton<T> : MonoBehaviour where T : Sinleton<T>
{
    private static T instance;

    private static readonly object locker = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = singleton.GetComponent<T>
                                      ().GetType().ToString();
                    }
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this.GetComponent<T>();
    }

    public void OnDestroy()
    {
        instance = null;
    }
}
