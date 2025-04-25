using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    private void OnBlockTouch(Block block)
    {
        Debug.Log($"Block touched: {block}");
    }
}