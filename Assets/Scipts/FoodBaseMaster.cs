using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBaseMaster : MonoBehaviour
{
    public static FoodBaseMaster Instance { get; private set; }

    public GameObject redFoodPrefab;
    public GameObject blueFoodPrefab;
    public int foodCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foodCount = 0;
    }

    public void CreateRedFood(int count)
    {
        for (int i = 0; i < count; i++)
        {
            foodCount += 1;

            var food = Instantiate(redFoodPrefab);
            food.transform.SetParent(transform, true);
            float x = -0.4f + (0.1f * foodCount);
            food.transform.localPosition = new Vector3(x, 1, 0);
        }
    }

    public GameObject GetRedFood()
    {
        var food = this.transform.Find("RedFoodPrefab(Clone)").gameObject;
        return food;
    }

    public GameObject GetBlueFood()
    {
        var food = Instantiate(blueFoodPrefab);

        return food;
    }
}
