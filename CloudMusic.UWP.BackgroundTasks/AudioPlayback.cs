using Windows.ApplicationModel.Background;
namespace CloudMusic.UWP.BackgroundTasks
{
    public sealed class AudioPlayback : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            taskInstance.Task.Completed += TaskCompleted;
        }
        void TaskCompleted(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            _deferral.Complete();
        }
    }
}
