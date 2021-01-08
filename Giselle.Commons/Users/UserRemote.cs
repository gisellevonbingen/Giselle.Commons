using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giselle.Commons.Users
{
    public class UserRemote : UserAbstract, IDisposable
    {
        private readonly Queue<string> InputQueue;
        private readonly ManualResetEventSlim InputEvent;

        public event EventHandler<string> MessageReceived;

        public UserRemote()
        {
            this.InputQueue = new Queue<string>();
            this.InputEvent = new ManualResetEventSlim();
        }

        protected virtual void Dispose(bool disposing)
        {
            this.InputEvent.DisposeQuietly();
        }

        public void EnqueueInput(string input)
        {
            lock (this.InputQueue)
            {
                this.InputQueue.Enqueue(input);
                this.InputEvent.ExecuteQuietly(e => e.Set());
            }

        }

        protected virtual void OnMessageReceived(string message)
        {
            this.MessageReceived?.Invoke(this, message);
        }

        public override void SendMessage(string message)
        {
            this.OnMessageReceived(message);
        }

        protected override string OnReadInput()
        {
            lock (this.InputQueue)
            {
                this.InputEvent.ExecuteQuietly(e => e.Reset());

                if (this.InputQueue.Count > 0)
                {
                    var input = this.InputQueue.Dequeue();
                    return input;
                }

            }

            this.InputEvent.ExecuteQuietly(e => e.Wait());
            return this.OnReadInput();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        ~UserRemote()
        {
            this.Dispose(false);
        }

    }

}
