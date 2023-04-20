using System.Collections;
using System.Collections.Generic;
using Bluegravity.Common;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public List<string> taget;
    public List<PlayerPart> data;

    [ContextMenu("find")]
    public void find()
    {
        data = new List<PlayerPart>();


        var itrms = AllChilds(gameObject);

        foreach (var it in itrms)
        {
            if (it.gameObject.TryGetComponent(out PlayerPart hinge))
            {
                foreach (var t in taget)
                {
                    if (hinge.name.Contains(t))
                    {
                        data.Add(hinge);
                        break;
                    }
                }
            }

            Debug.Log("s");
        }
    }

    [ContextMenu("findGameObject")]
    public void findGameObject()
    {
        data.Clear();

        var itrms = AllChilds(gameObject);

        foreach (var it in itrms)
        {
            if (it.gameObject.TryGetComponent(out PlayerPart hinge))
            {
                foreach (var t in taget)
                {
                    if (hinge.gameObject.name.Contains(t))
                    {
                        data.Add(hinge);
                        break;
                    }
                }
            }

            Debug.Log("s");
        }
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }

        return result;
    }


    [ContextMenu("na")]
    public void make()
    {
        var itrms = AllChilds(gameObject);

        foreach (var it in itrms)
        {
            if (it.gameObject.TryGetComponent(out PlayerPart item))
            {
                item.sprite = item.gameObject.GetComponent<SpriteRenderer>();
            }
        }
    }
}