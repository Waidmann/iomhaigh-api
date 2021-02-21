using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iomhiagh_api.mediator
{
    public class SkillInteractionTextLogNotificationHandler : INotificationHandler<SkillInteractiondNotificationMessage>
    {
        public static string LOG_FILE = "log/log_iomhaigh_{0}.txt";

        private readonly StreamWriter _writer;

        public SkillInteractionTextLogNotificationHandler()
        {
            System.IO.Directory.CreateDirectory("log");
            _writer = new StreamWriter(String.Format(LOG_FILE, DateTime.Now.ToString("yyyy-MM-dd_hh-mm-tt")));
        }

        public Task Handle(SkillInteractiondNotificationMessage notification, CancellationToken cancellationToken)
        {
            _writer.WriteLine(notification.ToString());
            _writer.Flush();

            return Task.CompletedTask;
        }
    }
}
