﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagementer : MonoBehaviour
{
    static public int Scene = 0;

    static public int Jouken = 0;

    [SerializeField] GameObject Hand; //見えている手
    [SerializeField] GameObject HandMesh; //見えている手のマテリアル関連
    [SerializeField] GameObject Demo; //デモモード管理
    [SerializeField] GameObject Cross; //正面の十字
    [SerializeField] GameObject AtariHantei;
    [SerializeField] GameObject Sphere;

    [SerializeField] Text PressKey;
    [SerializeField] Text Magnet;
    [SerializeField] Text Space;
    [SerializeField] Text HMD;

    [SerializeField] Image GrayBack; //磁石置くときのグレーバック

    [SerializeField] Material HandMeshA;//透明
    [SerializeField] Material HandMeshB;//白っぽい
    [SerializeField] Material Gray; //Sphereの色

    private bool demo = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Magnet == true)
        {
            Magnet.enabled = false;
            Space.enabled = false;
            HMD.enabled = false;
            GrayBack.enabled = false;
        }

        if (Demo)
        {
            demo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Scene == -1)
        {
            Jouken++;

            Scene++;
            Cross.gameObject.SetActive(true);
            Magnet.enabled = true;
            HMD.enabled = true;

            GrayBack.enabled = true;

            Invoke("ChangeUI", 30);

            HandMesh.GetComponent<Renderer>().material = HandMeshA;
        }

        if (Scene == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Jouken == 2)
                {

                }
                else
                {
                    Scene++;
                }
            }
        }

        if (Scene == 1)
        {
            Scene++;
            PressKey.enabled = false;
            Cross.gameObject.SetActive(false);
            Sphere.GetComponent<Renderer>().material = Gray;
        }

        if (Scene == 3)
        {
            AtariHantei.gameObject.SetActive(true);
        }

        if (Scene == 4)
        {
            Cross.gameObject.SetActive(true);
            Space.enabled = true;
            Magnet.enabled = true;

            GrayBack.enabled = true;
            Sphere.GetComponent<Renderer>().material = HandMeshA;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Scene++;

                Magnet.enabled = false;
                Space.enabled = false;
                Cross.gameObject.SetActive(false);

                GrayBack.enabled = false;

                HandMesh.GetComponent<Renderer>().material = HandMeshB;

                if (demo == true)
                {
                    Invoke("ChangeScene", 25);
                }
                else
                {
                    Invoke("ChangeScene", 70);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OVRManager.display.RecenterPose();
        }
    }

    void ChangeScene()
    {
        AtariHantei.gameObject.SetActive(true);

        Scene++;
    }

    void ChangeUI()
    {
        Magnet.enabled = false;
        HMD.enabled = false;
        GrayBack.enabled = false;

        PressKey.enabled = true;
    }
}
