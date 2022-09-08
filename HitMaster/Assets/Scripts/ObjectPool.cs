using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    List<T> objects;

    GameObject prefab;

    public ObjectPool(GameObject prefab, int size)
    {
        objects = new List<T>();

        this.prefab = prefab;

        for (int i = 0; i < size; i++)
        {
            AddObject();
        }
    }

    public T GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].gameObject.activeSelf)
            {
                objects[i].gameObject.SetActive(true);

                return objects[i];
            }
        }

        T newT = AddObject();

        newT.gameObject.SetActive(true);

        return newT;
    }

    private T AddObject()
    {
        GameObject newObject = Object.Instantiate(prefab);

        T newT = newObject.GetComponent<T>();

        objects.Add(newT);

        newObject.SetActive(false);

        return newT;
    }
}
