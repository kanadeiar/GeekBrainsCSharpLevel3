namespace MailSender.lib.Interfaces
{
    /// <summary> Сервис дополнений приложения </summary>
    public interface IAddinsService
    {
        ITextsAddin getTextsAddin();
    }
    /// <summary> Изменения в текстах приложения </summary>
    public interface ITextsAddin
    {
        string Title { get; set; }
        string Description { get; set; }
        string Status { get; set; }
    }
}
