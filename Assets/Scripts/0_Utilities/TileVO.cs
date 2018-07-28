[System.Serializable]
public class TileVO
{
    public TileTypeCategory Category = TileTypeCategory.Water;
    public int PositionX = 0;
    public int PositionY = 0;

    public TileVO()
    {

    }

    public TileVO(TileTypeCategory category, int x, int y)
    {
        Category = category;
        PositionX = x;
        PositionY = y;
    }

    public JSONObject GetTileJson()
    {
        JSONObject tileJson = new JSONObject();
        tileJson.AddField(GameConstants.CATEGORY_KEY, Category.ToString());
        tileJson.AddField(GameConstants.POSITION_X_KEY, PositionX.ToString());
        tileJson.AddField(GameConstants.POSITION_Y_KEY, PositionY.ToString());
        return tileJson;
    }
}