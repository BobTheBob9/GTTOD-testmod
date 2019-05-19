using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class patch_CutsceneObject : CutsceneObject {
    private GameManager GM;
    private bool inScene;
    private float MainFOV;
    private bool InRange;
    private extern IEnumerator CutsceneFOV();
    private extern IEnumerator CutsceneEffect();
    private extern IEnumerator orig_CutsceneEffect();
    private extern IEnumerator orig_CutsceneFOV();
    private void Update() {
        if (!inScene && InRange && (Input.GetKeyDown(GM.Interact) || Input.GetButtonDown("X"))) {
            inScene = true;
            StartCoroutine(orig_CutsceneEffect());
            StartCoroutine(orig_CutsceneFOV());
            MainFOV = PlayerPrefs.GetFloat("FieldOfView");
            CutsceneCam.fieldOfView = PlayerPrefs.GetFloat("FieldOfView");
        }
        CutsceneCam.fieldOfView = MainFOV;
    }
}

class patch_ActivateAbility : ActivateAbility {
    private ac_CharacterController CharacterController;
    private InventoryScript Inventory;
    private void Start() {
        CharacterController = GameManager.GM.Player.GetComponent<ac_CharacterController>();
        Inventory = GameManager.GM.Player.GetComponent<InventoryScript>();
        WallRun = true;
        Vault = true;
        DoubleJump = true;
        Slide = true;
        Dash = true;
        AquireAbility();
    }
}