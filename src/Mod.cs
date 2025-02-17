using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System.Linq;

namespace CropsGetWetNearLakes
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            // O evento que ocorre diariamente no jogo (para verificar se as plantas precisam ser regadas)
            this.Helper.Events.GameLoop.DayStarted += this.OnDayStarted;
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            foreach (var location in Game1.locations)
            {
                // Percorrer todas as plantas no local
                foreach (var terrainFeature in location.terrainFeatures.Values)
                {
                    if (terrainFeature is HoeDirt hoeDirt && hoeDirt.plant != null)
                    {
                        // Verifica se o terreno está próximo da água (lago, fonte, etc.)
                        if (IsNearWater(terrainFeature))
                        {
                            hoeDirt.state.Value = HoeDirt.watered;  // Regar a planta
                        }
                    }
                }
            }
        }

        private bool IsNearWater(TerrainFeature terrainFeature)
        {
            // Verifica se a plantação está a 2 quadrados de uma fonte de água
            foreach (var location in Game1.locations)
            {
                foreach (var feature in location.terrainFeatures.Values)
                {
                    if (feature is WaterSource waterSource)
                    {
                        // Verifica a distância da plantação para a água
                        if (terrainFeature.getBoundingBox(location).Intersects(waterSource.getBoundingBox(location)))
                        {
                            return true;  // Se a planta estiver perto da água, retorna true
                        }
                    }
                }
            }
            return false;
        }
    }
}
