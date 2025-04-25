using UnityEngine;

public class Block : MonoBehaviour
{
    private void Start()
    {
        SendMessageUpwards("OnBlockTouch", this);
    }
}