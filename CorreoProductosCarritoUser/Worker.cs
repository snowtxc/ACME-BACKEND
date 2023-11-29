using System.Threading.Tasks;

namespace CorreoProductosCarritoUser
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ITasks _task;
        private readonly Timer _timer;

        public Worker(ILogger<Worker> logger, ITasks task)
        {
            _logger = logger;
            _task = task;
            //_timer = new Timer(ExecuteTask, null, Timeout.Infinite, Timeout.Infinite);  //cada el primero de cada mes
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));  //cada 1 minuto
            SetTimer();
        }

        private void SetTimer()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayNextMonth = currentDate.AddMinutes(1);
            TimeSpan timeUntilFirstDay = firstDayNextMonth - currentDate;

            _timer.Change((long)timeUntilFirstDay.TotalMilliseconds, Timeout.Infinite);
        }

        private void ExecuteTask(object state)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _task.enviarEmailUsuariosConProductos();

                SetTimer();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the task.");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
