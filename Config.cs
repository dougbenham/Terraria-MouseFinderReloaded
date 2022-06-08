using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MouseFinderReloaded
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static Config Instance;

        [Label("Draw radial grid around cursor")]
        [DefaultValue(true)]
        public bool DrawRadialGridAroundCursor;

        [Label("Draw line toward cursor")]
        [DefaultValue(false)]
        public bool DrawLineTowardCursor;

        [Label("Draw radial pointer toward cursor")]
        [DefaultValue(false)]
        public bool DrawRadialPointerTowardCursor;

        [Label("Draw crosshair")]
        [DefaultValue(true)]
        public bool DrawCrosshair;
        
        [Label("Alpha")]
        [DefaultValue(1)]
        [Range(0f, 1f)]
        public float Alpha;
    }
}