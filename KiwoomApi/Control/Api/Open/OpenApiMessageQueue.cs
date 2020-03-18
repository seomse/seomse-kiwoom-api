using KiwoomApi.Control.Api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.Open
{
    /**
     * API 최대 호출 횟수 => 1초에 5번.
     * 1초에 5번만 실행하는 큐를 구현
     */
    class OpenApiMessageQueue : IDisposable
    {
        private Queue<string> messageList = new Queue<string>();
        private ManualResetEvent Controller = new ManualResetEvent(false);
        private Thread QueueThread = null; 
        private Object QueueLockObj = new object();
        private ApiCommunication api = new ApiCommunication();
        public bool IsAlive { get; private set; }

        public OpenApiMessageQueue() { 
            this.QueueThread = new Thread(this.ExecuteMessage) {
                IsBackground = true, Name = "MessageQueue" 
            }; 
            this.IsAlive = true; 
            this.QueueThread.Start(); 
        }
    

        //-------------------------------- 
        // Command 를 메세지큐에 넣음 
        //-------------------------------- 
        public void Enqueue(string message) {
            lock (QueueLockObj) { 
            // Queue 에 Command 를 Enqueue 
                this.messageList.Enqueue(message);
            }
            // 대기상태에 빠져 있을지 모르니, Thread 를 동작하게 만듦 
            this.Controller.Set(); 
        } 
    
        //-------------------------------- 
        // Queue 에 쌓여있는 Command 를 수행 
        //-------------------------------- 
        private void ExecuteMessage() {
            try {
                while (IsAlive) {
                    // Queue 에 있는 모든 Command 를 수행 
                    while (this.messageList.Count > 0) {
                        lock (QueueLockObj)
                        {
                            string message = this.messageList.Dequeue();
                            api.parseMessage(message);
                        }
                    } 
                    // 다 수행하고 나면, 대기상태로 진입 
                    this.Controller.Reset(); 
                    this.Controller.WaitOne(Timeout.Infinite);
                    // 초당 5번 실행 제한
                    Thread.Sleep(201);
                } 
            } catch (ThreadAbortException) {
            }
        }
        public void Dispose() {
            this.IsAlive = false; 
            this.Controller.Set(); 
            this.QueueThread.Abort(); 
        }
    }
}
