using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject panelNewOrJoin;
    public GameObject panelGameID;
    public GameObject panelExit;
    public GameObject panelUsername;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            OpenDialogExit();
        }
    }

    public void OpenDialogExit()
    {
        panelExit.gameObject.SetActive(true);
    }

    public void CloseDialogExit()
    {
        panelExit.gameObject.SetActive(false);
    }

    public void OpenDialogNewOrJoin()
    {
        panelNewOrJoin.gameObject.SetActive(true);
    }

    public void CloseDialogNewOrJoin()
    {
        panelNewOrJoin.gameObject.SetActive(false);
    }

    public void OpenDialogGameID()
    {
        CloseDialogNewOrJoin();
        panelGameID.gameObject.SetActive(true);
    }

    public void CloseDialogGameID()
    {
        panelGameID.gameObject.SetActive(false);
    }

    public void OpenDialogUsername()
    {
        CloseDialogNewOrJoin();
        CloseDialogGameID();
        panelUsername.gameObject.SetActive(true);
    }

    public void CloseDialogUsername()
    {
        panelUsername.gameObject.SetActive(false);
    }




}