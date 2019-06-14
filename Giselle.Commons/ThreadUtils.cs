using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class ThreadUtils
    {
        public static bool IsOtherThread(Thread thread)
        {
            return thread != null && thread != Thread.CurrentThread;
        }

        public static void InterruptAndJoin(Thread thread)
        {
            if (IsOtherThread(thread) == true)
            {
                thread.Interrupt();
                thread.Join();
            }

        }

        public static void Interrupt(Thread thread)
        {
            if (IsOtherThread(thread) == true)
            {
                thread.Interrupt();
            }

        }

        public static void AbortAndJoin(Thread thread)
        {
            if (IsOtherThread(thread) == true)
            {
                thread.Abort();
                thread.Join();
            }

        }

        public static void Abort(Thread thread)
        {
            if (IsOtherThread(thread) == true)
            {
                thread.Abort();
            }

        }

        public static void Join(Thread thread)
        {
            if (IsOtherThread(thread) == true)
            {
                thread.Join();
            }

        }

    }

}