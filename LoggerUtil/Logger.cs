using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace LoggerUtil
{
    public class Logger
    { 
        public const String SEPARATOR = " : ";
        public const String MODE = "MODE";
        public const String ALERT = "ALERT";

        private List<Alert> alerts;

        public List<Alert> Alerts
        {
            get { return alerts; }
            set { alerts = value; }
        }

        private List<Mode> modes;

        public List<Mode> Modes
        {
            get { return modes; }
            set { modes = value; }
        }

        private Notifier notifier;

        public Logger(List<Alert> alerts, List<Mode> modes)
        {
            this.Alerts = alerts;
            this.Modes = modes;

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        public void Log(String content)
        {
            foreach(Alert alert in Alerts)
            {
                switch (alert)
                {
                    case Alert.CONSOLE:
                        LogInConsole(LogType.ALERT, content);
                        break;
                    case Alert.TOAST:
                        notifier.ShowInformation(content);
                        break;
                    case Alert.MESSAGE_BOX:
                        break;
                    case Alert.OVERLAY:
                        break;
                    case Alert.NONE:
                        break;
                    default:
                        break;
                }
            }

            foreach (Mode mode in Modes)
            {
                switch (mode)
                {
                    case Mode.CONSOLE:
                        LogInConsole(LogType.MODE, content);
                        break;
                    case Mode.CURRENT_FOLDER:
                        break;
                    case Mode.TEMP_FOLDER:
                        break;
                    case Mode.NONE:
                        break;
                    default:
                        break;
                }
            }
        }

        public void Warning(String content)
        {
            foreach (Alert alert in Alerts)
            {
                switch (alert)
                {
                    case Alert.TOAST:
                        notifier.ShowWarning(content);
                        break;
                    case Alert.MESSAGE_BOX:
                        break;
                    case Alert.OVERLAY:
                        break;
                    case Alert.NONE:
                        break;
                    default:
                        break;
                }
            }
        }

        public void Error(String content)
        {
            foreach (Alert alert in Alerts)
            {
                switch (alert)
                {
                    case Alert.TOAST:
                        notifier.ShowError(content);
                        break;
                    case Alert.MESSAGE_BOX:
                        break;
                    case Alert.OVERLAY:
                        break;
                    case Alert.NONE:
                        break;
                    default:
                        break;
                }
            }
        }

        public void Success(String content)
        {
            foreach (Alert alert in Alerts)
            {
                switch (alert)
                {
                    case Alert.TOAST:
                        notifier.ShowSuccess(content);
                        break;
                    case Alert.MESSAGE_BOX:
                        break;
                    case Alert.OVERLAY:
                        break;
                    case Alert.NONE:
                        break;
                    default:
                        break;
                }
            }
        }

        private void LogInConsole(LogType logType, String content)
        {
            switch (logType)
            {
                case LogType.MODE:
                    Console.WriteLine(MODE + SEPARATOR + content);
                    break;
                case LogType.ALERT:
                    Console.WriteLine(ALERT + SEPARATOR + content);
                    break;
                default:
                    break;
            }
        }
    }
}
