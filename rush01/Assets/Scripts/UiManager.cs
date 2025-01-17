﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static string ToolTip;
    public GameObject InventoryUi;
    public GameObject Stats;
    public GameObject PlayerWeaponSlot;
    public GameObject Help;
    public GameObject talentUi;
    public GameObject LevelMaxUi;

    private void Awake()
    {
        PlayerWeaponSlot = GameObject.Find("PlayerWeaponSlot");
    }
    // Use this for initialization
    void Start()
    {
        InventoryUi = GameObject.Find("InventoryUI");
        talentUi = GameObject.Find("TalentUI");
        LevelMaxUi = GameObject.Find("LevelMaxUi");
        Help = GameObject.Find("GuidMenu");
        Stats = GameObject.Find("Stats");
        talentUi.SetActive(false);
        Help.SetActive(false);
        InventoryUi.SetActive(false);
        LevelMaxUi.SetActive(false);
        Stats.SetActive(false);
        ToolTip = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !talentUi.activeSelf && !Stats.activeSelf && !Help.activeSelf)
        {
            GameManager.gm.Dropping = !InventoryUi.activeSelf;
            GameManager.gm.Dropping = false;
            ToolTip = "";
            InventoryUi.SetActive(!InventoryUi.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.N) && !InventoryUi.activeSelf && !Stats.activeSelf && !Help.activeSelf)
        {
            talentUi.SetActive(!talentUi.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.C) && !InventoryUi.activeSelf && !talentUi.activeSelf && !Help.activeSelf)
        {
            Stats.SetActive(!Stats.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.H) && !InventoryUi.activeSelf && !talentUi.activeSelf && !Stats.activeSelf)
        {
            Help.SetActive(!Help.activeSelf);
        }

        if (PlayerWeaponSlot.transform.childCount > 0)
        {
            GameManager.gm.Player.Weapon = PlayerWeaponSlot.transform.GetChild(0).GetComponent<Weapons>();
        }
        else
        {
            GameManager.gm.Player.Weapon = null;
        }
    }

    private void OnGUI()
    {
        GUI.Label(
            new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, ToolTip.Length * 10,
                20 * ToolTip.Replace(System.Environment.NewLine, string.Empty).Length), ToolTip);
    }
}
