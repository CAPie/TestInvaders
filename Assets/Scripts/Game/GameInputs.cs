using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 KeyboardDirection { get; private set; }
    public bool IsPausePress { get; private set; } = false;


    void Update()
    {
        KeyboardDirection = GetKeybaordInputDirection();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) GameEvents.Instance.PlayerAttackInput();

        if (Input.GetKeyDown(KeyCode.Space)) GameEvents.Instance.NewGame();

        if (Input.GetKeyDown(KeyCode.Escape)) GameEvents.Instance.PauseGame();
    }

    private Vector3 GetKeybaordInputDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
