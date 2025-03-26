//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    private List<int> _collectionMemory = new List<int>();

    public int GetUnicElementFromCollection(List<int> collection, bool reset = false, int whisoutElement = -1)
    {
        if (reset == true || _collectionMemory == null || whisoutElement != -1)
        {
            _collectionMemory.Clear();
            _collectionMemory = new List<int>(collection);

            if (whisoutElement != -1)
                _collectionMemory.Remove(whisoutElement);

            return -1;
        }

        int result = _collectionMemory[Random.Range(0, _collectionMemory.Count)];
        _collectionMemory.Remove(result);

        return result;
    }

    public void DebugCollection(int no, int pos, int id, List<int> collection)
    {
        string str = no + "   pos " + pos + "   id " + id + "  //  ";

        foreach (var item in collection)
            str = str + " " + item;

        Debug.Log(str);
    }

}
