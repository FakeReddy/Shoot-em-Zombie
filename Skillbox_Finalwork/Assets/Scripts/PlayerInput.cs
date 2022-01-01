using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float InputHorizontal { get { return Input.GetAxisRaw(GlobalStringsVars.HorizontalAxis); } }
    public float InputVertical { get { return Input.GetAxisRaw(GlobalStringsVars.VerticalAxis); } }
    public bool InputFire { get { return Input.GetAxis(GlobalStringsVars.Fire1Inputs) != 0; } }
}