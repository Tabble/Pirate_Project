namespace MapCreator
{
    using UnityEngine;

    public class MapCreatorTile : MonoBehaviour
    {

        public TileTypeCategory Category = TileTypeCategory.Water;
        public MeshRenderer Renderer;
        public Material Water;
        public Material Island;
        public Material ShallowWater;
        public Material Goal;
        public Material Start;

        public int PositionX = 0;
        public int PositionY = 0;

        private int matIndex = 0;
        private const int MAX_TILE_TYPE_CATEGORIES = 5;

        public void OnChangeTile()
        {
            if (matIndex < MAX_TILE_TYPE_CATEGORIES - 1)
            {
                matIndex++;
            }
            else
            {
                matIndex = 0;
            }
            ChangeCategory();
            ChangeMaterial();
        }

        private void ChangeCategory()
        {
            Category = (TileTypeCategory)matIndex;
        }

        public void ChangeMaterial(TileTypeCategory category = TileTypeCategory.Undefined, bool randomGeneragot = false)
        {
            if(category != TileTypeCategory.Undefined && randomGeneragot)
            {
                Category = category;
            }

            switch (Category)
            {
                case TileTypeCategory.Goal:
                    Renderer.material = Goal;
                    break;
                case TileTypeCategory.Island:
                    Renderer.material = Island;
                    break;
                case TileTypeCategory.ShallowWater:
                    Renderer.material = ShallowWater;
                    break;
                case TileTypeCategory.Start:
                    Renderer.material = Start;
                    break;
                case TileTypeCategory.Water:
                    Renderer.material = Water;
                    break;
            }
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
}