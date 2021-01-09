using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreenState : State
{
    [SerializeField] private GameObject deadScreenUI;

    private int deadMenuIndex = 2;

    public override void OnEnable()
    {
        deadScreenUI.SetActive(false);
        base.OnEnable();
    }

    protected override void CreateActions()
    {
        onEnterActions.Add(EnterActions);
    }


    void EnterActions()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(deadMenuIndex);


    }
}
