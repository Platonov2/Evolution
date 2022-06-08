using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject panelListPlayers;
    public GameObject player0;
    public GameObject startGame;
    [SerializeField] public TextMeshProUGUI UIGameID;
    private List<GameObject> listPlayers = new List<GameObject>();
    public string gameID = "";
    //public bool owner = true;

    // Start is called before the first frame update
    void Start()
    {
        UIGameID.text = gameID;
        //startGame.gameObject.SetActive(owner);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            SceneManager.LoadScene(1);
        }
    }

    public void addPlayer(string name)
    {
        GameObject newPlayer = (GameObject)Instantiate(player0, panelListPlayers.transform);
        newPlayer.name = "Player_" + name;
        newPlayer.gameObject.SetActive(true);
        TextMeshProUGUI username = newPlayer.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        username.text = name;
        listPlayers.Add(newPlayer);        
    }

    public void removePlayer(string name)
    {
        foreach (GameObject elem in listPlayers)
        {
            if (elem.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text == name)
            {
                Destroy(elem);
                listPlayers.Remove(elem);
                break;
            }
        }
    }
}
