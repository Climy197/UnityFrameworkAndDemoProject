using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public static App Instance { get; private set; }
    AudioManager audioManager;
    EventManager eventManager;
    NetManager networkManager;
    ResManager resourceManager;
    UIManager uiManager;


    private void Awake()
    {
        Instance = this;
    }
}
