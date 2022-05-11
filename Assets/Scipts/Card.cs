using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Card : MonoBehaviour
{
    public string SpritePath;

    public IAbility mainAbility;
    public IAbility additionalAbility;

    public bool isMainAbility;

    private TransformController transformController;

    void Start()
    {
        transformController = GetComponent<TransformController>();
    }

    public void SetCardInfo(CardInfo cardInfo)
    {
        this.SpritePath = cardInfo.SpritePath;
        this.mainAbility = cardInfo.mainAbility;
        this.additionalAbility = cardInfo.additionalAbility;
        isMainAbility = true;
    }

    public void ChangeAbility()
    {
        if (additionalAbility != null)
        {
            if (isMainAbility)
                isMainAbility = false;
            else isMainAbility = true;

            transformController.Rotate();
        }
    }

    public IAbility GetAbility()
    {
        if (isMainAbility)
            return mainAbility;
        else return additionalAbility;
    }
}
