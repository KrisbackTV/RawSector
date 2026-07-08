using UnityEngine;

public class VoxelBrush : MonoBehaviour
{
    [Header("Settings")]
    public byte blockType = 1;
    public Vector3Int brushSize = new Vector3Int(1, 1, 1);

    [Header("Visuals")]
    public Color addColor = new Color(0, 1, 0, 0.4f);    // Зеленый для добавления
    public Color removeColor = new Color(1, 0, 0, 0.4f); // Красный для удаления
    public Color paintColor = new Color(0, 0, 1, 0.4f);  // Синий для покраски

    // Рассчитываем позицию сетки
    public Vector3Int GetGridPos(Vector3 hitPoint, Vector3 normal, bool isAdding)
    {
        // Если мы ставим блок, смещаем точку в сторону нормали поверхности, 
        // чтобы кубик рисовался "ПОВЕРХ" блока, а не внутри него.
        Vector3 offset = isAdding ? normal * 0.5f : -normal * 0.5f;
        Vector3 finalPos = hitPoint + offset;

        return new Vector3Int(
            Mathf.FloorToInt(finalPos.x),
            Mathf.FloorToInt(finalPos.y),
            Mathf.FloorToInt(finalPos.z)
        );
    }
}