using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.State
{
    public class AudioPlayer
    {
        private State state;
        public AudioPlayer()
        {
            this.state = new ReadyState(this);
        }
        public void  ChangeState(State state)
        {
            this.state = state;
        }
        public void ClickSwitch() => this.state.ClickSwitch();
        public void ClickPlay() => state.ClickPlay();
        public void ClickNext() => this.state.ClickNext();
        public void ClickPrevious() => state.ClickPrevious();
        public void Stop() => Console.WriteLine("Stopping...");
        public void ForwardFor(int sec) => Console.WriteLine($"Fast forwarding for {sec} sec");
        public void BackwardFor(int sec) => Console.WriteLine($"Rewinding for {sec} sec");
        public void Start() => Console.WriteLine($"Starting playing");
        public void Next() => Console.WriteLine($"Switching to next record");
        public void Previous() => Console.WriteLine($"Switching to previous record");
    }
}
