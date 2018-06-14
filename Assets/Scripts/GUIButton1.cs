using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIButton1 : MonoBehaviour {
    public Button Button1, Button2;
    public GameObject gameController;
    public Text text;
    //private string buildingName;
    //private GameObject[] buildings;

    void Start()
    {
        //buildingName = gameController.GetComponent<GameController>().buildingName;
        //buildings = gameController.GetComponent<GameController>().buildings;

        Button btn1 = Button1.GetComponent<Button>();
        Button btn2 = Button2.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick1);

        btn2.onClick.AddListener(TaskOnClick2);
    }

    void TaskOnClick1()
    {
        //Debug.Log("button1");
        gameController.GetComponent<GameController>().buildingName = gameController.GetComponent<GameController>().buildings[0].name;
        text.text = "Selected: Archer Tower\n      Cost:3";
    }

    void TaskOnClick2()
    {
        //Debug.Log("button2");
        gameController.GetComponent<GameController>().buildingName = gameController.GetComponent<GameController>().buildings[1].name;
        text.text = "Selected: Tank Factory\n      Cost:7";
    }
}
