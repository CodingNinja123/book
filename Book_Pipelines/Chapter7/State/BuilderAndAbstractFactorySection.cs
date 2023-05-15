

using Book_Pipelines.Chapter7.State;
using System.Diagnostics;

namespace Book_Pipelines.Chapter7.State
{
    public class StateSection
    {
        public static void Main()
        {
            AudioPlayer player = new AudioPlayer();
            player.ClickPlay();
            player.ClickNext();
            player.ClickSwitch();
            player.ClickSwitch();
            player.ClickNext();
            player.ClickPrevious();
            player.ClickPlay();
        }
    }
}
