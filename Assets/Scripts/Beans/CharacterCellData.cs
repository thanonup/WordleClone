using UnityEngine;

public class CharacterCellData
{
    public Vector2 position;
    public CharacterCellType characterCellType;

    public CharacterCellData(Vector2 position, CharacterCellType characterCellType)
    {
        this.position = position;
        this.characterCellType = characterCellType;
    }
}
