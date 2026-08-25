using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Level
{
    [Range(1, 8)]
    public int maxPartsNone;

    [Range(0, 15)]
    public int maxPartsPlatform;

    [Range(0, 15)]
    public int maxPartsDeath;

    [Range(0, 4)]
    public int maxRotatorAllPlatform;

    [Range(0, 5)]
    public int maxRotatorDeathPlatform;

    [Range(0, 5)]
    public int maxWallsPlatform;

    [Range(0, 5)]
    public int maxUpDownPlatform;
}

[Serializable]
public class Order
{
    public List<GameObject> order;
}

public class ProceduralHelix : MonoBehaviour
{
    private int partsNone;
    private int partsPlatform;
    private int partsDeath;
    private int partsRotatorAllPlatform;
    private int partsRotatorDeathPlatform;
    private int partsWallsPlatform;
    private int partsUpDownPlatform;

    private int arrayCompleted = 0;

    public float changeRotation = 45;
    private float changeRoationCode;
    public float changeY;
    private float changeYCode;
    public float startYPoint;

    public GameObject PassCheck;
    public List<GameObject> Parts;

    public List<Level> levels;
    public List<Order> orders;

    private bool usedTriggerEnter;

    private int currentLevel;    

    void Start()
    {
        DetermineLevel();
        CreateParts();
    }

    void CreateParts()
    {
        for (int m = 0; m < orders.Count; m++) //Nueva plataforma. Cantidad de elementos en Orders
        {
            arrayCompleted = 0;
            GameObject newCheck = Instantiate(PassCheck, new Vector3(transform.position.x, transform.position.y + startYPoint - changeYCode - 0.5f, transform.position.z), Quaternion.identity, transform);

            //Platform parts
            partsNone = UnityEngine.Random.Range(1, levels[currentLevel].maxPartsNone);
            partsPlatform = UnityEngine.Random.Range(0, levels[currentLevel].maxPartsPlatform);
            partsDeath = UnityEngine.Random.Range(0, levels[currentLevel].maxPartsDeath);
            partsRotatorAllPlatform = UnityEngine.Random.Range(0, levels[currentLevel].maxRotatorAllPlatform);
            partsRotatorDeathPlatform = UnityEngine.Random.Range(0, levels[currentLevel].maxRotatorDeathPlatform);
            partsWallsPlatform = UnityEngine.Random.Range(0, levels[currentLevel].maxWallsPlatform);
            partsUpDownPlatform = UnityEngine.Random.Range(0, levels[currentLevel].maxUpDownPlatform);

            for (int l = 0; l < partsNone; l++)
            {
                orders[m].order.Add(Parts[0]);
            }
            for (int j = 0; j < partsPlatform; j++)
            {
                orders[m].order.Add(Parts[1]);
            }
            for (int k = 0; k < partsDeath; k++)
            {
                orders[m].order.Add(Parts[2]);
            }
            for (int k = 0; k < partsRotatorAllPlatform; k++)
            {
                orders[m].order.Add(Parts[3]);
            }
            for (int k = 0; k < partsRotatorDeathPlatform; k++)
            {
                orders[m].order.Add(Parts[4]);
            }
            for (int k = 0; k < partsWallsPlatform; k++)
            {
                orders[m].order.Add(Parts[5]);
            }
            for (int k = 0; k < partsUpDownPlatform; k++)
            {
                orders[m].order.Add(Parts[6]);
            }


            changeRoationCode = UnityEngine.Random.Range(0, 360);
            bool[] ConfirmationArray = new bool[orders[m].order.Count];
            for (int a = 0; a < 8 - partsNone; a++) //Crear las partes de una plataforma, en este caso 8 plataformas - la cantidad minima de partes none que quiero que haya
            {                        
                //Confirm the elements don't repeat
                int RandomOrder = UnityEngine.Random.Range(0, orders[m].order.Count);

                while (ConfirmationArray[RandomOrder] == true && arrayCompleted < orders[m].order.Count)
                {
                    RandomOrder = UnityEngine.Random.Range(0, orders[m].order.Count);
                } // this while will make a random number until it is a "new" number
                
                ConfirmationArray[RandomOrder] = true;
                arrayCompleted++;

                if (arrayCompleted < orders[m].order.Count && orders[m].order[RandomOrder] != null)
                {
                    Instantiate(orders[m].order[RandomOrder], new Vector3(transform.position.x, transform.position.y + startYPoint - changeYCode, transform.position.z), Quaternion.Euler(0, 0 + changeRoationCode, 0), newCheck.transform);
                }
                changeRoationCode += changeRotation;                            
            }
            changeYCode += changeY;
        }
    }


    void DetermineLevel()
    {
        if(GameManager.singleton.score < 5)
        {
            currentLevel = 0;
        }
        else if(GameManager.singleton.score >= 5 && GameManager.singleton.score < 15)
        {
            currentLevel = 1;
        }
        else if(GameManager.singleton.score >= 15 && GameManager.singleton.score < 30)
        {
            currentLevel = 2;
        }
        else if(GameManager.singleton.score >= 30 && GameManager.singleton.score < 42)
        {
            currentLevel = 3;
        }
        else if(GameManager.singleton.score >= 42 && GameManager.singleton.score < 54)
        {
            currentLevel = 4;
        }
        else if(GameManager.singleton.score >= 54 && GameManager.singleton.score <66)
        {
            currentLevel = 5;
        }
        else if(GameManager.singleton.score >= 66 && GameManager.singleton.score < 78)//add new mecanics
        {
            currentLevel = 6;
        }
        else if(GameManager.singleton.score >= 78 && GameManager.singleton.score < 90)
        {
            currentLevel = 7;
        }
        else if(GameManager.singleton.score >= 92 && GameManager.singleton.score < 104)
        {
            currentLevel = 8;
        }
        else if(GameManager.singleton.score >= 104 && GameManager.singleton.score < 116)
        {
            currentLevel = 9;
        }
        else if (GameManager.singleton.score >= 116 && GameManager.singleton.score < 128)
        {
            currentLevel = 10;
        }
        else if (GameManager.singleton.score >= 128 && GameManager.singleton.score < 140)
        {
            currentLevel = 11;
        }
        else if (GameManager.singleton.score >= 140 && GameManager.singleton.score < 152)
        {
            currentLevel = 12;
        }
        else if (GameManager.singleton.score >= 152 && GameManager.singleton.score < 164)
        {
            currentLevel = 13;
        }
        else if (GameManager.singleton.score >= 164 && GameManager.singleton.score < 176)
        {
            currentLevel = 14;
        }
        else if (GameManager.singleton.score >= 176 && GameManager.singleton.score < 188)
        {
            currentLevel = 15;
        }
        else if (GameManager.singleton.score >= 188 && GameManager.singleton.score < 200)
        {
            currentLevel = 16;
        }
        else if (GameManager.singleton.score >= 200 && GameManager.singleton.score < 212)
        {
            currentLevel = 17;
        }
        else if (GameManager.singleton.score >= 212 && GameManager.singleton.score < 214)
        {
            currentLevel = 18;
        }
        else if (GameManager.singleton.score >= 214 && GameManager.singleton.score < 226)
        {
            currentLevel = 19;
        }
        else
        {
            currentLevel = 20;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !usedTriggerEnter)
        {
            Instantiate(Resources.Load("Helix"), new Vector3(transform.position.x, transform.position.y - 72f, transform.position.z), transform.rotation);
            usedTriggerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(gameObject, 10);
        }
    }
}
