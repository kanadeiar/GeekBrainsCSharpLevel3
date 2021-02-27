namespace MailSender.lib.Interfaces
{
    /// <summary> Сервис отправщика почты по времени </summary>
    public interface ISchedulerMailService
    {
        ISchedulerMailSender GetScheduler(IMailSender MailSender);
    }
}
