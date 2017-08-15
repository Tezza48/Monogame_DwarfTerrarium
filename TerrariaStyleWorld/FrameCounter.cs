using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaStyleWorld.Graphics
{
    static class FrameCounter
    {
        // number of seconds over which to average the framerate
        private const float SAMPLE_PERIOD = 1.0f;
        
        private static Queue<float> mFrameDeltaQueue;// seconds

        public static void Init()
        {
            mFrameDeltaQueue = new Queue<float>();
            mFrameDeltaQueue.Enqueue(1.0f);
        }

        public static void onDraw(float newDelta)
        {
            // if sum of times in queue > sample period
            // pop untils sum < sample period
            
            while (mFrameDeltaQueue.Sum() > SAMPLE_PERIOD)
            {
                mFrameDeltaQueue.Dequeue();
            }

            mFrameDeltaQueue.Enqueue(newDelta);
        }

        public static float GetAverageFramerate()
        {
            return 1/mFrameDeltaQueue.Average();
        }

    }
}
