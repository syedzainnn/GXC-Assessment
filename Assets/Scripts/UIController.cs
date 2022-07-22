using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class UIController : MonoBehaviour
{
    
    #region Variables
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject objToInstantiate;
    [SerializeField] private int objectsToInstantiate;

    private string string1 = "abcdefghijklmnopqrstuvwxyz";
    private string string2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    private List<string> stringList = new List<string>();
    private TextMeshProUGUI[] UIArray;
    [SerializeField] private Transform temporaryTransform;

    #endregion

    
    
    #region Methods
    public void InstantiateUIObjects()
    {
        UIArray = new TextMeshProUGUI[objectsToInstantiate];
        
        for (var i = 0; i < objectsToInstantiate; i++)
        {
            var obj = Instantiate(objToInstantiate, Vector3.zero, Quaternion.identity, contentTransform);
            var char1 = string1[Random.Range(0, string1.Length)];
            var char2 = string2 [Random.Range(0, string2.Length)];
            var integer = Random.Range(0, 10);
            obj.name = string.Concat(char1, char2, integer);
            obj.transform.GetComponentInChildren<TextMeshProUGUI>().text = obj.name;
            stringList.Add(string.Concat(char1, char2, integer));
        }
    }

    public void Reorder()
    {
        stringList.Sort();
        
        UIArray = contentTransform.GetComponentsInChildren<TextMeshProUGUI>();
        
        // Set another parent as transform
        foreach (var Text in UIArray)
        {
            Text.transform.SetParent(temporaryTransform);
        }
        
        // Redirect to original Content Transform
        for (int i = 0; i < stringList.Count; i++)
        {
            var str = stringList[i];
            var UITransforms = temporaryTransform.GetComponentsInChildren<RectTransform>();
            for (var j = 0; j < UITransforms.Length; j++)
            {
                var arrayString = UITransforms[j].name.ToString();
                
                if (string.Compare(str, arrayString) == 0)
                {
                    UITransforms[j].transform.SetParent(contentTransform);
                    break;
                }
            }
        }
    }

    public void InsertRandom()
    {
        int rand = Random.Range(0, UIArray.Length);
        
        var char1 = string1[Random.Range(0, string1.Length)];
        var char2 = string2 [Random.Range(0, string2.Length)];
        var integer = Random.Range(0, 10);
        var objName = string.Concat(char1, char2, integer);


        stringList[rand] = objName;
        UIArray = contentTransform.GetComponentsInChildren<TextMeshProUGUI>();
        UIArray[rand].transform.name = objName;
        UIArray[rand].text = objName;
        
    }
    #endregion
}