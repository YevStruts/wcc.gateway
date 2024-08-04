using System.ComponentModel;

namespace wcc.gateway.kernel.Models.RatingGame.Enums
{
    public enum WinRule
    {
        [Description("Best of 1")]
        bo1,

        [Description("Best of 3")]
        bo3,

        [Description("Best of 5")]
        bo5,
    }
}
