using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBaseMaster : MonoBehaviour
{
    public static FoodBaseMaster Instance { get; private set; }

    public GameObject foodPrefab;
    public int foodCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foodCount = 0;
    }

    public void CreateFood(int count)
    {
        for (int i = 0; i < count; i++)
        {
            foodCount += 1;

            var food = Instantiate(foodPrefab);
            food.transform.SetParent(transform, true);
            //float x = -0.4f + (0.02f * foodCount);
            float x = -0.4f + (0.1f * foodCount);
            food.transform.localPosition = new Vector3(x, 1, 0);
        }

    }
}
